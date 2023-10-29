using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace Infrastructure.Healthcheck.Concrete.MySql
{
    public class HealtCheckMySqlHandler : IHealtCheckMySqlHandler
    {
        public MySqlConnection MySqlConnection { get; private set; }
        public HealtCheckMySqlHandler(IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("JellyfishMySqlDatabase");
            MySqlConnection = new MySqlConnection(connectionString);

        }

        public QueryResponseData<T> ExecuteQueryWithDapper<T>(string query, object param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            QueryResponseData<T> responseValue = new QueryResponseData<T>(query);

            DataTable responseData = new DataTable();
            DataTable responseMetaData = new DataTable();

            if (string.IsNullOrEmpty(query))
                return null;

            bool openedConnection = Open();
            if (openedConnection)
            {

                try
                {
                    var response = MySqlConnection.Query<T>(query);
                    responseValue.DataStorage = response.ToList();
                    responseValue.Message = response.Count() + " returned";
                }
                catch (MySqlException ex)
                {
                    responseValue.Number = ex.Number;
                    responseValue.Exception = ex;
                    responseValue.ErrorCode = ex.ErrorCode;
                    responseValue.Message = ex.Message;
                }
                Close();
            }

            responseValue.Data = responseData;
            responseValue.MetaData = responseMetaData;


            return responseValue;
        }
        public bool Open()
        {
            if (MySqlConnection.State != ConnectionState.Open)
            {
                try
                {
                    MySqlConnection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                }
                return false;
            }
            return true;
        }
        public void Close()
        {
            if (MySqlConnection.State == ConnectionState.Open)
            {
                try
                {
                    MySqlConnection.Close();
                }
                catch (Exception ex)
                {
                }
            }
        }
        public async Task<QueryResponseData<T>> ExecuteQueryWithDapperAsync<T>(string query, object param = null, [CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            QueryResponseData<T> responseValue = new QueryResponseData<T>(query);

            DataTable responseData = new DataTable();
            DataTable responseMetaData = new DataTable();

            if (string.IsNullOrEmpty(query))
                return null;

            bool openedConnection = await OpenAsync();
            if (openedConnection)
            {

                try
                {
                    var response = await MySqlConnection.QueryAsync<T>(query);
                    responseValue.DataStorage = response.ToList();
                    responseValue.Message = response.Count() + " returned";
                }
                catch (MySqlException ex)
                {
                    responseValue.Number = ex.Number;
                    responseValue.Exception = ex;
                    responseValue.ErrorCode = ex.ErrorCode;
                    responseValue.Message = ex.Message;
                }
                CloseAsync();
            }

            responseValue.Data = responseData;
            responseValue.MetaData = responseMetaData;


            return responseValue;
        }
        public async Task<bool> OpenAsync()
        {
            if (MySqlConnection.State != ConnectionState.Open)
            {
                try
                {
                    await MySqlConnection.OpenAsync();
                    return true;
                }
                catch (Exception ex)
                {
                }
                return false;
            }
            return true;
        }
        public async void CloseAsync()
        {
            if (MySqlConnection.State == ConnectionState.Open)
            {
                try
                {
                    await MySqlConnection.CloseAsync();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Testmethod for healthcheck
        /// </summary>
        /// <returns></returns>
        public async Task<int> SelectOne()
        {
            int response = 0;
            if (MySqlConnection.State != ConnectionState.Open)
            {
                await MySqlConnection.OpenAsync();

            }

            using (var command = new MySqlCommand("SELECT 1;", MySqlConnection))
            {
                command.CommandType = CommandType.Text;
                using (var reader = await command.ExecuteReaderAsync())
                {

                    if (reader.FieldCount != 0)
                    {
                        DataTable responseData = new DataTable();

                        int relativeIncrement = 0;
                        bool run = true;
                        DateTime startGetData = DateTime.Now.AddSeconds(60);
                        DataTable schemaData = reader.GetSchemaTable();
                        DataTable responseMetaData = schemaData;

                        while (await reader.ReadAsync() && run)
                        {
                            if (startGetData <= DateTime.Now)
                            {
                                run = false;
                            }
                            DataRow row = responseData.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                object val = await reader.GetFieldValueAsync<object>(i);
                                string columnName = reader.GetName(i);
                                if (columnName == null) continue;
                                if (!ColumnExistInDataTable(responseData, columnName))
                                {
                                    responseData.Columns.Add(columnName, reader.GetFieldType(i));
                                }
                                row[columnName] = val;
                            }

                            if (row != null)
                            {
                                responseData.Rows.Add(row);
                            }

                            relativeIncrement++;
                        }
                        if (responseData.Rows.Count > 0)
                        {
                            if (int.TryParse(responseData.Rows[0][0].ToString(), out response))
                            {

                            }
                        }

                    }
                    reader.Close();
                }
            }

            await MySqlConnection.CloseAsync();
            return response;
        }

        public bool ColumnExistInDataTable(DataTable dataTable, string columnName)
        {
            if (dataTable == null)
                return false;

            if (columnName == null)
                return false;

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                string tmpColumnName = dataTable.Columns[i].ColumnName.ToLower();
                if (tmpColumnName.Equals(columnName.ToLower()))
                    return true;
            }
            return false;
        }
    }
}
