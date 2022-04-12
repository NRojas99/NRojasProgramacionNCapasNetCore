using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result Add(ML.Area area)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context= new DL.NRojasProgramacionNCapasContext())
                   {
                    var query = context.Database.ExecuteSqlRaw($"AreaAdd '{area.Nombre}'");
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
        public static ML.Result Update(ML.Area area)
        {
            ML.Result result=new ML.Result(); 
            try 
            {
                using (DL.NRojasProgramacionNCapasContext context= new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AreaUpdate {area.IdArea}, '{area.Nombre}'");
                    if(query > 0)
                    {
                        result.Correct=true;    
                    }
                    else
                    {
                        result.Correct=false;
                    }
                }
                result.Correct= true;   
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete (byte IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AreaDelete {IdArea}");
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
                    var areas = context.Areas.FromSqlRaw("AreaGetall").ToList();

                    result.Objects = new List<Object>();

                    if (areas != null)
                    {
                        foreach (var obj in areas)
                        {
                            ML.Area area = new ML.Area();

                            area.IdArea = obj.IdArea;
                            area.Nombre = obj.Nombre;

                            result.Objects.Add(area);
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
        public static ML.Result GetByIdEF(byte IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var Obj = context.Areas.FromSqlRaw($"AreaGetById {IdArea}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (Obj != null)
                    {


                        ML.Area area = new ML.Area();    

                        area.IdArea = Convert.ToByte(Obj.IdArea);
                        area.Nombre = Obj.Nombre;

                        result.Objects.Add(area);

                        result.Correct = true;
                        result.Object = area;
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
