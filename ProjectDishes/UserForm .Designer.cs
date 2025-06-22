
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            btnAddToStorage = new System.Windows.Forms.Button();
            btnOpenStorage = new System.Windows.Forms.Button();
            btnCreateRecipe = new System.Windows.Forms.Button();
            btnExit = new System.Windows.Forms.Button();
            txtSearch = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            flowLayoutPanelRecipes = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // btnAddToStorage
            // 
            btnAddToStorage.Location = new System.Drawing.Point(719, 14);
            btnAddToStorage.Name = "btnAddToStorage";
            btnAddToStorage.Size = new System.Drawing.Size(160, 51);
            btnAddToStorage.TabIndex = 1;
            btnAddToStorage.Text = "Сохранить";
            btnAddToStorage.UseVisualStyleBackColor = true;
            btnAddToStorage.Click += btnAddToStorage_Click;
            // 
            // btnOpenStorage
            // 
            btnOpenStorage.Location = new System.Drawing.Point(719, 73);
            btnOpenStorage.Name = "btnOpenStorage";
            btnOpenStorage.Size = new System.Drawing.Size(160, 51);
            btnOpenStorage.TabIndex = 2;
            btnOpenStorage.Text = "Избранное";
            btnOpenStorage.UseVisualStyleBackColor = true;
            btnOpenStorage.Click += btnOpenStorage_Click;
            // 
            // btnCreateRecipe
            // 
            btnCreateRecipe.Location = new System.Drawing.Point(719, 131);
            btnCreateRecipe.Name = "btnCreateRecipe";
            btnCreateRecipe.Size = new System.Drawing.Size(160, 51);
            btnCreateRecipe.TabIndex = 3;
            btnCreateRecipe.Text = "Создать";
            btnCreateRecipe.UseVisualStyleBackColor = true;
            btnCreateRecipe.Click += btnCreateRecipe_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new System.Drawing.Point(719, 504);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(160, 43);
            btnExit.TabIndex = 6;
            btnExit.Text = "Выйти";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(720, 250);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(159, 27);
            txtSearch.TabIndex = 7;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(719, 218);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 20);
            label1.TabIndex = 8;
            label1.Text = "Поиск";
            // 
            // flowLayoutPanelRecipes
            // 
            flowLayoutPanelRecipes.AutoScroll = true;
            flowLayoutPanelRecipes.Location = new System.Drawing.Point(12, 15);
            flowLayoutPanelRecipes.Name = "flowLayoutPanelRecipes";
            flowLayoutPanelRecipes.Size = new System.Drawing.Size(701, 532);
            flowLayoutPanelRecipes.TabIndex = 9;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            ClientSize = new System.Drawing.Size(890, 563);
            Controls.Add(flowLayoutPanelRecipes);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Controls.Add(btnExit);
            Controls.Add(btnCreateRecipe);
            Controls.Add(btnOpenStorage);
            Controls.Add(btnAddToStorage);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "UserForm";
            Text = "Форма пользователя";
            FormClosed += UserForm_FormClosed;
            Load += UserForm_Load;
            ResumeLayout(false);
            PerformLayout();
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