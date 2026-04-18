using System.Timers;

namespace ATM_G_9.Models
{
    /// <summary>
    /// Esta clase representa la cuenta bancaria de un usuario dentro de un sistema ATM.
    /// Este gestiona el saldo, el hitorial de movimientos financieros y los movimientos financieros como tal.
    /// <para>En este se encapsula el estado financiero del usuario y expone operaciones controladas sobre ese estado.
    /// El saldo no puedo ser modificado desde fuera solo atravez de metodos.</para>
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Aqui definimo los atributos que tendra una cuenta, como es el numero de cuenta generado automaticamente al crearse,
        /// el saldo disponible en la cuenta y un registro cronologico de todas las transacciones realizadas en al cuenta.
        /// </summary>
        string AccountNumber { get; set; }

        private decimal Balance { get; set; }
        public List<Transaccion> bankingHistory { get; private set; }

        public Account(decimal balance)
        {
            AccountNumber = Guid.NewGuid().ToString().Substring(0,8).ToUpper();
            Balance= balance;
            bankingHistory = new List<Transaccion>();
        }

        /// <summary>
        /// Agrega el dinero al saldo de la cuenta y registra la transaccion en el historial.
        /// </summary>
        /// <param name="monto">Monto a depositar</param>
        /// <returns>Mensaje indicando el resultado de la operacion</returns>

        public string deposit(decimal monto)
        {
            if (monto <= 0)
                return "El monto debe ser mayor a 0";
            Balance += monto;

            bankingHistory.Add(new Transaccion(Transaccion.transactionType.deposit, monto));
            return "Deposito Exitoso.";
        }
        /// <summary>
        /// Descuenta el dinero del saldo de la cuenta si este tiene fondos suficientes y registra 
        /// la transaccion en el historial.
        /// </summary>
        /// <param name="monto">Monto a retirar</param>
        /// <returns>Mensaje indicando el resultado de la operacion</returns>

        public string withdraw(decimal monto)
        {
            if (monto <= 0)
                return "El monto debe ser mayor a cero.";

            if (monto > Balance)
                return "Saldo insuficiented";

            Balance -= monto;
            bankingHistory.Add(new Transaccion(Transaccion.transactionType.withdraw, monto));

            return $"Retiro exitoso.";
        }
        /// <summary>
        /// Retorna el saldo actual de la cuenta sin modificarlo
        /// </summary>
        /// <returns>Saldo disponible como valor decimal</returns>
        public decimal consultarSaldo()
        {
            return Balance;
        }
    }
}
