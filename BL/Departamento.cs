using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result AddEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoAdd '{departamento.Nombre}', {departamento.Area.IdArea}");


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
        public static ML.Result UpdateEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoUpdate {departamento.IdDepartamento},{departamento.Nombre},{departamento.Area.IdArea}");
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
        public static ML.Result DeleteEF(byte IdDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoDelete {IdDepartamento}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al Borrar Departamento";
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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var departamentos = context.Departamentos.FromSqlRaw("DepartamentoGetAll").ToList();

                    result.Objects = new List<Object>();
                   
                    if (departamentos != null)
                    {
                        foreach (var obj in departamentos)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = Convert.ToByte(obj.IdDepartamento);
                            departamento.Nombre = obj.Nombre; 
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = Convert.ToByte(obj.IdArea);
                            departamento.Area.Nombre = obj.NombreArea;

                            result.Objects.Add(departamento);
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
        public static ML.Result GetByIdEF(byte IdDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var obj = context.Departamentos.FromSqlRaw($"DepartamentoGetById {IdDepartamento}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<Object>();

                    if (obj != null)
                    {
                        ML.Departamento departamento = new ML.Departamento();

                        departamento.IdDepartamento = Convert.ToByte(obj.IdDepartamento);
                        departamento.Nombre = obj.Nombre; 
                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = Convert.ToByte(obj.IdArea);
                        departamento.Area.Nombre = obj.NombreArea;

                        result.Correct = true;
                        result.Object = departamento;
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
        public static ML.Result DepartamentoGetByIdArea(byte IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context =  new DL.NRojasProgramacionNCapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByIdArea {IdArea}").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;
                            departamento.Area.Nombre = obj.NombreArea;

                            result.Objects.Add(departamento);
                        }
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



        public static ML.Result GetAllLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.NRojasProgramacionNCapasContext context = new DL.NRojasProgramacionNCapasContext())
                {
                    var DepartamentoList = (from DepartamentoTable in context.Departamentos
                                            select
                                            new
                                            {
                                                IdDepartamento = DepartamentoTable.IdDepartamento,
                                                Nombre = DepartamentoTable.Nombre,
                                                IdArea= DepartamentoTable.IdArea,   
                                                NombreArea = DepartamentoTable.IdAreaNavigation.Nombre,
                                            }
                                      ).ToList();
                    result.Objects = new List<object>();

                    if (DepartamentoList.Count > 0)
                    {
                        foreach (var DepartamentoItem in DepartamentoList)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento= DepartamentoItem.IdDepartamento;
                            departamento.Nombre = DepartamentoItem.Nombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = DepartamentoItem.IdArea.Value;
                            departamento.Area.Nombre = DepartamentoItem.NombreArea;
                            result.Objects.Add(departamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;

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

    }
}