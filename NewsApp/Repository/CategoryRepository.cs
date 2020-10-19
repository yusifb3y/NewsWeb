using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        string connection = "Data source=WINDOWS-C8FJ4V7; Initial Catalog=NewsApp ; Integrated Security=true";


        public void Create(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "Insert Into Category (Name,Priority)" +
                    " values (@Name,@Priority)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Name", category.Name);
                command.Parameters.AddWithValue("@Priority", category.Priority);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "delete from Category where Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public Category Get(int id)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "select * from Category where Id = @Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader dataReader = command.ExecuteReader();
                Category category = null;
                if (dataReader.Read())
                {
                     category = new Category();
                    category.Id = Convert.ToInt32(dataReader["Id"]);
                    category.Name = dataReader["Name"].ToString();
                    category.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    category.Priority = dataReader["Priority"].ToString();
                }
                dataReader.Close();
                conn.Close();
                return category;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "select * from Category";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                List<Category> categories = new List<Category>();
                while (dataReader.Read())
                {
                    Category category = new Category();
                    category.Id = Convert.ToInt32(dataReader["Id"]);
                    category.Name = dataReader["Name"].ToString();
                    category.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    category.Priority = dataReader["Priority"].ToString();
                    categories.Add(category);
                }
                dataReader.Close();
                conn.Close();
                return categories;
            }
        }

        public int GetCategoryId(string category)
        {
           using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "select Id from Category where Name=@Name";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Name", category);
                SqlDataReader dataReader = command.ExecuteReader();
                int id = 0;
                if (dataReader.Read())
                {
                    id = Convert.ToInt32(dataReader["Id"]);
                }
                dataReader.Close();
                conn.Close();
                return id;
            }
        }

        public void Update(int id, Category category)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "Update Category set Name=@Name,Priority=@Priority where Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Name", category.Name);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Priority", category.Priority);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdateIsActive(int bit,int id)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "update Category set IsActive=@IsActive where Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@IsActive", bit);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
