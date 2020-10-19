using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApp.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Error");
        }
        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
