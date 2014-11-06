﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TakeAsh;
using System.Xml;
using System.Xml.Serialization;

namespace SerializableDictionaryTest {

    [XmlRoot("Item")]
    public class MyData6Item : IGetKey<int> {

        [XmlAttribute]
        public int Index { get; set; }

        [XmlAttribute]
        public double Value { get; set; }

        public MyData6Item() { }

        public MyData6Item(int Index, double Value) {
            this.Index = Index;
            this.Value = Value;
        }

        #region IGetKey member

        public int getKey() {
            return Index;
        }

        #endregion
    }

    [XmlRoot("Channel")]
    public class MyData6Channel : ListableDictionary<int, MyData6Item>, IGetKey<MyData6Channel.Channels> {
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

        #region IGetKey member

        public new Channels getKey() {
            return Channel;
        }

        #endregion
    }

    [XmlRoot("Device")]
    public class MyData6Device : ListableDictionary<MyData6Channel.Channels, MyData6Channel>, IGetKey<MyData6Device.Devices> {
        public enum Devices {
            Monitor, Scanner, Camera,
        }

        static public Devices[] DeviceValues {
            get { return (Devices[])Enum.GetValues(typeof(Devices)); }
        }

        static public string[] DeviceNames {
            get { return Enum.GetNames(typeof(Devices)); }
        }

        public Devices Device { get; set; }

        public override string Name {
            get { return Device.ToString(); }
            set {
                Device = default(Devices);
                if (String.IsNullOrEmpty(value)) {
                    return;
                }
                var index = Array.IndexOf(DeviceNames, value);
                if (index >= 0) {
                    Device = DeviceValues[index];
                }
            }
        }

        public MyData6Device() : base() { }

        public MyData6Device(Devices Device) : base() {
            this.Device = Device;
        }

        #region IGetKey member

        public new Devices getKey() {
            return Device;
        }

        #endregion
    }

    public class MyData6 : ListableDictionary<MyData6Device.Devices, MyData6Device> {
        static public MyData6 import(string fileName) {
            return XmlHelper<MyData6>.importFile(fileName);
        }

        public bool export(string fileName) {
            return XmlHelper<MyData6>.exportFile(fileName, this);
        }
    }
}