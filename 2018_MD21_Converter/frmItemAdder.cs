using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roccus_MultiTool
{
    public partial class frmItemAdder : Form
    {

        private DBInstance con;
        public string server { get; set; }
        public string database { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public frmItemAdder()
        {
            InitializeComponent();

            foreach (var item in Enum.GetNames(typeof(EnumDefinition.EnumType.Tables)))
            {
                tableList.Items.Add(item);
            }
        }

        private void frmItemAdder_Load(object sender, EventArgs e)
        {
            con = new DBInstance(server, database, username, password);
            this.Activate();
            this.Select();
        }

        private void DBTableBtn_Click(object sender, EventArgs e)
        {
            frmBackupSQL form = new frmBackupSQL();
            if (tableList.Text == "" || tableList.Text == string.Empty)
            {
                MessageBox.Show("Choose a table to use the backup function", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                form.TableName = tableList.Text;
                form.server = server;
                form.database = database;
                form.username = username;
                form.password = password;
                form.BringToFront();
                form.Show();
            }

        }

        private void armorHotfixBtn_Click(object sender, EventArgs e)
        {
            frmArmorSmith frm = new frmArmorSmith();
            frm.server = server;
            frm.database = database;
            frm.username = username;
            frm.password = password;
            frm.BringToFront();
            frm.ShowDialog();
        }

        private void weaponHotfixBtn_Click(object sender, EventArgs e)
        {
            frmWeaponSmith frm = new frmWeaponSmith();
            frm.server = server;
            frm.database = database;
            frm.username = username;
            frm.password = password;
            frm.BringToFront();
            frm.ShowDialog();
        }
    }
}
