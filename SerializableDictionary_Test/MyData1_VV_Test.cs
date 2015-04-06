using System;
using System.IO;
using NUnit.Framework;
using TakeAsh;

using ieVV1 = SerializableDictionary_Caller.ImExPorter<int, string>;
using kvpVV1 = System.Collections.Generic.KeyValuePair<int, string>;

namespace SerializableDictionary_Test {

    [TestFixture]
    class MyData1_VV_Test {

        private SerializableDictionary<int, string> _dicVV1a;

        private string _dicVV1aXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
            "<SerializableDictionary key_type=\"Int32\" value_type=\"String\">\r\n" +
            "  <item key=\"0\" value=\"Zero\" />\r\n" +
            "  <item key=\"1\" value=\"One\" />\r\n" +
            "  <item key=\"2\" value=\"Two\" />\r\n" +
            "</SerializableDictionary>";

        static string _filePathVV1 = @"../../Data/SampleVV1.log";

        [SetUp]
        public void setup() {
            _dicVV1a = ieVV1.create(new kvpVV1[]{
                new kvpVV1(0, "Zero"),
                new kvpVV1(1, "One"),
                new kvpVV1(2, "Two"),
            });
        }

        [TestCase]
        public void VV_ToXml() {
            var actual = _dicVV1a.ToXml();
            Assert.AreEqual(_dicVV1aXml, actual);
        }

        [TestCase]
        public void VV_FromXml() {
            var actual = SerializableDictionary<int, string>.FromXml(_dicVV1aXml);
            Assert.AreEqual(_dicVV1aXml, actual.ToXml());
        }

        [TestCase]
        public void VV_export() {
            _dicVV1a.export(_filePathVV1);
            var actual = "";
            using (var reader = new StreamReader(_filePathVV1)) {
                actual = reader.ReadToEnd();
            }
            Assert.AreEqual(_dicVV1aXml, actual);
        }

        [TestCase]
        public void VV_import() {
            var actual = ieVV1.import(_filePathVV1);
            Assert.AreEqual(_dicVV1aXml, actual.ToXml());
        }
    }
}
