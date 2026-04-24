namespace ATM_G_9.Models
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(decimal balance) : base(balance)
        {
        }

        public override decimal GetDailyLimit() => 200000;

        public override string Withdraw(decimal monto)
        {
            if(dailyLimit == GetDailyLimit())
                return "Alcanzaste el limite diario de retiro";

            if (monto <= 0)
                return "El monto debe ser mayor a cero.";

            if (monto > GetDailyLimit())
                return $"Cuenta de ahorros: límite diario es ₡{GetDailyLimit():N0}";

            if (monto > Balance)
                return "Saldo insuficiented";

            dailyLimit += monto;
            Balance -= monto;
            bankingHistory.Add(new Transaccion(Transaccion.transactionType.withdraw, monto));

            return $"Retiro exitoso. Nuevo saldo: ₡{Balance:N2}";
        }
    }
}
