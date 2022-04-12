using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? DireccionWeb { get; set; }
        public string? Logo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
