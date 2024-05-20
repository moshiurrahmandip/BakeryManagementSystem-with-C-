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
    public partial class Supplier : Form
    {
        private string connectionString = "server=127.0.0.1;database=bms;username=root;password=";
        private int loggedInAdminID = -1;


        public Supplier(int loggedInAdminID)
        {
            InitializeComponent();
            this.loggedInAdminID = loggedInAdminID;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void Form4_Load(object sender, EventArgs e)
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
                    string query = "SELECT * FROM supplier";
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
                string address = textBox_Address.Text;

                // Validate input data
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address))
                {
                    MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate name: Check if the name contains any numeric characters
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Name cannot contain numeric characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate phone number: Ensure it contains only digits and is exactly 11 digits long
                if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length != 11)
                {
                    MessageBox.Show("Invalid phone number. Phone number must contain exactly 11 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the supplier already exists in the database based on name and phone number
                string checkQuery = "SELECT COUNT(*) FROM Supplier WHERE Name = @Name AND Phone_Number = @PhoneNumber";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing supplier
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Name", name);
                        checkCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        // Execute the command to check if the supplier exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the supplier already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Supplier with the same name and phone number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Construct SQL INSERT statement
                    string query = "INSERT INTO Supplier (Name, Phone_Number, Address) VALUES (@Name, @PhoneNumber, @Address)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Address", address);

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string address = textBox_Address.Text;

                // Validate input data
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address))
                {
                    MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate name: Check if the name contains any numeric characters
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Name cannot contain numeric characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate phone number: Ensure it contains only digits and is exactly 11 digits long
                if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length != 11)
                {
                    MessageBox.Show("Invalid phone number. Phone number must contain exactly 11 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the supplier already exists in the database based on name and phone number
                string checkQuery = "SELECT COUNT(*) FROM Supplier WHERE Name = @Name AND Phone_Number = @PhoneNumber";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing supplier
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Name", name);
                        checkCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        // Execute the command to check if the supplier exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the supplier already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Supplier with the same name and phone number already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Construct SQL INSERT statement
                    string query = "INSERT INTO Supplier (Name, Phone_Number, Address) VALUES (@Name, @PhoneNumber, @Address)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Address", address);

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int supplierID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Supplier_ID"].Value);

                // Construct SQL DELETE statement
                string query = "DELETE FROM Supplier WHERE Supplier_ID = @SupplierID";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the DELETE statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@SupplierID", supplierID);

                        // Execute the DELETE command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the deletion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        // Search Button

        private void button4_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Construct SQL SELECT statement with a WHERE clause to filter records based on the search criteria
                    string query = "SELECT * FROM Supplier WHERE ";

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
                    MessageBox.Show("Error searching for suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Show selected texts from the datagridview to textboxes
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                try
                {
                    // Check if the clicked cell is in the "Supplier_ID" column
                    if (e.ColumnIndex == dataGridView1.Columns["Supplier_ID"].Index && e.RowIndex >= 0)
                    {
                        // Retrieve data from the selected row and display it in the corresponding text boxes
                        DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                        textBox_SupplierID.Text = selectedRow.Cells["Supplier_ID"].Value.ToString();
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
