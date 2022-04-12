using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public string Calle { get; set; }

        [Display(Name = "Numero Exterior:")]
        public string NumeroExterior { get; set; }

        [Display(Name = "Numero Interior:")]
        public string NumeroInterior { get; set; }
        public ML.Colonia Colonia { get; set; }
        public int? IdColonia { get; set; }
        public string NombreColonia { get; set; }

        public int IdUsuario { get; set; }

        public List<object> Direcciones { get; set; }

    }
}
