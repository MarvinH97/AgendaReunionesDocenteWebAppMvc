using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgendaReunionesDocenteWebAppMvc.Helpers;
using AgendaReunionesDocenteWebAppMvc.Models;

namespace AgendaReunionesDocenteWebAppMvc.Controllers
{
    [SessionAuthorize]
    public class ReunionesController : Controller
    {
        private AgendaReunionDocenteDbContext db = new AgendaReunionDocenteDbContext();

        // GET: Reuniones
        public ActionResult Index(string filtro = "")
        {
            var reuniones = db.Reuniones.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                reuniones = reuniones.Where(r => r.Titulo.Contains(filtro) || r.Descripcion.Contains(filtro));
            }

            ViewBag.Filtro = filtro;
            return View(reuniones.ToList());
        }

        // GET: Reuniones/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reuniones reuniones = db.Reuniones.Find(id);
            if (reuniones == null)
            {
                return HttpNotFound();
            }
            return View(reuniones);
        }

        // GET: Reuniones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reuniones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUsuario,Titulo,Descripcion,FechaProgramacion,Estado")] Reuniones reuniones)
        {
            if (ModelState.IsValid)
            {
                db.Reuniones.Add(reuniones);
                db.SaveChanges();
                TempData["ToastMessage"] = "El registro se creó correctamente.";
                return RedirectToAction("Index");
            }

            return View(reuniones);
        }

        // GET: Reuniones/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reuniones reuniones = db.Reuniones.Find(id);
            if (reuniones == null)
            {
                return HttpNotFound();
            }
            return View(reuniones);
        }

        // POST: Reuniones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUsuario,Titulo,Descripcion,FechaProgramacion,Estado")] Reuniones reuniones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reuniones).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ToastMessage"] = "El registro se modificó correctamente.";
                return RedirectToAction("Index");
            }
            return View(reuniones);
        }

        // GET: Reuniones/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reuniones reuniones = db.Reuniones.Find(id);
            if (reuniones == null)
            {
                return HttpNotFound();
            }
            return View(reuniones);
        }

        // POST: Reuniones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Reuniones reuniones = db.Reuniones.Find(id);
            db.Reuniones.Remove(reuniones);
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
