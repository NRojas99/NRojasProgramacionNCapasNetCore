﻿using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Ventum
    {
        public Ventum()
        {
            VentaProductos = new HashSet<VentaProducto>();
        }

        public int IdVenta { get; set; }
        public decimal? Total { get; set; }
        public byte? IdMetodoPago { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual MetodoPago? IdMetodoPagoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<VentaProducto> VentaProductos { get; set; }
    }
}
