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
    public partial class Deposite : Form
    {
        private string connectionString = "Data Source=DESKTOP-I8N0NEP\\SQLEXPRESS;Initial Catalog=BankManagementSystem2;Integrated Security=True";

        public Deposite()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            My_Account form = new My_Account();
            form.Show();
            this.Hide();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            int AccountID = int.Parse(IDBox.Text);
            decimal depositeAmount = decimal.Parse(DepositeBox.Text);
            deposite(AccountID, depositeAmount);
        }
        private void deposite(int AccountID, decimal depositeAmount)
        {
            

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))

            {
                sqlConnection.Open();

                SqlCommand selectCommand = new SqlCommand("SELECT InitialBalance FROM RegisterTable WHERE AccountID = @AccountID", sqlConnection);
                selectCommand.Parameters.AddWithValue("@AccountID", AccountID);
                decimal currentBalance = (decimal)selectCommand.ExecuteScalar();

                decimal newBalance = currentBalance + depositeAmount;
                SqlCommand updateCommand = new SqlCommand("UPDATE RegisterTable SET InitialBalance=@newBalance WHERE AccountID = @AccountID", sqlConnection);
                updateCommand.Parameters.AddWithValue("@newBalance", newBalance);
                updateCommand.Parameters.AddWithValue("@AccountId", AccountID);
                updateCommand.ExecuteNonQuery();
                sqlConnection.Close();


                if (depositeAmount > 0)
                {
                    MessageBox.Show("Deposite successful. New balance: " + newBalance, "Success");
                    IDBox.Clear();
                    IDBox.Focus();
                    DepositeBox.Clear();

                }
                else
                {
                    MessageBox.Show("Deposite failed.", "Error");
                }
            }
        }
    }
}
