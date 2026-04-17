using System.Timers;

namespace ATM_G_9.Models
{
    public class Account
    {
        string accountNumber { get; set; }

        private decimal Balance { get; set; }
        public List<Transaccion> bankingHistory { get; private set; }

        public Account(decimal balance)
        {
            accountNumber = Guid.NewGuid().ToString().Substring(0,8).ToUpper();
            Balance= balance;
            bankingHistory = new List<Transaccion>();
        }


        public string deposit(decimal monto)
        {
            if (monto <= 0)
                return "El monto debe ser mayor a 0";
            Balance += monto;

            bankingHistory.Add(new Transaccion(Transaccion.transactionType.deposit, monto));
            return "Deposito Exitoso";
        }

        public string withdraw(decimal monto)
        {
            if (monto <= 0)
                return "El monto debe ser mayor a cero.";

            if (monto > Balance)
                return "Saldo insuficiented";

            Balance -= monto;
            bankingHistory.Add(new Transaccion(Transaccion.transactionType.withdraw, monto));

            return $"Retiro exitoso. Nuevo saldo: ₡{Balance:N2}";
        }

        public decimal consultarSaldo()
        {
            return Balance;
        }
    }
}
