using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestExtensionMethods
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int answer = IntExtensions.Half(4);
            //MessageBox.Show("Answer :" + answer);

            //int answer2 = 44.Half();
            //MessageBox.Show("Answer2 :" + answer2);

            int answer3 = 44.AddFive().Half();
            MessageBox.Show("Answer3 :" + answer3);

        }
    }
}
