using System;
using System.Collections.Generic;
using System.Text;

namespace Übungesaufgaben
{
    class Timer
    {
        public int Counter { get; set; }
        public TimeSpan TimerNow { get; set; }

        public static void FpsChecker()
        {
            Timer timer = new Timer();
            DateTime now = new DateTime();
            DateTime start = new DateTime();
            int counter = 0;

            do
            {
                timer.TimerNow = DateTime.Now - now;

                timer.Counter++;
                
                if (DateTime.Now - now >= TimeSpan.FromSeconds(1))
                {
                    counter++;
                    if (counter == 3)
                    {
                        Console.SetCursorPosition(Console.BufferWidth - 10, 0);
                        Console.WriteLine(timer.Counter / 3);
                        counter = 0;
                    }
                    else
                    {
                        timer.Counter /= counter;
                    }

                    timer.Counter = 0;

                    timer.TimerNow = new TimeSpan();

                    now = DateTime.Now;
                }
            } while (!Console.KeyAvailable);
        }
    }
}
