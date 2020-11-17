using System;


namespace TicTacToe
{
    public class Timer
    {
        DateTime lastUpdate;
        uint framesSinceLastUpdate;

        public Timer()
        {
            lastUpdate = DateTime.Now;
            framesSinceLastUpdate = 0;
        }
        public void FpsChecker()
        {
            framesSinceLastUpdate++;
            if ((DateTime.Now - lastUpdate).TotalMilliseconds >= 5000)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(Console.BufferWidth - 15, 0);
                Console.Write(@"{0,8} fps", framesSinceLastUpdate/5);
                framesSinceLastUpdate = 0;
                lastUpdate = DateTime.Now;
                Console.ResetColor();
            }
        }
    }
}
