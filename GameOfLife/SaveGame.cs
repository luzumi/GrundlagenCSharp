using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace GameOfLife
{
    public class SaveGame
    {
        public List<List<bool>> Field;
        public string fileName;
    }
}
