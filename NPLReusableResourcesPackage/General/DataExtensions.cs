using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NPLReusableResourcesPackage.General
{
    public static class DataExtensions
    {
        public static async Task<IEnumerable<T>> MapToListAsync<T>(this DbDataReader reader)
           where T : new()
        {
            if (reader != null && reader.HasRows)
            {
                var item = typeof(T);
                var items = new List<T>();
                var propertySet = new Dictionary<string, PropertyInfo>();
                var properties = item.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propertySet = properties.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (await reader.ReadAsync())
                {
                    var newItem = new T();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var key = reader.GetName(i).ToUpper();

                        if (propertySet.ContainsKey(key))
                        {
                            var propertyInfo = propertySet[key];

                            if ((propertyInfo != null) && propertyInfo.CanWrite)
                            {
                                var value = reader.GetValue(i);
                                propertyInfo.SetValue(newItem, (value == DBNull.Value) ? null : value, null);
                            }
                        }
                    }
                    items.Add(newItem);
                }
                return items;
            }
            return null;
        }
    }
}
