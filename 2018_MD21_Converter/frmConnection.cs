using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roccus_MultiTool
{
    public partial class frmConnection : Form
    {

        private DBInstance con;
        public int mode { get; set; }

        public frmConnection()
        {
            InitializeComponent();
        }

        private void frmConnection_Load(object sender, EventArgs e)
        {
            serverTxt.Text = ConfigurationManager.AppSettings["Connection"];
            databaseTxt.Text = ConfigurationManager.AppSettings["Database"];
            usernameTxt.Text = ConfigurationManager.AppSettings["Username"];
            passwordTxt.PasswordChar = '*';
            passwordTxt.Text = ConfigurationManager.AppSettings["Password"];
            this.Activate();
            this.Select();
            if (mode == 1)
            {
                databaseTxt.Enabled = true;
                databaseTxt.Select();
                databaseTxt.Focus();
            }
        }

        private void ConnectionBtn_Click(object sender, EventArgs e)
        {
            con = new DBInstance(serverTxt.Text, databaseTxt.Text, usernameTxt.Text, passwordTxt.Text);
            if (con.Connected())
            {
                lblWaiting.Text = "Connecting to " + con.ConnectionString().Substring(con.ConnectionString().IndexOf('=') + 1).Substring(0, con.ConnectionString().IndexOf(';') - (con.ConnectionString().IndexOf('=')) - 1);
                con.Disconnect();
                Connection();
            }
            else
            {
                con.Connection();
                lblWaiting.Text = "Connecting to " + con.ConnectionString().Substring(con.ConnectionString().IndexOf('=') + 1).Substring(0, con.ConnectionString().IndexOf(';') - (con.ConnectionString().IndexOf('=')) - 1);
                con.Disconnect();
                Connection();
            }

        }

        private async void Connection()
        {
            await Task.Delay(2000);
            this.Hide();
            frmItemAdder frm = new frmItemAdder();
            frm.server = serverTxt.Text;
            frm.database = databaseTxt.Text;
            frm.username = usernameTxt.Text;
            frm.password = passwordTxt.Text;
            frm.ShowDialog();
            this.Close();
        }

        private void ConnectionBtn_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                Connection();
            }
        }

        private void databaseTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionBtn_Click(sender, e);
            }
        }
    }
}
