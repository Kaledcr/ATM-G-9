namespace ATM_G_9.Models
{
    /// <summary>
    /// Este es el nucleo del sistema ATM. Este gestiona la sesion activa del usuario, controla los intentos de acceso 
    /// y delega las operaciones financieras hacia la cuenta del usuario autenticado.
    /// Este actua como el objeto central del sistema que mantiene estado y coordina la interaccion entre user y account.
    /// </summary>
    public class Atm
    {
        /// <summary>
        /// Usuario actualmente autenticado en el cajero. Este es null si no hay sesion.
        /// </summary>
        private User _currentUser{  get; set; }

        /// <summary>
        /// Cuenta los intentos fallidos al ingresar el PIN.
        /// </summary>
        private int _failedAttempts {  get; set; }

        /// <summary>
        /// Este es el numero maximo de intentos para ingresar el PIN, antes de que se bloquee el acceso.
        /// </summary>
        private const int MAX_ATTEMPTS = 3;

        /// <summary>
        /// Esta es la lista de usuarios que se registran en el sistema.
        /// Simula la base de datos de clientes.
        /// </summary>
        private List<User> _registeredUsers { get; set; }

        /// <summary>
        /// Inicializa el cajero con estos usuarios precargados en el sistema.
        /// </summary>
        public Atm()
        {
            _failedAttempts = 0;
            _currentUser = null;

            _registeredUsers = new List<User>
            {
                new User("Darian González", "1234",  new CheckingAccount(150000)),
                new User("Teresa Pineda",   "5678", new SavingsAccount(80000))
            };
        }

        /// <summary>
        /// Intenta autenticar el usuario con su nombre y PIN.
        /// Si falla al ingresar el PIN el contador de fallos incrementa.
        /// </summary>
        /// <param name="name">Nombre de usuario que intenta acceder.</param>
        /// <param name="pin">PIN ingresado por el usuario</param>
        /// <returns>true si la autenticacion es exitosa</returns>

        public bool StartSession(string name, string pin)
        {
            User foudUser = _registeredUsers.Find(x => x.Name == name);

            if (foudUser == null)
                return false;

            if (foudUser.verifyPin(pin))
            {
                _currentUser = foudUser;
                _failedAttempts = 0;
                return true;

            }
            
            _failedAttempts++;
            return false;

            
        }

        /// <summary>
        /// Indica si el cajero esta bloqueado por axceso de intentos fallidos.
        /// </summary>
        

        public bool IsBlocked()
        {
            return _failedAttempts >= MAX_ATTEMPTS;
        }

        /// <summary>
        /// Indica si hay una session activa en el sistema.
        /// </summary>
        /// <returns>true si hay una session activa, false en caso contrario</returns>
        public bool HasActiveSession()
        {
            return _currentUser!= null;
        }

        /// <summary>
        /// Ejecuta un deposito sobre la cuenta de el usuario activo
        /// </summary>
        /// <param name="amount">Monto a depositar</param>
        
        public string ProcessDeposit(decimal amount)
        {
            if (!HasActiveSession())
                return "No hay session activa";

            return _currentUser.bankAccount.deposit(amount);
            
        }
        /// <summary>
        /// Ejecuta el retiro obre la cuenta del usuario activo
        /// </summary>
        /// <param name="amount">Monto a retirar</param>
        

        public string ProcessWithdraw(decimal amount)
        {
            if (!HasActiveSession())
                return "No hay session activa";

            return _currentUser.bankAccount.Withdraw(amount);

        }

        /// <summary>
        /// Retorna el saldo actual del usuario activo.
        /// </summary>
        /// <returns></returns>
        public decimal GetBalance()
        {
            if (!HasActiveSession())
                return -1;

            return _currentUser.bankAccount.consultarSaldo();
        }

        /// <summary>
        /// Retorna el hisotorial completo de transacciones del usuario activo.
        /// </summary>
        /// <returns></returns>

        public List<Transaccion> GetHistory()
        {
            if (!HasActiveSession())
                return new List<Transaccion>();

            return _currentUser.bankAccount.bankingHistory;
        }

        /// <summary>
        /// Cierra la sesion actual, limpiando el usuario activo y reinicia el contador de intentos.
        /// </summary>
        public void EndSession()
        {
            _currentUser = null;
            _failedAttempts = 0;
        }

        /// <summary>
        /// Retorna el nombre del usuario en sesion o vacio si no hay sesion
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserName()
        {
            return HasActiveSession() ? _currentUser.Name : string.Empty;
        }

        /// <summary>
        /// Retorna los intentos restantes antes del bloqueo.
        /// </summary>
        /// <returns></returns>

        internal int RemainingAttempts()
        {
            return MAX_ATTEMPTS - _failedAttempts;
        }
    }
}
