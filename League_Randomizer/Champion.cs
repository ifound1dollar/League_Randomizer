using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Randomizer
{
    public class Champion
    {
        static readonly Random roll = new();
        static int oldSkinNum = -1;
        static int oldChromaNum = -1;           //private variables used inside PrintSkin and PrintChroma
        static bool enableDefaults;             //controls whether default skins can be rolled

        string _index;
        string _name;
        List<string> _skins = new();            //private attributes for Champion, List must be initialized with new()

        public Champion(string[] indeces)       //constructor
        {
            _index = indeces[0];                            //first object in list is the index
            _name  = indeces[1];                            //second object in list is the name
            for (int i = 2; i < indeces.Length; i++)        //starting AFTER the second object in list
            {
                _skins.Add(indeces[i]);                         //add each object in list to list of skins
            }
        }
        public string Index //champion's index getter
        {
            get
            {
                return _index;
            }
        }
        public string Name  //champion's name getter
        {
            get
            {
                return _name;
            }
        }

        public void PrintHeader(string appVer)      //prints header info
        {
            Colors.SetColor("Cyan");
            Console.WriteLine("League Randomizer " + appVer);
            Console.ResetColor();
            Console.WriteLine("Default skins enabled: " + enableDefaults);
            Colors.SetColor("Magenta");
            Console.WriteLine("  {0} {1}\n", _name, _index);
            Console.ResetColor();           //displays info in header: program title, version, and champion name + number
        }

        public void PrintSkin(int rolledNum = -1)   //uses optional int parameter so a roll can be forced
        {
            if (rolledNum == -1)                        //if default parameter is used
            {
                if (enableDefaults)     //if defaults enabled
                {
                    rolledNum = roll.Next(_skins.Count);
                    if (_skins.Count >= 2)                      //run anti-repeat ONLY if more than one skin exists
                    {
                        while (rolledNum == oldSkinNum)
                        {
                            rolledNum = roll.Next(_skins.Count);    //if the new skin is the same as previous, roll again until it isn't
                        }
                    }
                }
                else
                {
                    if (_skins.Count >= 2)                  //if more than one skin exists (not just Default)
                    {
                        rolledNum = roll.Next(1, _skins.Count);     //omit first entry (Default)
                        if (_skins.Count >= 3)                      //if there is Default and AT LEAST 2 other skins
                        {
                            while (rolledNum == oldSkinNum)
                            {
                                rolledNum = roll.Next(1, _skins.Count);   //run anti-repeat (roll until new skin does not match previous skin)
                            }
                        }
                    }
                    else
                    {
                        rolledNum = roll.Next(_skins.Count);    //if only one skin (Default), don't omit Default OR run anti-repeat
                    }
                }
            }

            Colors.SetColor("LightGreen");
            Console.Write(_skins[rolledNum]);           //display the name of the skin

            if (_skins[rolledNum].EndsWith(" "))        //skins with a chroma have a space as the last character
            {
                Console.Write(Chroma(_skins[rolledNum]));   //call Chroma local method, which returns a chroma
            }

            Console.WriteLine("\n");
            oldSkinNum = rolledNum;                     //oldSkinNum is assigned most recent skin each iteration
            Console.ResetColor();
        }
        string Chroma(string chromaSkin)
        {
            List<string> chroma = new();                //declare empty list for chromas

            if (chromaSkin == "Dragonslayer ")
            {
                chroma = new() { "", "Green", "Red", "Silver" };
            }   //Dragonslayer Vayne
            else if (chromaSkin == "Spirit Blossom ")
            {
                chroma = new() { "", "Red", "Yellow", "Green", "Purple", "Pink", "Black", "White" };
            }   //Spirit Blossom Vayne
            else if (chromaSkin == "Firecracker ")
            {
                chroma = new() { "", "White", "Black", "Light Blue", "Pink", "Orange", "Purple", "Green" };
            }   //Firecracker Jinx
            else if (chromaSkin == "Mecha ")
            {
                chroma = new() { "", "Yellow", "Green", "Black", "Tan", "White", "Blue", "Gray", "Orange" };
            }   //Mecha Malphite
            else if (chromaSkin == "Papercraft ")
            {
                chroma = new() { "", "White", "Light Blue", "Black", "Purple", "Pink", "Yellow" };
            }   //Papercraft Nunu & Willump
            else if (chromaSkin == "Rocket Girl ")
            {
                chroma = new() { "", "Blue", "Purple", "Red" };
            }   //Rocket Girl Tristana
            else if (chromaSkin == "Medieval ")
            {
                chroma = new() { "", "Purple" };
            }   //Medieval Twitch
            else if (chromaSkin == "Grey ")
            {
                chroma = new() { "", "Blue" };
            }   //Grey Warwick
            else if (chromaSkin == "Victorious ")
            {
                if      (_index == "32")  { chroma = new() { "", "Green", "Purple" }; }     //Blitzcrank
                else if (_index == "77")  { chroma = new() { "", "Green" }; }               //Orianna
                else if (_index == "85")  { chroma = new() { "", "White" }; }               //Graves
                else if (_index == "115") { chroma = new() { "", "Green", "Light Blue" }; } //Lucian
            }   //Victorious Blitzcrank, Orianna, Graves, Lucian

            int rolledNum = roll.Next(chroma.Count);    //'rolls' a number with the length of chroma
            while (rolledNum == oldChromaNum)
            {
                rolledNum = roll.Next(chroma.Count);        //run anti-repeat on the chroma
            }
            
            string chromaColor = chroma[rolledNum];     //assigns chroma to a new string chromaColor
            Colors.SetColor(chromaColor);               //pass chromaColor to Colors.SetColor to change console color to match
            oldChromaNum = rolledNum;                   //oldSkinNum is assigned most recent skin each iteration

            return chromaColor;                         //returns chroma string, with color changed to correspond
        }

        public static void EnableDefaults(bool first = false)   //takes optional argument for when the program is first run
        {
            if (first)                              //if program has just started
            {
                enableDefaults = false;                 //set defaults to false
                return;                                 //ignore code below since program has just began
            }
            
            Console.Clear();
            string enabledOrDisabled;
            if (enableDefaults)
            {
                enabledOrDisabled = "enabled";          //if enabled, assign string to represent
            }
            else
            {
                enabledOrDisabled = "disabled";         //opposite as 'enabled' above
            }

            Colors.SetColor("Yellow");
            Console.Write("Default skins are currently {0}.\n\tEnter 'enable' or 'disable' to update...",
                enabledOrDisabled);                     //prompt user to enable or disable default skins
            Console.ResetColor();
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "enable" || userInput == "e")          //if user chooses to enable
            {
                Console.WriteLine("\nEnabled defaults.");
                enableDefaults = true;                                  //acknowledge and set to TRUE
            }
            else if (userInput == "disable" || userInput == "d")    //else user chooses to disable
            {
                Console.WriteLine("\nDisabled defaults.");
                enableDefaults = false;                                 //acknowledge and set to FALSE
            }
            else
            {
                Colors.SetColor("Red");
                Console.WriteLine("\nInvalid input, cancelling.");
                Console.ResetColor();                                   //acknowledge invalid input
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
}
