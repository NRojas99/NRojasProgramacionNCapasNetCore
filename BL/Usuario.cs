using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BL
{
    public class Usuario
    {
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    ObjectParameter IdUsuario = new ObjectParameter("IdUsuario", typeof(int));

                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}', '{usuario.Password}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.FechaNacimiento}', '{ usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.Estatus}', '{usuario.CURP}', '{usuario.Rol.IdRol}', '{usuario.Imagen}', '{usuario.IdUsuario}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Object = Convert.ToInt32(IdUsuario.Value);
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }
            return result;
        }
        //Actualizar EntityFramework
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var updateResult = context.Database.ExecuteSqlRaw($"UsuarioUpdate '{usuario.IdUsuario}','{usuario.UserName}', '{usuario.Password}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.FechaNacimiento}', '{ usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.Estatus}', '{usuario.CURP}', '{usuario.Rol.IdRol}', '{usuario.Imagen}'");
                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al Actualizar Usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }
            return result;
        }
        //Borrar EntityFramework
        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al Eliminar al Usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }
            return result;
        }
        //Traer Todo EntityFramework
        public static ML.Result GetAllEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'").ToList();

                    result.Objects = new List<Object>();

                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            ML.Usuario Usuario= new ML.Usuario();

                            Usuario.IdUsuario = obj.IdUsuario;
                            Usuario.UserName = obj.UserName;
                            Usuario.Password = obj.Password;
                            Usuario.Nombre = obj.Nombre;
                            Usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            Usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            Usuario.Email = obj.Email;
                            Usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd/MM/yyyy");
                            Usuario.Sexo = obj.Sexo;
                            Usuario.Estatus = obj.Estatus;
                            Usuario.Telefono = obj.Telefono;
                            Usuario.Celular = obj.Celular;
                            Usuario.CURP = obj.Curp;

                            Usuario.Rol = new ML.Rol();
                            Usuario.Rol.IdRol = Convert.ToByte(obj.IdRol);
                            Usuario.Rol.Nombre = obj.NombreRol;
                            Usuario.Imagen = obj.Imagen;

                            Usuario.Direccion = new ML.Direccion();
                            Usuario.Direccion.IdDireccion = Convert.ToInt16(obj.IdDireccion);
                            Usuario.Direccion.Calle = obj.Calle;
                            Usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            Usuario.Direccion.NumeroInterior = obj.NumeroInterior;

                            Usuario.Direccion.Colonia = new ML.Colonia();
                            Usuario.Direccion.Colonia.IdColonia = Convert.ToInt16(obj.IdColonia);
                            Usuario.Direccion.Colonia.Nombre = obj.NombreColonia;

                            Usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            Usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt16(obj.IdMunicipio);
                            Usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            Usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            Usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt16(obj.IdEstado);
                            Usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            Usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            Usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = Convert.ToInt16(obj.IdPais) ;
                            Usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            result.Objects.Add(Usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        //Traer por ID EntityFramework


        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (obj != null)
                    {
                        ML.Usuario Usuario = new ML.Usuario();

                        Usuario.IdUsuario = obj.IdUsuario;
                        Usuario.UserName = obj.UserName;
                        Usuario.Password = obj.Password;
                        Usuario.Nombre = obj.Nombre;
                        Usuario.ApellidoPaterno = obj.ApellidoPaterno;
                        Usuario.ApellidoMaterno = obj.ApellidoMaterno;
                        Usuario.Email = obj.Email;
                        Usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd/MM/yyyy");
                        Usuario.Sexo = obj.Sexo;
                        Usuario.Telefono = obj.Telefono;
                        Usuario.Celular = obj.Celular;
                        Usuario.Estatus = obj.Estatus;
                        Usuario.CURP = obj.Curp;

                        Usuario.Rol = new ML.Rol();
                        Usuario.Rol.IdRol = Convert.ToByte(obj.IdRol);
                        Usuario.Rol.Nombre = obj.NombreRol;
                        //Usuario.Imagen = obj.Imagen;

                        Usuario.Direccion = new ML.Direccion();
                        Usuario.Direccion.IdDireccion = Convert.ToInt16(obj.IdDireccion);
                        Usuario.Direccion.Calle = obj.Calle;
                        Usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                        Usuario.Direccion.NumeroInterior = obj.NumeroInterior;

                        Usuario.Direccion.Colonia = new ML.Colonia();
                        Usuario.Direccion.Colonia.IdColonia = Convert.ToInt16(obj.IdColonia);
                        Usuario.Direccion.Colonia.Nombre = obj.NombreColonia;

                        Usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        Usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt16(obj.IdMunicipio);
                        Usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                        Usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        Usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt16(obj.IdEstado);
                        Usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                        Usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        Usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = Convert.ToInt16(obj.IdPais);
                        Usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                        result.Correct = true;
                        result.Object = Usuario;
                        //result.Objects.Add(usuario);
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al consultar al usuario";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
