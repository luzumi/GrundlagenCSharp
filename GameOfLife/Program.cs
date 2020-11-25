using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        public static Stack<Scene> Scenes = new Stack<Scene>(4);
        static public readonly List<IDrawable> NeedsRedraw = new List<IDrawable>();
        public static bool running = true;

        static void Main()
        {
            Console.CursorVisible = false;
            SceneAdd(new Intro());

            Console.CursorVisible = !running;
            do
            {
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


        public static void SceneAdd(Scene NewScene)
        {
            Scenes.Push(NewScene); // Neue Szene auf den Stapel an Szenen legen
            NewScene.Activate(); // Neue Szene aktivieren
        }

        public static Scene SceneRemove()
        {
            Scene temp = Scenes.Pop(); // Szene vom Szenenstapel entfernen

            if (Scenes.Count > 0) // Nachschauen ob noch Szenen vorhanden sind
            {
                {
                    Scenes.Peek().Activate(); // falls noch eine Szene vorhanden ist diese Aktivieren.
                }
            }
            
            Console.ResetColor();
            Console.Clear();

            return temp;
        }
    }
}
