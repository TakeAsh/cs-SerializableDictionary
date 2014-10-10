using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TakeAsh {
    public class XmlHelper<T> {
        static private XmlSerializer serializer = new XmlSerializer(typeof(T));
        static private XmlSerializerNamespaces blankNameSpace = new XmlSerializerNamespaces();

        static XmlHelper() {
            blankNameSpace.Add("", "");
        }

        static public T importFile(string fileName) {
            T ret = default(T);
            if (!File.Exists(fileName)) {
                Debug.Print("XmlHelper.importFile: Not Exist : " + fileName);
                return ret;
            }
            try {
                using(FileStream fs = new FileStream(fileName, FileMode.Open)) {
                    ret = (T)serializer.Deserialize(fs);
                }
            }
            catch(Exception ex) {
                Debug.Print(ex.Message);
            }
            return ret;
        }

        static public bool exportFile(string fileName, T obj) {
            bool ret = false;
            try {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                //settings.IndentChars = ("  ");
                settings.Encoding = Encoding.UTF8;
                using(XmlWriter writer = XmlWriter.Create(fileName, settings)) {
                    serializer.Serialize(writer, obj, blankNameSpace);
                }
                ret = true;
            }
            catch(Exception ex) {
                Debug.Print(ex.Message);
            }
            return ret;
        }

        static public T readElement(XmlReader reader) {
            return (T)serializer.Deserialize(reader);
        }

        static public void writeElement(XmlWriter writer, T value) {
            serializer.Serialize(writer, value, blankNameSpace);
        }
    }
}
