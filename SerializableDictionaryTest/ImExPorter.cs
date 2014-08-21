using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TakeAsh;

namespace SerializableDictionaryTest {
    public class ImExPorter<TKey, TValue> {

        public static SerializableDictionary<TKey, TValue> create(KeyValuePair<TKey, TValue>[] source) {
            var ret = new SerializableDictionary<TKey, TValue>();
            foreach (var kvp in source) {
                ret[kvp.Key] = kvp.Value;
            }
            return ret;
        }

        private static XmlSerializer serializer = new XmlSerializer(typeof(SerializableDictionary<TKey, TValue>));

        public static bool export(object obj, string fileName) {
            bool ret = false;
            try {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                //settings.IndentChars = ("\t");
                settings.Encoding = Encoding.UTF8;
                using (XmlWriter writer = XmlWriter.Create(fileName, settings)) {
                    serializer.Serialize(writer, obj);
                }
                ret = true;
            }
            catch (Exception ex) {
                Debug.Print(ex.Message);
            }
            return ret;
        }

        public static SerializableDictionary<TKey, TValue> import(string fileName) {
            SerializableDictionary<TKey, TValue> ret = default(SerializableDictionary<TKey, TValue>);
            try {
                using (FileStream fs = new FileStream(fileName, FileMode.Open)) {
                    ret = (SerializableDictionary<TKey, TValue>)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex) {
                Debug.Print(ex.Message);
            }
            return ret;
        }
    }
}
