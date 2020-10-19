using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Repository
{
    public class NewsRepository : INewsRepository
    {
        string connection = "Data source=WINDOWS-C8FJ4V7; Initial Catalog=NewsApp ; Integrated Security=true";

        public void Create(News news)

        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "Insert Into News (CategoryId,Title,Subtitle,PublishedDate,PhotoId,HtmlContent)" +
                    " values (@CategoryId,@Title,@Subtitle,@PublishedDate,@PhotoId,@HtmlContent)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@CategoryId", news.CategoryId);
                command.Parameters.AddWithValue("@Title", news.Title);
                command.Parameters.AddWithValue("@Subtitle", news.Subtitle);
                command.Parameters.AddWithValue("@PublishedDate", news.PublishedDate);
                command.Parameters.AddWithValue("@PhotoId", news.PhotoId);
                command.Parameters.AddWithValue("@HtmlContent", news.HtmlContent);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public int CreatePhoto(Photo photo)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "Insert Into Photos (FileName,FileTarget)" +
                    " values (@FileName,@FileTarget)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@FileName", photo.FileName);
                command.Parameters.AddWithValue("@FileTarget", photo.FileTarget);
                command.ExecuteNonQuery();
                query = "select max(Id) as Id from Photos ";
                command = new SqlCommand(query, conn);
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

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "delete from News where Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public News Get(int id)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "select * from News n inner join Category c on c.Id=n.CategoryId " +
                    " inner join Photos p on p.Id = n.PhotoId where n.Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader dataReader = command.ExecuteReader();
                News news1 = null;
                if (dataReader.Read())
                {
                     news1 = new News();
                    news1.Id = Convert.ToInt32(dataReader["Id"]);
                    news1.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    news1.Category = dataReader["Name"].ToString();
                    news1.PhotoId = Convert.ToInt32(dataReader["PhotoId"]);
                    news1.Photo.FileName = dataReader["FileName"].ToString();
                    news1.Photo.FileTarget = dataReader["FileTarget"].ToString();
                    news1.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                    news1.PublishedDate = Convert.ToDateTime(dataReader["PublishedDate"]);
                    news1.Title = dataReader["Title"].ToString();
                    news1.Subtitle = dataReader["Subtitle"].ToString();
                    news1.HtmlContent = dataReader["HtmlContent"].ToString();
                    news1.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    news1.IsDelete = Convert.ToBoolean(dataReader["IsDelete"]);
                }
                dataReader.Close();
                conn.Close();
                return news1;
            }
        }

        public IEnumerable<News> GetAll()
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "select * from News n inner join Category c on c.Id=n.CategoryId " +
                    " inner join Photos p on p.Id = n.PhotoId";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                List<News> news = new List<News>();
                while (dataReader.Read())
                {
                    News news1 = new News();
                    news1.Id = Convert.ToInt32(dataReader["Id"]);
                    news1.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    news1.Category = dataReader["Name"].ToString();
                    news1.PhotoId = Convert.ToInt32(dataReader["PhotoId"]);
                    news1.Photo.FileName = dataReader["FileName"].ToString();
                    news1.Photo.FileTarget = dataReader["FileTarget"].ToString();
                    news1.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                    news1.PublishedDate = Convert.ToDateTime(dataReader["PublishedDate"]);
                    news1.Title = dataReader["Title"].ToString();
                    news1.Subtitle = dataReader["Subtitle"].ToString();
                    news1.HtmlContent = dataReader["HtmlContent"].ToString();
                    news1.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    news1.IsDelete = Convert.ToBoolean(dataReader["IsDelete"]);
                    news.Add(news1);
                }
                dataReader.Close();
                conn.Close();
                return news;
            }
        }

        public IEnumerable<News> GetNewsByCategory(int id)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "select * from News n inner join Category c on c.Id=n.CategoryId " +
                    " inner join Photos p on p.Id = n.PhotoId where c.Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader dataReader = command.ExecuteReader();
                List<News> news = new List<News>();
                while (dataReader.Read())
                {
                    News news1 = new News();
                    news1.Id = Convert.ToInt32(dataReader["Id"]);
                    news1.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    news1.Category = dataReader["Name"].ToString();
                    news1.PhotoId = Convert.ToInt32(dataReader["PhotoId"]);
                    news1.Photo.FileName = dataReader["FileName"].ToString();
                    news1.Photo.FileTarget = dataReader["FileTarget"].ToString();
                    news1.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                    news1.PublishedDate = Convert.ToDateTime(dataReader["PublishedDate"]);
                    news1.Title = dataReader["Title"].ToString();
                    news1.Subtitle = dataReader["Subtitle"].ToString();
                    news1.HtmlContent = dataReader["HtmlContent"].ToString();
                    news1.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    news1.IsDelete = Convert.ToBoolean(dataReader["IsDelete"]);
                    news.Add(news1);
                }
                dataReader.Close();
                conn.Close();
                return news;
            }
        }

        public void Update(int id, News news)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "Update News set CategoryId=@CategoryId,Title=@Title,Subtitle=@Subtitle,PublishedDate=@PublishedDate" +
                    ",PhotoId=@PhotoId,HtmlContent=@HtmlContent where Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@CategoryId", news.CategoryId);
                command.Parameters.AddWithValue("@Title", news.Title);
                command.Parameters.AddWithValue("@Subtitle", news.Subtitle);
                command.Parameters.AddWithValue("@PublishedDate", news.PublishedDate);
                command.Parameters.AddWithValue("@PhotoId", news.PhotoId);
                command.Parameters.AddWithValue("@HtmlContent", news.HtmlContent);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void UpdateIsActive(int bit,int id)
        {
           using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "update News set IsActive=@IsActive where Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@IsActive", bit);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void UpdatePublishedDate(DateTime date,int id)
        {
            using(SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "update News set PublishedDate=@PublishedDate where Id=@Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@PublishedDate", date);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
