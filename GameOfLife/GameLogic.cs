using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
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
        public static (int x, int y) size = (70, 35);
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
            set => _fieldFalse = value;
        }


        public GameLogic((int x, int y) pSize)
        {
            _fieldTrue = new bool[pSize.x, pSize.y];
            _fieldFalse = new bool[pSize.x, pSize.y];
            _fieldToRead = false;
            puffer = new bool[_fieldFalse.GetLength(0), _fieldFalse.GetLength(1)];
            Reset(0);
        }

        public GameLogic(int template)
        {
            _fieldFalse = new bool[size.x, size.y];
            _fieldTrue = new bool[size.x, size.y];
            _fieldToRead = false;
            puffer = new bool[_fieldFalse.GetLength(0), _fieldFalse.GetLength(1)];
            Reset(template);
        }

        public bool GetActiveField()
        {
            return _fieldToRead;
        }


        public void Reset(int pChoice)
        {
            for (int row = 0; row < Field.GetLength(1); row++)
            {
                for (int column = 0; column < Field.GetLength(0); column++)
                {
                    Field[column, row] = false; //(rand.NextDouble() > 0.9);
                }
            }
        }

        #region FensterMuster-Vorlage

        public void GosperGliderGun(bool[,] pField)
        {
            pField[25, 16] = true;
            pField[25, 17] = true;
            pField[35, 13] = true;
            pField[35, 14] = true;
            pField[36, 13] = true;
            pField[36, 14] = true;
            pField[22, 15] = true;
            pField[23, 12] = true;
            pField[23, 16] = true;
            pField[25, 11] = true;
            pField[25, 12] = true;
            pField[21, 13] = true;
            pField[21, 14] = true;
            pField[21, 15] = true;
            pField[22, 13] = true;
            pField[22, 14] = true;
            pField[1, 15] = true;
            pField[2, 15] = true;
            pField[1, 16] = true;
            pField[2, 16] = true;
            pField[11, 15] = true;
            pField[11, 16] = true;
            pField[11, 17] = true;
            pField[12, 14] = true;
            pField[13, 13] = true;
            pField[14, 13] = true;
            pField[12, 18] = true;
            pField[13, 19] = true;
            pField[14, 19] = true;
            pField[15, 16] = true;
            pField[16, 14] = true;
            pField[17, 15] = true;
            pField[17, 16] = true;
            pField[17, 17] = true;
            pField[18, 16] = true;
            pField[16, 18] = true;
        }

        public void Window(bool[,] pField)
        {
            pField[15, 11] = true;

            pField[15, 12] = true;

            pField[15, 13] = true;

            pField[15, 14] = true;

            pField[15, 15] = true;

            pField[15, 16] = true;

            pField[15, 17] = true;

            pField[15, 18] = true;

            pField[15, 19] = true;

            pField[15, 20] = true;

            pField[15, 21] = true;

            pField[15, 22] = true;

            pField[15, 23] = true;

            pField[20, 11] = true;

            pField[20, 12] = true;

            pField[20, 13] = true;

            pField[20, 14] = true;

            pField[20, 15] = true;

            pField[20, 16] = true;

            pField[20, 17] = true;

            pField[20, 18] = true;

            pField[20, 19] = true;

            pField[20, 20] = true;

            pField[20, 21] = true;

            pField[20, 22] = true;

            pField[20, 23] = true;

            pField[25, 11] = true;

            pField[25, 12] = true;

            pField[25, 13] = true;

            pField[25, 14] = true;

            pField[25, 15] = true;

            pField[25, 16] = true;

            pField[25, 17] = true;

            pField[25, 18] = true;

            pField[25, 19] = true;

            pField[25, 20] = true;

            pField[25, 21] = true;

            pField[25, 22] = true;

            pField[25, 23] = true;

            pField[16, 11] = true;

            pField[17, 11] = true;

            pField[18, 11] = true;

            pField[19, 11] = true;

            pField[20, 11] = true;

            pField[21, 11] = true;

            pField[22, 11] = true;

            pField[23, 11] = true;

            pField[24, 11] = true;

            pField[25, 11] = true;

            pField[25, 11] = true;

            pField[16, 17] = true;

            pField[17, 17] = true;

            pField[18, 17] = true;

            pField[19, 17] = true;

            pField[20, 17] = true;

            pField[21, 17] = true;

            pField[22, 17] = true;

            pField[23, 17] = true;

            pField[24, 17] = true;

            pField[25, 17] = true;

            pField[16, 23] = true;

            pField[17, 23] = true;

            pField[18, 23] = true;

            pField[19, 23] = true;

            pField[20, 23] = true;

            pField[21, 23] = true;

            pField[22, 23] = true;

            pField[23, 23] = true;

            pField[24, 23] = true;

            pField[25, 23] = true;
        }

        #endregion

        public void Update()
        {
            for (int row = 0; row < _fieldFalse.GetLength(1); row++)
            {
                for (int column = 0; column < _fieldFalse.GetLength(0); column++)
                {
                    puffer[column, row] = Field[column, row];
                    if (CountNeighbours(column, row) < 2 ||
                        CountNeighbours(column, row) > 3)
                    {
                        puffer[column, row] = false;
                    }
                    else if (CountNeighbours(column, row) == 3)
                    {
                        puffer[column, row] = true;
                    }
                    else
                    {
                        puffer[column, row] = Field[column, row];
                    }
                }
            }

            _fieldToRead = !_fieldToRead;

            for (int row = 0; row < Field.GetLength(1); row++)
            {
                for (int column = 0; column < Field.GetLength(0); column++)
                {
                    Field[column, row] = puffer[column, row];
                }
            }
        }


        public bool SaveGame(string pFileName)
        {
            SaveGame sg = new SaveGame();
            List<List<bool>> convertedField = new List<List<bool>>();

            for (int row = 0; row < Field.GetLength(0); row++)
            {
                convertedField.Add(new List<bool>(Field.GetLength(0)));

                for (int column = 0; column < Field.GetLength(1); column++)
                {
                    convertedField[row].Add(Field[row, column]);
                }
            }

            sg.Field = convertedField;
            sg.fileName = pFileName + DateTime.Now.ToString();

            XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));

            using (Stream file = new FileStream(pFileName, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(file, sg);
                
            }

            return true;
        }

        public bool LoadGame(string pFileName)
        {
            if (!File.Exists(pFileName))
            {
                return false;
            }

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

            FieldFalse = convertedField;

            return true;
        }

        /// <summary>
        /// Überlaufschutz,
        /// wenn 0 dann setzte auf max,
        /// wenn max dann setzte auf 0
        /// </summary>
        /// <param name="pRow"></param>
        /// <returns></returns>
        private int CheckRow(int pRow)
        {
            if (pRow < 0) { return Field.GetLength(0) - 1; }

            if (pRow > Field.GetLength(0) - 1) { return 0; }

            return pRow;
        }


        /// <summary>
        /// Überlaufschutz,
        /// wenn 0 dann setzte auf max,
        /// wenn max dann setzte auf 0
        /// </summary>
        /// <param name="pColumn"></param>
        /// <returns></returns>
        private int CheckColumn(int pColumn)
        {
            if (pColumn < 0) { return Field.GetLength(1) - 1; }

            if (pColumn > Field.GetLength(1) - 1) { return 0; }

            return pColumn;
        }

        private int CountNeighbours(int column, int row)
        {
            int neighbours = 0;
            if (Field[CheckRow(column - 1), CheckColumn(row - 1)] == true) { neighbours++; }

            if (Field[CheckRow(column - 1), CheckColumn(row)] == true) { neighbours++; }

            if (Field[CheckRow(column - 1), CheckColumn(row + 1)] == true) { neighbours++; }

            if (Field[CheckRow(column), CheckColumn(row - 1)] == true) { neighbours++; }

            if (Field[CheckRow(column), CheckColumn(row + 1)] == true) { neighbours++; }

            if (Field[CheckRow(column + 1), CheckColumn(row - 1)] == true) { neighbours++; }

            if (Field[CheckRow(column + 1), CheckColumn(row)] == true) { neighbours++; }

            if (Field[CheckRow(column + 1), CheckColumn(row + 1)] == true) { neighbours++; }

            return neighbours;
        }
    }
}
