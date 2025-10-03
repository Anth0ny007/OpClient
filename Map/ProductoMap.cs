using CsvHelper.Configuration;

public class ProductoMap : ClassMap<ProductoCsv>
{
    public ProductoMap()
    {
        Map(m => m.IdProducto).Name("IdProducto");
        Map(m => m.Nombre).Name("Nombre");
        Map(m => m.Categoria).Name("Categoría");
    }
}