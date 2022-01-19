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
        #region declarations
        static readonly Random roll = new();
        static readonly string path = @"C:\Users\Tanner\Documents\Visual Studio 2019\Projects\League_Randomizer\League_Randomizer\champions.txt";

        static bool first = true;
        static bool invalid;
        public static bool dragonslayer;
        public static bool spiritBlossom;
        public static bool enableDefaults;              //three publics are accessed in Champion class

        static int champNum;
        static int oldChampNum;

        public static readonly List<string[]> champions = new();    //accessed in Update_ classes
        #endregion
        static readonly string appVer = "v1.0.6";
        static void LoadChampions()
        {
            try
            {
                using StreamReader sr = new(path);      //initialize new streamreader
                string line;
                while ((line = sr.ReadLine()) != null)  //for every line in the file
                {
                    string[] item = line.Split(",");        //split each line at ',' character (each item is separated by a comma)
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
        static void Write()
        {
            try
            {
                using StreamWriter sw = new(path);      //initialize new streamwriter
                foreach (string[] champ in champions)   //for each champion's data in champions list
                {
                    for (int i = 0; i < champ.Length - 1; i++)  //do NOT include last word in list
                    {
                        sw.Write(champ[i] + ",");               //write all instances of the array EXCEPT the last with a ',' following
                    }
                    sw.Write(champ[champ.Length - 1]);      //write last instance of array WITHOUT a comma
                    sw.WriteLine("");                       //end line to begin next
                }
                Console.WriteLine("\nWrote changes to file: champions.txt");
                Console.ReadLine();                     //wait on user input
                sw.Close();                             //close streamwriter
            }
            catch (Exception e)
            {
                Console.WriteLine("champions.txt could not be opened: ");
                Console.WriteLine(e.Message);               //if there is an exception, display error message rather than crash program
            }
        }
        static void Update()
        {
            Console.Clear();
            Colors.SetColor("Yellow");
            Console.Write("What would you like to update? Enter 'champion' or 'skin'.\n\t" +
                "Enter 'write' to permanently store any changes to file...");
            Console.ResetColor();
            string userInput = Console.ReadLine().ToLower();

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
                Write();                                //call Write
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
            Console.Clear();
            Console.ResetColor();
            if (first)
            {
                LoadChampions();
                first = false;
                champNum = 76;
                Intro();
                Console.Clear();
            }

            Champion currentChampion = new(champions[champNum]);    //declare new currentChampion object and pass current champion string array

            Console.WriteLine("League Randomizer " + appVer);
            Console.WriteLine("Default skins enabled: " + enableDefaults);
            Colors.SetColor("Magenta");
            Console.WriteLine("{0} {1}\n", currentChampion.Name, currentChampion.Index);
            Console.ResetColor();
            while (!false)
            {
                Reply();
                if (invalid) { invalid = false; continue; }
                currentChampion.PrintSkin();
            }
        }
        static void Reply()
        {
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
            oldChampNum = champNum;
            Console.Write("Press enter to run...");
            Colors.SetColor("Magenta");
            string userInput = Console.ReadLine().ToLower();
            Console.ResetColor();
            bool numeric = Int32.TryParse(userInput, out int num);  //check if 'reply' is numeric and try to parse
            #region misc.
            if (userInput == "clear")
            {
                Main();
            }
            else if (userInput == "c" || userInput == "changelog")
            {
                Changelog();
                Main();
            }
            else if (userInput == "h" || userInput == "help")
            {
                Help();
                Main();
            }
            else if (userInput == "reset" || userInput == "intro")
            {
                first = true;
                champions.Clear();                      //clears champions list array to be re-written
                Main();
            }
            else if (userInput == "r" || userInput == "rand" || userInput == "random")
            {
                champNum = roll.Next(champions.Count);
                Main();
            }
            else if (userInput == "def" || userInput == "default")
            {
                EnableDefaults();                       //call defaults function
                Main();                                 //call Main to reset
            }
            else if (userInput == "list")
            {
                List();                                 //call list function
                Main();                                 //return to main
            }
            else if (userInput == "update")
            {
                Update();                               //call update function
                Main();                                 //return to main
            }
            #endregion
            #region champions
            #region A
            else if (userInput == "aa"
                || userInput == "aat"
                || userInput == "aatr"
                || userInput == "aatro"
                || userInput == "aatrox") { champNum = 114; }
            else if (userInput == "ah"
                || userInput == "ahr"
                || userInput == "ninetails"
                || userInput == "rule34"
                || userInput == "ahri") { champNum = 89; }
            else if (userInput == "aka"
                || userInput == "akal"
                || userInput == "akali") { champNum = 51; }
            else if (userInput == "aks"
                || userInput == "aksh"
                || userInput == "aksha"
                || userInput == "akshan") { champNum = 156; }
            else if (userInput == "al"
                || userInput == "ali"
                || userInput == "cow"
                || userInput == "alis"
                || userInput == "alist"
                || userInput == "alistar") { champNum = 1; }
            else if (userInput == "am"
                || userInput == "mu"
                || userInput == "amu"
                || userInput == "amum"
                || userInput == "mummy"
                || userInput == "mum"
                || userInput == "mumy"
                || userInput == "amumu") { champNum = 24; }
            else if (userInput == "ani"
                || userInput == "egg"
                || userInput == "anivegg"
                || userInput == "eggnivia"
                || userInput == "aniv"
                || userInput == "anivia") { champNum = 26; }
            else if (userInput == "ann"
                || userInput == "anni"
                || userInput == "teddy"
                || userInput == "teddybear"
                || userInput == "teddy bear"
                || userInput == "tibbers"
                || userInput == "annie") { champNum = 2; }
            else if (userInput == "aph"
                || userInput == "aphe"
                || userInput == "aphel"
                || userInput == "aphelios") { champNum = 147; }
            else if (userInput == "as"
                || userInput == "ash"
                || userInput == "slow"
                || userInput == "ashe") { champNum = 3; }
            else if (userInput == "au"
                || userInput == "aur"
                || userInput == "sol"
                || userInput == "asol"
                || userInput == "star"
                || userInput == "star guy"
                || userInput == "aurelion sol"
                || userInput == "aurelionsol") { champNum = 130; }
            else if (userInput == "az"
                || userInput == "azi"
                || userInput == "soldier"
                || userInput == "soldiers"
                || userInput == "azir") { champNum = 121; }
            #endregion
            #region B
            else if (userInput == "ba"
                || userInput == "bar"
                || userInput == "bard") { champNum = 124; }
            else if (userInput == "bl"
                || userInput == "bli"
                || userInput == "blit"
                || userInput == "blitz"
                || userInput == "robot"
                || userInput == "blitzcrank") { champNum = 32; }
            else if (userInput == "bran"
                || userInput == "fire"
                || userInput == "fire guy"
                || userInput == "brand") { champNum = 74; }
            else if (userInput == "brau"
                || userInput == "daddy"
                || userInput == "braum") { champNum = 119; }
            #endregion
            #region C
            else if (userInput == "cai"
                || userInput == "cait"
                || userInput == "caitlyn") { champNum = 67; }
            else if (userInput == "cam"
                || userInput == "cami"
                || userInput == "camil"
                || userInput == "camille") { champNum = 134; }
            else if (userInput == "cas"
                || userInput == "cass"
                || userInput == "snake"
                || userInput == "snake lady"
                || userInput == "cassiopeia") { champNum = 66; }
            else if (userInput == "cho"
                || userInput == "munch"
                || userInput == "chogath"
                || userInput == "cho gath"
                || userInput == "cho'gath") { champNum = 25; }
            else if (userInput == "co"
                || userInput == "cor"
                || userInput == "cork"
                || userInput == "corki") { champNum = 36; }
            #endregion
            #region D
            else if (userInput == "da"
                || userInput == "dar"
                || userInput == "dari"
                || userInput == "darius") { champNum = 98; }
            else if (userInput == "di"
                || userInput == "dia"
                || userInput == "dian"
                || userInput == "moon"
                || userInput == "moon lady"
                || userInput == "diana") { champNum = 102; }
            else if (userInput == "dra"
                || userInput == "drav"
                || userInput == "drave"
                || userInput == "draven") { champNum = 99; }
            else if (userInput == "dr"
                || userInput == "dr."
                || userInput == "mu"
                || userInput == "mundo"
                || userInput == "drmundo"
                || userInput == "dr mundo"
                || userInput == "dr. mundo"
                || userInput == "dr.mundo") { champNum = 33; }
            #endregion
            #region E
            else if (userInput == "ek"
                || userInput == "ekk"
                || userInput == "ekko") { champNum = 125; }
            else if (userInput == "el"
                || userInput == "spider"
                || userInput == "spider lady"
                || userInput == "elise") { champNum = 106; }
            else if (userInput == "ev"
                || userInput == "eve"
                || userInput == "evelyn"
                || userInput == "demon bitch"
                || userInput == "evelynn") { champNum = 20; }
            else if (userInput == "ez"
                || userInput == "twink"
                || userInput == "ezr"
                || userInput == "ezreal") { champNum = 47; }
            #endregion
            #region F
            else if (userInput == "fid"
                || userInput == "fidd"
                || userInput == "fiddle"
                || userInput == "spooky"
                || userInput == "fiddlesticks") { champNum = 4; }
            else if (userInput == "fio"
                || userInput == "fior"
                || userInput == "fiora") { champNum = 94; }
            else if (userInput == "fiz"
                || userInput == "fish"
                || userInput == "fizz") { champNum = 87; }
            #endregion
            #region G
            else if (userInput == "gal"
                || userInput == "gali"
                || userInput == "galio") { champNum = 57; }
            else if (userInput == "gp"
                || userInput == "gang"
                || userInput == "plank"
                || userInput == "gangplank") { champNum = 30; }
            else if (userInput == "gar"
                || userInput == "demacia"
                || userInput == "gare"
                || userInput == "garen") { champNum = 50; }
            else if (userInput == "gn"
                || userInput == "gna"
                || userInput == "gnar") { champNum = 120; }
            else if (userInput == "grag"
                || userInput == "fat"
                || userInput == "fatty"
                || userInput == "large"
                || userInput == "graga"
                || userInput == "gragas") { champNum = 44; }
            else if (userInput == "grav"
                || userInput == "grave"
                || userInput == "graves") { champNum = 85; }
            else if (userInput == "gw"
                || userInput == "gwe"
                || userInput == "gwen"
                || userInput == "scissor"
                || userInput == "scissors") { champNum = 155; }
            #endregion
            #region H
            else if (userInput == "hec"
                || userInput == "heca"
                || userInput == "horse guy"
                || userInput == "hecarim") { champNum = 96; }
            else if (userInput == "donger"
                || userInput == "dong"
                || userInput == "dinger"
                || userInput == "heim"
                || userInput == "heime"
                || userInput == "heimer"
                || userInput == "heimerdinger") { champNum = 39; }
            #endregion
            #region I
            else if (userInput == "il"
                || userInput == "ill"
                || userInput == "illa"
                || userInput == "illao"
                || userInput == "illaoi") { champNum = 128; }
            else if (userInput == "ir"
                || userInput == "ire"
                || userInput == "irel"
                || userInput == "irelia") { champNum = 64; }
            else if (userInput == "iv"
                || userInput == "ive"
                || userInput == "iver"
                || userInput == "green"
                || userInput == "tree dude"
                || userInput == "ivern") { champNum = 133; }
            #endregion
            #region J
            else if (userInput == "ja"
                || userInput == "jan"
                || userInput == "jana"
                || userInput == "janna") { champNum = 34; }
            else if (userInput == "j4"
                || userInput == "jar"
                || userInput == "jarv"
                || userInput == "jarvan"
                || userInput == "jarvan4"
                || userInput == "jarvaniv") { champNum = 71; }
            else if (userInput == "jax"
                || userInput == "realweapon"
                || userInput == "real weapon") { champNum = 5; }
            else if (userInput == "jay"
                || userInput == "jayc"
                || userInput == "jayce") { champNum = 100; }
            else if (userInput == "jh"
                || userInput == "jhi"
                || userInput == "four"
                || userInput == "jhin") { champNum = 129; }
            else if (userInput == "ji"
                || userInput == "jin"
                || userInput == "jinx") { champNum = 116; }
            #endregion
            #region K
            else if (userInput == "kai"
                || userInput == "kaisa"
                || userInput == "kai sa"
                || userInput == "kai'sa") { champNum = 140; }
            else if (userInput == "kal"
                || userInput == "kali"
                || userInput == "kalis"
                || userInput == "kalist"
                || userInput == "kalista") { champNum = 122; }
            else if (userInput == "karm"
                || userInput == "karma") { champNum = 69; }
            else if (userInput == "kart"
                || userInput == "karth"
                || userInput == "karthu"
                || userInput == "karthus") { champNum = 23; }
            else if (userInput == "kas"
                || userInput == "kass"
                || userInput == "16"
                || userInput == "sixteen"
                || userInput == "level16"
                || userInput == "level 16"
                || userInput == "kassadin") { champNum = 29; }
            else if (userInput == "kat"
                || userInput == "kata"
                || userInput == "reset"
                || userInput == "resets"
                || userInput == "katarina") { champNum = 37; }
            else if (userInput == "kayl"
                || userInput == "kayle") { champNum = 6; }
            else if (userInput == "kany"
                || userInput == "kane"
                || userInput == "rhaast"
                || userInput == "rhast"
                || userInput == "darkin"
                || userInput == "kayn") { champNum = 137; }
            else if (userInput == "ke"
                || userInput == "ken"
                || userInput == "kenn"
                || userInput == "kenne"
                || userInput == "kennen") { champNum = 49; }
            else if (userInput == "kha"
                || userInput == "khazix"
                || userInput == "k6"
                || userInput == "bug"
                || userInput == "kha zix"
                || userInput == "kha'zix") { champNum = 105; }
            else if (userInput == "ki"
                || userInput == "kin"
                || userInput == "kind"
                || userInput == "kindr"
                || userInput == "kindre"
                || userInput == "kindred") { champNum = 127; }
            else if (userInput == "kl"
                || userInput == "kle"
                || userInput == "kled") { champNum = 132; }
            else if (userInput == "kog"
                || userInput == "kogm"
                || userInput == "kogmaw"
                || userInput == "kog maw"
                || userInput == "kog'maw") { champNum = 54; }
            #endregion
            #region L
            else if (userInput == "lb"
                || userInput == "leb"
                || userInput == "lebl"
                || userInput == "leblanc") { champNum = 63; }
            else if (userInput == "lee"
                || userInput == "sin"
                || userInput == "leesin"
                || userInput == "lee sin") { champNum = 73; }
            else if (userInput == "leo"
                || userInput == "leon"
                || userInput == "leona") { champNum = 79; }
            else if (userInput == "lil"
                || userInput == "lili"
                || userInput == "deer"
                || userInput == "lilia"
                || userInput == "lillia") { champNum = 149; }
            else if (userInput == "lis"
                || userInput == "liss"
                || userInput == "ice queen"
                || userInput == "lissandra") { champNum = 113; }
            else if (userInput == "luc"
                || userInput == "luci"
                || userInput == "lucia"
                || userInput == "lucian") { champNum = 115; }
            else if (userInput == "lul"
                || userInput == "poly"
                || userInput == "squirrel"
                || userInput == "polymorph"
                || userInput == "lulu") { champNum = 95; }
            else if (userInput == "lux"
                || userInput == "light lady") { champNum = 62; }
            #endregion
            #region M
            else if (userInput == "yi"
                || userInput == "mas"
                || userInput == "master"
                || userInput == "masteryi"
                || userInput == "master yi") { champNum = 7; }
            else if (userInput == "malp"
                || userInput == "malph"
                || userInput == "rock"
                || userInput == "mountain"
                || userInput == "malphite") { champNum = 35; }
            else if (userInput == "malz"
                || userInput == "malza"
                || userInput == "press userInput"
                || userInput == "malzahar") { champNum = 52; }
            else if (userInput == "mao"
                || userInput == "maok"
                || userInput == "tree"
                || userInput == "maokai") { champNum = 70; }
            else if (userInput == "mf"
                || userInput == "mis"
                || userInput == "for"
                || userInput == "fort"
                || userInput == "fortune"
                || userInput == "miss"
                || userInput == "missfortune"
                || userInput == "miss fortune") { champNum = 59; }
            else if (userInput == "mord"
                || userInput == "morde"
                || userInput == "kaiser"
                || userInput == "death realm"
                || userInput == "mordekaiser") { champNum = 46; }
            else if (userInput == "morg"
                || userInput == "morga"
                || userInput == "morgan"
                || userInput == "black shield"
                || userInput == "morgana") { champNum = 8; }
            #endregion
            #region N
            else if (userInput == "nam"
                || userInput == "nami") { champNum = 108; }
            else if (userInput == "nas"
                || userInput == "nasu"
                || userInput == "susan"
                || userInput == "nasus") { champNum = 38; }
            else if (userInput == "naut"
                || userInput == "nauti"
                || userInput == "nautil"
                || userInput == "nautilu"
                || userInput == "anchor"
                || userInput == "nautilus") { champNum = 93; }
            else if (userInput == "ne"
                || userInput == "nee"
                || userInput == "neek"
                || userInput == "lesbian"
                || userInput == "neeko") { champNum = 142; }
            else if (userInput == "ni"
                || userInput == "nid"
                || userInput == "nida"
                || userInput == "cat lady"
                || userInput == "nidalee") { champNum = 42; }
            else if (userInput == "no"
                || userInput == "noc"
                || userInput == "noct"
                || userInput == "noctu"
                || userInput == "nightmare"
                || userInput == "nocturne") { champNum = 72; }
            else if (userInput == "nu"
                || userInput == "willump"
                || userInput == "nunuandwillump"
                || userInput == "nunu and willump"
                || userInput == "nunu&willump"
                || userInput == "nunu & willump"
                || userInput == "nunu") { champNum = 9; }
            #endregion
            #region O
            else if (userInput == "ol"
                || userInput == "ola"
                || userInput == "olaf") { champNum = 53; }
            else if (userInput == "ori"
                || userInput == "oria"
                || userInput == "orian"
                || userInput == "oriana"
                || userInput == "orianna") { champNum = 77; }
            else if (userInput == "orn"
                || userInput == "horn"
                || userInput == "ornn") { champNum = 138; }
            #endregion
            #region P
            else if (userInput == "pa"
                || userInput == "pan"
                || userInput == "pant"
                || userInput == "panth"
                || userInput == "pantheon") { champNum = 45; }
            else if (userInput == "pop"
                || userInput == "popp"
                || userInput == "popy"
                || userInput == "poppy") { champNum = 43; }
            else if (userInput == "py"
                || userInput == "pyk"
                || userInput == "pyke") { champNum = 141; }
            #endregion
            #region Q
            else if (userInput == "qi"
                || userInput == "qiy"
                || userInput == "qiya"
                || userInput == "qiyan"
                || userInput == "qiyana") { champNum = 145; }
            else if (userInput == "qu"
                || userInput == "qui"
                || userInput == "valor"
                || userInput == "bird lady"
                || userInput == "quin"
                || userInput == "quinn") { champNum = 111; }
            #endregion
            #region R
            else if (userInput == "rak"
                || userInput == "rakan") { champNum = 135; }
            else if (userInput == "ram"
                || userInput == "ramm"
                || userInput == "ok"
                || userInput == "rammus") { champNum = 29; }
            else if (userInput == "rek"
                || userInput == "reks"
                || userInput == "reksai"
                || userInput == "rek sai"
                || userInput == "rek'sai") { champNum = 123; }
            else if (userInput == "rel"
                || userInput == "rell") { champNum = 153; }
            else if (userInput == "rene"
                || userInput == "croc"
                || userInput == "crocodile"
                || userInput == "renek"
                || userInput == "renekton") { champNum = 68; }
            else if (userInput == "reng"
                || userInput == "renga"
                || userInput == "knelse ifecat"
                || userInput == "knelse ife cat"
                || userInput == "rengar") { champNum = 103; }
            else if (userInput == "riv"
                || userInput == "rive"
                || userInput == "riven") { champNum = 83; }
            else if (userInput == "rum"
                || userInput == "rumb"
                || userInput == "rumbl"
                || userInput == "rumble") { champNum = 75; }
            else if (userInput == "ry"
                || userInput == "ryz"
                || userInput == "rework"
                || userInput == "reworks"
                || userInput == "ryze") { champNum = 10; }
            #endregion
            #region S
            else if (userInput == "sa"
                || userInput == "sam"
                || userInput == "sami"
                || userInput == "samir"
                || userInput == "samira") { champNum = 151; }
            else if (userInput == "sej"
                || userInput == "seju"
                || userInput == "sejuani") { champNum = 91; }
            else if (userInput == "sen"
                || userInput == "senn"
                || userInput == "senna") { champNum = 146; }
            else if (userInput == "ser"
                || userInput == "sera"
                || userInput == "phine"
                || userInput == "sona2.0"
                || userInput == "sona 2"
                || userInput == "sona 2.0"
                || userInput == "serap"
                || userInput == "seraph"
                || userInput == "seraphi"
                || userInput == "seraphin"
                || userInput == "seraphine") { champNum = 152; }
            else if (userInput == "set"
                || userInput == "hot"
                || userInput == "sett") { champNum = 148; }
            else if (userInput == "sha"
                || userInput == "shac"
                || userInput == "shaco") { champNum = 40; }
            else if (userInput == "she"
                || userInput == "shen") { champNum = 48; }
            else if (userInput == "shy"
                || userInput == "shyv"
                || userInput == "dragon lady"
                || userInput == "shyvana") { champNum = 86; }
            else if (userInput == "sin"
                || userInput == "sing"
                || userInput == "singe"
                || userInput == "singed") { champNum = 18; }
            else if (userInput == "sio"
                || userInput == "sion") { champNum = 11; }
            else if (userInput == "siv"
                || userInput == "sivi"
                || userInput == "sivir") { champNum = 12; }
            else if (userInput == "sk"
                || userInput == "ska"
                || userInput == "skar"
                || userInput == "skarn"
                || userInput == "skarner") { champNum = 81; }
            else if (userInput == "son"
                || userInput == "sona") { champNum = 60; }
            else if (userInput == "sor"
                || userInput == "sora"
                || userInput == "raka"
                || userInput == "soraka") { champNum = 13; }
            else if (userInput == "sw"
                || userInput == "swa"
                || userInput == "swai"
                || userInput == "bird guy"
                || userInput == "swain") { champNum = 61; }
            else if (userInput == "syl"
                || userInput == "syla"
                || userInput == "chains"
                || userInput == "sylas") { champNum = 143; }
            else if (userInput == "syn"
                || userInput == "synd"
                || userInput == "syndr"
                || userInput == "syndra") { champNum = 104; }
            #endregion
            #region T
            else if (userInput == "tah"
                || userInput == "tahm"
                || userInput == "ken"
                || userInput == "kenc"
                || userInput == "kench"
                || userInput == "frog"
                || userInput == "tahmkench"
                || userInput == "tahm kench") { champNum = 126; }
            else if (userInput == "tali"
                || userInput == "taliy"
                || userInput == "taliya"
                || userInput == "rock lady"
                || userInput == "taliyah") { champNum = 131; }
            else if (userInput == "talo"
                || userInput == "firstblood"
                || userInput == "first blood"
                || userInput == "25%"
                || userInput == "talon") { champNum = 82; }
            else if (userInput == "tar"
                || userInput == "tari"
                || userInput == "taric") { champNum = 31; }
            else if (userInput == "tee"
                || userInput == "teem"
                || userInput == "aids"
                || userInput == "teemo") { champNum = 14; }
            else if (userInput == "th"
                || userInput == "thr"
                || userInput == "thre"
                || userInput == "thres"
                || userInput == "thresh") { champNum = 110; }
            else if (userInput == "tri"
                || userInput == "tris"
                || userInput == "trist"
                || userInput == "trista"
                || userInput == "tristan"
                || userInput == "tristana") { champNum = 15; }
            else if (userInput == "tru"
                || userInput == "trun"
                || userInput == "trund"
                || userInput == "trundl"
                || userInput == "trundle") { champNum = 65; }
            else if (userInput == "try"
                || userInput == "tryn"
                || userInput == "trynd"
                || userInput == "trynda"
                || userInput == "tryndamere") { champNum = 22; }
            else if (userInput == "tf"
                || userInput == "fate"
                || userInput == "twistedfate"
                || userInput == "twisted fate") { champNum = 16; }
            else if (userInput == "tw"
                || userInput == "rat"
                || userInput == "sewer rat"
                || userInput == "twi"
                || userInput == "twit"
                || userInput == "twitc"
                || userInput == "twitch") { champNum = 21; }
            #endregion
            #region U
            else if (userInput == "ud"
                || userInput == "udy"
                || userInput == "udyr") { champNum = 41; }
            else if (userInput == "ur"
                || userInput == "urg"
                || userInput == "urgo"
                || userInput == "urgot") { champNum = 58; }
            #endregion
            #region V
            else if (userInput == "var"
                || userInput == "varu"
                || userInput == "varus") { champNum = 97; }
            else if (userInput == "vn"
                || userInput == "vay"
                || userInput == "vayn"
                || userInput == "vayne") { champNum = 76; }
            else if (userInput == "vei"
                || userInput == "veig"
                || userInput == "veiga"
                || userInput == "veigar") { champNum = 28; }
            else if (userInput == "vel"
                || userInput == "eye"
                || userInput == "eye of sauron"
                || userInput == "velk"
                || userInput == "velko"
                || userInput == "velkoz"
                || userInput == "vel koz"
                || userInput == "vel'koz") { champNum = 118; }
            else if (userInput == "vex") { champNum = 157; }
            else if (userInput == "6"
                || userInput == "six"
                || userInput == "vi") { champNum = 109; }
            else if (userInput == "vie"
                || userInput == "vieg"
                || userInput == "viego"
                || userInput == "bork") { champNum = 154; }
            else if (userInput == "vik"
                || userInput == "vikt"
                || userInput == "vikto"
                || userInput == "viktor") { champNum = 90; }
            else if (userInput == "blood"
                || userInput == "vla"
                || userInput == "vlad"
                || userInput == "vladi"
                || userInput == "vladim"
                || userInput == "vladimi"
                || userInput == "vladimir") { champNum = 56; }
            else if (userInput == "vo"
                || userInput == "vb"
                || userInput == "vol"
                || userInput == "voli"
                || userInput == "volib"
                || userInput == "bear"
                || userInput == "volibear") { champNum = 88; }
            #endregion
            #region W
            else if (userInput == "ww"
                || userInput == "war"
                || userInput == "wick"
                || userInput == "warwick") { champNum = 17; }
            else if (userInput == "wu"
                || userInput == "wuk"
                || userInput == "wuko"
                || userInput == "wukon"
                || userInput == "wukong") { champNum = 80; }
            #endregion
            #region X
            else if (userInput == "xa"
                || userInput == "xay"
                || userInput == "xaya"
                || userInput == "xayah") { champNum = 136; }
            else if (userInput == "xe"
                || userInput == "xer"
                || userInput == "xerath"
                || userInput == "xerat") { champNum = 84; }
            else if (userInput == "xi"
                || userInput == "xin"
                || userInput == "xinz"
                || userInput == "zhao"
                || userInput == "xinzhao"
                || userInput == "xin zhao") { champNum = 55; }
            #endregion
            #region Y
            else if (userInput == "ya"
                || userInput == "yas"
                || userInput == "yasu"
                || userInput == "yasuo") { champNum = 117; }
            else if (userInput == "yon"
                || userInput == "better yasuo"
                || userInput == "yone") { champNum = 150; }
            else if (userInput == "yor"
                || userInput == "yori"
                || userInput == "yoric"
                || userInput == "yorick") { champNum = 78; }
            else if (userInput == "yu"
                || userInput == "damn cat"
                || userInput == "yum"
                || userInput == "yumi"
                || userInput == "yummi"
                || userInput == "yuumi") { champNum = 144; }
            #endregion
            #region Z
            else if (userInput == "za"
                || userInput == "zac") { champNum = 112; }
            else if (userInput == "ze"
                || userInput == "zed") { champNum = 107; }
            else if (userInput == "zig"
                || userInput == "bomb"
                || userInput == "bombs"
                || userInput == "zigs"
                || userInput == "zigg"
                || userInput == "ziggs") { champNum = 92; }
            else if (userInput == "zil"
                || userInput == "zile"
                || userInput == "zilea"
                || userInput == "groovy"
                || userInput == "zilean") { champNum = 19; }
            else if (userInput == "zo"
                || userInput == "cancer"
                || userInput == "aids"
                || userInput == "zoe") { champNum = 139; }
            else if (userInput == "zy"
                || userInput == "zyr"
                || userInput == "plant"
                || userInput == "plants"
                || userInput == "plant lady"
                || userInput == "zyra") { champNum = 101; }
            #endregion
            #endregion
            //else if (IsChampionChanged())
                //inside ^ method, checks every available string in champion_strings and returns a bool
                //if true, calls Main() right away
                //can remove if statement at the bottom with this as well, and oldChampNum won't be needed anymore
            #region numbers
            else if (numeric)                               //if 'userInput' is numeric
            {
                if (num <= champions.Count - 1)
                {
                    champNum = num;                         //if 'num' is a valid champion number, change champNum to num
                }
                else
                {
                    Colors.SetColor("Red");
                    Console.WriteLine("{0} is not a valid champion number.\n", num);
                    Console.ResetColor();
                    invalid = true;                         //else acknowledge that it's not valid and change invalid to true
                }
            }
            #endregion
            #region ds/sb
            else if ((userInput == "ds") && champNum == 76) { dragonslayer = true; }
            else if ((userInput == "sb") && champNum == 76) { spiritBlossom = true; }
            #endregion
            #region invalid
            else if (userInput.Length > 0)
            {
                invalid = true;
                Colors.SetColor("Red");
                Console.WriteLine("Invalid input. Please try again.\n");
                Console.ResetColor();
            }
            #endregion
            if (champNum != oldChampNum) { Main(); }
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
            string intro = "ifound1dollar's League of Legends Randomizer ";
            string begin = "Press any key to begin...";
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n");
            Colors.SetColor("Cyan");
            Console.Write(new string(' ', (Console.WindowWidth - intro.Length) / 2));
            Console.Write(intro);
            Colors.SetColor("Magenta");
            Console.WriteLine("{0}\n", appVer);
            Console.ResetColor();
            Console.Write(new string(' ', (Console.WindowWidth - begin.Length) / 2));
            Console.Write(begin);
            Console.ReadKey();
        }
        static void List()
        {
            Console.Clear();
            Console.Write("\nEnter 'a' for alphabetical, 'n' for numeric, or press enter to cancel...");
            Colors.SetColor("Magenta");
            string userInput = Console.ReadLine().ToLower();
            Console.ResetColor();
            Console.WriteLine("\n");                        //simple whitespace

            if (userInput == "a")       ///user entered 'a' to sort alphabetically
            {
                List<string> names = new();                 //create new temporary list for names
                for (int i = 1; i < champions.Count; i++)   //for every name in list
                {
                    names.Add(champions[i][1]);             //add the second index (name) of each array
                }

                names.Sort();                               //sort the new list alphabetically

                foreach (string word in names)              //for each string in list 'names'
                {
                    for (int i = 1; i < champions.Count; i++)   //go through every array in 'champions' list to find what index the name in 'names' is at
                    {
                        if (champions[i].Contains(word))        //as soon as a match is found
                        {
                            Console.WriteLine("{0, 3} {1}", champions[i][0], word);
                            //write the number (that was just found) and the word (name), ALPHABETICALLY
                        }
                    }
                }
                Console.Write("\nPress enter to return...");
                Console.ReadLine();
            }
            else if (userInput == "n")
            {
                for (int i = 1; i < champions.Count; i++)       //for ever champion (string array) in list
                {
                    Console.WriteLine("{0,3} {1}", champions[i][0], champions[i][1]);
                    //displays index 0 (number) and index 1 (name), NUMERICALLY
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
                enabledOrDisabled = "enabled";
            }
            else
            {
                enabledOrDisabled = "disabled";
            }

            Colors.SetColor("Yellow");
            Console.WriteLine("Default skins are currently {0}.\n\tEnter 'enable' or 'disable' to update.",
                enabledOrDisabled);
            string userInput = Console.ReadLine().ToLower();
            if (userInput == "enable" || userInput == "e")                     //if user hits 'Y' key
            {
                Console.WriteLine("\nEnabled defaults.");
                enableDefaults = true;
            }
            else if (userInput == "disable" || userInput == "d")
            {
                Console.WriteLine("\nDisabled defaults.");
                enableDefaults = false;
            }
            else
            {
                Colors.SetColor("Red");
                Console.WriteLine("\nInvalid input, cancelling.");
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        static void Changelog()
        /// Simple changelog that can be accessed when user enters c (among other inputs).
        ///
        /// Integer 'ver' is defined and assigned 0 immediately, to be used in a while loop later (see below).
        /// 
        /// Two string arrays are defined and assigned strings corresponding to the app version. Separate
        ///     arrays are used so they can easily be displayed seperately with different colors.
        ///     
        /// Screen is cleared, then Yellow is called to change ConsoleColor. The header is tabbed using \t and
        ///     displays "CHANGELOG", then skips a line.
        ///     
        /// WHILE LOOP BEGINS. Runs until 'ver' displays every string in each array. Calls Cyan, displays
        ///     version (formatted to be left aligned using 21 chars), calls White, and displays text. Repeats
        ///     this until 'ver' equals the number of objects in 'versions'.
        ///     
        /// Resets color to gray, prompts user to press any key, then reads key and returns to Main.
        {
            string[] versions =
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
            };
            string[] details =
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
            };

            Console.Clear();
            Colors.SetColor("Yellow");
            Console.WriteLine("\tCHANGELOG:");
            for (int currIndex = 0; currIndex < versions.Length; currIndex++)
            {
                Colors.SetColor("Cyan");
                Console.Write(string.Format(" {0, -21}", versions[currIndex]));
                Colors.SetColor("White");
                Console.WriteLine(details[currIndex]);
            }
            Console.ResetColor();
            Console.Write("\nPress any key to return...");
            Console.ReadKey();
        }
        static void Help()
        /// Works very similar to Changelog; simple help screen showing commands.
        /// (See Changelog summary)
        ///
        {
            string[] commands =
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
            string[] instructions =
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
            Console.WriteLine("\tCOMMAND LIST:\n");
            for (int currIndex = 0; currIndex < commands.Length; currIndex++)
            {
                Colors.SetColor("Cyan");
                Console.Write(string.Format(" {0, -19}", commands[currIndex]));
                Colors.SetColor("White");
                Console.WriteLine(instructions[currIndex]);
            }
            Console.ResetColor();
            Console.Write("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
