using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.Repository;

namespace NewsApp.Controllers
{
    [Authorize (Roles ="Admin")]
    public class AdminController : Controller
    {
        private INewsRepository _newsRepository;
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public AdminController(INewsRepository newsRepository,ICategoryRepository categoryRepository,IMapper mapper)
        {
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("newNews")]
        public IActionResult NewNews()
        {
            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult CreateNews(NewsDto newsDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                News news = _mapper.Map<News>(newsDto);
                int categoryId = _categoryRepository.GetCategoryId(news.Category);
                int photoId = 0;
                if (categoryId <= 0)
                {
                    throw new Exception();
                }
                if (file != null)
                {
                    string dirPath = @"C:\Users\User\Documents\NewsApp3\NewsApp\NewsApp\wwwroot\images\" + file.FileName;
                    dirPath = dirPath.Replace("/", @"\");
                    using (FileStream fs = System.IO.File.Create(dirPath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    Photo photo = new Photo();
                    photo.FileName = file.FileName;
                    photo.FileTarget = dirPath;
                    photoId = _newsRepository.CreatePhoto(photo);
                }
                news.PhotoId = photoId;
                news.CategoryId = categoryId;
                _newsRepository.Create(news);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Input fields must be filled";
                return RedirectToAction("NewNews");
            }
        }
        [Route("newCategory")]
        public IActionResult NewCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateNews(int id , NewsDto newsDto)
        {
            if (ModelState.IsValid)
            {
                News news = _newsRepository.Get(id);
                news.Title = newsDto.Title;
                news.Subtitle = newsDto.Subtitle;
                news.HtmlContent = newsDto.HtmlContent;
                news.PublishedDate = newsDto.PublishedDate;
                news.CategoryId =_categoryRepository.GetCategoryId(news.Category);
                _newsRepository.Update(id, news);
                return RedirectToAction("AllNews");
            }
            else
            {
                TempData["error"] = "Input fields must be filled";
                return RedirectToAction("UpdateNewsModal");
            }
        }
        [HttpGet]
        [Route("UpdateNewsModal")]
        public IActionResult UpdateNewsModal(int id)
        {
            News news = _newsRepository.Get(id);
            if (news != null)
                return View(news);
            else
                throw new Exception();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(categoryDto);
                _categoryRepository.Create(category);
                return RedirectToAction("Category", "Home");
            }
            else
            {
                TempData["error"] = "Input fields must be filled";
                return RedirectToAction("NewCategory");
            }
        }
        [HttpGet]
        [Route("deleteNews")]
        public IActionResult DeleteNews(int id)
        {
            _newsRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult UpdateCategory(int id , CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(categoryDto);
                _categoryRepository.Update(id, category);
                return RedirectToAction("AllCategories");
            }
            else
            {
                TempData["error"] = "Input fields must be filled";
                return RedirectToAction("UpdateCategoryModal",id);
            }
        }
        [HttpGet]
        [Route("updateCategoryModal")]
        public IActionResult UpdateCategoryModal(int id)
        {
            Category category = _categoryRepository.Get(id);
            if (category != null)
                return View(category);
            else
                throw new Exception();
        }
        [HttpGet]
        [Route("trigNewsIsActive")]
        public IActionResult TrigNewsIsActive(int bit,int id)
        {
            if (bit == 0 || bit == 1)
            {
                _newsRepository.UpdateIsActive(bit,id);
                return RedirectToAction("AllNews");
            }
            throw new Exception();
        }
        [HttpGet]
        [Route("trigCategoryIsActive")]
        public IActionResult TrigCategoryIsActive(int bit, int id)
        {
            if (bit == 0 || bit == 1)
            {
                _categoryRepository.UpdateIsActive(bit, id);
                return RedirectToAction("AllCategories");
            }
            throw new Exception();
        }
        [Route("allNews")]
        public IActionResult AllNews()
        {
            return View(_newsRepository.GetAll());
        }
        [Route("allCategories")]
        public IActionResult AllCategories()
        {
            return View(_categoryRepository.GetAll());
        }
        [HttpPost]
        public IActionResult UpdatePublishedDate(DateTime dateTime , int id)
        {
            if(dateTime != null && id >=1)
            {
                _newsRepository.UpdatePublishedDate(dateTime, id);
                return RedirectToAction("AllNews");
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
