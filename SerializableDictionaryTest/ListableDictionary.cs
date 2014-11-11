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

    public class ListableDictionary<TKey, TItem> :
        Dictionary<TKey, TItem>, IXmlSerializable, IListableDictionariable<string>
        where TKey : IComparable
        where TItem : IListableDictionariable<TKey>, new() {

        const string NameAttributeName = "Name";
        const string KeyTypeAttributeName = "KeyType";
        const string CountAttributeName = "Count";

        static protected bool _autoNewItem = true;

        /// <summary>
        /// add new item with the key, when the key don't exit.
        /// </summary>
        /// <remarks>
        /// to disable this feature.
        /// <code>
        /// class delivered : ListableDictionary {
        ///     static delivered() {
        ///         AutoNewItem = false;
        ///     }
        /// }
        /// </code>
        /// </remarks>
        static protected bool AutoNewItem {
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

        public virtual string Name { get; set; }

        public ListableDictionary(string name = null) : base() {
            if (ExtraAttributeNames != null) {
                ExtraAttributes = new Dictionary<string, string>();
            }
            this.Name = name;
        }

        public ListableDictionary(TItem[] items, string name = null) : base() {
            if (ExtraAttributeNames != null) {
                ExtraAttributes = new Dictionary<string, string>();
            }
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
            return ret;
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
