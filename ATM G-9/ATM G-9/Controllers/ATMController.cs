using ATM_G_9.Models;
using ATM_G_9.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATM_G_9.Controllers
{
    public class ATMController : Controller
    {
        private readonly Atm _atm;
        public ATMController(Atm atm) 
        {
            _atm = atm;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login (string name, string pin)
        {
            ServiceResult result = ServiceATM.Login(_atm, name, pin);

            if(result.Success)
                return RedirectToAction("Index");

            ViewBag.Error = result.Message;
            return View();
        }

        [HttpPost]
        public IActionResult Withdraw(decimal amount)
        {
            ServiceResult result = ServiceATM.ProcessWithdraw(_atm, amount);
            ViewBag.Message = result.Message;
            ViewBag.Success = result.Success;
            return View("Menu");
        }

        [HttpPost]
        public IActionResult Deposit(decimal amount)
        {
            ServiceResult result = ServiceATM.ProcessDeposit(_atm, amount);
            ViewBag.Message = result.Message;
            ViewBag.Success = result.Success;
            return View("Menu");
        }

        [HttpGet]
        public IActionResult Menu()
        {
            if (!_atm.HasActiveSession())
                return RedirectToAction("Login");

            ViewBag.UserName = _atm.GetCurrentUserName();
            return View();
        }

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
    }
}
