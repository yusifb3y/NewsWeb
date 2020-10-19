using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Repository;

namespace NewsApp.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private INewsRepository _newsRepository;
        private ICategoryRepository _categoryRepository;

        public HomeController(INewsRepository newsRepository, ICategoryRepository categoryRepository)
        {
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View(_newsRepository.GetAll());
        }
        [Route("category")]
        public IActionResult Category()
        {
            return View(_categoryRepository.GetAll());

        }
        [Route("newsCategory")]
        public IActionResult NewsCategory(int id)
        {
            return View(_newsRepository.GetNewsByCategory(id));
        }
    }
}
