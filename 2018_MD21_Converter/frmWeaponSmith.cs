using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roccus_MultiTool
{
    public partial class frmWeaponSmith : Form
    {

        private DBInstance con;
        public string server { get; set; }
        public string database { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        private Bitmap bmp;
        Graphics g;
        StringBuilder sb = new StringBuilder();
        private string tableColumns;

        public frmWeaponSmith()
        {
            InitializeComponent();

            foreach (var item in Enum.GetNames(typeof(EnumDefinition.EnumType.InventoryTypeWeapon)))
            {
                inventoryTypeBox.Items.Add(item);
            }

            foreach (var item in Enum.GetNames(typeof(EnumDefinition.EnumType.MaterialWeapon)))
            {
                materialBox.Items.Add(item);
            }

            foreach (var item in Enum.GetNames(typeof(EnumDefinition.EnumType.Quality)))
            {
                qualityBox.Items.Add(item);
            }

            foreach (var item in Enum.GetNames(typeof(EnumDefinition.EnumType.Sheath)))
            {
                objectSlotBox.Items.Add(item);
            }

        }

        private void frmWeaponSmith_Load(object sender, EventArgs e)
        {
            con = new DBInstance(server, database, username, password);
            previewBox.Visible = false;
            logoBox.Visible = false;
            groupBox1.Visible = false;

            bmp = new Bitmap(previewBox.Size.Width, previewBox.Size.Height);
            previewBox.Image = bmp;
            g = Graphics.FromImage(bmp);

            iconFileDataIDTxt.Text = ConfigurationManager.AppSettings["iconfiledata"];

        }

        private void previewBtn_Click(object sender, EventArgs e)
        {
            if (itemNameTxt.Text != "" && itemDescTxt.Text != "" && qualityBox.Text != "" && materialBox.Text != "" && inventoryTypeBox.Text != "")
            {
                g.Clear(ColorTranslator.FromHtml("#1a1a1a"));

                switch ((int)(EnumDefinition.EnumType.Quality)Enum.Parse(typeof(EnumDefinition.EnumType.Quality), qualityBox.Text))
                {
                    case 0:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.DarkGray), (float)75.0, (float)5.0);
                        break;
                    case 1:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.White), (float)75.0, (float)5.0);
                        break;
                    case 2:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Green), (float)75.0, (float)5.0);
                        break;
                    case 3:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.AliceBlue), (float)75.0, (float)5.0);
                        break;
                    case 4:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.DarkViolet), (float)75.0, (float)5.0);
                        break;
                    case 5:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Orange), (float)75.0, (float)5.0);
                        break;
                    case 6:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.PaleGoldenrod), (float)75.0, (float)5.0);
                        break;
                    case 7:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Beige), (float)75.0, (float)5.0);
                        break;
                    case 8:
                        g.DrawString(itemNameTxt.Text, new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Aqua), (float)75.0, (float)5.0);
                        break;
                    default:
                        g.DrawString("", new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.DarkGray), (float)75.0, (float)5.0);
                        break;
                }

                g.DrawString(itemDescTxt.Text, new Font("Arial", 8, FontStyle.Bold), new SolidBrush(Color.LightGoldenrodYellow), (float)5.0, (float)100.0);

                g.DrawString(materialBox.Text, new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.White), (float)75.0, (float)25.0);

                g.DrawString(inventoryTypeBox.Text, new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.White), (float)(previewBox.Right - 75.0), (float)50.0);

                previewBox.Invalidate();
                logoBox.Invalidate();
            }
            else
            {
                MessageBox.Show("You must fill all the cells marked by *", "Not complete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            groupBox1.Visible = true;
            previewBox.Visible = true;
            logoBox.Visible = true;
        }

        private string getTableColumns(string tableName)
        {
            if (con.Connected())
            {
                sb = new StringBuilder();

                MySqlCommand cmd = new MySqlCommand(@"SELECT column_name as 'Column Name'
                                                      FROM information_schema.columns
                                                      WHERE table_name = '" + tableName + "'", con.TheConnection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sb.Append(reader["Column Name"].ToString());
                    sb.Append(',');
                }
                reader.Close();
                tableColumns = sb.ToString().Substring(0, sb.ToString().LastIndexOf(','));

                return tableColumns;
            }
            else
            {
                MessageBox.Show("No Active Connection");
                con.Disconnect();
                return null;
            }
        }

        private void hotfixBtn_Click(object sender, EventArgs e)
        {
            if (!con.Connected())
                con.Connection();

            if (con.Connected())
            {
                try
                {
                    if (!LegionBox.Checked)
                    {
                        MySqlCommand cmd_1 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.item.ToString())
                                                                + ") VALUES (@param1,2,4,@param3,@param4,@param5,1,@param2,11,0)", con.TheConnection);
                        cmd_1.Parameters.AddWithValue("@param1", itemIDTxt.Text);
                        cmd_1.Parameters.AddWithValue("@param2", iconFileDataIDTxt.Text);
                        cmd_1.Parameters.AddWithValue("@param3", (int)(EnumDefinition.EnumType.MaterialWeapon)Enum.Parse(typeof(EnumDefinition.EnumType.MaterialWeapon), materialBox.Text));
                        cmd_1.Parameters.AddWithValue("@param4", (int)(EnumDefinition.EnumType.InventoryTypeWeapon)Enum.Parse(typeof(EnumDefinition.EnumType.InventoryTypeWeapon), inventoryTypeBox.Text));
                        cmd_1.Parameters.AddWithValue("@param5", (int)(EnumDefinition.EnumType.Sheath)Enum.Parse(typeof(EnumDefinition.EnumType.Sheath), objectSlotBox.Text));

                        MySqlCommand cmd_2 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item_appearance.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.item_appearance.ToString())
                                                                + ") VALUES (@param1,@param4,@param2,@param3,3472000,0)", con.TheConnection);
                        cmd_2.Parameters.AddWithValue("@param1", itemID2Txt.Text);
                        cmd_2.Parameters.AddWithValue("@param2", displayIDTxt.Text);
                        cmd_2.Parameters.AddWithValue("@param3", iconFileDataIDTxt.Text);
                        cmd_2.Parameters.AddWithValue("@param4", 3);

                        MySqlCommand cmd_3 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item_modified_appearance.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.item_modified_appearance.ToString())
                                                                + ") VALUES (@param1,@param2,0,@param3,0,4,0)", con.TheConnection);
                        cmd_3.Parameters.AddWithValue("@param1", itemID3Txt.Text);
                        cmd_3.Parameters.AddWithValue("@param2", itemIDTxt.Text);
                        cmd_3.Parameters.AddWithValue("@param3", itemID2Txt.Text);

                        MySqlCommand cmd_4 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item_sparse.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.item_sparse.ToString()) + ") "
                                                                + @" VALUES (@param1,-1,@param3,@param2,@param2,@param2,@param2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,150,-1,7,0,0,0,0,0,0,@param7,@param6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,110,@param5,@param4,0)",
                                                                con.TheConnection);
                        cmd_4.Parameters.AddWithValue("@param1", itemIDTxt.Text);
                        cmd_4.Parameters.AddWithValue("@param2", itemNameTxt.Text);
                        cmd_4.Parameters.AddWithValue("@param3", itemDescTxt.Text);
                        cmd_4.Parameters.AddWithValue("@param4", (int)(EnumDefinition.EnumType.Quality)Enum.Parse(typeof(EnumDefinition.EnumType.Quality), qualityBox.Text));
                        cmd_4.Parameters.AddWithValue("@param5", (int)(EnumDefinition.EnumType.InventoryTypeWeapon)Enum.Parse(typeof(EnumDefinition.EnumType.InventoryTypeWeapon), inventoryTypeBox.Text));
                        cmd_4.Parameters.AddWithValue("@param6", (int)(EnumDefinition.EnumType.MaterialWeapon)Enum.Parse(typeof(EnumDefinition.EnumType.MaterialWeapon), materialBox.Text));
                        cmd_4.Parameters.AddWithValue("@param7", (int)(EnumDefinition.EnumType.Sheath)Enum.Parse(typeof(EnumDefinition.EnumType.Sheath), objectSlotBox.Text));

                        MySqlCommand cmd_5 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item_sparse_locale.ToString()
                        + @" VALUES (@param1,@param2,@param4,@param3,@param3,@param3,@param3,0)",
                        con.TheConnection);
                        cmd_5.Parameters.AddWithValue("@param1", itemIDTxt.Text);
                        cmd_5.Parameters.AddWithValue("@param2", CultureInfo.CurrentCulture.Name.Replace("-", ""));
                        cmd_5.Parameters.AddWithValue("@param3", itemNameTxt.Text);
                        cmd_5.Parameters.AddWithValue("@param4", itemDescTxt.Text);

                        MySqlCommand cmd_6 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.hotfix_data.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.hotfix_data.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,0)", con.TheConnection);
                        cmd_6.Parameters.AddWithValue("@param1", hotfixDataMaxID());
                        cmd_6.Parameters.AddWithValue("@param2", (UInt64)(EnumDefinition.EnumType.Tables)Enum.Parse(typeof(EnumDefinition.EnumType.Tables), EnumDefinition.EnumType.Tables.item.ToString()));
                        cmd_6.Parameters.AddWithValue("@param3", itemIDTxt.Text);

                        MySqlCommand cmd_7 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.hotfix_data.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.hotfix_data.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,0)", con.TheConnection);
                        cmd_7.Parameters.AddWithValue("@param1", hotfixDataMaxID() + 1);
                        cmd_7.Parameters.AddWithValue("@param2", (UInt64)(EnumDefinition.EnumType.Tables)Enum.Parse(typeof(EnumDefinition.EnumType.Tables), EnumDefinition.EnumType.Tables.item_appearance.ToString()));
                        cmd_7.Parameters.AddWithValue("@param3", itemID2Txt.Text);

                        MySqlCommand cmd_8 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.hotfix_data.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.hotfix_data.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,0)", con.TheConnection);
                        cmd_8.Parameters.AddWithValue("@param1", hotfixDataMaxID() + 2);
                        cmd_8.Parameters.AddWithValue("@param2", (UInt64)(EnumDefinition.EnumType.Tables)Enum.Parse(typeof(EnumDefinition.EnumType.Tables), EnumDefinition.EnumType.Tables.item_modified_appearance.ToString()));
                        cmd_8.Parameters.AddWithValue("@param3", itemID3Txt.Text);

                        MySqlCommand cmd_9 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.hotfix_data.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.hotfix_data.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,0)", con.TheConnection);
                        cmd_9.Parameters.AddWithValue("@param1", hotfixDataMaxID() + 3);
                        cmd_9.Parameters.AddWithValue("@param2", (UInt64)(EnumDefinition.EnumType.Tables)Enum.Parse(typeof(EnumDefinition.EnumType.Tables), EnumDefinition.EnumType.Tables.item_sparse.ToString()));
                        cmd_9.Parameters.AddWithValue("@param3", itemIDTxt.Text);

                        if (itemIDTxt.Text != "" && iconFileDataIDTxt.Text != "" && materialBox.Text != "" && inventoryTypeBox.Text != ""
                                && qualityBox.Text != "" && itemID2Txt.Text != "" && itemID3Txt.Text != "" && itemNameTxt.Text != ""
                                && itemDescTxt.Text != "" && displayIDTxt.Text != "")
                        {
                            cmd_1.ExecuteNonQuery();
                            cmd_2.ExecuteNonQuery();
                            cmd_3.ExecuteNonQuery();
                            cmd_4.ExecuteNonQuery();
                            cmd_5.ExecuteNonQuery();
                            cmd_6.ExecuteNonQuery();
                            cmd_7.ExecuteNonQuery();
                            cmd_8.ExecuteNonQuery();
                            cmd_9.ExecuteNonQuery();

                            MessageBox.Show("Your weapon item has been hotfixed", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            con.Disconnect();
                        }
                        else
                        {
                            MessageBox.Show("You haven't filled all the cells", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            con.Disconnect();
                        }
                    }
                    else
                    {
                        MySqlCommand cmd_1 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.item.ToString())
                                        + ") VALUES (@param1,@param2,2,4,1,@param3,@param4,@param5,11,0)", con.TheConnection);
                        cmd_1.Parameters.AddWithValue("@param1", itemIDTxt.Text);
                        cmd_1.Parameters.AddWithValue("@param2", iconFileDataIDTxt.Text);
                        cmd_1.Parameters.AddWithValue("@param3", (int)(EnumDefinition.EnumType.MaterialWeapon)Enum.Parse(typeof(EnumDefinition.EnumType.MaterialWeapon), materialBox.Text));
                        cmd_1.Parameters.AddWithValue("@param4", (int)(EnumDefinition.EnumType.InventoryTypeWeapon)Enum.Parse(typeof(EnumDefinition.EnumType.InventoryTypeWeapon), inventoryTypeBox.Text));
                        cmd_1.Parameters.AddWithValue("@param5", (int)(EnumDefinition.EnumType.Sheath)Enum.Parse(typeof(EnumDefinition.EnumType.Sheath), objectSlotBox.Text));

                        MySqlCommand cmd_2 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item_appearance.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.item_appearance.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,3472000,@param4,0)", con.TheConnection);
                        cmd_2.Parameters.AddWithValue("@param1", itemID2Txt.Text);
                        cmd_2.Parameters.AddWithValue("@param2", displayIDTxt.Text);
                        cmd_2.Parameters.AddWithValue("@param3", iconFileDataIDTxt.Text);
                        cmd_2.Parameters.AddWithValue("@param4", 3);

                        MySqlCommand cmd_3 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item_modified_appearance.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.item_modified_appearance.ToString())
                                                                + ") VALUES (@param1,@param3,0,0,4,@param2,0)", con.TheConnection);
                        cmd_3.Parameters.AddWithValue("@param1", itemIDTxt.Text);
                        cmd_3.Parameters.AddWithValue("@param2", itemID3Txt.Text);
                        cmd_3.Parameters.AddWithValue("@param3", itemID2Txt.Text);

                        MySqlCommand cmd_4 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item_sparse.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.item_sparse.ToString()) + ") "
                                                                + @" VALUES (@param1,-1,@param2,@param2,@param2,@param2,@param3,0,8192,0,0,0,0,0,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0,-1,930,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,@param4,@param5,110,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,@param6,@param7,0,0,0,0,0,0,6,0)",
                                                                con.TheConnection);
                        cmd_4.Parameters.AddWithValue("@param1", itemIDTxt.Text);
                        cmd_4.Parameters.AddWithValue("@param2", itemNameTxt.Text);
                        cmd_4.Parameters.AddWithValue("@param3", itemDescTxt.Text);
                        cmd_4.Parameters.AddWithValue("@param4", (int)(EnumDefinition.EnumType.Quality)Enum.Parse(typeof(EnumDefinition.EnumType.Quality), qualityBox.Text));
                        cmd_4.Parameters.AddWithValue("@param5", (int)(EnumDefinition.EnumType.InventoryTypeWeapon)Enum.Parse(typeof(EnumDefinition.EnumType.InventoryTypeWeapon), inventoryTypeBox.Text));
                        cmd_4.Parameters.AddWithValue("@param6", (int)(EnumDefinition.EnumType.MaterialWeapon)Enum.Parse(typeof(EnumDefinition.EnumType.MaterialWeapon), materialBox.Text));
                        cmd_4.Parameters.AddWithValue("@param7", (int)(EnumDefinition.EnumType.Sheath)Enum.Parse(typeof(EnumDefinition.EnumType.Sheath), objectSlotBox.Text));

                        MySqlCommand cmd_5 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.item_sparse_locale.ToString()
                        + @" VALUES (@param1,@param2,@param4,@param3,@param3,@param3,@param3,0)",
                        con.TheConnection);
                        cmd_5.Parameters.AddWithValue("@param1", itemIDTxt.Text);
                        cmd_5.Parameters.AddWithValue("@param2", CultureInfo.CurrentCulture.Name.Replace("-", ""));
                        cmd_5.Parameters.AddWithValue("@param3", itemNameTxt.Text);
                        cmd_5.Parameters.AddWithValue("@param4", itemDescTxt.Text);

                        MySqlCommand cmd_6 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.hotfix_data.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.hotfix_data.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,0)", con.TheConnection);
                        cmd_6.Parameters.AddWithValue("@param1", hotfixDataMaxID());
                        cmd_6.Parameters.AddWithValue("@param2", (UInt64)(EnumDefinition.EnumType.Tables)Enum.Parse(typeof(EnumDefinition.EnumType.Tables), EnumDefinition.EnumType.Tables.item.ToString()));
                        cmd_6.Parameters.AddWithValue("@param3", itemIDTxt.Text);

                        MySqlCommand cmd_7 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.hotfix_data.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.hotfix_data.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,0)", con.TheConnection);
                        cmd_7.Parameters.AddWithValue("@param1", hotfixDataMaxID() + 1);
                        cmd_7.Parameters.AddWithValue("@param2", (UInt64)(EnumDefinition.EnumType.Tables)Enum.Parse(typeof(EnumDefinition.EnumType.Tables), EnumDefinition.EnumType.Tables.item_appearance.ToString()));
                        cmd_7.Parameters.AddWithValue("@param3", itemID2Txt.Text);

                        MySqlCommand cmd_8 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.hotfix_data.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.hotfix_data.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,0)", con.TheConnection);
                        cmd_8.Parameters.AddWithValue("@param1", hotfixDataMaxID() + 2);
                        cmd_8.Parameters.AddWithValue("@param2", (UInt64)(EnumDefinition.EnumType.Tables)Enum.Parse(typeof(EnumDefinition.EnumType.Tables), EnumDefinition.EnumType.Tables.item_modified_appearance.ToString()));
                        cmd_8.Parameters.AddWithValue("@param3", itemID3Txt.Text);

                        MySqlCommand cmd_9 = new MySqlCommand(@"INSERT INTO " + EnumDefinition.EnumType.Tables.hotfix_data.ToString() + " (" + getTableColumns(EnumDefinition.EnumType.Tables.hotfix_data.ToString())
                                                                + ") VALUES (@param1,@param2,@param3,0)", con.TheConnection);
                        cmd_9.Parameters.AddWithValue("@param1", hotfixDataMaxID() + 3);
                        cmd_9.Parameters.AddWithValue("@param2", (UInt64)(EnumDefinition.EnumType.Tables)Enum.Parse(typeof(EnumDefinition.EnumType.Tables), EnumDefinition.EnumType.Tables.item_sparse.ToString()));
                        cmd_9.Parameters.AddWithValue("@param3", itemIDTxt.Text);

                        if (itemIDTxt.Text != "" && iconFileDataIDTxt.Text != "" && materialBox.Text != "" && inventoryTypeBox.Text != ""
                                && qualityBox.Text != "" && itemID2Txt.Text != "" && itemID3Txt.Text != "" && itemNameTxt.Text != ""
                                && itemDescTxt.Text != "" && displayIDTxt.Text != "")
                        {
                            cmd_1.ExecuteNonQuery();
                            cmd_2.ExecuteNonQuery();
                            cmd_3.ExecuteNonQuery();
                            cmd_4.ExecuteNonQuery();
                            cmd_5.ExecuteNonQuery();
                            cmd_6.ExecuteNonQuery();
                            cmd_7.ExecuteNonQuery();
                            cmd_8.ExecuteNonQuery();
                            cmd_9.ExecuteNonQuery();

                            MessageBox.Show("Your weapon item has been hotfixed", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            con.Disconnect();
                        }
                        else
                        {
                            MessageBox.Show("You haven't filled all the cells", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            con.Disconnect();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("No Active Connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void maxIDBtn_Click(object sender, EventArgs e)
        {
            if (!con.Connected())
                con.Connection();

            if (con.Connected())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(@"SELECT MAX(ID) + 1 FROM " + EnumDefinition.EnumType.Tables.item.ToString(), con.TheConnection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            itemIDTxt.Text = reader.GetUInt32(0).ToString();
                        }
                        reader.Close();
                        con.Disconnect();
                    }
                    else
                    {
                        MessageBox.Show("No result", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        reader.Close();
                        con.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No Active Connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void itemAppMaxIDBtn_Click(object sender, EventArgs e)
        {
            if (!con.Connected())
                con.Connection();

            if (con.Connected())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(@"SELECT MAX(ID) + 1 FROM " + EnumDefinition.EnumType.Tables.item_appearance.ToString(), con.TheConnection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            itemID2Txt.Text = reader.GetUInt32(0).ToString();
                        }
                        reader.Close();
                        con.Disconnect();
                    }
                    else
                    {
                        MessageBox.Show("No result", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        reader.Close();
                        con.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No Active Connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void itemModAppMaxIDBtn_Click(object sender, EventArgs e)
        {
            if (!con.Connected())
                con.Connection();

            if (con.Connected())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(@"SELECT MAX(ID) + 1 FROM " + EnumDefinition.EnumType.Tables.item_modified_appearance.ToString(), con.TheConnection);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            itemID3Txt.Text = reader.GetUInt32(0).ToString();
                        }
                        reader.Close();
                        con.Disconnect();
                    }
                    else
                    {
                        MessageBox.Show("No result", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No Active Connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private UInt32 hotfixDataMaxID()
        {
            UInt32 value = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(@"SELECT MAX(ID) + 1 FROM " + EnumDefinition.EnumType.Tables.hotfix_data.ToString(), con.TheConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        value = reader.GetUInt32(0);
                    }
                    reader.Close();
                    return value;
                }
                else
                {
                    MessageBox.Show("No result", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    reader.Close();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 1;
        }
    }
}
