using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IText7.HtmlToPdf.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
