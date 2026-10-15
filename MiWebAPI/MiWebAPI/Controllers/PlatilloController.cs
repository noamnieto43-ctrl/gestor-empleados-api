using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/platillos")]
    [ApiController]
    public class PlatilloController : ControllerBase
    {
        private readonly PlatilloData _platilloData;

        public PlatilloController(PlatilloData platilloData)
        {
            _platilloData = platilloData;
        }

        // GET: api/platillos
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Platillo> lista = await _platilloData.ConsultarTodos();
            return Ok(lista);
        }

        // GET: api/platillos/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Platillo objeto = await _platilloData.ConsultarPorId(id);
            if (objeto == null)
            {
                return NotFound(new { mensaje = "Platillo no encontrado" });
            }
            return Ok(objeto);
        }

        // POST: api/platillos
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Platillo objeto)
        {
            bool respuesta = await _platilloData.Insertar(objeto);
            return Ok(new { isSuccess = respuesta });
        }

        // PUT: api/platillos/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Platillo objeto)
        {
            objeto.IdPlatillo = id;
            bool respuesta = await _platilloData.Actualizar(objeto);
            return Ok(new { isSuccess = respuesta });
        }

        // DELETE: api/platillos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _platilloData.Eliminar(id);
            return Ok(new { isSuccess = respuesta });
        }
    }
}