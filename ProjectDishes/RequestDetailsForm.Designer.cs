
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIngredients = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblRecipeName = new System.Windows.Forms.Label();
            this.pictureBoxRequest = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRequest)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Описание:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Ингридиенты:";
            // 
            // txtIngredients
            // 
            this.txtIngredients.Location = new System.Drawing.Point(12, 215);
            this.txtIngredients.Name = "txtIngredients";
            this.txtIngredients.ReadOnly = true;
            this.txtIngredients.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIngredients.Size = new System.Drawing.Size(776, 22);
            this.txtIngredients.TabIndex = 13;
            this.txtIngredients.TabStop = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(198, 99);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(590, 22);
            this.txtDescription.TabIndex = 12;
            this.txtDescription.TabStop = false;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(198, 51);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(46, 17);
            this.lblCategory.TabIndex = 11;
            this.lblCategory.Text = "label1";
            // 
            // lblRecipeName
            // 
            this.lblRecipeName.AutoSize = true;
            this.lblRecipeName.Location = new System.Drawing.Point(198, 12);
            this.lblRecipeName.Name = "lblRecipeName";
            this.lblRecipeName.Size = new System.Drawing.Size(46, 17);
            this.lblRecipeName.TabIndex = 10;
            this.lblRecipeName.Text = "label1";
            // 
            // pictureBoxRequest
            // 
            this.pictureBoxRequest.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxRequest.Name = "pictureBoxRequest";
            this.pictureBoxRequest.Size = new System.Drawing.Size(180, 180);
            this.pictureBoxRequest.TabIndex = 9;
            this.pictureBoxRequest.TabStop = false;
            // 
            // RequestDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 316);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIngredients);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblRecipeName);
            this.Controls.Add(this.pictureBoxRequest);
            this.Name = "RequestDetailsForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.RequestDetailsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRequest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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