using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BakeryManagementSystem
{
    public partial class AdminDashboard : Form
    {
        private string connectionString = "server=127.0.0.1;database=bms;username=root;password=";

        private int loggedInAdminID = -1;

        public AdminDashboard(int loggedInAdminID)
        {
            InitializeComponent();
            this.loggedInAdminID = loggedInAdminID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee EmployeeForm= new Employee(loggedInAdminID);
            EmployeeForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Supplier SupplierForm = new Supplier(loggedInAdminID);
            SupplierForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Category CategoryForm = new Category(loggedInAdminID);
            CategoryForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProductForm ProductForm= new ProductForm(loggedInAdminID);
            ProductForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Customer customerForm = new Customer(loggedInAdminID);
            customerForm.Show();
            this.Hide();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Login LoginForm = new Login();
            LoginForm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_GenerateDailyReport_Click(object sender, EventArgs e)
        {
            GenerateDailyReport();
        }

        private void GenerateDailyReport()
        {
            try
            {
                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Construct SQL SELECT statement with JOIN operations to retrieve data from the Invoice table
                    string query = @"SELECT i.Invoice_ID, i.Product_ID, c.Name AS CustomerName, e.Name AS EmployeeName, p.Name AS ProductName, 
                                            i.`Buying Quantity`, i.Total_Amount, i.PaymentMethod, i.Date 
                                     FROM Invoice i 
                                     JOIN Customer c ON i.Customer_ID = c.Customer_ID 
                                     JOIN Employee e ON i.Employee_ID = e.Employee_ID 
                                     JOIN Product p ON i.Product_ID = p.Product_ID 
                                     WHERE DATE(i.Date) = CURDATE()
                                     ";

                    // Create a DataAdapter to fill a DataTable with the results of the query
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                    {
                        // Create a new DataTable
                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with the results of the query
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }

                    UpdateTotalProductsSold();
                    UpdateTotalSales();
                    UpdateProfit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating daily report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_GenerateReport_Click(object sender, EventArgs e)
        {
            GenerateCustomReport();
        }

        private void GenerateCustomReport()
        {
            try
            {
                // Clear the DataGridView before populating it with new data
                dataGridView1.DataSource = null;

                // Get the selected date range from the DateTimePicker
                DateTime startDate = dateTimePicker1.Value.Date;
                DateTime endDate = dateTimePicker2.Value.Date;

                // Create a connection to the database
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Construct SQL SELECT statement with JOIN operations to retrieve data from the Invoice table for the selected date range
                    string query = @"SELECT i.Invoice_ID, i.Product_ID, c.Name AS CustomerName, e.Name AS EmployeeName, p.Name AS ProductName, 
                                    i.`Buying Quantity`, i.Total_Amount, i.PaymentMethod, i.Date 
                             FROM Invoice i 
                             JOIN Customer c ON i.Customer_ID = c.Customer_ID 
                             JOIN Employee e ON i.Employee_ID = e.Employee_ID 
                             JOIN Product p ON i.Product_ID = p.Product_ID 
                             WHERE DATE(i.Date) BETWEEN @StartDate AND @EndDate";

                    // Create a MySqlCommand object
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters for the start date and end date
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

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

                    UpdateTotalProductsSold();
                    UpdateTotalSales();
                    UpdateProfit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating custom report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void UpdateTotalProductsSold()
        {
            int totalProductsSold = 0;

            // Loop through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check if the row is not null and is not a new row
                if (row != null && !row.IsNewRow)
                {
                    // Retrieve the buying quantity from the row and add it to the total
                    if (row.Cells["Buying Quantity"].Value != null)
                    {
                        totalProductsSold += Convert.ToInt32(row.Cells["Buying Quantity"].Value);
                    }
                }
            }

            // Display the total number of buying products in the textBox
            textBox_TotalProductsSold.Text = totalProductsSold.ToString();
        }

        private decimal GetSellingPrice(int productID)
        {
            decimal sellingPrice = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Selling_Price FROM Product WHERE Product_ID = @ProductID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productID);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            sellingPrice = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving selling price: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sellingPrice;
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

        private void UpdateTotalSales()
        {
            decimal totalSales = 0;

            // Loop through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check if the row is not null and is not a new row
                if (row != null && !row.IsNewRow)
                {
                    // Retrieve the buying quantity and selling price from the row
                    if (row.Cells["Buying Quantity"].Value != null && row.Cells["Product_ID"].Value != null)
                    {
                        int buyingQuantity = Convert.ToInt32(row.Cells["Buying Quantity"].Value);
                        int productID = Convert.ToInt32(row.Cells["Product_ID"].Value);

                        // Retrieve the selling price for the product
                        decimal sellingPrice = GetSellingPrice(productID);

                        // Calculate the total sales for this row
                        decimal rowTotalSales = buyingQuantity * sellingPrice;

                        // Add the row total sales to the overall total
                        totalSales += rowTotalSales;
                    }
                }
            }

            // Display the total sales in the Total Sales textbox
            textBox_TotalSales.Text = totalSales.ToString();
        }

        private void UpdateProfit()
        {
            decimal totalProfit = 0;

            // Loop through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check if the row is not null and is not a new row
                if (row != null && !row.IsNewRow)
                {
                    // Retrieve the product ID and buying quantity from the row
                    if (row.Cells["Product_ID"].Value != null && row.Cells["Buying Quantity"].Value != null)
                    {
                        int productID = Convert.ToInt32(row.Cells["Product_ID"].Value);
                        int buyingQuantity = Convert.ToInt32(row.Cells["Buying Quantity"].Value);

                        // Retrieve the selling price for the product
                        decimal sellingPrice = GetSellingPrice(productID);

                        // Calculate the profit for this row
                        decimal profit = sellingPrice - GetCostPrice(productID); // Assuming GetCostPrice retrieves cost price

                        // Add the profit for this row to the total profit
                        totalProfit += profit * buyingQuantity;
                    }
                }
            }

            // Display the total profit in the Profit textbox
            textBox_profit.Text = totalProfit.ToString();
        }

    }
}
