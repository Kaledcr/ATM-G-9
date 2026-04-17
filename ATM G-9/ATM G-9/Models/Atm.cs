namespace ATM_G_9.Models
{
    public class Atm
    {
        private User _currentUser{  get; set; }
        private int _failedAttempts {  get; set; }

        private const int MAX_ATTEMPTS = 3;
        private List<User> _registeredUsers { get; set; }

        public Atm()
        {
            _failedAttempts = 0;
            _currentUser = null;

            _registeredUsers = new List<User>
            {
                new User("Darian González", "1234", 150000),
                new User("Teresa Pineda",   "5678", 80000)
            };
        }

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

        public bool IsBlocked()
        {
            return _failedAttempts >= MAX_ATTEMPTS;
        }

        public bool HasActiveSession()
        {
            return _currentUser!= null;
        }

        public string ProcessDeposit(decimal amount)
        {
            if (!HasActiveSession())
                return "No hay session activa";

            return _currentUser.bankAccount.deposit(amount);
            
        }

        public string ProcessWithdraw(decimal amount)
        {
            if (!HasActiveSession())
                return "No hay session activa";

            return _currentUser.bankAccount.withdraw(amount);

        }

        public decimal GetBalance()
        {
            if (!HasActiveSession())
                return -1;

            return _currentUser.bankAccount.consultarSaldo();
        }

        public List<Transaccion> GetHistory()
        {
            if (!HasActiveSession())
                return new List<Transaccion>();

            return _currentUser.bankAccount.bankingHistory;
        }

        public void EndSession()
        {
            _currentUser = null;
            _failedAttempts = 0;
        }

        public string GetCurrentUserName()
        {
            return HasActiveSession() ? _currentUser.Name : string.Empty;
        }

        internal int RemainingAttempts()
        {
            return MAX_ATTEMPTS - _failedAttempts;
        }
    }
}
