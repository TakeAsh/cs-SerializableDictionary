using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

using ieRV2 = SerializableDictionary_Caller.ImExPorter<SerializableDictionary_Caller.MyData2, int>;
using kvpRV2 = System.Collections.Generic.KeyValuePair<SerializableDictionary_Caller.MyData2, int>;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData2_RV_Test {

        private Regex _regRegDate = new Regex(@"\{\{RegisteredDate\}\}");

        private SerializableDictionary<MyData2, int> _dicRV2a;

        private string _dicRV2aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<SerializableDictionary key_type=\"MyData2\" value_type=\"Int32\">\r\n" +
            "  <item key=\"{ID:'0', Name:'Zero', RegisteredDate:'{{RegisteredDate}}', Height:'170', }\" value=\"0\" />\r\n" +
            "  <item key=\"{ID:'1', Name:'One', RegisteredDate:'{{RegisteredDate}}', Height:'160', }\" value=\"1\" />\r\n" +
            "  <item key=\"{ID:'2', Name:'Two', RegisteredDate:'{{RegisteredDate}}', Height:'165', }\" value=\"2\" />\r\n" +
            "</SerializableDictionary>";

        DateTime _regDate;
        string _regDateString;

        static string _filePathRV2 = @"../Data/SampleRV2.log";

        [SetUp]
        public void setup() {
            var now = DateTime.Now;
            _regDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            _regDateString = _regDate.ToString().Replace(".0000000", "");

            _dicRV2a = ieRV2.create(new kvpRV2[]{
                new kvpRV2(new MyData2(){ ID=0, Name="Zero", RegisteredDate=_regDate, Height=170.0, }, 0),
                new kvpRV2(new MyData2(){ ID=1, Name="One", RegisteredDate=_regDate, Height=160.0, }, 1),
                new kvpRV2(new MyData2(){ ID=2, Name="Two", RegisteredDate=_regDate, Height=165.0, }, 2),
            });

            _dicRV2aXml = _regRegDate.Replace(_dicRV2aXml, _regDateString);
        }

        [TestCase]
        public void VR_ToXml() {
            var actual =_dicRV2a.ToXml();
            Assert.AreEqual(_dicRV2aXml, actual);
        }

        [TestCase]
        public void VR_FromXml() {
            var actual = SerializableDictionary<MyData2, int>.FromXml(_dicRV2aXml);
            Assert.AreEqual(_dicRV2aXml, actual.ToXml());
        }

        [TestCase]
        public void VR_export() {
            _dicRV2a.export(_filePathRV2);
            var actual = "";
            using (var reader = new StreamReader(_filePathRV2)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_dicRV2aXml, actual);
        }

        [TestCase]
        public void VR_import() {
            var actual = SerializableDictionary<MyData2, int>.import(_filePathRV2);
            Assert.AreEqual(_dicRV2aXml, actual.ToXml());
        }
    }
}
