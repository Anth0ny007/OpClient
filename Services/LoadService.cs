using Microsoft.Data.SqlClient;

public class LoadService
{
    private readonly string _conn;
    public LoadService(string conn)
    {
        _conn = conn ?? throw new ArgumentException(nameof(conn));
    }

    // Insertar Opiniones
    public void InsertarDatos(List<OpinionCsv> opiniones)
    {
        using var conn = new SqlConnection(_conn);
        conn.Open();

        foreach (var o in opiniones)
        {
            int productId = Upsert(conn, "Productos", "Nombre", o.IdProducto, "IdProducto");
            int clientId = Upsert(conn, "Clientes", "Email", o.IdCliente, "IdCliente");
            int fuenteId = Upsert(conn, "Fuentes", "Nombre", o.Fuente, "IdFuente");
            int clasifId = Upsert(conn, "Clasificacion", "Nombre", o.Clasificacion, "IdClasificacion");

            var cmd = new SqlCommand(@"
                INSERT INTO Opiniones
                (ProductoId, ClienteId, FuenteId, ClasificacionId, Comentario, FechaOpinion, PuntajeSatisfaccion)
                VALUES (@p, @c, @f, @cl, @com, @fecha, @puntaje)", conn
            );

            cmd.Parameters.AddWithValue("@p", productId);
            cmd.Parameters.AddWithValue("@c", clientId);
            cmd.Parameters.AddWithValue("@f", fuenteId);
            cmd.Parameters.AddWithValue("@cl", clasifId);
            cmd.Parameters.AddWithValue("@com", o.Comentario ?? "");
            cmd.Parameters.AddWithValue("@fecha", o.Fecha);
            cmd.Parameters.AddWithValue("@puntaje", o.PuntajeSatisfaccion);
            cmd.ExecuteNonQuery();
        }
    }

    // Insertar Clientes
   public void InsertarClientes(List<ClienteCsv> clientes)
    {
        using var conn = new SqlConnection(_conn);
        conn.Open();

        foreach (var c in clientes)
        {
            Upsert(conn, "Clientes", "Email", c.Email, "IdCliente", extraFields: new Dictionary<string, object>
            {
                { "Nombre", c.Nombre ?? "" }
            });
        }
    }

    // Insertar Productos
    public void InsertarProductos(List<ProductoCsv> productos)
    {
        using var conn = new SqlConnection(_conn);
        conn.Open();

        foreach (var p in productos)
        {
            Upsert(conn, "Productos", "Nombre", p.Nombre, "IdProducto", extraFields: new Dictionary<string, object>
            {
                { "Categoria", p.Categoria ?? "" }
            });
        }
    }

    // Insertar Fuentes
    public void InsertarFuentes(List<FuenteCsv> fuentes)
    {
        using var conn = new SqlConnection(_conn);
        conn.Open();
    
        foreach (var f in fuentes)
        {
            Upsert(conn, "Fuentes", "Nombre", f.TipoFuente, "IdFuente");
        }
    }

    // Método genérico Upsert
    private int Upsert(SqlConnection conn, string table, string field, string value, string idField, Dictionary<string, object> extraFields = null)
    {
        string insertFields = field;
        string insertValues = "@val";

        if (extraFields != null && extraFields.Count > 0)
        {
            foreach (var kv in extraFields)
            {
                insertFields += $", {kv.Key}";
                insertValues += $", @{kv.Key}";
            }
        }

        var cmd = new SqlCommand($@"
            IF NOT EXISTS (SELECT 1 FROM {table} WHERE {field} = @val)
            BEGIN
                INSERT INTO {table} ({insertFields}) VALUES ({insertValues});
                SELECT SCOPE_IDENTITY();
            END
            ELSE
                SELECT {idField} FROM {table} WHERE {field} = @val;", conn);

        cmd.Parameters.AddWithValue("@val", value ?? "");

        if (extraFields != null)
        {
            foreach (var kv in extraFields)
            {
                cmd.Parameters.AddWithValue("@" + kv.Key, kv.Value ?? "");
            }
        }

        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}