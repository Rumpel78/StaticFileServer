using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.IO;
using System;

namespace paprikon.StaticFileServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}
    