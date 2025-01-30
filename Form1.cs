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
using System.Media;

namespace AntJector
{
    public partial class Form1 : rounded
    {
        Setting set = new Setting();
        public Form1()
        {
            InitializeComponent();
            InfiniteLoop();
            functions.PopulateListBox(listBox1);
            loadtheme(Properties.Settings.Default.Theme);
            AeigisAPI.Module.SetConfig("AntJector", "2.0");
        }

        private void Exe_Click(object sender, EventArgs e)
        {
            AeigisAPI.Module.ExecuteScript(richTextBox1.Text);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds");
            string sound = Path.Combine(path, "Execute");
            SoundPlayer soundPlayer = new SoundPlayer(sound);
            soundPlayer.Play();
        }


        private void Inj_Click(object sender, EventArgs e)
        {
            AeigisAPI.Module.AttachAPI();

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds");
            string sound = Path.Combine(path, "Inject");
            SoundPlayer soundPlayer = new SoundPlayer(sound);
            soundPlayer.Play();
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
            Properties.Settings.Default.AutoexeDone = true;
            while (true)
            {
                await Task.Delay(500);
                if (Properties.Settings.Default.AutoInject == true)
                {
                    AeigisAPI.Module.AttachAPI();
                    
                    if (Properties.Settings.Default.AutoexeDone == true)
                    {
                        if (Properties.Settings.Default.Autoexe == true)
                        {
                            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
                            DirectoryInfo path = dir.GetDirectories("autoexec").FirstOrDefault();
                            
                            foreach (FileInfo file in path.GetFiles())
                            {
                                AeigisAPI.Module.ExecuteScript($"{file.Name}");
                            }
                        }
                        Properties.Settings.Default.AutoexeDone = true;
                    }
                }
                loadtheme(Properties.Settings.Default.Theme);
            }
        }

        void loadtheme(int id)
        {
            if (id == 0) //dark theme
            {
                this.BackColor = Color.FromArgb(10, 10, 10);
                this.richTextBox1.BackColor = Color.FromArgb(30, 30, 30);
                this.listBox1.BackColor = Color.FromArgb(30, 30, 30);
                this.label1.ForeColor = Color.White;
                this.richTextBox1.ForeColor = Color.White;
                this.listBox1.ForeColor = Color.White;
            }
            if (id == 1) //light theme
            {
                this.BackColor = Color.FromArgb(240, 240, 240);
                this.richTextBox1.BackColor = Color.FromArgb(255, 255, 255);
                this.listBox1.BackColor = Color.FromArgb(255, 255, 255);
                this.label1.ForeColor = Color.Black;
                this.richTextBox1.ForeColor = Color.Black;
                this.listBox1.ForeColor = Color.Black;
            }
            if (id == 2) //red theme
            {
                this.BackColor = Color.FromArgb(150, 20, 20);
                this.richTextBox1.BackColor = Color.FromArgb(170, 40, 40);
                this.listBox1.BackColor = Color.FromArgb(170, 40, 40);
                this.label1.ForeColor = Color.Black;
                this.richTextBox1.ForeColor = Color.Black;
                this.listBox1.ForeColor = Color.Black;
            }
            if (id == 3) // green theme
            {
                this.BackColor = Color.FromArgb(20, 150, 20);
                this.richTextBox1.BackColor = Color.FromArgb(40, 170, 40);
                this.listBox1.BackColor = Color.FromArgb(40, 170, 40);
                this.label1.ForeColor = Color.Black;
                this.richTextBox1.ForeColor = Color.Black;
                this.listBox1.ForeColor = Color.Black;
            }
            if (id == 4) // blue theme
            {
                this.BackColor = Color.FromArgb(40, 110, 200);
                this.richTextBox1.BackColor = Color.FromArgb(60, 130, 250);
                this.listBox1.BackColor = Color.FromArgb(60, 130, 250);
                this.label1.ForeColor = Color.Black;
                this.richTextBox1.ForeColor = Color.Black;
                this.listBox1.ForeColor = Color.Black;
            }
            if (id == 5) // sky theme
            {
                this.BackColor = Color.FromArgb(70, 150, 230);
                this.richTextBox1.BackColor = Color.FromArgb(110, 200, 240);
                this.listBox1.BackColor = Color.FromArgb(110, 200, 240);
                this.label1.ForeColor = Color.Black;
                this.richTextBox1.ForeColor = Color.Black;
                this.listBox1.ForeColor = Color.Black;
            }
            this.Opacity = 97;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form settings = new Setting();
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
