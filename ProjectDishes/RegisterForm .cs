using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectDishes
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void btnToLogin_Click(object sender, EventArgs e) //переход на авторизацию
        {
            new LoginForm().Show();
            this.Hide();
        }
        private async void btnLogin_Click(object sender, EventArgs e) //регистрация
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Введите все необходимые данные.", "Ошибка регистрации",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка регистрации",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string hashedPassword = HashPassword(password);
            var rpcParams = new
            {
                p_name = userName,
                p_pass = hashedPassword,
                p_email = email
            };
            int result = await Task.Run(() =>
                DatabaseHelper.ExecuteNonQueryWithReturnValue("register_user", rpcParams)
            );
            switch (result)
            {
                case 0:
                    MessageBox.Show("Пользователь успешно зарегистрирован!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new LoginForm().Show();
                    this.Hide();
                    break;
                case 1:
                    MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 2:
                    MessageBox.Show("Пользователь с таким email уже существует.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("Неизвестная ошибка регистрации.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        private string HashPassword(string password) //хэширование пароля
        {
            using var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();
            foreach (var b in hash) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
        private bool IsValidEmail(string email) //проверка емайла
        {
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
