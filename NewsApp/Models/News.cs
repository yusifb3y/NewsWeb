using Microsoft.AspNetCore.Http;
using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NewsApp
{
    public class News
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Category { get; set; }
        public string HtmlContent { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PhotoId { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public Photo Photo { get; set; } = new Photo();
    }
}
