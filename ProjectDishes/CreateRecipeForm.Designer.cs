
namespace ProjectDishes
{
    partial class CreateRecipeForm
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
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            btnSubmit = new System.Windows.Forms.Button();
            cmbCategory = new System.Windows.Forms.ComboBox();
            txtIngredients = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            txtRecipeName = new System.Windows.Forms.TextBox();
            pictureBoxRecipe = new System.Windows.Forms.PictureBox();
            btnUploadImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRecipe).BeginInit();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label4.Location = new System.Drawing.Point(11, 463);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(97, 22);
            label4.TabIndex = 18;
            label4.Text = "Категория";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label3.Location = new System.Drawing.Point(11, 371);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(122, 22);
            label3.TabIndex = 17;
            label3.Text = "Ингридиенты";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label2.Location = new System.Drawing.Point(11, 272);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(93, 22);
            label2.TabIndex = 16;
            label2.Text = "Описание";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label1.Location = new System.Drawing.Point(11, 177);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(151, 22);
            label1.TabIndex = 15;
            label1.Text = "Название блюда";
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new System.Drawing.Point(13, 537);
            btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new System.Drawing.Size(375, 55);
            btnSubmit.TabIndex = 14;
            btnSubmit.Text = "Создать";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new System.Drawing.Point(11, 499);
            cmbCategory.Margin = new System.Windows.Forms.Padding(4);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new System.Drawing.Size(377, 30);
            cmbCategory.TabIndex = 13;
            // 
            // txtIngredients
            // 
            txtIngredients.Location = new System.Drawing.Point(11, 409);
            txtIngredients.Margin = new System.Windows.Forms.Padding(4);
            txtIngredients.Name = "txtIngredients";
            txtIngredients.Size = new System.Drawing.Size(369, 28);
            txtIngredients.TabIndex = 12;
            // 
            // txtDescription
            // 
            txtDescription.Location = new System.Drawing.Point(11, 319);
            txtDescription.Margin = new System.Windows.Forms.Padding(4);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(369, 28);
            txtDescription.TabIndex = 11;
            // 
            // txtRecipeName
            // 
            txtRecipeName.Location = new System.Drawing.Point(11, 216);
            txtRecipeName.Margin = new System.Windows.Forms.Padding(4);
            txtRecipeName.Name = "txtRecipeName";
            txtRecipeName.Size = new System.Drawing.Size(369, 28);
            txtRecipeName.TabIndex = 10;
            // 
            // pictureBoxRecipe
            // 
            pictureBoxRecipe.Location = new System.Drawing.Point(15, 12);
            pictureBoxRecipe.Name = "pictureBoxRecipe";
            pictureBoxRecipe.Size = new System.Drawing.Size(207, 144);
            pictureBoxRecipe.TabIndex = 19;
            pictureBoxRecipe.TabStop = false;
            pictureBoxRecipe.Click += pictureBoxRecipe_Click;
            // 
            // btnUploadImage
            // 
            btnUploadImage.Location = new System.Drawing.Point(233, 12);
            btnUploadImage.Name = "btnUploadImage";
            btnUploadImage.Size = new System.Drawing.Size(155, 33);
            btnUploadImage.TabIndex = 20;
            btnUploadImage.Text = "Выбрать";
            btnUploadImage.UseVisualStyleBackColor = true;
            btnUploadImage.Click += btnUploadImage_Click_1;
            // 
            // CreateRecipeForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            ClientSize = new System.Drawing.Size(397, 605);
            Controls.Add(btnUploadImage);
            Controls.Add(pictureBoxRecipe);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSubmit);
            Controls.Add(cmbCategory);
            Controls.Add(txtIngredients);
            Controls.Add(txtDescription);
            Controls.Add(txtRecipeName);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "CreateRecipeForm";
            Text = "Создание рецепта";
            Load += CreateRecipeForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxRecipe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtIngredients;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtRecipeName;
        private System.Windows.Forms.PictureBox pictureBoxRecipe;
        private System.Windows.Forms.Button btnUploadImage;
    }
}