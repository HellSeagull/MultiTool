using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace _2018_MD21_Converter
{
    class rootGenCaschost
    {

        private MySqlConnection connection;
        private Start st;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string table;

        public rootGenCaschost(string server, string database, string uid, string password)
        {
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;

                    case 4060:
                        Console.WriteLine("Invalid database, please try again");
                        break;

                    case 18456:
                        Console.WriteLine("login failed, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void SelectM2Root(string table)
        {
            string query = "SELECT Path, FileDataId FROM " + database + "." + table + " WHERE Path like '%.m2'";

            //Create a list to store the result
            List<string> names = new List<string>();
            List<string> numbers = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    names.Add(dataReader["Path"] + "");
                    numbers.Add(dataReader["FileDataId"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

                string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string exeName = Path.GetFileName(binaryPath);
                binaryPath = binaryPath.Replace(exeName, "rootFiles\\M2_Root");

                List<string> listFileName = new List<string>();
                List<string> listFileIds = new List<string>();

                foreach (string s in names)
                {
                    var line = s;
                    var linelength = line.Length;
                    var lastSlashPosition = line.LastIndexOf('\\');
                    var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                    listFileName.Add(name);
                }

                foreach (string s in numbers)
                {
                    listFileIds.Add(s);
                }

                if (!File.Exists(binaryPath))
                    File.Create(binaryPath).Close();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Adding new rows to M2_Root...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                string[] lines = File.ReadAllLines(binaryPath);

                if (lines.Length == 0)
                {
                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        StreamWriter sw = File.AppendText(binaryPath);
                        if (listFileName[i].Contains("m2"))
                        {
                            sw.WriteLine(listFileName[i] + "," + listFileIds[i]);
                            Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                            sw.Close();
                        }
                        else
                        {
                            sw.Close();
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        if (!File.ReadAllText(binaryPath).Contains(listFileName[i]))
                        {
                            StreamWriter sw = File.AppendText(binaryPath);
                            if (listFileName[i].Contains("m2"))
                            {
                                Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                                sw.WriteLine(Environment.NewLine + listFileName[i] + "," + listFileIds[i]);
                                sw.Close();
                            }
                            else
                            {
                                sw.Close();
                            }
                        }
                        else
                        {
                            if (listFileName[i].Contains("m2"))
                            {
                                Console.WriteLine("- " + listFileName[i] + " already exists in M2_Root");
                            }
                        }
                    }
                }

                var tempFileName = Path.GetTempFileName();
                try
                {
                    using (var streamReader = new StreamReader(binaryPath))
                    using (var streamWriter = new StreamWriter(tempFileName))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                                streamWriter.WriteLine(line);
                        }
                    }
                    File.Copy(tempFileName, binaryPath, true);
                }
                finally
                {
                    File.Delete(tempFileName);
                }

            }

        }

        public void SelectANIMRoot(string table)
        {
            string query = "SELECT Path, FileDataId FROM " + database + "." + table + " WHERE Path like '%.anim'";

            //Create a list to store the result
            List<string> names = new List<string>();
            List<string> numbers = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    names.Add(dataReader["Path"] + "");
                    numbers.Add(dataReader["FileDataId"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

                string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string exeName = Path.GetFileName(binaryPath);
                binaryPath = binaryPath.Replace(exeName, "rootFiles\\ANIM_Root");

                List<string> listFileName = new List<string>();
                List<string> listFileIds = new List<string>();

                foreach (string s in names)
                {
                    var line = s;
                    var linelength = line.Length;
                    var lastSlashPosition = line.LastIndexOf('\\');
                    var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                    listFileName.Add(name);
                }

                foreach (string s in numbers)
                {
                    listFileIds.Add(s);
                }

                if (!File.Exists(binaryPath))
                    File.Create(binaryPath).Close();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Adding new rows to ANIM_Root...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                string[] lines = File.ReadAllLines(binaryPath);

                if (lines.Length == 0)
                {
                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        StreamWriter sw = File.AppendText(binaryPath);
                        if (listFileName[i].Contains(".anim"))
                        {
                            sw.WriteLine(listFileName[i] + "," + listFileIds[i]);
                            Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                            sw.Close();
                        }
                        else
                        {
                            sw.Close();
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        if (!File.ReadAllText(binaryPath).Contains(listFileName[i]))
                        {
                            StreamWriter sw = File.AppendText(binaryPath);
                            if (listFileName[i].Contains(".anim"))
                            {
                                Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                                sw.WriteLine(Environment.NewLine + listFileName[i] + "," + listFileIds[i]);
                                sw.Close();
                            }
                            else
                            {
                                sw.Close();
                            }
                        }
                        else
                        {
                            if (listFileName[i].Contains(".anim"))
                            {
                                Console.WriteLine("- " + listFileName[i] + " already exists in ANIM_Root");
                            }
                        }
                    }
                }

                var tempFileName = Path.GetTempFileName();
                try
                {
                    using (var streamReader = new StreamReader(binaryPath))
                    using (var streamWriter = new StreamWriter(tempFileName))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                                streamWriter.WriteLine(line);
                        }
                    }
                    File.Copy(tempFileName, binaryPath, true);
                }
                finally
                {
                    File.Delete(tempFileName);
                }

            }

        }

        public void SelectBONERoot(string table)
        {
            string query = "SELECT Path, FileDataId FROM " + database + "." + table + " WHERE Path like '%.bone'";

            //Create a list to store the result
            List<string> names = new List<string>();
            List<string> numbers = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    names.Add(dataReader["Path"] + "");
                    numbers.Add(dataReader["FileDataId"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

                string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string exeName = Path.GetFileName(binaryPath);
                binaryPath = binaryPath.Replace(exeName, "rootFiles\\BONE_Root");

                List<string> listFileName = new List<string>();
                List<string> listFileIds = new List<string>();

                foreach (string s in names)
                {
                    var line = s;
                    var linelength = line.Length;
                    var lastSlashPosition = line.LastIndexOf('\\');
                    var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                    listFileName.Add(name);
                }

                foreach (string s in numbers)
                {
                    listFileIds.Add(s);
                }

                if (!File.Exists(binaryPath))
                    File.Create(binaryPath).Close();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Adding new rows to BONE_Root...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                string[] lines = File.ReadAllLines(binaryPath);

                if (lines.Length == 0)
                {
                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        StreamWriter sw = File.AppendText(binaryPath);
                        if (listFileName[i].Contains(".bone"))
                        {
                            sw.WriteLine(listFileName[i] + "," + listFileIds[i]);
                            Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                            sw.Close();
                        }
                        else
                        {
                            sw.Close();
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        if (!File.ReadAllText(binaryPath).Contains(listFileName[i]))
                        {
                            StreamWriter sw = File.AppendText(binaryPath);
                            if (listFileName[i].Contains(".bone"))
                            {
                                Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                                sw.WriteLine(Environment.NewLine + listFileName[i] + "," + listFileIds[i]);
                                sw.Close();
                            }
                            else
                            {
                                sw.Close();
                            }
                        }
                        else
                        {
                            if (listFileName[i].Contains(".bone"))
                            {
                                Console.WriteLine("- " + listFileName[i] + " already exists in BONE_Root");
                            }
                        }
                    }
                }

                var tempFileName = Path.GetTempFileName();
                try
                {
                    using (var streamReader = new StreamReader(binaryPath))
                    using (var streamWriter = new StreamWriter(tempFileName))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                                streamWriter.WriteLine(line);
                        }
                    }
                    File.Copy(tempFileName, binaryPath, true);
                }
                finally
                {
                    File.Delete(tempFileName);
                }

            }

        }

        public void SelectSKINRoot(string table)
        {
            string query = "SELECT Path, FileDataId FROM " + database + "." + table + " WHERE Path like '%.skin'";

            //Create a list to store the result
            List<string> names = new List<string>();
            List<string> numbers = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    names.Add(dataReader["Path"] + "");
                    numbers.Add(dataReader["FileDataId"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

                string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string exeName = Path.GetFileName(binaryPath);
                binaryPath = binaryPath.Replace(exeName, "rootFiles\\SKIN_Root");

                List<string> listFileName = new List<string>();
                List<string> listFileIds = new List<string>();

                foreach (string s in names)
                {
                    var line = s;
                    var linelength = line.Length;
                    var lastSlashPosition = line.LastIndexOf('\\');
                    var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                    listFileName.Add(name);
                }

                foreach (string s in numbers)
                {
                    listFileIds.Add(s);
                }

                if (!File.Exists(binaryPath))
                    File.Create(binaryPath).Close();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Adding new rows to SKIN_Root...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                string[] lines = File.ReadAllLines(binaryPath);

                if (lines.Length == 0)
                {
                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        StreamWriter sw = File.AppendText(binaryPath);
                        if (listFileName[i].Contains(".skin"))
                        {
                            sw.WriteLine(listFileName[i] + "," + listFileIds[i]);
                            Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                            sw.Close();
                        }
                        else
                        {
                            sw.Close();
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        if (!File.ReadAllText(binaryPath).Contains(listFileName[i]))
                        {
                            StreamWriter sw = File.AppendText(binaryPath);
                            if (listFileName[i].Contains(".skin"))
                            {
                                Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                                sw.WriteLine(Environment.NewLine + listFileName[i] + "," + listFileIds[i]);
                                sw.Close();
                            }
                            else
                            {
                                sw.Close();
                            }
                        }
                        else
                        {
                            if (listFileName[i].Contains(".skin"))
                            {
                                Console.WriteLine("- " + listFileName[i] + " already exists in SKIN_Root");
                            }
                        }
                    }
                }

                var tempFileName = Path.GetTempFileName();
                try
                {
                    using (var streamReader = new StreamReader(binaryPath))
                    using (var streamWriter = new StreamWriter(tempFileName))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                                streamWriter.WriteLine(line);
                        }
                    }
                    File.Copy(tempFileName, binaryPath, true);
                }
                finally
                {
                    File.Delete(tempFileName);
                }

            }

        }

        public void SelectPHYSRoot(string table)
        {
            string query = "SELECT Path, FileDataId FROM " + database + "." + table + " WHERE Path like '%.phys'";

            //Create a list to store the result
            List<string> names = new List<string>();
            List<string> numbers = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    names.Add(dataReader["Path"] + "");
                    numbers.Add(dataReader["FileDataId"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

                string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string exeName = Path.GetFileName(binaryPath);
                binaryPath = binaryPath.Replace(exeName, "rootFiles\\PHYS_Root");

                List<string> listFileName = new List<string>();
                List<string> listFileIds = new List<string>();

                foreach (string s in names)
                {
                    var line = s;
                    var linelength = line.Length;
                    var lastSlashPosition = line.LastIndexOf('\\');
                    var name = line.Substring(lastSlashPosition + 1, linelength - lastSlashPosition - 1);
                    listFileName.Add(name);
                }

                foreach (string s in numbers)
                {
                    listFileIds.Add(s);
                }

                if (!File.Exists(binaryPath))
                    File.Create(binaryPath).Close();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("Adding new rows to PHYS_Root...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                string[] lines = File.ReadAllLines(binaryPath);

                if (lines.Length == 0)
                {
                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        StreamWriter sw = File.AppendText(binaryPath);
                        if (listFileName[i].Contains(".phys"))
                        {
                            sw.WriteLine(listFileName[i] + "," + listFileIds[i]);
                            Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                            sw.Close();
                        }
                        else
                        {
                            sw.Close();
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < listFileName.Count; i++)
                    {
                        if (!File.ReadAllText(binaryPath).Contains(listFileName[i]))
                        {
                            StreamWriter sw = File.AppendText(binaryPath);
                            if (listFileName[i].Contains(".phys"))
                            {
                                Console.WriteLine("- " + listFileName[i] + "," + listFileIds[i] + " added");
                                sw.WriteLine(Environment.NewLine + listFileName[i] + "," + listFileIds[i]);
                                sw.Close();
                            }
                            else
                            {
                                sw.Close();
                            }
                        }
                        else
                        {
                            if (listFileName[i].Contains(".phys"))
                            {
                                Console.WriteLine("- " + listFileName[i] + " already exists in PHYS_Root");
                            }
                        }
                    }
                }

                var tempFileName = Path.GetTempFileName();
                try
                {
                    using (var streamReader = new StreamReader(binaryPath))
                    using (var streamWriter = new StreamWriter(tempFileName))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                                streamWriter.WriteLine(line);
                        }
                    }
                    File.Copy(tempFileName, binaryPath, true);
                }
                finally
                {
                    File.Delete(tempFileName);
                }

            }

        }

    }
}
