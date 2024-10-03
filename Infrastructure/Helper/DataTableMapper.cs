using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public static class DataTableMapper
    {
        public static List<T> DataTableToList<T>(DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();
            var properties = typeof(T).GetProperties();

            foreach (DataRow row in dataTable.Rows)
            {
                T item = new T();

                foreach (var prop in properties)
                {
                    if (dataTable.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        try
                        {
                            if (propType.IsEnum)
                            {
                                prop.SetValue(item, Enum.Parse(propType, row[prop.Name].ToString()));
                            }
                            else
                            {
                                prop.SetValue(item, Convert.ChangeType(row[prop.Name], propType));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error converting property {prop.Name}: {ex.Message}");
                        }
                    }
                }

                dataList.Add(item);
            }

            return dataList;
        }
       
        public static T DataRowToObject<T>(DataRow row) where T : new()
        {
            T item = new T();
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                if (row.Table.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                {
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    prop.SetValue(item, Convert.ChangeType(row[prop.Name], propType));
                }

            }

            return item;
        }

    }
}
