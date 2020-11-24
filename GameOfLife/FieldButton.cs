using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GameOfLife
{
    class FieldButton
    {
        protected readonly (int column, int row) Mark;

        public FieldButton((int column, int row) pMark)
        {
            Mark = pMark;
        }
    }
}
