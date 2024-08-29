using System;
using System.Data.SqlClient;
using Dapper;

namespace TP6__JJOO__Blaser_Smucler.Models;

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

    public static List<Dificultad>
}