using System;
namespace TP07_PreguntadORT__Blaser_Smucler.Models;

public class Respuesta
{
    public int IdRespuesta {get; set;}
    public int IdPregunta {get; set;}
    public int Opcion {get; set;}
    public string Contenido {get; set;}
    public bool Correcta {get; set;}
    public string Foto {get; set;}


    public Respuesta() {}

    public Respuesta(int id, int Id, int opcion, string contenido, bool correcta, string foto)
    {
        IdRespuesta = id;
        IdPregunta = Id;
        Opcion = opcion;
        Contenido = contenido;
        Correcta = correcta;
        Foto = foto;
    }
}