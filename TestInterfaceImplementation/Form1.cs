using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestInterfaceImplementation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1 c = new Class1();
            c.DoThis();

            Interface2 i = c; //To explicitly access the explicitly declared interface method (without modifier) 
            i.Dothis(); 
        }
    }

    interface Interface1
    {
        void DoThis();
        void DoThat();
    }

    class Class1 : Interface1, Interface2
    {
        public void DoThat() //implicit
        {
            throw new NotImplementedException();
        }

        public void DoThis() //implicit
        {
            System.Windows.Forms.MessageBox.Show("Interface1 DoThis()");

        }

        void Interface2.DoThat() //explicit
        {
            throw new NotImplementedException();
        }

        void Interface2.Dothis() //explicit
        {
            System.Windows.Forms.MessageBox.Show("Interface2 DoThis()");
        }
    }

    interface Interface2
    {
        void Dothis();

        void DoThat();
    }
}
