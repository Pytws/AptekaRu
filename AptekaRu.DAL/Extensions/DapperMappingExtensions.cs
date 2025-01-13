using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptekaRu.DAL.Extensions
{
    public static class DapperMappingExtensions
    {
        public static void AddDapperTypeMappings<T>(Assembly assembly, string namePropertyAttribute) where T : Attribute
        {
            var classTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract);

            foreach (var classType in classTypes)
            {
                SqlMapper.SetTypeMap(classType, new CustomPropertyTypeMap(classType,
                    (type, columnName) =>
                    {
                        return type.GetProperties().FirstOrDefault(p =>
                        {
                            var attribute = p.GetCustomAttribute<T>();

                            if (attribute is null) return false;

                            var nameProperty = typeof(T).GetProperty(namePropertyAttribute);

                            if (nameProperty is null) return false;

                            var nameValue = nameProperty.GetValue(attribute) as string;

                            return nameValue == columnName;
                        }) ?? throw new NullReferenceException(nameof(type));
                    }));
            }

        }
    }
}
