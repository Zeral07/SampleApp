using Microsoft.AspNetCore.Mvc;
using SampleApp.Attributes;
using SampleApp.Models;
using System.Diagnostics;

namespace SampleApp.Controllers
{
    [ServiceFilter(typeof(AuthorizeValidateAttribute))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
