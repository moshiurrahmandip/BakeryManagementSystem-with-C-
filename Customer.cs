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
    public partial class Customer : Form
    {
        private string connectionString = "server=127.0.0.1;database=bms;username=root;password=";
        private int loggedInAdminID = -1;

        public Customer(int loggedInAdminID)
        {
            InitializeComponent();
            this.loggedInAdminID = loggedInAdminID;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void Form7_Load(object sender, EventArgs e)
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
                    string query = "SELECT * FROM Customer";
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
                string name = textBox_Name.Text;
                string phoneNumber = textBox_PhoneNumber.Text;

                // Validate input data
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber))
                {
                    MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Additional validation for name (no digits allowed)
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Customer name cannot contain digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Additional validation for phone number (only digits allowed and exactly 11 digits)
                if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length != 11)
                {
                    MessageBox.Show("Phone number must contain exactly 11 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the customer already exists in the database based on name and phone number
                string checkQuery = "SELECT COUNT(*) FROM Customer WHERE Name = @Name AND Phone_Number = @PhoneNumber";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing customer
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Name", name);
                        checkCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        // Execute the command to check if the customer exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the customer already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Customer with the same name and phone number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Construct SQL INSERT statement
                    string query = "INSERT INTO Customer (Name, Phone_Number) VALUES (@Name, @PhoneNumber)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Edit Button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data entered by the user
                string name = textBox_Name.Text;
                string phoneNumber = textBox_PhoneNumber.Text;

                // Validate input data
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber))
                {
                    MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Additional validation for name (no digits allowed)
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Customer name cannot contain digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Additional validation for phone number (only digits allowed and exactly 11 digits)
                if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length != 11)
                {
                    MessageBox.Show("Phone number must contain exactly 11 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the customer already exists in the database based on name and phone number
                string checkQuery = "SELECT COUNT(*) FROM Customer WHERE Name = @Name AND Phone_Number = @PhoneNumber";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing customer
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Name", name);
                        checkCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        // Execute the command to check if the customer exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the customer already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Customer with the same name and phone number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Construct SQL INSERT statement
                    string query = "INSERT INTO Customer (Name, Phone_Number) VALUES (@Name, @PhoneNumber)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Delete Button
        private void button3_Click(object sender, EventArgs e)
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
                int customerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Customer_ID"].Value);

                // Construct SQL DELETE statement
                string query = "DELETE FROM Customer WHERE Customer_ID = @CustomerID";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the DELETE statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@CustomerID", customerID);

                        // Execute the DELETE command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the deletion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        // Search Button
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Construct SQL SELECT statement with a WHERE clause to filter records based on the search criteria
                string query = "SELECT * FROM Customer WHERE ";

                // Add search conditions for each present text box
                List<string> searchConditions = new List<string>();

                // Check if text boxes have text entered and add corresponding search conditions
                if (!string.IsNullOrEmpty(textBox_Name.Text))
                {
                    searchConditions.Add("Name LIKE @Name");
                }

                if (!string.IsNullOrEmpty(textBox_PhoneNumber.Text))
                {
                    searchConditions.Add("Phone_Number LIKE @PhoneNumber");
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
                        if (!string.IsNullOrEmpty(textBox_Name.Text))
                        {
                            command.Parameters.AddWithValue("@Name", "%" + textBox_Name.Text.Trim() + "%");
                        }

                        if (!string.IsNullOrEmpty(textBox_PhoneNumber.Text))
                        {
                            command.Parameters.AddWithValue("@PhoneNumber", "%" + textBox_PhoneNumber.Text.Trim() + "%");
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
                MessageBox.Show("Error searching for customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        // Show selected row from datagridview to texboxes
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked cell is in the "Customer_ID" column
                if (e.ColumnIndex == dataGridView1.Columns["Customer_ID"].Index && e.RowIndex >= 0)
                {
                    // Retrieve data from the selected row and display it in the corresponding text boxes
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    textBox_CustomerID.Text = selectedRow.Cells["Customer_ID"].Value.ToString();
                    textBox_Name.Text = selectedRow.Cells["Name"].Value.ToString();
                    textBox_PhoneNumber.Text = selectedRow.Cells["Phone_Number"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        // Back to previous page
        private void button_Back_Click(object sender, EventArgs e)
        {
            AdminDashboard AdminDashboard = new AdminDashboard(loggedInAdminID);
            AdminDashboard.Show();
            this.Hide();
        }
    }
}
