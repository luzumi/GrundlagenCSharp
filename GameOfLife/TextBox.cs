using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace GameOfLife
{
    class TextBox : UiElement
    {
        
        private readonly ConsoleColor color;
        private byte cursorPosition;
        private string fileName;

        public string Text
        {
            get { return content.ToString(); }
        }

        public string FileName => fileName;


        public TextBox(byte Row, bool Centered, string Text = "") : base(Row, Centered)
        {
            content = new char[40];
            byte count = 0;
            fileName = Text;

            for (; count < Text.Length && count < content.Length; count++)
            {
                content[count] = Text[count];
            }
            cursorPosition = count;
            for (; count < content.Length; count++)
            {
                content[count] = ' ';
            }

            color = colorUnSelected;

            SetColors();

            OnStateChanged = StateChanged;

            
            State = ButtonStates.Available;
            
        }

        public override void Draw()
        {
            Console.SetCursorPosition((center ? Console.WindowWidth / 2 - content.Length / 2 - 1 : 2), row);
            Console.ForegroundColor = states == ButtonStates.Available? colorUnSelected : colorSelected;
            for (int counter = 0; counter < content.Length; counter++)
            {
                Console.BackgroundColor = (counter != cursorPosition? ConsoleColor.Black: ConsoleColor.DarkBlue);
                Console.Write(content[counter]);
            }
        }

        public override void ProcessKey(ConsoleKeyInfo KeyInformation)
        {
            switch (KeyInformation.Key)
            {
                case ConsoleKey.Delete:
                    Array.Fill(content, ' ');
                    Program.NeedsRedraw.Add(this);
                    break;
                case ConsoleKey.Backspace:
                    content[cursorPosition--] = ' ';
                    Program.NeedsRedraw.Add(this);
                    break;
                case ConsoleKey.LeftArrow:
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                    }
                    Program.NeedsRedraw.Add(this);
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorPosition < content.Length - 1)
                    {
                        cursorPosition++;
                    }
                    Program.NeedsRedraw.Add(this);
                    break;
                case ConsoleKey.F2:
                    string newName = "";
                    foreach (char letter in content)
                    {
                        newName += letter;
                    }
                    RenameFile(FileName, newName);
                    break;
                default:
                    if (KeyInformation.KeyChar is >= 'A' and <= 'Z' ||
                        KeyInformation.KeyChar is >= 'a' and <= 'z' ||
                        KeyInformation.KeyChar is >= '0' and <= '9')
                    {
                        content[cursorPosition++] = KeyInformation.KeyChar;
                        Program.NeedsRedraw.Add(this);
                    }
                    break;
                
            }
        }


        private static void RenameFile(string path, string newName)
        {
            var fileInfo = new FileInfo(@".\\" + path);
            File.Move(path, fileInfo.Directory + @".\\" + newName);
        }
    }
}


