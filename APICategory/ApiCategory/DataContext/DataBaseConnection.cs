using ApiCategory.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiCategory.DataContext
{
    public class DataBaseConnection
    {
        private MySqlConnection _conn;

        private string _cs;
        
        public DataBaseConnection()
        {
            try
            {
                // lê o arquivo 'ConfigDB.txt' com a connection string do banco
                var path = System.IO.Directory.GetCurrentDirectory();
                _cs = File.ReadAllText(path + @"\..\ConfigDB.txt");

                _conn = new MySqlConnection(_cs);
            }
            catch (Exception)
            {
                _cs = "";
                _conn = null;
            }
        }

        private bool OpenConn()
        {
            try
            {
                _conn.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool CloseConn()
        {
            try
            {
                if (_conn.State != System.Data.ConnectionState.Closed)
                    _conn.Close();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal List<Category> GetAllCategories()
        {
            try
            {
                OpenConn();

                // CASE: verifica se a categoria esta sendo atribuida em algum produto, se sim, retorna o campo 'InUse' como 'true' 
                var command = _conn.CreateCommand();
                command.CommandText = @"SELECT c.*,
                                               CASE 
                                                   WHEN (SELECT COUNT(p.ID) FROM PRODUCT p WHERE p.ID_CATEGORY = c.ID) > 0
                                                   THEN true 
                                                   ELSE false
                                               END AS InUse 
                            from CATEGORY c";
                MySqlDataReader dr = command.ExecuteReader();

                List<Category> _categories = new List<Category>();

                while (dr.Read())
                {
                    _categories.Add(new Category
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Name = dr["NAME"].ToString(),
                        Description = dr["DESCRIPTION"].ToString(),
                        InUse = Convert.ToBoolean(dr["InUse"]),
                        Date = Convert.ToDateTime(dr["DATE"])
                    });
                }

                CloseConn();

                return _categories;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal Category GetCategoryById(long id)
        {
            try
            {
                OpenConn();

                // verifica se a categoria esta sendo atribuida em algum produto, se sim, retorna o campo 'InUse' como 'true' 
                var command = _conn.CreateCommand();
                command.CommandText = @"SELECT c.*,
                                               CASE 
                                                   WHEN (SELECT COUNT(p.ID) FROM PRODUCT p WHERE p.ID_CATEGORY = c.ID) > 0
                                                   THEN true 
                                                   ELSE false
                                               END AS InUse 
                                       FROM CATEGORY c WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", id);

                MySqlDataReader dr = command.ExecuteReader();
                Category _category = new Category();

                while (dr.Read())
                {
                    _category = new Category
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Name = dr["NAME"].ToString(),
                        Description = dr["DESCRIPTION"].ToString(),
                        InUse = Convert.ToBoolean(dr["InUse"]),
                        Date = Convert.ToDateTime(dr["DATE"])
                    };
                }

                CloseConn();

                return _category;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal bool InsertCategory(Category newCat)
        {
            try
            {
                OpenConn();

                var command = _conn.CreateCommand();
                command.CommandText = "INSERT INTO CATEGORY (NAME, DESCRIPTION) VALUES (@NAME, @DESCRIPTION)";
                command.Parameters.AddWithValue("@NAME", newCat.Name);
                command.Parameters.AddWithValue("@DESCRIPTION", newCat.Description);

                command.ExecuteNonQuery();

                CloseConn();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool UpdateCategory(Category newCat)
        {
            try
            {
                OpenConn();

                var command = _conn.CreateCommand();
                command.CommandText = "UPDATE CATEGORY SET NAME = @NAME, DESCRIPTION = @DESCRIPTION WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", newCat.Id);
                command.Parameters.AddWithValue("@NAME", newCat.Name);
                command.Parameters.AddWithValue("@DESCRIPTION", newCat.Description);

                command.ExecuteNonQuery();

                CloseConn();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal bool DeleCategoryById(long id)
        {
            try
            {
                OpenConn();

                var command = _conn.CreateCommand();
                command.CommandText = "DELETE FROM CATEGORY WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", id);

                command.ExecuteNonQuery();

                CloseConn();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
