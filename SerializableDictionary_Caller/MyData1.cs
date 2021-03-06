﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializableDictionary_Caller {
    public class MyData1 {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime RegisteredDate { get; set; }
        public double Height { get; set; }

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
