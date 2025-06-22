using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class UsersForm : Form
    {
        private readonly Timer _refreshTimer;
        public UsersForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            _ = LoadUsers();
            _refreshTimer = new Timer { Interval = 10000 };
            _refreshTimer.Tick += async (_, __) => await LoadUsers();
            _refreshTimer.Start();
            AppStyle.ApplyDataGridViewStyle(dataGridViewUsers);
            AppStyle.ApplyStyle(this);
        }
        private async Task LoadUsers() //загрузка пользователей
        {
            DataTable users = await DatabaseHelper.ExecuteQuery("get_all_users");
            if (users == null || users.Columns.Count == 0)
            {
                return;
            }
            dataGridViewUsers.DataSource = users;
            if (dataGridViewUsers.Columns.Contains("role_id"))
                dataGridViewUsers.Columns["role_id"].Visible = false;
            if (dataGridViewUsers.Columns.Contains("is_admin"))
                dataGridViewUsers.Columns["is_admin"].Visible = false;
            if (dataGridViewUsers.Columns.Contains("role_name"))
                dataGridViewUsers.Columns["role_name"].HeaderText = "Роль";
            if (dataGridViewUsers.Columns.Contains("user_name"))
                dataGridViewUsers.Columns["user_name"].HeaderText = "Имя пользователя";
            if (dataGridViewUsers.Columns.Contains("password_hash"))
                dataGridViewUsers.Columns["password_hash"].HeaderText = "Пароль";
            if (dataGridViewUsers.Columns.Contains("email"))
                dataGridViewUsers.Columns["email"].HeaderText = "Электронная почта";
            if (dataGridViewUsers.Columns.Contains("user_id"))
                dataGridViewUsers.Columns["user_id"].HeaderText = "ID пользователя";
        }
        private async void txtSearch_TextChanged(object sender, EventArgs e) //поиск
        {
            string keyword = txtSearch.Text.Trim();
            var rpcParams = new { p_key = keyword };
            DataTable users = await DatabaseHelper.ExecuteQuery("search_users", rpcParams);
            if (users == null || users.Columns.Count == 0)
            {
                return;
            }
            dataGridViewUsers.DataSource = users;
            if (dataGridViewUsers.Columns.Contains("role_id"))
                dataGridViewUsers.Columns["role_id"].Visible = false;
            if (dataGridViewUsers.Columns.Contains("role_name"))
                dataGridViewUsers.Columns["role_name"].HeaderText = "Роль";
            if (dataGridViewUsers.Columns.Contains("user_name"))
                dataGridViewUsers.Columns["user_name"].HeaderText = "Имя пользователя";
            if (dataGridViewUsers.Columns.Contains("password_hash"))
                dataGridViewUsers.Columns["password_hash"].HeaderText = "Пароль";
            if (dataGridViewUsers.Columns.Contains("email"))
                dataGridViewUsers.Columns["email"].HeaderText = "Электронная почта";
            if (dataGridViewUsers.Columns.Contains("user_id"))
                dataGridViewUsers.Columns["user_id"].HeaderText = "ID пользователя";
        }
        private void btnUpdateUser_Click(object sender, EventArgs e) //изменение 
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["user_id"].Value);
            var form = new UpdateUserForm(userId);
            form.ShowDialog();
            _ = LoadUsers(); 
        }
        private async void btnDeleteUser_Click(object sender, EventArgs e) //удаление
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["user_id"].Value);
            var rpcParams = new { p_user = userId };
            bool ok = await DatabaseHelper.ExecuteNonQuery("delete_user_and_recipes", rpcParams);  // snake_case
            if (ok)
            {
                MessageBox.Show("Пользователь и его рецепты удалены.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ = LoadUsers();
            }
            else
            {
                MessageBox.Show("Не удалось удалить пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e) //датагрид
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string col = dataGridViewUsers.Columns[e.ColumnIndex].Name;
            if (col == "user_name" || col == "email")
            {
                int userId = Convert.ToInt32(dataGridViewUsers.Rows[e.RowIndex].Cells["user_id"].Value);
                var form = new UpdateUserForm(userId);
                form.ShowDialog();
                _ = LoadUsers();
            }
        }
        private async void btnSetAdminRights_Click_1(object sender, EventArgs e) //права администратора
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для изменения прав.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int userId = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["user_id"].Value);
            int currentRole = Convert.ToInt32(dataGridViewUsers.SelectedRows[0].Cells["role_id"].Value);
            int newRole = (currentRole == 1) ? 2 : 1;
            var rpcParams = new { p_user_id = userId, p_new_role_id = newRole };
            bool ok = await DatabaseHelper.ExecuteNonQuery("set_admin_rights", rpcParams);  // snake_case
            if (ok)
            {
                MessageBox.Show("Права пользователя обновлены.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _= LoadUsers();
            }
            else
            {
                MessageBox.Show("Не удалось обновить права.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
        }
    }
}
