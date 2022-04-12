using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AreaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result =BL.Area.GetAll();
            ML.Area area = new ML.Area();
            if (result != null)
            {
                area.Areas = result.Objects;
                return View(area);
            }
            else
            {
                ViewBag.Mesage = "Error al traer las areas";
                return View(area);  
            }

        }

        [HttpGet]
        public ActionResult Form(byte? IdArea)
        {
            ML.Area area=new ML.Area(); 
            if(IdArea == null)
            {
                    return View(area);
            }
            else//Upate
            {
              ML.Result result=new ML.Result();
             result = BL.Area.GetByIdEF(IdArea.Value);
                if (result.Correct)
                {
                    area= ((ML.Area)result.Object);
                    return View(area);
                }
            }
            return View("ValidationModal");
        }
        [HttpPost]  
        public ActionResult Form(ML.Area area)
        {
            ML.Result resu=new ML.Result(); 
            if(area.IdArea == 0)
            {
                resu = BL.Area.Add(area);
                if(resu.Correct)
                {
                    ViewBag.Message = "El area se ha registrado correctamente";
                }
                else
                {
                    ViewBag.Message = "El area NO se ha registrado correctamente";
                }
            }
            else//Update
            {
                resu = BL.Area.Update(area);
                if (resu.Correct) 
                {
                    ViewBag.Message = "El Area se ha Actualizado Correctamente";
                }
                else
                {
                    ViewBag.Message = "El Area NO se ha Actualizado Correctamente";
                }
            }
            return View("ValidationModal");
        }
        public ActionResult Delete(byte IdArea)
        {
            ML.Result result = BL.Area.Delete(IdArea);
            if (result.Correct)
            {
                ViewBag.Message = "El Area se a eliminado correctamente ";
            }
            else
            {
                ViewBag.Message = "El Area no se pudo eliminar correctamente";
            }
            return PartialView("ValidationModal");
        }
    }
}
