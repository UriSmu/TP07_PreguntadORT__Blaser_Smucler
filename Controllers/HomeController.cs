using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07_PreguntadORT__Blaser_Smucler.Models;

namespace TP07_PreguntadORT__Blaser_Smucler.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
