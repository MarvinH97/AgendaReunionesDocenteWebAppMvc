using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgendaReunionesDocenteWebAppMvc.Models
{
    public class Initializer :  CreateDatabaseIfNotExists<AgendaReunionDocenteDbContext>
    {
        protected override void Seed(AgendaReunionDocenteDbContext context)
        {
            base.Seed(context);
        }
    }
}