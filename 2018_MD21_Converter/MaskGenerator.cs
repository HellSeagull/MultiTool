using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roccus_MultiTool
{
    public partial class MaskGenerator : Form
    {
        public MaskGenerator()
        {
            InitializeComponent();
        }

        private int t = 35;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Activate();
            this.Select();
            this.Focus();
            List<String> BoxNames = NameBoxes();
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    c.Text = BoxNames.ElementAt(t);
                    t--;
                }
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
            t.AppendText(text + "\n");
            t.SelectionColor = t.ForeColor;
        }

        private List<String> NameBoxes()
        {
            List<String> BoxNames = new List<string>();

            BoxNames.Add("Human or War");
            BoxNames.Add("Orc or Pala");
            BoxNames.Add("Dwarf or Hunt");
            BoxNames.Add("NightElf or Rogue");
            BoxNames.Add("Scourge or Priest");
            BoxNames.Add("Tauren or DK");
            BoxNames.Add("Gnome or Shaman");
            BoxNames.Add("Troll or Mage");
            BoxNames.Add("Goblin or Warlock");
            BoxNames.Add("BloodElf or Monk");
            BoxNames.Add("Draenei or Druid");
            BoxNames.Add("FelOrc or DH");
            BoxNames.Add("Naga");
            BoxNames.Add("Broken");
            BoxNames.Add("Skeleton");
            BoxNames.Add("Vrykul");
            BoxNames.Add("Tuskarr");
            BoxNames.Add("ForestTroll");
            BoxNames.Add("Taunka");
            BoxNames.Add("NorthSkeleton");
            BoxNames.Add("IceTroll");
            BoxNames.Add("Worgen");
            BoxNames.Add("HumanWorgen");
            BoxNames.Add("PandarenNeutral");
            BoxNames.Add("PandarenAlliance");
            BoxNames.Add("PandarenHorde");
            BoxNames.Add("Nightborne");
            BoxNames.Add("HM Tauren");
            BoxNames.Add("VoidElf");
            BoxNames.Add("LF Draenei");
            BoxNames.Add("Zandalari");
            BoxNames.Add("Kultiran");
            BoxNames.Add("Thin Human");
            BoxNames.Add("DarkIronDwarf");
            BoxNames.Add("Vulpera");
            BoxNames.Add("MagharOrc");

            return BoxNames;
        }

        private double getBoxMask(double value)
        {
            double MaskValue = 0;
            MaskValue = Math.Pow(2, value);
            return MaskValue;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
            }

            richTextBox1.Clear();

            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = true;
                }
            }
            double maskValue = 0;
            for (int i = 0; i < 36; i++)
            {
                maskValue += Math.Pow(2, i);
            }
            addColorText(richTextBox1, Color.Black, "Mask Value : " + maskValue);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            double maskValue = 0;

            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    if (((CheckBox)c).Checked)
                    {
                        maskValue += getBoxMask(double.Parse(Regex.Match(c.Name, @"\d+").Value) - 1);
                    }
                }
            }

            addColorText(richTextBox1, Color.Black, "Mask Value : " + maskValue);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
            }

            richTextBox1.Clear();

            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    if (((CheckBox)c).Text.Contains(" or "))
                    {
                        ((CheckBox)c).Checked = true;
                    }
                    else
                    {
                        ((CheckBox)c).Checked = false;
                    }
                }
            }

            double maskValue = 0;
            for (int i = 0; i < 12; i++)
            {
                maskValue += Math.Pow(2, i);
            }
            addColorText(richTextBox1, Color.Black, "Mask Value : " + maskValue);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
            }

            richTextBox1.Clear();
        }

    }
}
