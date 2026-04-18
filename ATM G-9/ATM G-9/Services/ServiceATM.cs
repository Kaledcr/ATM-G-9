using ATM_G_9.Models;


namespace ATM_G_9.Services
{

    /// <summary>
    /// Servicio estático que implementa la lógica de negocio del cajero automático
    /// mediante el paradigma procedimental.
    /// Cada metodo define una secuencia explicita de pasos ordenados (validaciones, ejecucion y respuesta)
    /// sin mantener estado propio. Actua como puente entre el controlador y los objetos del sistema.
    /// </summary>
    public static class ServiceATM
    {

        /// <summary>
        /// Proceso de autenticación del usuario en el cajero.
        /// <para><b>Pasos:</b>
        /// 1. Verificar si el cajero está bloqueado.
        /// 2. Intentar iniciar sesión con las credenciales.
        /// 3. Retornar mensaje según el resultado.</para>
        /// </summary>
        /// <param name="atm">Instancia del cajero automático.</param>
        /// <param name="name">Nombre del usuario ingresado.</param>
        /// <param name="pin">PIN ingresado por el usuario.</param>
        /// <returns>Resultado con éxito o fallo y mensaje descriptivo.</returns>

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

        /// <summary>
        /// Proceso de retiro de dinero con validaciones de negocio.
        /// <para><b>Pasos:</b>
        /// 1. Verificar sesión activa.
        /// 2. Validar que el monto sea positivo.
        /// 3. Ejecutar el retiro.</para>
        /// </summary>
        /// <param name="atm">Instancia del cajero automático.</param>
        /// <param name="amount">Monto que el usuario desea retirar.</param>
        /// <returns>Resultado con éxito o fallo y mensaje descriptivo.</returns>

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


        /// <summary>
        /// Proceso de depósito de dinero con validaciones de negocio.
        /// <para><b>Pasos:</b>
        /// 1. Verificar sesión activa.
        /// 2. Validar que el monto sea positivo.
        /// 3. Validar que no supere el límite por depósito.
        /// 4. Ejecutar el depósito.</para>
        /// </summary>
        /// <param name="atm">Instancia del cajero automático.</param>
        /// <param name="amount">Monto que el usuario desea depositar.</param>
        /// <returns>Resultado con éxito o fallo y mensaje descriptivo.</returns>

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

        /// <summary>
        /// Consulta el saldo disponible del usuario activo.
        /// <para><b>Pasos:</b>
        /// 1. Verificar sesión activa.
        /// 2. Obtener y retornar el saldo formateado.</para>
        /// </summary>
        /// <param name="atm">Instancia del cajero automático.</param>
        /// <returns>Resultado con el saldo formateado o mensaje de error.</returns>

        public static ServiceResult CheckBalance(Atm atm)
        {
            
            if (!atm.HasActiveSession())
                return new ServiceResult(false, "No hay sesión activa.");

            
            decimal balance = atm.GetBalance();

            
            return new ServiceResult(true, $"Su saldo disponible es: ₡{balance:N2}");
        }


        /// <summary>
        /// Obtiene el historial completo de transacciones del usuario activo.
        /// <para><b>Pasos:</b>
        /// 1. Verificar sesión activa.
        /// 2. Obtener la lista de transacciones.
        /// 3. Verificar que no esté vacía.
        /// 4. Retornar lista y resultado.</para>
        /// </summary>
        /// <param name="atm">Instancia del cajero automático.</param>
        /// <returns> el resultado y la lista de transacciones.</returns>
        public static (ServiceResult result, List<Transaccion> transactions) GetHistory(Atm atm)
        {
            
            if (!atm.HasActiveSession())
                return (new ServiceResult(false, "No hay sesión activa."), null);

            
            List<Transaccion> history = atm.GetHistory();

            
            if (history.Count == 0)
                return (new ServiceResult(false, "No hay transacciones registradas."), null);

            
            return (new ServiceResult(true, $"Se encontraron {history.Count} transacciones."), history);
        }


        /// <summary>
        /// Cierra la sesión del usuario activo en el cajero.
        /// <para><b>Pasos:</b>
        /// 1. Verificar que haya sesión activa.
        /// 2. Cerrar la sesión.
        /// 3. Confirmar el cierre.</para>
        /// </summary>
        /// <param name="atm">Instancia del cajero automático.</param>
        /// <returns>Resultado confirmando el cierre de sesión.</returns>
        public static ServiceResult Logout(Atm atm)
        {
            
            if (!atm.HasActiveSession())
                return new ServiceResult(false, "No había sesión activa.");

            
            atm.EndSession();

            
            return new ServiceResult(true, "Sesión cerrada correctamente.");
        }
    }
}
