
namespace ProjectDishes
{
    partial class UsersForm
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
            dataGridViewUsers = new System.Windows.Forms.DataGridView();
            btnDeleteUser = new System.Windows.Forms.Button();
            btnUpdateUser = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            btnSetAdminRights = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewUsers
            // 
            dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsers.Location = new System.Drawing.Point(12, 15);
            dataGridViewUsers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridViewUsers.Name = "dataGridViewUsers";
            dataGridViewUsers.RowHeadersWidth = 51;
            dataGridViewUsers.RowTemplate.Height = 24;
            dataGridViewUsers.Size = new System.Drawing.Size(867, 531);
            dataGridViewUsers.TabIndex = 1;
            dataGridViewUsers.CellContentClick += dataGridViewUsers_CellContentClick;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Location = new System.Drawing.Point(888, 79);
            btnDeleteUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new System.Drawing.Size(162, 42);
            btnDeleteUser.TabIndex = 6;
            btnDeleteUser.Text = "Удалить";
            btnDeleteUser.UseVisualStyleBackColor = true;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // btnUpdateUser
            // 
            btnUpdateUser.Location = new System.Drawing.Point(888, 13);
            btnUpdateUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnUpdateUser.Name = "btnUpdateUser";
            btnUpdateUser.Size = new System.Drawing.Size(162, 42);
            btnUpdateUser.TabIndex = 4;
            btnUpdateUser.Text = "Изменить";
            btnUpdateUser.UseVisualStyleBackColor = true;
            btnUpdateUser.Click += btnUpdateUser_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(885, 254);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 20);
            label1.TabIndex = 12;
            label1.Text = "Поиск";
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(888, 284);
            txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(158, 27);
            txtSearch.TabIndex = 11;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnSetAdminRights
            // 
            btnSetAdminRights.Location = new System.Drawing.Point(888, 149);
            btnSetAdminRights.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnSetAdminRights.Name = "btnSetAdminRights";
            btnSetAdminRights.Size = new System.Drawing.Size(162, 42);
            btnSetAdminRights.TabIndex = 14;
            btnSetAdminRights.Text = "Изменить доступ";
            btnSetAdminRights.UseVisualStyleBackColor = true;
            btnSetAdminRights.Click += btnSetAdminRights_Click_1;
            // 
            // UsersForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1062, 562);
            Controls.Add(btnSetAdminRights);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Controls.Add(btnDeleteUser);
            Controls.Add(btnUpdateUser);
            Controls.Add(dataGridViewUsers);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "UsersForm";
            Text = "Пользователи";
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnUpdateUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSetAdminRights;
    }
}