using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roccus_MultiTool
{
    class Reader
    {
        public static void Work_TXID(string s)
        {

            string path = s;
            byte[] data = File.ReadAllBytes(path);
            List<long> positionOffset = new List<long>();
            if (Encoding.UTF8.GetString(data).Contains("TXID"))
            {
                long offset = SearchPattern(data, Encoding.UTF8.GetBytes("TXID"));
                positionOffset.Add(offset);
            }

            long firstChunkPos = positionOffset.Any() ? positionOffset.Min() : 0;

            using (var br = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.ReadWrite)))
            {
                br.BaseStream.Position = firstChunkPos + 4;
                Console.WriteLine(path.Substring(path.LastIndexOf("\\") + 1, path.Length - path.LastIndexOf("\\") - 1));
                Console.WriteLine("TXID offset : " + firstChunkPos);
                int nbTex = BitConverter.ToUInt16(br.ReadBytes(4), 0) / 4;
                Console.WriteLine("Nb Tex : " + nbTex);
                List<UInt32> values = new List<UInt32>();
                for (int i = 0; i < nbTex; i++)
                {
                    values.Add(BitConverter.ToUInt32(br.ReadBytes(4), 0));
                }
                foreach(UInt32 UI in values)
                {
                    if (UI == 0)
                    {
                        Console.WriteLine("Texture Root ID : " + UI);
                    }
                    else
                    {
                        Console.WriteLine(rootName(UI));
                    }
                }
            }
        }

        private static string rootName(UInt32 rootID)
        {
            string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "rootFiles");
            string result = "";

            if(File.Exists(Path.Combine(binaryPath + "\\BLP_Root")))
            {
                string[] BLPS = File.ReadAllLines(Path.Combine(binaryPath + "\\BLP_Root"));
                foreach(string line in BLPS)
                {
                    if(line.Split(',')[1] == rootID.ToString())
                    {
                        result = "Texture Root ID : " + rootID + " - " + line.Split(',')[0];
                        break;
                    }
                }
                if(result == "")
                {
                    result = "Texture Root ID : " + rootID;
                }
            }

            return result;
        }

        private static unsafe long SearchPattern(byte[] haystack, byte[] needle)
        {
            fixed (byte* h = haystack) fixed (byte* n = needle)
            {
                for (byte* hNext = h, hEnd = h + haystack.Length + 1 - needle.Length, nEnd = n + needle.Length; hNext < hEnd; hNext++)
                    for (byte* hInc = hNext, nInc = n; *nInc == *hInc; hInc++)
                        if (++nInc == nEnd)
                            return hNext - h;
                return -1;
            }
        }
    }
}
