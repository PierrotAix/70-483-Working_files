using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestInterfaceIComparable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int[] tabInt = new int[] {1, 6, 8, 3, 6, 8, 55, 66  };
            //tabInt.
            var myNbrs = new[] { 8, 5, 15, 9, 23 };
            Array.Sort(myNbrs);

            foreach (var i in myNbrs)
            {
                listBox1.Items.Add(i);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            if (false)
            { 
                List<Class1> listClass1 = new List<Class1>();
                listClass1.Add(new Class1 { ID = 1, Nom = "Pierre" });
                listClass1.Add(new Class1 { ID = 2, Nom = "Christine" });
                listClass1.Add(new Class1 { ID = 2, Nom = "Julia" });

                listClass1.Sort();

                foreach (var item in listClass1)
                {
                    listBox1.Items.Add(item.Nom);
                }
            }
            else
            {
                Class2[] mc = new Class2[5];

                mc[0] = new Class2();
                mc[0].TheValue = 5;

                mc[1] = new Class2();
                mc[1].TheValue = 3;

                mc[2] = new Class2();
                mc[2].TheValue = 11;

                mc[3] = new Class2();
                mc[3].TheValue = 8;

                mc[4] = new Class2();
                mc[4].TheValue = 4;

                Array.Sort(mc);

                foreach (var item in mc)
                {
                    listBox1.Items.Add(item.TheValue);
                }

            }
        }
    }

    [DebuggerDisplay("TheValue")]
    class Class2 : IComparable
    {
        public int TheValue;

        public int CompareTo(object obj)
        {
            Class2 mc = (Class2)obj;
            //if (this.TheValue == mc.TheValue) return 0;
            //int resultat = this.TheValue < mc.TheValue ? -1 : 1;
            //return resultat;
            return TheValue.CompareTo(mc.TheValue);
        }
    }

    class Class1 : IComparable
    {
        public int ID { get; set; }
        public string Nom { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Class1 monClass1 = (Class1)obj;

            if (this.Nom.Length > monClass1.Nom.Length)
                return 1;
            else return -1;

        }
    }
}
