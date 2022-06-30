using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management.Automation;

namespace YTLearningMySQL
{
    public partial class Information : Form
    {
        public Information()
        {
            InitializeComponent();
        }

        public string DataBaseName()
        {
            if (metroSetComboBoxActions.Text == "SHOW DATABASES")
            {
                return "C:\\xampp\\mysql\\bin\\mysql.exe ".ToString() + " -u root -e" + '"' + "USE mysql; SHOW DATABASES;" + '"'.ToString();
            }
            return "".ToString();
        }

        public string ShowTables(string database_name)
        {
            if (metroSetComboBoxActions.Text == "SHOW TABLES")
            {                
                return "C:\\xampp\\mysql\\bin\\mysql.exe ".ToString() + " -u root -e" + '"' + "USE mysql; USE " + database_name.ToString() + "; SHOW TABLES;"  + '"'.ToString();
            }
            return "".ToString();
        }

        public string ShowColumns(string database_name, string table_name)
        {
            if (metroSetComboBoxActions.Text == "SHOW COLUMNS FROM TABLE")
            {                
                return "C:\\xampp\\mysql\\bin\\mysql.exe ".ToString() + " -u root -e" + '"' + "USE mysql; USE " + database_name.ToString() + "; SHOW COLUMNS FROM " + table_name.ToString() + ";" + '"'.ToString();
            }
            return "".ToString();
        }

        private async void Information_Load(object sender, EventArgs e)
        {
            await Task.Delay(1);
            List<string> shell = new List<string>();
            shell.Add("SHOW DATABASES");
            shell.Add("SHOW TABLES");
            shell.Add("SHOW COLUMNS FROM TABLE");

            foreach (string item in shell)
            {
                metroSetComboBoxActions.Items.Add(item.ToString());
                await Task.Delay(1);
            }
            metroSetTextBoxMySQL.Enabled = false;
            metroSetTextBoxDatabaseName.Enabled = false;
            metroSetTextBoxTableName.Enabled = false;


        }

        private async void metroSetButtonCommand_Click(object sender, EventArgs e)
        {
            await Task.Delay(1);

            if (metroSetComboBoxActions.Text == "SHOW DATABASES")
            {
                metroSetRichTextBoxMySQLShellOutput.Text = "";
                string databaseName = DataBaseName().ToString();
                using (PowerShell powerShell = PowerShell.Create())
                {
                    powerShell.AddScript(@"" + databaseName.ToString() + "");
                    var results = powerShell.Invoke();
                    foreach (var result in results)
                    {                        
                        //metroSetRichTextBoxMySQLShellOutput.Text = result.ToString();
                        metroSetRichTextBoxMySQLShellOutput.AppendText(result.ToString() + "\n".ToString());
                    }
                    await Task.Delay(4);
                }
            }

            if ((metroSetComboBoxActions.Text == "SHOW TABLES") && (metroSetTextBoxDatabaseName.Text != "".ToString()) && (metroSetTextBoxDatabaseName.Enabled != false))
            {
                metroSetRichTextBoxMySQLShellOutput.Text = "";
                string showTables = ShowTables(metroSetTextBoxDatabaseName.Text.ToString()).ToString();
                using (PowerShell powerShell = PowerShell.Create())
                {
                    powerShell.AddScript(@"" + showTables.ToString() + "");
                    var results = powerShell.Invoke();
                    foreach (var result in results)
                    {
                        //metroSetRichTextBoxMySQLShellOutput.Text = result.ToString();
                        metroSetRichTextBoxMySQLShellOutput.AppendText(result.ToString() + "\n".ToString());
                    }
                    await Task.Delay(4);
                }
            }

            if ((metroSetComboBoxActions.Text == "SHOW COLUMNS FROM TABLE") && (metroSetTextBoxDatabaseName.Text != "".ToString()) && (metroSetTextBoxDatabaseName.Enabled != false)
                 && (metroSetTextBoxTableName.Text != "".ToString()) && (metroSetTextBoxTableName.Enabled != false))
            {
                metroSetRichTextBoxMySQLShellOutput.Text = "";
                string showColumns = ShowColumns(metroSetTextBoxDatabaseName.Text.ToString(), metroSetTextBoxTableName.Text.ToString());
                using (PowerShell powerShell = PowerShell.Create())
                {
                    powerShell.AddScript(@"" + showColumns.ToString() + "");
                    var results = powerShell.Invoke();
                    metroSetRichTextBoxMySQLShellOutput.Text = results.ToString();
                    foreach (var result in results)
                    {
                        //metroSetRichTextBoxMySQLShellOutput.Text = result.ToString();
                        metroSetRichTextBoxMySQLShellOutput.AppendText(result.ToString() + "\n".ToString());

                    }
                    await Task.Delay(4);
                }
            }
        }

        public async void DisableTextBox()
        {
            if (metroSetComboBoxActions.Text == "SHOW DATABASES")
            {
                await Task.Delay(1);
                metroSetTextBoxTableName.Enabled = false;
                metroSetTextBoxDatabaseName.Enabled = false;
                
            }
            if (metroSetComboBoxActions.Text == "SHOW TABLES")
            {
                await Task.Delay(1);
                metroSetTextBoxDatabaseName.Enabled = true;
                metroSetTextBoxTableName.Enabled = false;
            }
            if (metroSetComboBoxActions.Text == "SHOW COLUMNS FROM TABLE")
            {
                await Task.Delay(1);
                metroSetTextBoxDatabaseName.Enabled = true;
                metroSetTextBoxTableName.Enabled = true;
            }
        }



        private void metroSetComboBoxActions_TextChanged(object sender, EventArgs e)
        {
            DisableTextBox();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
