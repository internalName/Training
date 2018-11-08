using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LearningPoetry.Elements.BLogic;
using IronPython.Hosting;
using IronPython.Modules;
using Microsoft.Scripting.Hosting;



namespace LearningPoetry
{
    public partial class Form1 : Form
    {
        private EntryClient _entry = default(EntryClient);
        private List<TextBox> _failTextBox = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();
            button1.Text = "Регистрация";
            button2.Text = "Вход";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ErrorController())
            {
                _entry = new EntryClient();
                _entry.CreateNewUser(textBox1.Text, textBox2.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ErrorController())
            {
                _entry = new EntryClient(textBox1.Text, textBox2.Text);
            }
        }

        private bool ErrorController()
        {
            CheckTxtBox(textBox1,textBox2);

            if (_failTextBox.Count>0)
            {
                for (int i = 0; i < _failTextBox.Count; i++)
                    errorProvider1.SetError(_failTextBox[i], "beda");
                return false;
            }

            return true;
        }

        private void CheckTxtBox(params TextBox[] txtBox)
        {
            for (int i = 0; i < txtBox.Length; i++)
            {
                if (txtBox[i]?.Text.Length < 6)
                {
                    if (!_failTextBox.Contains(txtBox[i])) _failTextBox.Add(txtBox[i]);
                }
                else
                {
                    _failTextBox.Remove(txtBox[i]);
                    errorProvider1.Clear();
                }
            }
        }
    }
}
