using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Direccion
    {

        public static ML.Result AddEF(ML.Direccion direccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DirecciónAdd {direccion.Calle}, {direccion.NumeroExterior},{direccion.NumeroInterior},{direccion.Colonia.IdColonia}, {direccion.IdUsuario}");

                    if (query > 0)
                    {
                        result.Correct = true;
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
        public static ML.Result DeleteEF(int direccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DireccionDelete {direccion}");
                    if (query >= 1)
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

        //public static ML.Result GetAllEF()
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
        //        {
        //            var direcciones = context.Direccions.FromSqlRaw("DireccionGetall").ToList();

        //            result.Objects = new List<Object>();

        //            if (direcciones != null)
        //            {
        //                foreach (var obj in direcciones)
        //                {
        //                    ML.Direccion direccion = new ML.Direccion();

        //                    direccion.IdDireccion = obj.IdDireccion;
        //                    direccion.Calle = obj.Calle;
        //                    direccion.NumeroExterior = obj.NumeroExterior;
        //                    direccion.NumeroInterior = obj.NumeroInterior;

        //                    direccion.Colonia = new ML.Colonia();
        //                    direccion.Colonia.IdColonia = obj.IdColonia.Value;
        //                    direccion.Colonia.Nombre = obj.NombreColonia;

        //                    direccion.Colonia.Municipio = new ML.Municipio();
        //                    direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;
        //                    direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

        //                    direccion.Colonia.Municipio.Estado = new ML.Estado();
        //                    direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado.Value; ;
        //                    direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

        //                    direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
        //                    direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais.Value; ;
        //                    direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

        //                    direccion.Usuario = new ML.Usuario();
        //                    direccion.Usuario.IdUsuario = obj.IdUsuario.Value;
        //                    direccion.Usuario.UserName = obj.UserName;
        //                    direccion.Usuario.Password = obj.Password;
        //                    direccion.Usuario.Nombre = obj.NombreUsuario;
        //                    direccion.Usuario.ApellidoPaterno = obj.ApellidoPaterno;
        //                    direccion.Usuario.ApellidoMaterno = obj.ApellidoMaterno;
        //                    direccion.Usuario.Email = obj.Email;
        //                    direccion.Usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd/MM/yyyy");
        //                    direccion.Usuario.Sexo = obj.Sexo;
        //                    direccion.Usuario.Telefono = obj.Telefono;
        //                    direccion.Usuario.Celular = obj.Celular;
        //                    direccion.Usuario.Estatus = obj.Estatus;
        //                    direccion.Usuario.CURP = obj.Curp;
        //                    direccion.Usuario.Imagen = obj.Imagen;

        //                    direccion.Usuario.Rol = new ML.Rol();
        //                    direccion.Usuario.Rol.IdRol = obj.IdRol.Value;
        //                    direccion.Usuario.Rol.Nombre = obj.NombreRol;

        //                    result.Objects.Add(direccion);
        //                }
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontraron registros";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}
        //public static ML.Result GetByIdUsuario(int IdUsuario)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
        //        {
        //            var query = context.Direccions.FromSqlRaw($"DireccionGetByIdUsuario {IdUsuario}").AsEnumerable().FirstOrDefault();

        //            result.Objects = new List<object>();

        //            if (query != null)
        //            {
        //                ML.Direccion direccion = new ML.Direccion();

        //                direccion.IdDireccion = query.IdDireccion;
        //                direccion.Calle = query.Calle;
        //                direccion.NumeroExterior = query.NumeroExterior;
        //                direccion.NumeroInterior = query.NumeroInterior;

        //                direccion.Colonia = new ML.Colonia();
        //                direccion.Colonia.IdColonia = query.IdColonia.Value;
        //                direccion.Colonia.Nombre = query.NombreColonia;
        //                direccion.Colonia.Municipio = new ML.Municipio();
        //                direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio.Value;
        //                direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;
        //                direccion.Colonia.Municipio.Estado = new ML.Estado();
        //                direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado.Value;
        //                direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;
        //                direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
        //                direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais.Value;
        //                direccion.Colonia.Municipio.Estado.Pais.Nombre = query.NombrePais;

        //                direccion.Usuario = new ML.Usuario();
        //                direccion.Usuario.IdUsuario = query.IdUsuario.Value;
        //                direccion.Usuario.UserName = query.UserName;
        //                direccion.Usuario.Password = query.Password;
        //                direccion.Usuario.Nombre = query.NombreUsuario;
        //                direccion.Usuario.ApellidoPaterno = query.ApellidoPaterno;
        //                direccion.Usuario.ApellidoMaterno = query.ApellidoMaterno;
        //                direccion.Usuario.Email = query.Email;
        //                direccion.Usuario.FechaNacimiento = query.FechaNacimiento.ToString("dd/MM/yyyy");
        //                direccion.Usuario.Sexo = query.Sexo;
        //                direccion.Usuario.Telefono = query.Telefono;
        //                direccion.Usuario.Celular = query.Celular;
        //                direccion.Usuario.Estatus = query.Estatus;
        //                direccion.Usuario.CURP = query.Curp;

        //                direccion.Usuario.Rol = new ML.Rol();
        //                direccion.Usuario.Rol.IdRol = query.IdRol.Value;
        //                direccion.Usuario.Rol.Nombre = query.NombreRol;
        //                direccion.Usuario.Imagen = query.Imagen;



        //                result.Object = direccion; //boxing 
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "Error al Actualizar Usuario";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}

            //Actualizar EntityFramework
            public static ML.Result UpdateEF(ML.Direccion Direccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var update = context.Database.ExecuteSqlRaw($"DireccionUpdate '{Direccion.IdUsuario}', '{Direccion.Calle}', '{Direccion.NumeroExterior}','{Direccion.NumeroInterior}', '{Direccion.Colonia.IdColonia}'");
                    if (update >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al Actualizar dirección";
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
        public static ML.Result DireccionGetByIdColonia(int IdColonia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Direccions.FromSqlRaw($"DireccionGetAllByIDColonia {IdColonia}").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Direccion direccion = new ML.Direccion();

                            direccion.IdDireccion = obj.IdDireccion;
                            direccion.Calle = obj.Calle;
                            direccion.NumeroExterior = obj.NumeroExterior;
                            direccion.NumeroInterior = obj.NumeroInterior;
                            direccion.Colonia = new ML.Colonia();
                            direccion.Colonia.IdColonia = Convert.ToInt16(obj.IdColonia);

                            result.Objects.Add(direccion);
                        }
                        //result.Object = direccion; //boxing 
                        result.Correct = true;
                    }

                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error No Existen Registros";
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
