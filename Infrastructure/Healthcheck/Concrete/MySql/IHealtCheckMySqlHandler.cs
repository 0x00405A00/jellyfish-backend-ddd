using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Infrastructure.Healthcheck.Concrete.MySql
{
    public interface IHealtCheckMySqlHandler
    {
        DbConnection DatabaseConnection { get; }

        void Close();
        void CloseAsync();
        bool ColumnExistInDataTable(DataTable dataTable, string columnName);
        QueryResponseData<T> ExecuteQueryWithDapper<T>(string query, object param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
        Task<QueryResponseData<T>> ExecuteQueryWithDapperAsync<T>(string query, object param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0);
        bool Open();
        Task<bool> OpenAsync();
        Task<int> SelectOne();
    }
}