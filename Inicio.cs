using Microsoft.Extensions.Configuration;

public class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connStr = config.GetConnectionString("SqlServer");
        Console.WriteLine("=== INICIO ETL ===");

        var extractor = new ExtractService();
        var transformar = new TransformService();
        var carga = new LoadService(connStr);

        // 1. Cargar Clientes
        var clientes = extractor.LeerClientes("ArchivoCsv/clients.csv");
        carga.InsertarClientes(clientes);
        Console.WriteLine($"Clientes procesados: {clientes.Count}");

        // 2. Cargar Productos
        var productos = extractor.LeerProductos("ArchivoCsv/products.csv");
        carga.InsertarProductos(productos);
        Console.WriteLine($"Productos procesados: {productos.Count}");

        // 3. Cargar Fuentes
        var fuentes = extractor.LeerFuentes("ArchivoCsv/fuente_datos.csv");
        carga.InsertarFuentes(fuentes);
        Console.WriteLine($"Fuentes procesadas: {fuentes.Count}");

        // 4. Cargar Opiniones
        var opinionesRaw = extractor.LeerOpiniones("ArchivoCsv/surveys_part1.csv");
        var opinionesLimpias = transformar.LimpiarYClasificar(opinionesRaw);
        carga.InsertarDatos(opinionesLimpias);
        Console.WriteLine($"Opiniones cargadas: {opinionesLimpias.Count}");

        Console.WriteLine("=== ETL COMPLETADO ===");
    }
}