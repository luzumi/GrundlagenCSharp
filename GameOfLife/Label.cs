using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Label : IDrawable
    {
        private byte row;
        private bool centered;
        private List<string> text;

        public Label(byte Row, bool Centered, List<string> Text)
        {
            row = Row;
            centered = Centered;
            text = Text;
        }

        public void Draw()
        {
        }
    }
}
