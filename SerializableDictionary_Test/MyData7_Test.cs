using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData7_Test {

        private string _initialXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<MyData7 KeyType=\"String\" Count=\"5\">\r\n" +
            "  <Item Name=\"A\" />\r\n" +
            "  <Item Name=\"B\" />\r\n" +
            "  <Item Name=\"C\" />\r\n" +
            "  <Item Name=\"D\" />\r\n" +
            "  <Item Name=\"E\" />\r\n" +
            "</MyData7>";

        private string _expectedXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<MyData7 KeyType=\"String\" Count=\"5\">\r\n" +
            "  <Item Name=\"C\" />\r\n" +
            "  <Item Name=\"A\" />\r\n" +
            "  <Item Name=\"E\" />\r\n" +
            "  <Item Name=\"D\" />\r\n" +
            "  <Item Name=\"B\" />\r\n" +
            "</MyData7>";

        private string[] _names = { "A", "B", "C", "D", "E", };

        [TestCase]
        public void MyData7_SortByItem_Test() {
            var myData7a = new MyData7(
                _names.Select(name => new MyData7Item(name))
            );
            Assert.AreEqual(_initialXml, myData7a.ToXml());
            myData7a["B"].Index = 20;
            myData7a["C"].Index = -1;
            myData7a["D"].Index = 10;
            Assert.AreEqual(_expectedXml, myData7a.ToXml());
        }

        [TestCase]
        public void MyData7_ToMyData7_Test() {
            var myData7a = _names.
                Select(name => new MyData7Item(name)).
                ToMyData7();
            Assert.AreEqual(_initialXml, myData7a.ToXml());
            myData7a["B"].Index = 20;
            myData7a["C"].Index = -1;
            myData7a["D"].Index = 10;
            Assert.AreEqual(_expectedXml, myData7a.ToXml());
        }

        [TestCase]
        public void MyData7_ToListableDictionary_Test() {
            var myData7a = _names.
                Select(name => new MyData7Item(name)).
                ToListableDictionary<MyData7, string, MyData7Item>();
            Assert.AreEqual(_initialXml, myData7a.ToXml());
            myData7a["B"].Index = 20;
            myData7a["C"].Index = -1;
            myData7a["D"].Index = 10;
            Assert.AreEqual(_expectedXml, myData7a.ToXml());
        }

        [TestCase]
        public void MyData7_Clone_Test() {
            var myData7a = _names.
                Select(name => new MyData7Item(name)).
                ToMyData7();
            var myData7b = myData7a.Clone();
            myData7b["B"].Index = 20;
            myData7b["C"].Index = -1;
            myData7b["D"].Index = 10;
            Assert.AreEqual(_expectedXml, myData7b.ToXml());
            Assert.AreEqual(_initialXml, myData7a.ToXml());
            Assert.AreNotEqual(myData7a.ToXml(), myData7b.ToXml());
        }
    }
}
