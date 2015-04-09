using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TakeAsh;

namespace SerializableDictionary_Test {

    [TestFixture]
    class ObjectExtensionMethods_Test {

        public enum Weekdays {
            Sun,
            Mon,
            Tue,
            Wed,
            Thu,
            Fri,
            Sat,
        }

        [TestCase(null, null, null)]
        [TestCase(null, "", "")]
        [TestCase(null, "undefined", "undefined")]
        [TestCase(1, null, "1")]
        [TestCase(1, "", "1")]
        [TestCase(1, "undefined", "1")]
        [TestCase(2.2, null, "2.2")]
        [TestCase(2.2, "", "2.2")]
        [TestCase(2.2, "undefined", "2.2")]
        [TestCase(Weekdays.Mon, null, "Mon")]
        [TestCase(Weekdays.Mon, "", "Mon")]
        [TestCase(Weekdays.Mon, "Null", "Mon")]
        public void SafeToString_Test(object obj, string ifNull, string expected) {
            var actual = obj.SafeToString(ifNull);
            if (expected != null) {
                Assert.AreEqual(expected, actual);
            } else {
                Assert.IsNull(actual);
            }
        }
    }
}
