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

        [TestCase]
        public void MyData7_SortByItem_Test() {
            var names = new string[] { "A", "B", "C", "D", "E", };
            var myData7a = new MyData7(
                names.Select(name => new MyData7Item(name))
            );
            Assert.AreEqual(_initialXml, myData7a.ToXml());
            myData7a["B"].Index = 20;
            myData7a["C"].Index = -1;
            myData7a["D"].Index = 10;
            Assert.AreEqual(_expectedXml, myData7a.ToXml());
        }
    }
}
