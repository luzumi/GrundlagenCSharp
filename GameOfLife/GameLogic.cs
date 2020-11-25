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
        public static (int col, int row) size = (70, 40);
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


        public GameLogic((int col, int row) pSize)
        {
            _fieldTrue = new bool[pSize.row, pSize.col];
            _fieldFalse = new bool[pSize.row, pSize.col];
            _fieldToRead = false;
            puffer = new bool[_fieldFalse.GetLength(0), _fieldFalse.GetLength(1)];
            Reset(0);
        }

        public GameLogic(int template)
        {
            _fieldFalse = new bool[size.row, size.col];
            _fieldTrue = new bool[size.row, size.col];
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
            for (int row = 0; row < Field.GetLength(0); row++)
            {
                for (int col = 0; col < Field.GetLength(1); col++)
                {
                    Field[row, col] = false; //(rand.NextDouble() > 0.9);
                }
            }

            GosperGliderGun();
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


        public bool SaveGame(string pFileName)
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
            if( Field[CheckRow(column - 1), CheckColumn(row - 1)] ) { neighbours++; }
                
            if( Field[CheckRow(column - 1), CheckColumn(row)] ) { neighbours++; }
                
            if( Field[CheckRow(column - 1), CheckColumn(row + 1)] ) { neighbours++; }
                
            if( Field[CheckRow(column), CheckColumn(row - 1)] ) { neighbours++; }
                
            if( Field[CheckRow(column), CheckColumn(row + 1)] ) { neighbours++; }
                
            if( Field[CheckRow(column + 1), CheckColumn(row - 1)] ) { neighbours++; }
                
            if( Field[CheckRow(column + 1), CheckColumn(row)] ) { neighbours++; }
                
            if( Field[CheckRow(column + 1), CheckColumn(row + 1)] ) { neighbours++; }

            return neighbours;
        }
    }
}
