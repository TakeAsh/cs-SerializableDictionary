using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TakeAsh;

namespace SerializableDictionary_Caller {
    public class ImExPorter<TKey, TValue> {

        public static SerializableDictionary<TKey, TValue> create(KeyValuePair<TKey, TValue>[] source) {
            var ret = new SerializableDictionary<TKey, TValue>();
            foreach (var kvp in source) {
                ret[kvp.Key] = kvp.Value;
            }
            return ret;
        }

        static public bool export(SerializableDictionary<TKey, TValue> obj, string fileName) {
            return XmlHelper<SerializableDictionary<TKey, TValue>>.exportFile(fileName, obj);
        }

        static public SerializableDictionary<TKey, TValue> import(string fileName) {
            return XmlHelper<SerializableDictionary<TKey, TValue>>.importFile(fileName);
        }
    }
}
