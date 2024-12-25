using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace ProjectDishes
{
    public partial class UserStorageForm : Form
    {
        private int userId;
        private int? selectedRecipeId;
        public UserStorageForm(int userId)
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            this.userId = userId;
            LoadUserRecipes();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }
        private void LoadUserRecipes() //загрузка рецептов пользователей
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };
            DataTable userRecipes = DatabaseHelper.ExecuteQuery("GetUserRecipes", parameters);
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
            detailsButton.Click += (sender, e) => OpenRecipeDetails((int)panel.Tag);
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
            selectedRecipeId = (int?)panel.Tag;
        }
        private void btnDeleteRecipe_Click_1(object sender, EventArgs e) //удаление
        {
            if (!selectedRecipeId.HasValue)
            {
                MessageBox.Show("Пожалуйста, выберите рецепт для удаления.");
                return;
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserID", userId),
        new SqlParameter("@RecipeID", selectedRecipeId.Value)
            };
            if (DatabaseHelper.ExecuteNonQuery("DeleteRecipeFromUserStorage", parameters))
            {
                MessageBox.Show("Рецепт удален из избранного.");
                LoadUserRecipes();
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e) //поиск
        {
            string keyword = txtSearch.Text.Trim();
            SearchUserRecipes(keyword);
        }
        private void SearchUserRecipes(string keyword) //метод поиска
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadUserRecipes();
                return;
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Keyword", keyword)
            };
            DataTable results = DatabaseHelper.ExecuteQuery("SearchUserRecipes", parameters);
            flowLayoutPanelUserRecipes.Controls.Clear();

            foreach (DataRow row in results.Rows)
            {
                Panel recipePanel = CreateRecipePanel(row);
                flowLayoutPanelUserRecipes.Controls.Add(recipePanel);
            }
        }
    }
}
