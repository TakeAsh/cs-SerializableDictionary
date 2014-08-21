using System;

using ieAA = SerializableDictionaryTest.ImExPorter<int, string>;
using ieEE = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData, SerializableDictionaryTest.MyData>;
using kvpAA = System.Collections.Generic.KeyValuePair<int, string>;
using kvpEE = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData, SerializableDictionaryTest.MyData>;

namespace SerializableDictionaryTest {
    class Program {
        static void Main(string[] args) {
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

            string filePathAA = @"../Data/SampleAA.log";
            var dicAA1 = ieAA.create(new kvpAA[]{
                new kvpAA(0, "Zero"),
                new kvpAA(1, "One"),
                new kvpAA(2, "Two"),
            });
            ieAA.export(dicAA1, filePathAA);
            var dicAA2 = ieAA.import(filePathAA);
        }
    }
}
