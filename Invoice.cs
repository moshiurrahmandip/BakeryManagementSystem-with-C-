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
    public partial class Invoice : Form
    {
        // Connection Established
        private string connectionString = "server=127.0.0.1;database=bms;username=root;password=";
        private int loggedInEmployeeID = -1;

        public Invoice(int loggedInEmployeeID)
        {
            InitializeComponent();
            this.loggedInEmployeeID = loggedInEmployeeID;
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            textBox_CashProvided.KeyPress += textBox_CashProvided_KeyPress;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            PopulateCategoryComboBox();
            comboBox_Category.SelectedIndexChanged += comboBox_Category_SelectedIndexChanged;
        }

        // Search For Customer
        private void button_CustomerSearch_Click(object sender, EventArgs e)
        {
            string phoneNumber = textBox_CustomerPhoneNumber.Text.Trim();

            // Query the Customer table
            string query = "SELECT * FROM Customer WHERE Phone_Number = @PhoneNumber";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Customer found, populate text boxes
                        textBox_CustomerName.Text = reader["Name"].ToString();
                        textBox_CustomerPhoneNumber.Text = reader["Phone_Number"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Customer not listed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }
            }
        }

        // Add New Customer
        private void button_CustomerAdd_Click(object sender, EventArgs e)
        {
            string name = textBox_CustomerName.Text.Trim();
            string phoneNumber = textBox_CustomerPhoneNumber.Text.Trim();

            // Insert new customer into the Customer table
            string query = "INSERT INTO Customer (Name, Phone_Number) VALUES (@Name, @PhoneNumber)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Customer buying functions start --->

        private void PopulateCategoryComboBox()
        {
            try
            {
                // Construct SQL query to retrieve category names
                string query = "SELECT Name FROM Category";

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command with the SELECT statement
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the command and retrieve the data using a DataReader
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Clear existing items in the combo box
                            comboBox_Category.Items.Clear();

                            // Iterate through the results and add category names to the combo box
                            while (reader.Read())
                            {
                                // Get the category name from the current row
                                string categoryName = reader.GetString("Name");

                                // Add the category name to the combo box
                                comboBox_Category.Items.Add(categoryName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving category names: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateProductComboBox(int categoryID)
        {
            comboBox_ProductName.Items.Clear(); // Clear existing items

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Name FROM Product WHERE Category_ID = @CategoryID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryID", categoryID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string productName = reader["Name"].ToString();
                                comboBox_ProductName.Items.Add(productName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating product ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetCategoryID(string categoryName)
        {
            int categoryID = -1; // Default value

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Category_ID FROM Category WHERE Name = @CategoryName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryName", categoryName);
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
                MessageBox.Show("Error retrieving category ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return categoryID;
        }

        private void comboBox_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryName = comboBox_Category.SelectedItem.ToString();

            // Get the category ID associated with the selected category name
            int categoryID = GetCategoryID(categoryName);

            // Populate the product combobox with product names associated with the selected category
            PopulateProductComboBox(categoryID);
        }


        // Populate Boxes based on selected item in product name
        private void comboBox_ProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle the selection change event for the product ComboBox
            // Get the selected product name
            string productName = comboBox_ProductName.SelectedItem.ToString();

            // Populate text boxes with product information
            PopulateProductInformation(productName);
        }

        private void PopulateProductInformation(string productName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Selling_Price, Weight, Unit, Stock FROM Product WHERE Name = @ProductName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", productName);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate text boxes with product information
                                textBox_SellingPrice.Text = reader["Selling_Price"].ToString();
                                textBox_Weight.Text = reader["Weight"].ToString();
                                textBox_Unit.Text = reader["Unit"].ToString();
                                textBox_Stock.Text = reader["Stock"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving product information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the "Buying Quantity" column
            if (e.ColumnIndex == dataGridView1.Columns["BuyingQuantity"].Index && e.RowIndex >= 0)
            {
                // Get the product name from the clicked row
                string productName = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();

                // Populate ComboBox and TextBox with the corresponding product information
                comboBox_ProductName.SelectedItem = productName; // Select the product in the ComboBox
                PopulateProductInformation(productName); // Populate other text boxes with product information
            }

        }

        private void button_InvoiceAdd_Click(object sender, EventArgs e)
        {
            // Retrieve values from ComboBoxes and TextBoxes
            string productName = comboBox_ProductName.SelectedItem?.ToString();
            string sellingPrice = textBox_SellingPrice.Text;
            string weight = textBox_Weight.Text;
            string unit = textBox_Unit.Text;
            string stock = textBox_Stock.Text;

            // Retrieve buying quantity
            string quantity = textBox_BuyingQuantity.Text;

            // Validate input
            if (productName == null)
            {
                MessageBox.Show("Please select a product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("Please enter a buying quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(quantity, out int buyingQuantity) || buyingQuantity <= 0)
            {
                MessageBox.Show("Please enter a valid buying quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the buying quantity exceeds available stock
            int availableStock = Convert.ToInt32(stock);
            if (buyingQuantity > availableStock)
            {
                MessageBox.Show("Buying quantity cannot exceed available stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the product is already added to the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ProductName"].Value != null && row.Cells["ProductName"].Value.ToString() == productName)
                {
                    MessageBox.Show("Product already added to the invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Add columns to the DataGridView if they haven't been added yet
            if (dataGridView1.ColumnCount == 0)
            {
                dataGridView1.Columns.Add("ProductName", "Product Name");
                dataGridView1.Columns.Add("SellingPrice", "Selling Price");
                dataGridView1.Columns.Add("Weight", "Weight");
                dataGridView1.Columns.Add("Unit", "Unit");
                dataGridView1.Columns.Add("Stock", "Stock");
                dataGridView1.Columns.Add("BuyingQuantity", "Buying Quantity");
            }

            // Add the values to the DataGridView as a new row
            dataGridView1.Rows.Add(productName, sellingPrice, weight, unit, stock, buyingQuantity);

            CalculateTotalAmount();


        }


        private void button_Update_Click(object sender, EventArgs e)
        {
            // Retrieve values from ComboBoxes and TextBoxes
            string productName = comboBox_ProductName.SelectedItem?.ToString();
            string sellingPrice = textBox_SellingPrice.Text;
            string weight = textBox_Weight.Text;
            string unit = textBox_Unit.Text;
            string stock = textBox_Stock.Text;

            // Retrieve buying quantity
            string quantity = textBox_BuyingQuantity.Text;

            // Validate input
            if (productName == null)
            {
                MessageBox.Show("Please select a product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("Please enter a buying quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(quantity, out int buyingQuantity) || buyingQuantity <= 0)
            {
                MessageBox.Show("Please enter a valid buying quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the buying quantity exceeds available stock
            int availableStock = Convert.ToInt32(stock);
            if (buyingQuantity > availableStock)
            {
                MessageBox.Show("Buying quantity cannot exceed available stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the product is already added to the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ProductName"].Value != null && row.Cells["ProductName"].Value.ToString() == productName)
                {
                    MessageBox.Show("Product already updated to the invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Add columns to the DataGridView if they haven't been added yet
            if (dataGridView1.ColumnCount == 0)
            {
                dataGridView1.Columns.Add("ProductName", "Product Name");
                dataGridView1.Columns.Add("SellingPrice", "Selling Price");
                dataGridView1.Columns.Add("Weight", "Weight");
                dataGridView1.Columns.Add("Unit", "Unit");
                dataGridView1.Columns.Add("Stock", "Stock");
                dataGridView1.Columns.Add("BuyingQuantity", "Buying Quantity");
            }

            // Add the values to the DataGridView as a new row
            dataGridView1.Rows.Add(productName, sellingPrice, weight, unit, stock, buyingQuantity);

            CalculateTotalAmount();

        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            // Check if there is a selected row
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the index of the selected row
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Remove the selected row from the DataGridView
                dataGridView1.Rows.RemoveAt(selectedIndex);
                CalculateTotalAmount();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CalculateTotalAmount()
        {
            decimal totalAmount = 0;

            // Loop through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check if the row is not null and is not a new row
                if (row != null && !row.IsNewRow)
                {
                    // Retrieve selling price and buying quantity from the row
                    DataGridViewCell sellingPriceCell = row.Cells["SellingPrice"];
                    DataGridViewCell quantityCell = row.Cells["BuyingQuantity"];

                    // Check if the cells are not null and contain values
                    if (sellingPriceCell != null && sellingPriceCell.Value != null &&
                        quantityCell != null && quantityCell.Value != null)
                    {
                        // Retrieve the values as strings
                        string sellingPriceStr = sellingPriceCell.Value.ToString();
                        string quantityStr = quantityCell.Value.ToString();

                        // Convert selling price and buying quantity to decimal
                        if (decimal.TryParse(sellingPriceStr, out decimal sellingPrice) && decimal.TryParse(quantityStr, out decimal quantity))
                        {
                            // Calculate total price for the row and add it to the total amount
                            totalAmount += sellingPrice * quantity;
                        }
                    }
                }
            }

            // Display the total amount in the Total Amount textbox
            textBox_TotalAmount.Text = totalAmount.ToString();
        }


        private void textBox_CashProvided_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, decimal point, Backspace, and Enter
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            // If Enter key is pressed
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Convert the text in Cash Provided textbox to decimal
                if (decimal.TryParse(textBox_CashProvided.Text, out decimal cashProvided))
                {
                    // Get the total amount from the Total Amount textbox
                    if (decimal.TryParse(textBox_TotalAmount.Text, out decimal totalAmount))
                    {
                        // Check if cash provided is less than total amount
                        if (cashProvided < totalAmount)
                        {
                            MessageBox.Show("Cash provided cannot be less than the total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox_CashProvided.Clear(); // Clear the textbox
                            return;
                        }

                        // Calculate cash return and display it in the Cash Return textbox
                        decimal cashReturn = cashProvided - totalAmount;
                        textBox_CashReturn.Text = cashReturn.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Invalid total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid cash provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void CalculateCashReturn()
        {
            // Retrieve the values from the Cash Provided and Total Amount textboxes
            if (decimal.TryParse(textBox_CashProvided.Text, out decimal cashProvided) && decimal.TryParse(textBox_TotalAmount.Text, out decimal totalAmount))
            {
                // Calculate cash return
                decimal cashReturn = cashProvided - totalAmount;

                // Display the cash return in the Cash Return textbox
                textBox_CashReturn.Text = cashReturn.ToString();
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values for Cash Provided and Total Amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_ConfirmPurchase_Click(object sender, EventArgs e)
        {
            // Retrieve data from textboxes
            string customerPhoneNumber = textBox_CustomerPhoneNumber.Text.Trim();
            string productName = comboBox_ProductName.SelectedItem?.ToString();
            string sellingPrice = textBox_SellingPrice.Text;
            string weight = textBox_Weight.Text;
            string unit = textBox_Unit.Text;
            string stock = textBox_Stock.Text;
            string quantity = textBox_BuyingQuantity.Text;
            string totalAmount = textBox_TotalAmount.Text;
            string paymentMethod = comboBox_PaymentMethod.SelectedItem?.ToString();

            // Validate data
            if (string.IsNullOrEmpty(customerPhoneNumber) || string.IsNullOrEmpty(productName) ||
                string.IsNullOrEmpty(sellingPrice) || string.IsNullOrEmpty(weight) ||
                string.IsNullOrEmpty(unit) || string.IsNullOrEmpty(stock) ||
                string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(totalAmount) ||
                string.IsNullOrEmpty(paymentMethod))
            {
                MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the buying quantity exceeds available stock
            int availableStock = Convert.ToInt32(stock);
            if (!int.TryParse(quantity, out int buyingQuantity) || buyingQuantity <= 0 || buyingQuantity > availableStock)
            {
                MessageBox.Show("Invalid buying quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the product ID
            int productID = GetProductID(productName);

            // Get the cost price of the product
            decimal costPrice = GetCostPrice(productID);

            // Insert data into the Invoice table
            string query = @"INSERT INTO Invoice (Customer_ID, Employee_ID, Product_ID, `Buying Quantity`, Total_Amount, Date, PaymentMethod, Profit) 
             VALUES (@CustomerID, @EmployeeID, @ProductID, @BuyingQuantity, @TotalAmount, @Date, @PaymentMethod, @Profit)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Set parameter values
                    command.Parameters.AddWithValue("@CustomerID", GetCustomerID(customerPhoneNumber));
                    command.Parameters.AddWithValue("@EmployeeID", loggedInEmployeeID);
                    command.Parameters.AddWithValue("@ProductID", productID);
                    command.Parameters.AddWithValue("@BuyingQuantity", buyingQuantity);
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    command.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                    command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    command.Parameters.AddWithValue("@Profit", CalculateProfit(Convert.ToDecimal(sellingPrice), costPrice, buyingQuantity));

                    // Open connection and execute command
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update stock quantity
                            UpdateStock(productID, buyingQuantity);

                            MessageBox.Show("Purchase confirmed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            resetForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to confirm purchase.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private int GetCustomerID(string phoneNumber)
        {
            int customerID = -1;

            string query = "SELECT Customer_ID FROM Customer WHERE Phone_Number = @PhoneNumber";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            customerID = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving customer ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return customerID;
        }

        private int GetProductID(string productName)
        {
            int productID = -1;

            string query = "SELECT Product_ID FROM Product WHERE Name = @ProductName";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            productID = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving product ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return productID;
        }

        private decimal CalculateProfit(decimal sellingPrice, decimal costPrice, int quantity)
        {
            // Calculate the total revenue from selling the products
            decimal totalRevenue = sellingPrice * quantity;

            // Calculate the total cost of purchasing the products
            decimal totalCost = costPrice * quantity;

            // Calculate the profit as the difference between total revenue and total cost
            decimal profit = totalRevenue - totalCost;

            return profit;
        }

        private decimal GetCostPrice(int productID)
        {
            decimal costPrice = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Cost_Price FROM Product WHERE Product_ID = @ProductID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productID);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            costPrice = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving cost price: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return costPrice;
        }

        private void UpdateStock(int productID, int quantity)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Product SET Stock = Stock - @Quantity WHERE Product_ID = @ProductID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@ProductID", productID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_NewInvoice_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void resetForm()
        {
            Invoice invoice = new Invoice(loggedInEmployeeID);
            invoice.Show();
            this.Hide();
        }

    }
}
