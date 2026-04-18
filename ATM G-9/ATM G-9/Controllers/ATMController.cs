using ATM_G_9.Models;
using ATM_G_9.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace ATM_G_9.Controllers
{
    /// <summary>
    /// Controlador principal de la aplicación web ATM.
    /// Recibe las solicitudes HTTP del navegador, las delega al servicio
    /// correspondiente y retorna la vista adecuada con el resultado.
    /// </summary>
    public class ATMController : Controller
    {

        /// <summary>
        /// Instancia del cajero automático inyectada automáticamente por ASP.NET.
        /// Al ser Singleton, es la misma instancia durante toda la sesión del usuario.
        /// </summary>
        private readonly Atm _atm;

        /// <summary>
        /// Constructor — ASP.NET inyecta automáticamente el <see cref="Atm"/>
        /// registrado como Singleton en Program.cs.
        /// </summary>
        /// <param name="atm">Instancia única del cajero automático del sistema.</param>
        public ATMController(Atm atm) 
        {
            _atm = atm;
        }

        /// <summary>
        /// Muestra el formulario de inicio de sesión.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Procesa las credenciales del formulario de login.
        /// Redirige al menu si son correctas, o regresa al login con mensaje de error.
        /// </summary>
        /// <param name="name">Nombre ingreasado en el formulario</param>
        /// <param name="pin">PIN ingresado en el formulario</param>

        [HttpPost]
        public IActionResult Login (string name, string pin)
        {
            ServiceResult result = ServiceATM.Login(_atm, name, pin);

            if(result.Success)
                return RedirectToAction("Menu");

            ViewBag.Error = result.Message;
            return View();
        }

        /// <summary>
        /// Procesa un retiro de dinero enviado desde el menu.
        /// Retorna al menu con el mensaje de resultado de la operacion.
        /// </summary>
        /// <param name="amount">Monto a retirar ingresado en el formulario.</param>
        [HttpPost]
        public IActionResult Withdraw(decimal amount)
        {
            ServiceResult result = ServiceATM.ProcessWithdraw(_atm, amount);
            ViewBag.Message = result.Message;
            ViewBag.Success = result.Success;
            return View("Menu");
        }


        /// <summary>
        /// Procesa un deposito de dinero enviado desde el menu.
        /// Retorna al menu con el mensaje de resultado de la operacion.
        /// </summary>
        /// <param name="amount">Monto a depositar ingresado en el formulario.</param>

        [HttpPost]
        public IActionResult Deposit(decimal amount)
        {
            ServiceResult result = ServiceATM.ProcessDeposit(_atm, amount);
            ViewBag.Message = result.Message;
            ViewBag.Success = result.Success;
            return View("Menu");
        }


        /// <summary>
        /// Muestra el menú principal del cajero.
        /// Redirige al login si no hay sesión activa.
        /// </summary>
        [HttpGet]
        public IActionResult Menu()
        {
            if (!_atm.HasActiveSession())
                return RedirectToAction("Login");

            ViewBag.UserName = _atm.GetCurrentUserName();
            return View();
        }

        /// <summary>
        /// Muestra la pagina con el saldo disponible del usuario activo.
        /// Redirige al login si no hay sesión activa.
        /// </summary>
        [HttpGet]
        public IActionResult CheckBalance()
        {
            if (!_atm.HasActiveSession())
                return RedirectToAction("Login");

            ServiceResult result = ServiceATM.CheckBalance(_atm);
            ViewBag.Message = result.Message;
            ViewBag.Success = result.Success;
            return View();
        }


        /// <summary>
        /// Muestra el historial de transacciones del usuario activo.
        /// Pasa la lista de transacciones directamente como modelo a la vista.
        /// Redirige al login si no hay sesion activa.
        /// </summary>
        [HttpGet]
        public IActionResult History()
        {
            if (!_atm.HasActiveSession())
                return RedirectToAction("Login");

            var (result, transactions) = ServiceATM.GetHistory(_atm);

            ViewBag.Message = result.Message;
            ViewBag.Success = result.Success;

            return View(transactions ?? new List<Transaccion>());
        }

        /// <summary>
        /// Cierra la sesión del usuario actual y redirige al login.
        /// </summary>
        [HttpPost]
        public IActionResult Logout()
        {
            ServiceATM.Logout(_atm);
            return RedirectToAction("Login");
        }
    }
}
