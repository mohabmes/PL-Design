using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.calitha.commons;
using com.calitha.goldparser;


namespace PLD_Project
{
    public partial class Form1 : Form
    {
        MyParser parser;
        public Form1()
        {
            InitializeComponent();
            parser = new MyParser("myproject.cgt", listBox1, label2);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            parser.Parse(richTextBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
