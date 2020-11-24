using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        public static Stack<Scene> Scenes = new Stack<Scene>(4);

        public static bool running = true;

        static void Main()
        {
            Console.CursorVisible = false;
            Scenes.Push( new Intro());

            Console.CursorVisible = !running;
            do
            {
                Scenes.Peek().Update();
                //Thread.Sleep(300);

            } while (running);
        }



        public static string Logo()
        {
            return "     ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████    █████▒    ██▓     ██▓  █████▒▓█████ \n" +
                   "   ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██   ▒    ▓██▒    ▓██▒▓██   ▒ ▓█   ▀ \n" +
                   "  ▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒▒████ ░    ▒██░    ▒██▒▒████ ░ ▒███   \n" +
                   "  ░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░░▓█▒  ░    ▒██░    ░██░░▓█▒  ░ ▒▓█  ▄ \n" +
                   "  ░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░░▒█░       ░██████▒░██░░▒█░    ░▒████▒\n" +
                   "   ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░  ▒ ░       ░ ▒░▓  ░░▓   ▒ ░    ░░ ▒░ ░\n" +
                   "    ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░  ░         ░ ░ ▒  ░ ▒ ░ ░       ░ ░  ░\n" +
                   "  ░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒   ░ ░         ░ ░    ▒ ░ ░ ░       ░   \n" +
                   "        ░       ░  ░       ░      ░  ░       ░ ░                 ░  ░ ░             ░  ░\n" +
                   "                                                                                        ";
        }
    }
}
