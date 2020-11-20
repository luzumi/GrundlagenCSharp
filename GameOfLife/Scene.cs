using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Scene
    {
        protected static (int x, int y) size;

        public Scene()
        {
            Program.Scenes.Push(this);
        }


        public virtual void Update()
        {

        }
    }
}
