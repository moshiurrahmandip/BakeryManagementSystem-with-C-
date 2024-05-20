namespace BakeryManagementSystem
{
    partial class AdminDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_employee = new System.Windows.Forms.Button();
            this.button_supplier = new System.Windows.Forms.Button();
            this.button_category = new System.Windows.Forms.Button();
            this.button_product = new System.Windows.Forms.Button();
            this.button_customer = new System.Windows.Forms.Button();
            this.button_Logout = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_TotalProductsSold = new System.Windows.Forms.TextBox();
            this.textBox_TotalSales = new System.Windows.Forms.TextBox();
            this.label_TotalSales = new System.Windows.Forms.Label();
            this.textBox_profit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_employee
            // 
            this.button_employee.BackColor = System.Drawing.Color.GreenYellow;
            this.button_employee.Location = new System.Drawing.Point(12, 12);
            this.button_employee.Name = "button_employee";
            this.button_employee.Size = new System.Drawing.Size(122, 34);
            this.button_employee.TabIndex = 0;
            this.button_employee.Text = "Employee";
            this.button_employee.UseVisualStyleBackColor = false;
            this.button_employee.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_supplier
            // 
            this.button_supplier.BackColor = System.Drawing.Color.PeachPuff;
            this.button_supplier.Location = new System.Drawing.Point(140, 12);
            this.button_supplier.Name = "button_supplier";
            this.button_supplier.Size = new System.Drawing.Size(122, 34);
            this.button_supplier.TabIndex = 1;
            this.button_supplier.Text = "Supplier";
            this.button_supplier.UseVisualStyleBackColor = false;
            this.button_supplier.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_category
            // 
            this.button_category.BackColor = System.Drawing.Color.Orange;
            this.button_category.Location = new System.Drawing.Point(268, 12);
            this.button_category.Name = "button_category";
            this.button_category.Size = new System.Drawing.Size(110, 34);
            this.button_category.TabIndex = 2;
            this.button_category.Text = "Category";
            this.button_category.UseVisualStyleBackColor = false;
            this.button_category.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_product
            // 
            this.button_product.BackColor = System.Drawing.Color.Chocolate;
            this.button_product.Location = new System.Drawing.Point(12, 69);
            this.button_product.Name = "button_product";
            this.button_product.Size = new System.Drawing.Size(122, 36);
            this.button_product.TabIndex = 3;
            this.button_product.Text = "Product";
            this.button_product.UseVisualStyleBackColor = false;
            this.button_product.Click += new System.EventHandler(this.button4_Click);
            // 
            // button_customer
            // 
            this.button_customer.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button_customer.Location = new System.Drawing.Point(140, 69);
            this.button_customer.Name = "button_customer";
            this.button_customer.Size = new System.Drawing.Size(122, 36);
            this.button_customer.TabIndex = 4;
            this.button_customer.Text = "Customer";
            this.button_customer.UseVisualStyleBackColor = false;
            this.button_customer.Click += new System.EventHandler(this.button5_Click);
            // 
            // button_Logout
            // 
            this.button_Logout.BackColor = System.Drawing.Color.Red;
            this.button_Logout.Location = new System.Drawing.Point(939, 2);
            this.button_Logout.Name = "button_Logout";
            this.button_Logout.Size = new System.Drawing.Size(75, 44);
            this.button_Logout.TabIndex = 6;
            this.button_Logout.Text = "Logout";
            this.button_Logout.UseVisualStyleBackColor = false;
            this.button_Logout.Click += new System.EventHandler(this.button_back_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 201);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1002, 231);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(756, 97);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 21;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.button3.Location = new System.Drawing.Point(789, 151);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 44);
            this.button3.TabIndex = 18;
            this.button3.Text = "Generate Report";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button_GenerateReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(808, 453);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Total Products Sold";
            // 
            // textBox_TotalProductsSold
            // 
            this.textBox_TotalProductsSold.Location = new System.Drawing.Point(914, 450);
            this.textBox_TotalProductsSold.Name = "textBox_TotalProductsSold";
            this.textBox_TotalProductsSold.Size = new System.Drawing.Size(100, 20);
            this.textBox_TotalProductsSold.TabIndex = 23;
            // 
            // textBox_TotalSales
            // 
            this.textBox_TotalSales.Location = new System.Drawing.Point(914, 479);
            this.textBox_TotalSales.Name = "textBox_TotalSales";
            this.textBox_TotalSales.Size = new System.Drawing.Size(100, 20);
            this.textBox_TotalSales.TabIndex = 25;
            // 
            // label_TotalSales
            // 
            this.label_TotalSales.AutoSize = true;
            this.label_TotalSales.Location = new System.Drawing.Point(808, 482);
            this.label_TotalSales.Name = "label_TotalSales";
            this.label_TotalSales.Size = new System.Drawing.Size(60, 13);
            this.label_TotalSales.TabIndex = 24;
            this.label_TotalSales.Text = "Total Sales";
            // 
            // textBox_profit
            // 
            this.textBox_profit.Location = new System.Drawing.Point(914, 508);
            this.textBox_profit.Name = "textBox_profit";
            this.textBox_profit.Size = new System.Drawing.Size(100, 20);
            this.textBox_profit.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(808, 511);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Total Profit";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(756, 123);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(695, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Start Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(698, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "End Date";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MistyRose;
            this.button1.Location = new System.Drawing.Point(789, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 44);
            this.button1.TabIndex = 17;
            this.button1.Text = "Generate Daily Report";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button_GenerateDailyReport_Click);
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1026, 599);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.textBox_profit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_TotalSales);
            this.Controls.Add(this.label_TotalSales);
            this.Controls.Add(this.textBox_TotalProductsSold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_Logout);
            this.Controls.Add(this.button_customer);
            this.Controls.Add(this.button_product);
            this.Controls.Add(this.button_category);
            this.Controls.Add(this.button_supplier);
            this.Controls.Add(this.button_employee);
            this.Name = "AdminDashboard";
            this.Text = "Admin Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_employee;
        private System.Windows.Forms.Button button_supplier;
        private System.Windows.Forms.Button button_category;
        private System.Windows.Forms.Button button_product;
        private System.Windows.Forms.Button button_customer;
        private System.Windows.Forms.Button button_Logout;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_TotalProductsSold;
        private System.Windows.Forms.TextBox textBox_TotalSales;
        private System.Windows.Forms.Label label_TotalSales;
        private System.Windows.Forms.TextBox textBox_profit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}