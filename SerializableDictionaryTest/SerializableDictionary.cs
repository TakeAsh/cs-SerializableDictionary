using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TakeAsh {

    /// <summary>
    /// Serialize Class containing Dictionary member
    /// http://stackoverflow.com/questions/495647/
    /// </summary>
    /// <typeparam name="TKey">Type of Key</typeparam>
    /// <typeparam name="TValue">Type of Value</typeparam>
    [XmlRoot("Dictionary")]
    public class SerializableDictionary<TKey, TValue>
    : Dictionary<TKey, TValue>, IXmlSerializable {

        public enum SerializeTypes {
            Element_Element,        // Key:Element, Value:Element
            Element_Attribute,      // Key:Element, Value:Attribute
            Attribute_Element,      // Key:Attribute, Value:Element
            Attribute_Attribute,    // Key:Attribute, Value:Attribute
        };

        const string itemElementName = "item";
        const string keyElementName = "key";
        const string keyTypeAttributeName = "key_type";
        const string keyValueAttributeName = "key_value";
        const string valueElementName = "value";
        const string valueTypeAttributeName = "value_type";
        const string valueValueAttributeName = "value_value";

        static TypeConverter keyConverter = TypeDescriptor.GetConverter(typeof(TKey));
        static TypeConverter valueConverter = TypeDescriptor.GetConverter(typeof(TValue));
        static XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
        static XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        static private SerializeTypes _serializeType;

        static public SerializeTypes SerializeType {
            get { return _serializeType; }
        }

        static SerializableDictionary() {
            bool keyCanStringify = keyConverter.CanConvertFrom(typeof(string))
                && keyConverter.CanConvertTo(typeof(string));
            bool valueCanStringify = valueConverter.CanConvertFrom(typeof(string))
                && valueConverter.CanConvertTo(typeof(string));
            _serializeType = (SerializeTypes)((keyCanStringify ? 2 : 0) + (valueCanStringify ? 1 : 0));
        }

        #region IXmlSerializable Members

        public XmlSchema GetSchema() {
            return null;
        }

        public void ReadXml(XmlReader reader) {
            switch (SerializeType) {
                case SerializeTypes.Element_Element:
                    read_Element_Element(reader);
                    break;
                case SerializeTypes.Element_Attribute:
                    read_Element_Attribute(reader);
                    break;
                case SerializeTypes.Attribute_Element:
                    read_Attribute_Element(reader);
                    break;
                case SerializeTypes.Attribute_Attribute:
                    read_Attribute_Attribute(reader);
                    break;
            }
        }

        public void WriteXml(XmlWriter writer) {
            switch (SerializeType) {
                case SerializeTypes.Element_Element:
                    write_Element_Element(writer);
                    break;
                case SerializeTypes.Element_Attribute:
                    write_Element_Attribute(writer);
                    break;
                case SerializeTypes.Attribute_Element:
                    write_Attribute_Element(writer);
                    break;
                case SerializeTypes.Attribute_Attribute:
                    write_Attribute_Attribute(writer);
                    break;
            }
        }

        #endregion

        private void read_Element_Element(XmlReader reader) {
            while (reader.Read()) {
                if (reader.Name == itemElementName) {
                    XmlReader inner = reader.ReadSubtree();

                    inner.ReadToDescendant(keyElementName);
                    inner.ReadStartElement(keyElementName);
                    TKey key = (TKey)valueSerializer.Deserialize(inner);
                    inner.ReadEndElement();

                    inner.ReadStartElement(valueElementName);
                    TValue value = (TValue)valueSerializer.Deserialize(inner);
                    inner.ReadEndElement();
                    
                    inner.Close();

                    this.Add(key, value);
                }
            }
        }

        private void write_Element_Element(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteStartElement(keyElementName);
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement(valueElementName);
                valueSerializer.Serialize(writer, this[key]);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private void read_Element_Attribute(XmlReader reader) {
        }

        private void write_Element_Attribute(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteAttributeString(valueTypeAttributeName, typeof(TValue).Name);
                writer.WriteAttributeString(valueValueAttributeName, this[key].ToString());

                writer.WriteStartElement(keyElementName);
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private void read_Attribute_Element(XmlReader reader) {
            while (reader.Read()) {
                if (reader.Name == itemElementName) {
                    string strKey = reader.GetAttribute(keyValueAttributeName);
                    TKey key = strKey != null ?
                        (TKey)keyConverter.ConvertFromString(strKey) :
                        default(TKey);

                    XmlReader inner = reader.ReadSubtree();
                    inner.ReadToDescendant(valueElementName);
                    inner.ReadStartElement(valueElementName);
                    TValue value = (TValue)valueSerializer.Deserialize(inner);
                    inner.ReadEndElement();
                    inner.Close();

                    this.Add(key, value);
                }
            }
        }

        private void write_Attribute_Element(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteAttributeString(keyTypeAttributeName, typeof(TKey).Name);
                writer.WriteAttributeString(keyValueAttributeName, key.ToString());

                writer.WriteStartElement(valueElementName);
                valueSerializer.Serialize(writer, this[key]);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private void read_Attribute_Attribute(XmlReader reader) {
            while (reader.Read()) {
                if (reader.Name == itemElementName) {
                    string strKey = reader.GetAttribute(keyValueAttributeName);
                    TKey key = strKey != null ?
                        (TKey)keyConverter.ConvertFromString(strKey) :
                        default(TKey);

                    string strValue = reader.GetAttribute(valueValueAttributeName);
                    TValue value = strValue != null ?
                        (TValue)valueConverter.ConvertFromString(strValue) :
                        default(TValue);

                    this.Add(key, value);
                }
            }
        }

        private void write_Attribute_Attribute(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteAttributeString(keyTypeAttributeName, typeof(TKey).Name);
                writer.WriteAttributeString(keyValueAttributeName, key.ToString());

                writer.WriteAttributeString(valueTypeAttributeName, typeof(TValue).Name);
                writer.WriteAttributeString(valueValueAttributeName, this[key].ToString());

                writer.WriteEndElement();
            }
        }
    }
}
