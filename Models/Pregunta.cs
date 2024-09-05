using System;
namespace TP07_PreguntadORT__Blaser_Smucler.Models;

public class Pregunta
{
    public int IdPregunta {get; set;}
    public int IdCategoria {get; set;}
    public int IdDificultad {get; set;}
    public string Enunciado {get; set;}
    public string Foto {get; set;}

    public Pregunta() {}

    public Pregunta(int id, int cat, int dif, string enunciado, string foto)
    {
        IdPregunta = id;
        IdCategoria = cat;
        IdDificultad = dif;
        Enunciado = enunciado;
        Foto = foto;
    }
}