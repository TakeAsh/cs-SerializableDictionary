using System;

using ieAA = SerializableDictionaryTest.ImExPorter<int, string>;
using kvpAA = System.Collections.Generic.KeyValuePair<int, string>;

using ieAE = SerializableDictionaryTest.ImExPorter<int, SerializableDictionaryTest.MyData>;
using kvpAE = System.Collections.Generic.KeyValuePair<int, SerializableDictionaryTest.MyData>;

using ieEE = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData, SerializableDictionaryTest.MyData>;
using kvpEE = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData, SerializableDictionaryTest.MyData>;

namespace SerializableDictionaryTest {
    class Program {
        static void Main(string[] args) {
            string filePathAA = @"../Data/SampleAA.log";
            var dicAA1 = ieAA.create(new kvpAA[]{
                new kvpAA(0, "Zero"),
                new kvpAA(1, "One"),
                new kvpAA(2, "Two"),
            });
            ieAA.export(dicAA1, filePathAA);
            var dicAA2 = ieAA.import(filePathAA);

            string filePathAE = @"../Data/SampleAE.log";
            var dicAE1 = ieAE.create(new kvpAE[]{
                new kvpAE(0, new MyData(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }),
                new kvpAE(1, new MyData(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }),
                new kvpAE(2, new MyData(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }),
            });
            ieAE.export(dicAE1, filePathAE);
            var dicAE2 = ieAE.import(filePathAE);

            string filePathEE = @"../Data/SampleEE.log";
            var dicEE1 = ieEE.create(new kvpEE[]{
                new kvpEE(
                    new MyData(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, },
                    new MyData(){ ID=10, Name="AZero", RegisteredDate=DateTime.Now, Height=175.1, }
                ),
                new kvpEE(
                    new MyData(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, },
                    new MyData(){ ID=11, Name="AOne", RegisteredDate=DateTime.Now, Height=165.1, }
                ),
                new kvpEE(
                    new MyData(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, },
                    new MyData(){ ID=12, Name="ATwo", RegisteredDate=DateTime.Now, Height=170.1, }
                ),
            });
            ieEE.export(dicEE1, filePathEE);
            var dicEE2 = ieEE.import(filePathEE);
        }
    }
}
