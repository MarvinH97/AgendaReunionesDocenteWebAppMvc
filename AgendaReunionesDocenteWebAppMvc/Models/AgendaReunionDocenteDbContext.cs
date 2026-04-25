using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgendaReunionesDocenteWebAppMvc.Models
{
    public class AgendaReunionDocenteDbContext :  DbContext
    {
        public AgendaReunionDocenteDbContext() : base("name=_conn")
        {
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Reuniones> Reuniones { get; set; }
    }
}