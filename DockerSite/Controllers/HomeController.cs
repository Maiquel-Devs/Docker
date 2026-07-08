using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DockerSite.Models;
using Humanizer;

namespace DockerSite.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Message = "hello docker".Titleize();
        ViewBag.Date = DateTime.Now.Humanize();

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
