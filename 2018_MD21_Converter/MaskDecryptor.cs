using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Roccus_MultiTool
{
    class MaskDecryptor
    {
        static List<double> value = new List<double>();
        static List<string> results = new List<string>();
        static double mask = 0;

        public static void Work()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            for (double i = 0; i < 36; i++)
            {
                value.Add(Math.Pow(2, i));
            }

            Console.WriteLine("Write the Mask to Decrypt : ");
            string written = Console.ReadLine();
            Console.WriteLine();
            mask = double.Parse(Regex.Match(written.Trim(), "^[0-9]+$").Success ? written.Trim() : "-1");

            if (mask == -1)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a right Mask, Press Enter to quit");
            }
            else
            {
                DecryptMask(mask);
                GetResultValues();
                results.Clear();
                value.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press Enter to go back to the Main Menu");
            }

            Console.ReadLine();
            Console.Clear();
            Start.Main(new string[0]);
        }

        public static void GetResultValues()
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var s in results)
            {
                switch (double.Parse(s))
                {
                    case 1:
                        Console.WriteLine("1 Human or War");

                        break;
                    case 2:
                        Console.WriteLine("2 Orc or Pala");

                        break;
                    case 4:
                        Console.WriteLine("4 Dwarf or Hunt");

                        break;
                    case 8:
                        Console.WriteLine("8 NightElf or Rogue");

                        break;
                    case 16:
                        Console.WriteLine("16 Scourge or Priest");

                        break;
                    case 32:
                        Console.WriteLine("32 Tauren or DK");

                        break;
                    case 64:
                        Console.WriteLine("64 Gnome or Shaman");

                        break;
                    case 128:
                        Console.WriteLine("128 Troll or Mage");

                        break;
                    case 256:
                        Console.WriteLine("256 Goblin or Warlock");

                        break;
                    case 512:
                        Console.WriteLine("512 BloodElf or Monk");

                        break;
                    case 1024:
                        Console.WriteLine("1024 Draenei or Druid");

                        break;
                    case 2048:
                        Console.WriteLine("2048 FelOrc or DH");

                        break;
                    case 4096:
                        Console.WriteLine("4096 Naga");

                        break;
                    case 8192:
                        Console.WriteLine("8192 Broken");

                        break;
                    case 16384:
                        Console.WriteLine("16384 Skeleton");

                        break;
                    case 32768:
                        Console.WriteLine("32768 Vrykul");

                        break;
                    case 65536:
                        Console.WriteLine("65536 Tuskarr");

                        break;
                    case 131072:
                        Console.WriteLine("131072 ForestTroll");

                        break;
                    case 262144:
                        Console.WriteLine("262144 Taunka");

                        break;
                    case 524288:
                        Console.WriteLine("524288 NorthSkeleton");

                        break;
                    case 1048576:
                        Console.WriteLine("1048576 IceTroll");

                        break;
                    case 2097152:
                        Console.WriteLine("2097152 Worgen");

                        break;
                    case 4194304:
                        Console.WriteLine("4194304 HumanWorgen");

                        break;
                    case 8388608:
                        Console.WriteLine("8388608 PandarenNeutral");

                        break;
                    case 16777216:
                        Console.WriteLine("16777216 PandarenAlliance");

                        break;
                    case 33554432:
                        Console.WriteLine("33554432 PandarenHorde");

                        break;
                    case 67108864:
                        Console.WriteLine("67108864 Nightborne");

                        break;
                    case 134217728:
                        Console.WriteLine("134217728 HM Tauren");

                        break;
                    case 268435456:
                        Console.WriteLine("268435456 VoidElf");

                        break;
                    case 536870912:
                        Console.WriteLine("536870912 LF Draenei");

                        break;
                    case 1073741824:
                        Console.WriteLine("1073741824 Zandalari");

                        break;
                    case 2147483648:
                        Console.WriteLine("2147483648 Kultiran");

                        break;
                    case 4294967296:
                        Console.WriteLine("4294967296 Thin Human");

                        break;
                    case 8589934592:
                        Console.WriteLine("8589934592 DarkIronDwarf");

                        break;
                    case 17179869184:
                        Console.WriteLine("17179869184 Vulpera");

                        break;
                    case 34359738368:
                        Console.WriteLine("34359738368 MagharOrc");

                        break;
                }
            }
        }
        
        public static List<string> DecryptMask(double mask)
        {

            for (int i = value.Count - 1; i >= 0; i--)
            {
                if (mask - value.ElementAt(i) >= 0)
                {
                    mask -= value.ElementAt(i);
                    results.Add(value.ElementAt(i).ToString());
                    value.RemoveAt(i);
                }
            }

            return results;
        }
    }
}
