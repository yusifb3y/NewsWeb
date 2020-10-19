using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Repository
{
    public class UserRepository : IUserRepository
    {
        string connection = "Data source=WINDOWS-C8FJ4V7; Initial Catalog=NewsApp ; Integrated Security=true";


        public User Login(UserLoginModel user)
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "select * from Users where Email=@Email and Password=@Password";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                SqlDataReader dataReader = command.ExecuteReader();
                User loginedUser = null;
                if (dataReader.Read())
                {
                    loginedUser = new User();
                    loginedUser.Id = Convert.ToInt32(dataReader["Id"]);
                    loginedUser.Email = dataReader["Email"].ToString();
                    loginedUser.Name = dataReader["Name"].ToString();
                    loginedUser.Surname = dataReader["Surname"].ToString();
                    loginedUser.Mobil = dataReader["Mobil"].ToString();
                    loginedUser.Role = dataReader["Role"].ToString();
                }
                dataReader.Close();
                conn.Close();
                return loginedUser;
            }
        }
    }
}
