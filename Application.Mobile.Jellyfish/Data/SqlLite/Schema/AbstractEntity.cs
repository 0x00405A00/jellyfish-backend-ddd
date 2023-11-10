using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MobileApp.Data.SqlLite.Schema
{
    public class AbstractEntity
    {
    }
    public static class AbstractEntitiyExtension
    {
        public static string GetTableName(this AbstractEntity entity)
        {
            return entity.GetType().GetCustomAttributes<SQLite.TableAttribute>()?.First().Name;
        }
    }
}
