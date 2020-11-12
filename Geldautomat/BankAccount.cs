

namespace Geldautomat
{
    class BankAccount
    {
        // alle Variablen public damit wir sie von aussen befüllen und lesen können
        public readonly string UserName;
        public readonly ushort Pin;
        public int Balance;

        /// <summary>
        /// Constructor for a bankaccount
        /// </summary>
        /// <param name="pName">Name of Customer</param>
        /// <param name="pPin">Secret PIN of the Customer</param>
        /// <param name="pBalance">Kontostand</param>
        public BankAccount(string pName, ushort pPin, int pBalance)
        {
            UserName = pName;
            // this ist hier nötig damit der Compiler unterscheiden kann 
            // das die Klassenvariable (Pin) und nicht der Parameter (Pin) gemeint ist
            // im zweifelsfall wird die Variable genommen die am dichtesten deklariert wurde.
            this.Pin = pPin;
            this.Balance = pBalance;
        }
    }
}
