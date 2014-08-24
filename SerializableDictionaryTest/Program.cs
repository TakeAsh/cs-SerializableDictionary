using System;
using System.ComponentModel;

using ieVV1 = SerializableDictionaryTest.ImExPorter<int, string>;
using kvpVV1 = System.Collections.Generic.KeyValuePair<int, string>;

using ieVR1 = SerializableDictionaryTest.ImExPorter<int, SerializableDictionaryTest.MyData1>;
using kvpVR1 = System.Collections.Generic.KeyValuePair<int, SerializableDictionaryTest.MyData1>;

using ieRV1 = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData1, int>;
using kvpRV1 = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData1, int>;

using ieRR1 = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData1, SerializableDictionaryTest.MyData1>;
using kvpRR1 = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData1, SerializableDictionaryTest.MyData1>;

namespace SerializableDictionaryTest {
    class Program {
        static void Main(string[] args) {
            string filePathVV1 = @"../Data/SampleVV1.log";
            var dicVV1 = ieVV1.create(new kvpVV1[]{
                new kvpVV1(0, "Zero"),
                new kvpVV1(1, "One"),
                new kvpVV1(2, "Two"),
            });
            ieVV1.export(dicVV1, filePathVV1);
            var dicVV2 = ieVV1.import(filePathVV1);

            string filePathVR1 = @"../Data/SampleVR1.log";
            var dicVR1 = ieVR1.create(new kvpVR1[]{
                new kvpVR1(0, new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }),
                new kvpVR1(1, new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }),
                new kvpVR1(2, new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }),
            });
            ieVR1.export(dicVR1, filePathVR1);
            var dicVR2 = ieVR1.import(filePathVR1);

            string filePathRV1 = @"../Data/SampleRV1.log";
            var dicRV1 = ieRV1.create(new kvpRV1[]{
                new kvpRV1(new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }, 0),
                new kvpRV1(new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }, 1),
                new kvpRV1(new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }, 2),
            });
            ieRV1.export(dicRV1, filePathRV1);
            var dicRV2 = ieRV1.import(filePathRV1);

            string filePathRR1 = @"../Data/SampleRR1.log";
            var dicRR1 = ieRR1.create(new kvpRR1[]{
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
            });
            ieRR1.export(dicRR1, filePathRR1);
            var dicRR2 = ieRR1.import(filePathRR1);

            TypeConverter myData2Converter = TypeDescriptor.GetConverter(typeof(MyData2));
            MyData2 myData2 = (MyData2)myData2Converter.ConvertFromString(
                "{ID:'100', Name:'山田', RegisteredDate:'2014-08-22', Height:'165.4', }"
            );
        }
    }
}
