using System;
using System.IO;
using NUnit.Framework;
using TakeAsh;
using System.Text.RegularExpressions;

using ieRR1 = SerializableDictionary_Caller.ImExPorter<SerializableDictionary_Caller.MyData1, SerializableDictionary_Caller.MyData1>;
using kvpRR1 = System.Collections.Generic.KeyValuePair<SerializableDictionary_Caller.MyData1, SerializableDictionary_Caller.MyData1>;

namespace SerializableDictionary_Test {

    [TestFixture]
    class SerializableDictionaryTest_RR {

        private Regex _regRegDate = new Regex(@"\{\{RegisteredDate\}\}");

        private SerializableDictionary<SerializableDictionary_Caller.MyData1, SerializableDictionary_Caller.MyData1> _dicRR1a;

        private string _dicRR1aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<SerializableDictionary key_type=\"MyData1\" value_type=\"MyData1\">\r\n" +
            "  <item>\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>0</ID>\r\n" +
            "      <Name>Zero</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>170</Height>\r\n" +
            "    </MyData1>\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>10</ID>\r\n" +
            "      <Name>AZero</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>175.1</Height>\r\n" +
            "    </MyData1>\r\n" +
            "  </item>\r\n" +
            "  <item>\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>1</ID>\r\n" +
            "      <Name>One</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>160</Height>\r\n" +
            "    </MyData1>\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>11</ID>\r\n" +
            "      <Name>AOne</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>165.1</Height>\r\n" +
            "    </MyData1>\r\n" +
            "  </item>\r\n" +
            "  <item>\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>2</ID>\r\n" +
            "      <Name>Two</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>165</Height>\r\n" +
            "    </MyData1>\r\n" +
            "    <MyData1>\r\n" +
            "      <ID>12</ID>\r\n" +
            "      <Name>ATwo</Name>\r\n" +
            "      <RegisteredDate>{{RegisteredDate}}</RegisteredDate>\r\n" +
            "      <Height>170.1</Height>\r\n" +
            "    </MyData1>\r\n" +
            "  </item>\r\n" +
            "</SerializableDictionary>";

        DateTime _regDate;
        string _regDateString;

        static string _filePathRR1 = @"../Data/SampleRR1.log";

        [SetUp]
        public void setup() {
            var now = DateTime.Now;
            _regDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            _regDateString = _regDate.ToString("o").Replace(".0000000", "");

            _dicRR1a = ieRR1.create(new kvpRR1[]{
                new kvpRR1(
                    new SerializableDictionary_Caller.MyData1(){ ID=0, Name="Zero", RegisteredDate=_regDate, Height=170.0, },
                    new SerializableDictionary_Caller.MyData1(){ ID=10, Name="AZero", RegisteredDate=_regDate, Height=175.1, }
                ),
                new kvpRR1(
                    new SerializableDictionary_Caller.MyData1(){ ID=1, Name="One", RegisteredDate=_regDate, Height=160.0, },
                    new SerializableDictionary_Caller.MyData1(){ ID=11, Name="AOne", RegisteredDate=_regDate, Height=165.1, }
                ),
                new kvpRR1(
                    new SerializableDictionary_Caller.MyData1(){ ID=2, Name="Two", RegisteredDate=_regDate, Height=165.0, },
                    new SerializableDictionary_Caller.MyData1(){ ID=12, Name="ATwo", RegisteredDate=_regDate, Height=170.1, }
                ),
            });

            _dicRR1aXml = _regRegDate.Replace(_dicRR1aXml, _regDateString);
        }

        [TestCase]
        public void RR_ToXml() {
            var actual = _dicRR1a.ToXml();
            Assert.AreEqual(_dicRR1aXml, actual);
        }

        [TestCase]
        public void RR_FromXml() {
            var actual = SerializableDictionary<SerializableDictionary_Caller.MyData1, SerializableDictionary_Caller.MyData1>.FromXml(_dicRR1aXml);
            Assert.AreEqual(_dicRR1aXml, actual.ToXml());
        }

        [TestCase]
        public void RR_export() {
            _dicRR1a.export(_filePathRR1);
            var actual = "";
            using (var reader = new StreamReader(_filePathRR1)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_dicRR1aXml, actual);
        }

        [TestCase]
        public void RR_import() {
            var actual = SerializableDictionary<SerializableDictionary_Caller.MyData1, SerializableDictionary_Caller.MyData1>.import(_filePathRR1);
            Assert.AreEqual(_dicRR1aXml, actual.ToXml());
        }
    }
}
