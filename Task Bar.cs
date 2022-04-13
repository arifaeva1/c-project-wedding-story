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
    public partial class TaskBar : Form
    {
        String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
        private int empID;
        String orderID;
        public TaskBar()
        {
            InitializeComponent();
            BindGridView();
        }

        public TaskBar(int empID)
        {
            InitializeComponent();            
            this.empID = empID;
            BindGridView();
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from OrderTable Where EmployeeID = '" + empID + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }


        private void TaskBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmpHome e1 = new EmpHome(empID);
            e1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            String sql = "UPDATE [OrderTable] SET Status = 'started'" +
                                " WHERE OrderID = @OrderID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@OrderID", orderID);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("Order "+orderID+" has been started");

                BindGridView();

            }
            else
            {
                MessageBox.Show("Starting failed", "Error");
            }
            con.Close();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            orderID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            MessageBox.Show("Order " + orderID + " has been selected");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            String sql = "UPDATE [OrderTable] SET Status = 'finished'" +
                                " WHERE OrderID = @OrderID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@OrderID", orderID);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("Order " + orderID + " has been finished");

                BindGridView();

            }
            else
            {
                MessageBox.Show("finishing failed", "Error");
            }
            con.Close();
        }
    }
}
