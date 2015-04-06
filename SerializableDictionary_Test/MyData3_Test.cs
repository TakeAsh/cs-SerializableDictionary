using System;
using System.IO;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData3_Test {

        private MyData3 _myData3a;

        private string _myData3aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<MyData3>\r\n" +
            "  <Name>Sample3</Name>\r\n" +
            "  <RGB key_type=\"RGBTypes\" value_type=\"SerializableDictionary\">\r\n" +
            "    <item key=\"R\">\r\n" +
            "      <SerializableDictionary key_type=\"Int32\" value_type=\"Double\">\r\n" +
            "        <item key=\"0\" value=\"0\" />\r\n" +
            "        <item key=\"20\" value=\"25\" />\r\n" +
            "        <item key=\"40\" value=\"50\" />\r\n" +
            "        <item key=\"60\" value=\"70\" />\r\n" +
            "        <item key=\"80\" value=\"85\" />\r\n" +
            "        <item key=\"100\" value=\"100\" />\r\n" +
            "      </SerializableDictionary>\r\n" +
            "    </item>\r\n" +
            "    <item key=\"G\">\r\n" +
            "      <SerializableDictionary key_type=\"Int32\" value_type=\"Double\">\r\n" +
            "        <item key=\"0\" value=\"0\" />\r\n" +
            "        <item key=\"20\" value=\"15\" />\r\n" +
            "        <item key=\"40\" value=\"30\" />\r\n" +
            "        <item key=\"60\" value=\"50\" />\r\n" +
            "        <item key=\"80\" value=\"75\" />\r\n" +
            "        <item key=\"100\" value=\"100\" />\r\n" +
            "      </SerializableDictionary>\r\n" +
            "    </item>\r\n" +
            "    <item key=\"B\">\r\n" +
            "      <SerializableDictionary key_type=\"Int32\" value_type=\"Double\">\r\n" +
            "        <item key=\"0\" value=\"0\" />\r\n" +
            "        <item key=\"20\" value=\"25\" />\r\n" +
            "        <item key=\"40\" value=\"50\" />\r\n" +
            "        <item key=\"60\" value=\"50\" />\r\n" +
            "        <item key=\"80\" value=\"75\" />\r\n" +
            "        <item key=\"100\" value=\"100\" />\r\n" +
            "      </SerializableDictionary>\r\n" +
            "    </item>\r\n" +
            "  </RGB>\r\n" +
            "  <CMYK key_type=\"CMYKTypes\" value_type=\"SerializableDictionary\">\r\n" +
            "    <item key=\"C\">\r\n" +
            "      <SerializableDictionary key_type=\"Double\" value_type=\"Double\">\r\n" +
            "        <item key=\"0\" value=\"0\" />\r\n" +
            "        <item key=\"0.2\" value=\"0.25\" />\r\n" +
            "        <item key=\"0.4\" value=\"0.5\" />\r\n" +
            "        <item key=\"0.6\" value=\"0.7\" />\r\n" +
            "        <item key=\"0.8\" value=\"0.85\" />\r\n" +
            "        <item key=\"1\" value=\"1\" />\r\n" +
            "      </SerializableDictionary>\r\n" +
            "    </item>\r\n" +
            "    <item key=\"M\">\r\n" +
            "      <SerializableDictionary key_type=\"Double\" value_type=\"Double\">\r\n" +
            "        <item key=\"0\" value=\"0\" />\r\n" +
            "        <item key=\"0.2\" value=\"0.15\" />\r\n" +
            "        <item key=\"0.4\" value=\"0.3\" />\r\n" +
            "        <item key=\"0.6\" value=\"0.5\" />\r\n" +
            "        <item key=\"0.8\" value=\"0.75\" />\r\n" +
            "        <item key=\"1\" value=\"1\" />\r\n" +
            "      </SerializableDictionary>\r\n" +
            "    </item>\r\n" +
            "    <item key=\"Y\">\r\n" +
            "      <SerializableDictionary key_type=\"Double\" value_type=\"Double\">\r\n" +
            "        <item key=\"0\" value=\"0\" />\r\n" +
            "        <item key=\"0.2\" value=\"0.25\" />\r\n" +
            "        <item key=\"0.4\" value=\"0.5\" />\r\n" +
            "        <item key=\"0.6\" value=\"0.5\" />\r\n" +
            "        <item key=\"0.8\" value=\"0.75\" />\r\n" +
            "        <item key=\"1\" value=\"1\" />\r\n" +
            "      </SerializableDictionary>\r\n" +
            "    </item>\r\n" +
            "    <item key=\"K\">\r\n" +
            "      <SerializableDictionary key_type=\"Double\" value_type=\"Double\">\r\n" +
            "        <item key=\"0\" value=\"0\" />\r\n" +
            "        <item key=\"0.2\" value=\"0.15\" />\r\n" +
            "        <item key=\"0.4\" value=\"0.3\" />\r\n" +
            "        <item key=\"0.6\" value=\"0.7\" />\r\n" +
            "        <item key=\"0.8\" value=\"0.85\" />\r\n" +
            "        <item key=\"1\" value=\"1\" />\r\n" +
            "      </SerializableDictionary>\r\n" +
            "    </item>\r\n" +
            "  </CMYK>\r\n" +
            "</MyData3>";

        static string _filePathMyData3 = @"../../Data/SampleMyData3.log";

        [SetUp]
        public void setup() {
            _myData3a = new MyData3() {
                Name = "Sample3",
                RGB = new SerializableDictionary<MyData3.RGBTypes, SerializableDictionary<int, double>>(){
                    {
                        MyData3.RGBTypes.R,
                        new SerializableDictionary<int, double>(){
                            {  0,   0}, { 20,  25}, { 40,  50}, { 60,  70}, { 80,  85}, {100, 100},
                        }
                    },
                    {
                        MyData3.RGBTypes.G,
                        new SerializableDictionary<int, double>(){
                            {  0,   0}, { 20,  15}, { 40,  30}, { 60,  50}, { 80,  75}, {100, 100},
                        }
                    },
                    {
                        MyData3.RGBTypes.B,
                        new SerializableDictionary<int, double>(){
                            {  0,   0}, { 20,  25}, { 40,  50}, { 60,  50}, { 80,  75}, {100, 100},
                        }
                    },
                },
                CMYK = new SerializableDictionary<MyData3.CMYKTypes, SerializableDictionary<double, double>>(){
                    {
                        MyData3.CMYKTypes.C,
                        new SerializableDictionary<double, double>(){
                            {0.00, 0.00}, {0.20, 0.25}, {0.40, 0.50}, {0.60, 0.70}, {0.80, 0.85}, {1.00, 1.00},
                        }
                    },
                    {
                        MyData3.CMYKTypes.M,
                        new SerializableDictionary<double, double>(){
                            {0.00, 0.00}, {0.20, 0.15}, {0.40, 0.30}, {0.60, 0.50}, {0.80, 0.75}, {1.00, 1.00},
                        }
                    },
                    {
                        MyData3.CMYKTypes.Y,
                        new SerializableDictionary<double, double>(){
                            {0.00, 0.00}, {0.20, 0.25}, {0.40, 0.50}, {0.60, 0.50}, {0.80, 0.75}, {1.00, 1.00},
                        }
                    },
                    {
                        MyData3.CMYKTypes.K,
                        new SerializableDictionary<double, double>(){
                            {0.00, 0.00}, {0.20, 0.15}, {0.40, 0.30}, {0.60, 0.70}, {0.80, 0.85}, {1.00, 1.00},
                        }
                    },
                },
            };
        }

        [TestCase]
        public void MyData3Test_ToXml() {
            var actual = XmlHelper<MyData3>.convertToString(_myData3a);
            Assert.AreEqual(_myData3aXml, actual);
        }

        [TestCase]
        public void MyData3Test_FromXml() {
            var actual = XmlHelper<MyData3>.convertFromString(_myData3aXml);
            Assert.AreEqual(_myData3aXml, XmlHelper<MyData3>.convertToString(actual));
        }

        [TestCase]
        public void MyData3Test_export() {
            _myData3a.export(_filePathMyData3);
            var actual = "";
            using (var reader = new StreamReader(_filePathMyData3)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_myData3aXml, actual);
        }

        [TestCase]
        public void MyData3Test_import() {
            var actual = MyData3.import(_filePathMyData3);
            Assert.AreEqual(_myData3aXml, XmlHelper<MyData3>.convertToString(actual));
        }
    }
}
