
namespace KaffeeAutomat
{
    /// <summary>
    /// struct zum einfachem Zugriff auf die Behälter
    /// </summary>
    struct RecipeInfo
    {
        public Recipe RecipeID;
        public byte containerWater;
        public byte containerWasteCoffee;
        public byte containerWasteWater;
        public byte containerCoffee;
        public byte containerMilk;
    }
}
