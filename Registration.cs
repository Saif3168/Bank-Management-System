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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionString = "Data Source=DESKTOP-I8N0NEP\\SQLEXPRESS;Initial Catalog=BankManagementSystem2;Integrated Security=True";

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                var insertQuery = "insert into RegisterTable values(@AccountName, @AccountID, @AccountType, @Gender, @Branch, @InitialBalance, @MobileNumber)";

                SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@AccountName", NameBox.Text);
                sqlCommand.Parameters.AddWithValue("@AccountID", IDBox.Text);
                sqlCommand.Parameters.AddWithValue("@AccountType", TypeBox.Text);

                sqlCommand.Parameters.AddWithValue("@Gender", GBox.Text);
                sqlCommand.Parameters.AddWithValue("@Branch", BBox.Text);

                sqlCommand.Parameters.AddWithValue("@InitialBalance", double.Parse(BalanceBox.Text));
                sqlCommand.Parameters.AddWithValue("@MobileNumber", MobileBox.Text);

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                MessageBox.Show("Data Inserted Succesfully!!");

                NameBox.Clear();
                NameBox.Focus();
                IDBox.Clear();
                TypeBox.Clear();
                GBox.Clear();
                BBox.Clear();
                BalanceBox.Clear();
                MobileBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Somthing went wrong");
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            My_Account form = new My_Account();
            form.Show();
            this.Hide();
        }
    }
}
