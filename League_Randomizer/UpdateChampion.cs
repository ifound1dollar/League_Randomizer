using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Randomizer
{
    class UpdateChampion
    {
        static string userInput;                //declare userInput immediately
        public static void Add()
        {
            Console.WriteLine("\nEnter name of champion to add:");
            Colors.SetColor("Magenta");             //prompt user to enter name of champion they want to add
            string newChampName = Console.ReadLine();
            Console.ResetColor();

            if (!DoesChampExist(newChampName))      //if user's champion does NOT already exist
            {
                Console.Write("\n'{0}' not found as existing champion.\nDrafting new champion '{1}' " +
                        "with champNum = {2} and Default skin. Confirm append to end of list?",
                        newChampName, newChampName.ToUpper(), Program.champions.Count);
                    //capitalize new champion name and acknowledge that it will be at the end of the list
                
                string[] newData =                  //create new string[] with champion data
                {
                    (Program.champions.Count).ToString(),   //equal to one more than the index at end of list (starts at 1)
                    newChampName.ToUpper(),                 //name of champion, capitalized
                    "Default"                               //default skin for starters
                };
                string[] newStringData =            //create new string[] with data to switch to the new champion
                {
                    newChampName.ToLower()                  //only needs lowercase version of the champion name
                };

                Colors.SetColor("Green");
                userInput = Console.ReadLine().ToLower();   //gets userInput again to confirm
                Console.ResetColor();

                if (userInput == "y" || userInput == "yes")
                {
                    Colors.SetColor("Yellow");
                    Console.WriteLine("\nAppended {0} to champions list.", newChampName.ToUpper());
                    Console.ResetColor();
                    Program.champions.Add(newData);                 //append new champion data to list
                    Program.champion_strings.Add(newStringData);    //append new string data to list
                }
                else
                {
                    Colors.SetColor("Red");
                    Console.Write("\nDid not add new champion to list.");
                    Console.ResetColor();               //acknowledge did not confirm
                }
            }
            else
            {
                Colors.SetColor("Red");
                Console.Write("\n'{0}' found as already existing champion.",
                    newChampName);
                Console.ResetColor();               //acknowledge that the desired champion already exists
            }
            Console.ReadLine();                 //wait for user input
        }
        public static void Delete()
        {
            Console.Write("\nCan only delete last champion in list; last champion in list is " +
                    "currently ");
            Colors.SetColor("Magenta");
            Console.Write(Program.champions[^1][1]);    //print last champion in list in Magenta
            Console.ResetColor();
            Console.Write(".\n\tAre you sure you want to delete? This cannot be undone.");
                //accesses champions list at the last string[], at the second index (which is the champion's name)
            Colors.SetColor("Green");
            userInput = Console.ReadLine().ToLower();   //gets userInput to confirm delete
            Console.ResetColor();

            if (userInput == "y" || userInput == "yes")
            {
                Colors.SetColor("Yellow");
                Console.WriteLine("\nRemoved {0} from list of champions.",
                    Program.champions[^1][1]);          //like above, accesses last champion's name
                Console.ResetColor();
                Program.champions.RemoveAt(Program.champions.Count - 1);                //remove champion data at last index
                Program.champion_strings.RemoveAt(Program.champion_strings.Count - 1);  //remove string data at last index
            }
            else
            {
                Colors.SetColor("Red");
                Console.Write("\nDid not remove.");
                Console.ResetColor();               //acknowledge did not remove
            }
            Console.ReadLine();                 //wait for user input
        }
        static bool DoesChampExist(string newChampName)
        {
            bool championExists = false;            //defaults to false
            for (int i = 0; i < Program.champions.Count; i++)   //for each string[] in champions list
            {
                if (Program.champions[i].Contains(newChampName.ToUpper()))
                    //if there is already a champion with this name (capitalized)
                {
                    championExists = true;              //set championExists to true once encountering
                    break;                              //stops from unnecessarily continuing through list
                }
            }
            return championExists;                  //return championExists bool
        }
    }
}
