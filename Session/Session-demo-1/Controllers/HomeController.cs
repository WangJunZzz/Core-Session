using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Session_demo_1.Models;
using System.Text;
namespace Session_demo_1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
             HttpContext.Session.Set("keys",Encoding.UTF8.GetBytes("wangjunzzz"));
            byte[] value ;
            HttpContext.Session.TryGetValue("keys", out value);
            ViewBag.key=HttpContext.Session.Id;
            ViewBag.value=Encoding.UTF8.GetString(value);  
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
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
