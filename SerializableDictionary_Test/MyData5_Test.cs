using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData5_Test {

        static private readonly MyData5[] _myData5aArray = new[]{
                new MyData5(){
                    Position = MyData5.Positions.Floor4,
                    Name = "Charlie",
                    HitPoint = 300,
                    Weapon = new Equipment(){
                        Name= "Hammer",
                        Owner = "Charlie",
                    },
                },
                new MyData5(){
                    Position = MyData5.Positions.Floor3,
                    Name = "Bravo",
                    HitPoint = 200,
                    Weapon = new Equipment(){
                        Name= "Mace",
                        Owner = "Bravo",
                    },
                },
                new MyData5(){
                    Position = MyData5.Positions.FloorB1,
                    Name = "Delta",
                    HitPoint = 500,
                    Weapon = new Equipment(){
                        Name= "Sword",
                        Owner = "Delta",
                    },
                },
                new MyData5(){
                    Position = MyData5.Positions.Floor1,
                    Name = "Alpha",
                    HitPoint = 100,
                    Weapon = new Equipment(){
                        Name= "Club",
                        Owner = "Alpha",
                    },
                },
            };

        static private MyData5s _myData5a = _myData5aArray.
            ToListableDictionary<MyData5s, MyData5.Positions, MyData5>();

        static private string _myData5aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<MyData5s KeyType=\"Positions\" Count=\"4\">\r\n" +
            "  <MyData5 Name=\"Alpha\" Position=\"Floor1\" HitPoint=\"100\">\r\n" +
            "    <Weapon Name=\"Club\" Owner=\"Alpha\" />\r\n" +
            "  </MyData5>\r\n" +
            "  <MyData5 Name=\"Bravo\" Position=\"Floor3\" HitPoint=\"200\">\r\n" +
            "    <Weapon Name=\"Mace\" Owner=\"Bravo\" />\r\n" +
            "  </MyData5>\r\n" +
            "  <MyData5 Name=\"Charlie\" Position=\"Floor4\" HitPoint=\"300\">\r\n" +
            "    <Weapon Name=\"Hammer\" Owner=\"Charlie\" />\r\n" +
            "  </MyData5>\r\n" +
            "  <MyData5 Name=\"Delta\" Position=\"FloorB1\" HitPoint=\"500\">\r\n" +
            "    <Weapon Name=\"Sword\" Owner=\"Delta\" />\r\n" +
            "  </MyData5>\r\n" +
            "</MyData5s>";

        static string _filePathMyData5 = @"../../Data/SampleMyData5.log";

        [TestCase]
        public void MyData5Test_IEnumerable() {
            var sorted = _myData5aArray.OrderBy(item => item.getKey()).ToList();
            var i = 0;
            foreach (var item in _myData5a) {
                Assert.AreEqual(sorted[i++], item);
            }
        }

        [TestCase]
        public void MyData5Test_ToXml() {
            var actual = XmlHelper<MyData5s>.convertToString(_myData5a);
            Assert.AreEqual(_myData5aXml, actual);
        }

        [TestCase]
        public void MyData5Test_FromXml() {
            var actual = XmlHelper<MyData5s>.convertFromString(_myData5aXml);
            Assert.AreEqual(_myData5aXml, XmlHelper<MyData5s>.convertToString(actual));
        }

        [TestCase]
        public void MyData5Test_export() {
            _myData5a.Export(_filePathMyData5);
            var actual = "";
            using (var reader = new StreamReader(_filePathMyData5)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_myData5aXml, actual);
        }

        [TestCase]
        public void MyData5Test_import() {
            var actual = (null as MyData5s).Import(_filePathMyData5);
            Assert.AreEqual(_myData5aXml, XmlHelper<MyData5s>.convertToString(actual));
        }
    }
}
