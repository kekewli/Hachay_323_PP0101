using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            LoadRecipes();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }
        private void LoadRecipes() //загрузка рецептов
        {
            DataTable recipes = DatabaseHelper.ExecuteQuery("GetAllRecipes");
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
                Tag = row["RecipeID"]
            };

            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            if (row["Image"] != DBNull.Value)
            {
                byte[] imageData = (byte[])row["Image"];
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pictureBox.Image = Image.FromStream(ms);
                }
            }

            Label nameLabel = new Label
            {
                Text = row["RecipeName"].ToString(),
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
            RecipeDetailsForm detailsForm = new RecipeDetailsForm(recipeId);
            detailsForm.ShowDialog();
        }
        private void btnAddRecipe_Click(object sender, EventArgs e) //добавление
        {
            RecipeForm recipeForm = new RecipeForm();
            recipeForm.ShowDialog();
            LoadRecipes();
        }
        private void btnEditRecipe_Click(object sender, EventArgs e) //изменение
        {
            if (selectedRecipeId == null)
            {
                MessageBox.Show("Выберите рецепт для редактирования.");
                return;
            }
            EditRecipeForm editRecipeForm = new EditRecipeForm((int)selectedRecipeId);
            editRecipeForm.ShowDialog();
            LoadRecipes();
        }
        private void btnDeleteRecipe_Click(object sender, EventArgs e) //удадение
        {
            if (selectedRecipeId == null)
            {
                MessageBox.Show("Выберите рецепт для удаления.");
                return;
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RecipeID", selectedRecipeId)
            };
            if (DatabaseHelper.ExecuteNonQuery("DeleteRecipe", parameters))
            {
                MessageBox.Show($"Рецепт с ID {selectedRecipeId} удален.");
            }
            else
            {
                MessageBox.Show($"Не удалось удалить рецепт с ID {selectedRecipeId}.");
            }

            LoadRecipes();
        }
        private void btnUserRequests_Click(object sender, EventArgs e) //запросы
        {
            UserRequestsForm userRequestsForm = new UserRequestsForm();
            userRequestsForm.ShowDialog();
            LoadRecipes();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
        private void btnUsers_Click(object sender, EventArgs e)
        {
            UsersForm usersForm = new UsersForm();
            usersForm.ShowDialog();

        }
        private void txtSearch_TextChanged(object sender, EventArgs e) //поиск
        {
            string keyword = txtSearch.Text.Trim();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", keyword)
            };

            DataTable filteredRecipes = DatabaseHelper.ExecuteQuery("SearchRecipes", parameters);
            flowLayoutPanelRecipes.Controls.Clear();

            foreach (DataRow row in filteredRecipes.Rows)
            {
                Panel recipePanel = CreateRecipePanel(row);
                flowLayoutPanelRecipes.Controls.Add(recipePanel);
            }
        }
        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e) //выход
        {
            Application.Exit();
        }
    }
}
