using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProvedorData
    {
        private readonly string conexion;

        public ProvedorData(IConfiguration configuration)
        {
            // Cambia "CadenaSQL" por "CadenaSQLProvedor"
            conexion = configuration.GetConnectionString("CadenaSQLProvedor")!;
        }

        public async Task<List<PROVEDOR>> Lista()
        {
            List<PROVEDOR> lista = new List<PROVEDOR>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_SELECCIONA", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new PROVEDOR
                        {
                            ID = Convert.ToInt64(reader["ID"]),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            APELLIDO_PATERNO = reader["APELLIDO_PATERNO"].ToString(),
                            APELLIDO_MATERNO = reader["APELLIDO_MATERNO"].ToString(),
                            RFC = reader["RFC"].ToString(),
                            CORREO = reader["CORREO"].ToString(),
                            CREDITO = Convert.ToDecimal(reader["CREDITO"]),
                            NOMBRE_BANCO = reader["NOMBRE_BANCO"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<PROVEDOR> Obtener(long Id)
        {
            PROVEDOR objeto = new PROVEDOR();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_OBTENER_POR_ID", con);
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new PROVEDOR
                        {
                            ID = Convert.ToInt64(reader["ID"]),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            APELLIDO_PATERNO = reader["APELLIDO_PATERNO"].ToString(),
                            APELLIDO_MATERNO = reader["APELLIDO_MATERNO"].ToString(),
                            RFC = reader["RFC"].ToString(),
                            CORREO = reader["CORREO"].ToString(),
                            CREDITO = Convert.ToDecimal(reader["CREDITO"]),
                            NOMBRE_BANCO = reader["NOMBRE_BANCO"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(PROVEDOR objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_AGREGAR", con);
                cmd.Parameters.AddWithValue("@NOMBRE", objeto.NOMBRE);
                cmd.Parameters.AddWithValue("@APELLIDOPATERNO", objeto.APELLIDO_PATERNO);
                cmd.Parameters.AddWithValue("@APELLIDOMATERNO", objeto.APELLIDO_MATERNO);
                cmd.Parameters.AddWithValue("@RFC", objeto.RFC);
                cmd.Parameters.AddWithValue("@CORREO", objeto.CORREO);
                cmd.Parameters.AddWithValue("@CREDITO", objeto.CREDITO);
                cmd.Parameters.AddWithValue("@NOMBRE_BANCO", objeto.NOMBRE_BANCO);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(PROVEDOR objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_EDITAR", con);
                cmd.Parameters.AddWithValue("@ID", objeto.ID);
                cmd.Parameters.AddWithValue("@NOMBRE", objeto.NOMBRE);
                cmd.Parameters.AddWithValue("@APELLIDOPATERNO", objeto.APELLIDO_PATERNO);
                cmd.Parameters.AddWithValue("@APELLIDOMATERNO", objeto.APELLIDO_MATERNO);
                cmd.Parameters.AddWithValue("@RFC", objeto.RFC);
                cmd.Parameters.AddWithValue("@CORREO", objeto.CORREO);
                cmd.Parameters.AddWithValue("@CREDITO", objeto.CREDITO);
                cmd.Parameters.AddWithValue("@NOMBRE_BANCO", objeto.NOMBRE_BANCO);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(long id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_ELIMINAR", con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}