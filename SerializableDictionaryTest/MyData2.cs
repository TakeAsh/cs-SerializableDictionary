using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SerializableDictionaryTest {
    [TypeConverter(typeof(MyData2Converter))]
    public class MyData2 {
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
    }
}
