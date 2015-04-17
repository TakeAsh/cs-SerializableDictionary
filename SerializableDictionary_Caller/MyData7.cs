using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TakeAsh;
using System.Xml;
using System.Xml.Serialization;

namespace SerializableDictionary_Caller {
    public class MyData7 :
        ListableDictionary<string, MyData7Item>,
        IXmlHelper {

        static MyData7() {
            SortByItem = true;
        }

        public MyData7() : base() { }

        public MyData7(IEnumerable<MyData7Item> items) : base(items) { }

        public override void Add(MyData7Item item) {
            item.Index = Count;
            base.Add(item);
        }
    }

    public static class IEnumerableMyData7ItemExtensionMethods {
        public static MyData7 ToMyData7(this IEnumerable<MyData7Item> items) {
            return new MyData7(items);
        }
    }

    [XmlRoot("Item")]
    public class MyData7Item :
        IListableDictionariable<string>,
        IComparable<MyData7Item> {

        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public int Index { get; set; }

        public MyData7Item() { }

        public MyData7Item(string name) {
            this.Name = name;
        }

        public override string ToString() {
            return "Name:'" + Name + "', Index:" + Index;
        }

        #region IListableDictionariable<string> members

        public string getKey() { return Name; }
        public void setKey(string key) { Name = key; }

        #endregion

        #region IComparable<MyData7Item> members

        public int CompareTo(MyData7Item other) {
            if (other == null) {
                return -1;
            }
            return this.Index.CompareTo(other.Index);
        }

        #endregion

        #region IComparable members

        public int CompareTo(object obj) {
            if (obj == null) {
                return -1;
            }
            var other = obj as MyData7Item;
            if (other == null) {
                throw new ArgumentException("must be MyData7Item type", "obj");
            }
            return this.Index.CompareTo(other.Index);
        }

        #endregion
    }
}
