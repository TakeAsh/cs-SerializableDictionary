using System;
using System.ComponentModel;

namespace TakeAsh {

    /// <summary>
    /// [C# - TryParse系のメソッドで一時変数を用意したくない… - Qiita](http://qiita.com/Temarin_PITA/items/9aac6c1f569fc2113e0d)
    /// </summary>
    public static class StringExtensionMethods {

        /// <summary>
        /// Convert string to object
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <param name="text">text to convert</param>
        /// <returns>T type object</returns>
        public static T TryParse<T>(this string text)
            where T : struct {

            return text.TryParse(default(T));
        }

        /// <summary>
        /// Convert string to object
        /// </summary>
        /// <typeparam name="T">target type</typeparam>
        /// <param name="text">text to convert</param>
        /// <param name="defaultValue">return value if fail</param>
        /// <returns>T type object</returns>
        public static T TryParse<T>(this string text, T defaultValue)
            where T : struct {

            // コンバーターを作成
            var converter = TypeDescriptor.GetConverter(typeof(T));

            // 変換不可能な場合は規定値を返す
            if (!converter.CanConvertFrom(typeof(string))) {
                return defaultValue;
            }

            try {
                // 変換した値を返す
                return (T)converter.ConvertFrom(text);
            }
            catch {
                // 変換に失敗したら規定値を返す
                return defaultValue;
            }
        }

        /// <summary>
        /// Return defalut value if text is null or empty
        /// </summary>
        /// <param name="text">text to test</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>
        /// <list type="table">
        /// <item><term>text is neither null nor empty</term><description>text</description></item>
        /// <item><term>text is null or empty</term><description>default value</description></item>
        /// </list>
        /// </returns>
        public static string ToDefaultIfNullOrEmpty(this string text, string defaultValue = "") {
            return !String.IsNullOrEmpty(text) ?
                text :
                defaultValue;
        }
    }
}
