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
    public IActionResult IngresarJugador(string nombreJugador, int idDificultad)
    {
        if (string.IsNullOrEmpty(nombreJugador))
        {
            ViewBag.Error = "Por favor ingrese su nombre.";
            return View("ConfigurarJuego");
        }

        // Obtener la dificultad seleccionada por su ID (asegúrate de tener la lista de dificultades cargada)
        ViewBag.IdDificultad = idDificultad;
        ViewBag.NombreJugador = nombreJugador;
        ViewBag.Categorias = Juego.ObtenerCategorias();
        
        return View("Categorias");
    }


    [HttpPost]
    public IActionResult SeleccionarCategoria(int idCategoria, int idDificultad, string nombreJugador)
    {

        ViewBag.IdDificultad = idDificultad;
        ViewBag.NombreJugador = nombreJugador;
        ViewBag.idCategoria = idCategoria;
        List<Dificultad> dificultades = Juego.ObtenerDificultades();
        ViewBag.NombreDificultad= dificultades[idDificultad-1].Nombre;
        List<Categoria> categorias = Juego.ObtenerCategorias();
        ViewBag.NombreCategoria= categorias[idCategoria-1].Nombre;


        return View("IniciarJuego");
    }

    [HttpGet]
    public IActionResult IniciarJuego()
    {
        var nombreJugador = TempData["nombreJugador"]?.ToString();
        var nombreCategoria = TempData["NombreCategoria"]?.ToString();
        var nombreDificultad = TempData["NombreDificultad"]?.ToString();

        if (nombreJugador == null || nombreCategoria == null || nombreDificultad == null)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.NombreJugador = nombreJugador;
        ViewBag.NombreCategoria = nombreCategoria;
        ViewBag.NombreDificultad = nombreDificultad;

        return View();
    }
}
