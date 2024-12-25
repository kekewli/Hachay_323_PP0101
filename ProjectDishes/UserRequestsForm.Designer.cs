
namespace ProjectDishes
{
    partial class UserRequestsForm
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
            this.btnApproveRecipe = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnDeleteRequest = new System.Windows.Forms.Button();
            this.flowLayoutPanelRequests = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnApproveRecipe
            // 
            this.btnApproveRecipe.Location = new System.Drawing.Point(574, 12);
            this.btnApproveRecipe.Name = "btnApproveRecipe";
            this.btnApproveRecipe.Size = new System.Drawing.Size(162, 34);
            this.btnApproveRecipe.TabIndex = 1;
            this.btnApproveRecipe.Text = "Добавить";
            this.btnApproveRecipe.UseVisualStyleBackColor = true;
            this.btnApproveRecipe.Click += new System.EventHandler(this.btnApproveRecipe_Click);
            // 
            // btnDeleteRequest
            // 
            this.btnDeleteRequest.Location = new System.Drawing.Point(574, 52);
            this.btnDeleteRequest.Name = "btnDeleteRequest";
            this.btnDeleteRequest.Size = new System.Drawing.Size(162, 34);
            this.btnDeleteRequest.TabIndex = 2;
            this.btnDeleteRequest.Text = "Удалить";
            this.btnDeleteRequest.UseVisualStyleBackColor = true;
            this.btnDeleteRequest.Click += new System.EventHandler(this.btnDeleteRequest_Click);
            // 
            // flowLayoutPanelRequests
            // 
            this.flowLayoutPanelRequests.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanelRequests.Name = "flowLayoutPanelRequests";
            this.flowLayoutPanelRequests.Size = new System.Drawing.Size(556, 426);
            this.flowLayoutPanelRequests.TabIndex = 3;
            // 
            // UserRequestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(745, 450);
            this.Controls.Add(this.flowLayoutPanelRequests);
            this.Controls.Add(this.btnDeleteRequest);
            this.Controls.Add(this.btnApproveRecipe);
            this.Name = "UserRequestsForm";
            this.Text = "Запросы пользователей";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnApproveRecipe;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnDeleteRequest;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRequests;
    }
}