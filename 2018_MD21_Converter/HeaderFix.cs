using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_MD21_Converter
{
    class HeaderFix
    {
        private readonly string BaseFile;
        private readonly uint MD20Size;
        private readonly string[] ChunkNames = new[] { "SKID", "BFID", "AFID", "SFID", "PFID", "TXAC", "EXPT", "EXP2", "PADC", "PSBC", "PEDC" };

        public uint PFID { get; set; }
        public List<Row<uint>> SFID { get; set; } = new List<Row<uint>>();
        public List<AFID> AFID { get; set; } = new List<AFID>();
        public List<Row<uint>> BFID { get; set; } = new List<Row<uint>>();
        public uint SKID { get; set; }


        private Dictionary<string, long> Offsets = new Dictionary<string, long>();


        public HeaderFix(string filename)
        {
            BaseFile = filename;

            //Find offsets
            byte[] data = File.ReadAllBytes(filename);
            foreach (var c in ChunkNames)
                Offsets.Add(c, SearchPattern(data, Encoding.UTF8.GetBytes(c)));

            //Calculate MD20 size
            if (Encoding.UTF8.GetString(data, 0, 4) == "MD21")
                MD20Size = (uint)Offsets.Where(x => x.Value > -1).Min(x => x.Value) - 8;
            else
                MD20Size = (uint)data.Length;

            if (!Offsets.Any(x => x.Value > -1))
                return;

            //Read data
            using (var fs = new FileStream(BaseFile, FileMode.Open, FileAccess.ReadWrite))
            using (var br = new BinaryReader(fs))
            {
                foreach (var offset in Offsets)
                {
                    if (offset.Value == -1)
                        continue;

                    br.BaseStream.Position = offset.Value + 4; //Skip chunk name

                    uint size = br.ReadUInt32(); //Chunk size
                    switch (offset.Key)
                    {
                        case "SKID":
                            SKID = br.ReadUInt32();
                            break;
                        case "BFID":
                            for (int i = 0; i < (size / 4); i++)
                                BFID.Add(new Row<uint>(br.ReadUInt32()));
                            break;
                        case "AFID":
                            for (int i = 0; i < (size / 8); i++)
                                AFID.Add(new AFID(br));
                            break;
                        case "SFID":
                            for (int i = 0; i < (size / 4); i++)
                                SFID.Add(new Row<uint>(br.ReadUInt32()));
                            break;
                        case "PFID":
                            PFID = br.ReadUInt32();
                            break;
                    }
                }
            }
        }

        public void Save()
        {
            //Remove incomplete rows
            SFID.RemoveAll(x => x.FileId <= 0);
            BFID.RemoveAll(x => x.FileId <= 0);
            AFID.RemoveAll(x => x.FileId <= 0);

            using (var fs = new FileStream(BaseFile, FileMode.Open, FileAccess.ReadWrite))
            using (var bw = new BinaryWriter(fs))
            using (var br = new BinaryReader(fs))
            {
                //PFID
                if (PFID > 0)
                {
                    bw.BaseStream.Position = Offsets["PFID"] > -1 ? Offsets["PFID"] : bw.BaseStream.Length;
                    bw.Write(Encoding.UTF8.GetBytes("PFID"));
                    bw.Write(PFID);
                }

                //SKID
                if (SKID > 0)
                {
                    bw.BaseStream.Position = Offsets["SKID"] > -1 ? Offsets["SKID"] : bw.BaseStream.Length;
                    bw.Write(Encoding.UTF8.GetBytes("SKID"));
                    bw.Write(PFID);
                }

                //SFID
                if (SFID.Count > 0)
                {
                    bw.BaseStream.Position = Offsets["SFID"] > -1 ? Offsets["SFID"] : bw.BaseStream.Length;
                    bw.Write(Encoding.UTF8.GetBytes("SFID"));
                    bw.Write((uint)(SFID.Count * 4));
                    SFID.ForEach(x => bw.Write(x.FileId));
                }

                //BFID
                if (BFID.Count > 0)
                {
                    bw.BaseStream.Position = Offsets["BFID"] > -1 ? Offsets["BFID"] : bw.BaseStream.Length;
                    bw.Write(Encoding.UTF8.GetBytes("BFID"));
                    bw.Write((uint)(BFID.Count * 4));
                    BFID.ForEach(x => bw.Write(x.FileId));
                }

                //AFID
                if (AFID.Count > 0)
                {
                    bw.BaseStream.Position = Offsets["AFID"] > -1 ? Offsets["AFID"] : bw.BaseStream.Length;
                    bw.Write(Encoding.UTF8.GetBytes("AFID"));
                    bw.Write((uint)(AFID.Count * 8));
                    AFID.ForEach(x =>
                    {
                        bw.Write(x.AnimId);
                        bw.Write(x.SubAnimId);
                        bw.Write(x.FileId);
                    });
                }

                fs.Flush();

                FixHeader(bw, br);
            }
        }


        public void FixHeader(BinaryWriter bw, BinaryReader br)
        {
            //Validate if an M2
            if (Path.GetExtension(BaseFile).TrimStart('.').ToUpper() != "M2")
                return;

            byte[] magic = new byte[4];
            bw.BaseStream.Position = 0;
            bw.BaseStream.Read(magic, 0, magic.Length);

            if (Encoding.UTF8.GetString(magic) != "MD21")
            {
                //Store file to buffer with new header
                byte[] buffer = new byte[8 + bw.BaseStream.Length];
                Buffer.BlockCopy(Encoding.UTF8.GetBytes("MD21"), 0, buffer, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(MD20Size), 0, buffer, 4, 4);

                bw.BaseStream.Position = 0;
                bw.BaseStream.Read(buffer, 8, (int)bw.BaseStream.Length);

                //Overwrite file with buffer data
                bw.BaseStream.Position = 0;
                bw.Write(buffer);
                bw.BaseStream.Flush();
            }
            else
            {
                bw.BaseStream.Position = 4;
                bw.Write(MD20Size);
                bw.BaseStream.Flush();
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

    public class Row<T>
    {
        public T FileId { get; set; }

        public Row()
        {

        }

        public Row(T value)
        {
            FileId = value;
        }
    }

    public class AFID
    {
        public ushort AnimId { get; set; }
        public ushort SubAnimId { get; set; }
        public uint FileId { get; set; }

        public AFID()
        {

        }

        public AFID(BinaryReader br)
        {
            AnimId = br.ReadUInt16();
            SubAnimId = br.ReadUInt16();
            FileId = br.ReadUInt32();
        }
    }

    public class AnimChunk
    {

        public string fileName { get; set; }
        public string rootId { get; set; }
        public string AnimId { get; set; }
        public string subAnimId { get; set; }

    }

    public class BoneChunk
    {

        public string fileName { get; set; }
        public string rootId { get; set; }

    }

    public class SkinChunk
    {

        public string fileName { get; set; }
        public string rootId { get; set; }

    }

    public class PhysChunk
    {

        public string fileName { get; set; }
        public string rootId { get; set; }

    }
}
