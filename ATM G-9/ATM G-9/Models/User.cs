namespace ATM_G_9.Models
{
    /// <summary>
    /// Representa a un cliente del banco dentro del sistema.
    /// Aqui se almacena la identidad del usuario y su cuenta bancaria asociada.
    /// Aqui cada usuario tiene una instancia y se encapsula el pin, ya que este no es accesible fuera de la clase.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Aqui se define el nombre del usuario, el PIN que este tendra para acceder al sistema y se le asocia una cuenta bancaria.
        /// </summary>
        public string Name { get; private set; }

        private string PIN { get; set; }

        public Account bankAccount { get; set; }

        public  User(string name, string PIN, decimal initialBalance)
        {
            this.Name = name;
            this.PIN = PIN;
            this.bankAccount = new Account(initialBalance);
        }

        /// <summary>
        /// Este se usa para verificar si el PIN ingresado coincide con el PIN registrado del usuario.
        /// </summary>
        /// <param name="pin">PIN ingresado por el usuario en el cajero</param>
        /// <returns>Devuelve true si el PIN es correcto y false en caso contrario.</returns>
        public bool verifyPin(string pin) 
        {
        return PIN.Equals(pin);
        }
    }
}
