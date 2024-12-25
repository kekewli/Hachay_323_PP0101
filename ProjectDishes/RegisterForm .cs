using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

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
            AppStyle.ApplyStyle(this);
        }
        private void btnToLogin_Click(object sender, EventArgs e) //переход на авторизацию
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
        private void btnLogin_Click(object sender, EventArgs e) //регистрация
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Введите все необходимые данные.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string hashedPassword = HashPassword(password);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", hashedPassword),
                new SqlParameter("@Email", email),
            };
            try
            {

                int result = DatabaseHelper.ExecuteNonQueryWithReturnValue("RegisterUser", parameters);

                if (result == 0)
                {
                    MessageBox.Show("Пользователь успешно зарегистрирован!", "Регистрация завершена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Hide();
                }
                else if (result == 1)
                {
                    MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (result == 2)
                {
                    MessageBox.Show("Пользователь с такой электронной почтой уже существует.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Произошла ошибка при регистрации. Попробуйте еще раз.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string HashPassword(string password) //хэширование пароля
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private bool IsValidEmail(string email) //проверка емайла
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
