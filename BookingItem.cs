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
	public partial class BookingItem : Form
	{
		String cs = ConfigurationManager.ConnectionStrings["WS"].ConnectionString;
		private int clientID;
		private int orderID;
		String venueID;
		String foodID;
		String photographerID;
		String stageID;
		int price = 0;
		bool flag=false;
		 
		public BookingItem()
		{
			InitializeComponent();
			BindGridView();
			textBox1.Text = "0";
		}

		public BookingItem(int clientID, int orderID)
		{
			InitializeComponent();            
			this.clientID = clientID;
			this.orderID = orderID;
			BindGridView();
		}

		void GetPrice()
		{
			SqlConnection con = new SqlConnection(cs);
			String sql = "select * from VenueTable Where VenueID = '" + venueID + "' ";
			SqlCommand cmd = new SqlCommand(sql, con);
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();

			if (reader.HasRows == true)
			{
				while (reader.Read())
				{
					price += Convert.ToInt32(reader[2]);
				}
			}            
			con.Close();

			con = new SqlConnection(cs);
			sql = "select * from FoodTable Where FoodID = '" + foodID + "' ";
			cmd = new SqlCommand(sql, con);
			con.Open();
			reader = cmd.ExecuteReader();

			if (reader.HasRows == true)
			{
				while (reader.Read())
				{
					price += Convert.ToInt32(reader[2]);
				}
			}
			con.Close();

			con = new SqlConnection(cs);
			sql = "select * from StageTable Where StageID = '" + stageID + "' ";
			cmd = new SqlCommand(sql, con);
			con.Open();
			reader = cmd.ExecuteReader();

			if (reader.HasRows == true)
			{
				while (reader.Read())
				{
					price += Convert.ToInt32(reader[2]);
				}
			}
			con.Close();

			con = new SqlConnection(cs);
			sql = "select * from [PhotographyTable] Where PhotographerID = '" + photographerID + "' ";
			cmd = new SqlCommand(sql, con);
			con.Open();
			reader = cmd.ExecuteReader();

			if (reader.HasRows == true)
			{
				while (reader.Read())
				{
					price += Convert.ToInt32(reader[2]);
				}
			}
			con.Close();


		}

		void BindGridView()
		{
			SqlConnection con = new SqlConnection(cs);
			string query = "select * from OrderTable Where ClientID = '"+clientID+"' ";
			SqlDataAdapter sda = new SqlDataAdapter(query, con);

			DataTable data = new DataTable();
			sda.Fill(data);
			dataGridView1.DataSource = data;
		}


		private void BookingItem_FormClosing(object sender, FormClosingEventArgs e)
		{
			Application.Exit();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.Hide();
			clienthome c1 = new clienthome(clientID, orderID);
			c1.Show();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			
			SqlConnection con = new SqlConnection(cs);
			String sql = "select * from OrderTable Where OrderID = '" + orderID + "' ";
			SqlCommand cmd = new SqlCommand(sql, con);
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();

			if(reader.HasRows == true)
			{
				while (reader.Read())
				{
					foodID = reader[2].ToString();
					photographerID = reader[3].ToString();
					stageID = reader[4].ToString();
					venueID = reader[5].ToString();
				}
				if (foodID==null)
				{
					MessageBox.Show("Please select Food");
				}
				else if(photographerID == null)
				{
					MessageBox.Show("Please select Photographer");
				}
				else if (stageID == null)
				{
					MessageBox.Show("Please select Stage");
				}                
				else
				{
					flag = true;
				}
			}
			else
			{
				MessageBox.Show("Please select Venue");
			}
						
			con.Close();

			con = new SqlConnection(cs);
			sql = "select * from OrderTable Where OrderDate = '" + dateTimePicker1.Text + "' AND VenueID = '"+venueID+"'";
			cmd = new SqlCommand(sql, con);
			con.Open();
			reader = cmd.ExecuteReader();
			if (reader.HasRows == true)
			{
				MessageBox.Show("This venue is already booked at this date\nPlease try another date");
				con.Close();
			}
			else
			{
				con.Close();
				if (flag == true)
				{
					GetPrice();
					textBox1.Text = price.ToString();
					con = new SqlConnection(cs);
					sql = "UPDATE [OrderTable] SET OrderDate = @OrderDate, TotalPrice = @TotalPrice" +
								" WHERE OrderID = @OrderID";
					cmd = new SqlCommand(sql, con);
					cmd.Parameters.AddWithValue("@OrderDate", dateTimePicker1.Text);
					cmd.Parameters.AddWithValue("@OrderID", orderID);
					cmd.Parameters.AddWithValue("@TotalPrice", textBox1.Text);

					con.Open();
					int result = cmd.ExecuteNonQuery();

					if (result > 0)
					{
						MessageBox.Show("Order submitted successfully\nPlease pay order amount\nYour OrderID is " + orderID);
						                        
						

						BindGridView();

					}
					else
					{
						MessageBox.Show("Task failed", "Error");
					}
					con.Close();
				}

			}
			

		}

        private void button2_Click(object sender, EventArgs e)
        {
			SqlConnection con = new SqlConnection(cs);
			String sql = "UPDATE [OrderTable] SET Status = 'paid'" +
								" WHERE OrderID = @OrderID";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.AddWithValue("@OrderID", orderID);
			con.Open();
			int result = cmd.ExecuteNonQuery();
			if (result > 0)
			{
				MessageBox.Show("Order payment complete\nYour OrderID is " + orderID);
				orderID = -1;
				venueID = null;
				foodID = null;
				photographerID = null;
				stageID = null;
				price = 0;
				flag = false;

				BindGridView();

			}
			else
			{
				MessageBox.Show("Task failed", "Error");
			}
			con.Close();
		}

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
			Bitmap pr1 = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
			dataGridView1.DrawToBitmap(pr1, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));

			e.Graphics.DrawImage(pr1, 0,200 );
		}

        private void button1_Click(object sender, EventArgs e)
        {
			printPreviewDialog1.Document = printDocument1;
			printPreviewDialog1.ShowDialog();
		}


    }
}
