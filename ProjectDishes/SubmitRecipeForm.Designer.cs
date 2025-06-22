
namespace ProjectDishes
{
    partial class SubmitRecipeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubmitRecipeForm));
            txtRecipeName = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            txtIngredients = new System.Windows.Forms.TextBox();
            comboBoxCategory = new System.Windows.Forms.ComboBox();
            btnSubmitRecipe = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // txtRecipeName
            // 
            txtRecipeName.Location = new System.Drawing.Point(15, 47);
            txtRecipeName.Margin = new System.Windows.Forms.Padding(4);
            txtRecipeName.Name = "txtRecipeName";
            txtRecipeName.Size = new System.Drawing.Size(369, 28);
            txtRecipeName.TabIndex = 0;
            // 
            // txtDescription
            // 
            txtDescription.Location = new System.Drawing.Point(15, 120);
            txtDescription.Margin = new System.Windows.Forms.Padding(4);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(369, 28);
            txtDescription.TabIndex = 1;
            // 
            // txtIngredients
            // 
            txtIngredients.Location = new System.Drawing.Point(15, 191);
            txtIngredients.Margin = new System.Windows.Forms.Padding(4);
            txtIngredients.Name = "txtIngredients";
            txtIngredients.Size = new System.Drawing.Size(369, 28);
            txtIngredients.TabIndex = 2;
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new System.Drawing.Point(15, 263);
            comboBoxCategory.Margin = new System.Windows.Forms.Padding(4);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new System.Drawing.Size(369, 30);
            comboBoxCategory.TabIndex = 3;
            // 
            // btnSubmitRecipe
            // 
            btnSubmitRecipe.Location = new System.Drawing.Point(15, 315);
            btnSubmitRecipe.Margin = new System.Windows.Forms.Padding(4);
            btnSubmitRecipe.Name = "btnSubmitRecipe";
            btnSubmitRecipe.Size = new System.Drawing.Size(370, 55);
            btnSubmitRecipe.TabIndex = 4;
            btnSubmitRecipe.Text = "Отправить";
            btnSubmitRecipe.UseVisualStyleBackColor = true;
            btnSubmitRecipe.Click += btnSubmitRecipe_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label1.Location = new System.Drawing.Point(13, 17);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(151, 22);
            label1.TabIndex = 6;
            label1.Text = "Название блюда";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label2.Location = new System.Drawing.Point(13, 90);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(93, 22);
            label2.TabIndex = 7;
            label2.Text = "Описание";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label3.Location = new System.Drawing.Point(13, 162);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(122, 22);
            label3.TabIndex = 8;
            label3.Text = "Ингридиенты";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label4.Location = new System.Drawing.Point(13, 235);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(97, 22);
            label4.TabIndex = 9;
            label4.Text = "Категория";
            // 
            // SubmitRecipeForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            ClientSize = new System.Drawing.Size(400, 380);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSubmitRecipe);
            Controls.Add(comboBoxCategory);
            Controls.Add(txtIngredients);
            Controls.Add(txtDescription);
            Controls.Add(txtRecipeName);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4);
            Name = "SubmitRecipeForm";
            Text = "Запрос";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtRecipeName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtIngredients;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Button btnSubmitRecipe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}