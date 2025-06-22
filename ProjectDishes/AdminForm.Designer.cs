
namespace ProjectDishes
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            btnAddRecipe = new System.Windows.Forms.Button();
            btnEditRecipe = new System.Windows.Forms.Button();
            btnDeleteRecipe = new System.Windows.Forms.Button();
            btnUserRequests = new System.Windows.Forms.Button();
            btnExit = new System.Windows.Forms.Button();
            btnUsers = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            flowLayoutPanelRecipes = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // btnAddRecipe
            // 
            btnAddRecipe.Location = new System.Drawing.Point(714, 18);
            btnAddRecipe.Name = "btnAddRecipe";
            btnAddRecipe.Size = new System.Drawing.Size(163, 43);
            btnAddRecipe.TabIndex = 1;
            btnAddRecipe.Text = "Добавить";
            btnAddRecipe.UseVisualStyleBackColor = true;
            btnAddRecipe.Click += btnAddRecipe_Click;
            // 
            // btnEditRecipe
            // 
            btnEditRecipe.Location = new System.Drawing.Point(714, 85);
            btnEditRecipe.Name = "btnEditRecipe";
            btnEditRecipe.Size = new System.Drawing.Size(163, 43);
            btnEditRecipe.TabIndex = 2;
            btnEditRecipe.Text = "Редактировать";
            btnEditRecipe.UseVisualStyleBackColor = true;
            btnEditRecipe.Click += btnEditRecipe_Click;
            // 
            // btnDeleteRecipe
            // 
            btnDeleteRecipe.Location = new System.Drawing.Point(714, 148);
            btnDeleteRecipe.Name = "btnDeleteRecipe";
            btnDeleteRecipe.Size = new System.Drawing.Size(163, 43);
            btnDeleteRecipe.TabIndex = 3;
            btnDeleteRecipe.Text = "Удалить";
            btnDeleteRecipe.UseVisualStyleBackColor = true;
            btnDeleteRecipe.Click += btnDeleteRecipe_Click;
            // 
            // btnUserRequests
            // 
            btnUserRequests.Location = new System.Drawing.Point(714, 212);
            btnUserRequests.Name = "btnUserRequests";
            btnUserRequests.Size = new System.Drawing.Size(163, 43);
            btnUserRequests.TabIndex = 4;
            btnUserRequests.Text = "Запросы";
            btnUserRequests.UseVisualStyleBackColor = true;
            btnUserRequests.Click += btnUserRequests_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new System.Drawing.Point(714, 506);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(163, 43);
            btnExit.TabIndex = 5;
            btnExit.Text = "Выйти";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnUsers
            // 
            btnUsers.Location = new System.Drawing.Point(714, 277);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new System.Drawing.Size(163, 43);
            btnUsers.TabIndex = 6;
            btnUsers.Text = "Пользователи";
            btnUsers.UseVisualStyleBackColor = true;
            btnUsers.Click += btnUsers_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(715, 361);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 20);
            label1.TabIndex = 10;
            label1.Text = "Поиск";
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(715, 393);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(159, 27);
            txtSearch.TabIndex = 9;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // flowLayoutPanelRecipes
            // 
            flowLayoutPanelRecipes.AutoScroll = true;
            flowLayoutPanelRecipes.Location = new System.Drawing.Point(7, 17);
            flowLayoutPanelRecipes.Name = "flowLayoutPanelRecipes";
            flowLayoutPanelRecipes.Size = new System.Drawing.Size(701, 532);
            flowLayoutPanelRecipes.TabIndex = 11;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            ClientSize = new System.Drawing.Size(887, 563);
            Controls.Add(flowLayoutPanelRecipes);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Controls.Add(btnUsers);
            Controls.Add(btnExit);
            Controls.Add(btnUserRequests);
            Controls.Add(btnDeleteRecipe);
            Controls.Add(btnEditRecipe);
            Controls.Add(btnAddRecipe);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "AdminForm";
            Text = "Управление";
            FormClosed += AdminForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnAddRecipe;
        private System.Windows.Forms.Button btnEditRecipe;
        private System.Windows.Forms.Button btnDeleteRecipe;
        private System.Windows.Forms.Button btnUserRequests;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRecipes;
    }
}