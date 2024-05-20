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
    public partial class Category : Form
    {
        private string connectionString = "server=127.0.0.1;database=bms;username=root;password=";
        private int loggedInAdminID = -1;


        public Category(int loggedInAdminID)
        {
            InitializeComponent();
            this.loggedInAdminID = loggedInAdminID;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }


        private void Form5_Load(object sender, EventArgs e)
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
                    string query = "SELECT * FROM category";
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

                // Validate input data
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter the category name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the name contains any integers
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Category name cannot contain numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the category already exists in the database
                string checkQuery = "SELECT COUNT(*) FROM Category WHERE Name = @Name";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing category
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Name", name);

                        // Execute the command to check if the category exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the category already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Category with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Construct SQL INSERT statement
                    string query = "INSERT INTO Category (Name) VALUES (@Name)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Edit Button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data entered by the user
                string name = textBox_Name.Text;

                // Validate input data
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter the category name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the name contains any integers
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Category name cannot contain numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the category already exists in the database
                string checkQuery = "SELECT COUNT(*) FROM Category WHERE Name = @Name";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing category
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Name", name);

                        // Execute the command to check if the category exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the category already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Category with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Construct SQL INSERT statement
                    string query = "INSERT INTO Category (Name) VALUES (@Name)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", name);

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int categoryID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Category_ID"].Value);

                // Construct SQL DELETE statement
                string query = "DELETE FROM Category WHERE Category_ID = @CategoryID";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the DELETE statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@CategoryID", categoryID);

                        // Execute the DELETE command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the deletion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        // Search Button
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Construct SQL SELECT statement with a WHERE clause to filter records based on the search criteria
                string query = "SELECT * FROM Category WHERE ";

                // Add search conditions for each present text box
                List<string> searchConditions = new List<string>();

                // Check if text boxes have text entered and add corresponding search conditions
                if (!string.IsNullOrEmpty(textBox_Name.Text))
                {
                    searchConditions.Add("Name LIKE @Name");
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
                MessageBox.Show("Error searching for categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Show selected row from datagridview to texboxes
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked cell is in the "Category_ID" column
                if (e.ColumnIndex == dataGridView1.Columns["Category_ID"].Index && e.RowIndex >= 0)
                {
                    // Retrieve data from the selected row and display it in the corresponding text boxes
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    textBox_CategoryID.Text = selectedRow.Cells["Category_ID"].Value.ToString();
                    textBox_Name.Text = selectedRow.Cells["Name"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Go back to previous page
        private void button_Back_Click(object sender, EventArgs e)
        {
            AdminDashboard AdminDashboard = new AdminDashboard(loggedInAdminID);
            AdminDashboard.Show();
            this.Hide();
        }
    }
}
