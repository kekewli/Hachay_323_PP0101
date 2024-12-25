using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
            LoadUsers();
            AppStyle.ApplyStyle(this);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AppStyle.ApplyDataGridViewStyle(dataGridViewUsers);
            AppStyle.ApplyStyle(this);
        }
        private void LoadUsers() //загрухка пользователей
        {
            DataTable users = DatabaseHelper.ExecuteQuery("GetAllUsers");
            dataGridViewUsers.DataSource = users;
            dataGridViewUsers.Columns["RoleID"].Visible = false;
            dataGridViewUsers.Columns["IsAdmin"].Visible = false;
            dataGridViewUsers.Columns["RoleName"].HeaderText = "Роль";
            dataGridViewUsers.Columns["UserName"].HeaderText = "Имя пользователя";
            dataGridViewUsers.Columns["PasswordHash"].HeaderText = "Пароль";
            dataGridViewUsers.Columns["Email"].HeaderText = "Электронная почта";
        }
        private void txtSearch_TextChanged(object sender, EventArgs e) //поиск
        {
            string keyword = txtSearch.Text.Trim();
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Keyword", keyword)
            };
            dataGridViewUsers.DataSource = DatabaseHelper.ExecuteQuery("SearchUsers", parameters);
            dataGridViewUsers.Columns["RoleID"].Visible = false;
            dataGridViewUsers.Columns["RoleName"].HeaderText = "Роль";
            dataGridViewUsers.Columns["PasswordHash"].HeaderText = "Пароль";
            dataGridViewUsers.Columns["UserName"].HeaderText = "Имя пользователя";
            dataGridViewUsers.Columns["Email"].HeaderText = "Электронная почта";
        }
        private void btnUpdateUser_Click(object sender, EventArgs e) //изменение 
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для редактирования.");
                return;
            }
            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["UserID"].Value);
            UpdateUserForm updateUserForm = new UpdateUserForm(userId);
            updateUserForm.ShowDialog();
            LoadUsers();
        }
        private void btnDeleteUser_Click(object sender, EventArgs e) //удаление
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для удаления.");
                return;
            }
            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["UserID"].Value);
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserID", userId)
            };
            DatabaseHelper.ExecuteNonQuery("DeleteUserAndRecipes", parameters);
            MessageBox.Show("Пользователь и его рецепты удалены.");
            LoadUsers();
        }
        private void dataGridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e) //датагрид
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dataGridViewUsers.Columns[e.ColumnIndex].Name;
                if (columnName == "UserName" || columnName == "Email")
                {
                    int userId = Convert.ToInt32(dataGridViewUsers.Rows[e.RowIndex].Cells["UserID"].Value);
                    UpdateUserForm editUserForm = new UpdateUserForm(userId);
                    editUserForm.ShowDialog();
                    LoadUsers();
                }
            }
        }
        private void btnSetAdminRights_Click_1(object sender, EventArgs e) //права администратора
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для изменения прав.");
                return;
            }
            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["UserID"].Value);
            int currentRoleId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["RoleID"].Value);
            int newRoleId = (currentRoleId == 1) ? 2 : 1;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@RoleID", newRoleId)
            };
            DatabaseHelper.ExecuteNonQuery("SetAdminRights", parameters);
            MessageBox.Show("Права пользователя обновлены.");
            LoadUsers();
        }
    }
}
