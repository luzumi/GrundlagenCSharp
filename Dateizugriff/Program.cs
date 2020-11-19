using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Dateizugriff
{
    [Serializable]
    class Auto
    {
        public int Tempo;
        public string Name;
        [NonSerialized] public bool geheim;
    }


    public class Hund
    {
        public string Name;
        public byte Alter;
        public List<Bein> Beine;
    }

    public class Bein
    {
        public byte AnzahlKnochen;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //SaveAndLoadXML();
            LoadText();
        }


        /// <summary>
        /// liest kompletten Inhalt der Textdatei ein
        /// </summary>
        private static void LoadText()
        {
            using (var reader = new StreamReader("BeispielText.txt"))
            {
                var dateiInhalt = reader.ReadToEnd();
                Console.WriteLine(dateiInhalt);
            }

            Console.ReadLine();
            Console.Clear();

            using (var reader = new StreamReader("BeispielText.txt"))
            {
                string zeile;
                while ((zeile = reader.ReadLine()) is not null)
                {
                    Console.WriteLine(zeile);
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Ablauf des Speicherns eines Hund(Objekt) in eine XML Datei .xml
        /// XMLSerializer braucht kein [Serializable] aber die Klasse, die gespeichert werden soll, muss public sein
        /// </summary>
        static void SaveAndLoadXML()
        {
            Hund meinHund = new Hund();
            Hund andererHund = new Hund() {Alter = 5, Name = "dodo"};
            meinHund.Alter = 5;
            meinHund.Name = "Wuffi";
            meinHund.Beine = new List<Bein>();
            meinHund.Beine.Add(new Bein());
            meinHund.Beine.Add(new Bein());
            meinHund.Beine.Add(new Bein());
            meinHund.Beine.Add(new Bein());

            XmlSerializer formXML = new XmlSerializer(typeof(Hund));

            using (Stream datei = new FileStream("GespeicherterHund.xml", FileMode.Open, FileAccess.Write))
            {
                formXML.Serialize(datei, meinHund);
            }

            Console.WriteLine("Hund gespeichert");

            Hund meinGeladenerHund;
            using (Stream myFileStream = new FileStream("GespeicherterHund.xml", FileMode.Open, FileAccess.Read))
            {
                meinGeladenerHund = (Hund)formXML.Deserialize(myFileStream);
            }

            if (meinGeladenerHund.Name == meinHund.Name && meinGeladenerHund.Alter == meinHund.Alter)
            {
                Console.WriteLine("Der Hund wurde erfolgreich geladen {0}", meinGeladenerHund.Name);
            }
        }


        /// <summary>
        /// Ablauf des Speicherns eines Autos(Objekt) in eine binary Datei .bin
        /// BinaryFormatter speichert auch non-public klassen, die zu speichernde Klasse benötigt parameter [Serializable]
        /// </summary>
        static void SaveAndLoadBinary()
        {
            Auto myCar = new Auto();
            myCar.Name = "Ferrari Maranello";
            myCar.Tempo = 320;
            myCar.geheim = true;

            BinaryFormatter formBin = new BinaryFormatter();

            using (Stream myFileStream = new FileStream("GespeichertesAuto.bin", FileMode.Create, FileAccess.Write))
            {
                formBin.Serialize(myFileStream, myCar);
            }

            Console.WriteLine("Das Auto wurde gespeichert");

            Auto meinGeladenesAuto;
            using (Stream myFileStream = new FileStream("GespeichertesAuto.bin", FileMode.Open, FileAccess.Read))
            {
                meinGeladenesAuto = (Auto)formBin.Deserialize(myFileStream);
            }

            if (meinGeladenesAuto.Name == myCar.Name && meinGeladenesAuto.Tempo == myCar.Tempo)
            {
                Console.WriteLine("Das Auto wurde erfolgreich geladen {0}", meinGeladenesAuto.geheim);
            }
        }
    }
}
