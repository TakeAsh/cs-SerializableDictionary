using System;

using ieAA = SerializableDictionaryTest.ImExPorter<int, string>;
using kvpAA = System.Collections.Generic.KeyValuePair<int, string>;

using ieAE = SerializableDictionaryTest.ImExPorter<int, SerializableDictionaryTest.MyData1>;
using kvpAE = System.Collections.Generic.KeyValuePair<int, SerializableDictionaryTest.MyData1>;

using ieEA = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData1, int>;
using kvpEA = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData1, int>;

using ieEE = SerializableDictionaryTest.ImExPorter<SerializableDictionaryTest.MyData1, SerializableDictionaryTest.MyData1>;
using kvpEE = System.Collections.Generic.KeyValuePair<SerializableDictionaryTest.MyData1, SerializableDictionaryTest.MyData1>;

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
                new kvpAE(0, new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }),
                new kvpAE(1, new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }),
                new kvpAE(2, new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }),
            });
            ieAE.export(dicAE1, filePathAE);
            var dicAE2 = ieAE.import(filePathAE);

            string filePathEA = @"../Data/SampleEA.log";
            var dicEA1 = ieEA.create(new kvpEA[]{
                new kvpEA(new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, }, 0),
                new kvpEA(new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, }, 1),
                new kvpEA(new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, }, 2),
            });
            ieEA.export(dicEA1, filePathEA);
            var dicEA2 = ieEA.import(filePathEA);

            string filePathEE = @"../Data/SampleEE.log";
            var dicEE1 = ieEE.create(new kvpEE[]{
                new kvpEE(
                    new MyData1(){ ID=0, Name="Zero", RegisteredDate=DateTime.Now, Height=170.0, },
                    new MyData1(){ ID=10, Name="AZero", RegisteredDate=DateTime.Now, Height=175.1, }
                ),
                new kvpEE(
                    new MyData1(){ ID=1, Name="One", RegisteredDate=DateTime.Now, Height=160.0, },
                    new MyData1(){ ID=11, Name="AOne", RegisteredDate=DateTime.Now, Height=165.1, }
                ),
                new kvpEE(
                    new MyData1(){ ID=2, Name="Two", RegisteredDate=DateTime.Now, Height=165.0, },
                    new MyData1(){ ID=12, Name="ATwo", RegisteredDate=DateTime.Now, Height=170.1, }
                ),
            });
            ieEE.export(dicEE1, filePathEE);
            var dicEE2 = ieEE.import(filePathEE);
        }
    }
}
