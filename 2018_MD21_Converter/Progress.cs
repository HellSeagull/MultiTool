using Roccus_MultiTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roccus_MultiTool
{
    internal partial class Progress : Form
    {

        string DirectoryPath = System.Reflection.Assembly.GetEntryAssembly().Location.Substring(0, System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\"));
        Int32 ProcessedLines = 0;
        Int32 nbLines = 0;
        string FileName = string.Empty;
        Graphics g;

        public Progress()
        {
            InitializeComponent();
        }

        private void Progress_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
        }

        private void ParseStepOne()
        {
            try
            {
                //Sorting Filedata for Models
                string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string exeName = Path.GetFileName(binaryPath);
                binaryPath = Directory.GetCurrentDirectory();
                string[] file = File.ReadAllLines(Path.Combine(DirectoryPath, Resources.PathData));
                nbLines = file.Length;

                if (File.Exists(binaryPath + @"\tempProcess.dat"))
                {
                    File.Delete(binaryPath + @"\tempProcess.dat");
                }
                if (!Directory.Exists(binaryPath + @"\Model_Root"))
                {
                    Directory.CreateDirectory(binaryPath + @"\Model_Root");
                }

                StreamWriter sw = File.AppendText(binaryPath + @"\tempProcess.dat");
                foreach (string s in file)
                {
                    ProcessedLines++;
                    if (s.ToLowerInvariant().EndsWith(".m2") && s.ToLowerInvariant().Contains("character")
                        || s.ToLowerInvariant().EndsWith(".m2") && s.ToLowerInvariant().Contains("creature")
                        || s.ToLowerInvariant().EndsWith(".skin") && s.ToLowerInvariant().Contains("character")
                        || s.ToLowerInvariant().EndsWith(".skin") && s.ToLowerInvariant().Contains("creature"))
                    {
                        sw.WriteLine(s);
                        FileName = s;
                        PaintOnProgress(13, 39, (int)(ProcessedLines * 558f / (float)nbLines), 43, Color.Green);
                        Percentage.Text = (int)(ProcessedLines * 100f / (float)nbLines) + " %";
                        Percentage.Refresh();
                        Info.Text = FileName;
                        Info.Refresh();
                    }
                }

                sw.Close();

                nbLines = 0;
                ProcessedLines = 0;
                PaintOnProgress(13, 39, 558, 43, this.BackColor);
                ParseFinal();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
            }
        }

        private void ParseFinal()
        {
            try
            {
                string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string exeName = Path.GetFileName(binaryPath);
                binaryPath = Directory.GetCurrentDirectory();

                List<string>[] parse = new List<string>[2];
                parse[0] = new List<string>();
                parse[1] = new List<string>();

                var file_3 = File.ReadAllLines(binaryPath + @"\tempProcess.dat").Select(s => s.Split(' ')).ToArray();

                for (Int32 i = 0; i < file_3.Length; i++)
                {
                    parse[0].Add(file_3[i][0]);
                    parse[1].Add(file_3[i][2]);
                }

                nbLines = parse[0].Count;

                StreamWriter sw = File.AppendText(binaryPath + @"\Model_Root\Root");

                for (Int32 i = 0; i < parse[0].Count; i++)
                {
                    var line = parse[1].ElementAt(i);
                    var linelength = line.Length;
                    var lastSlashPosition = line.LastIndexOf('/');
                    var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                    sw.WriteLine(name + "," + parse[0].ElementAt(i).Substring(0, 10).TrimStart('0'));
                    FileName = name + "," + parse[0].ElementAt(i).Substring(0, 10).TrimStart('0');
                    ProcessedLines++;
                    PaintOnProgress(13, 39, (int)(ProcessedLines * 558f / (float)nbLines), 43, Color.Green);
                    Percentage.Text = (int)(ProcessedLines * 100f / (float)nbLines) + " %";
                    Percentage.Refresh();
                    Info.Text = FileName;
                    Info.Refresh();
                }

                sw.Close();
                sw.Dispose();

                binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                exeName = Path.GetFileName(binaryPath);
                binaryPath = Directory.GetCurrentDirectory();

                if (File.Exists(binaryPath + @"\tempProcess.dat"))
                {
                    File.Delete(binaryPath + @"\tempProcess.dat");
                }
                this.Visible = false;
                GeosetDecryptor frm = new GeosetDecryptor();
                frm.ShowDialog();
            }
            catch
            {
            }
        }

        private void Progress_Shown(object sender, EventArgs e)
        {
            if (File.Exists(DirectoryPath + @"\Model_Root\Root"))
            {
                FileInfo FI = new FileInfo(DirectoryPath + @"\Model_Root\Root");
                if (FI.Length > 0)
                {
                    GeosetDecryptor frm = new GeosetDecryptor();
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    Utils.Show("Empty File, trying to re-parse FileData", "Processing...", 2000);
                    if (File.Exists(Path.Combine(DirectoryPath, Resources.PathData)))
                    {
                        Utils.Show("Parsing FileData, please wait", "Processing...", 2000);
                        ParseStepOne();
                    }
                    else
                    {
                        MessageBox.Show("The FileData file is missing", "Error");
                        Application.Exit();
                    }
                }
            }
            else
            {
                if (File.Exists(Path.Combine(DirectoryPath, Resources.PathData)))
                {
                    Utils.Show("Parsing FileData, please wait", "Processing...", 2000);
                    ParseStepOne();
                }
                else
                {
                    MessageBox.Show("The FileData file is missing", "Error");
                    Application.Exit();
                }
            }
        }

        private void PaintOnProgress(int x, int y, float width, float height, Color color)
        {
            Graphics g = CreateGraphics();
            SolidBrush brush = new SolidBrush(color);
            RectangleF rect = new RectangleF(13, 39, (float)width, (float)height);
            g.FillRectangle(brush, rect);
            brush.Dispose();
        }

    }
}
