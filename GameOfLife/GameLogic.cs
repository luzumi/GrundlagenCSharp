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
        private bool[,] _fieldTrue;
        private bool[,] _fieldFalse;
        private bool _fieldToRead;
        public Random rand = new Random();
        bool[,] puffer;
        public List<SaveGame> savegames;

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
            puffer = new bool[_fieldFalse.GetLength(0),_fieldFalse.GetLength(1)];
            savegames = new List<SaveGame>();
            Reset();
        }

        public bool GetActiveField()
        {
            return _fieldToRead;
        }


        public void Reset()
        {
            for (int row = 0; row < Field.GetLength(1); row++)
            {
                for (int column = 0; column < Field.GetLength(0); column++)
                {
                    Field[column, row] = false;//(rand.NextDouble() > 0.9);

                    #region FensterMuster-Vorlage

                    //Window();
                    //GosperGliderGun();

                    #endregion
                }
            }
        }

        private void GosperGliderGun()
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

        private void Window()
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
            List<List<bool>> convertedField = new List<List<bool>>();
            SaveGame sg = new SaveGame();

            for (int row = 0; row < Field.GetLength(0); row++)
            {
                convertedField.Add(new List<bool>(Field.GetLength(0)));

                for (int column = 0; column < Field.GetLength(1); column++)
                {
                    convertedField[row].Add(Field[row, column]);
                } 
            }

            sg.Field = convertedField;
            sg.fileName = pFileName + DateTime.Now.ToShortDateString() + ".xml";

            XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));

            using (Stream file = new FileStream(pFileName, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(file, sg);
                savegames.Add(sg);
            }

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
