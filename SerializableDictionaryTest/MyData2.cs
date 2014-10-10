using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using TakeAsh;

namespace SerializableDictionaryTest {
    [TypeConverter(typeof(StringifyConverter<MyData2>))]
    public class MyData2 : IStringify<MyData2> {
        static Regex regInner = new Regex(@"^\s*\{\s*(?<inner>.*)\s*\}\s*$");
        static Regex regProperty = new Regex(@"\s*(?<key>[^\s:]+)\s*:\s*'(?<value>[^']+)'\s*");

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime RegisteredDate { get; set; }
        public double Height { get; set; }

        public string this[string property] {
            get {
                switch (property) {
                    case "ID":
                        return ID.ToString();
                    case "Name":
                        return Name;
                    case "RegisteredDate":
                        return RegisteredDate.ToString("yyyy-MM-dd hh:mm:ss");
                    case "Height":
                        return Height.ToString();
                    default:
                        return null;
                }
            }
            set {
                switch (property) {
                    case "ID": {
                            int tmp;
                            int.TryParse(value, out tmp);
                            ID = tmp;
                        }
                        break;
                    case "Name":
                        Name = value;
                        break;
                    case "RegisteredDate": {
                            DateTime tmp;
                            DateTime.TryParse(value, out tmp);
                            RegisteredDate = tmp;
                        }
                        break;
                    case "Height": {
                            double tmp;
                            double.TryParse(value, out tmp);
                            Height = tmp;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public override string ToString() {
            return
                "{" +
                    "ID:'" + ID + "', " +
                    "Name:'" + Name + "', " +
                    "RegisteredDate:'" + RegisteredDate.ToString() + "', " +
                    "Height:'" + Height + "', " +
                "}";
        }

        public MyData2 FromString(string source) {
            if (String.IsNullOrEmpty(source)) {
                return this;
            }
            MatchCollection mcInner = regInner.Matches(source);
            string strInner = mcInner[0].Groups["inner"].Value;
            string[] strProperties = strInner.Split(new char[] { ',' });
            foreach (var strProp in strProperties) {
                MatchCollection mcProp = regProperty.Matches(strProp);
                foreach (Match m in mcProp) {
                    this[m.Groups["key"].Value] = m.Groups["value"].Value;
                }
            }
            return this;
        }
    }
}
