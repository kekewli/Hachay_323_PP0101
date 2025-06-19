using System;
using System.Data;
using Npgsql;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class UserStorageForm : Form
    {
        private int userId;
        private int? selectedRecipeId;
        public UserStorageForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AppStyle.ApplyStyle(this);
            this.Shown += UserStorageForm_Shown;
        }
        private async void UserStorageForm_Shown(object sender, EventArgs e)
        {
            await LoadUserRecipes();
        }
        private async Task LoadUserRecipes() //загрузка рецептов пользователей
        {
            var rpcParams = new { p_user = userId };
            DataTable userRecipes = await DatabaseHelper.ExecuteQuery("get_user_recipes", rpcParams);
            flowLayoutPanelUserRecipes.Controls.Clear();
            foreach (DataRow row in userRecipes.Rows)
            {
                Panel recipePanel = CreateRecipePanel(row);
                flowLayoutPanelUserRecipes.Controls.Add(recipePanel);
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
            Label nameLabel = new Label
            {
                Text = row["recipe_name"].ToString(),
                Location = new Point(100, 10),
                Size = new Size(200, 50),
                Font = new Font("Arial", 10),
                AutoEllipsis = true
            };
            Button detailsButton = new Button
            {
                Text = "Подробнее",
                Location = new Point(100, 60),
                Size = new Size(100, 30)
            };
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(detailsButton);
            panel.Click += (sender, e) => SelectRecipePanel(panel);
            pictureBox.Click += (sender, e) => SelectRecipePanel(panel);
            nameLabel.Click += (sender, e) => SelectRecipePanel(panel);
            detailsButton.Click += (sender, e) => OpenRecipeDetails(Convert.ToInt32(panel.Tag));
            return panel;
        }
        private void OpenRecipeDetails(int recipeId) //открытие деталей
        {
            RecipeDetailsForm detailsForm = new RecipeDetailsForm(recipeId);
            detailsForm.ShowDialog();
        }
        private void SelectRecipePanel(Panel panel) //выбор панели
        {
            foreach (Control control in flowLayoutPanelUserRecipes.Controls)
            {
                if (control is Panel otherPanel)
                {
                    otherPanel.BackColor = SystemColors.Control;
                }
            }
            panel.BackColor = Color.LightBlue;
            selectedRecipeId = Convert.ToInt32(panel.Tag);
        }
        private async void btnDeleteRecipe_Click_1(object sender, EventArgs e) //удаление
        {
            if (!selectedRecipeId.HasValue)
            {
                MessageBox.Show("Пожалуйста, выберите рецепт для удаления.");
                return;
            }

            var rpcParams = new
            {
                p_user = userId,
                p_recipe = selectedRecipeId.Value
            };

            if (await DatabaseHelper.ExecuteNonQuery("delete_recipe_from_user_storage", rpcParams))
            {
                MessageBox.Show("Рецепт удален из избранного.");
                await LoadUserRecipes();
            }
            else
            {
                MessageBox.Show("Ошибка при удалении рецепта.");
            }
        }
        private async void txtSearch_TextChanged(object sender, EventArgs e) //поиск
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                await LoadUserRecipes();
                return;
            }
            await SearchUserRecipes(keyword);
        }
        private async Task SearchUserRecipes(string keyword) //метод поиска
        {
            var rpcParams = new
            {
                p_user = userId,
                p_key = keyword
            };
            DataTable results = await DatabaseHelper.ExecuteQuery("search_user_recipes", rpcParams);
            flowLayoutPanelUserRecipes.Controls.Clear();
            foreach (DataRow row in results.Rows)
            {
                Panel recipePanel = CreateRecipePanel(row);
                flowLayoutPanelUserRecipes.Controls.Add(recipePanel);
            }
        }
    }
}
