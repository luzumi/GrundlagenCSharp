using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Xml.Serialization;

namespace GameOfLife
{
    class GameLogic
    {
        public static (int col, int row) size = (70, 37);
        private bool[,] _fieldTrue;
        private bool[,] _fieldFalse;
        private bool _fieldToRead;
        public Random rand = new Random();
        bool[,] puffer;

        public bool[,] Field
        {
            get => _fieldToRead ? _fieldTrue : _fieldFalse;
        }

        public bool[,] FieldFalse
        {
            get => _fieldFalse;
            private set => _fieldFalse = value;
        }


        public GameLogic(int pTemplate)
        {
            _fieldFalse = new bool[size.row, size.col];
            _fieldTrue = new bool[size.row, size.col];
            _fieldToRead = false;
            puffer = new bool[_fieldFalse.GetLength(0), _fieldFalse.GetLength(1)];
            Reset(pTemplate);
        }

        /// <summary>
        /// Setzt Spielfeld zurück, alle Felder werden auf tod gesetzt
        /// Ein Auswahl vordefinierter Spielfeld ist möglich
        /// </summary>
        /// <param name="pChoice">Nummer zum auswählen eines vordefinierten Spielfelds</param>
        private void Reset(int pChoice)
        {
            for (int row = 0; row < Field.GetLength(0); row++)
            {
                for (int col = 0; col < Field.GetLength(1); col++)
                {
                    Field[row, col] = false; //(rand.NextDouble() > 0.9);
                }
            }

            if (pChoice == 1)
            {
                GosperGliderGun();
            }

            if (pChoice == 2)
            {
                Window();
            }
        }

        #region FensterMuster-Vorlage

        /// <summary>
        /// vordefiniertes Spielfeld, ein Glider
        /// </summary>
        public void GosperGliderGun()
        {
            Field[25, 16] = true;
            Field[25, 17] = true;
            Field[35, 13] = true;
            Field[35, 14] = true;
            Field[36, 13] = true;
            Field[36, 14] = true;
            Field[22, 15] = true;
            Field[23, 12] = true;
            Field[23, 16] = true;
            Field[25, 11] = true;
            Field[25, 12] = true;
            Field[21, 13] = true;
            Field[21, 14] = true;
            Field[21, 15] = true;
            Field[22, 13] = true;
            Field[22, 14] = true;
            Field[1, 15] = true;
            Field[2, 15] = true;
            Field[1, 16] = true;
            Field[2, 16] = true;
            Field[11, 15] = true;
            Field[11, 16] = true;
            Field[11, 17] = true;
            Field[12, 14] = true;
            Field[13, 13] = true;
            Field[14, 13] = true;
            Field[12, 18] = true;
            Field[13, 19] = true;
            Field[14, 19] = true;
            Field[15, 16] = true;
            Field[16, 14] = true;
            Field[17, 15] = true;
            Field[17, 16] = true;
            Field[17, 17] = true;
            Field[18, 16] = true;
            Field[16, 18] = true;
        }

        /// <summary>
        /// vordefiniertes Spielfeld, ein Fenster
        /// </summary>
        public void Window()
        {
            Field[15, 11] = true;

            Field[15, 12] = true;

            Field[15, 13] = true;

            Field[15, 14] = true;

            Field[15, 15] = true;

            Field[15, 16] = true;

            Field[15, 17] = true;

            Field[15, 18] = true;

            Field[15, 19] = true;

            Field[15, 20] = true;

            Field[15, 21] = true;

            Field[15, 22] = true;

            Field[15, 23] = true;

            Field[20, 11] = true;

            Field[20, 12] = true;

            Field[20, 13] = true;

            Field[20, 14] = true;

            Field[20, 15] = true;

            Field[20, 16] = true;

            Field[20, 17] = true;

            Field[20, 18] = true;

            Field[20, 19] = true;

            Field[20, 20] = true;

            Field[20, 21] = true;

            Field[20, 22] = true;

            Field[20, 23] = true;

            Field[25, 11] = true;

            Field[25, 12] = true;

            Field[25, 13] = true;

            Field[25, 14] = true;

            Field[25, 15] = true;

            Field[25, 16] = true;

            Field[25, 17] = true;

            Field[25, 18] = true;

            Field[25, 19] = true;

            Field[25, 20] = true;

            Field[25, 21] = true;

            Field[25, 22] = true;

            Field[25, 23] = true;

            Field[16, 11] = true;

            Field[17, 11] = true;

            Field[18, 11] = true;

            Field[19, 11] = true;

            Field[20, 11] = true;

            Field[21, 11] = true;

            Field[22, 11] = true;

            Field[23, 11] = true;

            Field[24, 11] = true;

            Field[25, 11] = true;

            Field[25, 11] = true;

            Field[16, 17] = true;

            Field[17, 17] = true;

            Field[18, 17] = true;

            Field[19, 17] = true;

            Field[20, 17] = true;

            Field[21, 17] = true;

            Field[22, 17] = true;

            Field[23, 17] = true;

            Field[24, 17] = true;

            Field[25, 17] = true;

            Field[16, 23] = true;

            Field[17, 23] = true;

            Field[18, 23] = true;

            Field[19, 23] = true;

            Field[20, 23] = true;

            Field[21, 23] = true;

            Field[22, 23] = true;

            Field[23, 23] = true;

            Field[24, 23] = true;

            Field[25, 23] = true;
        }

        #endregion

        public void Update()
        {
            for (int row = 0; row < _fieldFalse.GetLength(0); row++)
            {
                for (int col = 0; col < _fieldFalse.GetLength(1); col++)
                {
                    puffer[row, col] = Field[row, col];
                    if (CountNeighbours(row, col) < 2 ||
                        CountNeighbours(row, col) > 3)
                    {
                        puffer[row, col] = false;
                    }
                    else if (CountNeighbours(row, col) == 3)
                    {
                        puffer[row, col] = true;
                    }
                    else
                    {
                        puffer[row, col] = Field[row, col];
                    }
                }
            }

            _fieldToRead = !_fieldToRead;

            for (int row = 0; row < Field.GetLength(0); row++)
            {
                for (int col = 0; col < Field.GetLength(1); col++)
                {
                    Field[row, col] = puffer[row, col];
                }
            }
        }


        /// <summary>
        /// Speichert aktuelles Spielfeld als .gol datei im XML-Format 
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public bool SaveGameXml(string pFileName)
        {
            SaveGame sg = new SaveGame();
            List<List<bool>> convertedField = new List<List<bool>>();

            for (int row = 0; row < Field.GetLength(0); row++)
            {
                convertedField.Add(new List<bool>(Field.GetLength(1)));

                for (int col = 0; col < Field.GetLength(1); col++)
                {
                    convertedField[row].Add(Field[row, col]);
                }
            }

            sg.Field = convertedField;
            sg.fileText = (pFileName + DateTime.Now).Replace('.', '_').Replace(':', '-');

            XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));

            using (Stream file = new FileStream(sg.fileText + ".xml", FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(file, sg);
            }

            return true;
        }


        /// <summary>
        /// Speichert aktuelles Spielfeld als .gol datei im Ascii-Format
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>ErfolgsStatus</returns>
        public bool SaveGameTxt(string pFileName)
        {
            List<List<bool>> convertedField = new List<List<bool>>();
            SaveGame sg = new SaveGame();

            for (int row = 0; row < Field.GetLength(0); row++)
            {
                convertedField.Add(new List<bool>(Field.GetLength(1)));

                for (int col = 0; col < Field.GetLength(1); col++)
                {
                    convertedField[row].Add(Field[row, col]);
                }
            }

            WriteTextToFile(pFileName, sg, convertedField);

            return true;
        }


        /// <summary>
        /// schreibt aktuelles Spielfeld im AsciiFormat in eine .gol datei
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="sg"></param>
        /// <param name="convertedField"></param>
        private static void WriteTextToFile(string pFileName, SaveGame sg, List<List<bool>> convertedField)
        {
            using (StreamWriter file = new StreamWriter(pFileName + ".gol"))
            {
                sg.fileText = "GOLA" +
                              (((size.row / 10) > 0) ? size.row.ToString() : "0" + size.row) +
                              (((size.col / 10) > 0) ? size.col.ToString() : "0" + size.col);

                for (int row = 0; row < convertedField.Count; row++)
                {
                    for (int col = 0; col < convertedField[1].Count; col++)
                    {
                        sg.fileText += (convertedField[row][col] ? "1" : "0");
                    }
                }

                sg.fileText += (pFileName + DateTime.Now.ToString("D-M-HH-mm-ss")) + "\n";

                file.WriteLine(sg.fileText);
            }
        }


        //TODO: switch für verschiedene Dateitypen einbauen
        /// <summary>
        /// Lädt SaveGameTxt in Spielfeld ein
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>ErfolgsStatus</returns>
        public bool LoadGame(string pFileName, Enum pSaveGameVariante)
        {
            if (!File.Exists(pFileName))
            {
                Console.WriteLine("error 398");
                return false;
            }

            if( !ReadSaveGameFromFile(pFileName) )
            {
                Console.WriteLine("error 404");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Einlesen der Daten aus dem SavGameFile
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>ErfolgsStatus</returns>
        private bool ReadSaveGameFromFile(string pFileName)
        {
            string text;

            using (var reader = new StreamReader(pFileName))
            {
                text = reader.ReadLine();
                
                switch (text.Substring(0, 4))
                {
                    case "GOLA":
                        if (!CheckForValidTxtFileOutput(text))
                        {
                            Console.WriteLine("Error: DateiFehler.");
                            return false;
                        }
                        if (!FillTxtSaveToGameBoard(text))
                        {
                            Console.WriteLine("Error: Spielstand konnte nicht geladen werden");
                            return false;
                        }
                        break;
                    case "<?xm":
                        LoadGameXml(pFileName);
                        break;
                }
            }

            return true;
        }


        /// <summary>
        /// beschreibt SPielfeld mit Werten aus dem SaveGameTxt
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool FillTxtSaveToGameBoard(string text)
        {
            //liest jede Zahl und wandelt in true/false um und schreibt das in FieldFalse
            for (int row = 0; row < FieldFalse.GetLength(0); row++)
            {
                for (int col = 8; col < FieldFalse.GetLength(1); col++)
                {
                    if (!byte.TryParse(text.Substring(col + row * FieldFalse.GetLength(1), 1), out byte positionInText))
                        return false;

                    FieldFalse[row, col] = text[col + row * FieldFalse.GetLength(1)] == '1';
                }
            }

            return true;
        }


        /// <summary>
        /// Prüft Inhalt der Datei auf gültige Einträge
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Erfolgsstatus</returns>
        private bool CheckForValidTxtFileOutput(string text)
        {
            if (!byte.TryParse(text.Substring(4, 2), out byte y)) return false;
            if (!byte.TryParse(text.Substring(6, 2), out byte x)) return false;

            FieldFalse = new bool[y, x];

            return true;
        }


        /// <summary>
        /// Lädt ein SaveGameTxt aus einer xml Datei
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>Erfolgsstatus</returns>
        public bool LoadGameXml(string pFileName)
        {
            if (!File.Exists(pFileName))
            {
                return false;
            }

            var convertedField = ReadXmlFromFile(pFileName);

            FieldFalse = convertedField;

            return true;
        }


        /// <summary>
        /// liest xml SaveGameTxt ein und überträgt dieses in das Spielfeld
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>eingelesener Spielstand</returns>
        private static bool[,] ReadXmlFromFile(string pFileName)
        {
            XmlSerializer formXML = new XmlSerializer(typeof(SaveGame));
            SaveGame sg;

            using (var file = new StreamReader(pFileName))
            {
                sg = (SaveGame)formXML.Deserialize(file);
            }

            bool[,] convertedField = new bool[sg.Field.Count, sg.Field[0].Count];

            for (int row = 0; row < convertedField.GetLength(0); row++)
            {
                for (int col = 0; col < convertedField.GetLength(1); col++)
                {
                    convertedField[row, col] = sg.Field[row][col];
                }
            }

            return convertedField;
        }


        /// <summary>
        /// Überlaufschutz,
        /// wenn 0 dann setzte auf max,
        /// wenn max dann setzte auf 0
        /// </summary>
        /// <param name="pToCheckedNumber"></param>
        /// <param name="pDimension">row = 0, col = 1</param>
        /// <returns>gültige Zahl für Zählschleife</returns>
        private int CheckNumber(int pToCheckedNumber, int pDimension)
        {
            if (pToCheckedNumber < 0) { return Field.GetLength(pDimension) - 1; }

            if (pToCheckedNumber > Field.GetLength(pDimension) - 1) { return 0; }

            return pToCheckedNumber;
        }


        /// <summary>
        /// überprüft umliegende Felder auf true und zählt diese
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private int CountNeighbours(int column, int row)
        {
            int neighbours = 0;
            if (Field[CheckNumber(column - 1, 0), CheckNumber(row - 1, 1)]) { neighbours++; }

            if (Field[CheckNumber(column - 1, 0), CheckNumber(row, 1)]) { neighbours++; }

            if (Field[CheckNumber(column - 1, 0), CheckNumber(row + 1, 1)]) { neighbours++; }

            if (Field[CheckNumber(column, 0), CheckNumber(row - 1, 1)]) { neighbours++; }

            if (Field[CheckNumber(column, 0), CheckNumber(row + 1, 1)]) { neighbours++; }

            if (Field[CheckNumber(column + 1, 0), CheckNumber(row - 1, 1)]) { neighbours++; }

            if (Field[CheckNumber(column + 1, 0), CheckNumber(row, 1)]) { neighbours++; }

            if (Field[CheckNumber(column + 1, 0), CheckNumber(row + 1, 1)]) { neighbours++; }

            return neighbours;
        }
    }
}
