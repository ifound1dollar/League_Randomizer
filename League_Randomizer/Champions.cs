using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Randomizer
{
    class Champions
    {
        static readonly string[][] champions =
{
            new string[] { "null" },    //0
            new string[] { "ALISTAR", "Default" },                              ///
            new string[] { "ANNIE", "Annie-versary", "Prom Queen", "Default" }, ///
            new string[] { "ASHE", "Sherwood Forest", "Default" },              ///
            new string[] { "FIDDLESTICKS", "Fiddle Me Timbers", "Default" },    ///
            new string[] { "JAX", "Warden", "Default" },                //5     ///
            new string[] { "KAYLE", "Riot", "Transcendent (Unmasked)",
                "Default" },                                                    ///
            new string[] { "MASTER YI", "Default" },                            ///
            new string[] { "MORGANA", "Ghost Bride", "Blade Mistress",
                "Default"},                                                     ///
            new string[] { "NUNU & WILLUMP", "Workshop", "Papercraft ",
                "Default" },                                                    ///
            new string[] { "RYZE", "Dark Crystal", "Default" },         //10    ///
            new string[] { "SION", "Default" },                                 ///
            new string[] { "SIVIR", "Snowstorm", "Default" },                   ///
            new string[] { "SORAKA", "Default" },                               ///
            new string[] { "TEEMO", "Panda", "Default" },                       ///
            new string[] { "TRISTANA", "Bewitching", "Riot Girl",
                "Rocket Girl ", "Default" },                            //15    ///
            new string[] { "TWISTED FATE", "Default" },                         ///
            new string[] { "WARWICK", "Grey ", "Default" },                     ///
            new string[] { "SINGED", "Hextech", "Mad Scientist", "Default" },   ///
            new string[] { "ZILEAN", "Default" },                               ///
            new string[] { "EVELYNN", "Safecracker", "Default" },       //20    ///
            new string[] { "TWITCH", "Medieval ", "Whistler Village",
                "Default" },                                                    ///
            new string[] { "TRYNDAMERE", "Default" },                           ///
            new string[] { "KARTHUS", "Fnatic", "Default" },                    ///
            new string[] { "AMUMU", "Emumu", "Default" },                       ///
            new string[] { "CHO'GATH", "Jurassic", "Loch Ness",
                "Default" },                                            //25    ///
            new string[] { "ANIVIA", "Blackfrost", "Default" },                 ///
            new string[] { "RAMMUS", "Default" },                               ///
            new string[] { "VEIGAR", "White Mage", "Curling",
                "Superb Villain", "Final Boss", "Default" },                    ///
            new string[] { "KASSADIN", "Default" },                             ///
            new string[] { "GANGPLANK", "Default" },                    //30    ///
            new string[] { "TARIC", "Default" },                                ///
            new string[] { "BLITZCRANK", "Boom Boom", "Default" },              ///
            new string[] { "DR. MUNDO", "Default" },                            ///
            new string[] { "JANNA", "Default" },                                ///
            new string[] { "MALPHITE", "Mecha ", "Default" },           //35    ///
            new string[] { "CORKI", "Urfrider", "Default" },                    ///
            new string[] { "KATARINA", "Kitty Kat", "Default" },                ///
            new string[] { "NASUS", "Archduke", "Default" },                    ///
            new string[] { "HEIMERDINGER", "Default" },                         ///
            new string[] { "SHACO", "Default" },                        //40    ///
            new string[] { "UDYR", "Default" },                                 ///
            new string[] { "NIDALEE", "Default" },                              ///
            new string[] { "POPPY", "Default" },                                ///
            new string[] { "GRAGAS", "Default" },                               ///
            new string[] { "PANTHEON", "Default" },                     //45    ///
            new string[] { "MORDEKAISER", "Default" },                          ///
            new string[] { "EZREAL", "Pulsefire", "Default" },                  ///
            new string[] { "SHEN", "Default" },                                 ///
            new string[] { "KENNEN", "Default" },                               ///
            new string[] { "GAREN", "Default" },                        //50    ///
            new string[] { "AKALI", "Blood Moon", "Default" },                  ///
            new string[] { "MALZAHAR", "Overlord", "Default" },                 ///
            new string[] { "OLAF", "Default" },                                 ///
            new string[] { "KOG'MAW", "Reindeer", "Default" },                  ///
            new string[] { "XIN ZHAO", "Default" },                     //55    ///
            new string[] { "VLADIMIR", "Blood Lord", "Default" },               ///
            new string[] { "GALIO", "Default" },                                ///
            new string[] { "URGOT", "Default" },                                ///
            new string[] { "MISS FORTUNE", "Pool Party", "Default" },           ///
            new string[] { "SONA", "Sweetheart", "Default" },           //60    ///
            new string[] { "SWAIN", "Default" },                                ///
            new string[] { "LUX", "Lunar Empress", "Steel Legion", "Default" }, ///
            new string[] { "LEBLANC", "Default" },                              ///
            new string[] { "IRELIA", "Default" },                               ///
            new string[] { "TRUNDLE", "Default" },                      //65    ///
            new string[] { "CASSIOPEIA", "Default" },                           ///
            new string[] { "CAITLYN", "Safari", "Default" },                    ///
            new string[] { "RENEKTON", "SKT T1", "Default" },                   ///
            new string[] { "KARMA", "Default" },                                ///
            new string[] { "MAOKAI", "Victorious", "Default" },         //70    ///
            new string[] { "JARVAN IV", "Default" },                            ///
            new string[] { "NOCTURNE", "Void", "Default" },                     ///
            new string[] { "LEE SIN", "Default" },                              ///
            new string[] { "BRAND", "Vandal", "Default" },                      ///
            new string[] { "RUMBLE", "Default" },                       //75    ///
            new string[] { "VAYNE", "Vindicator", "Aristocrat",
                "Dragonslayer ", "Heartseeker", "SKT T1", "Arclight",
                "Soulstealer", "Firecracker Prestige Edition",
                "Spirit Blossom ", "Default" },                     //76        ///
            new string[] { "ORIANNA", "TPA", "Winter Wonder", "Victorious",
                "Default" },                                                    ///
            new string[] { "YORICK", "Default" },                               ///
            new string[] { "LEONA", "Default" },                                ///
            new string[] { "WUKONG", "Lancer Stratus", "Underworld",
                "Default" },                                            //80    ///
            new string[] { "SKARNER", "Default" },                              ///
            new string[] { "TALON", "Default" },                                ///
            new string[] { "RIVEN", "Arcade", "Default" },                      ///
            new string[] { "XERATH", "Default" },                               ///
            new string[] { "GRAVES", "Victorious ", "Default" },        //85    ///
            new string[] { "SHYVANA", "Default" },                              ///
            new string[] { "FIZZ", "Atlantean", "Default" },                    ///
            new string[] { "VOLIBEAR", "Thousand-Pierced Bear", "Default" },    ///
            new string[] { "AHRI", "Default" },                                 ///
            new string[] { "VIKTOR", "Default" },                       //90    ///
            new string[] { "SEJUANI", "Firecracker", "Default" },               ///
            new string[] { "ZIGGS", "Master Arcanist", "Default" },             ///
            new string[] { "NAUTILUS", "Default" },                             ///
            new string[] { "FIORA", "Default" },                                ///
            new string[] { "LULU", "Pool Party", "Default" },           //95    ///
            new string[] { "HECARIM", "Default" },                              ///
            new string[] { "VARUS", "Arclight", "Default" },                    ///
            new string[] { "DARIUS", "Default" },                               ///
            new string[] { "DRAVEN", "Default" },                               ///
            new string[] { "JAYCE", "Forsaken", "Default" },            //100   ///
            new string[] { "ZYRA", "Default" },                                 ///
            new string[] { "DIANA", "Default" },                                ///
            new string[] { "RENGAR", "Default" },                               ///
            new string[] { "SYNDRA", "Snow Day", "Default" },                   ///
            new string[] { "KHA'ZIX", "Default" },                      //105   ///
            new string[] { "ELISE", "Default" },                                ///
            new string[] { "ZED", "Default" },                                  ///
            new string[] { "NAMI", "Default" },                                 ///
            new string[] { "VI", "Default" },                                   ///
            new string[] { "THRESH", "SSW", "Default" },                //110   ///
            new string[] { "QUINN", "Phoenix", "Default" },                     ///
            new string[] { "ZAC", "Default" },                                  ///
            new string[] { "LISSANDRA", "Default" },                            ///
            new string[] { "AATROX", "Victorious", "Default" },                 ///
            new string[] { "LUCIAN", "Hired Gun", "Victorious ",
                "Default" },                                            //115   ///
            new string[] { "JINX", "Firecracker ", "Default" },                 ///
            new string[] { "YASUO", "Battle Boss", "Default" },                 ///
            new string[] { "VEL'KOZ", "Default" },                              ///
            new string[] { "BRAUM", "Santa", "Default" },                       ///
            new string[] { "GNAR", "Default" },                         //120   ///
            new string[] { "AZIR", "Default" },                                 ///
            new string[] { "KALISTA", "Blood Moon", "Default" },                ///
            new string[] { "REK'SAI", "Default" },                              ///
            new string[] { "BARD", "Default" },                                 ///
            new string[] { "EKKO", "Default" },                         //125   ///
            new string[] { "TAHM KENCH", "Default" },                           ///
            new string[] { "KINDRED", "Default" },                              ///
            new string[] { "ILLAOI", "Default" },                               ///
            new string[] { "JHIN", "Default" },                                 ///
            new string[] { "AURELION SOL", "Mecha", "Default" },        //130   ///
            new string[] { "TALIYAH", "Default" },                              ///
            new string[] { "KLED", "Default" },                                 ///
            new string[] { "IVERN", "Default" },                                ///
            new string[] { "CAMILLE", "Default" },                              ///
            new string[] { "RAKAN", "Default" },                        //135   ///
            new string[] { "XAYAH", "Star Guardian", "Default" },               ///
            new string[] { "KAYN", "Default" },                                 ///
            new string[] { "ORNN", "Default" },                                 ///
            new string[] { "ZOE", "Default" },                                  ///
            new string[] { "KAI'SA", "K/DA", "Default" },               //140   ///
            new string[] { "PYKE", "Default" },                                 ///
            new string[] { "NEEKO", "Default" },                                ///
            new string[] { "SYLAS", "Lunar Wraith", "Default" },                ///
            new string[] { "YUUMI", "Default" },                                ///
            new string[] { "QIYANA", "Default" },                       //145   ///
            new string[] { "SENNA", "Default" },                                ///
            new string[] { "APHELIOS", "Default" },                             ///
            new string[] { "SETT", "Default" },                                 ///
            new string[] { "LILLIA", "Default" },                               ///
            new string[] { "YONE", "Default" },                         //150   ///
            new string[] { "SAMIRA", "Default" },                               ///
            new string[] { "SERAPHINE", "Default" },                            ///
            new string[] { "RELL", "Default" },                                 ///
            new string[] { "VIEGO", "Default" },                                ///
            new string[] { "GWEN", "Default" },                         //155   ///
            new string[] { "AKSHAN", "Default" },                               ///
            new string[] { "VEX", "Default" },                                  ///
        };
        static void Main()
        {

        }
    }
}
