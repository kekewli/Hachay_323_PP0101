
namespace ProjectDishes
{
    partial class RecipeDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeDetailsForm));
            pictureBoxRecipe = new System.Windows.Forms.PictureBox();
            lblRecipeName = new System.Windows.Forms.Label();
            lblCategory = new System.Windows.Forms.Label();
            txtDescription = new System.Windows.Forms.TextBox();
            txtIngredients = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRecipe).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxRecipe
            // 
            pictureBoxRecipe.Location = new System.Drawing.Point(12, 15);
            pictureBoxRecipe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pictureBoxRecipe.Name = "pictureBoxRecipe";
            pictureBoxRecipe.Size = new System.Drawing.Size(180, 225);
            pictureBoxRecipe.TabIndex = 0;
            pictureBoxRecipe.TabStop = false;
            pictureBoxRecipe.Click += pictureBoxRecipe_Click;
            // 
            // lblRecipeName
            // 
            lblRecipeName.AutoSize = true;
            lblRecipeName.Location = new System.Drawing.Point(198, 15);
            lblRecipeName.Name = "lblRecipeName";
            lblRecipeName.Size = new System.Drawing.Size(50, 20);
            lblRecipeName.TabIndex = 1;
            lblRecipeName.Text = "label1";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new System.Drawing.Point(198, 64);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new System.Drawing.Size(50, 20);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "label1";
            // 
            // txtDescription
            // 
            txtDescription.Location = new System.Drawing.Point(198, 144);
            txtDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDescription.Size = new System.Drawing.Size(590, 27);
            txtDescription.TabIndex = 5;
            txtDescription.TabStop = false;
            // 
            // txtIngredients
            // 
            txtIngredients.Location = new System.Drawing.Point(12, 300);
            txtIngredients.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtIngredients.Name = "txtIngredients";
            txtIngredients.ReadOnly = true;
            txtIngredients.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtIngredients.Size = new System.Drawing.Size(776, 27);
            txtIngredients.TabIndex = 6;
            txtIngredients.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 266);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(107, 20);
            label1.TabIndex = 7;
            label1.Text = "Ингридиенты:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(198, 106);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(82, 20);
            label2.TabIndex = 8;
            label2.Text = "Описание:";
            // 
            // RecipeDetailsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 452);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtIngredients);
            Controls.Add(txtDescription);
            Controls.Add(lblCategory);
            Controls.Add(lblRecipeName);
            Controls.Add(pictureBoxRecipe);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "RecipeDetailsForm";
            Text = "Детали рецепта";
            ((System.ComponentModel.ISupportInitialize)pictureBoxRecipe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxRecipe;
        private System.Windows.Forms.Label lblRecipeName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtIngredients;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}