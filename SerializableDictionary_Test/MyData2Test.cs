using System;
using System.ComponentModel;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData2Test {

        static string _myData2stringIn = "{ID:'100', Name:'山田', RegisteredDate:'2014-08-22', Height:'165.4', }";
        static string _myData2stringOut = "{ID:'100', Name:'山田', RegisteredDate:'2014/08/22 0:00:00', Height:'165.4', }";
        static string _myData2xml = 
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<MyData2>\r\n" +
            "  <ID>100</ID>\r\n" +
            "  <Name>山田</Name>\r\n" +
            "  <RegisteredDate>2014-08-22T00:00:00</RegisteredDate>\r\n" +
            "  <Height>165.4</Height>\r\n" +
            "</MyData2>";

        TypeConverter _myData2Converter;
        MyData2 _defaultMyData2;

        [SetUp]
        public void setup() {
            _myData2Converter = TypeDescriptor.GetConverter(typeof(MyData2));
            _defaultMyData2 = (MyData2)_myData2Converter.ConvertFromString(_myData2stringIn);
        }

        [TestCase]
        public void MyData2Test_FromString() {
            var actual = (MyData2)_myData2Converter.ConvertFromString(_myData2stringIn);
            Assert.AreEqual(_myData2stringOut, actual.ToString());
        }

        [TestCase]
        public void MyData2Test_ToXml() {
            var actual = XmlHelper<MyData2>.convertToString(_defaultMyData2);
            Assert.AreEqual(_myData2xml, actual);
        }

        [TestCase]
        public void MyData2Test_FromXml() {
            var actual = XmlHelper<MyData2>.convertFromString(_myData2xml);
            Assert.AreEqual(_myData2stringOut, actual.ToString());
        }
    }
}
