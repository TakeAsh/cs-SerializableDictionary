using System;
using System.ComponentModel;

using ieVV = SerializableDictionaryTest.ImExPorter<int, string>;
using kvpVV = System.Collections.Generic.KeyValuePair<int, string>;

using ieVR = SerializableDictionaryTest.ImExPorter<int, SerializableDictionaryTest.MyData1>;
using kvpVR = System.Collections.Generic.KeyValuePair<int, SerializableDictionaryTest.MyData1>;

using ieRV = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData1, int>;
using kvpRV = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData1, int>;

using ieRR = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData1, SerializableDictionaryTest.MyData1>;
using kvpRR = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData1, SerializableDictionaryTest.MyData1>;

namespace SerializableDictionaryTest {
    class Program {
        static void Main(string[] args) {
            string filePathAA = @"../Data/SampleAA.log";
            var dicAA1 = ieVV.create(new kvpVV[]{
                new kvpVV(0, "Zero"),
                new kvpVV(1, "One"),
                new kvpVV(2, "Two"),
            });
            ieVV.export(dicAA1, filePathAA);
            var dicAA2 = ieVV.import(filePathAA);

            string filePathAE = @"../Data/SampleAE.log";
            var dicAE1 = ieVR.create(new kvpVR[]{
                new kvpVR(0, new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }),
                new kvpVR(1, new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }),
                new kvpVR(2, new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }),
            });
            ieVR.export(dicAE1, filePathAE);
            var dicAE2 = ieVR.import(filePathAE);

            string filePathEA = @"../Data/SampleEA.log";
            var dicEA1 = ieRV.create(new kvpRV[]{
                new kvpRV(new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }, 0),
                new kvpRV(new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }, 1),
                new kvpRV(new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }, 2),
            });
            ieRV.export(dicEA1, filePathEA);
            var dicEA2 = ieRV.import(filePathEA);

            string filePathEE = @"../Data/SampleEE.log";
            var dicEE1 = ieRR.create(new kvpRR[]{
                new kvpRR(
                    new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, },
                    new MyData1(){ ID=10, Name="AZero", RegisteredDate=DateTime.Now, Height=175.1, }
                ),
                new kvpRR(
                    new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, },
                    new MyData1(){ ID=11, Name="AOne", RegisteredDate=DateTime.Now, Height=165.1, }
                ),
                new kvpRR(
                    new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, },
                    new MyData1(){ ID=12, Name="ATwo", RegisteredDate=DateTime.Now, Height=170.1, }
                ),
            });
            ieRR.export(dicEE1, filePathEE);
            var dicEE2 = ieRR.import(filePathEE);

            TypeConverter myData2Converter = TypeDescriptor.GetConverter(typeof(MyData2));
            MyData2 myData2 = (MyData2)myData2Converter.ConvertFromString(
                "{ID:'100', Name:'山田', RegisteredDate:'2014-08-22', Height:'165.4', }"
            );
        }
    }
}
