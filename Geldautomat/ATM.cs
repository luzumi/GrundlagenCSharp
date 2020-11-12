using System.Collections.Generic;


namespace Geldautomat
{
    class ATM
    {
        readonly List<BankAccount> _bankAccounts;

        /// <summary>
        /// Creates an ATM with two sample accounts
        /// </summary>
        public ATM()
        {
            // erstellt eine liste für BankAccount und fügt direkt zwei einträge hinzu.
            // besonderheit dabei ist das keine runden klammern, sondern geschweifte
            // hinter der listenerstellung verwendet werden
            _bankAccounts =
                new List<BankAccount> {new BankAccount("hans", 1234, 5000), new BankAccount("katie", 4321, 6500)};
        }

        /// <summary>
        /// Reads the current balance of an account if login is successful
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Pin"></param>
        /// <param name="Balance"></param>
        /// <returns></returns>
        public ATMError GetBalance(string UserName, ushort Pin, out int Balance)
        {
            // testen ob der Container "bankAccounts" korrekt erstellt wurde
            // zuerst wird getestet ob bankAccounts den inhalt null enthält
            // sollte dies zutreffen wird sofort der inhalt des if ausgeführt
            // und der zweite test ignoriert
            // sollte der erste test fehlschlagen wird auch der zweite test gemacht
            // dadurch wissen wir bereits im zweiten test das das objekt existieren
            // muss und wir problemlos auf eigenschaften des objektes zugreifen können.
            if (_bankAccounts == null || _bankAccounts.Count == 0)
            {
                Balance = 0;
                return ATMError.ATMError;
            }

            // durch die liste aller BankAccounts durchlaufen
            foreach (var t in _bankAccounts)
            {
                // vergleichen ob nutzername und pin mit den hinterlegten übereinstimmt
                if (UserName == t.UserName)
                {
                    if (Pin == t.Pin)
                    {
                        // rückgabe und out füllen
                        Balance = t.Balance;
                        return ATMError.NoError;
                    }
                    else
                    {
                        // wenn keiner der Listeneinträge übereinstimmt fehler zurückgeben
                        Balance = 0;
                        return ATMError.PinError;
                    }
                }
            }

            // wenn keiner der Listeneinträge übereinstimmt fehler zurückgeben
            Balance = 0;
            return ATMError.UserError;
        }

        public ATMError Withdraw(string UserName, ushort Pin, int Amount)
        {
            // testen ob der Container "bankAccounts" korrekt erstellt wurde
            if (_bankAccounts == null || _bankAccounts.Count == 0)
            {
                return ATMError.ATMError;
            }

            // durch die liste aller BankAccounts durchlaufen
            foreach (var bac in _bankAccounts)
            {
                // vergleichen ob nutzername und pin mit den hinterlegten übereinstimmt
                if (UserName == bac.UserName)
                {
                    if (Pin == bac.Pin)
                    {
                        // rückgabe und out füllen
                        if (bac.Balance >= Amount)
                        {
                            bac.Balance -= Amount;
                            return ATMError.NoError;
                        }
                        else
                            // wenn nur ein befehl in geschweifte klammern kommen würde
                            // kann man sie weglassen. Je nach firmeninterner Konvention
                            // vielleicht nicht erlaubt. (if, while, for, do, else)
                            return ATMError.BalanceError;
                    }
                    else
                    {
                        // wenn keiner der Listeneinträge übereinstimmt fehler zurückgeben
                        return ATMError.PinError;
                    }
                }
            }

            // wenn keiner der Listeneinträge übereinstimmt fehler zurückgeben
            return ATMError.UserError;
        }
    }
}
