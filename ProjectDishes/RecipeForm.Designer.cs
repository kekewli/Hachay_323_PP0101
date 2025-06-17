
namespace ProjectDishes
{
    partial class RecipeForm
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
            txtRecipeName = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            txtIngredients = new System.Windows.Forms.TextBox();
            comboBoxCategory = new System.Windows.Forms.ComboBox();
            btnSave = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            btnUploadImage = new System.Windows.Forms.Button();
            pictureBoxRecipe = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRecipe).BeginInit();
            SuspendLayout();
            // 
            // txtRecipeName
            // 
            txtRecipeName.Location = new System.Drawing.Point(13, 197);
            txtRecipeName.Margin = new System.Windows.Forms.Padding(4);
            txtRecipeName.Name = "txtRecipeName";
            txtRecipeName.Size = new System.Drawing.Size(369, 28);
            txtRecipeName.TabIndex = 0;
            // 
            // txtDescription
            // 
            txtDescription.Location = new System.Drawing.Point(13, 271);
            txtDescription.Margin = new System.Windows.Forms.Padding(4);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(369, 28);
            txtDescription.TabIndex = 1;
            // 
            // txtIngredients
            // 
            txtIngredients.Location = new System.Drawing.Point(13, 341);
            txtIngredients.Margin = new System.Windows.Forms.Padding(4);
            txtIngredients.Name = "txtIngredients";
            txtIngredients.Size = new System.Drawing.Size(369, 28);
            txtIngredients.TabIndex = 2;
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new System.Drawing.Point(13, 413);
            comboBoxCategory.Margin = new System.Windows.Forms.Padding(4);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new System.Drawing.Size(369, 30);
            comboBoxCategory.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(13, 465);
            btnSave.Margin = new System.Windows.Forms.Padding(4);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(370, 55);
            btnSave.TabIndex = 4;
            btnSave.Text = "Добавить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label1.Location = new System.Drawing.Point(11, 166);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(151, 22);
            label1.TabIndex = 5;
            label1.Text = "Название блюда";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label2.Location = new System.Drawing.Point(11, 238);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(93, 22);
            label2.TabIndex = 6;
            label2.Text = "Описание";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label3.Location = new System.Drawing.Point(11, 311);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(122, 22);
            label3.TabIndex = 7;
            label3.Text = "Ингридиенты";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label4.Location = new System.Drawing.Point(11, 381);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(97, 22);
            label4.TabIndex = 8;
            label4.Text = "Категория";
            // 
            // btnUploadImage
            // 
            btnUploadImage.Location = new System.Drawing.Point(233, 12);
            btnUploadImage.Name = "btnUploadImage";
            btnUploadImage.Size = new System.Drawing.Size(155, 33);
            btnUploadImage.TabIndex = 22;
            btnUploadImage.Text = "Выбрать";
            btnUploadImage.UseVisualStyleBackColor = true;
            btnUploadImage.Click += btnUploadImage_Click;
            // 
            // pictureBoxRecipe
            // 
            pictureBoxRecipe.Location = new System.Drawing.Point(15, 12);
            pictureBoxRecipe.Name = "pictureBoxRecipe";
            pictureBoxRecipe.Size = new System.Drawing.Size(212, 151);
            pictureBoxRecipe.TabIndex = 21;
            pictureBoxRecipe.TabStop = false;
            pictureBoxRecipe.Click += pictureBoxRecipe_Click;
            // 
            // RecipeForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            ClientSize = new System.Drawing.Size(400, 533);
            Controls.Add(btnUploadImage);
            Controls.Add(pictureBoxRecipe);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Controls.Add(comboBoxCategory);
            Controls.Add(txtIngredients);
            Controls.Add(txtDescription);
            Controls.Add(txtRecipeName);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "RecipeForm";
            Text = "Создание";
            ((System.ComponentModel.ISupportInitialize)pictureBoxRecipe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtRecipeName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtIngredients;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.PictureBox pictureBoxRecipe;
    }
}