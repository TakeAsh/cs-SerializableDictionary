using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SerializableDictionary_Caller;
using TakeAsh;

using ieRR2 = SerializableDictionary_Caller.ImExPorter<SerializableDictionary_Caller.MyData2, SerializableDictionary_Caller.MyData2>;
using kvpRR2 = System.Collections.Generic.KeyValuePair<SerializableDictionary_Caller.MyData2, SerializableDictionary_Caller.MyData2>;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData2_RR_Test {

        private Regex _regRegDate = new Regex(@"\{\{RegisteredDate\}\}");

        private SerializableDictionary<MyData2, MyData2> _dicRR2a;

        private string _dicRR2aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<SerializableDictionary key_type=\"MyData2\" value_type=\"MyData2\">\r\n" +
            "  <item key=\"{ID:'0', Name:'Zero', RegisteredDate:'{{RegisteredDate}}', Height:'170', }\" value=\"{ID:'10', Name:'AZero', RegisteredDate:'{{RegisteredDate}}', Height:'175.1', }\" />\r\n" +
            "  <item key=\"{ID:'1', Name:'One', RegisteredDate:'{{RegisteredDate}}', Height:'160', }\" value=\"{ID:'11', Name:'AOne', RegisteredDate:'{{RegisteredDate}}', Height:'165.1', }\" />\r\n" +
            "  <item key=\"{ID:'2', Name:'Two', RegisteredDate:'{{RegisteredDate}}', Height:'165', }\" value=\"{ID:'12', Name:'ATwo', RegisteredDate:'{{RegisteredDate}}', Height:'170.1', }\" />\r\n" +
            "</SerializableDictionary>";

        DateTime _regDate;
        string _regDateString;

        static string _filePathRR2 = @"../../Data/SampleRR2.log";

        [SetUp]
        public void setup() {
            var now = DateTime.Now;
            _regDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            _regDateString = _regDate.ToString().Replace(".0000000", "");

            _dicRR2a = ieRR2.create(new kvpRR2[]{
                new kvpRR2(
                    new MyData2(){ ID=0, Name="Zero", RegisteredDate=_regDate, Height=170.0, },
                    new MyData2(){ ID=10, Name="AZero", RegisteredDate=_regDate, Height=175.1, }
                ),
                new kvpRR2(
                    new MyData2(){ ID=1, Name="One", RegisteredDate=_regDate, Height=160.0, },
                    new MyData2(){ ID=11, Name="AOne", RegisteredDate=_regDate, Height=165.1, }
                ),
                new kvpRR2(
                    new MyData2(){ ID=2, Name="Two", RegisteredDate=_regDate, Height=165.0, },
                    new MyData2(){ ID=12, Name="ATwo", RegisteredDate=_regDate, Height=170.1, }
                ),
            });

            _dicRR2aXml = _regRegDate.Replace(_dicRR2aXml, _regDateString);
        }

        [TestCase]
        public void VR_ToXml() {
            var actual = _dicRR2a.ToXml();
            Assert.AreEqual(_dicRR2aXml, actual);
        }

        [TestCase]
        public void VR_FromXml() {
            var actual = SerializableDictionary<MyData2, MyData2>.FromXml(_dicRR2aXml);
            Assert.AreEqual(_dicRR2aXml, actual.ToXml());
        }

        [TestCase]
        public void VR_export() {
            _dicRR2a.export(_filePathRR2);
            var actual = "";
            using (var reader = new StreamReader(_filePathRR2)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_dicRR2aXml, actual);
        }

        [TestCase]
        public void VR_import() {
            var actual = SerializableDictionary<MyData2, MyData2>.import(_filePathRR2);
            Assert.AreEqual(_dicRR2aXml, actual.ToXml());
        }
    }
}
