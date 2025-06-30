
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserRequestsForm));
            btnApproveRecipe = new System.Windows.Forms.Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btnDeleteRequest = new System.Windows.Forms.Button();
            flowLayoutPanelRequests = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // btnApproveRecipe
            // 
            btnApproveRecipe.Location = new System.Drawing.Point(719, 13);
            btnApproveRecipe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnApproveRecipe.Name = "btnApproveRecipe";
            btnApproveRecipe.Size = new System.Drawing.Size(162, 42);
            btnApproveRecipe.TabIndex = 1;
            btnApproveRecipe.Text = "Добавить";
            btnApproveRecipe.UseVisualStyleBackColor = true;
            btnApproveRecipe.Click += btnApproveRecipe_Click;
            // 
            // btnDeleteRequest
            // 
            btnDeleteRequest.Location = new System.Drawing.Point(719, 63);
            btnDeleteRequest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnDeleteRequest.Name = "btnDeleteRequest";
            btnDeleteRequest.Size = new System.Drawing.Size(162, 42);
            btnDeleteRequest.TabIndex = 2;
            btnDeleteRequest.Text = "Удалить";
            btnDeleteRequest.UseVisualStyleBackColor = true;
            btnDeleteRequest.Click += btnDeleteRequest_Click;
            // 
            // flowLayoutPanelRequests
            // 
            flowLayoutPanelRequests.AutoScroll = true;
            flowLayoutPanelRequests.Location = new System.Drawing.Point(12, 15);
            flowLayoutPanelRequests.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            flowLayoutPanelRequests.Name = "flowLayoutPanelRequests";
            flowLayoutPanelRequests.Size = new System.Drawing.Size(701, 532);
            flowLayoutPanelRequests.TabIndex = 3;
            // 
            // UserRequestsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            ClientSize = new System.Drawing.Size(888, 555);
            Controls.Add(flowLayoutPanelRequests);
            Controls.Add(btnDeleteRequest);
            Controls.Add(btnApproveRecipe);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "UserRequestsForm";
            Text = "Запросы пользователей";
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button btnApproveRecipe;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnDeleteRequest;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRequests;
    }
}