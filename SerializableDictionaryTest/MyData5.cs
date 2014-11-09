using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TakeAsh;
using System.Text.RegularExpressions;

namespace SerializableDictionaryTest {
    public class MyData5 : IListableDictionariable<MyData5.Positions> {

        public enum Positions {
            Floor1,
            Floor2,
            Floor3,
            Floor4,
            FloorB1,
            FloorB2,
        }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public Positions Position { get; set; }

        [XmlAttribute]
        public int HitPoint { get; set; }

        //[XmlIgnore]
        public Equipment Weapon { get; set; }

        //[XmlAttribute("Weapon")]
        [XmlIgnore]
        public string WeaponAttribute {
            get { return Weapon.ToString(); }
            set { Weapon = (new Equipment()).FromString(value); }
        }

        public override string ToString() {
            return 
                "Name:'" + Name + "'" +
                ", Position:'" + Position + "'" +
                ", HitPoint:" + HitPoint +
                ", Weapon:{" + Weapon + "}";
        }

        #region IListableDictionariable members

        public Positions getKey() {
            return Position;
        }

        public void setKey(MyData5.Positions Position) {
            this.Position = Position;
        }

        #endregion
    }

    public class Equipment : IStringify<Equipment> {
        static Regex regFromString = new Regex(@"\s*Name:\s*'(?<Name>[^']*)'\s*,\s*Owner:\s*'(?<Owner>[^']*)'\s*");

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Owner { get; set; }

        #region IStringify Members

        public override string ToString() {
            return "Name:'" + Name + "', Owner:'" + Owner + "'";
        }

        public Equipment FromString(string source) {
            var mc = regFromString.Matches(source);
            if (mc.Count > 0) {
                var m = mc[0];
                Name = m.Groups["Name"].Value;
                Owner = m.Groups["Owner"].Value;
            }
            return this;
        }

        #endregion
    }

    public class MyData5s : ListableDictionary<MyData5.Positions, MyData5> {

        public MyData5s() : base() { }

        public MyData5s(MyData5[] source) : base(source) { }

        static public explicit operator MyData5s(MyData5[] source) {
            return new MyData5s(source);
        }

        static public MyData5s import(string fileName) {
            return XmlHelper<MyData5s>.importFile(fileName);
        }

        public bool export(string fileName) {
            return XmlHelper<MyData5s>.exportFile(fileName, this);
        }
    }
}
