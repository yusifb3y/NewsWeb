using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class NewsDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string HtmlContent { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
