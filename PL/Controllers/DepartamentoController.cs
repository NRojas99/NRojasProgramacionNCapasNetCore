using Microsoft.AspNetCore.Mvc;


namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Departamento.GetAllEF();
            ML.Departamento departamento = new ML.Departamento();
            if (result.Correct)
            {
                departamento.Departamentos = result.Objects;
                return View(departamento);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al obtener la informacion";
                return PartialView("ValidationModal");
            }
        }

        [HttpGet]
        public ActionResult Form(byte? IdDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();

            departamento.Area = new ML.Area();
            ML.Result resultArea = BL.Area.GetAll();

            if (IdDepartamento == null)
            {
                departamento.Area.Areas = resultArea.Objects;
                return View(departamento);
            }
            else //Update 
            {
                ML.Result result = new ML.Result();

                result = BL.Departamento.GetByIdEF(IdDepartamento.Value);

                if (result.Correct)
                {

                    departamento = ((ML.Departamento)result.Object);

                    departamento.Area.Areas = resultArea.Objects;

                    return View(departamento);
                }
            }
            return View("ValidationModal");

        }

        [HttpPost]
        public ActionResult Form(ML.Departamento departamento)
        {
          if (ModelState.IsValid)
          { 
            ML.Result result = new ML.Result();

            if (departamento.IdDepartamento == 0)
            {
                result = BL.Departamento.AddEF(departamento);
                if (result.Correct)
                {
                    ViewBag.Message = "El departamento se ha registrado correctamente";
                }
                else
                {
                    ViewBag.Message = "El departamento NO se pudo registrar correctamente ";
                }
            }
            else//Update
            {
                result = BL.Departamento.UpdateEF(departamento);
                if (result.Correct)
                {
                    ViewBag.Message = "El Departamento se ha actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "El Departamento no se ha actualizado correctamente";
                }
            }
            return PartialView("ValidationModal");
          }
          else
            {
                departamento.Area = new ML.Area();
                ML.Result resultArea = BL.Area.GetAll();
                departamento.Area.Areas = resultArea.Objects;

                return View(departamento);
            }
        }

        public ActionResult Delete(byte IdDepartamento)
        {
            ML.Result result = BL.Departamento.DeleteEF(IdDepartamento);
            if (result.Correct)
            {
                ViewBag.Message = "El Departamento se a eliminado correctamente ";
            }
            else
            {
                ViewBag.Message = "El Departamento no se pudo eliminar correctamente";
            }
            return PartialView("ValidationModal");
        }
    }
}
