namespace ATM_G_9.Models
{
    public class User
    {
        public string Name { get; private set; }

        private string PIN { get; set; }

        public Account bankAccount { get; set; }

        public  User(string name, string PIN, decimal initialBalance)
        {
            this.Name = name;
            this.PIN = PIN;
            this.bankAccount = new Account(initialBalance);
        }


        public bool verifyPin(string pin) 
        {
        return PIN.Equals(pin);
        }
    }
}
