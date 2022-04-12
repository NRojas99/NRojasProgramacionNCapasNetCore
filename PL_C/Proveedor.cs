using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Proveedor
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();

            StreamReader file = new StreamReader(@"C:\Users\digis\Documents\Nayeli Rojas Aguado\CargaMasiva\LayoutProveedor.txt");
            
            string line;

            ML.Result result2 = new ML.Result();
            result2.Objects = new List<object>();   

            line = file.ReadLine();

            while ((line = file.ReadLine()) != null)
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
                        "No se inserto la DireccionWeb"+ proveedor.DireccionWeb );
                }
            }

            TextWriter Text = new StreamWriter(@"C:\Users\digis\Documents\Nayeli Rojas Aguado\CargaMasiva\Archivo.txt");
            foreach(string error in result2.Objects)
            {
                Text.WriteLine(error);  
            }
            Text.Close();

            return result;  
        }
    }
}
