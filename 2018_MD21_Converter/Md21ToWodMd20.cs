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
            //On crée un buffer pour stocker les données du m2
            byte[] data = File.ReadAllBytes(fileName);

            convert(fileName, data);

        }

        public void convert(string fileName, byte[] buffer)
        {
            //On test si les 4 premiers octets valent MD21
            if (Encoding.UTF8.GetString(buffer, 0, 4) == "MD21")
            {
                //On vérifie si il s'agit d'un modèle avec le SKEL
                if (Encoding.UTF8.GetString(buffer).Contains("SKID"))
                {
                    string modelName = Path.GetFileName(fileName);
                    string[] array = modelName.Split('.');
                    string subName = array[0];

                    Console.WriteLine("- " + subName + " : model with SKEL, can't be processed");
                }
                //Si pas de skel, on vérifie la présence des chunks principaux de légion
                else if (Encoding.UTF8.GetString(buffer).Contains("AFID") || Encoding.UTF8.GetString(buffer).Contains("SFID")
                            || Encoding.UTF8.GetString(buffer).Contains("PFID") || Encoding.UTF8.GetString(buffer).Contains("BFID"))
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    //Comme c'est un md21 on commence la lecture à 4 et non à 0 pour passer le 'MD21'
                    br.BaseStream.Position = 4;
                    //On lit le M2Size avec un readuint32 qui nous déplacement de 4 octets dans la lecture.
                    uint MD20Size = br.ReadUInt32();

                    fs.Close();
                    br.Close();

                    //On refait un filestream en démarrant à la position du stream cette fois et non du binaryreader à 8 pour passer le header.
                    fs = new FileStream(fileName, FileMode.Open);
                    fs.Position = 8;
                    //Read va remplacer le fichier en stream (m2) (données du fichier) par toutes les données après le header.
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();

                    //Du temps de gaché mais il faut aussi enlever les 8 à la taille vu notre position avant, j'avais fait ça pour être safe sur le truncate.
                    fs = new FileStream(fileName, FileMode.Truncate);
                    fs.Write(buffer, 0, buffer.Length - 8);
                    fs.Close();

                    //Notre fichier n'a maintenant plus de header, maintenant on lui donne en longueur de données le MD20Size pour ne pas écrire les chunks.
                    //Après ça on se retrouve uniquement avec ce qu'il y a entre les chunk et le header.
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                    fs.Position = 0;
                    fs.SetLength((long)MD20Size);
                    fs.Close();

                    //Ici on remplace le flag de version du m2 (12 à légion, 10 à WOD) sur le modèle à cette étape, il est à à la 5ème position.
                    //On se met donc à 4 et on doit écrire la valeur 272 en bytes pour avoir le bon octet hexa.
                    //Le fichier a été converti en MD20 Wod.
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
                    //si pas de chunk on procède de la même façon mais en enlevant juste le header, pas de setlength
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
                //SI c'est un m2 sans header MD21 mais qu'il a des chunks !
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
                   //On crée une liste de long pour les positions des offsets.
                    List<long> positionOffset = new List<long>();
                    //Avec la fonction SearchPattern on peut chopper la position des offsets, il suffit de passer dans la méthode
                    //le buffer (données du fichier m2) et le nom du chunk converti en bytes. On add à la liste.
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
                    //On récupère la position du premier chunk via LINQ pour avoir le premier en terme de valeur, soit le plus petit 'long'.
                    long firstChunkPos = positionOffset.Any() ? positionOffset.Min() : 0;
                    //On récupère la taille de données totale du premier chunk à la fin du dernier chunk.
                    long sizeToEnd = buffer.Length - firstChunkPos;
                    //On retire cette valeur à la taille du buffer.
                    long dataLength = buffer.Length - sizeToEnd;

                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                    //On utilise le setlength en donnant notre valeur dataLength pour avoir la taille du m2 sans chunk. Voilà un m2 Wod like.
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
                    //Si c'est un m2 sans header et sans chunk, on modifie seulement le flag de version.
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

        //Recherche position offset bytes dans buffer.
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
