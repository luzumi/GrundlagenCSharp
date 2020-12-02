using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace GameOfLifeLogic
{
    public class GameLogic
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
        public void SaveGameXml(string pFileName)
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

            try
            {
                sg.Field = convertedField;
                sg.fileText = (pFileName + DateTime.Now).Replace('.', '_').Replace(':', '-');

                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));

                using (Stream file = new FileStream(sg.fileText + ".xml", FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(file, sg);
                }
            }
            catch (XmlException)
            {
                throw new XmlException();
            }
        }


        /// <summary>
        /// Speichert aktuelles Spielfeld als .gol datei im Ascii-Format
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>ErfolgsStatus</returns>
        public void SaveGameTxt(string pFileName)
        {
            try
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
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }


        /// <summary>
        /// schreibt aktuelles Spielfeld im AsciiFormat in eine .gol datei
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="sg"></param>
        /// <param name="convertedField"></param>
        private void WriteTextToFile(string pFileName, SaveGame sg, List<List<bool>> convertedField)
        {
            try
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
            catch (Exception)
            {
                throw new Exception();
            }
        }


        /// <summary>
        /// Lädt SaveGameTxt in Spielfeld ein
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>ErfolgsStatus</returns>
        public void LoadGame(string pFileName)
        {
            try
            {
                if (!File.Exists(pFileName))
                {
                    LoadGameDatabase(pFileName);
                }

                ReadSaveGameFromFile(pFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// Einlesen der Daten aus dem SavGameFile
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>ErfolgsStatus</returns>
        private void ReadSaveGameFromFile(string pFileName)
        {
            string text;

            try
            {
                using (var reader = new StreamReader(pFileName))
                {
                    text = reader.ReadLine();

                    if (text is not null)
                    {
                        switch (text.Substring(0, 4))
                        {
                            case "GOLA":
                                if (!SetNewFieldSize(text)) { }
                                else FillTxtSaveToGameBoard(text);

                                break;

                            case "<?xm":
                                LoadGameXml(pFileName);
                                break;

                            case "GOLB":
                                LoadGameBinary(pFileName);
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }


        /// <summary>
        /// beschreibt SPielfeld mit Werten aus dem SaveGameTxt
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private void FillTxtSaveToGameBoard(string text)
        {
            //liest jede Zahl und wandelt in true/false um und schreibt das in FieldFalse
            try
            {
                for (int row = 0; row < FieldFalse.GetLength(0); row++)
                {
                    for (int col = 8; col < FieldFalse.GetLength(1); col += 2)
                    {
                        if (!byte.TryParse(text.Substring(col + row * FieldFalse.GetLength(1), 1),
                            out byte positionInText))
                        {
                            FieldFalse[row, col] = text[positionInText] == '1';
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }


        /// <summary>
        /// Prüft Inhalt der Datei auf gültige Einträge und setz die Größe Des Fields
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Erfolgsstatus</returns>
        private bool SetNewFieldSize(string text)
        {
            try
            {
                if (!byte.TryParse(text.Substring(4, 2), out byte y)) return false;
                if (!byte.TryParse(text.Substring(6, 2), out byte x)) return false;

                FieldFalse = new bool[y, x];
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public void SaveGame(string pFilename, Enum pSaveGameVariante)
        {
            try
            {
                switch (pSaveGameVariante)
                {
                    case SaveGameVariante.Text:
                        SaveGameTxt(pFilename);
                        break;

                    case SaveGameVariante.Xml:
                        SaveGameXml(pFilename);
                        break;

                    case SaveGameVariante.Binary:
                        SaveGameBinary(pFilename);
                        break;

                    case SaveGameVariante.Database:
                        SaveGameDatabase(pFilename);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// Lädt ein SaveGameTxt aus einer xml Datei
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns>Erfolgsstatus</returns>
        public void LoadGameXml(string pFileName)
        {
            try
            {
                File.Exists(pFileName);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }

            try
            {
                var convertedField = ReadXmlFromFile(pFileName);

                FieldFalse = convertedField;
            }
            catch (Exception)
            {
                throw new Exception();
            }
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


        private void LoadGameBinary(string FileName)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.OpenRead(FileName)))
                {
                    reader.ReadChars(4); // überspringen der bereits geprüften magic number
                    byte Y = reader.ReadByte();
                    byte X = reader.ReadByte();

                    _fieldFalse = new bool[Y, X];
                    _fieldTrue = new bool[Y, X];

                    byte[] bytes = reader.ReadBytes((Y * X - 1) / 8 + 1);
                    BitArray bits = new BitArray(bytes);

                    for (int row = 0; row < Field.GetLength(0); row++)
                    {
                        for (int col = 0; col < Field.GetLength(1); col++)
                        {
                            _fieldFalse[row, col] = bits[row * Field.GetLength(1) + col];
                        }
                    }
                }
            }
            catch (ArgumentException)
            {
                throw new ArgumentException();
            }
        }


        private void SaveGameBinary(string FileName)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(FileName + ".gol", FileMode.Create)))
                {
                    writer.Write("GOLB".ToCharArray());
                    writer.Write((byte)Field.GetLength(0));
                    writer.Write((byte)Field.GetLength(1));

                    byte[] bytes = new byte[Field.Length];
                    bytes = WriteFieldToByteArray(bytes);

                    writer.Write(bytes);
                }
            }
            catch (ArgumentException)
            {
                throw new ArgumentException();
            }
        }


        private byte[] WriteFieldToByteArray(byte[] pBytes)
        {
            BitArray bits = new BitArray(Field.GetLength(0) * Field.GetLength(1));
            for (int row = 0;
                row < Field.GetLength(0);
                row++)
            {
                for (int col = 0; col < Field.GetLength(1); col++)
                {
                    bits[row * Field.GetLength(1) + col] = Field[row, col];
                }
            }

            pBytes = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(pBytes, 0);
            return pBytes;
        }


        private void LoadGameDatabase(string pFileName)
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.Version = 3;
            builder.DataSource = "SaveGames.db";
            try
            {
                File.Exists(builder.DataSource);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException();
            }

            int y;
            int x;
            SQLiteBlob blob;

            byte[] byteArray;
            try

            {
                using (SQLiteConnection connection = new SQLiteConnection(builder.ToString()))
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = "select Height, Width, Field from SaveGames where Name = @name;";
                    command.Parameters.AddWithValue("@name", pFileName);

                    try
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                        {
                            if (reader.Read())
                            {
                                //reader.Read(); // nur die erste zeile der rückgabe.
                                y = reader.GetInt32(0); // spalte 0 = height
                                x = reader.GetInt32(1); // spalte 1 = width
                                using (blob = reader.GetBlob(2, true)) // spalte 2 = field
                                {
                                    byteArray = new byte[blob.GetCount()];
                                    blob.Read(byteArray, blob.GetCount(), 0);
                                }

                                BitArray bits = new BitArray(byteArray);

                                _fieldFalse = new bool[y, x];
                                _fieldTrue = new bool[y, x];
                                _fieldToRead = false;
                                for (int row = 0; row < y; row++)
                                {
                                    for (int col = 0; col < x; col++)
                                    {
                                        _fieldFalse[row, col] = bits[row * x + col];
                                    }
                                }
                            }
                        }
                    }
                    catch (SQLiteException)
                    {
                        throw new SQLiteException();
                    }
                }
            }
            catch (SQLiteException)
            {
                throw new SQLiteException();
            }
        }


        private void SaveGameDatabase(string pFileName)
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.Version = 3;
            builder.DataSource = "SaveGames.db";
            try
            {
                if (!File.Exists(builder.DataSource)
                ) // wenn die Datenbank noch nicht existiert soll sie erstellt werden
                {
                    // bei sqlite ist das erstellen der datenbank, falls sie noch nicht exisitert ok
                    // NIEMALS bei Datenbankservern! Nie! Nada! NULL! VOID! Gar nich! 404! Nööööö!
                    using (SQLiteConnection connection = new SQLiteConnection(builder.ToString()))
                    {
                        connection
                            .Open(); // Open erstellt automatisch die datenbank wenn sie nicht da ist, es fehlen nur die tabellen.
                        SQLiteCommand command = connection.CreateCommand();
                        command.CommandText =
                            "create table SaveGames (ID integer not null primary key, Name varchar(15) not null unique , Height integer not null, Width integer not null, Field blob not null);";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException)
            {
                throw new SQLiteException();
            }

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(builder.ToString()))
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();

                    byte[] bytes = new byte[Field.Length];

                    bytes = WriteFieldToByteArray(bytes);

                    command.CommandText =
                        "replace into SaveGames (Name, Height, Width, Field) values (@name, @height, @width, @field)";

                    command.Parameters.AddWithValue("@name", pFileName);
                    command.Parameters.AddWithValue("@height", Field.GetLength(0));
                    command.Parameters.AddWithValue("@width", Field.GetLength(1));
                    command.Parameters.AddWithValue("@field", bytes);

                    int linesAffected = command.ExecuteNonQuery();
                    if (linesAffected == 0)
                    {
                        throw new IOException();
                    }
                }
            }
            catch (SQLiteException)
            {
                throw new SQLiteException();
            }
        }


        public static List<(string Name, bool FromDatabase)> GetAvailableGames()
        {
            List<string> fileNames = new List<string>();
            fileNames.AddRange(Directory.GetFiles(@".\", "*.xml"));
            fileNames.AddRange(Directory.GetFiles(@".\", "*.gol"));
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.Version = 3;
            builder.DataSource = "SaveGames.db";

            List<(string Name, bool FromDatabase)> result = new List<(string Name, bool FromDatabase)>();
            foreach (var item in fileNames)
            {
                result.Add((item, false));
            }

            try
            {
                if (File.Exists(builder.DataSource))
                {
                    using (SQLiteConnection connection = new SQLiteConnection(builder.ToString()))
                    {
                        connection.Open();
                        SQLiteCommand command = connection.CreateCommand();
                        command.CommandText = "select Name from SaveGames order by Name asc;";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add((reader.GetString(0), true));
                            }
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                throw new SQLiteException();
            }

            return result;
        }
    }
}
