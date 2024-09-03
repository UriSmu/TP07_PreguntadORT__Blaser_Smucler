using System;
namespace TP07_PreguntadORT__Blaser_Smucler.Models;

public class Categoria
{
    public int IdCategoria {get; set;}
    public string Nombre {get; set;}
    public string Foto {get; set;}

    public Categoria() {}

    public Categoria(int id, string nombre, string foto)
    {
        IdCategoria = id;
        Nombre = nombre;
        Foto = foto;
    }
}