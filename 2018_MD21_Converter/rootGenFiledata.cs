using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Roccus_MultiTool
{
    class rootGenFiledata
    {

        public void SelectM2Root()
        {

            //Sorting Filedata
            string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");
            var file = File.ReadAllLines(binaryPath + @"\Filedata.csv");
            if (!File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Create(binaryPath + @"\tempProcess.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            StreamWriter sw = File.AppendText(binaryPath + @"\tempProcess.dat");

            foreach (string s in file)
            {
                if(s.Contains(".m2") && s.Contains("creature") || s.Contains(".m2") && s.Contains("character") || s.Contains(".m2") && s.Contains("item"))
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            var file_2 = File.ReadAllLines(binaryPath + @"\tempProcess.dat");
            if (!File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Create(binaryPath + @"\tempProcess2.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

            sw = File.AppendText(binaryPath + @"\tempProcess2.dat");

            foreach (string s in file_2)
            {
                if (s.Contains("hd_shadowmoon") || s.Contains("_sdr"))
                {
                   
                }
                else
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            List<string>[] parse = new List<string>[2];
            parse[0] = new List<string>();
            parse[1] = new List<string>();

            var file_3 = File.ReadAllLines(binaryPath + @"\tempProcess2.dat").Select(s => s.Split(' ')).ToArray();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "rootFiles\\M2_Root");

            for (Int32 i = 0; i < file_3.Length; i++)
            {
                parse[0].Add(file_3[i][0]);
                parse[1].Add(file_3[i][2]);
            }

            if (!File.Exists(binaryPath))
            {
                File.Create(binaryPath).Close();
            }
            else
            {
                File.Delete(binaryPath);
            }

            sw = File.AppendText(binaryPath);

            for(Int32 i = 0; i < parse[0].Count; i++)
            {
                var line = parse[1].ElementAt(i);
                var linelength = line.Length;
                var lastSlashPosition = line.LastIndexOf('/');
                var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                sw.WriteLine(name + "," + parse[0].ElementAt(i).Substring(0, 10).TrimStart('0'));
            }

            sw.Close();
            sw.Dispose();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");

            if(File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            if (File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

        }

        public void SelectANIMRoot()
        {
            //Sorting Filedata
            string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");
            var file = File.ReadAllLines(binaryPath + @"\Filedata.csv");
            if (!File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Create(binaryPath + @"\tempProcess.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            StreamWriter sw = File.AppendText(binaryPath + @"\tempProcess.dat");

            foreach (string s in file)
            {
                if (s.Contains(".anim") && s.Contains("creature") || s.Contains(".anim") && s.Contains("character") || s.Contains(".anim") && s.Contains("item"))
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            var file_2 = File.ReadAllLines(binaryPath + @"\tempProcess.dat");
            if (!File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Create(binaryPath + @"\tempProcess2.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

            sw = File.AppendText(binaryPath + @"\tempProcess2.dat");

            foreach (string s in file_2)
            {
                if (s.Contains("hd_shadowmoon") || s.Contains("_sdr"))
                {

                }
                else
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            List<string>[] parse = new List<string>[2];
            parse[0] = new List<string>();
            parse[1] = new List<string>();

            var file_3 = File.ReadAllLines(binaryPath + @"\tempProcess2.dat").Select(s => s.Split(' ')).ToArray();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "rootFiles\\ANIM_Root");

            for (Int32 i = 0; i < file_3.Length; i++)
            {
                parse[0].Add(file_3[i][0]);
                parse[1].Add(file_3[i][2]);
            }

            if (!File.Exists(binaryPath))
            {
                File.Create(binaryPath).Close();
            }
            else
            {
                File.Delete(binaryPath);
            }

            sw = File.AppendText(binaryPath);

            for (Int32 i = 0; i < parse[0].Count; i++)
            {
                var line = parse[1].ElementAt(i);
                var linelength = line.Length;
                var lastSlashPosition = line.LastIndexOf('/');
                var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                sw.WriteLine(name + "," + parse[0].ElementAt(i).Substring(0, 10).TrimStart('0'));
            }

            sw.Close();
            sw.Dispose();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");

            if (File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            if (File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

        }

        public void SelectBONERoot()
        {
            //Sorting Filedata
            string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");
            var file = File.ReadAllLines(binaryPath + @"\Filedata.csv");
            if (!File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Create(binaryPath + @"\tempProcess.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            StreamWriter sw = File.AppendText(binaryPath + @"\tempProcess.dat");

            foreach (string s in file)
            {
                if (s.Contains(".bone") && s.Contains("creature") || s.Contains(".bone") && s.Contains("character") || s.Contains(".bone") && s.Contains("item"))
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            var file_2 = File.ReadAllLines(binaryPath + @"\tempProcess.dat");
            if (!File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Create(binaryPath + @"\tempProcess2.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

            sw = File.AppendText(binaryPath + @"\tempProcess2.dat");

            foreach (string s in file_2)
            {
                if (s.Contains("hd_shadowmoon") || s.Contains("_sdr"))
                {

                }
                else
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            List<string>[] parse = new List<string>[2];
            parse[0] = new List<string>();
            parse[1] = new List<string>();

            var file_3 = File.ReadAllLines(binaryPath + @"\tempProcess2.dat").Select(s => s.Split(' ')).ToArray();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "rootFiles\\BONE_Root");

            for (Int32 i = 0; i < file_3.Length; i++)
            {
                parse[0].Add(file_3[i][0]);
                parse[1].Add(file_3[i][2]);
            }

            if (!File.Exists(binaryPath))
            {
                File.Create(binaryPath).Close();
            }
            else
            {
                File.Delete(binaryPath);
            }

            sw = File.AppendText(binaryPath);

            for (Int32 i = 0; i < parse[0].Count; i++)
            {
                var line = parse[1].ElementAt(i);
                var linelength = line.Length;
                var lastSlashPosition = line.LastIndexOf('/');
                var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                sw.WriteLine(name + "," + parse[0].ElementAt(i).Substring(0, 10).TrimStart('0'));
            }

            sw.Close();
            sw.Dispose();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");

            if (File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            if (File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

        }

        public void SelectSKINRoot()
        {
            //Sorting Filedata
            string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");
            var file = File.ReadAllLines(binaryPath + @"\Filedata.csv");
            if (!File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Create(binaryPath + @"\tempProcess.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            StreamWriter sw = File.AppendText(binaryPath + @"\tempProcess.dat");

            foreach (string s in file)
            {
                if (s.Contains(".skin") && s.Contains("creature") || s.Contains(".skin") && s.Contains("character") || s.Contains(".skin") && s.Contains("item"))
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            var file_2 = File.ReadAllLines(binaryPath + @"\tempProcess.dat");
            if (!File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Create(binaryPath + @"\tempProcess2.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

            sw = File.AppendText(binaryPath + @"\tempProcess2.dat");

            foreach (string s in file_2)
            {
                if (s.Contains("hd_shadowmoon") || s.Contains("_sdr"))
                {

                }
                else
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            List<string>[] parse = new List<string>[2];
            parse[0] = new List<string>();
            parse[1] = new List<string>();

            var file_3 = File.ReadAllLines(binaryPath + @"\tempProcess2.dat").Select(s => s.Split(' ')).ToArray();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "rootFiles\\SKIN_Root");

            for (Int32 i = 0; i < file_3.Length; i++)
            {
                parse[0].Add(file_3[i][0]);
                parse[1].Add(file_3[i][2]);
            }

            if (!File.Exists(binaryPath))
            {
                File.Create(binaryPath).Close();
            }
            else
            {
                File.Delete(binaryPath);
            }

            sw = File.AppendText(binaryPath);

            for (Int32 i = 0; i < parse[0].Count; i++)
            {
                var line = parse[1].ElementAt(i);
                var linelength = line.Length;
                var lastSlashPosition = line.LastIndexOf('/');
                var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                sw.WriteLine(name + "," + parse[0].ElementAt(i).Substring(0, 10).TrimStart('0'));
            }

            sw.Close();
            sw.Dispose();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");

            if (File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            if (File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }
        }

        public void SelectPHYSRoot()
        {
            //Sorting Filedata
            string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");
            var file = File.ReadAllLines(binaryPath + @"\Filedata.csv");
            if (!File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Create(binaryPath + @"\tempProcess.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            StreamWriter sw = File.AppendText(binaryPath + @"\tempProcess.dat");

            foreach (string s in file)
            {
                if (s.Contains(".phys") && s.Contains("creature") || s.Contains(".phys") && s.Contains("character") || s.Contains(".phys") && s.Contains("item"))
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            var file_2 = File.ReadAllLines(binaryPath + @"\tempProcess.dat");
            if (!File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Create(binaryPath + @"\tempProcess2.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

            sw = File.AppendText(binaryPath + @"\tempProcess2.dat");

            foreach (string s in file_2)
            {
                if (s.Contains("hd_shadowmoon") || s.Contains("_sdr"))
                {

                }
                else
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            List<string>[] parse = new List<string>[2];
            parse[0] = new List<string>();
            parse[1] = new List<string>();

            var file_3 = File.ReadAllLines(binaryPath + @"\tempProcess2.dat").Select(s => s.Split(' ')).ToArray();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "rootFiles\\PHYS_Root");

            for (Int32 i = 0; i < file_3.Length; i++)
            {
                parse[0].Add(file_3[i][0]);
                parse[1].Add(file_3[i][2]);
            }

            if (!File.Exists(binaryPath))
            {
                File.Create(binaryPath).Close();
            }
            else
            {
                File.Delete(binaryPath);
            }

            sw = File.AppendText(binaryPath);

            for (Int32 i = 0; i < parse[0].Count; i++)
            {
                var line = parse[1].ElementAt(i);
                var linelength = line.Length;
                var lastSlashPosition = line.LastIndexOf('/');
                var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                sw.WriteLine(name + "," + parse[0].ElementAt(i).Substring(0, 10).TrimStart('0'));
            }

            sw.Close();
            sw.Dispose();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");

            if (File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            if (File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }
        }

        public void SelectBLPRoot()
        {
            //Sorting Filedata
            string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");
            var file = File.ReadAllLines(binaryPath + @"\Filedata.csv");
            if (!File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Create(binaryPath + @"\tempProcess.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            StreamWriter sw = File.AppendText(binaryPath + @"\tempProcess.dat");

            foreach (string s in file)
            {
                if (s.Contains(".blp") && s.Contains("creature") || s.Contains(".blp") && s.Contains("character") || s.Contains(".blp") && s.Contains("item"))
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            var file_2 = File.ReadAllLines(binaryPath + @"\tempProcess.dat");
            if (!File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Create(binaryPath + @"\tempProcess2.dat").Close();
            }
            else
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }

            sw = File.AppendText(binaryPath + @"\tempProcess2.dat");

            foreach (string s in file_2)
            {
                if (s.Contains("hd_shadowmoon") || s.Contains("_sdr"))
                {

                }
                else
                {
                    sw.WriteLine(s);
                }
            }

            sw.Close();
            sw.Dispose();

            List<string>[] parse = new List<string>[2];
            parse[0] = new List<string>();
            parse[1] = new List<string>();

            var file_3 = File.ReadAllLines(binaryPath + @"\tempProcess2.dat").Select(s => s.Split(' ')).ToArray();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "rootFiles\\BLP_Root");

            for (Int32 i = 0; i < file_3.Length; i++)
            {
                parse[0].Add(file_3[i][0]);
                parse[1].Add(file_3[i][2]);
            }

            if (!File.Exists(binaryPath))
            {
                File.Create(binaryPath).Close();
            }
            else
            {
                File.Delete(binaryPath);
            }

            sw = File.AppendText(binaryPath);

            for (Int32 i = 0; i < parse[0].Count; i++)
            {
                var line = parse[1].ElementAt(i);
                var linelength = line.Length;
                var lastSlashPosition = line.LastIndexOf('/');
                var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                sw.WriteLine(name + "," + parse[0].ElementAt(i).Substring(0, 10).TrimStart('0'));
            }

            sw.Close();
            sw.Dispose();

            binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            exeName = Path.GetFileName(binaryPath);
            binaryPath = binaryPath.Replace(exeName, "Filedata");

            if (File.Exists(binaryPath + @"\tempProcess.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess.dat");
            }

            if (File.Exists(binaryPath + @"\tempProcess2.dat"))
            {
                File.Delete(binaryPath + @"\tempProcess2.dat");
            }
        }

    }
}
