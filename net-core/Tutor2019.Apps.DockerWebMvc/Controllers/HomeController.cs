//Author Maxim Kuzmin//makc//

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Tutor2019.Apps.DockerWebMvc.Models;
using Tutor2019.Apps.DockerWebMvc.Root;

namespace Tutor2019.Apps.DockerWebMvc.Controllers
{
    public class HomeController : Controller
    {
        private IRootRepository repository;
        private string message;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public HomeController(IRootRepository repo, IConfiguration config)
        {
            repository = repo;
            message = config["MESSAGE"] ?? "Essential Docker";
        }

        public IActionResult Index()
        {
            ViewBag.Message = message;
            return View(repository.Products);
        }
    }
}
