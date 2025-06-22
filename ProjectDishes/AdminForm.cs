using System;
using System.Data;
using Npgsql;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Supabase.Postgrest;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class AdminForm : Form
    {
        private int? selectedRecipeId;
        private readonly Timer _refreshTimer;
        public AdminForm()
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            _=LoadRecipes();
            _refreshTimer = new Timer { Interval = 10000 };
            _refreshTimer.Tick += async (_, __) => await LoadRecipes();
            _refreshTimer.Start();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private async Task LoadRecipes() //загрузка рецептов
        {
            try
            {
                DataTable recipes = await DatabaseHelper.ExecuteQuery("get_all_recipes");
                flowLayoutPanelRecipes.Controls.Clear();
                foreach (DataRow row in recipes.Rows)
                {
                    Panel recipePanel = CreateRecipePanel(row);
                    flowLayoutPanelRecipes.Controls.Add(recipePanel);
                }
            }
            catch { }
        }
        private Panel CreateRecipePanel(DataRow row) //создание панелей
        {
            var panel = new Panel
            {
                Size = new Size(300, 120),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = row["recipe_id"]
            };
            var pictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            if (row["image"] != DBNull.Value)
            {
                byte[] imageData = null;
                if (row["image"] is byte[] bytes)
                {
                    imageData = bytes;
                }
                else if (row["image"] is string hexString && hexString.StartsWith(@"\x", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        string hex = hexString.Substring(2);
                        int len = hex.Length;
                        imageData = new byte[len / 2];
                        for (int i = 0; i < len; i += 2)
                            imageData[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                    }
                    catch
                    {
                        imageData = null;
                    }
                }
                if (imageData?.Length > 0)
                {
                    using var ms = new MemoryStream(imageData);
                    try
                    {
                        pictureBox.Image = Image.FromStream(ms);
                    }
                    catch
                    {
                    }
                }
            }
            var nameLabel = new Label
            {
                Text = row["recipe_name"].ToString(),
                Location = new Point(100, 10),
                Size = new Size(150, 50),
                Font = new Font("Arial", 10, FontStyle.Regular),
                AutoEllipsis = true
            };
            var rating = Convert.ToDecimal(row["average_rating"]);
            var ratingLabel = new Label
            {
                Text = $"★ {rating:F1}",
                Location = new Point(250, 60),
                Size = new Size(60, 20),
                Font = new Font("Arial", 9, FontStyle.Italic),
                ForeColor = Color.Goldenrod
            };
            var viewButton = new Button
            {
                Text = "Подробнее",
                Location = new Point(100, 60),
                Size = new Size(100, 30)
            };
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(ratingLabel);
            panel.Controls.Add(viewButton);
            panel.Click += (s, e) => SelectRecipePanel(panel);
            pictureBox.Click += (s, e) => SelectRecipePanel(panel);
            nameLabel.Click += (s, e) => SelectRecipePanel(panel);
            viewButton.Click += (s, e) => OpenRecipeDetails(Convert.ToInt32(panel.Tag));
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
            selectedRecipeId = Convert.ToInt32(panel.Tag);
        }
        private void OpenRecipeDetails(int recipeId) //открытие деталей рецепта
        {
            bool isAdmin = true;
            int userId = -1;
            try
            {
                var detailsForm = new RecipeDetailsForm(recipeId, userId, isAdmin);
                detailsForm.ShowDialog();
            }
            catch { }

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
                MessageBox.Show("Выберите рецепт для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Выберите рецепт для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var rpcParams = new { p_recipe = selectedRecipeId.Value };
            bool ok = await DatabaseHelper.ExecuteNonQuery("delete_recipe", rpcParams);
            if (ok)
                MessageBox.Show($"Рецепт с ID {selectedRecipeId} удалён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            var rpcParams = new { p_key = keyword };
            DataTable filtered = await DatabaseHelper.ExecuteQuery("search_recipes", rpcParams);
            flowLayoutPanelRecipes.Controls.Clear();
            foreach (DataRow row in filtered.Rows)
            {
                flowLayoutPanelRecipes.Controls.Add(CreateRecipePanel(row));
            }
        }
        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e) //выход
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            Application.Exit();
        }

    }
}
