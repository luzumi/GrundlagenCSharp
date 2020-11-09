using System;
using System.Collections.Generic;
using System.Text;

namespace KaffeeAutomat
{
    class CoffeeMashine
    {
        public List<Container> containers;
        public bool run = true;

        public CoffeeMashine()
        {
            containers = new List<Container>();
            containers.Add(new WasteContainer(0,50, "Waste"));
            containers.Add(new Container(50,50,"Water"));
            containers.Add(new Container(20,20,"Coffee"));
            containers.Add(new Container(20,20,"Milk"));
            containers.Add(new Container(20,20,"Cacao"));
            containers.Add(new Container(20,20,"Tea"));
            containers.Add(new Container(20,20,"Sugar"));
        }

        void OutputCoffee(int request)
        {
            if (request > 0 && request <= Recipe.ProductNames.Length-1) 
                {
                if(containers[Constants.Waste].FillLevel == containers[Constants.Waste].MaxFillLevel){

                    Console.WriteLine("Waste is Full");
                
                }
            else {
                containers[Constants.Waste].FillLevel += 1;
                MakeProduct(request);
            }
        } else {
            if (request == 0) {
                CloseProgramm("Auswahl \"0\" -> Programmabruch");
            } else if (request == Constants.StopProgramm) {
                Maintenance();

            } else {
                CloseProgramm("Falsche Eingabe -> Programmabruch");
            }
        }
            
        }

        void Maintenance()
        {
            containers.ForEach(c => c.FillLevel = c.MaxFillLevel);
            containers[Constants.Waste].FillLevel = 0;

            InventoryStatus();
        }

        
        void DisplayProgress()
        {
            Console.WriteLine("In progress....");
            for (int i = 0; i < 20; i++)
            {
                System.Threading.Thread.Sleep(66);
                Console.Write("*");
            }
            Console.WriteLine();
        }                                              

        
        public void InventoryStatus()
        {
            Console.WriteLine();
            Console.WriteLine("Fill Level actual: ");
            containers.ForEach(c => Console.WriteLine( c.ContainerName + " : " + Math.Round(((double)c.FillLevel * 100 / c.MaxFillLevel), 2) + " %"));
        }

        
        public int DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("****** Kaffeemaschine ******\n" +
                              "============================\n" +
                              "Auswahl:____________________\n" +
                              "1.) Kaffee schwarz__________\n" +
                              "2.) Kaffee mit Zucker_______\n" +
                              "3.) Kaffee mit Milch________\n" +
                              "4.) Kaffee mit Milch/Zucker_\n" +
                              "5.) Kakao___________________\n" +
                              "9.) _________________Wartung\n" +
                              "0.) _________________Abbruch\n" +
                              "============================\n"  );
            
            return SelectProgram("Your Choice please:_________");
           
        }

        
        void MakeProduct(int request)
        {
            for (int counter = 1; counter < containers.Count; counter++) 
            {

                if (IsEmpty(request, counter)) 
                {

                    containers[counter].FillLevel -= Recipe.GetIncredience[request,counter];

                    Console.WriteLine( "Actual Fill Level " + containers[counter].ContainerName + " : " + containers[counter].FillLevel);
                } 
                else 
                {
                    run = false;

                    WaitForMaintenance(counter);

                }
            }
            DisplayProgress();
            Console.WriteLine(Recipe.ProductNames[request] + " is ready to drink!");
        }


        public bool IsEmpty(int eingabeUser, int counter)
        {
            return ((containers[counter].FillLevel - Recipe.GetIncredience[eingabeUser,counter]) > 0);
        }

        
        public void WaitForMaintenance(int counter)
        {
            while (!run) 
            {
                Console.WriteLine("Maintenance is required, Press '9' for Maintenace");
                
                DisplayMenu();

                if (SelectProgram("Programm auswählen") == 9) 
                {
                    Maintenance();                    
                }
            }

        }


        public int SelectProgram(String promptMsg) {

            int num = 0;
            bool isValid = false;
            Console.WriteLine(promptMsg);

            while(!isValid) {
                string strInput = Console.ReadLine();

                if (!int.TryParse(strInput, out num))
                {
                    Console.WriteLine("Eingabe war Falsch, bitte die '9' verwenden\n");
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            }
            return num;
        }


        void CloseProgramm(string input)
        {
            Console.WriteLine(input);

            run = false;
        }

        public void RunTheProgramm()
        {
            OutputCoffee(DisplayMenu());
            
        }
    }
}
