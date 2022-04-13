using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace wedding_test_2
{
    public partial class Review : Form
    {
        String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
        private int clientID;
        private int orderID;
        public Review()
        {
            InitializeComponent();
        }

        public Review(int clientID, int orderID)
        {
            InitializeComponent();
            this.clientID = clientID;
            this.orderID = orderID;
        }

        private void Review_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            clienthome c1 = new clienthome(clientID, orderID);
            c1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into ReviewTable values (@name,@reviewText)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@reviewText", textBox2.Text);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Review Inserted Successfully");

                }
            }
            else
            {
                MessageBox.Show("Review Not Inserted");
            }
        }

        private void Review_Load(object sender, EventArgs e)
        {

        }
    }
}
