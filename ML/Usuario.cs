using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Display(Name= "User Name:" )]
        [Required(ErrorMessage ="UserName es requerido")]
        public string UserName { get; set; }

        [Display(Name= "Contraseña:" )]
        [Required(ErrorMessage ="Contraseña Requerida")]
        public string Password { get; set; }
        public string Nombre { get; set; }

        [Display(Name= "Apellido Parterno:" )]
        [Required(ErrorMessage ="Apellido es necesario")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Marterno:")]
        [Required(ErrorMessage = "Apellido es necesario")]
        public string ApellidoMaterno { get; set; }

        [Display(Name = "Correo Electronico:")]
        [Required(ErrorMessage = "Su correo es necesario")]
        public string Email { get; set; }
        [Display(Name = "Fecha de Nacimiento:")]
        [Required(ErrorMessage = "Su Fecha de nacimiento es necesaria")]
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public bool Estatus { get; set; }
        public string CURP { get; set; }
        public ML.Rol Rol { get; set; }

        public string Imagen { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Direccion Direccion { get; set; }
    }
}
