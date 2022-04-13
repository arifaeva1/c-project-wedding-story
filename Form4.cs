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
    public partial class Form4 : Form
    {
        String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
        public Form4()
        {
            InitializeComponent();
            BindGridView();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from [Users] where userType = 'admin'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            dataGridView1.Columns[3].Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            String sql = "INSERT INTO [Users](Username, Email, Password, MobileNo, Address, UserType)" +
                        " VALUES(@user, @email, @pass, @mobile, @adddress, @userType )";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@user", textBox2.Text);
            cmd.Parameters.AddWithValue("@email", textBox6.Text);
            cmd.Parameters.AddWithValue("@pass", textBox3.Text);
            cmd.Parameters.AddWithValue("@mobile", textBox4.Text);
            cmd.Parameters.AddWithValue("@adddress", textBox5.Text);
            cmd.Parameters.AddWithValue("@userType", "admin");

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
            BindGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            String sql = "Delete from [Users] where userID = @id";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            if (textBox1.Text == "")
            {
                MessageBox.Show("Select a admin first", "Error");
            }
            else
            {
                con.Open();
                int flag = cmd.ExecuteNonQuery();
                if (flag > 0)
                {
                    MessageBox.Show("Admin deleted successfilly");
                }
                else
                {
                    MessageBox.Show("Task failed", "Error");
                }
                con.Close();
                BindGridView();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            String sql = "Update [Users] set Username = @user, Email = @email, Password = @pass, " +
                "MobileNo = @mobile, Address = @adddress where userID = @id";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@user", textBox2.Text);
            cmd.Parameters.AddWithValue("@email", textBox6.Text);
            cmd.Parameters.AddWithValue("@pass", textBox3.Text);
            cmd.Parameters.AddWithValue("@mobile", textBox4.Text);
            cmd.Parameters.AddWithValue("@adddress", textBox5.Text);
            if (textBox1.Text == "")
            {
                MessageBox.Show("Select a admin first", "Error");
            }
            else
            {
                con.Open();
                int flag = cmd.ExecuteNonQuery();
                if (flag > 0)
                {
                    MessageBox.Show("Admin updated successfilly");
                }
                else
                {
                    MessageBox.Show("Task failed", "Error");
                }
                con.Close();
                BindGridView();
            }
        }
    }
}
