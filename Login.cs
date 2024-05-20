using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BakeryManagementSystem
{
    public partial class Login : Form
    {
        private string connectionString = "server=127.0.0.1;database=bms;username=root;password=";
        private int loggedInAdminID = -1;
        private int loggedInEmployeeID = -1;
        public Login()
        {
            InitializeComponent();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            {
                string username = textBox_Username.Text.Trim();
                string password = textBox_Password.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Check in the Admin table
                        string adminQuery = "SELECT Admin_ID FROM Admin WHERE Username = @Username AND Password = @Password";
                        using (MySqlCommand adminCommand = new MySqlCommand(adminQuery, connection))
                        {
                            adminCommand.Parameters.AddWithValue("@Username", username);
                            adminCommand.Parameters.AddWithValue("@Password", password);
                            object adminResult = adminCommand.ExecuteScalar();

                            if (adminResult != null) // If authentication successful
                            {
                                // Save the Admin ID to the global variable
                                loggedInAdminID = Convert.ToInt32(adminResult);

                               // MessageBox.Show("Admin login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Redirect to the admin dashboard
                                AdminDashboard adminDashboardForm = new AdminDashboard(loggedInAdminID);
                                adminDashboardForm.Show();
                                this.Hide();
                                return;
                            }
                        }

                        // Check in the Employee table
                        string employeeQuery = "SELECT Employee_ID FROM Employee WHERE Username = @Username AND Password = @Password";
                        using (MySqlCommand employeeCommand = new MySqlCommand(employeeQuery, connection))
                        {
                            employeeCommand.Parameters.AddWithValue("@Username", username);
                            employeeCommand.Parameters.AddWithValue("@Password", password);
                            object employeeResult = employeeCommand.ExecuteScalar();

                            if (employeeResult != null) // If authentication successful
                            {
                                // Save the Employee ID to the global variable
                                loggedInEmployeeID = Convert.ToInt32(employeeResult);

                               // MessageBox.Show("Employee login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Redirect to the employee dashboard
                                Invoice employeeDashboardForm = new Invoice(loggedInEmployeeID); // Pass loggedInEmployeeID
                                employeeDashboardForm.Show();
                                this.Hide();
                                return;
                            }
                        }

                        // If no match found in either table
                        MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        // Exit Button
        private void button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}





