using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Label : IDrawable
    {
        readonly byte posY;
        private bool centered;
        private List<string> text;

        public Label(byte Row, bool Centered, List<string> Text)
        {
            posY = Row;
            centered = Centered;
            text = Text;
        }

        public void Draw()
        {
            Console.ResetColor();
            for (int row = 0; row < text.Count; row++)
            {
                Console.SetCursorPosition( (centered? Console.WindowWidth / 2 - text[row].Length / 2 : 2), posY + row);
                Console.Write(text[row]);
            }
        }
    }
}
