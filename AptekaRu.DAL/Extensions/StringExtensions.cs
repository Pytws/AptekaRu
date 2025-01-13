using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AptekaRu.DAL.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string value)
        {
            return Regex.Replace(value, @"([a-z])([A-Z])", "$1_$2").ToLower();
        }

        public static string ToPascalCase(this string value)
        {
            return Regex.Replace(value, @"(^\w)|(_+)(\w)", math =>
            {
                var groupValue = math.Value.Replace("_", "");
                return groupValue.ToUpper();
            });
        }
    }
}
