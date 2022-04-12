using System.ComponentModel.DataAnnotations;
namespace ML
{
    public class Departamento
    {
        public byte IdDepartamento { get; set; }

        //[Display(Name = "Nombre del Departamento")]
        //[Required(ErrorMessage = "El Nombre es requerido")]
        public string? Nombre { get; set; }
        public ML.Area? Area { get; set; } //FK 
        public List<object>? Departamentos { get; set; }
    }
}