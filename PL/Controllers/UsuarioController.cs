using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Usuario usuario= new ML.Usuario();

            usuario.Nombre= (usuario.Nombre == null)? "": usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "":usuario.ApellidoMaterno;  

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            //ML.Usuario usuario = new ML.Usuario();
            //ML.Usuario usuarios= new ML.Usuario();

            if (result.Correct)
            {
                usuario.Usuarios= result.Objects;

                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al obtener la informacion" + result.ErrorMessage;
                return PartialView("ValidationModal");
            }
        }

        [HttpPost]//Busqueda Abierta 
        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre; // Operador Tersario 
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            //ML.Usuario usuario = new ML.Usuario();
            //ML.Direccion direccion = new ML.Direccion();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al obtener la informacion" + result.ErrorMessage;
                return PartialView("ValidationModal");
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario Usuario = new ML.Usuario();

            Usuario.Direccion = new ML.Direccion();
            Usuario.Direccion.Colonia = new ML.Colonia();
            //ML.Result resultColonia= BL.Colonia.GetAll();
            Usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            Usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            Usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
            ML.Result resultPaises = BL.Pais.GetAllEF();
            Usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

            Usuario.Rol = new ML.Rol();
            ML.Result resultRol = BL.Rol.GetAll();



            if (IdUsuario == null)
            {

                ML.Result resultEstados = BL.Estado.EstadoGetByIdPais(Usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                ML.Result resultMunicipios = BL.Municipio.MunicipioGetByIdEstado(Usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                ML.Result resultColonias = BL.Colonia.ColoniaGetByIdMunicipio(Usuario.Direccion.Colonia.Municipio.IdMunicipio);
                ML.Result resultDirecciones = BL.Direccion.DireccionGetByIdColonia(Usuario.Direccion.Colonia.IdColonia);

                Usuario.Rol.Roles = resultRol.Objects;
                Usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                Usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                Usuario.Direccion.Colonia.Colonias = resultColonias.Objects;
                return View(Usuario);
            }
            else //Update 
            {

                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value); 
                if (result.Correct)
                {

                    //direccion.Colonia.Colonias = resultColonia.Objects;

                    Usuario.Direccion.Colonia = new ML.Colonia();
                    Usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    Usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    Usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                    Usuario = ((ML.Usuario)result.Object);

                    ML.Result resultEstados = BL.Estado.EstadoGetByIdPais(Usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    ML.Result resultMunicipios = BL.Municipio.MunicipioGetByIdEstado(Usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultColonias = BL.Colonia.ColoniaGetByIdMunicipio(Usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    ML.Result resultDirecciones = BL.Direccion.DireccionGetByIdColonia(Usuario.Direccion.Colonia.IdColonia);

                    Usuario.Rol.Roles = resultRol.Objects;
                    Usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                    Usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                    Usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                    Usuario.Direccion.Colonia.Colonias = resultColonias.Objects;

                    return View(Usuario);
                }
            }
            return View("ValidationModal");
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }



        [HttpPost]
        public ActionResult Form(ML.Usuario Usuario)
        {
            IFormFile imagen = Request.Form.Files["fuImagen"];
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen);
                Usuario.Imagen = Convert.ToBase64String(ImagenByte);
            }
            if (Usuario.IdUsuario == 0)
            {
                ML.Result result = BL.Usuario.AddEF(Usuario);

                if (result.Correct)
                {
                    Usuario.IdUsuario = ((int)result.Object);
                    ML.Result resultDireccion = BL.Direccion.AddEF(Usuario.Direccion);

                    if (resultDireccion.Correct)
                    {
                        ViewBag.Message = "El usuario se ha registrado correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "El usuario NO se pudo registrar correctamente ";
                    }
                }
                else
                {
                    ViewBag.Message = "Error al intentar ingresar al usuario" + result.ErrorMessage;
                }
            }
            else //Update
            { 
                ML.Result resultU = BL.Usuario.UpdateEF(Usuario);

                if (resultU.Correct)
                {
                    Usuario.Direccion.IdUsuario = Usuario.IdUsuario;
                   
                    ML.Result resultDireccion = BL.Direccion.UpdateEF( Usuario.Direccion);

                    if (resultDireccion.Correct)
                    {
                        ViewBag.Message = "La dirección del usuario se ha registrado correctamente";
                    }
                }
                else
                {
                    ViewBag.Message = "El Usuario no se ha actualizado correctamente";
                }
            }
            return PartialView("ValidationModal");
        }




        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.EstadoGetByIdPais(IdPais);
            return Json(result.Objects);
        }
        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.MunicipioGetByIdEstado(IdEstado);

            return Json(result.Objects);
        }
        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }


        public ActionResult UpdateStatus (int IdUsuario)
        {
            ML.Usuario usuario=new ML.Usuario();
            ML.Result result= BL.Usuario.GetByIdEF(IdUsuario);
            if(result.Correct)
            {
                usuario = ((ML.Usuario)result.Object);
                usuario.Estatus= (usuario.Estatus)? false: true;   // operador ternario

                ML.Result resultUpdate= BL.Usuario.UpdateEF(usuario);
                ViewBag.Message = "Se Actualizo Status";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al actualizar Status";
            }
            return PartialView("ValidationModal");
        }
        public ActionResult Delete(int IdUsuario)
        {
            ML.Direccion direccion = new ML.Direccion();

            ML.Result resultDireccion = BL.Usuario.GetByIdEF(IdUsuario);

            ML.Direccion direccionItem = (ML.Direccion)resultDireccion.Object;
            ML.Result resultDelete = BL.Direccion.DeleteEF(direccionItem.IdDireccion);

            if (resultDelete.Correct)
            {
                ML.Result resultDeleteUsuario = BL.Usuario.DeleteEF(IdUsuario);
                if (resultDeleteUsuario.Correct)
                {
                    Console.WriteLine("Alumno Eliminado correctamente");
                }
                else
                {
                    Console.WriteLine("Ocurrio un Error al eliminar al usuario");
                }
            }
            else
            {
                ViewBag.Message = "El Usuario no se pudo eliminar correctamente";
            }
            return PartialView("ValidationModal");
        }
    }
}
