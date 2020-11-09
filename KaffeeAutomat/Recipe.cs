using System;
using System.Collections.Generic;
using System.Text;

namespace KaffeeAutomat
{
    class Recipe
    {                                              //"Waste";"Water","Coffee","Milk";"Cacao";"Tea";"Sugar"));
        public static int[,] GetIncredience = {{   1,      1,      1,      0,      0,      0,      0},      //"Kaffee schwarz", 
                                                    {   1,      1,      1,      0,      0,      0,      1}, //"Kaffee Zucker",
                                                    {   1,      1,      1,      1,      0,      0,      0}, //"Kaffee Milch",
                                                    {   1,      1,      1,      1,      0,      0,      1}, //"Kaffee Milch und Zucker,
                                                    {   1,      1,      0,      1,      0,      0,      1}, //"Kakao",
                                                    {   1,      1,      0,      0,      0,      1,      0}, //"Tea"
            
    };
            

    public static String[] ProductNames = new String[]{
            "Kaffee schwarz", "Kaffee Zucker", "Kaffee Milch", "Kaffee Milch und Zucker", "Kakao", "Tea"
    };
    }
}
