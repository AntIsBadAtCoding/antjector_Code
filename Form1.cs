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
using System.Threading.Tasks;
using System.Windows.Forms;
using AeigisAPI;

namespace AntJector
{
    public partial class Form1 : rounded
    {
        public Form1()
        {
            InitializeComponent();
            InfiniteLoop();
            functions.PopulateListBox(listBox1);
        }

        private void Exe_Click(object sender, EventArgs e)
        {
            AeigisAPI.Module.ExecuteScript(richTextBox1.Text);
        }


        private void Inj_Click(object sender, EventArgs e)
        {
            AeigisAPI.Module.AttachAPI();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        class functions
        {
            public static void PopulateListBox(System.Windows.Forms.ListBox lsb)
            {
                lsb.Items.Clear();
                DirectoryInfo curentdir = new DirectoryInfo(Directory.GetCurrentDirectory());
                DirectoryInfo scriptsFolder = curentdir.GetDirectories("Scripts").FirstOrDefault();
                if (scriptsFolder != null)
                {
                    foreach (FileInfo file in scriptsFolder.GetFiles())
                    {
                        lsb.Items.Add(file.Name);
                    }
                }
                else
                {
                    curentdir.CreateSubdirectory("Scripts");
                }
            }
        }

        private void Ref_Click(object sender, EventArgs e)
        {
            functions.PopulateListBox(listBox1);
        }

        private void Openfolder_Click(object sender, EventArgs e)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts");

            if (Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath);
            }
            else
            {
                MessageBox.Show("Folder not found!");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AeigisAPI.Module.ExecuteScript($"./Scripts/{listBox1.SelectedItem}");

            string path = Path.GetFullPath($"./Scripts/{listBox1.SelectedItem}");
            string contents = File.ReadAllText(path);

            richTextBox1.Text = contents;
        }

        async void InfiniteLoop()
        {
            while (true)
            {
                await Task.Delay(500);
                if (Properties.Settings.Default.AutoInject == true)
                {
                    if (AeigisAPI.Module.GetClientsList().Count < 0)
                    {
                        AeigisAPI.Module.AttachAPI();
                    }
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form settings = new Settings();
            settings.ShowDialog();
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt|Lua files (*.lua*)|*.lua*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string selectedfile = ofd.FileName;
                string filecontents = File.ReadAllText(selectedfile);

                richTextBox1.Text = filecontents;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "txt files (*.txt)|*.txt|Lua files (*.lua*)|*.lua*";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                string filename = sf.FileName;

                File.WriteAllText(filename, richTextBox1.Text);
            }
        }
    }
}
