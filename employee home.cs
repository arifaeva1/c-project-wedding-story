using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wedding_test_2
{
    public partial class EmpHome : Form
    {
        int empID;
        public EmpHome()
        {
            InitializeComponent();
        }
        public EmpHome(int empID)
        {
            InitializeComponent();
            this.empID = empID;
        }



        private void EmpHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyAccountEm e1 = new MyAccountEm(empID);
            e1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            TaskBar t1 = new TaskBar(empID);
            t1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f1 = new Form2();
            f1.Show();
        }
    }
}
