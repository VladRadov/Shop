using System.Data;
using Microsoft.Data.SqlClient;

namespace Shop.Services
{
    public class Connection
    {
        private SqlConnection _connection;

        private static Connection _instance;

        private Connection()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = @"Server = " + StringConnection.Server + "; Initial catalog = " + StringConnection.InitialCatalog + 
                                            "; User ID = " + StringConnection.UserID + "; Password = " + StringConnection.Password + 
                                            "; Connection Timeout = " + StringConnection.Timeout.ToString() + "; TrustServerCertificate = True";
        }

        public static Connection Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Connection();

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }
    }

    class StringConnection
    {
        public static string Server { get; set; }

        public static string InitialCatalog { get; set; }

        public static string UserID { get; set; }

        public static string Password { get; set; }

        public static readonly int Timeout = 3;
    }

    class BaseQuery
    {
        protected DataTable _tableResult { get; set; }

        protected SqlConnection _connecting { get; set; }

        public virtual void Execute()
        {
            _connecting = Connection.Instance.GetConnection();
        }

        public int CountItemsOfTableResult()
        {
            return _tableResult.Rows.Count;
        }

        public string GetSubitemsOfItem(int indexRow, string nameColumn)
        {
            var itemArray = _tableResult.Rows[indexRow].ItemArray;
            return itemArray[_tableResult.Columns[nameColumn].Ordinal].ToString();
        }
    }

    class SelectProdcuts : BaseQuery
    {
        private readonly string _query = @"select p.Name as [Название товара], p.Description as [Описание товара], pc.Name as [Категория товара]
                                           from Products p join ProductСategories pc on p.IDCategory=pc.ID";

        public override void Execute()
        {
            try
            {
                base.Execute();
                _tableResult = new DataTable();
                SqlCommand cmd = _connecting.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = _query;
                _connecting.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                    _tableResult.Columns.Add(reader.GetName(i).ToString());
                while (reader.Read())
                {
                    string[] array = new string[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                        array[i] = reader.GetValue(i).ToString();

                    DataRow row = _tableResult.NewRow();
                    row.ItemArray = array;
                    _tableResult.Rows.Add(row);
                }
            }
            catch (Exception exception)
            {
                //TODO: возможно логирование
            }
            finally
            {
                if (_connecting!= null && _connecting.State == ConnectionState.Open)
                    _connecting.Close();
            }
        }
    }
}
