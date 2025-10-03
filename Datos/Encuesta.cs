public class Encuesta
{
    public string CodigoEncuesta { get; set; }      // Identificador único de la encuesta
    public string CodigoCliente { get; set; }       // Relación con el cliente
    public string Producto { get; set; }            // Nombre o código del producto evaluado
    public string Comentario { get; set; }          // Texto libre de la opinión
    public int Puntuacion { get; set; }             // Calificación numérica (ej. 1–5)
    public DateTime FechaRespuesta { get; set; }    // Fecha en que se llenó la encuesta
}