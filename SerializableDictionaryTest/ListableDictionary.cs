﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    [DebuggerDisplay("{{ToString(),nq}}")]
    [DebuggerTypeProxy(typeof(ListableDictionary<,>.DebugView))]
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

        static private bool _sortByItem;

        /// <summary>
        /// sort by item, when ToArray()
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>true: sort by item</item>
        /// <item>false: sort by key</item>
        /// </list>
        /// TItem must be either IComparable&lt;TItem&gt; or IComparable.
        /// </remarks>
        static protected bool SortByItem {
            get { return _sortByItem; }
            set {
                if (value && !typeof(TItem).GetInterfaces().Any(
                    x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IComparable<>) ||
                        x == typeof(IComparable))
                ) {
                    throw new ArgumentException(
                        typeof(TItem).Name + " must be either IComparable<" + typeof(TItem).Name + "> or IComparable",
                        typeof(TItem).Name
                    );
                }
                _sortByItem = value;
            }
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

        public TKey[] SortedKeys {
            get {
                var keys = this.Keys.ToArray();
                Array.Sort(keys);
                return keys;
            }
        }

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
            if (ExtraAttributeNames != null && ExtraAttributes.Count > 0) {
                var attr = new List<string>(ExtraAttributes.Count);
                foreach (var key in ExtraAttributeNames) {
                    if (ExtraAttributes.ContainsKey(key)) {
                        attr.Add("'" + key + "':'" + ExtraAttributes[key] + "'");
                    }
                }
                ret += ", ExtraAttribute:{" + String.Join(", ", attr) + "}";
            }
            if (ExtraElementManager != null && ExtraElement != null) {
                ret += ", " + ExtraElementManager.Name + ":{" + ExtraElement + "}";
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
            var keys = this.SortedKeys;
            var ret = new TItem[keys.Length];
            for (var i = 0; i < keys.Length; ++i) {
                ret[i] = this[keys[i]];
            }
            if (SortByItem) {
                Array.Sort(ret);
            }
            return ret;
        }

        public virtual void Add(TItem item) {
            this[item.getKey()] = item;
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
                    var value = reader.GetAttribute(key);
                    if (value != null) {
                        ExtraAttributes[key] = value;
                    }
                }
            }
            this.Clear();
            if (ExtraElementManager != null) {
                reader.Read();
                while (reader.NodeType == XmlNodeType.Comment) { reader.Skip(); }
                if (reader.NodeType != XmlNodeType.EndElement) {
                    if (reader.Name == ExtraElementManager.Name) {
                        ExtraElement = ExtraElementManager.Deserialize(reader.ReadSubtree());
                    } else {
                        var item = XmlHelper<TItem>.readElement(reader.ReadSubtree());
                        this.Add(item);
                    }
                }
            }
            while (reader.Read()) {
                while (reader.NodeType == XmlNodeType.Comment) { reader.Skip(); }
                if (reader.NodeType != XmlNodeType.EndElement) {
                    var item = XmlHelper<TItem>.readElement(reader.ReadSubtree());
                    this.Add(item);
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

        #region DebuggerDisplay

        [DebuggerDisplay("{value}", Name = "{key}")]
        internal class MyKeyValuePair<TKi, TVi> {

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private TKi key;

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            private TVi value;

            public MyKeyValuePair(TKi key, TVi value) {
                this.value = value;
                this.key = key;
            }
        }

        internal class DebugView {

            private ListableDictionary<TKey, TItem> _listableDictionary;

            public DebugView(ListableDictionary<TKey, TItem> listableDictionary) {
                this._listableDictionary = listableDictionary;
            }

            public bool AutoNewItem {
                get { return _listableDictionary.AutoNewItem; }
            }

            private string ExtraAttributeNamesString {
                get {
                    var extraAttributeNames = ListableDictionary<TKey, TItem>.ExtraAttributeNames;
                    if (extraAttributeNames==null) {
                        return "null";
                    }
                    var ret = "Length:" + extraAttributeNames.Length;
                    if (extraAttributeNames.Length > 0) {
                        ret += ", {" + String.Join(", ", extraAttributeNames) + "}";
                    }
                    return ret;
                }
            }

            [DebuggerDisplay("{ExtraAttributeNamesString,nq}")]
            public string[] ExtraAttributeNames {
                get { return ListableDictionary<TKey, TItem>.ExtraAttributeNames; }
            }

            private string ExtraAttributesString {
                get {
                    var extraAttributes = _listableDictionary.ExtraAttributes;
                    if (extraAttributes == null) {
                        return "null";
                    }
                    var ret = "Count:" + extraAttributes.Count;
                    if (extraAttributes.Count > 0) {
                        var attr = new string[extraAttributes.Count];
                        var i = 0;
                        foreach (var key in extraAttributes.Keys) {
                            attr[i++] = "'" + key + "':'" + extraAttributes[key] + "'";
                        }
                        ret += ", {" + String.Join(", ", attr) + "}";
                    }
                    return ret;
                }
            }

            [DebuggerDisplay("{ExtraAttributesString,nq}")]
            public MyKeyValuePair<string, string>[] ExtraAttributes {
                get {
                    if (_listableDictionary.ExtraAttributes == null) {
                        return null;
                    }
                    var ret = new MyKeyValuePair<string, string>[_listableDictionary.ExtraAttributes.Count];
                    var i = 0;
                    foreach (var key in _listableDictionary.ExtraAttributes.Keys) {
                        ret[i++] = new MyKeyValuePair<string, string>(key, _listableDictionary.ExtraAttributes[key]);
                    }
                    return ret;
                }
            }

            public object ExtraElement {
                get { return _listableDictionary.ExtraElement; }
            }

            public string Name {
                get { return _listableDictionary.Name; }
            }

            private string SortedKeysString {
                get {
                    var sortedKeys = _listableDictionary.SortedKeys;
                    if (sortedKeys == null) {
                        return "null";
                    }
                    var ret = "Length:" + sortedKeys.Length;
                    if (sortedKeys.Length > 0) {
                        ret += ", {" + String.Join(", ", sortedKeys) + "}";
                    }
                    return ret;
                }
            }

            [DebuggerDisplay("{SortedKeysString,nq}")]
            public TKey[] SortedKeys {
                get { return _listableDictionary.SortedKeys; }
            }

            [DebuggerDisplay("Count:{_this.Length}")]
            public MyKeyValuePair<TKey, TItem>[] _this {
                get {
                    var ret = new MyKeyValuePair<TKey, TItem>[_listableDictionary.Count];
                    var i = 0;
                    foreach (var key in _listableDictionary.Keys) {
                        ret[i++] = new MyKeyValuePair<TKey, TItem>(key, _listableDictionary[key]);
                    }
                    return ret;
                }
            }
        }

        #endregion
    }
}
