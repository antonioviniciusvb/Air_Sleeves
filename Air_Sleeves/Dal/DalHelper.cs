//using System;
//using System.Data;
//using System.Data.SQLite;
//using System.IO;
//using Air_Sleeves.Ferramentas;

//namespace Air_Sleeves.Dal
//{
//    public class DalHelper
//    {

//        private static SQLiteConnection sqliteConection;
//        private static string pathDatabase = $"{System.AppDomain.CurrentDomain.BaseDirectory}Database.sqlite";

//        public DalHelper()
//        {

//        }

//        public static SQLiteConnection DbConnection()
//        {
//                sqliteConection = new SQLiteConnection(
//                     $"Data Source={pathDatabase};Version=3");

//            sqliteConection.Open();

//            return sqliteConection;
//        }

//        public static void CriarBancoSQLite()
//        {
//            try
//            {
//                if (!(Util.fileExist($"{pathDatabase}")))
//                    SQLiteConnection.CreateFile($"{pathDatabase}");
//            }
//            catch 
//            {
//                throw;
//            }
//        }

//        public static void CriarTabelaSQlite()
//        {

//            try
//            {
//                    //using (var cmd = DbConnection().CreateCommand())
//                    //{
//                    //    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Clientes(Id int, Nome Varchar(50), email VarChar(80))";
//                    //    cmd.ExecuteNonQuery();
//                    //}

//                using (var cmd = DbConnection().CreateCommand())
//                {
//                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Produtos(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Nome TEXT NOT NULL, VALOR REAL NOT NULL)";
//                    cmd.ExecuteNonQuery();
//                }

//                using (var cmd = DbConnection().CreateCommand())
//                {
//                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS User(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Nome Text NOT NULL,Senha Text NOT NULL)";
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static DataTable GetItems(string table)
//        {
//            SQLiteDataAdapter da = null;
//            DataTable dt = new DataTable();

//            try
//            {
//                using (var cmd = DbConnection().CreateCommand())
//                {
//                    cmd.CommandText = $"SELECT * FROM {table}";
//                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
//                    da.Fill(dt);
//                    return dt;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static DataTable GetItem(string table, int id)
//        {
//            SQLiteDataAdapter da = null;
//            DataTable dt = new DataTable();

//            try
//            {
//                using(var cmd = DbConnection().CreateCommand())
//                {
//                    cmd.CommandText = $"SELECT * FROM {table} Where Id={id}";
//                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
//                    da.Fill(dt);
//                    return dt;
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public static void AddProdutos(Produto produto)
//        {
//            try
//            {
//                using(var cmd = DbConnection().CreateCommand())
//                {
//                    cmd.CommandText = "INSERT INTO Produtos(id,Nome,valor) values (@id, @nome, @valor)";
//                    cmd.Parameters.AddWithValue("@Id", produto.Id);
//                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
//                    cmd.Parameters.AddWithValue("@Email", produto.Valor);
//                    cmd.ExecuteNonQuery();                 
//                }
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//        public static void UpdateProdutos(Produto produto, int id)
//        {
//            try
//            {
//                using(var cmd = new SQLiteCommand(DbConnection()))
//                {
//                    cmd.CommandText = "UPDATE Produtos SET Nome=@Nome, Valor=@Valor WHERE Id=@Id";
//                    cmd.Parameters.AddWithValue(@"Nome", produto.Nome);
//                    cmd.Parameters.AddWithValue(@"Valor", produto.Valor);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public static void AddUser(User user)
//        {
//            try
//            {
//                using (var cmd = DbConnection().CreateCommand())
//                {
//                    cmd.CommandText = "INSERT INTO User (id,Nome,senha) values (@id, @nome, @senha)";
//                    cmd.Parameters.AddWithValue("@Nome", user.Nome);
//                    cmd.Parameters.AddWithValue("@Senha", user.Senha);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//        public static void UpdateUser(User user, int id)
//        {
//            try
//            {
//                using (var cmd = new SQLiteCommand(DbConnection()))
//                {
//                    cmd.CommandText = "UPDATE User SET Nome=@Nome, Senha=@Senha WHERE Id=@Id";
//                    cmd.Parameters.AddWithValue(@"Nome", user.Nome);
//                    cmd.Parameters.AddWithValue(@"Valor", user.Senha);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public static void Delete(string table, int id)
//        {
//            try
//            {
//                using(var cmd = new SQLiteCommand(DbConnection()))
//                {
//                    cmd.CommandText = $"DELETE FROM {table} WHERE Id=@id";
//                    cmd.Parameters.AddWithValue("@Id", id);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }
//    }
//}
