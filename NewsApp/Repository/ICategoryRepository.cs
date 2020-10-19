using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Repository
{
    public interface ICategoryRepository
    {
        public void Create(Category category);
        public void Delete(int id);
        public void Update(int id, Category category);
        public Category Get(int id);
        public IEnumerable<Category> GetAll();
        public int GetCategoryId(string category);
        public void UpdateIsActive(int bit,int id);
    }
}
