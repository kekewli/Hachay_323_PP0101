using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class UpdateUserForm : Form
    {
        private int userId;
        public UpdateUserForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            _ = LoadUserDetails();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AppStyle.ApplyStyle(this);
        }
        private async Task LoadUserDetails() //загрузка деталей пользователей
        {
            var rpcParams = new { p_user = userId };
            DataTable dt = await DatabaseHelper.ExecuteQuery("get_user_by_id", rpcParams);

            if (dt.Rows.Count > 0)
            {
                txtUserName.Text = dt.Rows[0]["user_name"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
            }
        }
        private async void btnSave_Click(object sender, EventArgs e) //кнопка сохранения 
        {
            string userName = txtUserName.Text.Trim();
            string newPassword = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Введите имя пользователя и email.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string hashedPassword = string.IsNullOrEmpty(newPassword) ? null : HashPassword(newPassword);
            var rpcParams = new
            {
                p_user_id = userId,
                p_name = userName,
                p_pass = hashedPassword,
                p_em = email
            };
            bool success = await DatabaseHelper.ExecuteNonQuery("update_user", rpcParams);
            if (success)
            {
                MessageBox.Show("Данные пользователя успешно обновлены.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось обновить данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
