using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class UserForm : Form
    {
        private int userId;
        private int? selectedRecipeId;
        public UserForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            _ = LoadRecipes();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AppStyle.ApplyStyle(this);
        }
        private async Task LoadRecipes() //загрузка рецептов
        {
            DataTable recipes = await DatabaseHelper.ExecuteQuery("get_all_recipes");
            flowLayoutPanelRecipes.Controls.Clear();
            foreach (DataRow row in recipes.Rows)
            {
                Panel recipePanel = CreateRecipePanel(row);
                flowLayoutPanelRecipes.Controls.Add(recipePanel);
            }
        }
        private Panel CreateRecipePanel(DataRow row) //создание панелей
        {
            var panel = new Panel
            {
                Size = new Size(300, 100),
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
                byte[] imageData = (byte[])row["image"];
                using var ms = new MemoryStream(imageData);
                pictureBox.Image = Image.FromStream(ms);
            }
            var nameLabel = new Label
            {
                Text = row["recipe_name"].ToString(),
                Location = new Point(100, 10),
                Size = new Size(200, 50),
                Font = new Font("Arial", 10, FontStyle.Regular),
                AutoEllipsis = true
            };
            var viewButton = new Button
            {
                Text = "Подробнее",
                Location = new Point(100, 60),
                Size = new Size(100, 30)
            };
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(viewButton);
            panel.Click += (s, e) => SelectRecipePanel(panel);
            pictureBox.Click += (s, e) => SelectRecipePanel(panel);
            nameLabel.Click += (s, e) => SelectRecipePanel(panel);
            viewButton.Click += (s, e) => OpenRecipeDetails(Convert.ToInt32(panel.Tag));
            return panel;
        }
        private void SelectRecipePanel(Panel panel) //выбор панелей
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
            var detailsForm = new RecipeDetailsForm(recipeId);
            detailsForm.ShowDialog();
        }
        private async void btnAddToStorage_Click(object sender, EventArgs e) //сохранение в избранное
        {
            if (selectedRecipeId == null)
            {
                MessageBox.Show("Выберите рецепт для добавления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var rpcParams = new
            {
                p_user = userId,
                p_recipe = selectedRecipeId.Value
            };
            bool ok = await DatabaseHelper.ExecuteNonQuery("add_recipe_to_user_storage", rpcParams); // snake_case
            if (ok)
                MessageBox.Show("Рецепт добавлен в избранное.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Не удалось добавить рецепт.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnOpenStorage_Click(object sender, EventArgs e) //открытие избранного
        {
            var storageForm = new UserStorageForm(userId);
            storageForm.ShowDialog();
        }
        private void btnCreateRecipe_Click(object sender, EventArgs e) //создание рецепта
        {
            var createRecipeForm = new CreateRecipeForm(userId);
            createRecipeForm.ShowDialog();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }
        private async void txtSearch_TextChanged(object sender, EventArgs e) //поиск 
        {
            string keyword = txtSearch.Text.Trim();
            var rpcParams = new { p0 = keyword };
            DataTable filtered = await DatabaseHelper.ExecuteQuery("search_recipes", rpcParams);
            flowLayoutPanelRecipes.Controls.Clear();
            foreach (DataRow row in filtered.Rows)
            {
                var panel = CreateRecipePanel(row);
                flowLayoutPanelRecipes.Controls.Add(panel);
            }
        }
        private void UserForm_Load(object sender, EventArgs e)
        {
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
