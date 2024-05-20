using System;
using System.Collections;
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
    public partial class ProductForm : Form
    {
        private string connectionString = "server=127.0.0.1;database=bms;username=root;password=";
        private int loggedInAdminID = -1;


        public ProductForm(int loggedInAdminID)
        {
            InitializeComponent();
            this.loggedInAdminID = loggedInAdminID;
            dataGridView1.CellClick += dataGridView1_CellContentClick;

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // Populate the combo box for Supplier names
            PopulateSupplierComboBox();
            // Populate the combo box for Category names
            PopulateCategoryComboBox();
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
                    string query = "SELECT * FROM product";
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

        private void PopulateSupplierComboBox()
        {
            try
            {
                // Clear existing items
                comboBox_Supplier.Items.Clear();

                // Construct SQL SELECT statement to retrieve supplier names
                string query = "SELECT Name FROM Supplier";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the SELECT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the command and read the results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Add each supplier name to the combo box
                            while (reader.Read())
                            {
                                comboBox_Supplier.Items.Add(reader["Name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating Supplier combo box: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateCategoryComboBox()
        {
            try
            {
                // Clear existing items
                comboBox_Category.Items.Clear();

                // Construct SQL SELECT statement to retrieve category names
                string query = "SELECT Name FROM Category";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the SELECT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the command and read the results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Add each category name to the combo box
                            while (reader.Read())
                            {
                                comboBox_Category.Items.Add(reader["Name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating Category combo box: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adding
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data entered by the user
                string supplierName = comboBox_Supplier.SelectedItem?.ToString();
                string categoryName = comboBox_Category.SelectedItem?.ToString();
                string name = textBox_Name.Text;
                decimal costPrice;
                decimal sellingPrice;
                decimal weight;
                string unit = textBox_Unit.Text.Trim();
                int stock;

                // Validate input data
                if (string.IsNullOrEmpty(supplierName) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(name) ||
                    !decimal.TryParse(textBox_CostPrice.Text, out costPrice) || !decimal.TryParse(textBox_SellingPrice.Text, out sellingPrice) ||
                    !decimal.TryParse(textBox_Weight.Text, out weight) || !int.TryParse(textBox_Stock.Text, out stock))
                {
                    MessageBox.Show("Please enter all required fields with valid values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Additional validation for name and string fields
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Product name cannot contain digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(unit) && unit.Any(char.IsDigit))
                {
                    MessageBox.Show("Unit cannot contain digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate cost price, selling price, and weight to ensure they are greater than zero
                if (costPrice <= 0 || sellingPrice <= 0 || weight <= 0 || stock <= 0)
                {
                    MessageBox.Show("Cost price, selling price, weight, and stock must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the product already exists in the database based on name, supplier, and category
                string checkQuery = "SELECT COUNT(*) FROM Product p " +
                                    "INNER JOIN Supplier s ON p.Supplier_ID = s.Supplier_ID " +
                                    "INNER JOIN Category c ON p.Category_ID = c.Category_ID " +
                                    "WHERE p.Name = @Name AND s.Name = @SupplierName AND c.Name = @CategoryName";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing product
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Name", name);
                        checkCommand.Parameters.AddWithValue("@SupplierName", supplierName);
                        checkCommand.Parameters.AddWithValue("@CategoryName", categoryName);

                        // Execute the command to check if the product exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the product already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Product with the same name, supplier, and category already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Retrieve Supplier ID based on selected name
                    int supplierID = GetSupplierIDByName(supplierName);

                    // Retrieve Category ID based on selected name
                    int categoryID = GetCategoryIDByName(categoryName);

                    // Construct SQL INSERT statement
                    string query = "INSERT INTO Product (Supplier_ID, Category_ID, Name, Cost_Price, Selling_Price, Weight, Unit, Stock) VALUES (@SupplierID, @CategoryID, @Name, @CostPrice, @SellingPrice, @Weight, @Unit, @Stock)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@SupplierID", supplierID);
                        command.Parameters.AddWithValue("@CategoryID", categoryID);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@CostPrice", costPrice);
                        command.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                        command.Parameters.AddWithValue("@Weight", weight);
                        command.Parameters.AddWithValue("@Unit", unit);
                        command.Parameters.AddWithValue("@Stock", stock);

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int GetSupplierIDByName(string supplierName)
        {
            int supplierID = -1; // Default value for error handling

            try
            {
                // Construct SQL SELECT statement to retrieve Supplier ID by name
                string query = "SELECT Supplier_ID FROM Supplier WHERE Name = @Name";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the SELECT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", supplierName);

                        // Execute the command and retrieve the Supplier ID
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            supplierID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Supplier ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return supplierID;
        }

        private int GetCategoryIDByName(string categoryName)
        {
            int categoryID = -1; // Default value for error handling

            try
            {
                // Construct SQL SELECT statement to retrieve Category ID by name
                string query = "SELECT Category_ID FROM Category WHERE Name = @Name";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the SELECT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", categoryName);

                        // Execute the command and retrieve the Category ID
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            categoryID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Category ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return categoryID;
        }

        //Edit
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve data entered by the user
                string supplierName = comboBox_Supplier.SelectedItem?.ToString();
                string categoryName = comboBox_Category.SelectedItem?.ToString();
                string name = textBox_Name.Text;
                decimal costPrice;
                decimal sellingPrice;
                decimal weight;
                string unit = textBox_Unit.Text.Trim();
                int stock;

                // Validate input data
                if (string.IsNullOrEmpty(supplierName) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(name) ||
                    !decimal.TryParse(textBox_CostPrice.Text, out costPrice) || !decimal.TryParse(textBox_SellingPrice.Text, out sellingPrice) ||
                    !decimal.TryParse(textBox_Weight.Text, out weight) || !int.TryParse(textBox_Stock.Text, out stock))
                {
                    MessageBox.Show("Please enter all required fields with valid values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Additional validation for name and string fields
                if (name.Any(char.IsDigit))
                {
                    MessageBox.Show("Product name cannot contain digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!string.IsNullOrEmpty(unit) && unit.Any(char.IsDigit))
                {
                    MessageBox.Show("Unit cannot contain digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate cost price, selling price, and weight to ensure they are greater than zero
                if (costPrice <= 0 || sellingPrice <= 0 || weight <= 0 || stock <= 0)
                {
                    MessageBox.Show("Cost price, selling price, weight, and stock must be greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the product already exists in the database based on name, supplier, and category
                string checkQuery = "SELECT COUNT(*) FROM Product p " +
                                    "INNER JOIN Supplier s ON p.Supplier_ID = s.Supplier_ID " +
                                    "INNER JOIN Category c ON p.Category_ID = c.Category_ID " +
                                    "WHERE p.Name = @Name AND s.Name = @SupplierName AND c.Name = @CategoryName";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command to check for existing product
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        // Add parameters to the command
                        checkCommand.Parameters.AddWithValue("@Name", name);
                        checkCommand.Parameters.AddWithValue("@SupplierName", supplierName);
                        checkCommand.Parameters.AddWithValue("@CategoryName", categoryName);

                        // Execute the command to check if the product exists
                        int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If the product already exists, show an error message and return
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Product with the same name, supplier, and category already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Retrieve Supplier ID based on selected name
                    int supplierID = GetSupplierIDByName(supplierName);

                    // Retrieve Category ID based on selected name
                    int categoryID = GetCategoryIDByName(categoryName);

                    // Construct SQL INSERT statement
                    string query = "INSERT INTO Product (Supplier_ID, Category_ID, Name, Cost_Price, Selling_Price, Weight, Unit, Stock) VALUES (@SupplierID, @CategoryID, @Name, @CostPrice, @SellingPrice, @Weight, @Unit, @Stock)";

                    // Create a command with the INSERT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@SupplierID", supplierID);
                        command.Parameters.AddWithValue("@CategoryID", categoryID);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@CostPrice", costPrice);
                        command.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                        command.Parameters.AddWithValue("@Weight", weight);
                        command.Parameters.AddWithValue("@Unit", unit);
                        command.Parameters.AddWithValue("@Stock", stock);

                        // Execute the INSERT command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Delete
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
                int productID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Product_ID"].Value);

                // Construct SQL DELETE statement
                string query = "DELETE FROM Product WHERE Product_ID = @ProductID";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the DELETE statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@ProductID", productID);

                        // Execute the DELETE command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the deletion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the DataGridView to display the updated data
                            LoadDataIntoDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Search
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Construct SQL SELECT statement with a WHERE clause to filter records based on the search criteria
                string query = "SELECT Product.Product_ID, Supplier.Name AS Supplier, Category.Name AS Category, Product.Name, Product.Cost_Price, Product.Selling_Price, Product.Weight, Product.Unit, Product.Stock " +
                               "FROM Product " +
                               "JOIN Supplier ON Product.Supplier_ID = Supplier.Supplier_ID " +
                               "JOIN Category ON Product.Category_ID = Category.Category_ID " +
                               "WHERE ";

                // Add search conditions for each present text box and combo box
                List<string> searchConditions = new List<string>();

                // Check if combo boxes have items selected and add corresponding search conditions
                if (comboBox_Supplier.SelectedIndex != -1)
                {
                    // Retrieve supplier ID based on the selected supplier name
                    int supplierID = GetSupplierIDByName(comboBox_Supplier.SelectedItem.ToString());
                    if (supplierID != -1)
                    {
                        searchConditions.Add("Product.Supplier_ID = @SupplierID");
                    }
                }

                if (comboBox_Category.SelectedIndex != -1)
                {
                    // Retrieve category ID based on the selected category name
                    int categoryID = GetCategoryIDByName(comboBox_Category.SelectedItem.ToString());
                    if (categoryID != -1)
                    {
                        searchConditions.Add("Product.Category_ID = @CategoryID");
                    }
                }

                // Check if text boxes have text entered and add corresponding search conditions
                if (!string.IsNullOrEmpty(textBox_Name.Text))
                {
                    searchConditions.Add("Product.Name LIKE @Name");
                }

                if (!string.IsNullOrEmpty(textBox_CostPrice.Text))
                {
                    searchConditions.Add("Product.Cost_Price LIKE @CostPrice");
                }

                if (!string.IsNullOrEmpty(textBox_Weight.Text))
                {
                    searchConditions.Add("Product.Weight LIKE @Weight");
                }

                if (!string.IsNullOrEmpty(textBox_Unit.Text))
                {
                    searchConditions.Add("Product.Unit LIKE @Unit");
                }

                if (!string.IsNullOrEmpty(textBox_Stock.Text))
                {
                    searchConditions.Add("Product.Stock LIKE @Stock");
                }

                // If no search conditions were added, show all records
                if (searchConditions.Count == 0)
                {
                    query += "1"; // Always true condition
                }
                else
                {
                    // Combine search conditions with AND operator
                    query += string.Join(" AND ", searchConditions);
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
                        if (comboBox_Supplier.SelectedIndex != -1)
                        {
                            int supplierID = GetSupplierIDByName(comboBox_Supplier.SelectedItem.ToString());
                            if (supplierID != -1)
                            {
                                command.Parameters.AddWithValue("@SupplierID", supplierID);
                            }
                        }

                        if (comboBox_Category.SelectedIndex != -1)
                        {
                            int categoryID = GetCategoryIDByName(comboBox_Category.SelectedItem.ToString());
                            if (categoryID != -1)
                            {
                                command.Parameters.AddWithValue("@CategoryID", categoryID);
                            }
                        }

                        if (!string.IsNullOrEmpty(textBox_Name.Text))
                        {
                            command.Parameters.AddWithValue("@Name", "%" + textBox_Name.Text.Trim() + "%");
                        }

                        if (!string.IsNullOrEmpty(textBox_CostPrice.Text))
                        {
                            command.Parameters.AddWithValue("@CostPrice", "%" + textBox_CostPrice.Text.Trim() + "%");
                        }

                        if (!string.IsNullOrEmpty(textBox_Weight.Text))
                        {
                            command.Parameters.AddWithValue("@Weight", "%" + textBox_Weight.Text.Trim() + "%");
                        }

                        if (!string.IsNullOrEmpty(textBox_Unit.Text))
                        {
                            command.Parameters.AddWithValue("@Unit", "%" + textBox_Unit.Text.Trim() + "%");
                        }

                        if (!string.IsNullOrEmpty(textBox_Stock.Text))
                        {
                            command.Parameters.AddWithValue("@Stock", "%" + textBox_Stock.Text.Trim() + "%");
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
                MessageBox.Show("Error searching for products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked cell is in the "Product_ID" column
                if (e.ColumnIndex == dataGridView1.Columns["Product_ID"].Index && e.RowIndex >= 0)
                {
                    // Retrieve data from the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Retrieve the IDs for supplier and category
                    int supplierID = Convert.ToInt32(selectedRow.Cells["Supplier_ID"].Value);
                    int categoryID = Convert.ToInt32(selectedRow.Cells["Category_ID"].Value);

                    // Perform a lookup to get the supplier and category names based on the IDs
                    string supplierName = GetSupplierNameByID(supplierID); 
                    string categoryName = GetCategoryNameByID(categoryID); 

                    // Set the selected items in the combo boxes
                    comboBox_Supplier.SelectedItem = supplierName;
                    comboBox_Category.SelectedItem = categoryName;

                    // Retrieve and display data in the text boxes
                    textBox_ProductID.Text = selectedRow.Cells["Product_ID"].Value.ToString();
                    textBox_Name.Text = selectedRow.Cells["Name"].Value.ToString();
                    textBox_CostPrice.Text = selectedRow.Cells["Cost_Price"].Value.ToString(); 
                    textBox_SellingPrice.Text = selectedRow.Cells["Selling_Price"].Value.ToString(); 
                    textBox_Weight.Text = selectedRow.Cells["Weight"].Value.ToString();
                    textBox_Unit.Text = selectedRow.Cells["Unit"].Value.ToString(); 
                    textBox_Stock.Text = selectedRow.Cells["Stock"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Method to get supplier name by ID
        private string GetSupplierNameByID(int supplierID)
        {
            string supplierName = string.Empty;
            try
            {
                // Construct SQL query to retrieve supplier name by ID
                string query = "SELECT Name FROM Supplier WHERE Supplier_ID = @SupplierID";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the SELECT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@SupplierID", supplierID);

                        // Execute the command and retrieve the supplier name
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            supplierName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving supplier name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return supplierName;
        }

        // Method to get category name by ID
        private string GetCategoryNameByID(int categoryID)
        {
            string categoryName = string.Empty;
            try
            {
                // Construct SQL query to retrieve category name by ID
                string query = "SELECT Name FROM Category WHERE Category_ID = @CategoryID";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the SELECT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command to prevent SQL injection
                        command.Parameters.AddWithValue("@CategoryID", categoryID);

                        // Execute the command and retrieve the category name
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            categoryName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving category name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return categoryName;
        }


        private void button_Back_Click(object sender, EventArgs e)
        {
            AdminDashboard AdminDashboard = new AdminDashboard(loggedInAdminID);
            AdminDashboard.Show();
            this.Hide();
        }

        private void textBox_Stock_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
