using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Channels;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(60,25);
            Console.SetCursorPosition(9,0);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("TIC TAC TOE");

            
            
            Spielfeld s = new Spielfeld();

            for (int i = 1; i <= s.PlayerNames.Length; i++)
            {
                Console.SetCursorPosition(9,4+i);
                Console.Write("Spieler{0}, geben Sie Ihren Namen ein:", i);
                s.PlayerNames[i-1] = Console.ReadLine();
            }
            s.ResetBoard();

            Console.SetCursorPosition(12,12);    

            Draw(s.Board, s);


            Console.ReadKey(true);
            Console.Clear();
        }

        static void Draw(FieldState[,] pFieldStates, Spielfeld s)
        {
            byte fieldX = 0;
            byte fieldY = 0;

            s.GetBoard();

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    //s.Turn(s.buttons[fieldX + 1, fieldY + 1](new Point(fieldX, fieldY)));
                    break;

                case ConsoleKey.LeftArrow:
                    
                    break;
                case ConsoleKey.UpArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.DownArrow:
                    break;
                case ConsoleKey.Help:
                    break;


                case ConsoleKey.Escape:
                    break;

                default:
                    //throw new ArgumentOutOfRangeException();
                    break;
            }
        }


        void Draw(Spielfeld s)

        {
            for (int xAchse = 0; xAchse < 3; xAchse++)
            {
                for (int yAchse = 0; yAchse < 3; yAchse++)
                {
                    switch (s.Board[xAchse, yAchse])
                    {
                        case FieldState.Empty:
                            Console.Write("- ");
                            break;

                        case FieldState.X:
                            Console.Write("X ");
                            break;

                        case FieldState.O:
                            Console.Write("O ");
                            break;

                        case FieldState.Hint:
                            Console.Write("+ ");
                            break;

                        default:
                            break;
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
