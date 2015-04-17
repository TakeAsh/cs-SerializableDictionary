using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

using kvpVR2 = System.Collections.Generic.KeyValuePair<int, SerializableDictionary_Caller.MyData2>;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData2_VR_Test {

        private Regex _regRegDate = new Regex(@"\{\{RegisteredDate\}\}");

        private SerializableDictionary<int, MyData2> _dicVR2a;

        private string _dicVR2aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<SerializableDictionary key_type=\"Int32\" value_type=\"MyData2\">\r\n" +
            "  <item key=\"0\" value=\"{ID:'0', Name:'Zero', RegisteredDate:'{{RegisteredDate}}', Height:'170', }\" />\r\n" +
            "  <item key=\"1\" value=\"{ID:'1', Name:'One', RegisteredDate:'{{RegisteredDate}}', Height:'160', }\" />\r\n" +
            "  <item key=\"2\" value=\"{ID:'2', Name:'Two', RegisteredDate:'{{RegisteredDate}}', Height:'165', }\" />\r\n" +
            "</SerializableDictionary>";

        DateTime _regDate;
        string _regDateString;

        static string _filePathVR2 = @"../../Data/SampleVR2.log";

        [SetUp]
        public void setup() {
            var now = DateTime.Now;
            _regDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            _regDateString = _regDate.ToString().Replace(".0000000", "");

            _dicVR2a = new[]{
                new kvpVR2(0, new MyData2(){ ID=0, Name="Zero", RegisteredDate=_regDate, Height=170.0, }),
                new kvpVR2(1, new MyData2(){ ID=1, Name="One", RegisteredDate=_regDate, Height=160.0, }),
                new kvpVR2(2, new MyData2(){ ID=2, Name="Two", RegisteredDate=_regDate, Height=165.0, }),
            }.ToSerializableDictionary();

            _dicVR2aXml = _regRegDate.Replace(_dicVR2aXml, _regDateString);
        }

        [TestCase]
        public void VR_ToXml() {
            var actual =_dicVR2a.ToXml();
            Assert.AreEqual(_dicVR2aXml, actual);
        }

        [TestCase]
        public void VR_FromXml() {
            var actual = (null as SerializableDictionary<int, MyData2>).FromXml(_dicVR2aXml);
            Assert.AreEqual(_dicVR2aXml, actual.ToXml());
        }

        [TestCase]
        public void VR_export() {
            _dicVR2a.Export(_filePathVR2);
            var actual = "";
            using (var reader = new StreamReader(_filePathVR2)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_dicVR2aXml, actual);
        }

        [TestCase]
        public void VR_import() {
            var actual = (null as SerializableDictionary<int, MyData2>).Import(_filePathVR2);
            Assert.AreEqual(_dicVR2aXml, actual.ToXml());
        }
    }
}
