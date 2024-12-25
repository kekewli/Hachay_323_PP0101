using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace ProjectDishes
{
    public partial class UpdateUserForm : Form
    {
        private int userId;
        public UpdateUserForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserDetails();
            InitializeDataGridView();
            AppStyle.ApplyStyle(this);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AppStyle.ApplyStyle(this);
        }
        private void LoadUserDetails() //загрузка деталей пользователей
        {
            SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
            DataTable userTable = DatabaseHelper.ExecuteQuery("GetUserById", parameters);
            if (userTable.Rows.Count > 0)
            {
                txtUserName.Text = userTable.Rows[0]["UserName"].ToString();
                txtEmail.Text = userTable.Rows[0]["Email"].ToString();
            }
        }
        private void InitializeDataGridView()
        {
            LoadUserDetails();
        }
        private void btnSave_Click(object sender, EventArgs e) //сохранение
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
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", hashedPassword ?? (object)DBNull.Value),
                new SqlParameter("@Email", email)
            };
            try
            {
                DatabaseHelper.ExecuteNonQuery("UpdateUser", parameters);
                MessageBox.Show("Данные пользователя успешно обновлены.", "Обновление завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string HashPassword(string password) //хэширование пароля
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
