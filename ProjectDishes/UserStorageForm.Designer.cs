
namespace ProjectDishes
{
    partial class UserStorageForm
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
            btnDeleteRecipe = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            flowLayoutPanelUserRecipes = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // btnDeleteRecipe
            // 
            btnDeleteRecipe.Location = new System.Drawing.Point(719, 15);
            btnDeleteRecipe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnDeleteRecipe.Name = "btnDeleteRecipe";
            btnDeleteRecipe.Size = new System.Drawing.Size(158, 58);
            btnDeleteRecipe.TabIndex = 1;
            btnDeleteRecipe.Text = "Удалить";
            btnDeleteRecipe.UseVisualStyleBackColor = true;
            btnDeleteRecipe.Click += btnDeleteRecipe_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(717, 106);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 20);
            label1.TabIndex = 10;
            label1.Text = "Поиск";
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(719, 140);
            txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(158, 27);
            txtSearch.TabIndex = 9;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // flowLayoutPanelUserRecipes
            // 
            flowLayoutPanelUserRecipes.AutoScroll = true;
            flowLayoutPanelUserRecipes.Location = new System.Drawing.Point(12, 15);
            flowLayoutPanelUserRecipes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            flowLayoutPanelUserRecipes.Name = "flowLayoutPanelUserRecipes";
            flowLayoutPanelUserRecipes.Size = new System.Drawing.Size(701, 532);
            flowLayoutPanelUserRecipes.TabIndex = 11;
            // 
            // UserStorageForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            ClientSize = new System.Drawing.Size(889, 562);
            Controls.Add(flowLayoutPanelUserRecipes);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Controls.Add(btnDeleteRecipe);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "UserStorageForm";
            Text = "Избранное";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnDeleteRecipe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelUserRecipes;
    }
}