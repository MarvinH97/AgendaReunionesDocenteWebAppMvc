using AgendaReunionesDocenteWebAppMvc.Models;
using AgendaReunionesDocenteWebAppMvc.Helpers;
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

        internal void InicializarDatos()
        {
            var generos = new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Masculino" },
                new SelectListItem { Value = "F", Text = "Femenino" }
            };
            ViewBag.Generos = generos;
        }

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
                    {
                        Session["userId"] = usuario.Id;
                        Session["userName"] = $"{usuario.Nombres} {usuario.Apellidos}";
                        TempData["ToastMessage"] = $"Bienvenido {usuario.Nombres} {usuario.Apellidos}";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        TempData["mensaje"] = "Credenciales inválidas";
                } 
                else 
                {
                    TempData["mensaje"] = "Credenciales inválidas";
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout() { 
            Session.Clear();
            return RedirectToAction("Login", "Usuarios");
        }

        [HttpGet]
        public ActionResult CrearUsuario()
        {
            InicializarDatos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearUsuario([Bind(Include = "Id,Nombres,Apellidos,Edad,Genero,Correo,Telefono,Usuario,Password1,Password2")] UsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                if (db.Usuarios.FirstOrDefault(u => u.Usuario == usuarioDTO.Usuario) != null)
                {
                    TempData["mensaje"] = "El nombre de usuario ya existe";
                    InicializarDatos();
                    return View(usuarioDTO);
                }
                else 
                {
                    var (hash, salt) = SeguridadHelper.CrearPasswordHash(usuarioDTO.Password1);
                    Usuarios usuarioDB = new Usuarios
                    {
                        Nombres = usuarioDTO.Nombres,
                        Apellidos = usuarioDTO.Apellidos,
                        Edad = usuarioDTO.Edad,
                        Genero = usuarioDTO.Genero,
                        Correo = usuarioDTO.Correo,
                        Telefono = usuarioDTO.Telefono,
                        Usuario = usuarioDTO.Usuario,
                        Password = hash,
                        Salt = salt
                    };
                    db.Usuarios.Add(usuarioDB);
                    db.SaveChanges();
                    TempData["ToastMessage"] = "El registro se creó correctamente.";
                    return RedirectToAction("Login", "Usuarios");
                }
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