
namespace ProjectDishes
{
    partial class RequestDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestDetailsForm));
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            txtIngredients = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            lblCategory = new System.Windows.Forms.Label();
            lblRecipeName = new System.Windows.Forms.Label();
            pictureBoxRequest = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRequest).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(198, 106);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(82, 20);
            label2.TabIndex = 15;
            label2.Text = "Описание:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 266);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(107, 20);
            label1.TabIndex = 14;
            label1.Text = "Ингридиенты:";
            // 
            // txtIngredients
            // 
            txtIngredients.Location = new System.Drawing.Point(12, 300);
            txtIngredients.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtIngredients.Name = "txtIngredients";
            txtIngredients.ReadOnly = true;
            txtIngredients.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtIngredients.Size = new System.Drawing.Size(776, 27);
            txtIngredients.TabIndex = 13;
            txtIngredients.TabStop = false;
            // 
            // txtDescription
            // 
            txtDescription.Location = new System.Drawing.Point(198, 144);
            txtDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtDescription.Size = new System.Drawing.Size(590, 27);
            txtDescription.TabIndex = 12;
            txtDescription.TabStop = false;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new System.Drawing.Point(198, 64);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new System.Drawing.Size(50, 20);
            lblCategory.TabIndex = 11;
            lblCategory.Text = "label1";
            // 
            // lblRecipeName
            // 
            lblRecipeName.AutoSize = true;
            lblRecipeName.Location = new System.Drawing.Point(198, 15);
            lblRecipeName.Name = "lblRecipeName";
            lblRecipeName.Size = new System.Drawing.Size(50, 20);
            lblRecipeName.TabIndex = 10;
            lblRecipeName.Text = "label1";
            // 
            // pictureBoxRequest
            // 
            pictureBoxRequest.Location = new System.Drawing.Point(12, 15);
            pictureBoxRequest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pictureBoxRequest.Name = "pictureBoxRequest";
            pictureBoxRequest.Size = new System.Drawing.Size(180, 225);
            pictureBoxRequest.TabIndex = 9;
            pictureBoxRequest.TabStop = false;
            // 
            // RequestDetailsForm
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
            Controls.Add(pictureBoxRequest);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "RequestDetailsForm";
            Text = "Form1";
            Load += RequestDetailsForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxRequest).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIngredients;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblRecipeName;
        private System.Windows.Forms.PictureBox pictureBoxRequest;
    }
}