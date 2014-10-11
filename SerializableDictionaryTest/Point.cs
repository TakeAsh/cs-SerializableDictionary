using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using TakeAsh;

namespace SerializableDictionaryTest {
    [TypeConverter(typeof(StringifyConverter<Point>))]
    public struct Point : IStringify<Point> {
        static private Regex regXY = new Regex(@"^\s*(?<X>[\-\+\d\.eE]+)\s*,\s*(?<Y>[\-\+\d\.eE]+)\s*$");

        [XmlAttribute]
        public double X { get; set; }

        [XmlAttribute]
        public double Y { get; set; }

        public Point(double X, double Y) : this() {
            this.X = X;
            this.Y = Y;
        }

        public override string ToString() {
            return X + ", " + Y;
        }

        public Point FromString(string source) {
            if (String.IsNullOrEmpty(source)) {
                return this;
            }
            MatchCollection mcLab = regXY.Matches(source);
            if (mcLab.Count > 0) {
                double x, y;
                double.TryParse(mcLab[0].Groups["X"].Value, out x);
                double.TryParse(mcLab[0].Groups["Y"].Value, out y);
                this.X = x;
                this.Y = y;
            }
            return this;
        }
    }
}
