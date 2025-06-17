using System;
using System.Data;
using Npgsql;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Supabase.Postgrest;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class AdminForm : Form
    {
        private int? selectedRecipeId;
        public AdminForm()
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            _=LoadRecipes();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private async Task LoadRecipes() //загрузка рецептов
        {
            var rpcParams = new { };
            DataTable recipes = await DatabaseHelper.ExecuteQuery("get_all_recipes", rpcParams);

            flowLayoutPanelRecipes.Controls.Clear();
            foreach (DataRow row in recipes.Rows)
            {
                Panel recipePanel = CreateRecipePanel(row);
                flowLayoutPanelRecipes.Controls.Add(recipePanel);
            }
        }
        private Panel CreateRecipePanel(DataRow row) //создание панелей
        {
            Panel panel = new Panel
            {
                Size = new Size(300, 100),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = row["recipe_id"]
            };
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            if (row["image"] != DBNull.Value)
            {
                byte[] imageData = (byte[])row["image"];
                using (var ms = new MemoryStream(imageData))
                    pictureBox.Image = Image.FromStream(ms);
            }
            Label nameLabel = new Label
            {
                Text = row["recipe_name"].ToString(),
                Location = new Point(100, 10),
                Size = new Size(200, 50),
                Font = new Font("Arial", 10, FontStyle.Regular),
                AutoEllipsis = true
            };
            Button viewButton = new Button
            {
                Text = "Подробнее",
                Location = new Point(100, 60),
                Size = new Size(100, 30)
            };
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(viewButton);
            panel.Click += (sender, e) => SelectRecipePanel(panel);
            pictureBox.Click += (sender, e) => SelectRecipePanel(panel);
            nameLabel.Click += (sender, e) => SelectRecipePanel(panel);
            viewButton.Click += (sender, e) => OpenRecipeDetails((int)panel.Tag);
            return panel;
        }
        private void SelectRecipePanel(Panel panel) //выбор панели
        {
            foreach (Control control in flowLayoutPanelRecipes.Controls)
            {
                if (control is Panel otherPanel)
                {
                    otherPanel.BackColor = SystemColors.Control;
                }
            }
            panel.BackColor = Color.LightBlue;
            selectedRecipeId = (int?)panel.Tag;
        }
        private void OpenRecipeDetails(int recipeId) //открытие деталей рецепта
        {
            var detailsForm = new RecipeDetailsForm(recipeId);
            detailsForm.ShowDialog();
        }
        private void btnAddRecipe_Click(object sender, EventArgs e) //добавление
        {
            var recipeForm = new RecipeForm();
            recipeForm.ShowDialog();
            _ = LoadRecipes();
        }
        private async void btnEditRecipe_Click(object sender, EventArgs e) //изменение
        {
            if (selectedRecipeId == null)
            {
                MessageBox.Show("Выберите рецепт для редактирования.");
                return;
            }
            var editForm = new EditRecipeForm(selectedRecipeId.Value);
            editForm.ShowDialog();
            await LoadRecipes();
        }
        private async void btnDeleteRecipe_Click(object sender, EventArgs e) //удаление
        {
            if (selectedRecipeId == null)
            {
                MessageBox.Show("Выберите рецепт для удаления.");
                return;
            }
            var rpcParams = new { p0 = selectedRecipeId.Value };
            bool ok = await DatabaseHelper.ExecuteNonQuery("delete_recipe", rpcParams);
            if (ok)
                MessageBox.Show($"Рецепт с ID {selectedRecipeId} удалён.");
            else
                MessageBox.Show($"Не удалось удалить рецепт с ID {selectedRecipeId}.");
            await LoadRecipes();
        }
        private void btnUserRequests_Click(object sender, EventArgs e) //запросы
        {
            var userRequests = new UserRequestsForm();
            userRequests.ShowDialog();
            _ = LoadRecipes();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            var login = new LoginForm();
            login.Show();
            this.Hide();
        }
        private void btnUsers_Click(object sender, EventArgs e)
        {
            var usersForm = new UsersForm();
            usersForm.ShowDialog();

        }
        private async void txtSearch_TextChanged(object sender, EventArgs e) //поиск
        {
            var keyword = txtSearch.Text.Trim();
            var rpcParams = new { p0 = keyword };
            DataTable filtered = await DatabaseHelper.ExecuteQuery("search_recipes", rpcParams);

            flowLayoutPanelRecipes.Controls.Clear();
            foreach (DataRow row in filtered.Rows)
            {
                flowLayoutPanelRecipes.Controls.Add(CreateRecipePanel(row));
            }
        }
        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e) //выход
        {
            Application.Exit();
        }
    }
}
