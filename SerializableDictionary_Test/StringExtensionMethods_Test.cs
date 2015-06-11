﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TakeAsh;

namespace SerializableDictionary_Test {

    [TestFixture]
    class StringExtensionMethods_Test {

        const int DefaultIntValue = -100;
        const double DefaultDoubleValue = -100;
        static readonly DateTime DefaultDateTimeValue = new DateTime(2010, 11, 12, 13, 14, 15);
        const string DefaultStringValue = "xyz";

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("-1", -1)]
        [TestCase("-2", -2)]
        [TestCase("a", 0)]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        public void TryParse_Int_Test(string input, int expected) {
            Assert.AreEqual(expected, input.TryParse<int>());
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("-1", -1)]
        [TestCase("-2", -2)]
        [TestCase("a", DefaultIntValue)]
        [TestCase("", DefaultIntValue)]
        [TestCase(null, DefaultIntValue)]
        public void TryParse_Int_WithDefalut_Test(string input, int expected) {
            Assert.AreEqual(expected, input.TryParse(DefaultIntValue));
        }

        [TestCase("0.0", 0)]
        [TestCase("1.0", 1)]
        [TestCase("2.0", 2)]
        [TestCase("-1.0", -1)]
        [TestCase("-2.0", -2)]
        [TestCase("a", 0)]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        public void TryParse_Double_Test(string input, double expected) {
            Assert.AreEqual(expected, input.TryParse<double>());
        }

        [TestCase("0.0", 0)]
        [TestCase("1.0", 1)]
        [TestCase("2.0", 2)]
        [TestCase("-1.0", -1)]
        [TestCase("-2.0", -2)]
        [TestCase("a", DefaultDoubleValue)]
        [TestCase("", DefaultDoubleValue)]
        [TestCase(null, DefaultDoubleValue)]
        public void TryParse_Double_WithDefalut_Test(string input, double expected) {
            Assert.AreEqual(expected, input.TryParse(DefaultDoubleValue));
        }

        [TestCase("2015/06/11 01:02:03", "2015-06-11 01:02:03")]
        [TestCase("a", "0001-01-01 00:00:00")]
        [TestCase("", "0001-01-01 00:00:00")]
        [TestCase(null, "0001-01-01 00:00:00")]
        public void TryParse_DateTime_Test(string input, string expected) {
            DateTime expectedDate;
            DateTime.TryParse(expected, out expectedDate);
            Assert.AreEqual(expectedDate, input.TryParse<DateTime>());
        }

        [TestCase("2015/06/11 01:02:03", "2015-06-11 01:02:03")]
        [TestCase("a", "2010-11-12 13:14:15")]
        [TestCase("", "0001-01-01 00:00:00")]
        [TestCase(null, "2010-11-12 13:14:15")]
        public void TryParse_DateTime_WithDefalut_Test(string input, string expected) {
            DateTime expectedDate;
            DateTime.TryParse(expected, out expectedDate);
            Assert.AreEqual(expectedDate, input.TryParse(DefaultDateTimeValue));
        }

        [TestCase("abc", "", "abc")]
        [TestCase("", "", "")]
        [TestCase(null, "", "")]
        [TestCase("abc", null, "abc")]
        [TestCase("", null, null)]
        [TestCase(null, null, null)]
        [TestCase("abc", DefaultStringValue, "abc")]
        [TestCase("", DefaultStringValue, DefaultStringValue)]
        [TestCase(null, DefaultStringValue, DefaultStringValue)]
        public void ToDefaultIfNullOrEmpty_Test(string input, string defaultValue, string expected) {
            Assert.AreEqual(expected, input.ToDefaultIfNullOrEmpty(defaultValue));
        }
    }
}
