using System;
using System.Data.SqlClient;
using Dapper;

namespace TP07_PreguntadORT__Blaser_Smucler.Models;

static public class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=PreguntadORT; Trusted_Connection=true;";

    public static List<Categoria> ObtenerCategorias()
    {
        List<Categoria> Lista = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            Lista = db.Query<Categoria>("SELECT * FROM Categorias").ToList();
        }       
        return Lista;
    }

    public static List<Dificultad> ObtenerDificultades()
    {
        List<Dificultad> Lista = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            Lista = db.Query<Dificultad>("SELECT * FROM Dificultades").ToList();
        }       
        return Lista;
    }

    public static Pregunta ObtenerPreguntas(int dificultad, int categoria)
    {
        Pregunta preg = null;
        string sql = " ";
        if(dificultad != -1 && categoria != -1)
        {
            sql = "SELECT TOP(1) * FROM Preguntas WHERE IdDificultad = @pDificultad and IdCategoria = @pCategoria ORDER BY NEWID()";
        }
        else if(dificultad == -1 && categoria == -1)
        {
            sql = "SELECT TOP(1) * FROM Preguntas ORDER BY NEWID()";
        }
        else if(categoria == -1)
        {
            sql = "SELECT TOP(1) * FROM Preguntas WHERE IdDificultad = @pDificultad ORDER BY NEWID()";
        }
        else
        {
            sql = "SELECT TOP(1) * FROM Preguntas WHERE IdCategoria = @pCategoria ORDER BY NEWID()";
        }

        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            preg = db.QueryFirstOrDefault<Pregunta>(sql, new{pDificultad = dificultad, pCategoria = categoria});
        }       
        return preg;
    }

    public static List<Respuesta> ObtenerRespuestas(int idPregunta)
    {
        List<Respuesta> Lista = null;
        string sql = "SELECT * FROM Respuestas WHERE IdPregunta = @pId ORDER BY NEWID()";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            Lista = db.Query<Respuesta>(sql, new{pId = idPregunta}).ToList();
        }       
        return Lista;
    }


}