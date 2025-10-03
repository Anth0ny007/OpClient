using System.Text.RegularExpressions;
using System.Linq;

public class TransformService
{
    public List<OpinionCsv> LimpiarYClasificar(List<OpinionCsv> datos)
    {
        var clean = new List<OpinionCsv>();
        foreach (var d in datos)
        {
            if (string.IsNullOrWhiteSpace(d.Comentario)) continue;
            d.Comentario = Regex.Replace(d.Comentario, @"[^\w\sáéíóúÁÉÍÓÚ]", "");
            d.Clasificacion = Clasificar(d.Comentario);
            clean.Add(d);
        }
        return clean.DistinctBy(x => x.Comentario).ToList();
    }

    private string Clasificar(string texto)
    {
        string t = texto.ToLower();
        if (t.Contains("excelente") || t.Contains("bueno")) return "Positivo";
        if (t.Contains("malo") || t.Contains("terrible")) return "Negativo";
        return "Neutra";
    }
}