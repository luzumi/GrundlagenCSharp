namespace KaffeeAutomat
{
    public class Container
    {
        public int FillLevel { get; set; }
        public int MaxFillLevel { get; set; }
        public string ContainerName { get; set; }

        public Container(int pFillLevel, int pMaxFilLevel, string pContainername)
        {
            this.FillLevel = pFillLevel;
            this.MaxFillLevel = pMaxFilLevel;
            this.ContainerName = pContainername;
        }

        public Container()
        {

        }
         

    


    }
}
