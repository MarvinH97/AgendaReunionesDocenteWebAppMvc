using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgendaReunionesDocenteWebAppMvc.Models
{
    public class Docentes
    {
        [Key]
        public Int64 Id { get; set; }

        [Display(Name = "Nombres *")]
        [Required(ErrorMessage = "El campo Nombres es obligatorio")]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos *")]
        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Display(Name = "Edad (18 - 70 años) *")]
        [Required(ErrorMessage = "El campo Edad es obligatorio.")]
        [Range(18, 70, ErrorMessage = "La edad debe estar entre 18 y 70.")]
        public int Edad { get; set; }

        [Column(TypeName = "char")]
        [Display(Name = "Género")]
        [StringLength(1)]
        public string Genero { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }

        [StringLength(10)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
    }
}