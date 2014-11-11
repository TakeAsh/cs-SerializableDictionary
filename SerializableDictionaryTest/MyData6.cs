using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TakeAsh;
using System.Xml;
using System.Xml.Serialization;

namespace SerializableDictionaryTest {

    [XmlRoot("Item")]
    public class MyData6Item : IListableDictionariable<int> {

        [XmlAttribute]
        public int Index { get; set; }

        [XmlAttribute]
        public double Value { get; set; }

        public MyData6Item() { }

        public MyData6Item(int Index, double Value) {
            this.Index = Index;
            this.Value = Value;
        }

        #region IListableDictionariable members

        public int getKey() {
            return Index;
        }

        public void setKey(int Index) {
            this.Index = Index;
        }

        #endregion
    }

    [XmlRoot("Channel")]
    public class MyData6Channel : ListableDictionary<int, MyData6Item>, IListableDictionariable<MyData6Channel.Channels> {
        public enum Channels {
            Red, Green, Blue,
        }

        static public Channels[] ChannelValues {
            get { return (Channels[])Enum.GetValues(typeof(Channels)); }
        }

        static public string[] ChannelNames {
            get { return Enum.GetNames(typeof(Channels)); }
        }

        public Channels Channel { get; set; }

        public override string Name {
            get { return Channel.ToString(); }
            set {
                Channel = default(Channels);
                if (String.IsNullOrEmpty(value)) {
                    return;
                }
                var index = Array.IndexOf(ChannelNames, value);
                if (index >= 0) {
                    Channel = ChannelValues[index];
                }
            }
        }

        public MyData6Channel() : base() { }

        public MyData6Channel(Channels Channel) : base() {
            this.Channel = Channel;
        }

        #region IListableDictionariable members

        public new Channels getKey() {
            return Channel;
        }

        public void setKey(MyData6Channel.Channels Channel) {
            this.Channel = Channel;
        }

        #endregion
    }

    [XmlRoot("Device")]
    public class MyData6Device : ListableDictionary<MyData6Channel.Channels, MyData6Channel> {

        const string SizeAttributeName = "Size";
        const string MakerIDAttributeName = "MakerID";

        private int _makerID;

        [XmlAttribute]  // XmlAttribute don't work
        public string Size {
            get { return ExtraAttributes[SizeAttributeName]; }
            set { ExtraAttributes[SizeAttributeName] = value; }
        }

        public int MakerID {
            get {
                int.TryParse(ExtraAttributes[MakerIDAttributeName], out _makerID);
                return _makerID;
            }
            set {
                _makerID = value;
                ExtraAttributes[MakerIDAttributeName] = _makerID.ToString();
            }
        }

        static MyData6Device() {
            ExtraAttributeNames = new string[]{
                SizeAttributeName,
                MakerIDAttributeName,
            };
        }

        public MyData6Device() : base() { }

        public MyData6Device(string Name) : base(Name) { }
    }

    public class MyData6 : ListableDictionary<string, MyData6Device> {
        static public MyData6 import(string fileName) {
            return XmlHelper<MyData6>.importFile(fileName);
        }

        public bool export(string fileName) {
            return XmlHelper<MyData6>.exportFile(fileName, this);
        }
    }
}
