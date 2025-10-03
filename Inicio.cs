using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connStr = config.GetConnectionString("SqlServer");
        Console.WriteLine("=== INICIO ETL ===");

        var extractor = new ExtractService();

        // 1. Cargar Clientes
        var clientes = extractor.LeerClientes("ArchivoCsv/clients.csv");
        var loader = new LoadService(connStr);
        loader.InsertarClientes(clientes);

        // 2. Cargar Productos
        var productos = extractor.LeerProductos("ArchivoCsv/products.csv");
        loader.InsertarProductos(productos);

        // 3. Cargar Fuentes
        var fuentes = extractor.LeerFuentes("ArchivoCsv/fuente_datos.csv");
        loader.InsertarFuentes(fuentes);

        // 4. Cargar Opiniones
        var opinionesRaw = extractor.LeerOpiniones("ArchivoCsv/surveys_part1.csv");
        var transformar = new TransformService();
        var opinionesLimpias = transformar.LimpiarYClasificar(opinionesRaw);
        loader.InsertarDatos(opinionesLimpias);

        Console.WriteLine("=== ETL COMPLETADO ===");
    }
}