using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SerializableDictionaryTest {
    class MyData2Converter : TypeConverter {
        static Regex regInner = new Regex(@"^\s*\{\s*(?<inner>.*)\s*\}\s*$");
        static Regex regProperty = new Regex(@"\s*(?<key>[^\s:]+)\s*:\s*'(?<value>[^']+)'\s*");

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            if (sourceType == typeof(string)) {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            if (value is string) {
                MatchCollection mcInner = regInner.Matches((string)value);
                string strInner = mcInner[0].Groups["inner"].Value;
                string[] strProperties = strInner.Split(new char[] { ',' });
                var ret = new MyData2();
                foreach(var strProp in strProperties){
                    MatchCollection mcProp = regProperty.Matches(strProp);
                    foreach (Match m in mcProp) {
                        ret[m.Groups["key"].Value] = m.Groups["value"].Value;
                    }
                }
                return ret;
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
