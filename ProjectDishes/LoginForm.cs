using System;
using System.Data;
using System.Data.SqlClient;
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
            AppStyle.ApplyStyle(this);
        }
        private void btnToLogin_Click(object sender, EventArgs e) //перезод к регистрации
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string hashedPassword = HashPassword(password);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", hashedPassword)
            };
            DataTable result = DatabaseHelper.ExecuteQuery("LoginUser", parameters);
            if (result.Rows.Count > 0)
            {
                int roleID = Convert.ToInt32(result.Rows[0]["RoleID"]);
                if (roleID == 1)
                {
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                }
                else
                {
                    UserForm userForm = new UserForm(Convert.ToInt32(result.Rows[0]["UserID"]));
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
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder result = new StringBuilder();
                foreach (byte b in hash)
                {
                    result.Append(b.ToString("x2"));
                }
                return result.ToString();
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
