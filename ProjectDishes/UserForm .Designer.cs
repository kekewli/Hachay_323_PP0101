
namespace ProjectDishes
{
    partial class UserForm
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
            this.btnAddToStorage = new System.Windows.Forms.Button();
            this.btnOpenStorage = new System.Windows.Forms.Button();
            this.btnCreateRecipe = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelRecipes = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnAddToStorage
            // 
            this.btnAddToStorage.Location = new System.Drawing.Point(437, 10);
            this.btnAddToStorage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddToStorage.Name = "btnAddToStorage";
            this.btnAddToStorage.Size = new System.Drawing.Size(120, 33);
            this.btnAddToStorage.TabIndex = 1;
            this.btnAddToStorage.Text = "Сохранить";
            this.btnAddToStorage.UseVisualStyleBackColor = true;
            this.btnAddToStorage.Click += new System.EventHandler(this.btnAddToStorage_Click);
            // 
            // btnOpenStorage
            // 
            this.btnOpenStorage.Location = new System.Drawing.Point(437, 48);
            this.btnOpenStorage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOpenStorage.Name = "btnOpenStorage";
            this.btnOpenStorage.Size = new System.Drawing.Size(120, 33);
            this.btnOpenStorage.TabIndex = 2;
            this.btnOpenStorage.Text = "Избранное";
            this.btnOpenStorage.UseVisualStyleBackColor = true;
            this.btnOpenStorage.Click += new System.EventHandler(this.btnOpenStorage_Click);
            // 
            // btnCreateRecipe
            // 
            this.btnCreateRecipe.Location = new System.Drawing.Point(437, 86);
            this.btnCreateRecipe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCreateRecipe.Name = "btnCreateRecipe";
            this.btnCreateRecipe.Size = new System.Drawing.Size(120, 33);
            this.btnCreateRecipe.TabIndex = 3;
            this.btnCreateRecipe.Text = "Создать";
            this.btnCreateRecipe.UseVisualStyleBackColor = true;
            this.btnCreateRecipe.Click += new System.EventHandler(this.btnCreateRecipe_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(437, 328);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 28);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Выйти";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(439, 159);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(120, 20);
            this.txtSearch.TabIndex = 7;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(437, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Поиск";
            // 
            // flowLayoutPanelRecipes
            // 
            this.flowLayoutPanelRecipes.AutoScroll = true;
            this.flowLayoutPanelRecipes.Location = new System.Drawing.Point(9, 10);
            this.flowLayoutPanelRecipes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanelRecipes.Name = "flowLayoutPanelRecipes";
            this.flowLayoutPanelRecipes.Size = new System.Drawing.Size(424, 346);
            this.flowLayoutPanelRecipes.TabIndex = 9;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(564, 366);
            this.Controls.Add(this.flowLayoutPanelRecipes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCreateRecipe);
            this.Controls.Add(this.btnOpenStorage);
            this.Controls.Add(this.btnAddToStorage);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UserForm";
            this.Text = "Форма пользователя";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserForm_FormClosed);
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddToStorage;
        private System.Windows.Forms.Button btnOpenStorage;
        private System.Windows.Forms.Button btnCreateRecipe;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRecipes;
    }
}