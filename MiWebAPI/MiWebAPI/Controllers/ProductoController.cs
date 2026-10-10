using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoData _productoData;

        public ProductoController(ProductoData productoData)
        {
            _productoData = productoData;
        }

        // 1. GET: api/Producto
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<PRODUCTO_DETALLE> Lista = await _productoData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        // 2. GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            PRODUCTO_DETALLE objeto = await _productoData.Obtener(id);

            if (objeto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Producto no encontrado" });
            }

            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        // 3. POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PRODUCTO_DETALLE objeto)
        {
            bool respuesta = await _productoData.Crear(objeto);
            if (respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = respuesta });
            }
        }

        // 4. PUT: api/Producto
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] PRODUCTO_DETALLE objeto)
        {
            bool respuesta = await _productoData.Editar(objeto);
            if (respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = respuesta });
            }
        }

        // 5. DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _productoData.Eliminar(id);
            if (respuesta)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = respuesta });
            }
        }
    }
}