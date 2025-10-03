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