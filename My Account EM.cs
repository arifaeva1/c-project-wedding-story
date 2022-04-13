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
    public partial class MyAccountEm : Form
    {
        String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
        int empID;
        public MyAccountEm()
        {
            InitializeComponent();
            BindGridView();

        }


        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Users Where userId = '" + empID + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }
        public MyAccountEm(int empID)
        {
            InitializeComponent();

            this.empID = empID;
            BindGridView();
        }
         


        private void MyAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmpHome e1 = new EmpHome(empID);
            e1.Show();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            String sql = "Update [Users] set Username = @user, Email = @email, Password = @pass, " +
                "MobileNo = @mobile, Address = @adddress where userID = @id";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@id", empID);
            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@email", textBox5.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);
            cmd.Parameters.AddWithValue("@mobile", textBox3.Text);
            cmd.Parameters.AddWithValue("@adddress", textBox4.Text);
            if (textBox1.Text == "")
            {
                MessageBox.Show("Select a data first", "Error");
            }
            else
            {
                con.Open();
                int flag = cmd.ExecuteNonQuery();
                if (flag > 0)
                {
                    MessageBox.Show("Employee updated successfilly");

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
