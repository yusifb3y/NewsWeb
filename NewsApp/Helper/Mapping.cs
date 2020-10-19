using AutoMapper;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<NewsDto, News>();
            CreateMap<News, NewsDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
