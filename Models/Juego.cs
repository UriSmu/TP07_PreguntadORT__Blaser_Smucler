namespace TP07_PreguntadORT__Blaser_Smucler.Models;

static public class Juego
{
    static private string Username;
    static private int PuntajeActual;
    static private int CantidadPreguntasCorrectas;
    static private int ContadorNroPreguntaActual;
    static private Pregunta PreguntaActual;
    static private List<Pregunta> ListaPreguntas;
    static private List<Respuesta> ListaRespuestas;


    static private void InicializarJuego(string user)
    {
        Username = user;
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        ContadorNroPreguntaActual = 0;
        PreguntaActual = null;
        ListaPreguntas = null; 
        ListaRespuestas = null;
    }

    static public List<Categoria> ObtenerCategorias()
    {
        return(BD.ObtenerCategorias());
    }

    static public List<Dificultad> ObtenerDificultades()
    {
        return(BD.ObtenerDificultades());
    }

    static public void CargarPartida(string username, int dificultad, int categoria)
    {
        InicializarJuego(username);
        ListaPreguntas = BD.ObtenerPreguntas(dificultad, categoria);
    }

    static public Pregunta ObtenerProximaPregunta()
    {
        PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];
        return PreguntaActual;
    }

    static public List<Respuesta> ObtenerProximasRespuestas(int idPregunta)
    {
        ListaRespuestas = BD.ObtenerRespuestas(idPregunta);
        return ListaRespuestas;
    }

    static public bool VerificarRespuesta(int idRespuesta)
    {
        bool a = false;
        foreach (Respuesta answer in ListaRespuestas)
        {
            if(answer.IdRespuesta == idRespuesta)
            {
                if(answer.Correcta)
                {
                    a = true;

                    if(PreguntaActual.IdDificultad == 1)
                    {
                        PuntajeActual+=250;
                    }
                    else if(PreguntaActual.IdDificultad == 2)
                    {
                        PuntajeActual+=500;
                    }
                    else
                    {
                        PuntajeActual+=1000;
                    }

                    CantidadPreguntasCorrectas++;
                }
            }
        }

        ContadorNroPreguntaActual++;
        PreguntaActual = ListaPreguntas[ContadorNroPreguntaActual];

        return a;
    }
}