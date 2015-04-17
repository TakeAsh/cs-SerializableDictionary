using System.Xml;
using System.Xml.Serialization;
using TakeAsh;

namespace SerializableDictionary_Caller {
    public class MyData4 {

        private Point _position;

        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public Point Size { get; set; }

        /// <summary>
        /// * BUG *
        /// </summary>
        /// <remarks>
        /// Size property is NOT deserialized correctly.
        /// Size property is Point type, and Point type is not reference type but value type.
        /// So, "Size.FromString(value)" update just a copy of Size property, and actual Size property is not updated.
        /// </remarks>
        [XmlAttribute("Size")]
        public string SizeAttribute {
            get { return Size.ToString(); }
            set { Size.FromString(value); }
        }

        [XmlIgnore]
        public Point Position {
            get { return _position; }
            set { _position = value; }
        }

        [XmlAttribute("Position")]
        public string PositionAttribute {
            get { return _position.ToString(); }
            set { _position.FromString(value); }
        }
    }

    public class MyData4List :
        IXmlHelper {

        /// <summary>
        /// Actual array of MyData4
        /// </summary>
        /// <remarks>
        /// When using [XmlArray], there is a child element "Items" under the parent.
        /// When using [XmlElement], there is array of "Item" under the parent directly.
        /// </remarks>
        //[XmlArray]
        [XmlElement("Item")]
        public MyData4[] Items { get; set; }

        [XmlAttribute]
        public int Count {
            get { return Items != null ? Items.Length : 0; }
            set { Items = new MyData4[value]; }
        }

        public MyData4List() {
        }

        public MyData4List(MyData4[] items) {
            this.Items = items;
        }

        public MyData4 this[int index] {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        static public implicit operator MyData4List(MyData4[] source) {
            return new MyData4List(source);
        }
    }
}
