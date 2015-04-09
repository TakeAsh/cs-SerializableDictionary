using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TakeAsh {

    /// <summary>
    /// Safe ToString Method
    /// </summary>
    /// <remarks>
    /// [c# - Checking for null before ToString() - Stack Overflow](http://stackoverflow.com/questions/550374/)
    /// </remarks>
    public static class ObjectExtensionMethods {

        /// <summary>
        /// check null before ToString()
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="valueIfNull">value if object is null</param>
        /// <returns>
        /// <item><term>object is not null</term><description>object.ToString()</description></item>
        /// <item><term>object is null</term><description>valueIfNull</description></item>
        /// </returns>
        public static string SafeToString(this object obj, string valueIfNull = "") {
            return obj != null ?
                obj.ToString() :
                valueIfNull;
        }
    }
}
