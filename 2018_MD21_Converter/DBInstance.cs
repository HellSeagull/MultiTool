using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roccus_MultiTool
{
    class DBInstance
    {

        private string connection;
        private string database;
        private string username;
        private string password;
        private string connectionString;
        private MySqlConnection con;

        public DBInstance(string connection, string database, string username, string password)
        {
            this.connection = connection;
            this.database = database;
            this.username = username;
            this.password = password;
            connectionString = "SERVER=" + connection + "; DATABASE=" + database + "; UID=" + username + "; PASSWORD=" + password + "; SslMode=None";

            con = new MySqlConnection(connectionString);
        }

        public string ConnectionString()
        {
            return connectionString;
        }

        public MySqlConnection TheConnection { get { return con; } }

        public void Connection()
        {
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                con.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool Connected()
        {
            if (con.State == System.Data.ConnectionState.Open)
                return true;
            else
                return false;
        }

    }
}
