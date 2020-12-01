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

        /// <summary>
        /// returnt einen String aus CharArray content
        /// </summary>
        /// <returns>ein neuer String</returns>
        public override string ToString()
        {
            return new string(content).Trim();
        }

        public string FileName => fileName;


        public TextBox(byte Row, bool Centered, string pText = "") : base(Row, Centered)
        {
            content = new char[40];
            
            fileName = pText;

            SetContent(pText);

            color = colorUnSelected;

            SetColors();

            OnStateChanged = StateChanged;

            State = ButtonStates.Available;
        }

        public TextBox(byte pRow, bool pCentered, string pText, Action pSceneRemove) : base(pRow, pCentered)
        {
            content = new char[40];
            fileName = pText;

            SetContent(pText);

            color = colorUnSelected;

            SetColors();

            OnStateChanged = StateChanged;

            State = ButtonStates.Available;

            method = pSceneRemove;
        }

        private void SetContent(string Text)
        {
            byte count = 0;

            for (; count < Text.Length && count < content.Length; count++)
            {
                content[count] = Text[count];
            }

            cursorPosition = count;
            for (; count < content.Length; count++)
            {
                content[count] = ' ';
            }
        }

        public override void Draw()
        {
            Console.SetCursorPosition((center ? Console.WindowWidth / 2 - content.Length / 2 - 1 : 2), row);
            Console.ForegroundColor = states == ButtonStates.Available ? colorUnSelected : colorSelected;
            for (int counter = 0; counter < content.Length; counter++)
            {
                Console.BackgroundColor = (counter != cursorPosition ? ConsoleColor.Black : ConsoleColor.DarkBlue);
                Console.Write(content[counter]);
            }
        }

        public override void ProcessKey(ConsoleKeyInfo KeyInformation)
        {
            switch (KeyInformation.Key)
            {
                case ConsoleKey.Delete:
                    if (KeyInformation.Modifiers.HasFlag(ConsoleModifiers.Shift))
                    {
                        File.Delete(FileName);
                        Program.SceneRefresh(new LoadGame());
                        break;
                    }

                    content[cursorPosition] = ' ';

                    char tmp;

                    for (int i = cursorPosition; i < content.Length-1; i++)
                    {
                        tmp = content[i];
                        content[i] = content[i + 1];
                        content[i + 1] = tmp;
                    }

                    Program.NeedsRedraw.Add(this);
                    break;

                case ConsoleKey.Backspace:
                    content[--cursorPosition] = ' ';

                    for (int i = cursorPosition; i < content.Length-1; i++)
                    {
                        tmp = content[i];
                        content[i] = content[i + 1];
                        content[i + 1] = tmp;
                    }

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

                case ConsoleKey.Enter:
                    string newName = ToString(); //merkt sich den Dateinamen
                    
                    if (FileName == "Back")
                    {
                        method();
                        break;
                    }

                    if (FileName.Equals(newName.Trim()))
                    {
                        Program.SceneAdd(new Game(FileName));
                    }
                    else
                    {
                        RenameFile(FileName, newName.Trim());
                        Program.NeedsRedraw.Add(this);
                        fileName = newName.Trim();
                    }
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
