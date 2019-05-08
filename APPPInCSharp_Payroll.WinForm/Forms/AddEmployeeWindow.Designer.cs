namespace APPPInCSharp_Payroll.WinForm
{
    partial class AddEmployeeWindow
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
            this.empIdLabel = new System.Windows.Forms.Label();
            this.empIdTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.addressLabel = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.hourlyRadioButton = new System.Windows.Forms.RadioButton();
            this.salaryRadioButton = new System.Windows.Forms.RadioButton();
            this.commissionRadioButton = new System.Windows.Forms.RadioButton();
            this.hourlyRateLabel = new System.Windows.Forms.Label();
            this.hourlyRateTextBox = new System.Windows.Forms.TextBox();
            this.salaryLabel = new System.Windows.Forms.Label();
            this.salaryTextBox = new System.Windows.Forms.TextBox();
            this.commissionLabel = new System.Windows.Forms.Label();
            this.commissionTextBox = new System.Windows.Forms.TextBox();
            this.commissionSalaryLabel = new System.Windows.Forms.Label();
            this.commissionSalaryTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // empIdLabel
            // 
            this.empIdLabel.AutoSize = true;
            this.empIdLabel.Location = new System.Drawing.Point(12, 22);
            this.empIdLabel.Name = "empIdLabel";
            this.empIdLabel.Size = new System.Drawing.Size(37, 12);
            this.empIdLabel.TabIndex = 0;
            this.empIdLabel.Text = "EmpId";
            // 
            // empIdTextBox
            // 
            this.empIdTextBox.Location = new System.Drawing.Point(63, 12);
            this.empIdTextBox.Name = "empIdTextBox";
            this.empIdTextBox.Size = new System.Drawing.Size(100, 22);
            this.empIdTextBox.TabIndex = 1;
            this.empIdTextBox.TextChanged += new System.EventHandler(this.empIdTextBox_TextChanged);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 54);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(32, 12);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(63, 44);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 22);
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Location = new System.Drawing.Point(12, 85);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(42, 12);
            this.addressLabel.TabIndex = 4;
            this.addressLabel.Text = "Address";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(63, 75);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(100, 22);
            this.addressTextBox.TabIndex = 5;
            this.addressTextBox.TextChanged += new System.EventHandler(this.addressTextBox_TextChanged);
            // 
            // hourlyRadioButton
            // 
            this.hourlyRadioButton.AutoSize = true;
            this.hourlyRadioButton.Location = new System.Drawing.Point(426, 22);
            this.hourlyRadioButton.Name = "hourlyRadioButton";
            this.hourlyRadioButton.Size = new System.Drawing.Size(56, 16);
            this.hourlyRadioButton.TabIndex = 6;
            this.hourlyRadioButton.TabStop = true;
            this.hourlyRadioButton.Text = "Hourly";
            this.hourlyRadioButton.UseVisualStyleBackColor = true;
            this.hourlyRadioButton.CheckedChanged += new System.EventHandler(this.hourlyRadioButton_CheckedChanged);
            // 
            // salaryRadioButton
            // 
            this.salaryRadioButton.AutoSize = true;
            this.salaryRadioButton.Location = new System.Drawing.Point(426, 133);
            this.salaryRadioButton.Name = "salaryRadioButton";
            this.salaryRadioButton.Size = new System.Drawing.Size(60, 16);
            this.salaryRadioButton.TabIndex = 7;
            this.salaryRadioButton.TabStop = true;
            this.salaryRadioButton.Text = "Salaried";
            this.salaryRadioButton.UseVisualStyleBackColor = true;
            this.salaryRadioButton.CheckedChanged += new System.EventHandler(this.salaryRadioButton_CheckedChanged);
            // 
            // commissionRadioButton
            // 
            this.commissionRadioButton.AutoSize = true;
            this.commissionRadioButton.Location = new System.Drawing.Point(426, 270);
            this.commissionRadioButton.Name = "commissionRadioButton";
            this.commissionRadioButton.Size = new System.Drawing.Size(92, 16);
            this.commissionRadioButton.TabIndex = 8;
            this.commissionRadioButton.TabStop = true;
            this.commissionRadioButton.Text = "Commissioned";
            this.commissionRadioButton.UseVisualStyleBackColor = true;
            this.commissionRadioButton.CheckedChanged += new System.EventHandler(this.commissionRadioButton_CheckedChanged);
            // 
            // hourlyRateLabel
            // 
            this.hourlyRateLabel.AutoSize = true;
            this.hourlyRateLabel.Location = new System.Drawing.Point(456, 54);
            this.hourlyRateLabel.Name = "hourlyRateLabel";
            this.hourlyRateLabel.Size = new System.Drawing.Size(26, 12);
            this.hourlyRateLabel.TabIndex = 9;
            this.hourlyRateLabel.Text = "Rate";
            // 
            // hourlyRateTextBox
            // 
            this.hourlyRateTextBox.Enabled = false;
            this.hourlyRateTextBox.Location = new System.Drawing.Point(499, 44);
            this.hourlyRateTextBox.Name = "hourlyRateTextBox";
            this.hourlyRateTextBox.Size = new System.Drawing.Size(100, 22);
            this.hourlyRateTextBox.TabIndex = 10;
            this.hourlyRateTextBox.TextChanged += new System.EventHandler(this.hourlyRateTextBox_TextChanged);
            // 
            // salaryLabel
            // 
            this.salaryLabel.AutoSize = true;
            this.salaryLabel.Location = new System.Drawing.Point(456, 166);
            this.salaryLabel.Name = "salaryLabel";
            this.salaryLabel.Size = new System.Drawing.Size(34, 12);
            this.salaryLabel.TabIndex = 11;
            this.salaryLabel.Text = "Salary";
            // 
            // salaryTextBox
            // 
            this.salaryTextBox.Enabled = false;
            this.salaryTextBox.Location = new System.Drawing.Point(499, 156);
            this.salaryTextBox.Name = "salaryTextBox";
            this.salaryTextBox.Size = new System.Drawing.Size(100, 22);
            this.salaryTextBox.TabIndex = 12;
            this.salaryTextBox.TextChanged += new System.EventHandler(this.salaryTextBox_TextChanged);
            // 
            // commissionLabel
            // 
            this.commissionLabel.AutoSize = true;
            this.commissionLabel.Location = new System.Drawing.Point(458, 304);
            this.commissionLabel.Name = "commissionLabel";
            this.commissionLabel.Size = new System.Drawing.Size(26, 12);
            this.commissionLabel.TabIndex = 13;
            this.commissionLabel.Text = "Rate";
            // 
            // commissionTextBox
            // 
            this.commissionTextBox.Enabled = false;
            this.commissionTextBox.Location = new System.Drawing.Point(499, 293);
            this.commissionTextBox.Name = "commissionTextBox";
            this.commissionTextBox.Size = new System.Drawing.Size(100, 22);
            this.commissionTextBox.TabIndex = 14;
            this.commissionTextBox.TextChanged += new System.EventHandler(this.commissionTextBox_TextChanged);
            // 
            // commissionSalaryLabel
            // 
            this.commissionSalaryLabel.AutoSize = true;
            this.commissionSalaryLabel.Location = new System.Drawing.Point(460, 343);
            this.commissionSalaryLabel.Name = "commissionSalaryLabel";
            this.commissionSalaryLabel.Size = new System.Drawing.Size(34, 12);
            this.commissionSalaryLabel.TabIndex = 15;
            this.commissionSalaryLabel.Text = "Salary";
            // 
            // commissionSalaryTextBox
            // 
            this.commissionSalaryTextBox.Enabled = false;
            this.commissionSalaryTextBox.Location = new System.Drawing.Point(499, 332);
            this.commissionSalaryTextBox.Name = "commissionSalaryTextBox";
            this.commissionSalaryTextBox.Size = new System.Drawing.Size(100, 22);
            this.commissionSalaryTextBox.TabIndex = 16;
            this.commissionSalaryTextBox.TextChanged += new System.EventHandler(this.commissionSalaryTextBox_TextChanged);
            // 
            // submitButton
            // 
            this.submitButton.Enabled = false;
            this.submitButton.Location = new System.Drawing.Point(680, 542);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 17;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // AddEmployeeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 602);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.commissionSalaryTextBox);
            this.Controls.Add(this.commissionSalaryLabel);
            this.Controls.Add(this.commissionTextBox);
            this.Controls.Add(this.commissionLabel);
            this.Controls.Add(this.salaryTextBox);
            this.Controls.Add(this.salaryLabel);
            this.Controls.Add(this.hourlyRateTextBox);
            this.Controls.Add(this.hourlyRateLabel);
            this.Controls.Add(this.hourlyRadioButton);
            this.Controls.Add(this.commissionRadioButton);
            this.Controls.Add(this.salaryRadioButton);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.empIdTextBox);
            this.Controls.Add(this.empIdLabel);
            this.Name = "AddEmployeeWindow";
            this.Text = "AddEmployeeWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label empIdLabel;
        public System.Windows.Forms.TextBox empIdTextBox;
        private System.Windows.Forms.Label nameLabel;
        public System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label addressLabel;
        public System.Windows.Forms.TextBox addressTextBox;
        public System.Windows.Forms.RadioButton hourlyRadioButton;
        public System.Windows.Forms.RadioButton salaryRadioButton;
        public System.Windows.Forms.RadioButton commissionRadioButton;
        private System.Windows.Forms.Label hourlyRateLabel;
        public System.Windows.Forms.TextBox hourlyRateTextBox;
        private System.Windows.Forms.Label salaryLabel;
        public System.Windows.Forms.TextBox salaryTextBox;
        private System.Windows.Forms.Label commissionLabel;
        public System.Windows.Forms.TextBox commissionTextBox;
        private System.Windows.Forms.Label commissionSalaryLabel;
        public System.Windows.Forms.TextBox commissionSalaryTextBox;
        public System.Windows.Forms.Button submitButton;
    }
}