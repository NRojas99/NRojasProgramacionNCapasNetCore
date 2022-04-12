using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }

        [Display(Name = "Nombre del Producto")]
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        public ML.Proveedor Proveedor { get; set; }
        public ML.Departamento  Departamento{ get; set; }
        public List<object>? Productos { get; set; }
    }
}
