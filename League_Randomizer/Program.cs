using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace League_Randomizer
{
    class Program
    {
        /// Defines variables upon program start that are used throughout the class.
        /// 
        /// Defines Random that is used in Skin function.
        /// 
        /// Booleans: (default to false)
        ///     first: Used in Reply and Main, and assigned True immediately so Main assigns value to champNum 
        ///         (see below) and calls Intro. Assigned in Reply when input is to reset program; used in Main
        ///         before loop to re-assign Vayne's champNum and call Intro again.
        ///     invalid: Used in Reply and Main. Assigned in Reply when user input is invalid; used in Main to
        ///         continue (return to top of) the while loop.
        ///     dragonslayer/spiritBlossom: Used in Reply and Skin. Assigned in Reply when user input
        ///         corresponds with skin string(s); used in Skin to ensure that Dragonslayer or Spirit Blossom
        ///         (and their variants) are rolled.
        ///     
        /// Integers:
        ///     champNum: Used in Main, Reply, and Skin. Assigned values in Reply when user input is equal to
        ///         corresponding champion name; used in Main to display header info, used in Reply to check if
        ///         user intends to change champion mode (in addition to assignment), and used in Skin to index
        ///         which string array (aka champion) is in use.
        ///     oldChampNum: Used in Reply. Assigned values at top of Reply equal to previously rolled skin;
        ///         used to check whether champNum has changed during the Reply function.
        ///     skinNum: Used in Skin. Assigned values using roll (Random) according to number of strings in
        ///         corresponding arrays; used to select specific string in corresponding array.
        ///     oldSkin: Used in Skin. Assigned values at the end of skin equal to rolled skin; used to prevent
        ///         repeats.
        ///     chromaNum: Used in Skin (similar to skinNum). Assigned values using roll (Random) according to
        ///         number of strings in LOCALLY defined arrays corresponding to specific skins with chromas;
        ///         used to select specific string in corresponding array.
        ///     oldChroma: Used in Skin (very similar to oldSkin). Assigned values equal to rolled skin at end
        ///         and end of conditional statement within Skin; used to prevent repeats of chromas.
        /// (See summaries (specifically Skin) for more detailed info)
        /// 
        /// String array (jagged, aka arrays within array): This is where all of the champion arrays and strings 
        ///     are located. Each champion is arranged chronologically (Alistar being the first, most recent being
        ///     the last). Index 0 is null so the indexes correspond to the literal champion number, beginning at
        ///     1. Each champion's string array includes the champion name in index 0, then the skin names after.
        ///     Champions with only "Default" available need only the name and "Default" in the array. Skins with
        ///     chromas available need to end with a SPACE; Skin checks this to determine whether it has a chroma.
        ///     (See Skin summary for more)
        //COMMENT BLOCK ABOVE IS OLD AND USELESS

        #region Program class variable declarations
        static bool first = true;                   //tells Main() to run initial operations
        public static bool dragonslayer;
        public static bool spiritBlossom;
        public static bool enableDefaults;          //three publics are also accessed in Champion class

        static int champNum;                        //integer that is used to select/change the current champion

        public static readonly List<string[]> champions = new();
            //list of string arrays with all champions' names, indeces, and available skins
        public static readonly List<string[]> champion_strings = new();
            //list of string arrays with all strings that are compared with input to change the current champion mode
        #endregion
        static readonly string appVer = "v2.0.1";       //app version string that is easily accessible
        static void Main()
        /// Program is focused primarily in Main function, where the while loop is located. Calls other
        /// functions generously.
        ///
        /// Clears the screen each time Main is called (including program start).
        /// 
        /// Checks 'first', which is set to true when the program is first started (or reset). If true, changes
        /// boolean to false (so it doesn't run Intro and reset champNum every time Main is called), then
        /// assigns Vayne's index to champNum and calls Intro function. Clears screen when returning from Intro.
        /// 
        /// Both oldSkin and oldChroma are reset to -1 (which is inaccessible by indexing and therefore safe to
        /// use as a placeholder value) because Main is only ever called when there is a change that would not
        /// need to use the anti-repeat functionality.
        /// 
        /// Displays header info. First, program name and then the app version. Second, displays the first 
        ///     string in the array, indexed by champNum, inside of the 'champions' jagged array.
        ///     ex. champNum = 76 -> "VAYNE" (index 0 of 76th array)
        ///     Third, displays the champion index number.
        /// 
        /// BEGINS WHILE LOOP. Immediately calls Reply to get user input. After program returns to loop from
        /// Reply (if it does not call Main), checks if 'invalid' was set to true. If so, returns to top of
        /// loop and calls Reply again. If not, continues on to call Skin function. When program returns to
        /// loop, reaches the end and returns to the top. Rinse and repeat.
        {
            //COMMENT BLOCK ABOVE IS OLD AND USELESS

            Console.Clear();                    //clears screen as Main() starts
            Console.ResetColor();
            if (first)                          //checks if it should run initial operations
            {
                LoadChampions();                    //loads list of champion string[]s from champions file
                LoadStrings();                      //loads list of keyword string[]s from champion strings file
                first = false;                      //sets first to false
                enableDefaults = false;             //set enableDefaults to false
                champNum = 76;                      //default to Vayne mode
                Intro();                            //call Intro to display
                Console.Clear();                    //clear console after intro
            }

            Champion currentChampion = new(champions[champNum]);
                                            //declare new currentChampion object and pass current champion string array

            Colors.SetColor("Cyan");
            Console.WriteLine("League Randomizer " + appVer);
            Console.ResetColor();
            Console.WriteLine("Default skins enabled: " + enableDefaults);
            Colors.SetColor("Magenta");
            Console.WriteLine("  {0} {1}\n", currentChampion.Name, currentChampion.Index);
            Console.ResetColor();           //displays info in header: program title, version, and champion name + number

            while (!false)
            {
                if (GetInput())                 //calls GetInput() each iteration to get user input
                                                //GetInput() returns true/false depending if input was valid/invalid
                {
                    currentChampion.PrintSkin();    //if input was valid, call PrintSkin() method from currentChampion object
                }
            }
        }


        static bool GetInput()
        /// Reply function exists to get the user's input. All conditionals use else if (except for first).
        /// 
        /// 'oldChampNum' is reset to equal champNum every time Reply is called. This allows the below if()
        /// statement to check if the variable was changed inside the 'champions' region.
        ///
        /// Displays a prompt and defines a string according to the user's input, casted to lowercase.
        ///
        /// Misc region:
        ///     clear: returns to Main, automatically clearing the screen.
        ///     changelog: calls Changelog function, which displays changelog and has then returns to Main.
        ///     help: calls Help function, which displays commands, prompts user and returns to Main.
        ///     intro/reset: assigns True to 'first' then returns to Main. This allows Intro to be called and 
        ///         resets champNum to Vayne's corresponding index number.
        ///     random: rolls champNum using the number of string arrays inside champions (jagged array), then
        ///         returns to Main.
        /// 
        /// Champions region:
        ///     Checks if user input corresponds with any champion names or numbers. Organized alphbetically to
        ///     make it readable. If so, changes champNum according to which champion name was input. (More
        ///     relating to this below)
        ///     
        /// Dragonslayer/Spirit Blossom region:
        ///     Checks if user input corresponds with dragonslayer or spirit blossom. If so, respective booleans
        ///     are assigned True, which are used in Skin. (More on this summarized in Skin function)
        ///     
        /// Invalid region:
        ///     Checks if user input was more than enter, but did not make any of the above conditionals true.
        ///     If so, displays an error message and sets 'invalid' boolean to true, which is used in Main
        ///     (Explained in summary). Marks the end of input conditionals and returns to while loop in Main.
        ///     
        /// FINALLY, checks if champNum has changed since the beginning of the Reply function. If so, returns to
        /// Main; if not, returns to while loop.
        {
            //COMMENT BLOCK ABOVE IS OLD AND USELESS

            bool inputIsValid = true;               //is returned at end of method, tells Main() whether input was valid
            Console.Write("Press enter to run...");
            Colors.SetColor("Magenta");
            string userInput = Console.ReadLine().ToLower().Trim();         //get user input, casting to lowercase and trimming whitespace
            bool numeric = Int32.TryParse(userInput, out int numericInput); //check if userInput is numeric and try to parse
            Console.ResetColor();

            #region clear screen
            if (userInput == "clear")
            {
                Main();                             //calls to main, which automatically clears screen
            }
            #endregion
            #region reset
            else if (userInput == "reset" || userInput == "intro")
            {
                first = true;                       //forces program to print intro and populate champions list again
                champions.Clear();                  //clears champions list array to be re-written
                champion_strings.Clear();
                Main();                             //return to main
            }
            #endregion
            #region help
            else if (userInput == "h" || userInput == "help")
            {
                Help();                             //call Help and return to main
                Main();
            }
            #endregion
            #region changelog
            else if (userInput == "c" || userInput == "changelog")
            {
                Changelog();                        //call Changelog and return to main
                Main();
            }
            #endregion

            #region random champion
            else if (userInput == "r" || userInput == "rand" || userInput == "random")
            {
                RandomChampion();
                Main();
            }
            #endregion
            #region enable/disable defaults
            else if (userInput == "def" || userInput == "default" || userInput == "defaults")
            {
                EnableDefaults();                   //call defaults function
                Main();                             //call Main to reset
            }
            #endregion
            #region list champions
            else if (userInput == "list")
            {
                List();                             //call list function
                Main();                             //return to main
            }
            #endregion
            #region update data/files
            else if (userInput == "update")
            {
                Update();                           //call update function
                Main();                             //return to main
            }
            #endregion

            #region dragonslayer / spirit blossom
            else if ((userInput == "ds") && champNum == 76)     //if user inputs "ds" while in Vayne mode
            {
                dragonslayer = true;                //set dragonslayer bool to true, which is checked in Champions.PrintSkin()
            }
            else if ((userInput == "sb") && champNum == 76)
            {
                spiritBlossom = true;               //same as above with dragonslayer
            }
            #endregion

            #region numeric input
            else if (numeric)                       //if 'userInput' is numeric
            {
                if (numericInput > 0 && numericInput <= champions.Count - 1)
                                                    //ensure numericInput is between 1 and the number of champions
                {
                    champNum = numericInput;            //if 'numericInput' is a valid champion number, change champNum to it
                    Main();                             //call Main() when champNum is changed
                }
                else
                {
                    Colors.SetColor("Red");
                    Console.WriteLine("{0} is not a valid champion number.\n", numericInput);
                    Console.ResetColor();               //else acknowledge that the input number is not valid
                    inputIsValid = false;               //set inputIsValid to false
                }
            }
            #endregion
            #region champion strings
            else if (userInput.Length > 0 && IsChampionChanged(userInput))
            //check length first to not unnecessarily call IsChampionChanged when user only pressed enter
            {
                Main();                             //call Main if champion has been changed, resetting
            }
            #endregion

            #region invalid
            else if (userInput.Length > 0)      //if none of the above are true AND user input at least one character
            {
                Colors.SetColor("Red");
                Console.WriteLine("Invalid input. Please try again.\n");
                Console.ResetColor();
                inputIsValid = false;               //acknowledge invalid input and set inputIsValid to false
            }
            #endregion
            return inputIsValid;                    //return bool based on whether input was valid
        }
        static bool IsChampionChanged(string userInput)
        {
            bool championChanged = false;
            for (int i = 1; i < champion_strings.Count; i++)    //iterate through every string[] in champion strings list, starting at 1
            {
                foreach (string championString in champion_strings[i])  //for each individual string in string[]
                {
                    if (userInput == championString)    //if passed string 'userInput' matches the current champion string
                    {
                        champNum = i;                       //if a match is found, change champNum to the index of the line of the string[]
                        championChanged = true;
                        break;                              //set championChanged to true and break from foreach loop
                    }
                }
                if (championChanged)
                {
                    break;                      //break from for loop after match is found to avoid unnecessarily looping
                }
            }
            return championChanged;         //return championChanged bool
        }

        static void LoadChampions()
        {
            string path = @"C:\Users\Tanner\Documents\Visual Studio 2019\Projects\League_Randomizer\League_Randomizer\champions.txt";
                //path for champions text file
            try
            {
                using StreamReader sr = new(path);      //initialize new streamreader
                string line;
                while ((line = sr.ReadLine()) != null)  //for every line in the file
                {
                    string[] item = line.Split(", ");       //split each line at ', ' string (each item is separated by a comma + space)
                    champions.Add(item);                    //add the new string Array (from the file) to the champions list of Arrays
                }
                sr.Close();                             //close streamreader
            }
            catch (Exception e)
            {
                Console.WriteLine("champions.txt could not be read: ");
                Console.WriteLine(e.Message);               //if there is an exception, display error message rather than crash program
            }
        }
        static void LoadStrings()
        {
            string path = @"C:\Users\Tanner\Documents\Visual Studio 2019\Projects\League_Randomizer\League_Randomizer\champion_strings.txt";
                //path for champion strings text file
            try
            {
                using StreamReader sr = new(path);      //initialize new streamreader
                string line;
                while ((line = sr.ReadLine()) != null)  //for every line in the file
                {
                    string[] item = line.Split(", ");       //split each line at ', ' string (each item is separated by a comma + space)
                    champion_strings.Add(item);             //add the new string Array (from the file) to the champion_strings list of Arrays
                }
                sr.Close();                             //close streamreader
            }
            catch (Exception e)
            {
                Console.WriteLine("champion_strings.txt could not be read: ");
                Console.WriteLine(e.Message);               //if there is an exception, display error message rather than crash program
            }
        }
        static void WriteChampions()
        {
            string path = @"C:\Users\Tanner\Documents\Visual Studio 2019\Projects\League_Randomizer\League_Randomizer\champions.txt";
                //path for champions text file
            try
            {
                using StreamWriter sw = new(path);      //initialize new streamwriter
                foreach (string[] champ in champions)   //for each champion's data in champions list
                {
                    for (int i = 0; i < champ.Length - 1; i++)  //do NOT include last word in list
                    {
                        sw.Write(champ[i] + ", ");              //write all instances of the array EXCEPT the last with a ', ' following
                    }
                    sw.Write(champ[^1]);                        //write last instance of array WITHOUT a comma + string
                    sw.WriteLine("");                           //end line to begin next
                }
                Console.WriteLine("\nWrote changes to file: champions.txt");
                sw.Close();                             //close streamwriter
            }
            catch (Exception e)
            {
                Console.WriteLine("champions.txt could not be opened: ");
                Console.WriteLine(e.Message);               //if there is an exception, display error message rather than crash program
            }
        }
        static void WriteStrings()
        {
            string path = @"C:\Users\Tanner\Documents\Visual Studio 2019\Projects\League_Randomizer\League_Randomizer\champion_strings.txt";
                //path for champions text file
            try
            {
                using StreamWriter sw = new(path);      //initialize new streamwriter
                foreach (string[] champString in champion_strings)  //for each string data array in champion strings list
                {
                    for (int i = 0; i < champString.Length - 1; i++)    //do NOT include last word in list
                    {
                        sw.Write(champString[i] + ", ");                //write all instances of the array EXCEPT the last with a ', ' following
                    }
                    sw.Write(champString[^1]);                          //write last item in array WITHOUT a comma + space
                    sw.WriteLine("");                                   //end line to begin next
                }
                Console.WriteLine("Wrote changes to file: champion_strings.txt");
                sw.Close();                             //close streamwriter
            }
            catch (Exception e)
            {
                Console.WriteLine("champion_strings.txt could not be opened: ");
                Console.WriteLine(e.Message);           //if there is an exception, display error message rather than crash program
            }
        }
        static void Update()
        {
            Console.Clear();
            Colors.SetColor("Yellow");
            Console.Write("What would you like to update? Enter 'champion' or 'skin'.\n\t" +
                "Enter 'write' to permanently store any changes to file...");
                //prompt user for type of item to change
            Console.ResetColor();
            string userInput = Console.ReadLine().ToLower();    //get input and cast to lowercase

            if (userInput == "champion")
            {
                Console.Write("\nAdd or delete champion? Enter 'add' or 'delete'...");
                Colors.SetColor("Green");
                userInput = Console.ReadLine().ToLower();
                Console.ResetColor();

                if (userInput == "add")
                {
                    UpdateChampion.Add();           //add champion elsewhere
                }
                else if (userInput == "delete")
                {
                    UpdateChampion.Delete();        //remove champion elsewhere
                }
                else
                {
                    Colors.SetColor("Red");
                    Console.Write("\nInvalid input, exiting update screen.");
                    Console.ResetColor();
                }
            }
            else if (userInput == "skin")
            {
                Console.Write("\nAdd or delete skin? Enter 'add' or 'delete'...");
                Colors.SetColor("Green");
                userInput = Console.ReadLine().ToLower();
                Console.ResetColor();

                if (userInput == "add")
                {
                    UpdateSkin.Add();               //add skin elsewhere
                }
                else if (userInput == "delete")
                {
                    UpdateSkin.Delete();            //remove skin elsewhere
                }
                else
                {
                    Colors.SetColor("Red");
                    Console.Write("\nInvalid input, exiting update screen.");
                    Console.ResetColor();
                }
            }
            else if (userInput == "write")
            {
                WriteChampions();                       //call WriteChampions()
                WriteStrings();                         //call WriteStrings()
                Console.ReadLine();                     //wait on user input
            }
            else
            {
                Colors.SetColor("Red");
                Console.Write("\nInvalid input, exiting update screen.");
                Console.ResetColor();
                Console.ReadLine();
            }

            Main();                                     //return to main afterword
        }

        static void Intro()
        /// This is a basic formatted introduction screen for the app.
        /// 
        /// Begin by defining the two strings being displayed and assigning them to 'intro' and 'begin'.
        /// 
        /// Text is shifted down 12 lines using \n. Next, a new string begins at the window width minus the
        ///     length of the string, divided by 2; WriteLine displays the actual text and ends the line.
        /// Both 'begin' and 'intro' use this method.
        /// 
        /// Gets user key input to continue on to program.
        {
            //QUICK SUMMARY IN ABOVE COMMENT BLOCK

            string intro = "ifound1dollar's League of Legends Randomizer ";
            string begin = "Press any key to begin...";     //declares/assigns intro screen text that is formatted and printed below
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n");
            Colors.SetColor("Cyan");
            Console.Write(new string(' ', (Console.WindowWidth - (intro.Length + appVer.Length)) / 2));
            Console.Write(intro);                   //prints first string, centered (with length of appVer added to calculation)
            Colors.SetColor("Magenta");
            Console.WriteLine("{0}\n", appVer);
            Console.ResetColor();
            Console.Write(new string(' ', (Console.WindowWidth - begin.Length) / 2));
            Console.Write(begin);                   //prints second screen, centered
            Console.ReadKey();                      //wait for user input
        }
        static void List()
        {
            Console.Clear();
            Console.Write("\nEnter 'a' for alphabetical, 'n' for numeric, or press enter to cancel...");
            Colors.SetColor("Magenta");                     //prompts user to enter how they want list to be sorted
            string userInput = Console.ReadLine().ToLower();
            Console.ResetColor();
            Console.WriteLine("\n");                        //simple whitespace

            if (userInput == "a")           ///user entered 'a' to sort alphabetically
            {
                List<string> names = new();                 //create new temporary list for names
                for (int i = 1; i < champions.Count; i++)   //for every name in list
                {
                    names.Add(champions[i][1]);                 //add the second index (name) of each array
                }

                names.Sort();                               //sort the new list alphabetically

                foreach (string word in names)              //for each string in list 'names'
                {
                    for (int i = 1; i < champions.Count; i++)
                        //iterate through every array in 'champions' list to find what index the name in 'names' is at
                    {
                        if (champions[i].Contains(word))        //as soon as a match is found
                        {
                            Console.WriteLine("{0, 3} {1}", champions[i][0], word);
                                //write the number (that was just found) and the word (name), ALPHABETICALLY
                            break;                                  //break so not to unnecessarily continue iterating
                        }
                    }
                }
                Console.Write("\nPress enter to return...");
                Console.ReadLine();
            }
            else if (userInput == "n")      ///user entered 'n' to sort numerically
            {
                for (int i = 1; i < champions.Count; i++)       //for ever champion (string array) in list
                {
                    Console.WriteLine("{0,3} {1}", champions[i][0], champions[i][1]);
                        //prints index 0 (number) and index 1 (name), NUMERICALLY
                }
                Console.Write("\nPress enter to return...");
                Console.ReadLine();
            }
        }
        static void EnableDefaults()
        {
            Console.Clear();
            string enabledOrDisabled;
            if (enableDefaults)
            {
                enabledOrDisabled = "enabled";          //if enabled, change string to represent
            }
            else
            {
                enabledOrDisabled = "disabled";         //same as 'enabled' above
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
        }
        static void RandomChampion()
        {
            Random roll = new();                    //declares 'roll' Random variable
            champNum = roll.Next(champions.Count);  //rolls a random champion, then returns
        }

        static void Changelog()
        /// Simple changelog that can be accessed when user enters c (among other inputs).
        /// 
        /// Two string arrays are defined and assigned strings corresponding to the app version. Separate
        ///     arrays are used so they can easily be displayed seperately with different colors.
        ///     
        /// Screen is cleared, then Colors.SetColor is called to change color to yellow. The header is tabbed
        ///     using \t and displays "CHANGELOG", then skips a line.
        ///     
        /// WHILE LOOP BEGINS. Runs until 'currIndex' displays every string in each array. Changes color to
        ///     cyan, displays version (formatted to be left aligned using 21 chars), changes color to white,
        ///     and displays text. Repeats this until currIndex equals the number of objects in 'versions'.
        ///     
        /// Resets color to gray, prompts user to press any key, then reads key and returns to Main.
        {
            //QUICK SUMMARY IN ABOVE COMMENT BLOCK

            string[] versions =                 //string[] with each version so far
            {
                "(09/16/2021) alpha1: ",
                "(09/16/2021) alpha2: ",
                "(09/17/2021) alpha3: ",
                "(09/18/2021) alpha4: ",
                "(09/18/2021) v0.1.0: ",
                "(10/21/2021) v0.1.1: ",
                "(10/23/2021) v0.2.0: ",
                "(10/24/2021) v0.2.1: ",
                "(10/24/2021) v0.3.0: ",
                "(10/26/2021) v0.3.1: ",
                "(10/29/2021) v0.4.0: ",
                "(10/29/2021) v0.4.1: ",
                "(10/30/2021) v0.4.2: ",
                "(11/14/2021) v1.0.0: ",
                "(11/15/2021) v1.0.1: ",
                "(11/21/2021) v1.0.2: ",
                "(12/06/2021) v1.0.3: ",
                "(01/18/2022) v1.0.5: ",
                "(01/19/2022) v1.0.6: ",
                "(01/22/2022) v2.0.0: ",
                "(01/22/2022) v2.0.1: ",
            };
            string[] details =                  //string[] with each version's details so far
            {
                "Began re-write of Skin_Chooser_2 using arrays and proper form.",
                "Added champion name to index 0 of each array, to be displayed in header. Adjusted random " +
                    "roll to\n    not include index 0 and added more comments. Random function now works, " +
                    "and changelog can be called.",
                "Simplified dragonslayer and spiritBlossom by swapping switch statement with array that is " +
                    "called\n    based on roll value. String arrays now initialized within champions jagged " +
                    "array, also added null at index 0.",
                "Removed \"4\" from Jhin in Reply so number input works. Added extensive summaries to " +
                    "functions.\n    Fixed ds/sb conditional to require Vayne's champNum value.",
                "Created proper intro screen and functions for each ConsoleColor. Changelog and Help are no " +
                    "longer\n    colossal walls of text; they now use arrays and while loops to change " +
                    "colors and display info. Adjusted input\n    conditionals in misc. region of Reply.",
                "Removed individual color functions and replaced with Colors() function that takes a string " +
                    "\n    parameter then selects a color based on that.",
                "Added filler arrays for every champion, now using correct champion numbers. Colors now " +
                    "change\n    when they should (including skins and chromas).",
                "Added all chromas and begun adding all skins. Change chroma roll to length of array, like " +
                    "it\n    already should have been.",
                "Finished implementing correct skins. Eliminated redundancy when rolling/displaying chromas. " +
                    "added\n    option to disable defaults on champions with 1 or more skins owned.",
                "Added Coven LeBlanc, Project Vi, Subterranian Nautilus, Surprise Party Fiddlesticks, and " +
                    "Playmaker\n    Lee Sin.",
                "List of champions and skins now pulls data from .txt file. Soon will be able to update in-" +
                    "app.",
                "'List' function now exists with both numeric and alphabetical organization.",
                "Started implementation of update functionality.",
                "Completed update function. Inputting numbers now parses and uses the int value to select a " +
                    "\n    champion by number.",
                "Fixed out of bounds exception when removing skin. Removed fake skin from Vex. Adjusted while " +
                    "loop\n    in changelog to make more sense.",
                "Fixed Blitzcrank bug (changing champNum to Janna's) and added Victorious Blitzcrank chromas.",
                "Fixed various bugs with champion numbers.",
                "Added better (and more) in-line comments throughout code. Changed some variable names to be " +
                    "\n    more descriptive. UpdateChampion and UpdateSkin now have their own classes.",
                "Continued adding better comments and adjusting variable names. EnableDefaults() is much less " +
                    "\n    ridiculous and is now its own function. Began preparing to implement automatic " +
                    "champion string checking.",
                "Fully implemented champion string checking from file. Added/fixed remaining comments " +
                    "\n    throughout program. Re-organized method locations to be more sensible. Plan to " +
                    "add string-checking logic in the\n    future that automatically generates possible " +
                    "strings based on full champion name.",
                "Fixed 2 errors with numeric champion changing: can no longer include numbers less than 0 and " +
                    "\n    now properly returns swaps champions when inputting valid numbers. Adjusted some " +
                    "formatting with default\n    enabling/disabling. Adjusted formatting in update " +
                    "functions, intro, and main.",
            };

            Console.Clear();
            Colors.SetColor("Yellow");
            Console.WriteLine("\tCHANGELOG:");      //prints header in yellow
            for (int currIndex = 0; currIndex < versions.Length; currIndex++)   //for each item in string[]s above
            {
                Colors.SetColor("Cyan");
                Console.Write(string.Format(" {0, -21}", versions[currIndex])); //print version in cyan
                Colors.SetColor("White");
                Console.WriteLine(details[currIndex]);                          //print details in white
            }
            Console.ResetColor();
            Console.Write("\nPress any key to return...");
            Console.ReadKey();                          //wait for user to press a key to return
        }
        static void Help()
        /// Works very similar to Changelog; simple help screen showing commands.
        /// (See Changelog summary)
        {
            string[] commands =                 //string[] with commands
            {
                "Change mode: ",
                "Random champion: ",
                "View changelog: ",
                "This screen: ",
                "Clear screen: ",
                "Reset program: ",
                "List champions: ",
                "Update info: "
            };
            string[] instructions =             //string[] with command use instructions
            {
                "input Champion name or number (champion numbers are chronological)\n",
                "\"r\" or \"rand\" or \"random\"",
                "\"c\" or \"changelog\"",
                "\"h\" or \"help\"",
                "\"clear\"",
                "\"reset\" or \"intro\"\n",
                "\"list\"",
                "\"update\""
            };

            Console.Clear();
            Colors.SetColor("Yellow");
            Console.WriteLine("\tCOMMAND LIST:\n");     //prints header in yellow
            for (int currIndex = 0; currIndex < commands.Length; currIndex++)   //for each item in string[]s above
            {
                Colors.SetColor("Cyan");
                Console.Write(string.Format(" {0, -19}", commands[currIndex])); //prints commands in cyan
                Colors.SetColor("White");
                Console.WriteLine(instructions[currIndex]);                     //prints instructions in white
            }
            Console.ResetColor();
            Console.Write("\nPress any key to return...");
            Console.ReadKey();                          //wait for user to press a key to return
        }
    }
}
