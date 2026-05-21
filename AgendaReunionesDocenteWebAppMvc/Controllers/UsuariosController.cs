using AgendaReunionesDocenteWebAppMvc.Models;
using AgendaReunionesDocenteWebAppMvc.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaReunionesDocenteWebAppMvc.Controllers
{
    public class UsuariosController : Controller
    {
        private AgendaReunionDocenteDbContext db = new AgendaReunionDocenteDbContext();
        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var usuario = db.Usuarios
                    .FirstOrDefault(u => u.Usuario == model.Usuario);
                if (usuario != null)
                {
                    bool valido = SeguridadHelper.ValidarPassword(model.Clave, usuario.Password, usuario.Salt);
                    if (valido)
                        Console.WriteLine("✅ Login exitoso");
                    else
                        Console.WriteLine("❌ Credenciales inválidas");
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult CrearUsuario()
        {
            var generos = new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Masculino" },
                new SelectListItem { Value = "F", Text = "Femenino" }
            };

            ViewBag.Generos = generos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearUsuario([Bind(Include = "Id,Nombres,Apellidos,Edad,Genero,Correo,Telefono,Usuario,Password1,Password2")] UsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                var (hash, salt) = SeguridadHelper.CrearPasswordHash(usuarioDTO.Password1);
                Usuarios usuarioDB = new Usuarios();
                usuarioDB.Nombres = usuarioDTO.Nombres;
                usuarioDB.Apellidos = usuarioDTO.Apellidos;
                usuarioDB.Edad = usuarioDTO.Edad;
                usuarioDB.Genero = usuarioDTO.Genero;
                usuarioDB.Correo = usuarioDTO.Correo;
                usuarioDB.Telefono = usuarioDTO.Telefono;
                usuarioDB.Usuario = usuarioDTO.Usuario;
                usuarioDB.Password = hash;
                usuarioDB.Salt = salt;
                db.Usuarios.Add(usuarioDB);
                db.SaveChanges();
                TempData["ToastMessage"] = "El registro se creó correctamente.";
                return RedirectToAction("Login", "Home");
            }

            return View(usuarioDTO);
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