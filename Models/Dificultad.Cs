using System;
namespace TP07_PreguntadORT__Blaser_Smucler.Models;

public class Dificultad
{
    public int IdDificultad {get; set;}
    public string Nombre {get; set;}
    

    public Dificultad() {}

    public Dificultad(int id, string nombre)
    {
        IdDificultad = id;
        Nombre = nombre;
    }
}
