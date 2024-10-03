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
                        prop.SetValue(item, Convert.ChangeType(row[prop.Name], prop.PropertyType));
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
                    prop.SetValue(item, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                }
            }

            return item;
        }

    }
}
