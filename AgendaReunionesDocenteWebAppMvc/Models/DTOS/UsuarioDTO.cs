using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgendaReunionesDocenteWebAppMvc.Models.DTOS
{
    public class UsuarioDTO
    {
        [Key]
        public Int64 Id { get; set; }
        [Required(ErrorMessage = "El campo Nombres es obligatorio")]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo Edad es obligatorio")]
        [Range(18, 70, ErrorMessage = "La edad de ser entre 18 y 70 años")]
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

        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        [StringLength(50)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El campo Password es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password1 { get; set; }

        [Required(ErrorMessage = "El campo Confirmación de Password es obligatorio")]
        [StringLength(50)]
        [Display(Name = "Repetir Contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password1", ErrorMessage = "Las contraseñas no coinciden")]
        public string Password2 { get; set; }
    }
}