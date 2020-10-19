using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileTarget { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
