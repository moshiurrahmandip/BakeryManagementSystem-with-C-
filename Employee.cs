using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace BakeryManagementSystem
{
    public partial class Employee : Form
    {
        private string connectionString = "server=127.0.0.1;database=bms;username=root;password=";
        private int loggedInAdminID = -1;
        public Employee(int loggedInAdminID)
        {
            InitializeComponent();
            this.loggedInAdminID = loggedInAdminID;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            
            
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            try
            {
                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to select all data from the relevant table
                    string query = "SELECT * FROM employee";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    // Create a DataAdapter to fill a DataTable with the results of the query
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        // Create a new DataTable
                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with the results of the query
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // Add Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data entered by the user
                string username = textBox_Username.Text;
                string password = textBox_Password.Text;
                string name = textBox_Name.Text;
                string phoneNumber = textBox_PhoneNumber.Text;
                string address = textBox_Address.Text;

                // Validate input data
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address))
                {
                    MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate that the name does not contain any digits
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Name cannot contain digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate that the phone number contains only digits and is exactly 11 digits long
                if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length != 11)
                {
                    MessageBox.Show("Invalid phone number. Phone number must contain exactly 11 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the username already exists in the database
                string checkQuery = "SELECT COUNT(*) FROM Employee WHERE Username = @Username";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing username
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Username", username);

                        // Execute the command to check if the username exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the username already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Employee with the same username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Construct SQL INSERT statement with Admin_ID included
                    string query = "INSERT INTO Employee (Username, Password, Name, Phone_Number, Address, Admin_ID) VALUES (@Username, @Password, @Name, @PhoneNumber, @Address, @AdminID)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@AdminID", loggedInAdminID); // Include the loggedInAdminID

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Edit Button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data entered by the user
                string username = textBox_Username.Text;
                string password = textBox_Password.Text;
                string name = textBox_Name.Text;
                string phoneNumber = textBox_PhoneNumber.Text;
                string address = textBox_Address.Text;

                // Validate input data
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address))
                {
                    MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate that the name does not contain any digits
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Name cannot contain digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate that the phone number contains only digits and is exactly 11 digits long
                if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length != 11)
                {
                    MessageBox.Show("Invalid phone number. Phone number must contain exactly 11 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the username already exists in the database
                string checkQuery = "SELECT COUNT(*) FROM Employee WHERE Username = @Username";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing username
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Username", username);

                        // Execute the command to check if the username exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the username already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Employee with the same username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Construct SQL INSERT statement with Admin_ID included
                    string query = "INSERT INTO Employee (Username, Password, Name, Phone_Number, Address, Admin_ID) VALUES (@Username, @Password, @Name, @PhoneNumber, @Address, @AdminID)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@AdminID", loggedInAdminID); // Include the loggedInAdminID

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete Button
        private void button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Check if a row is selected in the DataGridView
                    if (dataGridView1.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Retrieve the ID of the selected record
                    int employeeID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Employee_ID"].Value);

                    // Construct SQL DELETE statement
                    string query = "DELETE FROM Employee WHERE Employee_ID = @EmployeeID";

                    // Create a connection to the database
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        // Open the connection
                        connection.Open();

                        // Create a command with the DELETE statement
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            // Add parameters to the command to prevent SQL injection
                            command.Parameters.AddWithValue("@EmployeeID", employeeID);

                            // Execute the DELETE command
                            int rowsAffected = command.ExecuteNonQuery();

                            // Check if the deletion was successful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Employee deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh the DataGridView to display the updated data
                                LoadDataIntoDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Search Button
        private void button4_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Construct SQL SELECT statement with a WHERE clause to filter records based on the search criteria
                    string query = "SELECT * FROM Employee WHERE ";

                    // Add search conditions for each present text box
                    List<string> searchConditions = new List<string>();

                    // Check if text boxes have text entered and add corresponding search conditions
                    if (!string.IsNullOrEmpty(textBox_Username.Text))
                    {
                        searchConditions.Add("Username LIKE @Username");
                    }

                    if (!string.IsNullOrEmpty(textBox_Name.Text))
                    {
                        searchConditions.Add("Name LIKE @Name");
                    }

                    if (!string.IsNullOrEmpty(textBox_PhoneNumber.Text))
                    {
                        searchConditions.Add("Phone_Number LIKE @PhoneNumber");
                    }

                    if (!string.IsNullOrEmpty(textBox_Address.Text))
                    {
                        searchConditions.Add("Address LIKE @Address");
                    }

                    // If no search conditions were added, show all records
                    if (searchConditions.Count == 0)
                    {
                        query += "1"; // Always true condition
                    }
                    else
                    {
                        // Combine search conditions with OR operator
                        query += string.Join(" OR ", searchConditions);
                    }

                    // Create a connection to the database
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        // Open the connection
                        connection.Open();

                        // Create a command with the SELECT statement
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            // Add parameters to the command to prevent SQL injection
                            if (!string.IsNullOrEmpty(textBox_Username.Text))
                            {
                                command.Parameters.AddWithValue("@Username", "%" + textBox_Username.Text.Trim() + "%");
                            }

                            if (!string.IsNullOrEmpty(textBox_Name.Text))
                            {
                                command.Parameters.AddWithValue("@Name", "%" + textBox_Name.Text.Trim() + "%");
                            }

                            if (!string.IsNullOrEmpty(textBox_PhoneNumber.Text))
                            {
                                command.Parameters.AddWithValue("@PhoneNumber", "%" + textBox_PhoneNumber.Text.Trim() + "%");
                            }

                            if (!string.IsNullOrEmpty(textBox_Address.Text))
                            {
                                command.Parameters.AddWithValue("@Address", "%" + textBox_Address.Text.Trim() + "%");
                            }

                            // Create a DataAdapter to fill a DataTable with the results of the query
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                // Create a new DataTable
                                DataTable dataTable = new DataTable();

                                // Fill the DataTable with the results of the query
                                adapter.Fill(dataTable);

                                // Bind the DataTable to the DataGridView
                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching for employees: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Show Selected Text from datagridview to textboxs
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the cell clicked is not the header row
                if (e.RowIndex >= 0)
                {
                    // Retrieve data from the selected row and display it in the corresponding text boxes
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    textBox_Username.Text = selectedRow.Cells["Username"].Value.ToString();
                    textBox_Password.Text = selectedRow.Cells["Password"].Value.ToString();
                    textBox_Name.Text = selectedRow.Cells["Name"].Value.ToString();
                    textBox_PhoneNumber.Text = selectedRow.Cells["Phone_Number"].Value.ToString();
                    textBox_Address.Text = selectedRow.Cells["Address"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Go Back To Previous Page
        private void button_Back_Click(object sender, EventArgs e)
        {
            AdminDashboard AdminDashboard = new AdminDashboard(loggedInAdminID);
            AdminDashboard.Show();
            this.Hide();
        }

    }
}
