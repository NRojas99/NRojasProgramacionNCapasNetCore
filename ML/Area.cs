using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Area
    {
        [Display(Name = "Nombre del Area a la que pertenece")]
        [Required(ErrorMessage = "La Area es necesario")]
        public byte IdArea { get; set; }
        public string? Nombre { get; set; }
        public List<object>? Areas { get; set; }
    }
}
