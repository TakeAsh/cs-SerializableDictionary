using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TakeAsh;

namespace SerializableDictionaryTest {
    public class MyData3 {
        public enum RGBTypes {
            R, G, B,
        }

        public string Name { get; set; }
        public SerializableDictionary<RGBTypes, SerializableDictionary<int, double>> RGB { get; set; }

        private static XmlSerializer serializer = new XmlSerializer(typeof(MyData3));

        public bool export(string fileName) {
            bool ret = false;
            try {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                //settings.IndentChars = ("\t");
                settings.Encoding = Encoding.UTF8;
                using (XmlWriter writer = XmlWriter.Create(fileName, settings)) {
                    serializer.Serialize(writer, this);
                }
                ret = true;
            }
            catch (Exception ex) {
                Debug.Print(ex.Message);
            }
            return ret;
        }

        public static MyData3 import(string fileName) {
            var ret = default(MyData3);
            try {
                using (FileStream fs = new FileStream(fileName, FileMode.Open)) {
                    ret = (MyData3)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex) {
                Debug.Print(ex.Message);
            }
            return ret;
        }
    }
}
