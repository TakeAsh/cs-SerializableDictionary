using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TakeAsh;

namespace SerializableDictionary_Caller {
    public class MyData3 {
        public enum RGBTypes {
            R, G, B,
        }

        public enum CMYKTypes {
            C, M, Y, K,
        }

        public string Name { get; set; }
        public SerializableDictionary<RGBTypes, SerializableDictionary<int, double>> RGB { get; set; }
        public SerializableDictionary<CMYKTypes, SerializableDictionary<double, double>> CMYK { get; set; }

        public bool export(string fileName) {
            return XmlHelper<MyData3>.exportFile(fileName, this);
        }

        static public MyData3 import(string fileName) {
            return XmlHelper<MyData3>.importFile(fileName);
        }
    }
}
