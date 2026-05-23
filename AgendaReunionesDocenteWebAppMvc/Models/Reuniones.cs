using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgendaReunionesDocenteWebAppMvc.Models
{
    public class Reuniones
    {
        [Key]
        public Int64 Id { get; set; }

        [Column(TypeName = "bigint")]
        public Int64 IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Título *")]
        public string Titulo { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Descripción *")]
        [DataType(DataType.MultilineText)] // Especifica que el campo es de tipo texto multilínea
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Fecha Programación *")] // Etiqueta para mostrar en la vista
        [DataType(DataType.Date)] // Especifica que el campo es de tipo fecha
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaProgramacion { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }
    }
}