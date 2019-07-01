using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roccus_MultiTool
{
    public partial class frmBackupSQL : Form
    {

        private DBInstance con;
        public string server { get; set; }
        public string database { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        private string pathCSV = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private int nbRows = 0;
        private int counter = 0;
        private string pathFile = "";
        private string queryResult = "";
        private string csvQuery = "";
        private string tableColumns = "";
        private string[] tableColumnsArray;
        private DataTable dt;
        private StringBuilder sb;
        public string TableName { get; set; }
        private Stopwatch timer;

        public frmBackupSQL()
        {
            InitializeComponent();
        }

        private void frmBackupSQL_Load(object sender, EventArgs e)
        {
            con = new DBInstance(server, database, username, password);
            progressBar1.Enabled = false;
            progressBar1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

            this.Text = TableName;

            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }

        #region BGW 1
        protected void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            InvokeUpdateControls();
            SQL_Script_Generation();
        }

        protected void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Stop();
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            progressBar1.Enabled = false;
            progressBar1.Visible = false;
            dt.Clear();
            dt.Dispose();
            con.Disconnect();
            counter = 0;
            nbRows = 0;
            timer.Reset();
            this.Close();
        }

        protected void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label3.Text = nbRows.ToString() + " Rows";
            label1.Text = counter.ToString();
        }
        #endregion

        #region Delegate update controls
        public delegate void UpdateControlsDelegate();

        public void InvokeUpdateControls()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateControlsDelegate(UpdateControls));
            }
            else
            {
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            pathFile = Path.Combine(pathCSV, string.Concat(TableName + "_" + DateTime.Now.ToString("yyyy_MM_dd"), ".sql"));
            progressBar1.Enabled = true;
            progressBar1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
        }
        #endregion

        private void SQL_Script_Generation()
        {
            if (!con.Connected())
                con.Connection();

            if (con.Connected())
            {
                timer = new Stopwatch();
                timer.Start();
                sb = new StringBuilder();

                if (File.Exists(pathFile))
                    File.Delete(pathFile);

                File.AppendAllText(pathFile, "##Script " + TableName + " du " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss\n\n"));

                try
                {

                    MySqlCommand cmd = new MySqlCommand(@"SELECT column_name as 'Column Name'
                                                      FROM information_schema.columns
                                                      WHERE table_name = '" + TableName + "'", con.TheConnection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        sb.Append(reader["Column Name"].ToString());
                        sb.Append(',');
                    }

                    tableColumns = sb.ToString().Substring(0, sb.ToString().LastIndexOf(','));
                    tableColumnsArray = tableColumns.Split(',');

                    sb.Clear();
                    reader.Close();

                    cmd = new MySqlCommand("SELECT " + tableColumns + " FROM " + TableName, con.TheConnection);
                    reader = cmd.ExecuteReader();

                    dt = new DataTable();
                    dt.Load(reader);
                    nbRows = dt.Rows.Count;

                    reader.Close();

                    cmd = new MySqlCommand("SELECT " + tableColumns + " FROM " + TableName, con.TheConnection);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i < tableColumnsArray.Length; i++)
                        {
                            sb.Append(reader[tableColumnsArray[i]] + ",");
                        }
                        queryResult = sb.ToString().Substring(0, sb.ToString().LastIndexOf(','));
                        csvQuery = "INSERT INTO `" + TableName + "` (" + tableColumns + ") VALUES (" + queryResult + ");\n";
                        File.AppendAllText(pathFile, csvQuery);
                        sb.Clear();
                        counter++;
                        if (timer.ElapsedMilliseconds % 2 == 0)
                            backgroundWorker1.ReportProgress(counter * 100 / nbRows);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("No Active Connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void frmBackupSQL_FormClosed(object sender, FormClosedEventArgs e)
        {
            backgroundWorker1.CancelAsync();
            backgroundWorker1.Dispose();
            con.Disconnect();
        }
    }
}
