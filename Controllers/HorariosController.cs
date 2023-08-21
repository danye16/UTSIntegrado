using Microsoft.AspNetCore.Mvc;
using UTS.Datos;
using UTS.Models;

namespace UTS.Controllers
{
    public class HorariosController : Controller
    {
        HorarioDatos _horarioDatos = new HorarioDatos();
        public IActionResult Listar()
        {
            var lista = _horarioDatos.Listar();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Insertar() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(horario_agendaModel model)
        {
            var respuesta = _horarioDatos.InsertarApartado(model);
            if(respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
    }


}
