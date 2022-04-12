using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult ProductoGetByIdDepartamento()
        {
            ML.Producto producto = new ML.Producto();

            ML.Result result = BL.Producto.GetAllEF();

            producto.Departamento = new ML.Departamento();
            ML.Result resultDepartamento = BL.Departamento.GetAllEF();

            producto.Departamento.Area = new ML.Area();
            ML.Result resultArea = BL.Area.GetAll();

            if (result.Correct)
            {
                producto.Productos = result.Objects;
                producto.Departamento.Departamentos= resultDepartamento.Objects;
                producto.Departamento.Area.Areas = resultArea.Objects;

                return View(producto);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al obtener la informacion";
                return PartialView("ValidationModal");
            }
        }

        [HttpPost]
        public ActionResult ProductoGetByIdDepartamento(ML.Producto producto)
        { 

            ML.Result result = BL.Producto.ProductoGetByIdDepartamento(producto.Departamento.IdDepartamento);

            ML.Result resultDepartamento = BL.Departamento.DepartamentoGetByIdArea(producto.Departamento.Area.IdArea);

            ML.Result resultArea = BL.Area.GetAll();

            if (result.Correct)
            {
                producto.Productos = result.Objects;    
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                producto.Departamento.Area.Areas = resultArea.Objects;

                return View(producto);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al obtener la informacion";
                return PartialView("ValidationModal");
            }
        }

        public IActionResult Carrito (int? IdProducto)
        {
            ML.VentaProducto ventaProducto= new ML.VentaProducto();
            ventaProducto.VentasProductos = new List<object>();

            object carrito = HttpContext.Session.GetString("Carrito");

            if(IdProducto != null)
            {
                if (carrito == null)
                {
                    ML.Result resultProducto = BL.Producto.GetByIdEF(IdProducto.Value);//Boxing

                    ML.VentaProducto ventaProductoItem = new ML.VentaProducto();

                    ventaProductoItem.Producto = new ML.Producto();
                    ventaProductoItem.Producto.IdProducto = IdProducto.Value;
                    ventaProductoItem.Cantidad = 1;

                    string SessionProducto = Convert.ToString(ventaProducto.VentasProductos);


                    if (resultProducto.Correct)
                    {
                        ventaProductoItem.Producto = (ML.Producto)resultProducto.Object;//Unboxing
                        ventaProducto.VentasProductos.Add(ventaProductoItem);

                        ventaProductoItem.Producto.Productos = ventaProducto.VentasProductos;
                        HttpContext.Session.SetString(SessionProducto, JsonConvert.SerializeObject(ventaProducto.VentasProductos));

                        return View(ventaProductoItem);
                    }
                }
            }
            return View("ValidationModal");
        }


        public JsonResult GetDepartamento(byte IdArea)
        {
            var result = BL.Departamento.DepartamentoGetByIdArea(IdArea);
            return Json(result.Objects);
        }
    }
}
