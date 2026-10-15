
using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class PlatilloData
    {
        private readonly string _cadenaSQL;

        public PlatilloData(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("CadenaSQL") ?? string.Empty;
        }

        // 1. Consultar Todos (Activos)
        public async Task<List<Platillo>> ConsultarTodos()
        {
            List<Platillo> lista = new List<Platillo>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                await conexion.OpenAsync();
                using (var cmd = new SqlCommand("Platillo_ConsultarTodos", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var lector = await cmd.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            lista.Add(new Platillo
                            {
                                IdPlatillo = Convert.ToInt32(lector["IdPlatillo"]),
                                Codigo = lector["Codigo"].ToString() ?? string.Empty,
                                Nombre = lector["Nombre"].ToString() ?? string.Empty,
                                Descripcion = lector["Descripcion"] is DBNull ? null : lector["Descripcion"].ToString(),
                                Categoria = lector["Categoria"].ToString() ?? string.Empty,
                                Precio = Convert.ToDecimal(lector["Precio"]),
                                Activo = Convert.ToBoolean(lector["Activo"]),
                                FechaRegistro = Convert.ToDateTime(lector["FechaRegistro"]),
                                FechaActualizacion = lector["FechaActualizacion"] is DBNull ? null : Convert.ToDateTime(lector["FechaActualizacion"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 2. Consultar por ID
        public async Task<Platillo?> ConsultarPorId(int idPlatillo)
        {
            Platillo? objeto = null;

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                await conexion.OpenAsync();
                using (var cmd = new SqlCommand("Platillo_ConsultarPorId", conexion))
                {
                    cmd.Parameters.AddWithValue("@IdPlatillo", idPlatillo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var lector = await cmd.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            objeto = new Platillo
                            {
                                IdPlatillo = Convert.ToInt32(lector["IdPlatillo"]),
                                Codigo = lector["Codigo"].ToString() ?? string.Empty,
                                Nombre = lector["Nombre"].ToString() ?? string.Empty,
                                Descripcion = lector["Descripcion"] is DBNull ? null : lector["Descripcion"].ToString(),
                                Categoria = lector["Categoria"].ToString() ?? string.Empty,
                                Precio = Convert.ToDecimal(lector["Precio"]),
                                Activo = Convert.ToBoolean(lector["Activo"]),
                                FechaRegistro = Convert.ToDateTime(lector["FechaRegistro"]),
                                FechaActualizacion = lector["FechaActualizacion"] is DBNull ? null : Convert.ToDateTime(lector["FechaActualizacion"])
                            };
                        }
                    }
                }
            }
            return objeto;
        }

        // 3. Insertar
        public async Task<bool> Insertar(Platillo objeto)
        {
            bool respuesta = true;

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                await conexion.OpenAsync();
                using (var cmd = new SqlCommand("Platillo_Insertar", conexion))
                {
                    cmd.Parameters.AddWithValue("@Codigo", objeto.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Categoria", objeto.Categoria);
                    cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas = await cmd.ExecuteNonQueryAsync();
                    if (filas <= 0) respuesta = false;
                }
            }
            return respuesta;
        }

        // 4. Actualizar
        public async Task<bool> Actualizar(Platillo objeto)
        {
            bool respuesta = true;

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                await conexion.OpenAsync();
                using (var cmd = new SqlCommand("Platillo_Actualizar", conexion))
                {
                    cmd.Parameters.AddWithValue("@IdPlatillo", objeto.IdPlatillo);
                    cmd.Parameters.AddWithValue("@Codigo", objeto.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Categoria", objeto.Categoria);
                    cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas = await cmd.ExecuteNonQueryAsync();
                    if (filas <= 0) respuesta = false;
                }
            }
            return respuesta;
        }

        // 5. Eliminar (Baja Lógica)
        public async Task<bool> Eliminar(int idPlatillo)
        {
            bool respuesta = true;

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                await conexion.OpenAsync();
                using (var cmd = new SqlCommand("Platillo_Eliminar", conexion))
                {
                    cmd.Parameters.AddWithValue("@IdPlatillo", idPlatillo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int filas = await cmd.ExecuteNonQueryAsync();
                    if (filas <= 0) respuesta = false;
                }
            }
            return respuesta;
        }
    }
}