using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Departamento
    {
        public Departamento()
        {
            Productos = new HashSet<Producto>();
        }

        public byte IdDepartamento { get; set; }
        public string? Nombre { get; set; }
        public byte? IdArea { get; set; }
        public string NombreArea { get; set; }

        public virtual Area? IdAreaNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
