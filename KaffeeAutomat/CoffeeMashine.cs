namespace KaffeeAutomat
{
    /// <summary>
    /// Eine KaffeeMaschine
    /// </summary>
    class CoffeeMashine
    {
        byte containerCoffee;
        byte containerWater;
        byte containerWasteCoffee;
        byte containerWasteWater;
        byte containerMilk;

        public byte CCoffee
        {
            get { return containerCoffee; }
            private set { containerCoffee = value; }
        }
        public byte CWater
        {
            get { return containerWater; }
            private set { containerWater = value; }
        }
        public byte CWasteWater
        {
            get { return containerWasteWater; }
            private set { containerWasteWater = value; }
        }
        public byte CWasteCoffee
        {
            get { return containerWasteCoffee; }
            private set { containerWasteCoffee = value; }
        }

        public byte CMilk
        {
            get { return containerMilk; }
            private set { containerMilk = value; }
        }



        public CoffeeMashine()
        {
            Maintenance();

            #region Recipes;

            recipeInfos[0].containerCoffee = 3;
            recipeInfos[0].containerWasteCoffee = 3;
            recipeInfos[0].containerWasteWater = 1;
            recipeInfos[0].containerWater = 10;
            recipeInfos[0].containerMilk = 2;
            recipeInfos[0].RecipeID = Recipe.Coffee;

            recipeInfos[1].containerCoffee = 3;
            recipeInfos[1].containerWasteCoffee = 3;
            recipeInfos[1].containerWasteWater = 1;
            recipeInfos[1].containerWater = 10;
            recipeInfos[1].containerMilk = 2;
            recipeInfos[1].RecipeID = Recipe.HotWater;

            recipeInfos[2].containerCoffee = 3;
            recipeInfos[2].containerWasteCoffee = 3;
            recipeInfos[2].containerWasteWater = 1;
            recipeInfos[2].containerWater = 10;
            recipeInfos[2].containerMilk = 2;
            recipeInfos[2].RecipeID = Recipe.Capucchino;

            recipeInfos[3].containerCoffee = 2;
            recipeInfos[3].containerWasteCoffee = 2;
            recipeInfos[3].containerWasteWater = 1;
            recipeInfos[3].containerWater = 5;
            recipeInfos[3].containerMilk = 5;
            recipeInfos[3].RecipeID = Recipe.CoffeeMilk;

            recipeInfos[4].containerCoffee = 0;
            recipeInfos[4].containerWasteCoffee = 0;
            recipeInfos[4].containerWasteWater = 0;
            recipeInfos[4].containerWater = 0;
            recipeInfos[4].containerMilk = 10;
            recipeInfos[4].RecipeID = Recipe.HotMilk;

            #endregion;
        }


        /// <summary>
        /// Alle Behälter werden zurückgesetzt
        /// </summary>
        public void Maintenance()
        {
            CCoffee = 100;
            CWater = 100;
            CWasteCoffee = 0;
            CWasteWater = 0;
        }

        //Liste der Reszepte
        RecipeInfo[] recipeInfos = new RecipeInfo[5];


        /// <summary>
        /// Prüft Ob ausreichend Vorräte vorhanden und entnimmt Zutaten den Behältern und befüllt die Waste laut Rezept
        /// </summary>
        /// <param name="ChosenProduct"></param>
        /// <returns></returns>
        public virtual bool Dispense(Recipe ChosenProduct)
        {

            if (recipeInfos[(int)ChosenProduct].containerMilk < CMilk &&
                recipeInfos[(int)ChosenProduct].containerCoffee < CCoffee &&
                recipeInfos[(int)ChosenProduct].containerWasteCoffee > CWasteCoffee &&
                recipeInfos[(int)ChosenProduct].containerWasteWater > CWasteWater &&
                recipeInfos[(int)ChosenProduct].containerWater < CWater)
            {
                System.Threading.Thread.Sleep(4000);
                CMilk -= recipeInfos[(int)ChosenProduct].containerMilk;
                CCoffee -= recipeInfos[(int)ChosenProduct].containerCoffee;
                containerWasteCoffee += recipeInfos[(int)ChosenProduct].containerWasteCoffee;
                containerWasteWater += recipeInfos[(int)ChosenProduct].containerWasteWater;
                containerMilk -= recipeInfos[(int)ChosenProduct].containerWater;
                return true;
            }

            return false;
        }
    }
}
