using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SametApp.UI.Models;

namespace SametApp.UI.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
