using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _2018_MD21_Converter
{
    class Md21ToWodMd20
    {

        private readonly string[] ChunkNames = new[] { "SKID", "BFID", "AFID", "SFID", "PFID", "TXAC", "EXPT", "EXP2", "PADC", "PSBC", "PEDC" };
        public uint PFID { get; set; }
        public uint SFID { get; set; }
        public uint AFID { get; set; }
        public uint BFID { get; set; }
        public uint TXAC { get; set; }
        private Dictionary<string, long> Offsets = new Dictionary<string, long>();

        public Md21ToWodMd20(string fileName)
        {
            byte[] data = File.ReadAllBytes(fileName);

            convert(fileName, data);

        }

        public void convert(string fileName, byte[] buffer)
        {

            if (Encoding.UTF8.GetString(buffer, 0, 4) == "MD21")
            {
                if (Encoding.UTF8.GetString(buffer).Contains("SKID"))
                {
                    string modelName = Path.GetFileName(fileName);
                    string[] array = modelName.Split('.');
                    string subName = array[0];

                    Console.WriteLine("- " + subName + " : model with SKEL, can't be processed");
                }
                else if (Encoding.UTF8.GetString(buffer).Contains("AFID") || Encoding.UTF8.GetString(buffer).Contains("SFID")
                            || Encoding.UTF8.GetString(buffer).Contains("PFID") || Encoding.UTF8.GetString(buffer).Contains("BFID"))
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    br.BaseStream.Position = 4;
                    uint MD20Size = br.ReadUInt32();

                    fs.Close();
                    br.Close();

                    fs = new FileStream(fileName, FileMode.Open);
                    fs.Position = 8;
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();

                    fs = new FileStream(fileName, FileMode.Truncate);
                    fs.Write(buffer, 0, buffer.Length - 8);
                    fs.Close();

                    fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                    fs.Position = 0;
                    fs.SetLength((long)MD20Size);
                    fs.Close();

                    fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.BaseStream.Position = 4;
                    bw.Write(BitConverter.GetBytes(272));
                    bw.Close();
                    fs.Close();

                    string modelName = Path.GetFileName(fileName);
                    string[] array = modelName.Split('.');
                    string subName = array[0];

                    Console.WriteLine("- " + subName + " : Processed");
                }
                else
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open);
                    fs.Position = 8;
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();

                    fs = new FileStream(fileName, FileMode.Truncate);
                    fs.Write(buffer, 0, buffer.Length - 8);
                    fs.Close();

                    fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.BaseStream.Position = 4;
                    bw.Write(BitConverter.GetBytes(272));
                    bw.Close();
                    fs.Close();

                    string modelName = Path.GetFileName(fileName);
                    string[] array = modelName.Split('.');
                    string subName = array[0];

                    Console.WriteLine("- " + subName + " : Processed, only the MD21 chunk needed to be removed");
                }
            }else if(Encoding.UTF8.GetString(buffer,0,4) == "MD20")
            {
                if (Encoding.UTF8.GetString(buffer).Contains("SKID"))
                {
                    string modelName = Path.GetFileName(fileName);
                    string[] array = modelName.Split('.');
                    string subName = array[0];

                    Console.WriteLine("- " + subName + " : model with SKEL, can't be processed");
                }
                else if (Encoding.UTF8.GetString(buffer).Contains("AFID") || Encoding.UTF8.GetString(buffer).Contains("SFID")
                           || Encoding.UTF8.GetString(buffer).Contains("PFID") || Encoding.UTF8.GetString(buffer).Contains("BFID"))
                {
                   
                    List<long> positionOffset = new List<long>();

                    if (Encoding.UTF8.GetString(buffer).Contains("AFID"))
                    {
                        long offset = SearchPattern(buffer, Encoding.UTF8.GetBytes("AFID"));
                        positionOffset.Add(offset);
                    }
                    if (Encoding.UTF8.GetString(buffer).Contains("BFID"))
                    {
                        long offset = SearchPattern(buffer, Encoding.UTF8.GetBytes("BFID"));
                        positionOffset.Add(offset);
                    }
                    if (Encoding.UTF8.GetString(buffer).Contains("SFID"))
                    {
                        long offset = SearchPattern(buffer, Encoding.UTF8.GetBytes("SFID"));
                        positionOffset.Add(offset);
                    }
                    if (Encoding.UTF8.GetString(buffer).Contains("PFID"))
                    {
                        long offset = SearchPattern(buffer, Encoding.UTF8.GetBytes("PFID"));
                        positionOffset.Add(offset);
                    }
                    if (Encoding.UTF8.GetString(buffer).Contains("TXAC"))
                    {
                        long offset = SearchPattern(buffer, Encoding.UTF8.GetBytes("TXAC"));
                        positionOffset.Add(offset);
                    }

                    long firstChunkPos = positionOffset.Any() ? positionOffset.Min() : 0;
                    long sizeToEnd = buffer.Length - firstChunkPos;
                    long dataLength = buffer.Length - sizeToEnd;

                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);

                    fs.Position = 0;
                    fs.SetLength((long)dataLength);
                    fs.Close();

                    fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.BaseStream.Position = 4;
                    bw.Write(BitConverter.GetBytes(272));
                    bw.Close();
                    fs.Close();

                    string modelName = Path.GetFileName(fileName);
                    string[] array = modelName.Split('.');
                    string subName = array[0];

                    Console.WriteLine("- " + subName + " : Processed, Legion chunks deleted");
                }
                else
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.BaseStream.Position = 4;
                    bw.Write(BitConverter.GetBytes(272));
                    bw.Close();
                    fs.Close();

                    string modelName = Path.GetFileName(fileName);
                    string[] array = modelName.Split('.');
                    string subName = array[0];

                    Console.WriteLine("- " + subName + " : Already in cata+/WoD format");
                }
            }
           

        }

        private unsafe long SearchPattern(byte[] haystack, byte[] needle)
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
