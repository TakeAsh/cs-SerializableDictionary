using System;
using System.IO;
using NUnit.Framework;
using TakeAsh;

using kvpVV2 = System.Collections.Generic.KeyValuePair<double, string>;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData2_VV_Test {

        private SerializableDictionary<double, string> _dicVV2a;

        private string _dicVV2aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<SerializableDictionary key_type=\"Double\" value_type=\"String\">\r\n" +
            "  <item key=\"0.1\" value=\"Zero\" />\r\n" +
            "  <item key=\"1.2\" value=\"One\" />\r\n" +
            "  <item key=\"2.3\" value=\"Two\" />\r\n" +
            "</SerializableDictionary>";

        static string _filePathVV2 = @"../../Data/SampleVV2.log";

        [SetUp]
        public void setup() {
            _dicVV2a = new []{
                new kvpVV2(0.1, "Zero"),
                new kvpVV2(1.2, "One"),
                new kvpVV2(2.3, "Two"),
            }.ToSerializableDictionary();
        }

        [TestCase]
        public void VV_ToXml() {
            var actual = _dicVV2a.ToXml();
            Assert.AreEqual(_dicVV2aXml, actual);
        }

        [TestCase]
        public void VV_FromXml() {
            var actual = (null as SerializableDictionary<double, string>).FromXml(_dicVV2aXml);
            Assert.AreEqual(_dicVV2aXml, actual.ToXml());
        }

        [TestCase]
        public void VV_export() {
            _dicVV2a.Export(_filePathVV2);
            var actual = "";
            using (var reader = new StreamReader(_filePathVV2)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_dicVV2aXml, actual);
        }

        [TestCase]
        public void VV_import() {
            var actual = (null as SerializableDictionary<double, string>).Import(_filePathVV2);
            Assert.AreEqual(_dicVV2aXml, actual.ToXml());
        }
    }
}
