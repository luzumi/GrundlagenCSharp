﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace GameOfLife
{
    static class Program
    {
        public static Stack<Scene> Scenes = new Stack<Scene>(4);
        public static readonly List<IDrawable> NeedsRedraw = new List<IDrawable>();
        public static bool running = true;

        static void Main(string[] pFilenames)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;

            if (pFilenames.Length > 0)
            {
                Environment.CurrentDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
                Console.WriteLine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName);
                Console.ReadLine();
                SceneAdd(new MainMenue());
                SceneAdd(new Game(pFilenames[0]));
                SceneAdd(new Intro());
            }
            else
            {
                Environment.CurrentDirectory = Directory.GetParent(Environment.CommandLine).FullName;
                SceneAdd(new MainMenue());
                SceneAdd(new Intro());
            }

            Console.CursorVisible = !running;
            do
            {
                //Console.WindowWidth = GameLogic.size.col * 2;
                //Console.WindowHeight = GameLogic.size.row;

                if (NeedsRedraw.Count > 0)
                {
                    foreach (var item in NeedsRedraw)
                    {
                        item.Draw();
                    }

                    NeedsRedraw.Clear();
                }

                Scenes.Peek().Update();
            } while (running);

            Console.ResetColor();
            Console.Clear();
        }

        /// <summary>
        /// Fügt neue Scene dem Stack hinzu
        /// </summary>
        /// <param name="NewScene"></param>
        public static void SceneAdd(Scene NewScene)
        {
            Scenes.Push(NewScene); // Neue Szene auf den Stapel an Szenen legen
            NewScene.Activate(); // Neue Szene aktivieren
        }


        /// <summary>
        /// Nimmt aktuélle Scene vom Stack und aktiviert, wenn vorhanden, die letzte davor auf dem Stack gelegte
        /// </summary>
        public static void SceneRemove()
        {
            NeedsRedraw.Clear();

            Scene temp = Scenes.Pop(); // Szene vom Szenenstapel entfernen

            if (Scenes.Count > 0) // Nachschauen ob noch Szenen vorhanden sind
            {
                {
                    Scenes.Peek().Activate(); // falls noch eine Szene vorhanden ist diese Aktivieren.
                }
            }

            Console.ResetColor();
            Console.Clear();
        }
    }
}
