using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Infrastructure.Healthcheck.Concrete.MySql
{
    public class QueryResponseData : IDisposable
    {
        #region Private
        private string _bulkfile = null;
        private bool _bulkerLoad = false;
        private string _statement = null;
        private Stopwatch _fetchData = new Stopwatch();
        private string _message = null;
        private int _number = -1;
        private int _errorCode = -1;
        private DataTable _data = null;
        private DbParameterCollection _queryParametes = null;
        private DataTable _metaData = null;
        private DbException _exceptionData = null;
        private DbTransaction _transaction = null;
        #endregion
        #region Public
        public DbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }
        public bool IsTransactionalOperation { get => _transaction != null; }
        public string StatementWithoutSpecialChars
        {
            get
            {
                if (_statement == null)
                    return null;
                return
                    _statement = _statement.Replace("`", "");
            }
        }
        public string Statement
        {
            get
            {
                return _statement;
            }
            private set
            {
                _statement = value;
            }
        }
        public bool IsBulkLoad
        {
            get
            {
                return _bulkerLoad;
            }
            set
            {
                _bulkerLoad = value;
            }
        }
        public string BulkFile
        {
            get
            {
                return _bulkfile;
            }
            private set
            {
                _bulkfile = value;
            }
        }
        public DataTable Data
        {
            get
            {
                return _data;
            }
            set
            {
                InitStopFetching();
                _data = value;

            }
        }
        public DataTable MetaData
        {
            get
            {
                return _metaData;
            }
            set
            {
                InitStopFetching();
                _metaData = value;
            }
        }

        public bool HasSuccess
        {
            get
            {
                if (_errorCode != -1)
                    return false;

                return true;
            }
        }
        public bool HasErrors
        {
            get
            {
                if (_errorCode == -1)
                    return false;

                return true;
            }
        }
        public bool HasData
        {
            get
            {
                if (_data == null)
                    return false;

                if (_data.Rows.Count == 0)
                    return false;

                return true;
            }
        }
        public bool HasQueryParameters
        {
            get
            {
                if (_queryParametes == null)
                    return false;

                if (_queryParametes.Count == 0)
                    return false;

                return true;
            }
        }
        public bool HasNoDataOrErrors
        {
            get
            {
                return !HasData || HasErrors;
            }
        }
        public bool HasMetaData
        {
            get
            {
                if (_metaData == null)
                    return false;

                return true;
            }
        }
        public object LastInsertedId { get; set; } = Guid.Empty;
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                InitStopFetching();
                _number = value;
            }
        }
        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                InitStopFetching();
                _errorCode = value;
            }
        }
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {

                InitStopFetching();
                _message = value;
            }
        }
        public DbParameterCollection QueryParametes
        {
            get
            {
                return _queryParametes;
            }
            set
            {
                _queryParametes = value;
            }
        }
        public long FetchTime
        {
            get
            {
                return _fetchData.ElapsedMilliseconds;
            }
        }
        public DbException Exception
        {
            get
            {
                return _exceptionData;
            }
            set
            {
                _exceptionData = value;
            }
        }
        #endregion
        #region Ctor & Dtor
        public QueryResponseData(string query)
        {
            Statement = query;
            _fetchData.Start();
        }
        public QueryResponseData(bool bulkLoad, string filePath)
        {
            IsBulkLoad = bulkLoad;
            _bulkfile = filePath;
            _fetchData.Start();
        }
        ~QueryResponseData()
        {
            Dispose();
        }
        #endregion
        #region Methods
        public void SetTransaction(DbTransaction transaction)
        {
            _transaction = transaction;
        }
        private void InitStopFetching()
        {
            if (_fetchData.IsRunning)
                _fetchData.Stop();
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class QueryResponseData<T> : QueryResponseData
    {
        private List<T> _dataStorage = default;
        public List<T> DataStorage
        {
            get
            {
                return _dataStorage;
            }
            set
            {
                _dataStorage = value;
            }
        }
        public T FirstRow
        {
            get
            {

                return GetNestedObjByIndex(0);
            }
        }
        public T LastRow
        {
            get
            {

                return GetNestedObjByIndex(_dataStorage != null ? _dataStorage.Count - 1 : -1);
            }
        }
        public bool HasStorageData
        {
            get
            {
                return _dataStorage != null ?
                    _dataStorage.Count != 0 && !HasErrors ? true : false : false;
            }
        }
        #region Ctor
        public QueryResponseData(string query) : base(query)
        {

        }
        #endregion
        #region Methods
        private T GetNestedObjByIndex(int index)
        {
            if (_dataStorage != null)
            {
                if (_dataStorage.Count != 0)
                {
                    if (index >= _dataStorage.Count || index < 0)
                        return default;

                    return _dataStorage[index];
                }
            }
            return default;
        }
        #endregion
    }
}
