using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProductoData
    {
        private readonly string conexion;

        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<PRODUCTO_DETALLE>> Lista()
        {
            List<PRODUCTO_DETALLE> lista = new List<PRODUCTO_DETALLE>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.sp_Producto_detalle_listar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new PRODUCTO_DETALLE
                        {
                            Id = Convert.ToInt64(reader["Id"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            // Agregué la validación de nulos aquí también para evitar errores si un producto no tiene descripción
                            Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        // NUEVO MÉTODO: Buscar/Obtener por ID
        public async Task<PRODUCTO_DETALLE> Obtener(int id)
        {
            PRODUCTO_DETALLE objeto = null;

            using (var con = new SqlConnection(conexion))
            {
                // Nombre sugerido para tu Store Procedure. Cámbialo si en SQL se llama distinto.
                SqlCommand cmd = new SqlCommand("dbo.Producto_detalle_obtener", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            objeto = new PRODUCTO_DETALLE
                            {
                                Id = Convert.ToInt64(reader["Id"]),
                                NombreProducto = reader["NombreProducto"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                Total = Convert.ToDecimal(reader["Total"]),
                                Descripcion = reader["Descripcion"] == DBNull.Value ? null : reader["Descripcion"].ToString()
                            };
                        }
                    }
                }
                catch (Exception)
                {
                    objeto = null;
                }
            }

            return objeto;
        }

        public async Task<bool> Crear(PRODUCTO_DETALLE objeto)
        {
            bool respuesta = false;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_AGREGA", con);
                cmd.Parameters.AddWithValue("@NombreProducto", objeto.NombreProducto);
                cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@Total", objeto.Total);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public async Task<bool> Editar(PRODUCTO_DETALLE objeto)
        {
            bool respuesta = false;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_EDITAR", con);
                cmd.Parameters.AddWithValue("@Id", objeto.Id);
                cmd.Parameters.AddWithValue("@NombreProducto", objeto.NombreProducto);
                cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@Total", objeto.Total);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = false;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_eliminarProducto", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }
    }
}