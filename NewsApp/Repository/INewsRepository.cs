using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Repository
{
   public interface INewsRepository
    {
        public void Create(News news);
        public void Delete(int id);
        public void Update(int id, News news);
        public News Get(int id);
        public IEnumerable<News> GetAll();
        public int CreatePhoto(Photo photo);
        public IEnumerable<News> GetNewsByCategory(int id);
        public void UpdateIsActive(int bit,int id);
        public void UpdatePublishedDate(DateTime date,int id);
    }
}
