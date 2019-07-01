using Roccus_MultiTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roccus_MultiTool
{
    internal partial class GeosetDecryptor : Form
    {

        string DirectoryPath = System.Reflection.Assembly.GetEntryAssembly().Location.Substring(0, System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf("\\"));

        public GeosetDecryptor()
        {
            InitializeComponent();
        }

        private void GeosetDecryptor_Load(object sender, EventArgs e)
        {
            addColorText(ResultBox, Color.Red, "Root File Status : OK");
            gridSkin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.Select();
            this.Activate();
            DisplayID.Select();
            DisplayID.Focus();
        }

        private void DisplayID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                AddCombo_Click(sender, e);
                DisplayID.Clear();
            }
        }

        private void AddCombo_Click(object sender, EventArgs e)
        {
            if (Regex.Match(DisplayID.Text.Trim(), "^[0-9]+$").Success)
            {
                LstSearch.Items.Add("Display ID : " + DisplayID.Text.Trim());
                LstSearch.SelectedIndex = LstSearch.FindStringExact("Display ID : " + DisplayID.Text.Trim());
                addColorText(ResultBox, Color.Blue, "DisplayID Added to Decrypt List : " + LstSearch.SelectedItem.ToString().Split(':')[1].Trim());
            }
            else
            {
                Utils.Show("Enter a DisplayID please", "Error", 2000);
                DisplayID.Clear();
            }
        }

        private delegate void addColorTextBox(RichTextBox t, Color color, string text);

        private void addColorText(RichTextBox t, Color color, string text)
        {
            if (t.InvokeRequired)
            {
                t.Invoke(new addColorTextBox(addColorText), t, color, text);
                return;
            }
            t.SelectionStart = t.TextLength;
            t.SelectionLength = 0;
            t.SelectionColor = color;
            t.AppendText(text + "\n\n");
            t.SelectionColor = t.ForeColor;
        }

        private void ResultBox_TextChanged(object sender, EventArgs e)
        {
            ResultBox.ScrollToCaret();
        }

        private void GetSkin_Click(object sender, EventArgs e)
        {
            var t = new Thread(() =>
            {
                FileSearcher.Filter = "Skin File (Skin)|*.skin";
                if (FileSearcher.ShowDialog() == DialogResult.OK && FileSearcher.FileName.Contains("00.skin"))
                {
                    addColorText(ResultBox, Color.Orange, "Skin00 of Model : " + FileSearcher.FileName.Split('.')[0].Substring(FileSearcher.FileName.Split('.')[0].LastIndexOf("\\") + 1, FileSearcher.FileName.Split('.')[0].Length - FileSearcher.FileName.Split('.')[0].LastIndexOf("\\") - 3) + " opened");
                    List<UInt16> lstGeoset = new List<UInt16>();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Model_Name");
                    dt.Columns.Add("Geosets");
                    using (BinaryReader br = new BinaryReader(new FileStream(FileSearcher.FileName, FileMode.Open, FileAccess.Read)))
                    {
                        br.BaseStream.Position = 28;
                        UInt16 nbSubmesh = BitConverter.ToUInt16(br.ReadBytes(4), 0);
                        addColorText(ResultBox, Color.Black, "Number of SubMesh : " + nbSubmesh + " containing duplicates meshes .XXX");
                        addColorText(ResultBox, Color.Black, "Grid results will contain only one occurence of each geosets");
                        long offsetToRead = BitConverter.ToUInt32(br.ReadBytes(4), 0);
                        br.BaseStream.Position += 4;
                        long offsetStop = BitConverter.ToUInt32(br.ReadBytes(4), 0);
                        long counter = 0;
                        br.BaseStream.Position = offsetToRead;
                        bool Exists = false;
                        do
                        {
                            UInt16 geosetID = BitConverter.ToUInt16(br.ReadBytes(4), 0);
                            if (geosetID != 0)
                            {
                                if (lstGeoset.Any())
                                {
                                    foreach (UInt16 value in lstGeoset)
                                    {
                                        if (value == geosetID)
                                            Exists = true;
                                        else
                                            Exists = false;
                                    }
                                }

                                if (!Exists)
                                {
                                    dt.Rows.Add(new object[] { FileSearcher.FileName.Split('.')[0].Substring(FileSearcher.FileName.Split('.')[0].LastIndexOf("\\") + 1, FileSearcher.FileName.Split('.')[0].Length - FileSearcher.FileName.Split('.')[0].LastIndexOf("\\") - 3), geosetID });
                                    lstGeoset.Add(geosetID);
                                }

                            }
                            br.BaseStream.Position += 44;
                            counter += 48;
                        } while (counter != (offsetStop - offsetToRead));
                    }

                    gridSkin.BeginInvoke(new MethodInvoker(() => {
                        gridSkin.DataSource = dt;
                        lstGeoset.Clear();
                    }));
                }
                else
                {
                    Utils.Show("Wrong file type", "Error", 2000);
                }
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void GeosetDecryptor_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c taskkill /f /im GeosetDecryptor.exe");
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            Process.Start(psi).WaitForExit();
        }

        private void ListGeosets_Click(object sender, EventArgs e)
        {
            DataTable displayInfoData = new DataTable();

            displayInfoData.Columns.Add("ModelName");
            displayInfoData.Columns.Add("DisplayId");
            displayInfoData.Columns.Add("Geosets");
            string[] GeosetData = null;
            string[] DisplayInfo = null;
            string[] ModelData = null;

            if (!LegionBox.Checked)
            {
                GeosetData = File.ReadAllLines(Path.Combine(DirectoryPath, Resources.DisplayInfoDataBFA));
                for (int i = 1; i < GeosetData.Length; i++)
                {
                    GeosetData[i] = Regex.Replace(GeosetData[i], @"(?<=\d),(?=\d)", ".");
                    GeosetData[i] = GeosetData[i].Replace("\"", "").Split(',')[3] + "," + GeosetData[i].Replace("\"", "").Split(',')[2] + "," + GeosetData[i].Replace("\"", "").Split(',')[1];
                }

                DisplayInfo = File.ReadAllLines(Path.Combine(DirectoryPath, Resources.DisplayInfoBFA));
                for (int i = 1; i < DisplayInfo.Length; i++)
                {
                    DisplayInfo[i] = Regex.Replace(DisplayInfo[i], @"(?<=\d),(?=\d)", ".");
                    DisplayInfo[i] = DisplayInfo[i].Replace("\"", "").Split(',')[0] + "," + DisplayInfo[i].Replace("\"", "").Split(',')[1];
                }

                ModelData = File.ReadAllLines(Path.Combine(DirectoryPath, Resources.ModelDataBFA));
                for (int i = 1; i < ModelData.Length; i++)
                {
                    ModelData[i] = Regex.Replace(ModelData[i], @"(?<=\d),(?=\d)", ".");
                    ModelData[i] = ModelData[i].Split(',')[0] + "," + ModelData[i].Split(',')[8];
                    ModelData[i] = ModelData[i].Replace("\"", "");
                }
            }
            else
            {
                DisplayInfo = File.ReadAllLines(Path.Combine(DirectoryPath, Resources.DisplayInfo735));
                for (int i = 1; i < DisplayInfo.Length; i++)
                {
                    DisplayInfo[i] = Regex.Replace(DisplayInfo[i], @"(?<=\d),(?=\d)", ".");
                    DisplayInfo[i] = DisplayInfo[i].Replace("\"", "").Split(',')[0] + "," + DisplayInfo[i].Replace("\"", "").Split(',')[2]
                         + "," + DisplayInfo[i].Replace("\"", "").Split(',')[15];
                }

                ModelData = File.ReadAllLines(Path.Combine(DirectoryPath, Resources.ModelData735));
                for (int i = 1; i < ModelData.Length; i++)
                {
                    ModelData[i] = Regex.Replace(ModelData[i], @"(?<=\d),(?=\d)", ".");
                    ModelData[i] = ModelData[i].Split(',')[0] + "," + ModelData[i].Split(',')[25];
                    ModelData[i] = ModelData[i].Replace("\"", "");
                }
            }

            string[] ModelRoot = File.ReadAllLines(Path.Combine(DirectoryPath, @"Model_Root\Root"));

            if (LstSearch.Items.Count > 0)
            {
                addColorText(ResultBox, Color.Red, "Processing please wait...");
                for (int i = 0; i < LstSearch.Items.Count; i++)
                {
                    string cbValue = LstSearch.GetItemText(LstSearch.Items[i]).Split(':')[1].Trim();
                    if (!LegionBox.Checked)
                    {
                        foreach (string s in GeosetData)
                        {
                            if (s.Split(',')[0] == cbValue)
                            {
                                bool working = true;
                                if (working)
                                {
                                    foreach (string s_1 in DisplayInfo)
                                    {
                                        if (s.Split(',')[0] == s_1.Split(',')[0])
                                        {
                                            foreach (string s_2 in ModelData)
                                            {
                                                if (s_1.Split(',')[1] == s_2.Split(',')[0])
                                                {
                                                    foreach (string s_3 in ModelRoot)
                                                    {
                                                        if (s_2.Split(',')[1] == s_3.Split(',')[1])
                                                        {
                                                            if(int.Parse(s.Split(',')[1]) < 10)
                                                            {
                                                                displayInfoData.Rows.Add(new object[] { s_3.Split(',')[0].Split('.')[0], s.Split(',')[0], (int.Parse(s.Split(',')[2]) + 1) + "0" + s.Split(',')[1] });
                                                            }
                                                            else
                                                            {
                                                                displayInfoData.Rows.Add(new object[] { s_3.Split(',')[0].Split('.')[0], s.Split(',')[0], (int.Parse(s.Split(',')[2]) + 1) + s.Split(',')[1] });
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        List<string> Geosets = new List<string>();
                        foreach (string s in DisplayInfo)
                        {
                            if (s.Split(',')[0] == cbValue)
                            {
                                int codeGeo = int.Parse(s.Split(',')[2]);
                                string hexa = codeGeo.ToString("X");
                                char[] values = hexa.ToCharArray();
                                Array.Reverse(values);
                                foreach(char c in values)
                                {
                                    Geosets.Add(c.ToString());
                                }
                                bool working = true;
                                if (working)
                                {
                                    foreach (string s_1 in ModelData)
                                    {
                                        if (s_1.Split(',')[0] == s.Split(',')[1])
                                        {
                                            foreach (string s_2 in ModelRoot)
                                            {
                                                if (s_2.Split(',')[1] == s_1.Split(',')[1])
                                                {
                                                    for (int j = 0; j < Geosets.Count(); j++)
                                                    {
                                                        switch (j)
                                                        {
                                                            case 0:
                                                                if(Convert.ToInt32(Geosets[j], 16) < 10 && Convert.ToInt32(Geosets[j], 16) > 0)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "10" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                else if(Convert.ToInt32(Geosets[j], 16) >= 10)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "1" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                break;
                                                            case 1:
                                                                if (Convert.ToInt32(Geosets[j], 16) < 10 && Convert.ToInt32(Geosets[j], 16) > 0)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "20" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                else if (Convert.ToInt32(Geosets[j], 16) >= 10)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "2" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                break;
                                                            case 2:
                                                                if (Convert.ToInt32(Geosets[j], 16) < 10 && Convert.ToInt32(Geosets[j], 16) > 0)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "30" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                else if (Convert.ToInt32(Geosets[j], 16) >= 10)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "3" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                break;
                                                            case 3:
                                                                if (Convert.ToInt32(Geosets[j], 16) < 10 && Convert.ToInt32(Geosets[j], 16) > 0)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "40" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                else if (Convert.ToInt32(Geosets[j], 16) >= 10)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "4" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                break;
                                                            case 4:
                                                                if (Convert.ToInt32(Geosets[j], 16) < 10 && Convert.ToInt32(Geosets[j], 16) > 0)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "50" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                else if (Convert.ToInt32(Geosets[j], 16) >= 10)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "5" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                break;
                                                            case 5:
                                                                if (Convert.ToInt32(Geosets[j], 16) < 10 && Convert.ToInt32(Geosets[j], 16) > 0)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "60" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                else if (Convert.ToInt32(Geosets[j], 16) >= 10)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "6" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                break;
                                                            case 6:
                                                                if (Convert.ToInt32(Geosets[j], 16) < 10 && Convert.ToInt32(Geosets[j], 16) > 0)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "70" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                else if (Convert.ToInt32(Geosets[j], 16) >= 10)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "7" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                break;
                                                            case 7:
                                                                if (Convert.ToInt32(Geosets[j], 16) < 10 && Convert.ToInt32(Geosets[j], 16) > 0)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "80" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                else if (Convert.ToInt32(Geosets[j], 16) >= 10)
                                                                {
                                                                    displayInfoData.Rows.Add(new object[] { s_2.Split(',')[0].Split('.')[0], s.Split(',')[0], "8" + Convert.ToInt32(Geosets[j], 16) });
                                                                }
                                                                break;
                                                        }                                                        
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                if (displayInfoData.Rows.Count > 0)
                {
                    gridSkin.DataSource = displayInfoData;
                    addColorText(ResultBox, Color.Orange, "Results found !");
                }
                else
                {
                    addColorText(ResultBox, Color.Red, "This displayid has no geosets enabled");
                }
            }
            else
            {
                addColorText(ResultBox, Color.Red, "No DisplayID Saved to process");
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ResultBox.Clear();
            LstSearch.SelectedIndex = -1;
            LstSearch.Items.Clear();
            gridSkin.DataSource = null;
        }
    }
}
