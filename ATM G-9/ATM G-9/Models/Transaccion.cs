using static System.Net.Mime.MediaTypeNames;

namespace ATM_G_9.Models
{
    public class Transaccion
    {
        public enum transactionType
        {
            deposit,
            withdraw
        }
        public transactionType Type { get; set; }
        private decimal _amount { get; set; }
        private DateTime date { get; set; }

        public Transaccion(transactionType type, decimal amount)
        {
            Type = type;
            _amount = amount;
            date = DateTime.Now;

        }

       
    }
}
