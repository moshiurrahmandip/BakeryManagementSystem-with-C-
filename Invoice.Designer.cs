namespace BakeryManagementSystem
{
    partial class Invoice
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_CustomerName = new System.Windows.Forms.TextBox();
            this.button_CustomerSearch = new System.Windows.Forms.Button();
            this.textBox_CustomerPhoneNumber = new System.Windows.Forms.TextBox();
            this.label_PhoneNumber = new System.Windows.Forms.Label();
            this.button_CustomerAdd = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button_ConfirmPurchase = new System.Windows.Forms.Button();
            this.button_NewInvoice = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Category = new System.Windows.Forms.ComboBox();
            this.comboBox_ProductName = new System.Windows.Forms.ComboBox();
            this.textBox_SellingPrice = new System.Windows.Forms.TextBox();
            this.textBox_Weight = new System.Windows.Forms.TextBox();
            this.textBox_Unit = new System.Windows.Forms.TextBox();
            this.textBox_Stock = new System.Windows.Forms.TextBox();
            this.textBox_BuyingQuantity = new System.Windows.Forms.TextBox();
            this.textBox_TotalAmount = new System.Windows.Forms.TextBox();
            this.textBox_CashProvided = new System.Windows.Forms.TextBox();
            this.textBox_CashReturn = new System.Windows.Forms.TextBox();
            this.button_InvoiceAdd = new System.Windows.Forms.Button();
            this.button_Remove = new System.Windows.Forms.Button();
            this.button_Update = new System.Windows.Forms.Button();
            this.comboBox_PaymentMethod = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Name";
            // 
            // textBox_CustomerName
            // 
            this.textBox_CustomerName.Location = new System.Drawing.Point(12, 28);
            this.textBox_CustomerName.Name = "textBox_CustomerName";
            this.textBox_CustomerName.Size = new System.Drawing.Size(100, 20);
            this.textBox_CustomerName.TabIndex = 1;
            // 
            // button_CustomerSearch
            // 
            this.button_CustomerSearch.BackColor = System.Drawing.Color.LavenderBlush;
            this.button_CustomerSearch.Location = new System.Drawing.Point(224, 25);
            this.button_CustomerSearch.Name = "button_CustomerSearch";
            this.button_CustomerSearch.Size = new System.Drawing.Size(75, 41);
            this.button_CustomerSearch.TabIndex = 2;
            this.button_CustomerSearch.Text = "Search";
            this.button_CustomerSearch.UseVisualStyleBackColor = false;
            this.button_CustomerSearch.Click += new System.EventHandler(this.button_CustomerSearch_Click);
            // 
            // textBox_CustomerPhoneNumber
            // 
            this.textBox_CustomerPhoneNumber.Location = new System.Drawing.Point(118, 28);
            this.textBox_CustomerPhoneNumber.Name = "textBox_CustomerPhoneNumber";
            this.textBox_CustomerPhoneNumber.Size = new System.Drawing.Size(100, 20);
            this.textBox_CustomerPhoneNumber.TabIndex = 4;
            // 
            // label_PhoneNumber
            // 
            this.label_PhoneNumber.AutoSize = true;
            this.label_PhoneNumber.Location = new System.Drawing.Point(118, 12);
            this.label_PhoneNumber.Name = "label_PhoneNumber";
            this.label_PhoneNumber.Size = new System.Drawing.Size(78, 13);
            this.label_PhoneNumber.TabIndex = 3;
            this.label_PhoneNumber.Text = "Phone Number";
            // 
            // button_CustomerAdd
            // 
            this.button_CustomerAdd.BackColor = System.Drawing.Color.SkyBlue;
            this.button_CustomerAdd.Location = new System.Drawing.Point(305, 25);
            this.button_CustomerAdd.Name = "button_CustomerAdd";
            this.button_CustomerAdd.Size = new System.Drawing.Size(75, 41);
            this.button_CustomerAdd.TabIndex = 5;
            this.button_CustomerAdd.Text = "Add";
            this.button_CustomerAdd.UseVisualStyleBackColor = false;
            this.button_CustomerAdd.Click += new System.EventHandler(this.button_CustomerAdd_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(121, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 96;
            this.label7.Text = "Unit";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(234, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 94;
            this.label8.Text = "Stock";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 90;
            this.label6.Text = "Weight";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(676, 255);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 89;
            this.label9.Text = "Payment Method";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "Product Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 103;
            this.label4.Text = "Buying Quantity";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 195);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(559, 243);
            this.dataGridView1.TabIndex = 108;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(676, 202);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 109;
            this.label10.Text = "Total Amount";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(676, 299);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 112;
            this.label11.Text = "Cash Provided";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(676, 346);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 114;
            this.label12.Text = "Cash Return";
            // 
            // button_ConfirmPurchase
            // 
            this.button_ConfirmPurchase.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button_ConfirmPurchase.Location = new System.Drawing.Point(679, 395);
            this.button_ConfirmPurchase.Name = "button_ConfirmPurchase";
            this.button_ConfirmPurchase.Size = new System.Drawing.Size(121, 43);
            this.button_ConfirmPurchase.TabIndex = 115;
            this.button_ConfirmPurchase.Text = "Confirm Purchase";
            this.button_ConfirmPurchase.UseVisualStyleBackColor = false;
            this.button_ConfirmPurchase.Click += new System.EventHandler(this.button_ConfirmPurchase_Click);
            // 
            // button_NewInvoice
            // 
            this.button_NewInvoice.BackColor = System.Drawing.Color.Yellow;
            this.button_NewInvoice.Location = new System.Drawing.Point(648, 12);
            this.button_NewInvoice.Name = "button_NewInvoice";
            this.button_NewInvoice.Size = new System.Drawing.Size(100, 36);
            this.button_NewInvoice.TabIndex = 116;
            this.button_NewInvoice.Text = "New Invoice";
            this.button_NewInvoice.UseVisualStyleBackColor = false;
            this.button_NewInvoice.Click += new System.EventHandler(this.button_NewInvoice_Click);
            // 
            // button_Back
            // 
            this.button_Back.BackColor = System.Drawing.Color.Salmon;
            this.button_Back.Location = new System.Drawing.Point(763, 12);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(100, 36);
            this.button_Back.TabIndex = 118;
            this.button_Back.Text = "Back";
            this.button_Back.UseVisualStyleBackColor = false;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 120;
            this.label13.Text = "Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 123;
            this.label2.Text = "Selling Price";
            // 
            // comboBox_Category
            // 
            this.comboBox_Category.FormattingEnabled = true;
            this.comboBox_Category.Location = new System.Drawing.Point(8, 108);
            this.comboBox_Category.Name = "comboBox_Category";
            this.comboBox_Category.Size = new System.Drawing.Size(104, 21);
            this.comboBox_Category.TabIndex = 124;
            this.comboBox_Category.SelectedIndexChanged += new System.EventHandler(this.comboBox_Category_SelectedIndexChanged);
            // 
            // comboBox_ProductName
            // 
            this.comboBox_ProductName.FormattingEnabled = true;
            this.comboBox_ProductName.Location = new System.Drawing.Point(118, 108);
            this.comboBox_ProductName.Name = "comboBox_ProductName";
            this.comboBox_ProductName.Size = new System.Drawing.Size(100, 21);
            this.comboBox_ProductName.TabIndex = 125;
            this.comboBox_ProductName.SelectedIndexChanged += new System.EventHandler(this.comboBox_ProductName_SelectedIndexChanged);
            // 
            // textBox_SellingPrice
            // 
            this.textBox_SellingPrice.Location = new System.Drawing.Point(262, 108);
            this.textBox_SellingPrice.Name = "textBox_SellingPrice";
            this.textBox_SellingPrice.ReadOnly = true;
            this.textBox_SellingPrice.Size = new System.Drawing.Size(100, 20);
            this.textBox_SellingPrice.TabIndex = 126;
            // 
            // textBox_Weight
            // 
            this.textBox_Weight.Location = new System.Drawing.Point(8, 169);
            this.textBox_Weight.Name = "textBox_Weight";
            this.textBox_Weight.ReadOnly = true;
            this.textBox_Weight.Size = new System.Drawing.Size(100, 20);
            this.textBox_Weight.TabIndex = 127;
            // 
            // textBox_Unit
            // 
            this.textBox_Unit.Location = new System.Drawing.Point(124, 169);
            this.textBox_Unit.Name = "textBox_Unit";
            this.textBox_Unit.ReadOnly = true;
            this.textBox_Unit.Size = new System.Drawing.Size(100, 20);
            this.textBox_Unit.TabIndex = 128;
            // 
            // textBox_Stock
            // 
            this.textBox_Stock.Location = new System.Drawing.Point(237, 169);
            this.textBox_Stock.Name = "textBox_Stock";
            this.textBox_Stock.ReadOnly = true;
            this.textBox_Stock.Size = new System.Drawing.Size(100, 20);
            this.textBox_Stock.TabIndex = 129;
            // 
            // textBox_BuyingQuantity
            // 
            this.textBox_BuyingQuantity.Location = new System.Drawing.Point(350, 169);
            this.textBox_BuyingQuantity.Name = "textBox_BuyingQuantity";
            this.textBox_BuyingQuantity.Size = new System.Drawing.Size(100, 20);
            this.textBox_BuyingQuantity.TabIndex = 130;
            // 
            // textBox_TotalAmount
            // 
            this.textBox_TotalAmount.Location = new System.Drawing.Point(679, 218);
            this.textBox_TotalAmount.Name = "textBox_TotalAmount";
            this.textBox_TotalAmount.ReadOnly = true;
            this.textBox_TotalAmount.Size = new System.Drawing.Size(100, 20);
            this.textBox_TotalAmount.TabIndex = 131;
            // 
            // textBox_CashProvided
            // 
            this.textBox_CashProvided.Location = new System.Drawing.Point(679, 315);
            this.textBox_CashProvided.Name = "textBox_CashProvided";
            this.textBox_CashProvided.Size = new System.Drawing.Size(100, 20);
            this.textBox_CashProvided.TabIndex = 133;
            this.textBox_CashProvided.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_CashProvided_KeyPress);
            // 
            // textBox_CashReturn
            // 
            this.textBox_CashReturn.Location = new System.Drawing.Point(679, 362);
            this.textBox_CashReturn.Name = "textBox_CashReturn";
            this.textBox_CashReturn.ReadOnly = true;
            this.textBox_CashReturn.Size = new System.Drawing.Size(100, 20);
            this.textBox_CashReturn.TabIndex = 134;
            // 
            // button_InvoiceAdd
            // 
            this.button_InvoiceAdd.BackColor = System.Drawing.Color.Aqua;
            this.button_InvoiceAdd.Location = new System.Drawing.Point(8, 444);
            this.button_InvoiceAdd.Name = "button_InvoiceAdd";
            this.button_InvoiceAdd.Size = new System.Drawing.Size(75, 38);
            this.button_InvoiceAdd.TabIndex = 135;
            this.button_InvoiceAdd.Text = "Add";
            this.button_InvoiceAdd.UseVisualStyleBackColor = false;
            this.button_InvoiceAdd.Click += new System.EventHandler(this.button_InvoiceAdd_Click);
            // 
            // button_Remove
            // 
            this.button_Remove.BackColor = System.Drawing.Color.IndianRed;
            this.button_Remove.Location = new System.Drawing.Point(170, 444);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.Size = new System.Drawing.Size(75, 38);
            this.button_Remove.TabIndex = 136;
            this.button_Remove.Text = "Remove";
            this.button_Remove.UseVisualStyleBackColor = false;
            this.button_Remove.Click += new System.EventHandler(this.button_Remove_Click);
            // 
            // button_Update
            // 
            this.button_Update.BackColor = System.Drawing.Color.SkyBlue;
            this.button_Update.Location = new System.Drawing.Point(89, 444);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(75, 38);
            this.button_Update.TabIndex = 137;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = false;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // comboBox_PaymentMethod
            // 
            this.comboBox_PaymentMethod.FormattingEnabled = true;
            this.comboBox_PaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "E-wallet",
            "Bank"});
            this.comboBox_PaymentMethod.Location = new System.Drawing.Point(679, 271);
            this.comboBox_PaymentMethod.Name = "comboBox_PaymentMethod";
            this.comboBox_PaymentMethod.Size = new System.Drawing.Size(104, 21);
            this.comboBox_PaymentMethod.TabIndex = 138;
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(875, 494);
            this.Controls.Add(this.comboBox_PaymentMethod);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.button_Remove);
            this.Controls.Add(this.button_InvoiceAdd);
            this.Controls.Add(this.textBox_CashReturn);
            this.Controls.Add(this.textBox_CashProvided);
            this.Controls.Add(this.textBox_TotalAmount);
            this.Controls.Add(this.textBox_BuyingQuantity);
            this.Controls.Add(this.textBox_Stock);
            this.Controls.Add(this.textBox_Unit);
            this.Controls.Add(this.textBox_Weight);
            this.Controls.Add(this.textBox_SellingPrice);
            this.Controls.Add(this.comboBox_ProductName);
            this.Controls.Add(this.comboBox_Category);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button_Back);
            this.Controls.Add(this.button_NewInvoice);
            this.Controls.Add(this.button_ConfirmPurchase);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button_CustomerAdd);
            this.Controls.Add(this.textBox_CustomerPhoneNumber);
            this.Controls.Add(this.label_PhoneNumber);
            this.Controls.Add(this.button_CustomerSearch);
            this.Controls.Add(this.textBox_CustomerName);
            this.Controls.Add(this.label1);
            this.Name = "Invoice";
            this.Text = "Invoice";
            this.Load += new System.EventHandler(this.Form8_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_CustomerName;
        private System.Windows.Forms.Button button_CustomerSearch;
        private System.Windows.Forms.TextBox textBox_CustomerPhoneNumber;
        private System.Windows.Forms.Label label_PhoneNumber;
        private System.Windows.Forms.Button button_CustomerAdd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button_ConfirmPurchase;
        private System.Windows.Forms.Button button_NewInvoice;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Category;
        private System.Windows.Forms.ComboBox comboBox_ProductName;
        private System.Windows.Forms.TextBox textBox_SellingPrice;
        private System.Windows.Forms.TextBox textBox_Weight;
        private System.Windows.Forms.TextBox textBox_Unit;
        private System.Windows.Forms.TextBox textBox_Stock;
        private System.Windows.Forms.TextBox textBox_BuyingQuantity;
        private System.Windows.Forms.TextBox textBox_TotalAmount;
        private System.Windows.Forms.TextBox textBox_CashProvided;
        private System.Windows.Forms.TextBox textBox_CashReturn;
        private System.Windows.Forms.Button button_InvoiceAdd;
        private System.Windows.Forms.Button button_Remove;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.ComboBox comboBox_PaymentMethod;
    }
}