
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProveedorController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Proveedor.GetAll();
            ML.Proveedor proveedor = new ML.Proveedor();
            if (result.Correct)
            {
                proveedor.Proveedores = result.Objects;
                return View(proveedor);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al obtener la informacion";
                return PartialView("ValidationModal");
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdProveedor)
        {
        
           ML.Proveedor proveedor= new ML.Proveedor();

            if (IdProveedor == null)
            {
                return View(proveedor);
            }
            else //UpDate 
            {
                ML.Result result = new ML.Result();

                result = BL.Proveedor.GetByIdEF(IdProveedor.Value);

                if (result.Correct)
                {

                    proveedor = ((ML.Proveedor)result.Object);

                    return View(proveedor);
                }
            }
            return View("ValidationModal");
        }

        [HttpPost]
        public ActionResult Form(ML.Proveedor proveedor )
        {
            //if (ModelState.IsValid)
            //{
            ML.Result result = new ML.Result();

            IFormFile imagen = Request.Form.Files["fuImagen"];
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen);
                proveedor.Logo = Convert.ToBase64String(ImagenByte);
            }

            if (proveedor.IdProveedor == 0)
            {
                result = BL.Proveedor.Add(proveedor);
                if (result.Correct)
                {
                    ViewBag.Message = "El proveedor se ha registrado correctamente";
                }
                else
                {
                    ViewBag.Message = "El proveedor  NO se pudo registrar correctamente ";
                }
            }
            else
            {
                result = BL.Proveedor.Update(proveedor);
                if (result.Correct)
                {
                    ViewBag.Message = "El prroveedor se ha actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "El proveedor no se ha actualizado correctamente";
                }
            }
            return PartialView("ValidationModal");
            //}
            //else
            //{
            //    return View(producto);
            //}
        }

        [HttpPost]
        public ActionResult CargaMasiva()
        {
            IFormFile file = Request.Form.Files["File"];
            try
            {
                ML.Result result = new ML.Result();
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(file.OpenReadStream()))
                {
                    string line;
                    ML.Result result2 = new ML.Result();
                    result2.Objects = new List<object>();

                    line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        string[] parts = line.Split('|');

                        ML.Proveedor proveedor = new ML.Proveedor();
                        parts[0] = (parts[0] == "") ? null : parts[0];
                        //proveedor.Nombre = (proveedor.Nombre == "") ? null: proveedor.Nombre;   
                        proveedor.Nombre = parts[0];
                        proveedor.Telefono = parts[1];
                        proveedor.Email = parts[2];
                        proveedor.DireccionWeb = parts[3];
                        result = BL.Proveedor.Add(proveedor);
                        if (!result.Correct)
                        {
                            result2.Objects.Add(
                                "No se inserto el Nombre " + proveedor.Nombre +
                                "No se inserto el Telefono " + proveedor.Telefono +
                                "No se inserto el Email " + proveedor.Email +
                                "No se inserto la DireccionWeb" + proveedor.DireccionWeb);
                        }
                    }
                    if (result2 != null)
                    {
                        TextWriter Text = new StreamWriter(@"C:\Users\digis\Documents\Nayeli Rojas Aguado\CargaMasiva\ErroresCargaMasiva.txt");
                        foreach (string error in result2.Objects)
                        {
                            Text.WriteLine(error);
                        }
                        Text.Close();
                    }
                    return PartialView("ValidationModal");
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return PartialView("ValidationModal");
        }

        public ActionResult DownloadDocument()
        {
            string filePath = @"C:\Users\digis\Documents\Nayeli Rojas Aguado\CargaMasiva\ErroresCargaMasiva.txt";
            string fileName = "ErroresCargaMasiva.txt";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);

        }

        //Metodo de conversión de la imagen
        public byte[] ConvertToBytes(IFormFile Imagen)
        {
            using var fileStream = Imagen.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            return bytes;
        }

        public ActionResult Delete(int IdProveedor)
        {
            ML.Result result = BL.Proveedor.Delete(IdProveedor);
            if (result.Correct)
            {
                ViewBag.Message = "El proveedor se a eliminado correctamente ";
            }
            else
            {
                ViewBag.Message = "El proveedor no se pudo eliminar correctamente";
            }
            return PartialView("ValidationModal");
        }

        
    }
}
