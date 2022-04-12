using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Proveedor
    {
        //[Display(Name = "Nombre del Proveedor")]

        public int IdProveedor { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? DireccionWeb { get; set; }
        public string? Logo { get; set; }
        public List<object>? Proveedores { get; set; }
    }
}
