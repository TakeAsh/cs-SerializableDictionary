using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializableDictionaryTest {
    public class MyData1 {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime RegisteredDate { get; set; }
        public double Height { get; set; }

        public new string ToString() {
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
