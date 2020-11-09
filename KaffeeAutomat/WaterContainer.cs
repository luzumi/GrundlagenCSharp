using System;
using System.Collections.Generic;
using System.Text;

namespace KaffeeAutomat
{
    public class WasteContainer : Container
    {
        public WasteContainer(int pFillLevel, int pMaxFilLevel, string pContainername) : base(pFillLevel, pMaxFilLevel, pContainername)
        {
            this.FillLevel = pFillLevel;
            this.MaxFillLevel = pMaxFilLevel;
            this.ContainerName = pContainername;
        }
    }
}
