
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
            this.btnAddRecipe = new System.Windows.Forms.Button();
            this.btnEditRecipe = new System.Windows.Forms.Button();
            this.btnDeleteRecipe = new System.Windows.Forms.Button();
            this.btnUserRequests = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelRecipes = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnAddRecipe
            // 
            this.btnAddRecipe.Location = new System.Drawing.Point(434, 10);
            this.btnAddRecipe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddRecipe.Name = "btnAddRecipe";
            this.btnAddRecipe.Size = new System.Drawing.Size(122, 28);
            this.btnAddRecipe.TabIndex = 1;
            this.btnAddRecipe.Text = "Добавить";
            this.btnAddRecipe.UseVisualStyleBackColor = true;
            this.btnAddRecipe.Click += new System.EventHandler(this.btnAddRecipe_Click);
            // 
            // btnEditRecipe
            // 
            this.btnEditRecipe.Location = new System.Drawing.Point(434, 53);
            this.btnEditRecipe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEditRecipe.Name = "btnEditRecipe";
            this.btnEditRecipe.Size = new System.Drawing.Size(122, 28);
            this.btnEditRecipe.TabIndex = 2;
            this.btnEditRecipe.Text = "Редактировать";
            this.btnEditRecipe.UseVisualStyleBackColor = true;
            this.btnEditRecipe.Click += new System.EventHandler(this.btnEditRecipe_Click);
            // 
            // btnDeleteRecipe
            // 
            this.btnDeleteRecipe.Location = new System.Drawing.Point(434, 94);
            this.btnDeleteRecipe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDeleteRecipe.Name = "btnDeleteRecipe";
            this.btnDeleteRecipe.Size = new System.Drawing.Size(122, 28);
            this.btnDeleteRecipe.TabIndex = 3;
            this.btnDeleteRecipe.Text = "Удалить";
            this.btnDeleteRecipe.UseVisualStyleBackColor = true;
            this.btnDeleteRecipe.Click += new System.EventHandler(this.btnDeleteRecipe_Click);
            // 
            // btnUserRequests
            // 
            this.btnUserRequests.Location = new System.Drawing.Point(434, 136);
            this.btnUserRequests.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUserRequests.Name = "btnUserRequests";
            this.btnUserRequests.Size = new System.Drawing.Size(122, 28);
            this.btnUserRequests.TabIndex = 4;
            this.btnUserRequests.Text = "Запросы";
            this.btnUserRequests.UseVisualStyleBackColor = true;
            this.btnUserRequests.Click += new System.EventHandler(this.btnUserRequests_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(434, 327);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(122, 28);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Выйти";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.Location = new System.Drawing.Point(434, 178);
            this.btnUsers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(122, 28);
            this.btnUsers.TabIndex = 6;
            this.btnUsers.Text = "Пользователи";
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 233);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Поиск";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(436, 249);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(120, 20);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // flowLayoutPanelRecipes
            // 
            this.flowLayoutPanelRecipes.AutoScroll = true;
            this.flowLayoutPanelRecipes.Location = new System.Drawing.Point(5, 11);
            this.flowLayoutPanelRecipes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanelRecipes.Name = "flowLayoutPanelRecipes";
            this.flowLayoutPanelRecipes.Size = new System.Drawing.Size(424, 346);
            this.flowLayoutPanelRecipes.TabIndex = 11;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(564, 366);
            this.Controls.Add(this.flowLayoutPanelRecipes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnUsers);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUserRequests);
            this.Controls.Add(this.btnDeleteRecipe);
            this.Controls.Add(this.btnEditRecipe);
            this.Controls.Add(this.btnAddRecipe);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AdminForm";
            this.Text = "Управление";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

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