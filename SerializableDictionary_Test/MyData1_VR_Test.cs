using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

using kvpVR1 = System.Collections.Generic.KeyValuePair<int, SerializableDictionary_Caller.MyData1>;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData1_VR_Test {

        private Regex _regRegDate = new Regex(@"\{\{RegisteredDate\}\}");

        private SerializableDictionary<int, MyData1> _dicVR1a;

        private string _dicVR1aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<SerializableDictionary key_type=\"Int32\" value_type=\"MyData1\">\r\n" +
            "  <item key=\"0\">\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>0</ID>\r\n" +
            "      <Name>Zero</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>170</Height>\r\n" +
            "    </MyData1>\r\n" +
            "  </item>\r\n" +
            "  <item key=\"1\">\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>1</ID>\r\n" +
            "      <Name>One</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>160</Height>\r\n" +
            "    </MyData1>\r\n" +
            "  </item>\r\n" +
            "  <item key=\"2\">\r\n" +
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

        static string _filePathVR1 = @"../../Data/SampleVR1.log";

        [SetUp]
        public void setup() {
            var now = DateTime.Now;
            _regDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            _regDateString = _regDate.ToString("o").Replace(".0000000", "");

            _dicVR1a = new[]{
                new kvpVR1(0, new MyData1(){ ID=0, Name="Zero", RegisteredDate=_regDate, Height=170.0, }),
                new kvpVR1(1, new MyData1(){ ID=1, Name="One", RegisteredDate=_regDate, Height=160.0, }),
                new kvpVR1(2, new MyData1(){ ID=2, Name="Two", RegisteredDate=_regDate, Height=165.0, }),
            }.ToSerializableDictionary();

            _dicVR1aXml = _regRegDate.Replace(_dicVR1aXml, _regDateString);
        }

        [TestCase]
        public void VR_ToXml() {
            var actual =_dicVR1a.ToXml();
            Assert.AreEqual(_dicVR1aXml, actual);
        }

        [TestCase]
        public void VR_FromXml() {
            var actual = (null as SerializableDictionary<int, MyData1>).FromXml(_dicVR1aXml);
            Assert.AreEqual(_dicVR1aXml, actual.ToXml());
        }

        [TestCase]
        public void VR_export() {
            _dicVR1a.Export(_filePathVR1);
            var actual = "";
            using (var reader = new StreamReader(_filePathVR1)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_dicVR1aXml, actual);
        }

        [TestCase]
        public void VR_import() {
            var actual = (null as SerializableDictionary<int, MyData1>).Import(_filePathVR1);
            Assert.AreEqual(_dicVR1aXml, actual.ToXml());
        }
    }
}
