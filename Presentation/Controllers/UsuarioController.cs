using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(ModelL.Usuario usuario)
        {
            Dictionary<string, object> result = Business.Usuario.Add(usuario);

            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ViewBag.Mensaje = "Registro Exitoso!";
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                ViewBag.Mensaje = "Error! No se pudo completar el registro " + excepcion;
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            Dictionary<string, object> result = Business.Usuario.Login(userName, password);
            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ModelL.Historial historial = (ModelL.Historial)result["Usuario"];
                Session["Historial"] = historial;
                return Redirect("SuperDigito");
            }
            else
            {
                ViewBag.Mensaje = "Datos incorrectos! Si no esta registrado, favor de registrarse";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["Historial"] = null;
            return Redirect("Login");
        }

        [HttpGet]
        public ActionResult SuperDigito()
        {
            ModelL.Historial historial = (ModelL.Historial)Session["Historial"];

            if (historial != null)
            {
                Dictionary<string, object> resultHistorial = Business.Historial.GetByUsuario(historial.Usuario.IdUsuario);

                bool resultadoHistorial = (bool)resultHistorial["Resultado"];

                if (resultadoHistorial)
                {
                    historial.Historiales = ((ModelL.Historial)resultHistorial["Historial"]).Historiales;
                    //ViewBag.Mensaje = "El numero se registro en su historial!";
                }
                else
                {
                    ViewBag.Mensaje = "Error";
                }

                return View(historial);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult SuperDigito(ModelL.Historial historial)
        {
            historial.Resultado = Business.Usuario.SuperDigito(historial);

            Dictionary<string, object> resultNumero = Business.Historial.GetNumero(historial);

            bool resultadoNumero = (bool)resultNumero["Resultado"];

            if (resultadoNumero)
            {
                Dictionary<string, object> resultHistorial = Business.Historial.GetByUsuario(historial.Usuario.IdUsuario);
                historial.Historiales = ((ModelL.Historial)resultHistorial["Historial"]).Historiales;
                ViewBag.Mensaje = "El numero ya estaba registrado en su historial!";
            }
            else
            {
                Dictionary<string, object> result = Business.Historial.Add(historial);

                bool resultado = (bool)result["Resultado"];
                //bool resultadoHistorial = (bool)resultHistorial["Resultado"];

                if (resultado)
                {
                    Dictionary<string, object> resultHistorial = Business.Historial.GetByUsuario(historial.Usuario.IdUsuario);
                    historial.Historiales = ((ModelL.Historial)resultHistorial["Historial"]).Historiales;
                    ViewBag.Mensaje = "El numero se registro en su historial!";
                }
                else
                {
                    ViewBag.Mensaje = "Error! El numero no pudo ser registrado en su historial";
                }
            }

            return View(historial);
        }

        [HttpGet]
        public ActionResult Delete(int idUsuario)
        {
            Dictionary<string, object> result = Business.Historial.DeleteByUsuario(idUsuario);

            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ViewBag.Mensaje = "Historial Eliminado";
                return RedirectToAction("SuperDigito");
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                ViewBag.Mensaje = "Error! el hitorial no pudo ser elimando " + excepcion;
                return RedirectToAction("SuperDigito");
            }
        }

        public ActionResult DeleteHistorial(int idHistorial)
        {
            Dictionary<string, object> result = Business.Historial.Delete(idHistorial);

            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ViewBag.Mensaje = "Registro Eliminado";
                return RedirectToAction("SuperDigito");
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                ViewBag.Mensaje = "Error! el registro no pudo ser elimando " + excepcion;
                return RedirectToAction("SuperDigito");
            }
        }
    }
}