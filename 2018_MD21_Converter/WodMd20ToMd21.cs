using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Roccus_MultiTool
{
    class WodMd20ToMd21
    {
        //Voir classes chunk dans headerFix (structure des chunks
        List<AnimChunk> AnimChunk { get; set; } = new List<AnimChunk>();
        List<BoneChunk> BoneChunk { get; set; } = new List<BoneChunk>();
        List<SkinChunk> SkinChunk { get; set; } = new List<SkinChunk>();
        List<PhysChunk> PhysChunk { get; set; } = new List<PhysChunk>();

        public WodMd20ToMd21(string modelName, string path)
        {

            AnimChunk.Clear();
            BoneChunk.Clear();
            SkinChunk.Clear();
            PhysChunk.Clear();
            //Lecture des fichiers root du multitool pour peupler les listes.
            populateAnimChunk(modelName);
            populateBoneChunk(modelName);
            populateSkinChunk(modelName);
            populatePhysChunk(modelName);

            writeData(modelName, path);

        }

        public void populateBoneChunk(string modelName)
        {
            var file = File.ReadAllLines(@"rootFiles\BONE_Root").Select(s => s.Split(',')).ToArray();

            for (int i = 0; i < file.Length; i++)
            {

                if ((file[i][0].ToLowerInvariant()).StartsWith(modelName) && (file[i][0].ToLowerInvariant()).EndsWith(".bone"))
                {
                    string name = (file[i][0].ToLowerInvariant());
                    name = name.Replace("-", "");
                    name = Regex.Replace(name, @"[\d-]", string.Empty);
                    name = name.Replace("_.", ".");
                    name = name.Replace(".bone", "");
                    if (name == modelName)
                    {
                        BoneChunk.Add(new BoneChunk()
                        {
                            fileName = file[i][0],
                            rootId = file[i][1],
                        });
                    }

                }
                else if (((file[i][0].ToLowerInvariant()).StartsWith(modelName) && (file[i][0].ToLowerInvariant()).EndsWith(".bone")) == false)
                {

                    continue;

                }

            }

        }

        public void populateAnimChunk(string modelName)
        {

            var file = File.ReadAllLines(@"rootFiles\ANIM_Root").Select(s => s.Split(',')).ToArray();

            for (int i = 0; i < file.Length; i++)
            {

                if ((file[i][0].ToLowerInvariant()).StartsWith(modelName) && (file[i][0].ToLowerInvariant()).EndsWith(".anim"))
                {
                    string name = (file[i][0].ToLowerInvariant());
                    name = name.Replace(".anim", "");
                    name = name.Replace("-", "");
                    name = Regex.Replace(name, @"[\d-]", string.Empty);
                    if (name == modelName)
                    {
                        AnimChunk.Add(new AnimChunk()
                        {
                            fileName = file[i][0],
                            rootId = file[i][1],
                            //Particularité des AFID, parfois il y a un subanimID
                            AnimId = file[i][0].Substring(modelName.Length, 4).TrimStart('0'),
                            subAnimId = file[i][0].Substring(modelName.Length + 6, 1)
                        });
                    }

                }
                else if (((file[i][0].ToLowerInvariant()).StartsWith(modelName) && (file[i][0].ToLowerInvariant()).EndsWith(".anim")) == false)
                {

                    continue;

                }

            }

        }

        public void populateSkinChunk(string modelName)
        {
            var file = File.ReadAllLines(@"rootFiles\SKIN_Root").Select(s => s.Split(',')).ToArray();

            for (int i = 0; i < file.Length; i++)
            {

                if ((file[i][0].ToLowerInvariant()).StartsWith(modelName) && (file[i][0].ToLowerInvariant()).EndsWith(".skin"))
                {
                    string name = (file[i][0].ToLowerInvariant());
                    name = name.Replace(".skin", "");
                    name = name.Replace("-", "");
                    name = name.Replace("_lod", "");
                    if (name.StartsWith("collections") || name.StartsWith("shoulder") || name.StartsWith("helm") || name.StartsWith("lshoulder") || name.StartsWith("rshoulder"))
                    {
                        name = name.Substring(0, name.Length - 2);
                    }
                    else
                    {
                        //Fix Weapon
                        if (name.Length == modelName.Length + 2)
                            name = name.Substring(0, name.Length - 2);
                    }
                    if (name == modelName)
                    {
                        SkinChunk.Add(new SkinChunk()
                        {
                            fileName = file[i][0],
                            rootId = file[i][1],

                        });
                    }

                }
                else if (((file[i][0].ToLowerInvariant()).StartsWith(modelName) && (file[i][0].ToLowerInvariant()).EndsWith(".skin")) == false)
                {

                    continue;

                }

            }

        }

        public void populatePhysChunk(string modelName)
        {
            var file = File.ReadAllLines(@"rootFiles\PHYS_Root").Select(s => s.Split(',')).ToArray();

            for (int i = 0; i < file.Length; i++)
            {

                if ((file[i][0].ToLowerInvariant()).StartsWith(modelName) && (file[i][0].ToLowerInvariant()).EndsWith(".phys"))
                {
                    string name = (file[i][0].ToLowerInvariant());
                    name = name.Replace(".phys", "");
                    name = name.Replace("-", "");
                    name = Regex.Replace(name, @"[\d-]", string.Empty);
                    if (name == modelName)
                    {
                        PhysChunk.Add(new PhysChunk()
                        {
                            fileName = file[i][0],
                            rootId = file[i][1],

                        });
                    }

                }
                else if (((file[i][0].ToLowerInvariant()).StartsWith(modelName) && (file[i][0].ToLowerInvariant()).EndsWith(".phys")) == false)
                {

                    continue;

                }

            }
        }

        public void writeData(string modelName, string path)
        {
            //Liste pour contenir les éléments des Chunk et obtenir leur taille de lecture.
            List<UInt32> afidSize = new List<UInt32>();
            List<UInt32> bfidSize = new List<UInt32>();
            List<UInt32> sfidSize = new List<UInt32>();
            List<UInt32> pfidSize = new List<UInt32>();

            if (AnimChunk != null)
            {
                foreach (var data in AnimChunk)
                {

                    string valued1 = data.AnimId.ToString();
                    string valued2 = data.rootId.ToString();
                    if (valued1 != string.Empty)
                    {

                        UInt32 animIdValue = UInt32.Parse(valued1);
                        afidSize.Add(animIdValue);

                    }

                    if (valued2 != string.Empty)
                    {


                        UInt32 rootAnimIdValue = UInt32.Parse(valued2);
                        afidSize.Add(rootAnimIdValue);

                    }
                }
            }

            if (BoneChunk != null)
            {

                foreach (var data in BoneChunk)
                {

                    string valued = data.rootId.ToString();
                    if (valued != string.Empty)
                    {

                        UInt32 rootBoneIdValue = UInt32.Parse(valued);
                        bfidSize.Add(rootBoneIdValue);

                    }

                }

            }

            if (SkinChunk != null)
            {

                foreach (var data in SkinChunk)
                {
                    string valued = data.rootId.ToString();
                    if (valued != string.Empty)
                    {

                        UInt32 rootSkinIdValue = UInt32.Parse(valued);
                        sfidSize.Add(rootSkinIdValue);

                    }

                }

            }

            if (PhysChunk != null)
            {

                foreach (var data in PhysChunk)
                {

                    string valued = data.rootId.ToString();
                    if (valued != string.Empty)
                    {

                        UInt32 rootPhysIdValue = UInt32.Parse(valued);
                        pfidSize.Add(rootPhysIdValue);

                    }

                }

            }
            //On crée le buffer de donénes du m2
            byte[] modelData = File.ReadAllBytes(path);

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            using (var bw = new BinaryWriter(fs))
            using (var br = new BinaryReader(fs))
            {
                //Etant un modèle m2 Wod, on se place à la toute fin stream du fichier ainsi.
                bw.BaseStream.Seek(modelData.Length, SeekOrigin.Current);

                if (AnimChunk != null)
                {

                    if (afidSize.Count != 0)
                    {
                        //On commence par l'écriture d'AFID, pour ça il faut donct un getbytes 'AFID' et utiliser le nombre d'éléments * 4 de la liste Size associée
                        //Pour avoir la bonne size d'AFID dans le m2. le write écrira 4 octets et déplacera le curseur à chaque fois.
                        bw.Write(Encoding.UTF8.GetBytes("AFID"));
                        bw.Write(BitConverter.GetBytes((afidSize.Count) * 4));

                    }

                    foreach (var data in AnimChunk)
                    {

                        string valued1 = Convert.ToString(data.AnimId);
                        string valued2 = Convert.ToString(data.subAnimId);
                        string valued3 = Convert.ToString(data.rootId);

                        if (valued1 != string.Empty && valued2 != string.Empty)
                        {
                            //AnimID et subAnimID sans des Uint16 donc sur 2 octets chacun dans la structure.
                            UInt16 animIdValue = UInt16.Parse(valued1);
                            UInt16 subAnimIdValue = UInt16.Parse(valued2);
                            //Chaque write sur un uint16 écrira 2 octets, ainsi on aura la vraie structure avec un total de 4 octets incluant
                            //l'animid et son subanimid pour la différenciation.
                            bw.Write(BitConverter.GetBytes(animIdValue));
                            bw.Write(BitConverter.GetBytes(subAnimIdValue));

                        }

                        if (valued3 != string.Empty)
                        {
                            //Une fois l'animid écrit il faut écrire à sa suite le rootid.
                            UInt32 rootAnimIdValue = UInt32.Parse(valued3);
                            bw.Write(BitConverter.GetBytes(rootAnimIdValue));

                        }

                    }

                }
                //On répète l'opération pour cahque chunk mais cette fois-ci pas de particularité, il suffit décrire le root_id
                if (BoneChunk != null)
                {

                    if (bfidSize.Count != 0)
                    {

                        bw.Write(Encoding.UTF8.GetBytes("BFID"));
                        bw.Write(BitConverter.GetBytes((bfidSize.Count) * 4));

                    }

                    foreach (var data in BoneChunk)
                    {

                        string valued = (Convert.ToString(data.rootId));
                        if (valued != string.Empty)
                        {

                            UInt32 rootAnimIdValue = UInt32.Parse(valued);
                            bw.Write(BitConverter.GetBytes(rootAnimIdValue));

                        }

                    }

                }

                if (SkinChunk != null)
                {

                    if (sfidSize.Count != 0)
                    {

                        bw.Write(Encoding.UTF8.GetBytes("SFID"));
                        bw.Write(BitConverter.GetBytes((sfidSize.Count) * 4));

                    }

                    foreach (var data in SkinChunk)
                    {

                        string valued = Convert.ToString(data.rootId);
                        if (valued != string.Empty)
                        {

                            UInt32 rootSkinIdValue = UInt32.Parse(valued);
                            bw.Write(BitConverter.GetBytes(rootSkinIdValue));

                        }

                    }

                }

                if (PhysChunk != null)
                {

                    if (pfidSize.Count != 0)
                    {

                        bw.Write(Encoding.UTF8.GetBytes("PFID"));
                        bw.Write(BitConverter.GetBytes((pfidSize.Count) * 4));

                    }

                    foreach (var data in PhysChunk)
                    {

                        string valued = Convert.ToString(data.rootId);
                        if (valued != string.Empty)
                        {

                            UInt32 rootPhysIdValue = UInt32.Parse(valued);
                            bw.Write(BitConverter.GetBytes(rootPhysIdValue));

                        }

                    }

                }

                //Check sur les modèles de race car ceux-ci possède un TXAC (je sais pas vraiment en quoi il est utile)
                if (modelName == "orcmale_hd" || modelName == "orcfemale_hd" || modelName == "humanmale_hd" || modelName == "humanfemale_hd"
                        || modelName == "dwarfmale_hd" || modelName == "dwarffemale_hd" || modelName == "gnomemale_hd" || modelName == "gnomefemale_hd"
                        || modelName == "nightelfmale_hd" || modelName == "nightelffemale_hd" || modelName == "draeneimale_hd" || modelName == "draeneifemelle_hd"
                        || modelName == "pandarenmale" || modelName == "pandarenfemale" || modelName == "taurenmale_hd" || modelName == "taurenfemale_hd"
                        || modelName == "trollmale_hd" || modelName == "trollfemale_hd" || modelName == "scourgemale_hd" || modelName == "scourgefemale_hd"
                        || modelName == "bloodelfmale_hd" || modelName == "bloodelffemale_hd")
                {
                    //Ecriture du TXAC à la fin, taille fixe qui est la même pour tous.
                    bw.Write(Encoding.UTF8.GetBytes("TXAC"));
                    Int16 v = 14;
                    Int16 u = Int16.Parse(v.ToString());
                    //le U est le TXACSize
                    bw.Write(BitConverter.GetBytes(u));

                    bw.Write(BitConverter.GetBytes(0x00));
                    bw.Write(BitConverter.GetBytes(0x00));
                    bw.Write(BitConverter.GetBytes(0x00));
                    bw.Write(BitConverter.GetBytes(0x00));

                    fs.SetLength(fs.Length - 2);
                    //Je sais pas vraiment l'expliquer mais pour avoir la bonne taille pour le txac à la fin je dois truncate la fin en enlevant 2 à la taille du stream.
                    //Le fichier est maintenant peuplé de ses chunks.
                }

                br.Close();
                bw.Close();
                fs.Close();
                //Maintenant qu'on a un md21 avec header and chunk, il faut se placer à l'offset 12 pour changer le flag de version donc 12 dans 010Editor
                FileStream fs2 = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                BinaryWriter bw2 = new BinaryWriter(fs2);
                bw2.BaseStream.Position = 12;
                bw2.Write(BitConverter.GetBytes(274));
                bw2.Close();
                fs2.Close();
                //Done !
                afidSize.Clear();
                bfidSize.Clear();
                sfidSize.Clear();
                pfidSize.Clear();

            }

        }
    }
}
