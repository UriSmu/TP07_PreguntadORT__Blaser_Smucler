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
    public IActionResult ConfigurarJuego()
    {
<<<<<<< HEAD
        ViewBag.Categoria = BD.ObtenerCategorias();
        ViewBag.Dificultad = BD.ObtenerDificultades();
=======
        //ViewBag.Categoria = BD.ObtenerCategorias();
>>>>>>> d9f1959da1e3a12891e474e40909ee7ae7d4f9bf
        return View();
    }
}
