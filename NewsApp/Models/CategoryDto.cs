using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Priority { get; set; }
    }
}
