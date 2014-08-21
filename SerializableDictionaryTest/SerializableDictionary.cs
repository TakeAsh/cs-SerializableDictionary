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
        const string keyName = "key";
        const string valueName = "value";

        static TypeConverter converterKey = TypeDescriptor.GetConverter(typeof(TKey));
        static TypeConverter converterValue = TypeDescriptor.GetConverter(typeof(TValue));
        static XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
        static XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        static private SerializeTypes _serializeType;

        static public SerializeTypes SerializeType {
            get { return _serializeType; }
        }

        static SerializableDictionary() {
            bool isKeyStringifyable = converterKey.CanConvertTo(typeof(string));
            isKeyStringifyable &= converterKey.CanConvertFrom(typeof(string));
            bool isValueStringifyable = converterValue.CanConvertTo(typeof(string));
            isValueStringifyable &= converterValue.CanConvertFrom(typeof(string));
            _serializeType = (SerializeTypes)((isKeyStringifyable ? 2 : 0) + (isValueStringifyable ? 1 : 0));
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
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty) {
                return;
            }

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement) {
                reader.ReadStartElement(itemElementName);

                reader.ReadStartElement(keyName);
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement(valueName);
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
        }

        private void write_Element_Element(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteStartElement(keyName);
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement(valueName);
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

                writer.WriteAttributeString(valueName, this[key].ToString());

                writer.WriteStartElement(keyName);
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private void read_Attribute_Element(XmlReader reader) {
        }

        private void write_Attribute_Element(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteAttributeString(keyName, key.ToString());

                writer.WriteStartElement(valueName);
                valueSerializer.Serialize(writer, this[key]);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        private void read_Attribute_Attribute(XmlReader reader) {
            while (reader.Read()) {
                if (reader.Name == itemElementName) {
                    string strKey = reader.GetAttribute(keyName);
                    TKey key = strKey != null && converterKey != null ?
                        (TKey)converterKey.ConvertFromString(strKey) :
                        default(TKey);

                    string strValue = reader.GetAttribute(valueName);
                    TValue value = strValue != null && converterValue != null ?
                        (TValue)converterValue.ConvertFromString(strValue) :
                        default(TValue);

                    this.Add(key, value);
                }
            }
        }

        private void write_Attribute_Attribute(XmlWriter writer) {
            foreach (TKey key in this.Keys) {
                writer.WriteStartElement(itemElementName);

                writer.WriteAttributeString(keyName, key.ToString());

                writer.WriteAttributeString(valueName, this[key].ToString());

                writer.WriteEndElement();
            }
        }
    }
}
