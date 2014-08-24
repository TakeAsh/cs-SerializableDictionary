﻿using System;
using System.ComponentModel;

using ieVV1 = SerializableDictionaryTest.ImExPorter<int, string>;
using kvpVV1 = System.Collections.Generic.KeyValuePair<int, string>;

using ieVR1 = SerializableDictionaryTest.ImExPorter<int, SerializableDictionaryTest.MyData1>;
using kvpVR1 = System.Collections.Generic.KeyValuePair<int, SerializableDictionaryTest.MyData1>;

using ieRV1 = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData1, int>;
using kvpRV1 = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData1, int>;

using ieRR1 = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData1, SerializableDictionaryTest.MyData1>;
using kvpRR1 = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData1, SerializableDictionaryTest.MyData1>;

using ieVV2 = SerializableDictionaryTest.ImExPorter<double, string>;
using kvpVV2 = System.Collections.Generic.KeyValuePair<double, string>;

using ieVR2 = SerializableDictionaryTest.ImExPorter<int, SerializableDictionaryTest.MyData2>;
using kvpVR2 = System.Collections.Generic.KeyValuePair<int, SerializableDictionaryTest.MyData2>;

using ieRV2 = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData2, int>;
using kvpRV2 = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData2, int>;

using ieRR2 = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData2, SerializableDictionaryTest.MyData2>;
using kvpRR2 = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData2, SerializableDictionaryTest.MyData2>;

namespace SerializableDictionaryTest {
    class Program {
        static void Main(string[] args) {
            string filePathVV1 = @"../Data/SampleVV1.log";
            var dicVV1a = ieVV1.create(new kvpVV1[]{
                new kvpVV1(0, "Zero"),
                new kvpVV1(1, "One"),
                new kvpVV1(2, "Two"),
            });
            ieVV1.export(dicVV1a, filePathVV1);
            var dicVV1b = ieVV1.import(filePathVV1);

            string filePathVR1 = @"../Data/SampleVR1.log";
            var dicVR1a = ieVR1.create(new kvpVR1[]{
                new kvpVR1(0, new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }),
                new kvpVR1(1, new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }),
                new kvpVR1(2, new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }),
            });
            ieVR1.export(dicVR1a, filePathVR1);
            var dicVR1b = ieVR1.import(filePathVR1);

            string filePathRV1 = @"../Data/SampleRV1.log";
            var dicRV1a = ieRV1.create(new kvpRV1[]{
                new kvpRV1(new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }, 0),
                new kvpRV1(new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }, 1),
                new kvpRV1(new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }, 2),
            });
            ieRV1.export(dicRV1a, filePathRV1);
            var dicRV1b = ieRV1.import(filePathRV1);

            string filePathRR1 = @"../Data/SampleRR1.log";
            var dicRR1a = ieRR1.create(new kvpRR1[]{
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
            ieRR1.export(dicRR1a, filePathRR1);
            var dicRR1b = ieRR1.import(filePathRR1);

            TypeConverter myData2Converter = TypeDescriptor.GetConverter(typeof(MyData2));
            MyData2 myData2 = (MyData2)myData2Converter.ConvertFromString(
                "{ID:'100', Name:'山田', RegisteredDate:'2014-08-22', Height:'165.4', }"
            );

            string filePathVV2 = @"../Data/SampleVV2.log";
            var dicVV2a = ieVV2.create(new kvpVV2[]{
                new kvpVV2(0.1, "Zero"),
                new kvpVV2(1.2, "One"),
                new kvpVV2(2.3, "Two"),
            });
            ieVV2.export(dicVV2a, filePathVV2);
            var dicVV2b = ieVV2.import(filePathVV2);

            string filePathVR2 = @"../Data/SampleVR2.log";
            var dicVR2a = ieVR2.create(new kvpVR2[]{
                new kvpVR2(0, new MyData2(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }),
                new kvpVR2(1, new MyData2(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }),
                new kvpVR2(2, new MyData2(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }),
            });
            ieVR2.export(dicVR2a, filePathVR2);
            var dicVR2b = ieVR2.import(filePathVR2);

            string filePathRV2 = @"../Data/SampleRV2.log";
            var dicRV2a = ieRV2.create(new kvpRV2[]{
                new kvpRV2(new MyData2(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }, 0),
                new kvpRV2(new MyData2(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }, 1),
                new kvpRV2(new MyData2(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }, 2),
            });
            ieRV2.export(dicRV2a, filePathRV2);
            var dicRV2 = ieRV2.import(filePathRV2);

            string filePathRR2 = @"../Data/SampleRR2.log";
            var dicRR2a = ieRR2.create(new kvpRR2[]{
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
            });
            ieRR2.export(dicRR2a, filePathRR2);
            var dicRR2b = ieRR2.import(filePathRR2);
        }
    }
}
