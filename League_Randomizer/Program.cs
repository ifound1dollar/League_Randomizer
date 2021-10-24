using System;
using System.Collections.Generic;

namespace League_Randomizer
{
    class Program   ///add 'include default' setting to program
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
        #region init
        static readonly Random roll = new();

        static bool first = true;
        static bool invalid;
        static bool dragonslayer;
        static bool spiritBlossom;

        static int champNum;
        static int oldChampNum;
        static int skinNum;
        static int oldSkin;
        static int chromaNum;
        static int oldChroma;

        static readonly string[][] champions =
        {
            new string[] { "null" },    //0
            new string[] { "ALISTAR", "Default" },                              ///
            new string[] { "ANNIE", "Annie-versary", "Prom Queen" },            ///
            new string[] { "ASHE", "Sherwood Forest", "Default" },              ///
            new string[] { "FIDDLESTICKS", "Fiddle Me Timbers", "Default" },    ///
            new string[] { "JAX", "Warden", "Default" },                        ///
            new string[] { "KAYLE", "Riot", "Transcendent (Unmasked)" },        ///
            new string[] { "MASTER YI", "Default" },                            ///
            new string[] { "MORGANA", "Ghost Bride", "Blade Mistress" },        ///
            new string[] { "NUNU & WILLUMP", "Workshop", "Papercraft " },       ///
            new string[] { "RYZE", "Dark Crystal", "Default" },     //10        ///
            new string[] { "SION", "Default" },                                 ///
            new string[] { "SIVIR", "Snowstorm", "Default" },                   ///
            new string[] { "SORAKA", "Default" },                               ///
            new string[] { "TEEMO", "Panda", "Default" },                       ///
            new string[] { "TRISTANA", "Bewitching", "Riot Girl",
                "Rocket Girl " },                                               ///
            new string[] { "TWISTED FATE", "Default" },                         ///
            new string[] { "WARWICK", "Grey " },                                ///
            new string[] { "SINGED", "Hextech", "Mad Scientist" },              ///
            new string[] { "ZILEAN", "Default" },                               ///
            new string[] { "EVELYNN", "Safecracker", "Default" },   //20        ///
            new string[] { "TWITCH", "Medieval ", "Whistler Village" },         ///
            new string[] { "TRYNDAMERE", "Default" },                           ///
            new string[] { "KARTHUS", "Fnatic", "Default" },                    ///
            new string[] { "AMUMU", "Emumu", "Default" },                       ///
            new string[] { "CHO'GATH", "Jurassic", "Loch Ness" },               ///
            new string[] { "ANIVIA", "Blackfrost", "Default" },                 ///
            new string[] { "RAMMUS", "Default" },                               ///
            new string[] { "VEIGAR", "White Mage", "Curling",
                "Superb Villain", "Final Boss" },                               ///
            new string[] { "KASSADIN", "Default" },                             ///
            new string[] { "GANGPLANK", "Default" },                //30        ///
            new string[] { "TARIC", "Default" },                                ///
            new string[] { "BLITZCRANK", "Boom Boom", "Default" },              ///
            new string[] { "DR. MUNDO", "Default" },                            ///
            new string[] { "JANNA", "Default" },                                ///
            new string[] { "MALPHITE", "Mecha " },                  //35        ///
            new string[] { "CORKI", "Urfrider", "Default" },                    ///
            new string[] { "KATARINA", "Kitty Kat", "Default" },                ///
            new string[] { "NASUS", "Archduke", "Default" },                    ///
            new string[] { "HEIMERDINGER", "Default" },                         ///
            new string[] { "SHACO", "Default" },                    //40        ///
            new string[] { "UDYR", "Default" },                                 ///
            new string[] { "NIDALEE", "Default" },                              ///
            new string[] { "POPPY", "Default" },                                ///
            new string[] { "GRAGAS", "Default" },                               ///
            new string[] { "PANTHEON", "Default" },                 //45        ///
            new string[] { "MORDEKAISER", "Default" },                          ///
            new string[] { "EZREAL", "Pulsefire", "Default" },                  ///
            new string[] { "SHEN", "Default" },                                 ///
            new string[] { "KENNEN", "Default" },                               ///
            new string[] { "GAREN", "Default" },                    //50        ///
            new string[] { "AKALI", "Blood Moon", "Default" },                  ///
            new string[] { "MALZAHAR", "Overlord", "Default" },                 ///
            new string[] { "OLAF", "Default" },                                 ///
            new string[] { "KOG'MAW", "Reindeer", "Default" },                  ///
            new string[] { "XIN ZHAO", "Default" },                             ///
            new string[] { "VLADIMIR", "Blood Lord", "Default" },               ///
            new string[] { "GALIO", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "URGOT", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "MISS FORTUNE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "SONA", "tw1", "tw2", "tw3", "tw4" },    //60
            new string[] { "SWAIN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "LUX", "Lunar Empress", "Steel Legion" },            ///
            new string[] { "LEBLANC", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "IRELIA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "TRUNDLE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "CASSIOPEIA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "CAITLYN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "RENEKTON", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "KARMA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "MAOKAI", "tw1", "tw2", "tw3", "tw4" },  //70
            new string[] { "JARVAN IV", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "NOCTURNE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "LEE SIN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "BRAND", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "RUMBLE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "VAYNE", "Vindicator", "Aristocrat",
                "Dragonslayer ", "Heartseeker", "SKT T1", "Arclight",
                "Soulstealer", "Firecracker Prestige Edition",
                "Spirit Blossom "},                                 //76        ///
            new string[] { "ORIANNA", "TPA", "Winter Wonder", "Victorious" },   ///
            new string[] { "YORICK", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "LEONA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "WUKONG", "Lancer Stratus", "Underworld" },  //80    ///
            new string[] { "SKARNER", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "TALON", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "RIVEN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "XERATH", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "GRAVES", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "SHYVANA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "FIZZ", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "VOLIBEAR", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "AHRI", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "VIKTOR", "tw1", "tw2", "tw3", "tw4" },  //90
            new string[] { "SEJUANI", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "ZIGGS", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "NAUTILUS", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "FIORA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "LULU", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "HECARIM", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "VARUS", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "DARIUS", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "DRAVEN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "JAYCE", "tw1", "tw2", "tw3", "tw4" },   //100
            new string[] { "ZYRA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "DIANA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "RENGAR", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "SYNDRA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "KHA'ZIX", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "ELISE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "ZED", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "NAMI", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "VI", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "THRESH", "tw1", "tw2", "tw3", "tw4" },  //110
            new string[] { "QUINN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "ZAC", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "LISSANDRA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "AATROX", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "LUCIAN", "Hired Gun", "Victorious " },          ///
            new string[] { "JINX", "Firecracker "},                         ///
            new string[] { "YASUO", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "VEL'KOZ", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "BRAUM", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "GNAR", "tw1", "tw2", "tw3", "tw4" },    //120
            new string[] { "AZIR", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "KALISTA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "REK'SAI", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "BARD", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "EKKO", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "TAHM KENCH", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "KINDRED", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "ILLAOI", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "JHIN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "AURELION SOL", "tw1", "tw2", "tw3", "tw4" },    //130
            new string[] { "TALIYAH", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "KLED", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "IVERN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "CAMILLE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "RAKAN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "XAYAH", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "KAYN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "ORNN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "ZOE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "KAI'SA", "tw1", "tw2", "tw3", "tw4" },  //140
            new string[] { "PYKE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "NEEKO", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "SYLAS", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "YUUMI", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "QIYANA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "SENNA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "APHELIOS", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "SETT", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "LILLIA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "YONE", "tw1", "tw2", "tw3", "tw4" },    //150
            new string[] { "SAMIRA", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "SERAPHINE", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "RELL", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "VIEGO", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "GWEN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "AKSHAN", "tw1", "tw2", "tw3", "tw4" },
            new string[] { "VEX", "tw1", "tw2", "tw3", "tw4" },
        };
        #endregion
        static readonly string appVer = "v0.2.1";

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
            if (first)
            {
                first = false;
                champNum = 76;
                Intro();
                Console.Clear();
            }
            oldSkin = oldChroma = -1;
            Console.WriteLine("League Randomizer " + appVer);
            Console.WriteLine(champions[champNum][0]);
            Console.WriteLine(champNum);
            while (!false)
            {
                Reply();
                if (invalid) { invalid = false; continue; }
                Skin();
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
            string r = Console.ReadLine().ToLower();
            #region misc.
            if (r == "clear"
                || r == "cle"
                || r == "clea") { Main(); }
            else if (r == "c"
                || r == "cl"
                || r == "changelog") { Changelog(); }
            else if (r == "h"
                || r == "hel"
                || r == "help") { Help(); }
            else if (r == "intro" || r == "reset")
            {
                first = true;
                Main();
            }
            else if (r == "r" || r == "rand" || r == "random")
            {
                champNum = roll.Next(champions.Length);
                Main();
            }
            #endregion
            #region champions
            #region A
            else if (r == "aa"
                || r == "aat"
                || r == "aatr"
                || r == "aatro"
                || r == "aatrox" || r == "114") { champNum = 114; }
            else if (r == "ah"
                || r == "ahr"
                || r == "ninetails"
                || r == "rule34"
                || r == "ahri" || r == "89") { champNum = 89; }
            else if (r == "ak"
                || r == "aka"
                || r == "akal"
                || r == "akali" || r == "51") { champNum = 51; }
            else if (r == "al"
                || r == "ali"
                || r == "cow"
                || r == "alis"
                || r == "alist"
                || r == "alistar" || r == "1") { champNum = 1; }
            else if (r == "am"
                || r == "mu"
                || r == "amu"
                || r == "amum"
                || r == "mummy"
                || r == "mum"
                || r == "mumy"
                || r == "amumu" || r == "24") { champNum = 24; }
            else if (r == "ani"
                || r == "egg"
                || r == "anivegg"
                || r == "eggnivia"
                || r == "aniv"
                || r == "anivia" || r == "26") { champNum = 26; }
            else if (r == "ann"
                || r == "anni"
                || r == "teddy"
                || r == "teddybear"
                || r == "teddy bear"
                || r == "tibbers"
                || r == "annie" || r == "2") { champNum = 2; }
            else if (r == "aph"
                || r == "aphe"
                || r == "aphel"
                || r == "aphelios" || r == "147") { champNum = 147; }
            else if (r == "as"
                || r == "ash"
                || r == "slow"
                || r == "ashe" || r == "3") { champNum = 3; }
            else if (r == "au"
                || r == "aur"
                || r == "sol"
                || r == "asol"
                || r == "star"
                || r == "star guy"
                || r == "aurelion sol"
                || r == "aurelionsol" || r == "130") { champNum = 130; }
            else if (r == "az"
                || r == "azi"
                || r == "soldier"
                || r == "soldiers"
                || r == "azir" || r == "121") { champNum = 121; }
            #endregion
            #region B
            else if (r == "ba"
                || r == "bar"
                || r == "bard") { champNum = 124; }
            else if (r == "bl"
                || r == "bli"
                || r == "blit"
                || r == "blitz"
                || r == "robot"
                || r == "blitzcrank") { champNum = 34; }
            else if (r == "bran"
                || r == "fire"
                || r == "fire guy"
                || r == "brand") { champNum = 74; }
            else if (r == "brau"
                || r == "daddy"
                || r == "braum") { champNum = 119; }
            #endregion
            #region C
            else if (r == "cai"
                || r == "cait"
                || r == "caitlyn") { champNum = 67; }
            else if (r == "cam"
                || r == "cami"
                || r == "camil"
                || r == "camille") { champNum = 134; }
            else if (r == "cas"
                || r == "cass"
                || r == "snake"
                || r == "snake lady"
                || r == "cassiopeia") { champNum = 66; }
            else if (r == "cho"
                || r == "munch"
                || r == "chogath"
                || r == "cho gath"
                || r == "cho'gath") { champNum = 25; }
            else if (r == "co"
                || r == "cor"
                || r == "cork"
                || r == "corki") { champNum = 37; }
            #endregion
            #region D
            else if (r == "da"
                || r == "dar"
                || r == "dari"
                || r == "darius") { champNum = 98; }
            else if (r == "di"
                || r == "dia"
                || r == "dian"
                || r == "moon"
                || r == "moon lady"
                || r == "diana") { champNum = 102; }
            else if (r == "dra"
                || r == "drav"
                || r == "drave"
                || r == "draven") { champNum = 99; }
            else if (r == "dr"
                || r == "dr."
                || r == "mu"
                || r == "mundo"
                || r == "drmundo"
                || r == "dr mundo"
                || r == "dr. mundo"
                || r == "dr.mundo") { champNum = 35; }
            #endregion
            #region E
            else if (r == "ek"
                || r == "ekk"
                || r == "ekko") { champNum = 125; }
            else if (r == "el"
                || r == "spider"
                || r == "spider lady"
                || r == "elise") { champNum = 106; }
            else if (r == "ev"
                || r == "eve"
                || r == "evelyn"
                || r == "demon bitch"
                || r == "evelynn") { champNum = 20; }
            else if (r == "ez"
                || r == "twink"
                || r == "ezr"
                || r == "ezreal") { champNum = 47; }
            #endregion
            #region F
            else if (r == "fid"
                || r == "fidd"
                || r == "fiddle"
                || r == "spooky"
                || r == "fiddlesticks") { champNum = 4; }
            else if (r == "fio"
                || r == "fior"
                || r == "fiora") { champNum = 94; }
            else if (r == "fiz"
                || r == "fish"
                || r == "fizz") { champNum = 87; }
            #endregion
            #region G
            else if (r == "gal"
                || r == "gali"
                || r == "galio") { champNum = 57; }
            else if (r == "gp"
                || r == "gang"
                || r == "plank"
                || r == "gangplank") { champNum = 30; }
            else if (r == "gar"
                || r == "demacia"
                || r == "gare"
                || r == "garen") { champNum = 50; }
            else if (r == "gn"
                || r == "gna"
                || r == "gnar") { champNum = 120; }
            else if (r == "grag"
                || r == "fat"
                || r == "fatty"
                || r == "large"
                || r == "graga"
                || r == "gragas") { champNum = 44; }
            else if (r == "grav"
                || r == "grave"
                || r == "graves") { champNum = 85; }
            else if (r == "gw"
                || r == "gwe"
                || r == "gwen"
                || r == "scissor"
                || r == "scissors") { champNum = 155; }
            #endregion
            #region H
            else if (r == "hec"
                || r == "heca"
                || r == "horse guy"
                || r == "hecarim") { champNum = 96; }
            else if (r == "donger"
                || r == "dong"
                || r == "dinger"
                || r == "heim"
                || r == "heime"
                || r == "heimer"
                || r == "heimerdinger") { champNum = 39; }
            #endregion
            #region I
            else if (r == "il"
                || r == "ill"
                || r == "illa"
                || r == "illao"
                || r == "illaoi") { champNum = 128; }
            else if (r == "ir"
                || r == "ire"
                || r == "irel"
                || r == "irelia") { champNum = 64; }
            else if (r == "iv"
                || r == "ive"
                || r == "iver"
                || r == "green"
                || r == "tree dude"
                || r == "ivern") { champNum = 133; }
            #endregion
            #region J
            else if (r == "ja"
                || r == "jan"
                || r == "jana"
                || r == "janna") { champNum = 33; }
            else if (r == "j4"
                || r == "jar"
                || r == "jarv"
                || r == "jarvan"
                || r == "jarvan4"
                || r == "jarvaniv") { champNum = 71; }
            else if (r == "jax"
                || r == "realweapon"
                || r == "real weapon") { champNum = 5; }
            else if (r == "jay"
                || r == "jayc"
                || r == "jayce") { champNum = 100; }
            else if (r == "jh"
                || r == "jhi"
                || r == "four"
                || r == "jhin") { champNum = 129; }
            else if (r == "ji"
                || r == "jin"
                || r == "jinx") { champNum = 116; }
            #endregion
            #region K
            else if (r == "kai"
                || r == "kaisa"
                || r == "kai sa"
                || r == "kai'sa") { champNum = 140; }
            else if (r == "kal"
                || r == "kali"
                || r == "kalis"
                || r == "kalist"
                || r == "kalista") { champNum = 122; }
            else if (r == "karm"
                || r == "karma") { champNum = 69; }
            else if (r == "kart"
                || r == "karth"
                || r == "karthu"
                || r == "karthus") { champNum = 23; }
            else if (r == "kas"
                || r == "kass"
                || r == "16"
                || r == "sixteen"
                || r == "level16"
                || r == "level 16"
                || r == "kassadin") { champNum = 29; }
            else if (r == "kat"
                || r == "kata"
                || r == "reset"
                || r == "resets"
                || r == "katarina") { champNum = 36; }
            else if (r == "kayl"
                || r == "kayle") { champNum = 6; }
            else if (r == "kany"
                || r == "kane"
                || r == "rhaast"
                || r == "rhast"
                || r == "darkin"
                || r == "kayn") { champNum = 137; }
            else if (r == "ke"
                || r == "ken"
                || r == "kenn"
                || r == "kenne"
                || r == "kennen") { champNum = 49; }
            else if (r == "kha"
                || r == "khazix"
                || r == "k6"
                || r == "bug"
                || r == "kha zix"
                || r == "kha'zix") { champNum = 105; }
            else if (r == "ki"
                || r == "kin"
                || r == "kind"
                || r == "kindr"
                || r == "kindre"
                || r == "kindred") { champNum = 127; }
            else if (r == "kl"
                || r == "kle"
                || r == "kled") { champNum = 132; }
            else if (r == "kog"
                || r == "kogm"
                || r == "kogmaw"
                || r == "kog maw"
                || r == "kog'maw") { champNum = 54; }
            #endregion
            #region L
            else if (r == "lb"
                || r == "leb"
                || r == "lebl"
                || r == "leblanc") { champNum = 63; }
            else if (r == "lee"
                || r == "sin"
                || r == "leesin"
                || r == "lee sin") { champNum = 73; }
            else if (r == "leo"
                || r == "leon"
                || r == "leona") { champNum = 79; }
            else if (r == "lil"
                || r == "lili"
                || r == "deer"
                || r == "lilia"
                || r == "lillia") { champNum = 149; }
            else if (r == "lis"
                || r == "liss"
                || r == "ice queen"
                || r == "lissandra") { champNum = 113; }
            else if (r == "luc"
                || r == "luci"
                || r == "lucia"
                || r == "lucian") { champNum = 115; }
            else if (r == "lul"
                || r == "poly"
                || r == "squirrel"
                || r == "polymorph"
                || r == "lulu") { champNum = 95; }
            else if (r == "lux"
                || r == "light lady") { champNum = 62; }
            #endregion
            #region M
            else if (r == "yi"
                || r == "mas"
                || r == "master"
                || r == "masteryi"
                || r == "master yi") { champNum = 7; }
            else if (r == "malp"
                || r == "malph"
                || r == "rock"
                || r == "mountain"
                || r == "malphite") { champNum = 35; }
            else if (r == "malz"
                || r == "malza"
                || r == "press r"
                || r == "malzahar") { champNum = 52; }
            else if (r == "mao"
                || r == "maok"
                || r == "tree"
                || r == "maokai") { champNum = 70; }
            else if (r == "mf"
                || r == "mis"
                || r == "for"
                || r == "fort"
                || r == "fortune"
                || r == "miss"
                || r == "missfortune"
                || r == "miss fortune") { champNum = 59; }
            else if (r == "mord"
                || r == "morde"
                || r == "kaiser"
                || r == "death realm"
                || r == "mordekaiser") { champNum = 46; }
            else if (r == "morg"
                || r == "morga"
                || r == "morgan"
                || r == "black shield"
                || r == "morgana") { champNum = 8; }
            #endregion
            #region N
            else if (r == "nam"
                || r == "nami") { champNum = 108; }
            else if (r == "nas"
                || r == "nasu"
                || r == "susan"
                || r == "nasus") { champNum = 38; }
            else if (r == "naut"
                || r == "nauti"
                || r == "nautil"
                || r == "nautilu"
                || r == "anchor"
                || r == "nautilus") { champNum = 93; }
            else if (r == "ne"
                || r == "nee"
                || r == "neek"
                || r == "lesbian"
                || r == "neeko") { champNum = 142; }
            else if (r == "ni"
                || r == "nid"
                || r == "nida"
                || r == "cat lady"
                || r == "nidalee") { champNum = 42; }
            else if (r == "no"
                || r == "noc"
                || r == "noct"
                || r == "noctu"
                || r == "nightmare"
                || r == "nocturne") { champNum = 72; }
            else if (r == "nu"
                || r == "willump"
                || r == "nunuandwillump"
                || r == "nunu and willump"
                || r == "nunu&willump"
                || r == "nunu & willump"
                || r == "nunu") { champNum = 9; }
            #endregion
            #region O
            else if (r == "ol"
                || r == "ola"
                || r == "olaf") { champNum = 53; }
            else if (r == "ori"
                || r == "oria"
                || r == "orian"
                || r == "oriana"
                || r == "orianna") { champNum = 77; }
            else if (r == "orn"
                || r == "horn"
                || r == "ornn") { champNum = 138; }
            #endregion
            #region P
            else if (r == "pa"
                || r == "pan"
                || r == "pant"
                || r == "panth"
                || r == "pantheon") { champNum = 45; }
            else if (r == "pop"
                || r == "popp"
                || r == "popy"
                || r == "poppy") { champNum = 43; }
            else if (r == "py"
                || r == "pyk"
                || r == "pyke") { champNum = 141; }
            #endregion
            #region Q
            else if (r == "qi"
                || r == "qiy"
                || r == "qiya"
                || r == "qiyan"
                || r == "qiyana") { champNum = 145; }
            else if (r == "qu"
                || r == "qui"
                || r == "valor"
                || r == "bird lady"
                || r == "quin"
                || r == "quinn") { champNum = 111; }
            #endregion
            #region R
            else if (r == "rak"
                || r == "rakan") { champNum = 135; }
            else if (r == "ram"
                || r == "ramm"
                || r == "ok"
                || r == "rammus") { champNum = 29; }
            else if (r == "rek"
                || r == "reks"
                || r == "reksai"
                || r == "rek sai"
                || r == "rek'sai") { champNum = 123; }
            else if (r == "rel"
                || r == "rell") { champNum = 153; }
            else if (r == "rene"
                || r == "croc"
                || r == "crocodile"
                || r == "renek"
                || r == "renekton") { champNum = 68; }
            else if (r == "reng"
                || r == "renga"
                || r == "knelse ifecat"
                || r == "knelse ife cat"
                || r == "rengar") { champNum = 103; }
            else if (r == "riv"
                || r == "rive"
                || r == "riven") { champNum = 83; }
            else if (r == "rum"
                || r == "rumb"
                || r == "rumbl"
                || r == "rumble") { champNum = 75; }
            else if (r == "ry"
                || r == "ryz"
                || r == "rework"
                || r == "reworks"
                || r == "ryze") { champNum = 10; }
            #endregion
            #region S
            else if (r == "sa"
                || r == "sam"
                || r == "sami"
                || r == "samir"
                || r == "samira") { champNum = 151; }
            else if (r == "sej"
                || r == "seju"
                || r == "sejuani") { champNum = 91; }
            else if (r == "sen"
                || r == "senn"
                || r == "senna") { champNum = 146; }
            else if (r == "ser"
                || r == "sera"
                || r == "phine"
                || r == "sona2.0"
                || r == "sona 2"
                || r == "sona 2.0"
                || r == "serap"
                || r == "seraph"
                || r == "seraphi"
                || r == "seraphin"
                || r == "seraphine") { champNum = 152; }
            else if (r == "set"
                || r == "hot"
                || r == "sett") { champNum = 148; }
            else if (r == "sha"
                || r == "shac"
                || r == "shaco") { champNum = 40; }
            else if (r == "she"
                || r == "shen") { champNum = 48; }
            else if (r == "shy"
                || r == "shyv"
                || r == "dragon lady"
                || r == "shyvana") { champNum = 86; }
            else if (r == "sin"
                || r == "sing"
                || r == "singe"
                || r == "singed") { champNum = 18; }
            else if (r == "sio"
                || r == "sion") { champNum = 11; }
            else if (r == "siv"
                || r == "sivi"
                || r == "sivir") { champNum = 12; }
            else if (r == "sk"
                || r == "ska"
                || r == "skar"
                || r == "skarn"
                || r == "skarner") { champNum = 81; }
            else if (r == "son"
                || r == "sona") { champNum = 60; }
            else if (r == "sor"
                || r == "sora"
                || r == "raka"
                || r == "soraka") { champNum = 13; }
            else if (r == "sw"
                || r == "swa"
                || r == "swai"
                || r == "bird guy"
                || r == "swain") { champNum = 61; }
            else if (r == "syl"
                || r == "syla"
                || r == "chains"
                || r == "sylas") { champNum = 143; }
            else if (r == "syn"
                || r == "synd"
                || r == "syndr"
                || r == "syndra") { champNum = 104; }
            #endregion
            #region T
            else if (r == "tah"
                || r == "tahm"
                || r == "ken"
                || r == "kenc"
                || r == "kench"
                || r == "frog"
                || r == "tahmkench"
                || r == "tahm kench") { champNum = 126; }
            else if (r == "tali"
                || r == "taliy"
                || r == "taliya"
                || r == "rock lady"
                || r == "taliyah") { champNum = 131; }
            else if (r == "talo"
                || r == "firstblood"
                || r == "first blood"
                || r == "25%"
                || r == "talon") { champNum = 82; }
            else if (r == "tar"
                || r == "tari"
                || r == "taric") { champNum = 31; }
            else if (r == "tee"
                || r == "teem"
                || r == "aids"
                || r == "teemo") { champNum = 14; }
            else if (r == "th"
                || r == "thr"
                || r == "thre"
                || r == "thres"
                || r == "thresh") { champNum = 110; }
            else if (r == "tri"
                || r == "tris"
                || r == "trist"
                || r == "trista"
                || r == "tristan"
                || r == "tristana") { champNum = 15; }
            else if (r == "tru"
                || r == "trun"
                || r == "trund"
                || r == "trundl"
                || r == "trundle") { champNum = 65; }
            else if (r == "try"
                || r == "tryn"
                || r == "trynd"
                || r == "trynda"
                || r == "tryndamere") { champNum = 22; }
            else if (r == "tf"
                || r == "fate"
                || r == "twistedfate"
                || r == "twisted fate") { champNum = 16; }
            else if (r == "tw"
                || r == "rat"
                || r == "sewer rat"
                || r == "twi"
                || r == "twit"
                || r == "twitc"
                || r == "twitch") { champNum = 21; }
            #endregion
            #region U
            else if (r == "ud"
                || r == "udy"
                || r == "udyr") { champNum = 41; }
            else if (r == "ur"
                || r == "urg"
                || r == "urgo"
                || r == "urgot") { champNum = 58; }
            #endregion
            #region V
            else if (r == "var"
                || r == "varu"
                || r == "varus") { champNum = 97; }
            else if (r == "vn"
                || r == "vay"
                || r == "vayn"
                || r == "vayne") { champNum = 76; }
            else if (r == "vei"
                || r == "veig"
                || r == "veiga"
                || r == "veigar") { champNum = 28; }
            else if (r == "vel"
                || r == "eye"
                || r == "eye of sauron"
                || r == "velk"
                || r == "velko"
                || r == "velkoz"
                || r == "vel koz"
                || r == "vel'koz") { champNum = 118; }
            else if (r == "6"
                || r == "six"
                || r == "vi") { champNum = 109; }
            else if (r == "vie"
                || r == "vieg"
                || r == "viego"
                || r == "bork") { champNum = 154; }
            else if (r == "vik"
                || r == "vikt"
                || r == "vikto"
                || r == "viktor") { champNum = 90; }
            else if (r == "blood"
                || r == "vla"
                || r == "vlad"
                || r == "vladi"
                || r == "vladim"
                || r == "vladimi"
                || r == "vladimir") { champNum = 56; }
            else if (r == "vo"
                || r == "vb"
                || r == "vol"
                || r == "voli"
                || r == "volib"
                || r == "bear"
                || r == "volibear") { champNum = 88; }
            #endregion
            #region W
            else if (r == "ww"
                || r == "war"
                || r == "wick"
                || r == "warwick") { champNum = 17; }
            else if (r == "wu"
                || r == "wuk"
                || r == "wuko"
                || r == "wukon"
                || r == "wukong") { champNum = 80; }
            #endregion
            #region X
            else if (r == "xa"
                || r == "xay"
                || r == "xaya"
                || r == "xayah") { champNum = 136; }
            else if (r == "xe"
                || r == "xer"
                || r == "xerath"
                || r == "xerat") { champNum = 84; }
            else if (r == "xi"
                || r == "xin"
                || r == "xinz"
                || r == "zhao"
                || r == "xinzhao"
                || r == "xin zhao") { champNum = 55; }
            #endregion
            #region Y
            else if (r == "ya"
                || r == "yas"
                || r == "yasu"
                || r == "yasuo") { champNum = 117; }
            else if (r == "yon"
                || r == "better yasuo"
                || r == "yone") { champNum = 150; }
            else if (r == "yor"
                || r == "yori"
                || r == "yoric"
                || r == "yorick") { champNum = 78; }
            else if (r == "yu"
                || r == "damn cat"
                || r == "yum"
                || r == "yumi"
                || r == "yummi"
                || r == "yuumi") { champNum = 144; }
            #endregion
            #region Z
            else if (r == "za"
                || r == "zac") { champNum = 112; }
            else if (r == "ze"
                || r == "zed") { champNum = 107; }
            else if (r == "zig"
                || r == "bomb"
                || r == "bombs"
                || r == "zigs"
                || r == "zigg"
                || r == "ziggs") { champNum = 92; }
            else if (r == "zil"
                || r == "zile"
                || r == "zilea"
                || r == "groovy"
                || r == "zilean") { champNum = 19; }
            else if (r == "zo"
                || r == "cancer"
                || r == "aids"
                || r == "zoe") { champNum = 139; }
            else if (r == "zy"
                || r == "zyr"
                || r == "plant"
                || r == "plants"
                || r == "plant lady"
                || r == "zyra") { champNum = 101; }
            #endregion
            #endregion
            #region ds/sb
            else if ((r == "ds"
                || r == "dra"
                || r == "drag"
                || r == "dragon"
                || r == "slayer"
                || r == "dragonslayer") && champNum == 76) { dragonslayer = true; }
            else if ((r == "sb"
                || r == "sp"
                || r == "spi"
                || r == "spir"
                || r == "spiri"
                || r == "flor"
                || r == "spirit"
                || r == "blossom"
                || r == "spirit blossom") && champNum == 76) { spiritBlossom = true; }
            #endregion
            #region invalid
            else if (r.Length > 0)
            {
                invalid = true;
                Colors("Red");
                Console.WriteLine("Invalid input. Please try again.\n");
                Console.ResetColor();
            }
            #endregion
            if (champNum != oldChampNum) { Main(); }
        }
        static void Skin()
        {
            /// Defines 'length' by getting the Length of the array in champions (jagged array), indexed by champNum;
            /// ex. champNum=2 -> 3 objects in Annie (includes first object which is the champion name).
            ///
            /// Rolls skinNum starting at index 1 (0 is the champion's name) and up to length, since that's how many
            /// objects exist inside the array.
            ///
            /// While loop checks if the newly rolled skin is equal to the previous skin (which is not defined the
            /// first time) AND the amount of available skins in the array is more than 2 (if it is two, the only
            /// skin is "Default" and it is only rolling that skin; while loop will run infinitely if it does not
            /// check for length because skinNum will always equal oldSkin). Inside of while loop rolls again until
            /// the new skin is not the same as the previous one.
            ///
            /// If dragonslayer or spiritBlossom are true (from Reply), changes them back to false and assigns 3 or 9
            /// (respectively) to skinNum so it displays the corresponding skin. (More on this below)
            ///
            /// Defines 'name' by locating the string inside of champions (jagged array), using champNum to index
            /// which array (champion) to select then skinNum to index which string (skin) to select.
            /// ex. champNum=76 & skinNum=2 -> Aristocrat
            Colors("LightGreen");   //change color to green right away
            int length = champions[champNum].Length;
            skinNum = roll.Next(1, length);
            while ((skinNum == oldSkin) && (length > 2))
            {
                skinNum = roll.Next(1, length);
            }
            if (dragonslayer) { dragonslayer = false; skinNum = 3; }
            if (spiritBlossom) { spiritBlossom = false; skinNum = 9; }
            string name = champions[champNum][skinNum];
            Console.Write(name);

            /// Checks if the last character in name is a space using EndsWith method. If it is, the program knows
            /// that the selected skin has chromas and the colors need to be rolled individually. (See next)
            ///
            /// Checks which skin with a chroma was rolled and assigns an array with the chroma colors depending on
            /// which skin it is.
            /// ex. Dragonslayer has Green, Red, and Silver (and a blank option so it is able to display the skin
            /// without a chroma). It then rolls a number and eliminates repeats like above (does not need to check
            /// for length, though) and displays the chroma color from the array by indexing it according to the
            /// value of chromaNum.
            ///
            /// Assigns the chromaNum value to oldChroma so if the same skin is rolled again (which will only happen
            /// if the user activates dragonslayer or spiritBlossom), the anti-repeat while loop will run.
            ///
            /// FINALLY, assigns the skinNum value to oldSkin to prevent repeats, similar to above with chromas.
            /// Console.WriteLine ends the line and jumps another line using \n.
            if (name.EndsWith(' '))                         //skins with chromas always end with a space
            {
                if (name == "Dragonslayer ")
                {
                    string[] chroma = { "", "Green", "Red", "Silver" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);    //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]); //prints chroma color after name of skin
                }   //Dragonslayer Vayne
                else if (name == "Spirit Blossom ")
                {
                    string[] chroma = { "", "Red", "Yellow", "Green", "Purple", "Pink", "Black", "White" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);       //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);    //prints chroma color after name of skin
                }   //Spirit Blossom Vayne
                else if (name == "Firecracker ")
                {
                    string[] chroma = { "", "White", "Black", "Light Blue", "Pink", "Orange", "Purple", "Green" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);         //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);  //prints chroma color after name of skin

                }   //Firecracker Jinx
                else if (name == "Mecha ")
                {
                    string[] chroma = { "", "Yellow", "Green", "Black", "Tan", "White", "Blue", "Gray", "Orange" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);               //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);        //prints chroma color after name of skin

                }   //Mecha Malphite
                else if (name == "Papercraft ")
                {
                    string[] chroma = { "", "White", "Light Blue", "Black", "Purple", "Pink", "Yellow" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);         //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);  //prints chroma color after name of skin

                }   //Papercraft Nunu & Willump
                else if (name == "Rocket Girl ")
                {
                    string[] chroma = { "", "Blue", "Purple", "Red" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);         //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);  //prints chroma color after name of skin

                }   //Rocket Girl Tristana
                else if (name == "Medieval ")
                {
                    string[] chroma = { "", "Purple" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);         //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);  //prints chroma color after name of skin
                }   //Medieval Twitch
                else if (name == "Grey ")
                {
                    string[] chroma = { "", "Blue" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);         //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);  //prints chroma color after name of skin

                }   //Grey Warwick
                else if (name == "Victorious " && champNum == 115)  //Lucian
                {
                    string[] chroma = { "", "Green", "Light Blue" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);         //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);  //prints chroma color after name of skin
                }   //Victorious Lucian
                else if (name == "Victorious " && champNum == 77)   //Orianna
                {
                    string[] chroma = { "", "Green" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);         //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);  //prints chroma color after name of skin
                }   //Victorious Orianna
                else if (name == "Victorious " && champNum == 85)    //Graves
                {
                    string[] chroma = { "", "White" };
                    chromaNum = roll.Next(chroma.Length);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(chroma.Length); }
                    Colors(chroma[chromaNum]);         //calls Colors() with the string from the array
                    Console.Write(chroma[chromaNum]);  //prints chroma color after name of skin

                }   //Victorious Graves
                oldChroma = chromaNum;
            }   //CHROMAS
            oldSkin = skinNum;
            Console.ResetColor();
            Console.WriteLine("\n");
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
            Colors("Cyan");
            Console.Write(new string(' ', (Console.WindowWidth - intro.Length) / 2));
            Console.Write(intro);
            Colors("Magenta");
            Console.WriteLine("{0}\n", appVer);
            Console.ResetColor();
            Console.Write(new string(' ', (Console.WindowWidth - begin.Length) / 2));
            Console.Write(begin);
            Console.ReadKey();
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
            int ver = 0;
            string[] versions =
            {
                "(09/16/2021) alpha1: ",
                "(09/16/2021) alpha2: ",
                "(09/17/2021) alpha3: ",
                "(09/18/2021) alpha4: ",
                "(09/18/2021) v0.1.0: ",
                "(10/21/2021) v0.1.1: ",
                "(10/24/2021) v0.2.0: ",
                "(10/24/2021) v0.2.1: ",
            };
            string[] text =
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
                    "\nparameter then selects a color based on that.",
                "Added filler arrays for every champion, now using correct champion numbers. Colors now " +
                    "change\nwhen they should (including skins and chromas).",
                "Added all chromas and begun adding all skins. Change chroma roll to length of array, like " +
                "it\nalready should have been."
            };

            Console.Clear();
            Colors("Yellow");
            Console.WriteLine("\tCHANGELOG:");
            while (ver < versions.Length)
            {
                Colors("Cyan");
                Console.Write(string.Format("\n {0, -21}", versions[ver]));
                Colors("White");
                Console.WriteLine(text[ver]);
                ver++;
            }
            Console.ResetColor();
            Console.Write("\nPress any key to return...");
            Console.ReadKey();
            Main();
        }
        static void Help()
        /// Works very similar to Changelog; simple help screen showing commands.
        /// (See Changelog summary)
        ///
        {
            int num = 0;
            string[] commands =
            {
                "Change mode: ",
                "Random champion: ",
                "View changelog: ",
                "This screen: ",
                "Clear screen: ",
                "Reset program: "
            };
            string[] text =
            {
                "input Champion name or number (champion numbers are chronological)\n",
                "\"r\" or \"rand\" or \"random\"",
                "\"c\" or \"cl\" or \"changelog\"",
                "\"h\" or \"help\"",
                "\"clear\"",
                "\"reset\" or \"intro\""
            };

            Console.Clear();
            Colors("Yellow");
            Console.WriteLine("\tCOMMAND LIST:\n");
            while (num < commands.Length)
            {
                Colors("Cyan");
                Console.Write(string.Format(" {0, -18}", commands[num]));
                Colors("White");
                Console.WriteLine(text[num]);
                num++;
            }
            Console.ResetColor();
            Console.Write("\nPress any key to return...");
            Console.ReadKey();
            Main();
        }


        static void Colors(string color)
        {
            if (color == "Green") { Console.ForegroundColor = ConsoleColor.DarkGreen; }
            else if (color == "LightGreen") { Console.ForegroundColor = ConsoleColor.Green; }
            else if (color == "Red" || color == "Orange") { Console.ForegroundColor = ConsoleColor.Red; }
            else if (color == "Blue") { Console.ForegroundColor = ConsoleColor.Blue; }
            else if (color == "Cyan" || color == "Silver" || color == "Light Blue" || color == "Tan")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (color == "Yellow") { Console.ForegroundColor = ConsoleColor.Yellow; }
            else if (color == "Magenta" || color == "Pink") { Console.ForegroundColor = ConsoleColor.Magenta; }
            else if (color == "Purple") { Console.ForegroundColor = ConsoleColor.DarkMagenta; }
            else if (color == "Gray") { Console.ForegroundColor = ConsoleColor.Gray; }
            else if (color == "White") { Console.ForegroundColor = ConsoleColor.White; }
            else if (color == "Black") { Console.ForegroundColor = ConsoleColor.DarkGray; }
        }
    }
}
