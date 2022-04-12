using Microsoft.AspNetCore.Mvc;
using System.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class ProductoCargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _environment;

        public ProductoCargaMasivaController(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }
        public IActionResult CargaMasiva()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CargaMasiva(ML.Proveedor proveedor)
        {
            IFormFile file = Request.Form.Files["File"];
            string filename=Path.GetFileName(file.FileName);

            if (HttpContext.Session.GetString("PathArchivo")==null)
            { 
             if(file.Length> 0)
                {
                    var filePath = _configuration["PathFolder:value"];
                    var extensionArchivo= Path.GetExtension(file.FileName).ToLower();
                    var extensionModulo = _configuration["TipoExcel:value"];

                    if(extensionArchivo == extensionModulo)
                    {
                        string path = Path.Combine(_environment.ContentRootPath,filePath, Path.GetFileNameWithoutExtension(filename)) +'-'+ DateTime.Now.ToString("yyyyMMddHHmmss")+".xlsx";
                        if(!System.IO.File.Exists (path))
                        {
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            string ConnectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            ML.Result result = BL.Producto.GetAllExcel(ConnectionString);
                        }
                    }

                }
            }
            return View();
        }
    }
}
