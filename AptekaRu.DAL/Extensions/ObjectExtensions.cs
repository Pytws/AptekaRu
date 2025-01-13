using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptekaRu.DAL.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, string> GetJsonPropertyName(this object obj)
        {
            var propertyAttr = new Dictionary<string, string>();

            var type = obj.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();

                if (attribute != null)
                {
                    propertyAttr.Add(property.Name, attribute.Name);
                }
            }

            return propertyAttr;
        }

    }
}
