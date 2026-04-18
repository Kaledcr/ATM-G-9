using static System.Net.Mime.MediaTypeNames;

namespace ATM_G_9.Models
{
     /// <summary>
     /// Este es un movimiento financiero que se realiza dentro de una cuenta bancaria.
     /// Esta clase modela una entidad del mundo real con sus propios datos encapsulados.
     /// </summary>

    public class Transaccion
    {
        /// <summary>
        /// Definimos los atributos, como lo son el tipo de movimiento posible en el sistema,
        /// el tipo de transaccion, monto de la transaccion y la fecha y hora en la que se realizo la transaccion.
        /// </summary>
        public enum transactionType
        {
            deposit,
            withdraw
        }
        public transactionType Type { get; private set; }
        public decimal _amount { get; private set; }
        public DateTime date { get; private set; }

        //Con este constructor creamos una transaccion con sus datos completos.
        //La fecha se registra auomaticamente cuando se crea la transaccion.
        public Transaccion(transactionType type, decimal amount)
        {
            Type = type;
            _amount = amount;
            date = DateTime.Now;

        }

       
    }
}
