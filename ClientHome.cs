using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace wedding_test_2
{
    public partial class clienthome : Form
    {
        String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
        private int clientID;
        private int orderID;

        public clienthome()
        {
            InitializeComponent();
        }
        public clienthome(int clientID)
        {
            InitializeComponent();
            this.clientID = clientID;
            this.orderID = -1;
        }
        public clienthome(int clientID, int orderID)
        {
            InitializeComponent();
            this.clientID = clientID;
            this.orderID = orderID;
        }



        /*private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new bookvenue().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new clientprofile().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new bookstage().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new bookfood().Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }*/

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (orderID == -1)
            {
                Application.Exit();
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                String sql = "DELETE FROM [OrderTable] WHERE OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                orderID = -1;
                Application.Exit();
            }*/
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
  
        clientprofile c1 = new clientprofile(clientID,orderID);
            c1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            bookstage b1 = new bookstage(clientID, orderID);
            b1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            bookfood f1 = new bookfood(clientID, orderID);
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookPhotography p1 = new BookPhotography(clientID, orderID);
            p1.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookingItem b2 = new BookingItem(clientID, orderID);
            b2.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Review r1 = new Review(clientID, orderID);
            r1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            bookvenue b3 = new bookvenue(clientID, orderID);
            b3.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (orderID != -1)
            {
                DialogResult r = MessageBox.Show("Are you sure you want to Log out?\nAny unpaid Order will be deleted!", "WARNING", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(cs);
                    String sql = "DELETE FROM [OrderTable] WHERE OrderID = @OrderID";                                
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@OrderID", orderID);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        this.Hide();
                        new Form2().Show();                        
                    }
                    else
                    {
                        MessageBox.Show("Task failed", "Error");
                    }
                    con.Close();
                }
            }
            else
            {
                this.Hide();
                new Form2().Show();
            }
        }
    }
}
