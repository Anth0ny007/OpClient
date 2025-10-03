using CsvHelper.Configuration;

public class FuenteMap : ClassMap<FuenteCsv>
{
    public FuenteMap()
    {
        Map(m => m.IdFuente).Name("IdFuente");
        Map(m => m.TipoFuente).Name("TipoFuente");
        Map(m => m.FechaCarga).Name("FechaCarga");
    }
}