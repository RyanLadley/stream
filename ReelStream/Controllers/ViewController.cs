using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReelStream.Controllers
{
    public class ViewController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}

