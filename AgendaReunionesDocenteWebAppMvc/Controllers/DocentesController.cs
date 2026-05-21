using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgendaReunionesDocenteWebAppMvc.Models;

namespace AgendaReunionesDocenteWebAppMvc.Controllers
{
    public class DocentesController : Controller
    {
        private AgendaReunionDocenteDbContext db = new AgendaReunionDocenteDbContext();

        // GET: Docentes
        public ActionResult Index(string filtro = "")
        {
            var docentes = db.Docentes.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                docentes = docentes.Where(u => u.Nombres.Contains(filtro) || u.Apellidos.Contains(filtro));
            }

            ViewBag.Filtro = filtro;
            return View(docentes.ToList());
        }

        // GET: Docentes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docentes docentes = db.Docentes.Find(id);
            if (docentes == null)
            {
                return HttpNotFound();
            }
            return View(docentes);
        }

        // GET: Docentes/Create
        public ActionResult Create()
        {
            var generos = new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Masculino" },
                new SelectListItem { Value = "F", Text = "Femenino" }
            };

            ViewBag.Generos = generos;
            return View();
        }

        // POST: Docentes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombres,Apellidos,Edad,Genero,Correo,Telefono")] Docentes docentes)
        {
            if (ModelState.IsValid)
            {
                db.Docentes.Add(docentes);
                db.SaveChanges();
                TempData["ToastMessage"] = "El registro se creó correctamente.";
                return RedirectToAction("Index");
            }

            return View(docentes);
        }

        // GET: Docentes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docentes docentes = db.Docentes.Find(id);
            if (docentes == null)
            {
                return HttpNotFound();
            }
            var generos = new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Masculino" },
                new SelectListItem { Value = "F", Text = "Femenino" }
            };
            ViewBag.Generos = generos;
            return View(docentes);
        }

        // POST: Docentes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Apellidos,Edad,Genero,Correo,Telefono")] Docentes docente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docente).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ToastMessage"] = "El registro se modificó correctamente.";
                return RedirectToAction("Index");
            }
            return View(docente);
        }

        // GET: Docentes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docentes docente = db.Docentes.Find(id);
            if (docente == null)
            {
                return HttpNotFound();
            }
            return View(docente);
        }

        // POST: Docentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Docentes docente = db.Docentes.Find(id);
            db.Docentes.Remove(docente);
            db.SaveChanges();
            TempData["ToastMessage"] = "El registro se eliminó correctamente.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
