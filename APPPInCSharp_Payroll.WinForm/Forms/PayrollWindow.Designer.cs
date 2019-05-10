namespace APPPInCSharp_Payroll.WinForm
{
    partial class PayrollWindow
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.actionMenuItem = new System.Windows.Forms.MenuItem();
            this.addEmployeeMenuItem = new System.Windows.Forms.MenuItem();
            this.transactionsTextBox = new System.Windows.Forms.TextBox();
            this.employeesTextBox = new System.Windows.Forms.TextBox();
            this.transactionsLabel = new System.Windows.Forms.Label();
            this.emploeesLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.actionMenuItem});
            // 
            // actionMenuItem
            // 
            this.actionMenuItem.Index = 0;
            this.actionMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.addEmployeeMenuItem});
            this.actionMenuItem.Text = "Action";
            // 
            // addEmployeeMenuItem
            // 
            this.addEmployeeMenuItem.Index = 0;
            this.addEmployeeMenuItem.Text = "Add Employee";
            this.addEmployeeMenuItem.Click += new System.EventHandler(this.addEmployeeMenuItem_Click);
            // 
            // transactionsTextBox
            // 
            this.transactionsTextBox.Location = new System.Drawing.Point(12, 60);
            this.transactionsTextBox.Multiline = true;
            this.transactionsTextBox.Name = "transactionsTextBox";
            this.transactionsTextBox.Size = new System.Drawing.Size(389, 111);
            this.transactionsTextBox.TabIndex = 0;
            // 
            // employeesTextBox
            // 
            this.employeesTextBox.Location = new System.Drawing.Point(13, 230);
            this.employeesTextBox.Multiline = true;
            this.employeesTextBox.Name = "employeesTextBox";
            this.employeesTextBox.Size = new System.Drawing.Size(388, 121);
            this.employeesTextBox.TabIndex = 1;
            // 
            // transactionsLabel
            // 
            this.transactionsLabel.AutoSize = true;
            this.transactionsLabel.Location = new System.Drawing.Point(13, 42);
            this.transactionsLabel.Name = "transactionsLabel";
            this.transactionsLabel.Size = new System.Drawing.Size(85, 12);
            this.transactionsLabel.TabIndex = 2;
            this.transactionsLabel.Text = "TransactionLabel";
            // 
            // emploeesLabel
            // 
            this.emploeesLabel.AutoSize = true;
            this.emploeesLabel.Location = new System.Drawing.Point(12, 212);
            this.emploeesLabel.Name = "emploeesLabel";
            this.emploeesLabel.Size = new System.Drawing.Size(56, 12);
            this.emploeesLabel.TabIndex = 3;
            this.emploeesLabel.Text = "Employees";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(13, 432);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 4;
            this.runButton.Text = "Run Trans";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // PayrollWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 615);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.emploeesLabel);
            this.Controls.Add(this.transactionsLabel);
            this.Controls.Add(this.employeesTextBox);
            this.Controls.Add(this.transactionsTextBox);
            this.Menu = this.mainMenu1;
            this.Name = "PayrollWindow";
            this.Text = "PayrollWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem actionMenuItem;
        public System.Windows.Forms.MenuItem addEmployeeMenuItem;
        public System.Windows.Forms.TextBox transactionsTextBox;
        public System.Windows.Forms.TextBox employeesTextBox;
        private System.Windows.Forms.Label transactionsLabel;
        private System.Windows.Forms.Label emploeesLabel;
        public System.Windows.Forms.Button runButton;
    }
}