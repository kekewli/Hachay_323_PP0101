using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
namespace ProjectDishes
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtPassword.PasswordChar = '*';
        }
        private void btnToLogin_Click(object sender, EventArgs e) //пехезод к форме регистрации
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
        private async void btnLogin_Click_1(object sender, EventArgs e) //кнопка авторизации
        {
            var userName = txtUserName.Text.Trim();
            var password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var hashedPassword = HashPassword(password);
            var rpcParams = new { p_name = userName, p_pass = hashedPassword };
            DataTable result = await DatabaseHelper.ExecuteQuery("login_user", rpcParams);
            if (result.Rows.Count > 0)
            {
                int roleId = Convert.ToInt32(result.Rows[0]["role_id"]);
                int userId = Convert.ToInt32(result.Rows[0]["user_id"]);
                if (roleId == 1)
                {
                    var adminForm = new AdminForm();
                    adminForm.Show();
                }
                else
                {
                    var userForm = new UserForm(userId);
                    userForm.Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string HashPassword(string password) //хеширование пароля
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach (var b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
