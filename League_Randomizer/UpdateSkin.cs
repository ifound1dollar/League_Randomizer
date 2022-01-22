using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Randomizer
{
    class UpdateSkin
    {
        static int updateChampNum = -1;         //needed later to index champions list
        static string userInput;                //declare userInput immediately
        public static void Add()
        {
            Console.WriteLine("\nAdd skin for which champion?");
            Colors.SetColor("Magenta");             //get name of champion user desires to add skin to
            string newChampName = Console.ReadLine().ToLower();
            Console.ResetColor();

            if (DoesChampExist(newChampName))       //if desired champion DOES exist (also updates updateChampNum)
            {
                List<string> tempChampionData = new();  //declare empty array for temporary champion data
                foreach (string item in Program.champions[updateChampNum])
                {
                    tempChampionData.Add(item);     //creates a copy (list) of the current champion's array
                }

                Console.WriteLine("\n'{0}' found as existing champion. \n\tEnter name of the new skin " +
                    "(ENSURE IT IS PROPERLY CAPITALIZED AND SPELLED CORRECTLY):", newChampName.ToUpper());
                Colors.SetColor("Magenta");
                string newSkinName = Console.ReadLine();    //get name of new skin from user
                Console.ResetColor();

                if (!DoesSkinExist(newSkinName))        //if desired skin does NOT exist
                {
                    Console.Write("\nYou have entered '");
                    Colors.SetColor("Magenta");
                    Console.Write(newSkinName);     //print skin name in Magenta, surrounded by single quotes
                    Console.ResetColor();
                    Console.Write("'.\n\tConfirm add to {0}'s available skins?",
                        Program.champions[updateChampNum][1]);  //ask user for confirmation and list champion's name again
                    Colors.SetColor("Green");
                    userInput = Console.ReadLine().ToLower();   //get confirmation from user
                    Console.ResetColor();

                    if (userInput == "y" || userInput == "yes")
                    {
                        Colors.SetColor("Yellow");
                        Console.WriteLine("\nAdded to end of list.");
                        Console.ResetColor();
                        tempChampionData.Add(newSkinName);      //append new skin to end of temporary data list
                        Program.champions[updateChampNum] = tempChampionData.ToArray();
                            //replace current champion's string[] with new data (after casting to [])
                    }
                    else
                    {
                        Colors.SetColor("Red");
                        Console.Write("\nDid not add to list of skins.");
                        Console.ResetColor();           //acknowledge did not add
                    }
                }
                else
                {
                    Colors.SetColor("Red");
                    Console.Write("\n{0} is already an existing skin.", newSkinName);
                    Console.ResetColor();           //acknowlege skin already exists for champion
                }
            }
            else
            {
                Colors.SetColor("Red");
                Console.Write("\n'{0}' is not an existing champion. Entered champion name must be exact.",
                    newChampName);
                Console.ResetColor();           //acknowledge champion doesn't exist
            }
            Console.ReadLine();                 //wait for user input
        }
        public static void Delete()
        {
            Console.WriteLine("\nDelete skin for which champion?");
            Colors.SetColor("Magenta");             //get desired champion's name from user
            string newChampName = Console.ReadLine().ToLower();
            Console.ResetColor();

            if (DoesChampExist(newChampName))       //if desired champion DOES exist (also updates updateChampNum)
            {
                if (Program.champions[updateChampNum].Length > 3)
                    //if there are more than 3 items in array (meaning there is more than only Default skin)
                {
                    Console.Write("\nCan only delete last skin in list; last skin in list is currently '");
                    Colors.SetColor("Magenta");
                    Console.Write(Program.champions[updateChampNum][^1]);   
                                                                //displays last item in array in Magenta, surrounded by single quotes
                    Console.ResetColor();
                    Console.Write("'\n\tConfirm delete? This cannot be undone.");   //print last skin in array (using operator ^1)
                    Colors.SetColor("Green");
                    userInput = Console.ReadLine().ToLower();   //get delete confirmation from user
                    Console.ResetColor();

                    if (userInput == "y" || userInput == "yes")
                    {
                        List<string> tempChampionData = new();
                        foreach (string item in Program.champions[updateChampNum])
                        {
                            tempChampionData.Add(item);     //take LIST copy of champion's data
                        }
                        tempChampionData.RemoveAt(tempChampionData.Count - 1);  //remove last item from list

                        Colors.SetColor("Yellow");
                        Console.WriteLine("\nRemoved from list of available skins.");
                        Console.ResetColor();
                        Program.champions[updateChampNum] = tempChampionData.ToArray();
                            //replace current champion's string[] with new data (without last skin)
                    }
                    else
                    {
                        Colors.SetColor("Red");
                        Console.Write("\nDid not remove.");
                        Console.ResetColor();           //acknowledge did not remove
                    }
                }
                else
                {
                    Colors.SetColor("Red");
                    Console.Write("\nOnly skin currently available is 'Default'; cannot further remove a " +
                        "skin.");
                    Console.ResetColor();           //cannot remove skin if there are 3 or less total items (only skin is Default)
                }
            }
            else
            {
                Colors.SetColor("Red");
                Console.Write("\n'{0}' is not an existing champion. Entered champion name must be exact.",
                    newChampName);
                Console.ResetColor();           //acknowledge champion doesn't exist
            }
            Console.ReadLine();                 //wait for user input
        }
        static bool DoesChampExist(string newChampName)
        {
            bool championExists = false;        //defaults to false
            for (int i = 0; i < Program.champions.Count; i++)   //for each string[] in champions list
            {
                if (Program.champions[i].Contains(newChampName.ToUpper()))
                    //if there is already a champion with this name (capitalized)
                {
                    championExists = true;          //set championExists to true once encountering
                    updateChampNum = i;             //equal to the index of the current string[] (IMPORTANT)
                    break;                          //stops from unnecessarily continuing through list
                }
            }
            return championExists;              //return championExists bool
        }
        static bool DoesSkinExist(string newSkinName)
        {
            bool skinExists = false;            //defaults to false
            for (int i = 0; i < Program.champions[updateChampNum].Length; i++)   //for each skin in array
            {
                if (Program.champions[updateChampNum][i] == newSkinName)    //if there is already a skin with this name
                {
                    skinExists = true;                  //set skinExists to true once encountering
                    break;                              //stops from unnecessarily continuing through list
                }
            }
            return skinExists;                  //return skinExists bool
        }
    }
}
