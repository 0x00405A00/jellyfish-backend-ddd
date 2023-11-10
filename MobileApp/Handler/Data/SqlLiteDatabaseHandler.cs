using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Data.SqlLite.Schema;
using MobileApp.Extension;
using MobileApp.Handler.Abstraction;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace MobileApp.Handler.Data
{


    public class SqlLiteDatabaseHandler<T> : AbstractDeviceActionHandler<Permissions.StorageRead, Permissions.StorageWrite>,IDisposable
        where T : AbstractEntity,new()
    {
        public string DatabasePath { get;private set; }
        public override void SetUserDeniedAction()
        {
            UserDeniedAction = () => { };
        }

        public override void SetUserRationalAction()
        {
            UserRationalAction = () => { };
        }
        public SQLiteAsyncConnection DatabaseHandle
        {
            get; private set;
        }

        //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-7.0
        public SqlLiteDatabaseHandler() 
        {

        }
        public virtual async Task<bool> Init(string databasePath, SQLiteOpenFlags openFlags) {

            DatabasePath = databasePath;    
            bool dbFileExists = File.Exists(databasePath);
            var response = OpenDatabase(databasePath, openFlags);
            if (!dbFileExists)
            {
            }

            var createRestults = await CreateSchema();
            return createRestults != null&&createRestults.Results?.Count() > 0;
        }

        public bool OpenDatabase(string databasePath, SQLiteOpenFlags openFlags)
        {
            try
            {
                DatabaseHandle = new SQLiteAsyncConnection(databasePath, openFlags);
                if (DatabaseHandle == null)
                    throw new Exception();
                if(DatabaseHandle.Trace ==false)//Abfrage sollte bei TypeInitializationException innerhalb hier in unsere Assembly einen NullRefExc. auslösen
                    //da in einigen Versionen die SQLLite PCL buggy ist, d.h. hier diese eigentlich "unsinnige" Abfrage
                {

                }
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public List<Type> GetAllDatabaseEntitiesFromAssembly()
        {
            var allAbstractsFromTinAssembly = Assembly.GetExecutingAssembly().GetTypes()?.ToList().FindAll(x => x.BaseType == typeof(T) || x.DeclaringType == typeof(T));
            return allAbstractsFromTinAssembly;
        }

        public async Task<CreateTablesResult> CreateSchema(CreateFlags createFlags = CreateFlags.None)
        {

            var allAbstractsFromTinAssembly = GetAllDatabaseEntitiesFromAssembly();
            if (allAbstractsFromTinAssembly != null)
            {
                try
                {
                    var createResult = await DatabaseHandle.CreateTablesAsync(createFlags, allAbstractsFromTinAssembly.ToArray());

                    return createResult;
                }
                catch (Exception ex) { 
                
                }
            }
            return null;
        }

        /// <summary>
        /// Funktion prüft ob Assembly Types für Databasetables aus der Runtime noch der Struktur entsprechen wie sie in der Runtime vorkommen,
        /// wenn diese sich unterscheiden werden die Tabellen altered oder ob vorhanden oder nicht gelöscht/hinzugefügt
        /// Die Datensätze werden bei einer Strukturanpassung auf die neue Zielstruktur umgesetzt
        /// </summary>
        /// <param name="createFlags"></param>
        /*public async void MigrateSchema(CreateFlags createFlags = CreateFlags.None)
        {
            //Runtime Types aus der Assembly mit der Struktur in der Database abgleichen (update/delete wenn Klassen ungleich zugehöriger Table in Database)
            try
            {
                var allAbstractsFromTinAssembly = GetAllDatabaseEntitiesFromAssembly();
                if (allAbstractsFromTinAssembly != null && allAbstractsFromTinAssembly.Any())
                {
                    foreach (var type in allAbstractsFromTinAssembly)
                    {
                        var tableContent = await GetItemsFromTableFromType(type);
                        if (tableContent != null)
                        {
                            var firstRow = tableContent.FirstOrDefault();
                            if (firstRow != null)
                            {
                                var isTableEqualThanClass = firstRow.GetType().IsClassStructEqualLike(type,true,false);
                                var isClassEqualThanTable = type.IsClassStructEqualLike(firstRow.GetType(),true, false);


                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }*/
        public async Task<List<object>> GetItemsFromTableFromType(Type type)
        {
            return await (Task<List<object>>)this.GetType().
                GetMethod("GetItemsFromTable").
                MakeGenericMethod(new Type[] { type }).
                Invoke(null,null);
        }
        public async Task<List<T1>> Select<T1>(Func<T1,bool> whereClause = null) 
            where T1 : AbstractEntity,new()
        {
            var res = await DatabaseHandle.Table<T1>().ToListAsync();
            if(whereClause != null)
            {
                res = res.Where(whereClause).ToList();

            }
            return res;
        }
        public async Task<TResult?> Max<T1,TResult>(Func<T1,TResult> comparer)
            where T1 : AbstractEntity, new()
        {

            var maxVal = (await this.Select<T1>()).Max<T1,TResult>(comparer);
            return maxVal;
        }
        public async Task<int> Insert<T1>(T1 value)
            where T1 : AbstractEntity, new()
        {
            var res = await DatabaseHandle.InsertAsync(value);
            return res;
        }
        public async Task<int> Update<T1>(T1 value)
            where T1 : AbstractEntity, new()
        {
            var res = await DatabaseHandle.UpdateAsync(value);
            return res;
        }
        public async Task<int> Delete<T1>(T1 value)
            where T1 : AbstractEntity, new()
        {
            var res = await DatabaseHandle.DeleteAsync(value);
            return res;
        }

        public void CloseDatabase() 
        { 
            if(DatabaseHandle != null)
            {

            }
        }

        public void Dispose()
        {
            CloseDatabase();    
            GC.SuppressFinalize(this);
        }
    }
}
