using System;
using System.ComponentModel;
using System.Globalization;

namespace TakeAsh {
    public interface IStringify<T> {
        /// <summary>
        /// Serialize an object as a string.
        /// </summary>
        /// <returns>serialized object</returns>
        string ToString();

        /// <summary>
        /// Deserialize an object from a string.
        /// </summary>
        /// <param name="source">serialized object</param>
        /// <returns>deserialized object</returns>
        T FromString(string source);
    }

    public class StringifyConverter<T> : TypeConverter
        where T : IStringify<T> {
        public override bool CanConvertTo(
            ITypeDescriptorContext context,
            Type destinationType
        ) {
            if (destinationType == typeof(string)) {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType
        ) {
            if (destinationType == typeof(string)) {
                return ((T)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(
            ITypeDescriptorContext context,
            Type sourceType
        ) {
            if (sourceType == typeof(string)) {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value
        ) {
            if (value is string) {
                return default(T).FromString((string)value);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
