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
        ViewBag.Categoria =  Juego.ObtenerCategorias();
        ViewBag.Dificultad = Juego.ObtenerDificultades();
        return View();
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        Juego.CargarPartida(username, dificultad, categoria);
        return RedirectToAction("Jugar");
    }
    public IActionResult Jugar()
    {
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
        if(ViewBag.Pregunta == null)
        {
            return View("Fin");
        }

        ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);
        return View();
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        ViewBag.EsCorrecta = Juego.VerificarRespuesta(idRespuesta);
        ViewBag.Correcta = Juego.ObtenerCorrecta();
        return View("Respuesta");
    }

}
