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

    public class ListableDictionary<TKey, TItem> : Dictionary<TKey, TItem>, IXmlSerializable
        where TKey : IComparable
        where TItem : IGetKey<TKey> {

        const string CountAttributeName = "Count";

        public ListableDictionary() : base() { }

        public ListableDictionary(TItem[] items) : base() {
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

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema() {
            return null;
        }

        public void ReadXml(XmlReader reader) {
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
            writer.WriteAttributeString(CountAttributeName, this.Count.ToString());
            foreach(var item in ToArray()){
                XmlHelper<TItem>.writeElement(writer, item);
            }
        }

        #endregion
    }
}
