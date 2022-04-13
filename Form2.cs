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
    public partial class Form2 : Form
    {
        String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            String sql = "select * from [Users] where Username = @user AND Password = @pass";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@user", userNameTextBox.Text);
            cmd.Parameters.AddWithValue("@pass", passwordTextBox.Text);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == true)
            {
                MessageBox.Show("Login successfull");
                while (reader.Read())
                {
                    string userType = reader["UserType"].ToString();
                    int id = Convert.ToInt32(reader[0]);
                    if (userType == "admin")
                    {
                        Form3 form3 = new Form3();
                        form3.Show();
                        this.Hide();
                    }
                    else if (userType == "emp")
                    {
                        EmpHome empHome = new EmpHome(id);
                        empHome.Show();
                        this.Hide();
                    }
                    else if (userType == "client")
                    {
                        clienthome clienthome = new clienthome(id);
                        clienthome.Show();
                        this.Hide();
                    }
                }


            }
            else
            {
                MessageBox.Show("Login failed\nIncorrect username or password");
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form9 f9 = new Form9();
            f9.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    passwordTextBox.UseSystemPasswordChar = false;
                    break;
                default:
                    passwordTextBox.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void userNameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTextBox.Text))
            {
                userNameTextBox.Focus();
                errorProvider1.SetError(this.userNameTextBox, "Fill the field");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                passwordTextBox.Focus();
                errorProvider2.SetError(this.passwordTextBox, "Fill the field");

            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void AboutUS_Click(object sender, EventArgs e)
        {
            this.Hide();
            About_us a1 = new About_us();
            a1.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void userNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
