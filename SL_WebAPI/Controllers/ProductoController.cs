using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // GET: api/<ProductoController>
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<ProductoController>/5
        [HttpGet("{IdProducto}")]
        public IActionResult Get(int IdProducto)
        {
            ML.Result result = BL.Producto.GetByIdEF(IdProducto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ProductoController>
        [HttpPost]
        public IActionResult Add([FromBody] ML.Producto producto)
        {
            ML.Result result = BL.Producto.AddEF(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{IdProducto}")]
        public IActionResult Update(int IdProducto, [FromBody] ML.Producto producto)
            {
            ML.Result result = BL.Producto.UpdateEF(producto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{IdProducto}")]
        public IActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.DeleteEF(IdProducto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
