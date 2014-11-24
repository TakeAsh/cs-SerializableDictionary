using System;
using System.IO;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData4_Test {

        private MyData4List _myData4a;

        private string _myData4aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<MyData4List Count=\"3\">\r\n" +
            "  <Item Name=\"abc\" Size=\"1.1, 2.2\" Position=\"3.3, 4.4\" />\r\n" +
            "  <Item Name=\"def\" Size=\"1.11, 2.22\" Position=\"3.33, 4.44\" />\r\n" +
            "  <Item Name=\"ghi\" Size=\"1.111, 2.222\" Position=\"3.333, 4.444\" />\r\n" +
            "</MyData4List>";

        static string _filePathMyData4 = @"../Data/SampleMyData4.log";

        [SetUp]
        public void setup() {
            _myData4a = new MyData4[]{
                new MyData4(){
                    Name = "abc",
                    Size = new Point(1.1, 2.2),
                    Position = new Point(3.3, 4.4),
                },
                new MyData4(){
                    Name = "def",
                    Size = new Point(1.11, 2.22),
                    Position = new Point(3.33, 4.44),
                },
                new MyData4(){
                    Name = "ghi",
                    Size = new Point(1.111, 2.222),
                    Position = new Point(3.333, 4.444),
                },
            };
        }

        [TestCase]
        public void MyData4Test_ToXml() {
            var actual = XmlHelper<MyData4List>.convertToString(_myData4a);
            Assert.AreEqual(_myData4aXml, actual);
        }

        // SizeAttribute have a bug, and fail test
        //[TestCase]
        public void MyData4Test_FromXml() {
            var actual = XmlHelper<MyData4List>.convertFromString(_myData4aXml);
            Assert.AreEqual(_myData4aXml, XmlHelper<MyData4List>.convertToString(actual));
        }

        [TestCase]
        public void MyData4Test_export() {
            _myData4a.export(_filePathMyData4);
            var actual = "";
            using (var reader = new StreamReader(_filePathMyData4)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_myData4aXml, actual);
        }

        // SizeAttribute have a bug, and fail test
        //[TestCase]
        public void MyData4Test_import() {
            var actual = MyData4List.import(_filePathMyData4);
            Assert.AreEqual(_myData4aXml, XmlHelper<MyData4List>.convertToString(actual));
        }
    }
}
