using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TakeAsh {
    public interface IListableDictionariable<TKey> {
        TKey getKey();
        void setKey(TKey key);
    }

    public class ListableDictionaryExtraElementManager {

        static private XmlSerializerNamespaces _blankNameSpace = new XmlSerializerNamespaces();

        private XmlSerializer _serializer;

        public string Name { get; private set; }

        static ListableDictionaryExtraElementManager() {
            _blankNameSpace.Add("", "");
        }

        public ListableDictionaryExtraElementManager(Type type, string name = null) {
            this.Name = !String.IsNullOrEmpty(name) ?
                name :
                type.Name;
            this._serializer = new XmlSerializer(type, new XmlRootAttribute(this.Name));
        }

        public object Deserialize(XmlReader reader) {
            return _serializer.Deserialize(reader);
        }

        public void Serialize(XmlWriter writer, object obj) {
            _serializer.Serialize(writer, obj, _blankNameSpace);
        }
    }

    public class ListableDictionary<TKey, TItem> :
        Dictionary<TKey, TItem>, IXmlSerializable, IListableDictionariable<string>
        where TKey : IComparable
        where TItem : IListableDictionariable<TKey>, new() {

        const string NameAttributeName = "Name";
        const string KeyTypeAttributeName = "KeyType";
        const string CountAttributeName = "Count";

        private bool _autoNewItem = true;

        /// <summary>
        /// add new item with the key, when the key don't exit.
        /// </summary>
        public bool AutoNewItem {
            get { return _autoNewItem; }
            set { _autoNewItem = value; }
        }

        /// <summary>
        /// Extra Attribute Names
        /// </summary>
        /// <remarks>
        /// When this property is not null,
        /// <list type="bullet">
        /// <item>ReadXml() use this as attribute names, and set them into ExtraAttributes.</item>
        /// <item>WriteXml() use this as attribute names, and output them from ExtraAttributes.</item>
        /// <item>ToString() output not only "Name" and "Count" but also ExtraAttributes.</item>
        /// </list>
        /// </remarks>
        static protected string[] ExtraAttributeNames { get; set; }

        protected virtual Dictionary<string, string> ExtraAttributes { get; set; }

        static protected ListableDictionaryExtraElementManager ExtraElementManager { get; set; }

        protected virtual object ExtraElement { get; set; }

        public virtual string Name { get; set; }

        public ListableDictionary() : base() {
            if (ExtraAttributeNames != null) {
                ExtraAttributes = new Dictionary<string, string>();
            }
        }

        public ListableDictionary(string name = null) : this() {
            this.Name = name;
        }

        public ListableDictionary(TItem[] items, string name = null) : this() {
            this.Name = name;
            FromArray(items);
        }

        public new TItem this[TKey key] {
            get {
                if (AutoNewItem && !base.ContainsKey(key)) {
                    var item = new TItem();
                    item.setKey(key);
                    base[key] = item;
                }
                return base[key];
            }
            set { base[key] = value; }
        }

        static public explicit operator ListableDictionary<TKey, TItem>(TItem[] source) {
            return new ListableDictionary<TKey, TItem>(source);
        }

        static public explicit operator TItem[](ListableDictionary<TKey, TItem> source) {
            return source.ToArray();
        }

        public override string ToString() {
            var ret = "";
            if (!String.IsNullOrEmpty(Name)) {
                ret += NameAttributeName + ":'" + Name + "', ";
            }
            ret += CountAttributeName + ":" + this.Count;
            if (ExtraAttributeNames != null) {
                foreach (var key in ExtraAttributeNames) {
                    if (ExtraAttributes.ContainsKey(key)) {
                        ret += ", " + key + ":'" + ExtraAttributes[key] + "'";
                    }
                }
            }
            if (ExtraElement != null) {
                ret += ", " + ExtraElement;
            }
            return ret;
        }

        static public ListableDictionary<TKey, TItem> FromXml(string xml) {
            return XmlHelper<ListableDictionary<TKey, TItem>>.convertFromString(xml);
        }

        public virtual string ToXml() {
            return XmlHelper<ListableDictionary<TKey, TItem>>.convertToString(this);
        }

        static public ListableDictionary<TKey, TItem> import(string fileName) {
            return XmlHelper<ListableDictionary<TKey, TItem>>.importFile(fileName);
        }

        public virtual bool export(string fileName) {
            return XmlHelper<ListableDictionary<TKey, TItem>>.exportFile(fileName, this);
        }

        public void FromArray(TItem[] items) {
            this.Clear();
            foreach (var item in items) {
                this[item.getKey()] = item;
            }
        }

        public TItem[] ToArray() {
            var keys = this.Keys.ToArray();
            Array.Sort(keys);
            var ret = new TItem[keys.Length];
            for (var i = 0; i < keys.Length; ++i) {
                ret[i] = this[keys[i]];
            }
            return ret;
        }

        public ListableDictionary<TKey, TItem> Add(TItem item) {
            this[item.getKey()] = item;
            return this;
        }

        #region IXmlSerializable Members

        public virtual XmlSchema GetSchema() {
            return null;
        }

        public virtual void ReadXml(XmlReader reader) {
            Name = reader.GetAttribute(NameAttributeName);
            var keyTypeName = reader.GetAttribute(KeyTypeAttributeName);
            if (keyTypeName != typeof(TKey).Name) {
                throw new XmlException("KeyType mismatch");
            }
            if (ExtraAttributeNames != null) {
                ExtraAttributes = new Dictionary<string, string>();
                foreach (var key in ExtraAttributeNames) {
                    ExtraAttributes[key] = reader.GetAttribute(key);
                }
            }
            this.Clear();
            if (ExtraElementManager != null) {
                reader.Read();
                if (reader.NodeType != XmlNodeType.EndElement) {
                    if (reader.Name == ExtraElementManager.Name) {
                        ExtraElement = ExtraElementManager.Deserialize(reader.ReadSubtree());
                    } else {
                        var item = XmlHelper<TItem>.readElement(reader.ReadSubtree());
                        this[item.getKey()] = item;
                    }
                }
            }
            while (reader.Read()) {
                if (reader.NodeType != XmlNodeType.EndElement) {
                    var item = XmlHelper<TItem>.readElement(reader.ReadSubtree());
                    this[item.getKey()] = item;
                } else {
                    reader.Skip();
                    break;
                }
            }
        }

        public virtual void WriteXml(XmlWriter writer) {
            if (Name != null) {
                writer.WriteAttributeString(NameAttributeName, Name);
            }
            writer.WriteAttributeString(KeyTypeAttributeName, typeof(TKey).Name);
            writer.WriteAttributeString(CountAttributeName, this.Count.ToString());
            if (ExtraAttributeNames != null) {
                foreach (var key in ExtraAttributeNames) {
                    string val;
                    if (ExtraAttributes.ContainsKey(key) &&
                        (val = ExtraAttributes[key]) != null) {
                        writer.WriteAttributeString(key, val);
                    }
                }
            }
            if (ExtraElementManager != null && ExtraElement != null) {
                ExtraElementManager.Serialize(writer, ExtraElement);
            }
            foreach (var item in ToArray()) {
                XmlHelper<TItem>.writeElement(writer, item);
            }
        }

        #endregion

        #region IListableDictionariable members

        public string getKey() {
            return Name;
        }

        public void setKey(string name) {
            this.Name = name;
        }

        #endregion
    }
}
