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

    public IActionResult Tutorial()
    {
        return View();
    }

    public IActionResult Creditos()
    {
        return View();
    }

    [HttpPost]
    public IActionResult IngresarJugador(string nombreJugador)
    {
        if (string.IsNullOrEmpty(nombreJugador))
        {
            ViewBag.Error = "Por favor ingrese su nombre.";
            return View("ConfigurarJuego");
        }

        TempData["nombreJugador"] = nombreJugador;

        return View("Categorias");
    }


    [HttpPost]
    public IActionResult SeleccionarCategoria(int idCategoria)
    {
        TempData["idCategoria"] = idCategoria;

        return View("Dificultad");
    }

    [HttpPost]
    public IActionResult SeleccionarDificultad(int idDificultad)
    {
        TempData["idDificultad"] = idDificultad;
        return RedirectToAction("IniciarJuego");
    }

    [HttpGet]
    public IActionResult IniciarJuego()
    {
        var nombreJugador = TempData["nombreJugador"]?.ToString();
        var idCategoria = TempData["idCategoria"];
        var idDificultad = TempData["idDificultad"];

        if (nombreJugador == null || idCategoria == null || idDificultad == null)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.NombreJugador = nombreJugador;
        ViewBag.IdCategoria = idCategoria;
        ViewBag.IdDificultad = idDificultad;

        return View();
    }
}
