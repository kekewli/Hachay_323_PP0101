
namespace ProjectDishes
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            btnToLogin = new System.Windows.Forms.Button();
            btnLogin = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            txtPassword = new System.Windows.Forms.TextBox();
            txtUserName = new System.Windows.Forms.TextBox();
            btnExit = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // btnToLogin
            // 
            btnToLogin.Location = new System.Drawing.Point(124, 303);
            btnToLogin.Name = "btnToLogin";
            btnToLogin.Size = new System.Drawing.Size(180, 37);
            btnToLogin.TabIndex = 15;
            btnToLogin.Text = "Зарегистрировать";
            btnToLogin.UseVisualStyleBackColor = true;
            btnToLogin.Click += btnToLogin_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new System.Drawing.Point(124, 246);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(180, 51);
            btnLogin.TabIndex = 14;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.ForeColor = System.Drawing.Color.White;
            label2.Location = new System.Drawing.Point(124, 157);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 20);
            label2.TabIndex = 13;
            label2.Text = "Пароль";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(124, 89);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 20);
            label1.TabIndex = 12;
            label1.Text = "Логин";
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(124, 195);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(180, 27);
            txtPassword.TabIndex = 11;
            // 
            // txtUserName
            // 
            txtUserName.Location = new System.Drawing.Point(124, 122);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new System.Drawing.Size(180, 27);
            txtUserName.TabIndex = 10;
            // 
            // btnExit
            // 
            btnExit.Location = new System.Drawing.Point(124, 360);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(180, 37);
            btnExit.TabIndex = 16;
            btnExit.Text = "Выйи";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(456, 543);
            Controls.Add(btnExit);
            Controls.Add(btnToLogin);
            Controls.Add(btnLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPassword);
            Controls.Add(txtUserName);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "LoginForm";
            Text = "Авторизация";
            FormClosed += LoginForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnToLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnExit;
    }
}