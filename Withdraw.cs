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
    public partial class Withdraw : Form
    {
        private string connectionString = "Data Source=DESKTOP-I8N0NEP\\SQLEXPRESS;Initial Catalog=BankManagementSystem2;Integrated Security=True";
        public Withdraw()
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
            decimal withdrawalAmount = decimal.Parse(WithdrawBox.Text);
            withdraw(AccountID, withdrawalAmount);
        }
        private void withdraw(int AccountID, decimal withdrawalAmount)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand selectCommand = new SqlCommand("SELECT InitialBalance FROM RegisterTable WHERE AccountID = @AccountID", sqlConnection);
                selectCommand.Parameters.AddWithValue("@AccountID", AccountID);
                decimal currentBalance = (decimal)selectCommand.ExecuteScalar();

                if (currentBalance >= withdrawalAmount)
                {
                    decimal newBalance = currentBalance - withdrawalAmount;
                    SqlCommand updateCommand = new SqlCommand("UPDATE RegisterTable SET InitialBalance = @newBalance WHERE AccountID = @AccountID", sqlConnection);
                    updateCommand.Parameters.AddWithValue("@newBalance", newBalance);
                    updateCommand.Parameters.AddWithValue("@AccountID", AccountID);

                    updateCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                    if (withdrawalAmount > 0)
                    {
                        MessageBox.Show("Withdrawa successful. New balance: " + newBalance, "Success");
                        IDBox.Clear();
                        IDBox.Focus();
                        WithdrawBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Withdrawa failed.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Insufficient balance.", "Error");
                }
            }
        }
    }
}
