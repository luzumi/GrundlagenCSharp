using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Spielfeld
    {
        private bool[,] _fieldTrue;
        private bool[,] _fieldFalse;
        private bool _fieldToRead;
        Random rand = new Random();

        public bool[,] Field
        {
            get => _fieldToRead ? _fieldTrue : _fieldFalse;
        }

        public bool[,] FieldFalse
        {
            get => _fieldFalse;
            set => _fieldFalse = value;
        }

        public bool FieldToRead
        {
            get => _fieldToRead;
            set => _fieldToRead = value;
        }


        public Spielfeld((int x, int y) pSize)
        {
            _fieldTrue = new bool[pSize.x, pSize.y];
            _fieldFalse = new bool[pSize.x, pSize.y];
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
                for (int column = 0; column < 3; column++)
                {
                    Field[column, row] = false; //(rand.NextDouble() > 0.8);
                    Field[1, 1] = true;
                    Field[1, 2] = true;
                    Field[1, 3] = true;
                    Field[2, 1] = true;
                    Field[1, 4] = true;
                    Field[2, 4] = true;
                    Field[5, 4] = true;
                    Field[4, 4] = true;
                }
            }
        }


        public void Update()
        {
            bool[,] arrayToDraw = Field;

            for (int row = 0; row < Field.GetLength(1); row++)
            {
                for (int column = 0; column < Field.GetLength(0); column++)
                {
                    if (CountNeighbours(column, row) < 2 ||
                        CountNeighbours(column, row) > 3)
                    {
                        arrayToDraw[column, row] = false;
                    }
                    else if (CountNeighbours(column, row) == 3)
                    {
                        arrayToDraw[column, row] = true;
                    }
                }
            }
        }


        public bool LoadGame(string pFileName)
        {
            return false;
        }


        public bool SaveGame(string pFileName)
        {
            return false;
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
            //Console.WriteLine(Field[column,row]);
            if (Field[CheckRow(column - 1), CheckColumn(row - 1)] == true) { neighbours++; }
            //Console.WriteLine(Field[CheckRow(column - 1), CheckColumn(row - 1)]);

            if (Field[CheckRow(column - 1), CheckColumn(row)] == true) { neighbours++; }
            //Console.WriteLine(Field[CheckRow(column - 1), CheckColumn(row)]);

            if (Field[CheckRow(column - 1), CheckColumn(row + 1)] == true) { neighbours++; }
            //Console.WriteLine(Field[CheckRow(column - 1), CheckColumn(row + 1)]);

            if (Field[CheckRow(column), CheckColumn(row - 1)] == true) { neighbours++; }
            //Console.WriteLine(Field[CheckRow(column), CheckColumn(row - 1)]);

            if (Field[CheckRow(column), CheckColumn(row + 1)] == true) { neighbours++; }
            //Console.WriteLine(Field[CheckRow(column ), CheckColumn(row + 1)]);

            if (Field[CheckRow(column + 1), CheckColumn(row - 1)] == true) { neighbours++; }
            //Console.WriteLine(Field[CheckRow(column + 1), CheckColumn(row - 1)]);

            if (Field[CheckRow(column + 1), CheckColumn(row)] == true) { neighbours++; }
            //Console.WriteLine(Field[CheckRow(column + 1), CheckColumn(row )]);

            if (Field[CheckRow(column + 1), CheckColumn(row + 1)] == true) { neighbours++; }
            //Console.WriteLine(Field[CheckRow(column + 1), CheckColumn(row + 1)]);

            return neighbours;
        }
    }
}
