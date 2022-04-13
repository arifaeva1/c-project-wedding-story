using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wedding_test_2
{
    public partial class Form9 : Form
    {
        String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
        public Form9()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            String sql = "INSERT INTO [Users](Username, Email, Password, MobileNo, Address, UserType)" +
                        " VALUES(@user, @email, @pass, @mobile, @adddress, @userType )";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@email", textBox2.Text);
            cmd.Parameters.AddWithValue("@pass", textBox5.Text);
            cmd.Parameters.AddWithValue("@mobile", textBox3.Text);
            cmd.Parameters.AddWithValue("@adddress", textBox4.Text);
            cmd.Parameters.AddWithValue("@userType", "client");

            con.Open();
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                MessageBox.Show("Registration successfull");
            }
            else
            {
                MessageBox.Show("Registration failed");
            }
            con.Close();
        }

        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
