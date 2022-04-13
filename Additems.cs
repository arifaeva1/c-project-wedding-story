using System;
using System.Windows.Forms;

namespace wedding_test_2
{
    public partial class Additems : Form
    {
        public Additems()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Addvenue A2 = new Addvenue();
            A2.Show();
        }


        private void Additems_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Addfoodcs A3 = new Addfoodcs();
            A3.Show();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Addstage A4 = new Addstage();
            A4.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Addphotographer A5 = new Addphotographer();
            A5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }
    }
}
