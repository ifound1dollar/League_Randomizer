using System;

namespace League_Randomizer
{
    class Program
    {
        #region public vars
        public static Random roll = new();

        public static bool first;
        public static bool invalid = false;
        public static bool dragonslayer = false;
        public static bool spiritBlossom = false;

        public static string appVer = "alpha1";

        public static int skinNum;
        public static int oldSkin;
        public static int chromaNum;
        public static int oldChroma;
        public static int champNum = 0;
        #endregion
        #region arrays
        public static string[] vayne = { "Vindicator", "Aristocrat", "Dragonslayer ", "Arclight" };
        public static string[] annie = { "Annie-versary", "Prom Queen" };
        public static string[] tristana = { "tristana1", "tristana2", "tristana3" };
        public static string[] twitch = { "twitch1", "twitch2", "twitch3", "twitch4" };
        public static string[][] champions = 
        {
            vayne, annie, tristana, twitch
        };
        #endregion

        static void Main()
        {
            Console.Clear();
            if (first)
            {
                first = false;
                Intro();
            }
            oldSkin = oldChroma = 20;                       //both ints back to 20 after clear/reset/champ change
            Console.WriteLine("League Randomizer " + appVer);
            Console.WriteLine(champions[champNum]);
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
            Console.Write("Press enter to run...");
            string r = Console.ReadLine().ToLower();
            #region misc.
            if (r == "clear"
                || r == "cle"
                || r == "clea") { Main(); }
            /*else if (r == "c"
                || r == "cl"
                || r == "changelog") { Changelog(); }
            else if (r == "h"
                || r == "hel"
                || r == "help") { Help(); }*/
            else if (r == "intro" || r == "reset")
            {
                first = true;
                Main();
            }
            /*else if (r == "r" || r == "rand" || r == "random")
            {
                champNum = randomObject.Next(1, total + 1); //max always has to be 1 more
                while (champNum == oldchampNum) { champNum = randomObject.Next(1, total + 1); }
                ChampName(champNum);
            }*/
            #endregion
            #region champions
            #region A
            else if (r == "aa"
                || r == "aat"
                || r == "aatr"
                || r == "aatro"
                || r == "aatrox") { champNum = 114; }
            else if (r == "ah"
                || r == "ahr"
                || r == "ninetails"
                || r == "rule34"
                || r == "ahri") { champNum = 89; }
            else if (r == "ak"
                || r == "aka"
                || r == "akal"
                || r == "akali") { champNum = 51; }
            else if (r == "al"
                || r == "ali"
                || r == "cow"
                || r == "alis"
                || r == "alist"
                || r == "alistar") { champNum = 1; }
            else if (r == "am"
                || r == "mu"
                || r == "amu"
                || r == "amum"
                || r == "mummy"
                || r == "mum"
                || r == "mumy"
                || r == "amumu") { champNum = 24; }
            else if (r == "ani"
                || r == "egg"
                || r == "anivegg"
                || r == "eggnivia"
                || r == "aniv"
                || r == "anivia") { champNum = 26; }
            else if (r == "ann"
                || r == "anni"
                || r == "teddy"
                || r == "teddybear"
                || r == "teddy bear"
                || r == "tibbers"
                || r == "annie") { champNum = 2; }
            else if (r == "aph"
                || r == "aphe"
                || r == "aphel"
                || r == "aphelios") { champNum = 147; }
            else if (r == "as"
                || r == "ash"
                || r == "slow"
                || r == "ashe") { champNum = 3; }
            else if (r == "au"
                || r == "aur"
                || r == "sol"
                || r == "asol"
                || r == "star"
                || r == "star guy"
                || r == "aurelion sol"
                || r == "aurelionsol") { champNum = 130; }
            else if (r == "az"
                || r == "azi"
                || r == "soldier"
                || r == "soldiers"
                || r == "azir") { champNum = 121; }
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
                || r == "4"
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
                || r == "malphite") { champNum = 20; }
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
            else if (r == "sb"
                || r == "sp"
                || r == "spi"
                || r == "spir"
                || r == "spiri"
                || r == "flor"
                || r == "spirit"
                || r == "blossom"
                || r == "spirit blossom") { champNum = 1076; }
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
            else if (r == "ds"
                || r == "dra"
                || r == "drag"
                || r == "dragon"
                || r == "slayer"
                || r == "dragonslayer") { dragonslayer = true; }
            else if (r == "sb"
                || r == "sp"
                || r == "spi"
                || r == "spir"
                || r == "spiri"
                || r == "flor"
                || r == "spirit"
                || r == "blossom"
                || r == "spirit blossom") { spiritBlossom = true; }
            #endregion
            else if (r.Length > 0)
            {
                invalid = true;
                Red();
                Console.WriteLine("Invalid input. Please try again.\n");
            }
        }
        static void Skin()
        {
            int length = champions[champNum].Length;        //gets NUMBER OF OBJECTS in specific array, indexed
                                                            //by champNum; ex. champNum=1 -> 2 in Annie
            skinNum = roll.Next(length);                    //rolls amount equal to amount of skins in the array
            while (skinNum == oldSkin) { skinNum = roll.Next(length); } //eliminates repeats
            if (dragonslayer) { dragonslayer = false; skinNum = 2; }    //assigns 2 to skin so it always rolls Dragonslayer
            if (spiritBlossom) { spiritBlossom = false; skinNum = 8; }  //same as above but 8 so it rolls Spirit Blossom
            string name = champions[champNum][skinNum];     //gets STRING in specific array in specific place, 
                                                            //indexed by champNum then skinNum
                                                            //ex. champNum=75 & skinNum=1 -> Aristocrat
            Console.Write(name);
            int lastIndex = name.Length - 1;                //assigns value of last index of name
            if (name[lastIndex] == ' ')                     //checks for a space at the end of name because skins
            {                                                   //with chromas have a space at the end of the index
                if (name == "Dragonslayer ")
                {
                    chromaNum = roll.Next(4);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(4); }
                    switch (chromaNum)
                    {
                        case 0: { Console.Write(""); break; }
                        case 1: { Console.Write("Green"); break; }
                        case 2: { Console.Write("Red"); break; }
                        case 3: { Console.Write("Silver"); break; }

                    }
                }
                if (name == "Spirit Blossom ")
                {
                    chromaNum = roll.Next(8);
                    while (chromaNum == oldChroma) { chromaNum = roll.Next(8); }
                    switch (chromaNum)
                    {
                        case 0: { Console.Write(""); break; }
                        case 1: { Console.Write("Red"); break; }
                        case 2: { Console.Write("Yellow"); break; }
                        case 3: { Console.Write("Green"); break; }
                        case 4: { Console.Write("Purple"); break; }
                        case 5: { Console.Write("Pink"); break; }
                        case 6: { Console.Write("Black"); break; }
                        case 7: { Console.Write("White"); break; }
                    }
                }
                oldChroma = chromaNum;                      //prevents repeats when chroma skin is called again
            }
            oldSkin = skinNum;
            Console.WriteLine("\n");
        }



        static void Intro()
        {
            Console.WriteLine("EVEN BETTER RANDOMIZER?");
        }
        static void Red() { Console.ForegroundColor = ConsoleColor.Red; }
    }
}
