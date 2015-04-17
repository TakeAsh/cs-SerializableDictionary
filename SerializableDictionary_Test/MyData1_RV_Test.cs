using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

using kvpRV1 = System.Collections.Generic.KeyValuePair<SerializableDictionary_Caller.MyData1, int>;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData1_RV_Test {

        private Regex _regRegDate = new Regex(@"\{\{RegisteredDate\}\}");

        private SerializableDictionary<MyData1, int> _dicRV1a;

        private string _dicRV1aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<SerializableDictionary key_type=\"MyData1\" value_type=\"Int32\">\r\n" +
            "  <item value=\"0\">\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>0</ID>\r\n" +
            "      <Name>Zero</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>170</Height>\r\n" +
            "    </MyData1>\r\n" +
            "  </item>\r\n" +
            "  <item value=\"1\">\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>1</ID>\r\n" +
            "      <Name>One</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>160</Height>\r\n" +
            "    </MyData1>\r\n" +
            "  </item>\r\n" +
            "  <item value=\"2\">\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>2</ID>\r\n" +
            "      <Name>Two</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>165</Height>\r\n" +
            "    </MyData1>\r\n" +
            "  </item>\r\n" +
            "</SerializableDictionary>";

        DateTime _regDate;
        string _regDateString;

        static string _filePathRV1 = @"../../Data/SampleRV1.log";

        [SetUp]
        public void setup() {
            var now = DateTime.Now;
            _regDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            _regDateString = _regDate.ToString("o").Replace(".0000000", "");

            _dicRV1a = new[]{
                new kvpRV1(new MyData1(){ ID=0, Name="Zero", RegisteredDate=_regDate, Height=170.0, }, 0),
                new kvpRV1(new MyData1(){ ID=1, Name="One", RegisteredDate=_regDate, Height=160.0, }, 1),
                new kvpRV1(new MyData1(){ ID=2, Name="Two", RegisteredDate=_regDate, Height=165.0, }, 2),
            }.ToSerializableDictionary();

            _dicRV1aXml = _regRegDate.Replace(_dicRV1aXml, _regDateString);
        }

        [TestCase]
        public void VR_ToXml() {
            var actual = _dicRV1a.ToXml();
            Assert.AreEqual(_dicRV1aXml, actual);
        }

        [TestCase]
        public void VR_FromXml() {
            var actual = (null as SerializableDictionary<MyData1, int>).FromXml(_dicRV1aXml);
            Assert.AreEqual(_dicRV1aXml, actual.ToXml());
        }

        [TestCase]
        public void VR_export() {
            _dicRV1a.Export(_filePathRV1);
            var actual = "";
            using (var reader = new StreamReader(_filePathRV1)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_dicRV1aXml, actual);
        }

        [TestCase]
        public void VR_import() {
            var actual = (null as SerializableDictionary<MyData1, int>).Import(_filePathRV1);
            Assert.AreEqual(_dicRV1aXml, actual.ToXml());
        }
    }
}
