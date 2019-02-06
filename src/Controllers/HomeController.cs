using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Encodings.Web;
using System.IO;
using System;

namespace paprikon.StaticFileServer.Controllers
{
    public class HomeController : Controller
    {
        private string indexFile;

        public HomeController(IConfiguration config)
        {
            this.indexFile = config["ASPNETCORE_INDEX"] ?? "index.html";
        }

        public IActionResult Index()
        {
            return File($"~/{indexFile}", "text/html");
        }
    }
}
    