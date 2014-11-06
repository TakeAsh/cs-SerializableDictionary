using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TakeAsh {
    public interface IGetKey<TKey> {
        TKey getKey();
    }

    public class ListableDictionary<TKey, TItem> : Dictionary<TKey, TItem>, IXmlSerializable, IGetKey<string>
        where TKey : IComparable
        where TItem : IGetKey<TKey> {

        const string NameAttributeName = "Name";
        const string KeyTypeAttributeName = "KeyType";
        const string CountAttributeName = "Count";

        public virtual string Name { get; set; }

        public ListableDictionary(string Name = null) : base() {
            this.Name = Name;
        }

        public ListableDictionary(TItem[] items, string Name = null) : base() {
            this.Name = Name;
            FromArray(items);
        }

        static public explicit operator ListableDictionary<TKey, TItem>(TItem[] source) {
            return new ListableDictionary<TKey, TItem>(source);
        }

        static public explicit operator TItem[](ListableDictionary<TKey, TItem> source) {
            return source.ToArray();
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

        public System.Xml.Schema.XmlSchema GetSchema() {
            return null;
        }

        public void ReadXml(XmlReader reader) {
            Name = reader.GetAttribute(NameAttributeName);
            var keyTypeName = reader.GetAttribute(KeyTypeAttributeName);
            if (keyTypeName != typeof(TKey).Name) {
                throw new XmlException("KeyType mismatch");
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

        public void WriteXml(XmlWriter writer) {
            if (Name != null) {
                writer.WriteAttributeString(NameAttributeName, Name);
            }
            writer.WriteAttributeString(KeyTypeAttributeName, typeof(TKey).Name);
            writer.WriteAttributeString(CountAttributeName, this.Count.ToString());
            foreach (var item in ToArray()) {
                XmlHelper<TItem>.writeElement(writer, item);
            }
        }

        #endregion

        #region IGetKey member

        public string getKey() {
            return Name;
        }

        #endregion
    }
}
