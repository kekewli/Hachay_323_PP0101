
namespace ProjectDishes
{
    partial class RegisterForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            txtUserName = new System.Windows.Forms.TextBox();
            txtPassword = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            btnLogin = new System.Windows.Forms.Button();
            btnToLogin = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            txtEmail = new System.Windows.Forms.TextBox();
            btnExit = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // txtUserName
            // 
            txtUserName.Location = new System.Drawing.Point(124, 122);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new System.Drawing.Size(180, 27);
            txtUserName.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(124, 195);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(180, 27);
            txtPassword.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(124, 89);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 20);
            label1.TabIndex = 3;
            label1.Text = "Логин";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.ForeColor = System.Drawing.Color.White;
            label2.Location = new System.Drawing.Point(124, 157);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 20);
            label2.TabIndex = 4;
            label2.Text = "Пароль";
            // 
            // btnLogin
            // 
            btnLogin.Location = new System.Drawing.Point(124, 314);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(180, 51);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Зарегистрировать";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnToLogin
            // 
            btnToLogin.Location = new System.Drawing.Point(124, 371);
            btnToLogin.Name = "btnToLogin";
            btnToLogin.Size = new System.Drawing.Size(180, 37);
            btnToLogin.TabIndex = 7;
            btnToLogin.Text = "Войти";
            btnToLogin.UseVisualStyleBackColor = true;
            btnToLogin.Click += btnToLogin_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.ForeColor = System.Drawing.Color.White;
            label3.Location = new System.Drawing.Point(124, 231);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(46, 20);
            label3.TabIndex = 9;
            label3.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new System.Drawing.Point(124, 265);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(180, 27);
            txtEmail.TabIndex = 8;
            txtEmail.TextChanged += txtEmail_TextChanged;
            // 
            // btnExit
            // 
            btnExit.Location = new System.Drawing.Point(124, 425);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(180, 37);
            btnExit.TabIndex = 10;
            btnExit.Text = "Выйти";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Lavender;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(456, 543);
            Controls.Add(btnExit);
            Controls.Add(label3);
            Controls.Add(txtEmail);
            Controls.Add(btnToLogin);
            Controls.Add(btnLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPassword);
            Controls.Add(txtUserName);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "RegisterForm";
            Text = "Регистрация";
            FormClosed += RegisterForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnToLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnExit;
    }
}

