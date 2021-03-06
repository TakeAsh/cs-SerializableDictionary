﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TakeAsh;

using kvpVV1 = System.Collections.Generic.KeyValuePair<int, string>;

using kvpVR1 = System.Collections.Generic.KeyValuePair<int, SerializableDictionary_Caller.MyData1>;

using kvpRV1 = System.Collections.Generic.KeyValuePair<SerializableDictionary_Caller.MyData1, int>;

using kvpRR1 = System.Collections.Generic.KeyValuePair<SerializableDictionary_Caller.MyData1, SerializableDictionary_Caller.MyData1>;

using kvpVV2 = System.Collections.Generic.KeyValuePair<double, string>;

using kvpVR2 = System.Collections.Generic.KeyValuePair<int, SerializableDictionary_Caller.MyData2>;

using kvpRV2 = System.Collections.Generic.KeyValuePair<SerializableDictionary_Caller.MyData2, int>;

using kvpRR2 = System.Collections.Generic.KeyValuePair<SerializableDictionary_Caller.MyData2, SerializableDictionary_Caller.MyData2>;

namespace SerializableDictionary_Caller {
    class Program {
        static void Main(string[] args) {
            string filePathVV1 = @"../Data/SampleVV1.log";
            var dicVV1a = new[]{
                new kvpVV1(0, "Zero"),
                new kvpVV1(1, "One"),
                new kvpVV1(2, "Two"),
            }.ToSerializableDictionary();
            dicVV1a.Export(filePathVV1);
            var dicVV1b = (null as SerializableDictionary<int, string>).Import(filePathVV1);

            string filePathVR1 = @"../Data/SampleVR1.log";
            var dicVR1a = new[]{
                new kvpVR1(0, new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }),
                new kvpVR1(1, new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }),
                new kvpVR1(2, new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }),
            }.ToSerializableDictionary();
            dicVR1a.Export(filePathVR1);
            var dicVR1b = (null as SerializableDictionary<int, SerializableDictionary_Caller.MyData1>).Import(filePathVR1);

            string filePathRV1 = @"../Data/SampleRV1.log";
            var dicRV1a = new[]{
                new kvpRV1(new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }, 0),
                new kvpRV1(new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }, 1),
                new kvpRV1(new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }, 2),
            }.ToSerializableDictionary();
            dicRV1a.Export(filePathRV1);
            var dicRV1b = (null as SerializableDictionary<SerializableDictionary_Caller.MyData1, int>).Import(filePathRV1);

            string filePathRR1 = @"../Data/SampleRR1.log";
            var dicRR1a = new[]{
                new kvpRR1(
                    new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, },
                    new MyData1(){ ID=10, Name="AZero", RegisteredDate=DateTime.Now, Height=175.1, }
                ),
                new kvpRR1(
                    new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, },
                    new MyData1(){ ID=11, Name="AOne", RegisteredDate=DateTime.Now, Height=165.1, }
                ),
                new kvpRR1(
                    new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, },
                    new MyData1(){ ID=12, Name="ATwo", RegisteredDate=DateTime.Now, Height=170.1, }
                ),
            }.ToSerializableDictionary();
            dicRR1a.Export(filePathRR1);
            var dicRR1b = (null as SerializableDictionary<SerializableDictionary_Caller.MyData1, SerializableDictionary_Caller.MyData1>).Import(filePathRR1);

            TypeConverter myData2Converter = TypeDescriptor.GetConverter(typeof(MyData2));
            MyData2 myData2 = (MyData2)myData2Converter.ConvertFromString(
                "{ID:'100', Name:'山田', RegisteredDate:'2014-08-22', Height:'165.4', }"
            );
            var myData2b = XmlHelper<MyData2>.convertToString(myData2);
            Console.WriteLine(myData2b);
            var myData2c = XmlHelper<MyData2>.convertFromString(myData2b);

            string filePathVV2 = @"../Data/SampleVV2.log";
            var dicVV2a = new[]{
                new kvpVV2(0.1, "Zero"),
                new kvpVV2(1.2, "One"),
                new kvpVV2(2.3, "Two"),
            }.ToSerializableDictionary();
            dicVV2a.Export(filePathVV2);
            var dicVV2b = (null as SerializableDictionary<double, string>).Import(filePathVV2);

            string filePathVR2 = @"../Data/SampleVR2.log";
            var dicVR2a = new[]{
                new kvpVR2(0, new MyData2(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }),
                new kvpVR2(1, new MyData2(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }),
                new kvpVR2(2, new MyData2(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }),
            }.ToSerializableDictionary();
            dicVR2a.Export(filePathVR2);
            var dicVR2b = (null as SerializableDictionary<int, SerializableDictionary_Caller.MyData2>).Import(filePathVR2);

            string filePathRV2 = @"../Data/SampleRV2.log";
            var dicRV2a = new[]{
                new kvpRV2(new MyData2(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }, 0),
                new kvpRV2(new MyData2(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }, 1),
                new kvpRV2(new MyData2(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }, 2),
            }.ToSerializableDictionary();
            dicRV2a.Export(filePathRV2);
            var dicRV2 = (null as SerializableDictionary<SerializableDictionary_Caller.MyData2, int>).Import(filePathRV2);

            string filePathRR2 = @"../Data/SampleRR2.log";
            var dicRR2a = new[]{
                new kvpRR2(
                    new MyData2(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, },
                    new MyData2(){ ID=10, Name="AZero", RegisteredDate=DateTime.Now, Height=175.1, }
                ),
                new kvpRR2(
                    new MyData2(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, },
                    new MyData2(){ ID=11, Name="AOne", RegisteredDate=DateTime.Now, Height=165.1, }
                ),
                new kvpRR2(
                    new MyData2(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, },
                    new MyData2(){ ID=12, Name="ATwo", RegisteredDate=DateTime.Now, Height=170.1, }
                ),
            }.ToSerializableDictionary();
            dicRR2a.Export(filePathRR2);
            var dicRR2b = (null as SerializableDictionary<SerializableDictionary_Caller.MyData2, SerializableDictionary_Caller.MyData2>).Import(filePathRR2);

            string filePathMyData3 = @"../Data/SampleMyData3.log";
            var myData3a = new MyData3() {
                Name = "Sample3",
                RGB = new SerializableDictionary<MyData3.RGBTypes, SerializableDictionary<int, double>>(){
                    {
                        MyData3.RGBTypes.R,
                        new SerializableDictionary<int, double>(){
                            {  0,   0}, { 20,  25}, { 40,  50}, { 60,  70}, { 80,  85}, {100, 100},
                        }
                    },
                    {
                        MyData3.RGBTypes.G,
                        new SerializableDictionary<int, double>(){
                            {  0,   0}, { 20,  15}, { 40,  30}, { 60,  50}, { 80,  75}, {100, 100},
                        }
                    },
                    {
                        MyData3.RGBTypes.B,
                        new SerializableDictionary<int, double>(){
                            {  0,   0}, { 20,  25}, { 40,  50}, { 60,  50}, { 80,  75}, {100, 100},
                        }
                    },
                },
                CMYK = new SerializableDictionary<MyData3.CMYKTypes, SerializableDictionary<double, double>>(){
                    {
                        MyData3.CMYKTypes.C,
                        new SerializableDictionary<double, double>(){
                            {0.00, 0.00}, {0.20, 0.25}, {0.40, 0.50}, {0.60, 0.70}, {0.80, 0.85}, {1.00, 1.00},
                        }
                    },
                    {
                        MyData3.CMYKTypes.M,
                        new SerializableDictionary<double, double>(){
                            {0.00, 0.00}, {0.20, 0.15}, {0.40, 0.30}, {0.60, 0.50}, {0.80, 0.75}, {1.00, 1.00},
                        }
                    },
                    {
                        MyData3.CMYKTypes.Y,
                        new SerializableDictionary<double, double>(){
                            {0.00, 0.00}, {0.20, 0.25}, {0.40, 0.50}, {0.60, 0.50}, {0.80, 0.75}, {1.00, 1.00},
                        }
                    },
                    {
                        MyData3.CMYKTypes.K,
                        new SerializableDictionary<double, double>(){
                            {0.00, 0.00}, {0.20, 0.15}, {0.40, 0.30}, {0.60, 0.70}, {0.80, 0.85}, {1.00, 1.00},
                        }
                    },
                },
            };
            myData3a.Export(filePathMyData3);
            var myData3b = (null as MyData3).Import(filePathMyData3);

            string filePathMyData4 = @"../Data/SampleMyData4.log";
            MyData4List myData4a = new MyData4[]{
                new MyData4(){
                    Name = "abc",
                    Size = new Point(1.1, 2.2),
                    Position = new Point(3.3, 4.4),
                },
                new MyData4(){
                    Name = "def",
                    Size = new Point(1.11, 2.22),
                    Position = new Point(3.33, 4.44),
                },
                new MyData4(){
                    Name = "ghi",
                    Size = new Point(1.111, 2.222),
                    Position = new Point(3.333, 4.444),
                },
            };
            myData4a.Export(filePathMyData4);
            var myData4b = (null as MyData4List).Import(filePathMyData4);

            string filePathMyData5 = @"../Data/SampleMyData5.log";
            var myData5a = new []{
                new MyData5(){
                    Position = MyData5.Positions.Floor4,
                    Name = "Charlie",
                    HitPoint = 300,
                    Weapon = new Equipment(){
                        Name= "Hammer",
                        Owner = "Charlie",
                    },
                },
                new MyData5(){
                    Position = MyData5.Positions.Floor3,
                    Name = "Bravo",
                    HitPoint = 200,
                    Weapon = new Equipment(){
                        Name= "Mace",
                        Owner = "Bravo",
                    },
                },
                new MyData5(){
                    Position = MyData5.Positions.FloorB1,
                    Name = "Delta",
                    HitPoint = 500,
                    Weapon = new Equipment(){
                        Name= "Sword",
                        Owner = "Delta",
                    },
                },
                new MyData5(){
                    Position = MyData5.Positions.Floor1,
                    Name = "Alpha",
                    HitPoint = 100,
                    Weapon = new Equipment(){
                        Name= "Club",
                        Owner = "Alpha",
                    },
                },
            }.ToListableDictionary<MyData5s, MyData5.Positions, MyData5>();
            myData5a.Export(filePathMyData5);
            var myData5b = (null as MyData5s).Import(filePathMyData5);
            var myData5c = myData5a.ToList();
            var myData5d = XmlHelper<MyData5s>.convertToString(myData5a);
            Console.WriteLine(myData5d);
            var myData5e = XmlHelper<MyData5s>.convertFromString(myData5d);

            string filePathMyData6 = @"../Data/SampleMyData6.log";
            var myData6a = new MyData6() {
                new MyData6Device("Camera"){
                    new MyData6Channel(MyData6Channel.Channels.Red){
                        new MyData6Item(255, 100),
                        new MyData6Item(127, 50),
                        new MyData6Item(63, 25),
                        new MyData6Item(0, 0),
                    },
                    new MyData6Channel(MyData6Channel.Channels.Green){
                        new MyData6Item(255, 90),
                        new MyData6Item(127, 35),
                        new MyData6Item(63, 10),
                        new MyData6Item(0, 0),
                    },
                    new MyData6Channel(MyData6Channel.Channels.Blue){
                        new MyData6Item(255, 95),
                        new MyData6Item(127, 60),
                        new MyData6Item(63, 35),
                        new MyData6Item(0, 5),
                    },
                },
                new MyData6Device("Monitor"){
                    new MyData6Channel(MyData6Channel.Channels.Blue){
                        new MyData6Item(255, 95),
                        new MyData6Item(127, 60),
                        new MyData6Item(63, 35),
                        new MyData6Item(0, 5),
                    },
                    new MyData6Channel(MyData6Channel.Channels.Green){
                        new MyData6Item(255, 90),
                        new MyData6Item(127, 35),
                        new MyData6Item(63, 10),
                        new MyData6Item(0, 0),
                    },
                    new MyData6Channel(MyData6Channel.Channels.Red){
                        new MyData6Item(255, 100),
                        new MyData6Item(127, 50),
                        new MyData6Item(63, 25),
                        new MyData6Item(0, 0),
                    },
                },
                new MyData6Device("Scanner"){
                    new MyData6Channel(MyData6Channel.Channels.Red){
                        new MyData6Item(255, 100),
                        new MyData6Item(127, 50),
                        new MyData6Item(63, 25),
                        new MyData6Item(0, 0),
                    },
                    new MyData6Channel(MyData6Channel.Channels.Blue){
                        new MyData6Item(255, 95),
                        new MyData6Item(127, 60),
                        new MyData6Item(63, 35),
                        new MyData6Item(0, 5),
                    },
                    new MyData6Channel(MyData6Channel.Channels.Green){
                        new MyData6Item(255, 90),
                        new MyData6Item(127, 35),
                        new MyData6Item(63, 10),
                        new MyData6Item(0, 0),
                    },
                },
            };
            myData6a["Camera"].Size = "8\"";
            myData6a["Monitor"].MakerID = 1000;
            myData6a["Scanner"].ID = "SomeID";
            myData6a.Export(filePathMyData6);
            var myData6ax = myData6a.ToXml();
            var myData6b = (null as MyData6).Import(filePathMyData6);
            var myData6c = (null as MyData6).FromXml(myData6ax);
            myData6a["Printer"][MyData6Channel.Channels.Blue].Add(new MyData6Item(255, 100));
            var myData6d = new MyData6() { Name = "d", };
            var myData6ex = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                "<ListableDictionaryOfStringMyData6Device KeyType=\"String\" Count=\"3\">\r\n" +
                "  <!-- comment -->" +
                "  <Device Name=\"Camera\" KeyType=\"Channels\" Count=\"3\" Size=\"8&quot;\">\r\n" +
                "    <!-- comment -->" +
                "    <Channel Name=\"Red\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <!-- comment -->" +
                "      <Item Index=\"0\" Value=\"0\" />\r\n" +
                "      <Item Index=\"63\" Value=\"25\" />\r\n" +
                "      <Item Index=\"127\" Value=\"50\" />\r\n" +
                "      <Item Index=\"255\" Value=\"100\" />\r\n" +
                "      <!-- comment -->" +
                "    </Channel>\r\n" +
                "    <Channel Name=\"Green\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <Item Index=\"0\" Value=\"0\" />\r\n" +
                "      <Item Index=\"63\" Value=\"10\" />\r\n" +
                "      <Item Index=\"127\" Value=\"35\" />\r\n" +
                "      <Item Index=\"255\" Value=\"90\" />\r\n" +
                "    </Channel>\r\n" +
                "    <Channel Name=\"Blue\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <Item Index=\"0\" Value=\"5\" />\r\n" +
                "      <Item Index=\"63\" Value=\"35\" />\r\n" +
                "      <Item Index=\"127\" Value=\"60\" />\r\n" +
                "      <Item Index=\"255\" Value=\"95\" />\r\n" +
                "    </Channel>\r\n" +
                "  </Device>\r\n" +
                "  <Device Name=\"Monitor\" KeyType=\"Channels\" Count=\"3\" MakerID=\"1000\">\r\n" +
                "    <Channel Name=\"Red\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <Item Index=\"0\" Value=\"0\" />\r\n" +
                "      <Item Index=\"63\" Value=\"25\" />\r\n" +
                "      <Item Index=\"127\" Value=\"50\" />\r\n" +
                "      <Item Index=\"255\" Value=\"100\" />\r\n" +
                "    </Channel>\r\n" +
                "    <Channel Name=\"Green\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <Item Index=\"0\" Value=\"0\" />\r\n" +
                "      <Item Index=\"63\" Value=\"10\" />\r\n" +
                "      <Item Index=\"127\" Value=\"35\" />\r\n" +
                "      <Item Index=\"255\" Value=\"90\" />\r\n" +
                "    </Channel>\r\n" +
                "    <Channel Name=\"Blue\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <Item Index=\"0\" Value=\"5\" />\r\n" +
                "      <Item Index=\"63\" Value=\"35\" />\r\n" +
                "      <Item Index=\"127\" Value=\"60\" />\r\n" +
                "      <Item Index=\"255\" Value=\"95\" />\r\n" +
                "    </Channel>\r\n" +
                "  </Device>\r\n" +
                "  <Device Name=\"Scanner\" KeyType=\"Channels\" Count=\"3\">\r\n" +
                "    <!-- comment -->" +
                "    <Option ID=\"SomeID\">\r\n" +
                "      <CreateDate>2014-11-17T01:35:52.663294+09:00</CreateDate>\r\n" +
                "    </Option>\r\n" +
                "    <!-- comment -->" +
                "    <Channel Name=\"Red\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <Item Index=\"0\" Value=\"0\" />\r\n" +
                "      <Item Index=\"63\" Value=\"25\" />\r\n" +
                "      <Item Index=\"127\" Value=\"50\" />\r\n" +
                "      <Item Index=\"255\" Value=\"100\" />\r\n" +
                "    </Channel>\r\n" +
                "    <Channel Name=\"Green\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <Item Index=\"0\" Value=\"0\" />\r\n" +
                "      <Item Index=\"63\" Value=\"10\" />\r\n" +
                "      <Item Index=\"127\" Value=\"35\" />\r\n" +
                "      <Item Index=\"255\" Value=\"90\" />\r\n" +
                "    </Channel>\r\n" +
                "    <Channel Name=\"Blue\" KeyType=\"Int32\" Count=\"4\">\r\n" +
                "      <Item Index=\"0\" Value=\"5\" />\r\n" +
                "      <Item Index=\"63\" Value=\"35\" />\r\n" +
                "      <Item Index=\"127\" Value=\"60\" />\r\n" +
                "      <Item Index=\"255\" Value=\"95\" />\r\n" +
                "    </Channel>\r\n" +
                "  </Device>\r\n" +
                "</ListableDictionaryOfStringMyData6Device>";
            var myData6e = (null as MyData6).FromXml(myData6ex);

            var myData7a = new MyData7() {
                new MyData7Item("A"),
                new MyData7Item("B"),
                new MyData7Item("C"),
                new MyData7Item("D"),
                new MyData7Item("E"),
            };
            myData7a["B"].Index = 20;
            myData7a["C"].Index = -1;
            myData7a["D"].Index = 10;
            var myData7ax = myData7a.ToXml();
            var myData7b = (null as MyData7).FromXml(myData7ax);
        }
    }
}
