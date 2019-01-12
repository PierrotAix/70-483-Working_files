using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_77_DelegateExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            c.d += EventRaise_Handler; //subscribing
        }

        public delegate void DoMethod(string x);

        public delegate int rmDel(int x);

        public delegate int Calculate(int x, int y);

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            Class1 c = new Class1();

            DoMethod DoIt = new DoMethod(c.DoThis);
            //DoMethod DoIt = delegate (string s) { MessageBox.Show("Appel depuis une méthode anomyne avec parametre: " + s); };
            //DoMethod DoIt = delegate (string s) { MessageBox.Show("Appel depuis une méthode anomyne avec parametre: " + s); };


            //DoIt.Invoke("Test msg");
            //DoIt("test another msg");

            DoMethod DoIt2;
            DoIt2 = c.DoThat;
            DoIt2("test a message");

            DoIt("Test");

            //DoIt -= c.DoThis;
            //DoIt += c.DoThat;

            DoIt("Pierre");

            DoMethod DoIt3;
            DoIt3 = DoIt + DoIt2;
            DoIt3("Multi");
            */

            RegMethog r = new RegMethog();
            rmDel d = r.Add50;
            int answer = d(25);
            MessageBox.Show("The delegate a returned: " + answer);

            rmDel ad = delegate (int x)
            {
                return x + 25;
            };
            int answer2 = ad(33);
            MessageBox.Show("The delegate ad returned: " + answer2);


        }

        Class2 c = new Class2();

        private void button2_Click(object sender, EventArgs e)
        {
            c.CheckValue(6);
        }

        public void EventRaise_Handler(object sender, CustomEventArgs e)
        {
            MessageBox.Show("The object: " + sender + " raised this message: " + e.Message);
        }

        private void button3_Click(object sender, EventArgs e)
        {            

        Calculate calc = (int x, int y) =>
            {
                return x + y;
            }; //Lamba expression

            MessageBox.Show("Le résultat de calc(5,5) est :" +
                 calc(5,5) );
        }

        /// <summary>
        /// Test action delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Action<int, int> calc1 = (int x, int y) =>
                {
                    MessageBox.Show("Le résultat est :" + (x + y));
                };

            calc1(78, 9);
            
        }

        /// <summary>
        /// Test Event overspeeb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Car c = new Car();
            c.OnChange += C_OnChange;//subscribing // deux fois TAB pour construire la fonction C_OnChange()
            c.Speed = 30;
            c.Speed = 70;
        }

        private void C_OnChange()
        {
            MessageBox.Show("Event FIRED: CAR IS >= 60 MPH");
        }
    }

    class Car
    {
        public event Action OnChange;

        private double speed;   

        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                if (speed >= 60)
                {
                    if (OnChange != null)
                    {
                        OnChange();
                    }
                }
            }
        }

    }

    class Class2
    {
        // public delegate void del(object sender, EventArgs e);

        //public event EventHandler d;

        public delegate void CustomEvent(object sender, CustomEventArgs e);

        public event CustomEvent d;

        public void CheckValue(int a)
        {
            if (a > 5)
            {
                // d(this, EventArgs.Empty ); //publisher
                CustomEventArgs ce = new CustomEventArgs("Hello");
                d(this, ce);
                MessageBox.Show("valeur de message: "+ ce.Message);
            }

        }
    }

    public class CustomEventArgs : EventArgs
    {
        private string message;

        public CustomEventArgs(string s)
        {
            message = s;
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    class RegMethog
    {
        public int Add50(int a)
        {
            return a + 50;
        }
    }

    public class Class1
    {
        public void DoThis(string a)
        {
            MessageBox.Show("Class1.DoThis a été appelé avec le paramètre: " + a);
        }

        public void DoThat(string a)
        {
            MessageBox.Show("Class1.DoThat a été appelé avec le paramètre: " + a);
        }

        public void DoSomethingElse(string a)
        {
            MessageBox.Show("Class1.DoSomethingElse a été appelé avec le paramètre: " + a);
        }
    }
}
