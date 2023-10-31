using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp16
{
    public partial class My_Account : Form
    {
        public My_Account()
        {
            InitializeComponent();
        }

        private void View_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionString = "Data Source=DESKTOP-I8N0NEP\\SQLEXPRESS;Initial Catalog=BankManagementSystem2;Integrated Security=True";
                SqlConnection sqlConnecton = new SqlConnection(connectionString);
                sqlConnecton.Open();

                string readCommand = "select * from RegisterTable where AccountID=@AccountID ";
                SqlCommand sqlCommand = new SqlCommand(readCommand, sqlConnecton);
                sqlCommand.Parameters.AddWithValue("@AccountID", IDBox.Text);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error in Data Search");
            }
        }

        private void Deposite_Click(object sender, EventArgs e)
        {
            Deposite form = new Deposite();
            form.Show();
            this.Hide();
        }

        private void Withdraw_Click(object sender, EventArgs e)
        {
            Withdraw form = new Withdraw();
            form.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            form.Show();
            this.Hide();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionString = "Data Source=DESKTOP-I8N0NEP\\SQLEXPRESS;Initial Catalog=BankManagementSystem2;Integrated Security=True";
                SqlConnection sqlConnecton = new SqlConnection(connectionString);
                sqlConnecton.Open();

                string deleteCommand = "delete RegisterTable where AccountID= @AccountID";
                SqlCommand sqlCommand = new SqlCommand(deleteCommand, sqlConnecton);
                sqlCommand.Parameters.AddWithValue("@AccountID", IDBox.Text);


                sqlCommand.ExecuteNonQuery();
                sqlConnecton.Close();

                MessageBox.Show("Data Deleted Successfully!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error in Data Delete");
            }
        }
    }
}
