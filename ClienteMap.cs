using CsvHelper.Configuration;

public class ClienteMap : ClassMap<ClienteCsv>
{
    public ClienteMap()
    {
        Map(m => m.IdCliente).Name("IdCliente");
        Map(m => m.Nombre).Name("Nombre");
        Map(m => m.Email).Name("Email");
    }
}

public class ProductoMap : ClassMap<ProductoCsv>
{
    public ProductoMap()
    {
        Map(m => m.IdProducto).Name("IdProducto");
        Map(m => m.Nombre).Name("Nombre");
        Map(m => m.Categoria).Name("Categoría");
    }
}

public class FuenteMap : ClassMap<FuenteCsv>
{
    public FuenteMap()
    {
        Map(m => m.IdFuente).Name("IdFuente");
        Map(m => m.TipoFuente).Name("TipoFuente");
        Map(m => m.FechaCarga).Name("FechaCarga");
    }
}

public class OpinionMap : ClassMap<OpinionCsv>
{
    public OpinionMap()
    {
        Map(m => m.IdOpinion).Name("IdOpinion");
        Map(m => m.IdCliente).Name("IdCliente");
        Map(m => m.IdProducto).Name("IdProducto");
        Map(m => m.Fecha).Name("Fecha");
        Map(m => m.Comentario).Name("Comentario");
        Map(m => m.Clasificacion).Name("Clasificación");
        Map(m => m.PuntajeSatisfaccion).Name("PuntajeSatisfacción");
        Map(m => m.Fuente).Name("Fuente");
    }
}