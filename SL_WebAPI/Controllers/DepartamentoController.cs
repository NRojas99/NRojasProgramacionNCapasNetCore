using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        // GET: api/<DepartamentoController>
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Departamento.GetAllEF();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<DepartamentoController>/5
        [HttpGet("{IdDepartamento}")]
        public IActionResult Get(byte IdDepartamento)
        {
            ML.Result result = BL.Departamento.GetByIdEF(IdDepartamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<DepartamentoController>
        [HttpPost]
        public IActionResult Add([FromBody] ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.AddEF(departamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/<DepartamentoController>/5
        [HttpPut("{IdDepartamento}")]
        public IActionResult Update(byte IdDepartamento, [FromBody] ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.UpdateEF(departamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<DepartamentoController>/5
        [HttpDelete("{IdDepartamento}")]
        public IActionResult Delete(byte IdDepartamento)
        {
            ML.Result result = BL.Departamento.DeleteEF(IdDepartamento);
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
