
namespace SchedulingApp_JoshuaRea.Forms
{
    partial class Reports
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
            this.lblReports = new System.Windows.Forms.Label();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.dgvUserAppointments = new System.Windows.Forms.DataGridView();
            this.dgvAppointmentTypes = new System.Windows.Forms.DataGridView();
            this.dgvCountryCustomers = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCountryCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReports
            // 
            this.lblReports.AutoSize = true;
            this.lblReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReports.Location = new System.Drawing.Point(28, 21);
            this.lblReports.Name = "lblReports";
            this.lblReports.Size = new System.Drawing.Size(87, 25);
            this.lblReports.TabIndex = 0;
            this.lblReports.Text = "Reports";
            // 
            // cmbUsers
            // 
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(707, 47);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(176, 28);
            this.cmbUsers.TabIndex = 1;
            this.cmbUsers.SelectedIndexChanged += new System.EventHandler(this.cmbUsers_SelectedIndexChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(658, 50);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(43, 20);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "User";
            // 
            // dgvUserAppointments
            // 
            this.dgvUserAppointments.AllowUserToAddRows = false;
            this.dgvUserAppointments.AllowUserToDeleteRows = false;
            this.dgvUserAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUserAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserAppointments.Location = new System.Drawing.Point(33, 81);
            this.dgvUserAppointments.Name = "dgvUserAppointments";
            this.dgvUserAppointments.ReadOnly = true;
            this.dgvUserAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserAppointments.Size = new System.Drawing.Size(850, 189);
            this.dgvUserAppointments.TabIndex = 3;
            // 
            // dgvAppointmentTypes
            // 
            this.dgvAppointmentTypes.AllowUserToAddRows = false;
            this.dgvAppointmentTypes.AllowUserToDeleteRows = false;
            this.dgvAppointmentTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointmentTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointmentTypes.Location = new System.Drawing.Point(33, 292);
            this.dgvAppointmentTypes.Name = "dgvAppointmentTypes";
            this.dgvAppointmentTypes.ReadOnly = true;
            this.dgvAppointmentTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointmentTypes.Size = new System.Drawing.Size(364, 187);
            this.dgvAppointmentTypes.TabIndex = 4;
            // 
            // dgvCountryCustomers
            // 
            this.dgvCountryCustomers.AllowUserToAddRows = false;
            this.dgvCountryCustomers.AllowUserToDeleteRows = false;
            this.dgvCountryCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCountryCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCountryCustomers.Location = new System.Drawing.Point(603, 292);
            this.dgvCountryCustomers.Name = "dgvCountryCustomers";
            this.dgvCountryCustomers.ReadOnly = true;
            this.dgvCountryCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCountryCustomers.Size = new System.Drawing.Size(280, 187);
            this.dgvCountryCustomers.TabIndex = 5;
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(794, 496);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(89, 34);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 548);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvCountryCustomers);
            this.Controls.Add(this.dgvAppointmentTypes);
            this.Controls.Add(this.dgvUserAppointments);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.cmbUsers);
            this.Controls.Add(this.lblReports);
            this.Name = "Reports";
            this.Text = "Scheduler";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCountryCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReports;
        private System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.DataGridView dgvUserAppointments;
        private System.Windows.Forms.DataGridView dgvAppointmentTypes;
        private System.Windows.Forms.DataGridView dgvCountryCustomers;
        private System.Windows.Forms.Button btnBack;
    }
}