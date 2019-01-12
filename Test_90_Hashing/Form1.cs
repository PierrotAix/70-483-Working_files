using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_90_Hashing
{
    public partial class Form1 : Form
    {
        private string hashedPwd;
        private string pwdEntered;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hashedPwd = ComputeHash(textBox1.Text);
            textBox1.Text = hashedPwd;
        }

        public string ComputeHash(string data)
        {
            HashAlgorithm sha = SHA256.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(data));
            return Convert.ToBase64String(hashData);
        }

        private bool VerifyHash(string pwdEntered, string hashedPwd)
        {
            HashAlgorithm sha = SHA256.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(pwdEntered));
            return Convert.ToBase64String(hashData) == hashedPwd;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pwdEntered = textBox2.Text;
            bool hashCheck = VerifyHash(pwdEntered, hashedPwd);
            if (hashCheck)
                label1.Text = "The values match...";
            else
                label1.Text = "The values do not match...";

        }
    }
}
