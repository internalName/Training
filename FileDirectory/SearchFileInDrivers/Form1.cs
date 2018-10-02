using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace SearchFileInDrivers
{
    public partial class Form1 : Form
    {
        private List<string> listError=new List<string>();
        private int counterError;

        public Form1()
        {
            InitializeComponent();
            this.button1.Text = "Поиск";
            this.button2.Text = "Вывести ошибки";
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width / 2;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height / 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            counterError = 10;

            var drive = DriveInfo.GetDrives();

            for (int i = 0; i < drive.Length; i++)
            {
                if (drive[i].DriveType != DriveType.CDRom)
                {
                    checkList.Items.Add(drive[i]);
                    checkList.SetItemCheckState(i,CheckState.Checked);
                }
            }
            if (checkList.Items.Count == 1) checkList.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int changeCounter = listBox1.Items.Count;
            FuncName(new DirectoryInfo($@"C:\"), new StringBuilder(txtBox.Text));
            if (listBox1.Items.Count == changeCounter) MessageBox.Show($"Не нашёл или уже есть");
            counterError*= 2;
        }

        public void FuncName(DirectoryInfo dir, StringBuilder fileName)
        {
            StringBuilder str;

            try
            {
                var files = dir.GetFiles(fileName.ToString()).ToArray();
                foreach (var file in files)
                {
                    try
                    {
                        if (TestOriginality(file)) listBox1.Items.Add(file.FullName);
                        return;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"{e.Message}\n{e.GetType()}");
                    }
                }

                foreach (var directory in dir.GetDirectories())
                {
                    FuncName(directory, fileName);
                    if(listError.Count==counterError) return;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                listError.Add(e.Message);
            }
        }

        private bool TestOriginality(FileInfo file)
        {
                
            foreach (var VARIABLE in listBox1.Items)
            {
                if (VARIABLE.Equals(file.FullName)) return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string errorList=String.Empty;

            foreach (var VARIABLE in listError)
            {
                errorList += VARIABLE;
            }

            MessageBox.Show($"{errorList}");
        }
    }
}