using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class ExtractService
{
    private CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = ",",
        HasHeaderRecord = true,
        Encoding = System.Text.Encoding.UTF8
    };

    public List<ClienteCsv> LeerClientes(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<ClienteMap>();
        return csv.GetRecords<ClienteCsv>().ToList();
    }

    public List<ProductoCsv> LeerProductos(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<ProductoMap>();
        return csv.GetRecords<ProductoCsv>().ToList();
    }

    public List<FuenteCsv> LeerFuentes(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<FuenteMap>();
        return csv.GetRecords<FuenteCsv>().ToList();
    }

    public List<OpinionCsv> LeerOpiniones(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<OpinionMap>();
        return csv.GetRecords<OpinionCsv>().ToList();
    }
}