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
    public class Producto
    {
        //Agregar EntityFramework
        public static ML.Result AddEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{ producto.Nombre}', { producto.PrecioUnitario}, {producto.Stock}, {producto.Proveedor.IdProveedor},{ producto.Departamento.IdDepartamento}, '{producto.Descripcion}', '{producto.Imagen}'");
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
        //Actualizar EntityFramework
        public static ML.Result UpdateEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto},'{ producto.Nombre}', '{ producto.PrecioUnitario}', {producto.Stock}, '{producto.Proveedor.IdProveedor}','{ producto.Departamento.IdDepartamento}', '{producto.Descripcion}', '{producto.Imagen}'");
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
        //Borrar EntityFramework
        public static ML.Result DeleteEF(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoDelete {IdProducto}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al borrar Producto";
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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var productos = context.Productos.FromSqlRaw("ProductoGetAll").ToList();

                    result.Objects = new List<Object>();

                    if (productos != null)
                    {
                        foreach (var obj in productos)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.PrecioUnitario = Convert.ToDecimal(obj.PrecioUnitario);
                            producto.Stock = Convert.ToInt16(obj.Stock);
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = Convert.ToInt16(obj.IdProveedor);
                            producto.Proveedor.Nombre = obj.NombreProveedor;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = Convert.ToByte(obj.IdDepartamento);
                            producto.Departamento.Nombre = obj.NombreDepartamento;

                            producto.Departamento.Area = new ML.Area();
                            producto.Departamento.Area.IdArea = Convert.ToByte(obj.IdArea);
                            producto.Departamento.Area.Nombre = obj.NombreArea;

                            producto.Descripcion = obj.Descripcion;
                            //producto.Imagen = obj.Imagen;
                            result.Objects.Add(producto);

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
        public static ML.Result GetByIdEF(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var Obj = context.Productos.FromSqlRaw($"ProductoGetById {IdProducto}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (Obj != null)
                    {


                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = Obj.IdProducto;
                        producto.Nombre = Obj.Nombre;
                        producto.PrecioUnitario = Convert.ToDecimal(Obj.PrecioUnitario);
                        producto.Stock = Convert.ToInt16(Obj.Stock);
                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = Convert.ToInt16(Obj.IdProveedor);
                        producto.Proveedor.Nombre = Obj.NombreProveedor;
                        producto.Proveedor.IdProveedor = Convert.ToInt16(Obj.IdProveedor);
                        producto.Proveedor.Nombre = Obj.NombreProveedor;
                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = Convert.ToByte(Obj.IdDepartamento);
                        producto.Departamento.Nombre = Obj.NombreDepartamento;
                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = Convert.ToByte(Obj.IdArea);
                        producto.Departamento.Area.Nombre = Obj.NombreArea;
                        producto.Descripcion = Obj.Descripcion;
                        //producto.Imagen = Obj.Imagen;
                        result.Objects.Add(producto);

                        result.Correct = true;
                        result.Object = producto;
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

        public static ML.Result ProductoGetByIdDepartamento(byte IdDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context=new DL.NRojasProgramacionNCapasContext())
                {
                    var query=context.Productos.FromSqlRaw($"ProductoGetByIdDepartamento {IdDepartamento}").ToList();
                    result.Objects = new List<object>();

                if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();   

                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.Stock = obj.Stock.Value;
                            producto.PrecioUnitario = Convert.ToDecimal(obj.PrecioUnitario);
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Proveedor.Nombre = obj.NombreProveedor;
                            producto.Departamento= new ML.Departamento();   
                            producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Departamento.Nombre = obj.NombreDepartamento;
                            producto.Departamento.Area = new ML.Area();
                            producto.Departamento.Area.IdArea = Convert.ToByte(obj.IdArea);
                            producto.Departamento.Area.Nombre = obj.NombreArea;
                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen;

                            result.Objects.Add(producto);
                        }
                        result.Correct=true;    
                    }
                    else
                    {
                        result.ErrorMessage = "Ocurrio un error al traer los productos";
                        result.Correct = false; 
                    }
                }
                
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;   
                result.Correct= false;  
                result.Ex=ex;
            }
            return result;
        }

        public static ML.Result GetAllExcel(string ConnectionString)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (OleDbConnection context = new OleDbConnection(ConnectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.Connection.Open();
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                        DataTable tablaProveedor = new DataTable();

                        da.SelectCommand = cmd;
                        da.Fill(tablaProveedor);
                        if (tablaProveedor.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaProveedor.Rows)
                            {
                                ML.Producto producto= new ML.Producto();
                                producto.Nombre = row[0].ToString();
                                producto.PrecioUnitario =Convert.ToDecimal(row[1].ToString());
                                producto.Stock = Convert.ToInt32(row[2].ToString());
                                producto.Proveedor = new ML.Proveedor();
                                producto.Proveedor.IdProveedor = Convert.ToInt32(row[3].ToString());
                                producto.Departamento = new ML.Departamento();
                                producto.Departamento.IdDepartamento = Convert.ToByte(row[4].ToString());
                                producto.Descripcion = row[5].ToString();
                                producto.Imagen = row[6].ToString();

                                result.Objects.Add(producto);
                                result.Correct = true;
                            }
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error";
                        }
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

        public static ML.Result Validacion(List<object> Object)
        {
            ML.Result result = new ML.Result();
            result.Object = new List<object>();
            string errorMesage;
            int contador = 1;

            foreach (ML.Producto producto in Object)
            {
                errorMesage = "";
                errorMesage += (producto.Nombre == "") ? "Falta Nombre " : "";
                errorMesage += (producto.PrecioUnitario == null) ? "Falta Precio " : "";
                errorMesage += (producto.Stock == null) ? "Falta Stock " : "";
                errorMesage += (producto.Proveedor.IdProveedor == null) ? "Falta Proveedor" : "";
                errorMesage += (producto.Departamento.IdDepartamento == null) ? "Falta Departamento" : "";
                errorMesage += (producto.Descripcion == "") ? "Falta Descriocion" : "";

                if (errorMesage != "")
                {
                    ML.ExcelErrores error = new ML.ExcelErrores();
                    error.IdError = contador;
                    error.Error = errorMesage;
                    result.Objects.Add(error);
                }
                contador++;
            }
            return result;  
        }
}
}
