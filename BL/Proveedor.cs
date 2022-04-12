using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result Add(ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorAdd '{proveedor.Nombre}','{proveedor.Telefono}','{proveedor.Email}','{proveedor.DireccionWeb}','{proveedor.Logo}'");
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
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Proveedor proveedor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorUpdate {proveedor.IdProveedor}, '{proveedor.Nombre}','{proveedor.Telefono}','{proveedor.Email}','{proveedor.DireccionWeb}','{proveedor.Logo}'");
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
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdProveedor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProveedorDelete {IdProveedor}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al borrar Area";
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var proveedores = context.Proveedors.FromSqlRaw("ProveedorGetAll").ToList();

                    result.Objects = new List<Object>();

                    if (proveedores != null)
                    {
                        foreach (var obj in proveedores)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = Convert.ToInt16(obj.IdProveedor);
                            proveedor.Nombre = obj.Nombre;
                            proveedor.Telefono = obj.Telefono;
                            proveedor.Email = obj.Email;
                            proveedor.DireccionWeb = obj.DireccionWeb;
                            proveedor.Logo = obj.Logo;
                            result.Objects.Add(proveedor);
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
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetByIdEF(int IdProveedor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var Obj = context.Proveedors.FromSqlRaw($"ProveedorGetById {IdProveedor}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (Obj != null)
                    {
                        ML.Proveedor proveedor = new ML.Proveedor();

                        proveedor.IdProveedor = Convert.ToInt16(Obj.IdProveedor);
                        proveedor.Nombre = Obj.Nombre;
                        proveedor.Telefono = Obj.Telefono;
                        proveedor.Email = Obj.Email;
                        proveedor.DireccionWeb = Obj.DireccionWeb;
                        proveedor.Logo = Obj.Logo;
                        //result.Objects.Add(usuario);
                        result.Correct = true;
                        result.Object = proveedor;
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
