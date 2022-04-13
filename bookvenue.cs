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
    public partial class bookvenue : Form
    {
        String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
        private int clientID;
        private int orderID;
        public bookvenue()
        {
            InitializeComponent();
            BindGridView();
        }

        public bookvenue(int clientID, int orderID)
        {
            this.clientID = clientID;
            this.orderID = orderID;
            InitializeComponent();
            BindGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            clienthome c1 = new clienthome(clientID, orderID);
            c1.Show();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from VenueTable ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[3];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.RowTemplate.Height = 100;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[3].Value);
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (orderID == -1)
                {
                    SqlConnection con = new SqlConnection(cs);
                    String sql = "INSERT INTO [OrderTable] (ClientID, VenueID, Status)" +
                                " VALUES(@ClientID, @VenueID, 'unpaid');" +
                                " SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ClientID", clientID);
                    cmd.Parameters.AddWithValue("@VenueID", textBox1.Text);

                    con.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    if (result >= 0)
                    {
                        orderID = result;
                        MessageBox.Show("Order created successfully\nYour OrderID is " + orderID);
                    }
                    else
                    {
                        MessageBox.Show("Task failed", "Error");
                    }
                    con.Close();
                }
                else
                {
                    SqlConnection con = new SqlConnection(cs);
                    String sql = "UPDATE [OrderTable] SET ClientID = @ClientID, VenueID = @VenueID" +
                                " WHERE OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ClientID", clientID);
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    cmd.Parameters.AddWithValue("@VenueID", textBox1.Text);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Order updated successfully successfull\nYour OrderID is " + orderID);
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
                MessageBox.Show("Select an Item first", "Error");
            }
        }
    }
}
