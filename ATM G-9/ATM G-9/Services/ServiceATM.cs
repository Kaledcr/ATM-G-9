using ATM_G_9.Models;

namespace ATM_G_9.Services
{
    public static class ServiceATM
    {
        
        public static ServiceResult Login(Atm atm, string name, string pin)
        {
            if (atm.IsBlocked())
                return new ServiceResult(false, "Cuenta bloqueada por demasiados intentos fallidos.");

            bool success = atm.StartSession(name, pin);

            if (success)
                return new ServiceResult(true, $"Bienvenido, {atm.GetCurrentUserName()}");

            int remaining = atm.RemainingAttempts();
            return new ServiceResult(false, $"PIN incorrecto. Intentos restantes: {remaining}");
        }

        public static ServiceResult ProcessWithdraw(Atm atm, decimal amount)
        {
            
            if (!atm.HasActiveSession())
                return new ServiceResult(false, "No hay sesión activa.");

            
            if (amount <= 0)
                return new ServiceResult(false, "El monto debe ser mayor a cero.");

            
            string result = atm.ProcessWithdraw(amount);

            bool wasSuccessful = result.Contains("exitoso");
            return new ServiceResult(wasSuccessful, result);
        }

        public static ServiceResult ProcessDeposit(Atm atm, decimal amount)
        {
            
            if (!atm.HasActiveSession())
                return new ServiceResult(false, "No hay sesión activa.");

            
            if (amount <= 0)
                return new ServiceResult(false, "El monto debe ser mayor a cero.");


            if (amount > 2000000)
                return new ServiceResult(false, "No se permiten depósitos mayores a ₡2,000,000.");

            
            string result = atm.ProcessDeposit(amount);

            
            return new ServiceResult(true, result);

        }

        public static ServiceResult CheckBalance(Atm atm)
        {
            
            if (!atm.HasActiveSession())
                return new ServiceResult(false, "No hay sesión activa.");

            
            decimal balance = atm.GetBalance();

            
            return new ServiceResult(true, $"Su saldo disponible es: ₡{balance:N2}");
        }

        public static (ServiceResult result, List<Transaccion> transactions) GetHistory(Atm atm)
        {
            
            if (!atm.HasActiveSession())
                return (new ServiceResult(false, "No hay sesión activa."), null);

            
            List<Transaccion> history = atm.GetHistory();

            
            if (history.Count == 0)
                return (new ServiceResult(false, "No hay transacciones registradas."), null);

            
            return (new ServiceResult(true, $"Se encontraron {history.Count} transacciones."), history);
        }


        public static ServiceResult Logout(Atm atm)
        {
            
            if (!atm.HasActiveSession())
                return new ServiceResult(false, "No había sesión activa.");

            
            atm.EndSession();

            
            return new ServiceResult(true, "Sesión cerrada correctamente.");
        }
    }
}
