using Microsoft.AspNetCore.Mvc;
using UTS.Datos;
using UTS.Models;

namespace UTS.Controllers
{
    public class InstalacionController : Controller
    {
        InstalacionDatos _instalacionDatos = new InstalacionDatos();
        public IActionResult Listar()
        {
            var lista = _instalacionDatos.Lista();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(InstalacionModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View();
            }
            bool respuesta = _instalacionDatos.GuardarInstalacion(model);
           // var respuesta = _instalacionDatos.GuardarInstalacion(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Editar(int idaula)
        {
            //para obtener y mostrar el contacto a modificar
            InstalacionModel _instalacion = _instalacionDatos.ConsultarInstalacion(idaula);
                return View(_instalacion);
        }
        [HttpPost]
        public IActionResult Editar(InstalacionModel model)
        {
            //Para obtener los datos que se editaron del formulario y enviarlo en la base de datos
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _instalacionDatos.EditarInstalacion(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }    
            else
            { return View();
            }
        }

        //Contraolador de Eliminar
        public IActionResult Eliminar(int idaula) 
        {
            //para obtener y mostrar la instalacion a eliminar
            var _instalacion = _instalacionDatos.ConsultarInstalacion(idaula);
            return View(_instalacion);
        }

        [HttpPost]
        public IActionResult Eliminar(InstalacionModel model)
        {
            //Para obtener los datos que se van a eliminar del formulario y enviarlo en la base de datos
            var respuesta= _instalacionDatos.EliminarInstalacion(model.idaula);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            { return View(); }
        }
    }
}
