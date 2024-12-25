using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace ProjectDishes
{
    public partial class UserForm : Form
    {
        private int userId;
        private int? selectedRecipeId;
        public UserForm(int userId)
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            this.userId = userId;
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
        private Panel CreateRecipePanel(DataRow row) //создание панеелй
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
            selectedRecipeId = (int?)panel.Tag;
        }
        private void OpenRecipeDetails(int recipeId) //открытие деталей рецепта
        {
            RecipeDetailsForm detailsForm = new RecipeDetailsForm(recipeId);
            detailsForm.ShowDialog();
        }
        private void btnAddToStorage_Click(object sender, EventArgs e) //сохранение в избранное
        {
            Panel selectedPanel = flowLayoutPanelRecipes.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => p.BackColor == Color.LightBlue);
            if (selectedPanel == null)
            {
                MessageBox.Show("Выберите рецепт для добавления.");
                return;
            }
            int recipeId = (int)selectedPanel.Tag;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@RecipeID", recipeId)
            };

            if (DatabaseHelper.ExecuteNonQuery("AddRecipeToUserStorage", parameters))
            {
                MessageBox.Show("Рецепт добавлен в избранное.");
            }
        }
        private void btnOpenStorage_Click(object sender, EventArgs e) //открытие избранного
        {
            UserStorageForm storageForm = new UserStorageForm(userId);
            storageForm.ShowDialog();
        }
        private void btnCreateRecipe_Click(object sender, EventArgs e) //создание рецепта
        {
            CreateRecipeForm createRecipeForm = new CreateRecipeForm(userId);
            createRecipeForm.ShowDialog();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
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
        private void UserForm_Load(object sender, EventArgs e)
        {
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
