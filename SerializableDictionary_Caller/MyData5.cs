using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using TakeAsh;
using TakeAshUtility;

namespace SerializableDictionary_Caller {
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

    public class MyData5s :
        ListableDictionary<MyData5.Positions, MyData5>,
        IXmlHelper {

        public MyData5s() : base() { }

        public MyData5s(IEnumerable<MyData5> source) : base(source) { }
    }
}
