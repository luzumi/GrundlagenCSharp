using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    abstract class Scene
    {
        public abstract void Update();

        public abstract void Activate();
    }
}
