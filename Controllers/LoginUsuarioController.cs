using Microsoft.AspNetCore.Mvc;
using UTS.Datos;
using UTS.Models;
// using UTS.Recurso;
//referencias para el trabajo de Autnticacion por galletas
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using UTS.Recurso;

namespace UTS.Controllers
{
    public class LoginUsuarioController : Controller
    {
        //instancia de nuestro controlador
        LoginUsuario logU= new LoginUsuario();
        //crear Accion del Registro
   //esto ver si asi agarra clave     Utilidades utilidades = new Utilidades();
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
    public IActionResult Registro(UsuarioModel model)
        {
            //  if (ModelState.IsValid) 
            if (!ModelState.IsValid)
            {
                return View();
            }
            model.contraseña=Utilidades.EncriptarClave(model.contraseña);
            bool crearUsuario = logU.Registro(model);
            if (!crearUsuario) 
            {
                //Retornar una alerta warning para aclarar que el correo ya esta registrado
                ViewData["Mensaje"] = "El correo ingresado ya existe joven";
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        //Crear accion del Login
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contraseña)
        {
            //UsuarioModel usuario = logU.ValidarUsuario(correo, contraseña);
             UsuarioModel usuario = logU.ValidarUsuario(correo, Utilidades.EncriptarClave(contraseña));
            if (usuario.clave_empleado == 0)
            {
                ViewData["Mensaje"] = "El correo o la contraseña esta mal joven";
                return View();
            }
            //Configuracion de la autentificacion de los usuarios
            //Crear una lista para poder almacenar la informacion del usuario
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,usuario.nombre)
            };
            //registrar el claim (Toda la info del usuario) en una estructura por defecto
            ClaimsIdentity claimsIdentity= new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            //Crear las propiedades de la autentificacion
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };
            //ya con esto el usuario ya se encuentra iniciado sesion dentro del sistema
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Listar", "Instalacion");
        }
        //Acion de clambiar contraseña
        public IActionResult CambiarContraseña()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CambiarContraseña(string correo, string contraseña)
        {
           // bool respuesta = logU.CambiarClave(correo, contraseña);

             bool respuesta = logU.CambiarClave(correo, Utilidades.EncriptarClave(contraseña));
            if (!respuesta) 
            {
                ViewData["Mensaje"] = "El correo no existe joven";
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
