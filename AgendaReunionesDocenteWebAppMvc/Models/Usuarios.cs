using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgendaReunionesDocenteWebAppMvc.Models
{
    public class Usuarios
    {
        [Key]
        public Int64 Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Required]
        [Range(18, 70)]
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

        [StringLength(50)]
        public string Usuario { get; set; }

        [StringLength(50)]
        public string Clave { get; set; }
        public bool EsDocente { get; set; }
    }
}