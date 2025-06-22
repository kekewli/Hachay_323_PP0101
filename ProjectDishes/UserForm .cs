using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace ProjectDishes
{
    public partial class UserForm : Form
    {
        private int userId;
        private int? selectedRecipeId;
        private readonly Timer _refreshTimer;
        public UserForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            _ = LoadRecipes();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            _refreshTimer = new Timer { Interval = 10000 };
            _refreshTimer.Tick += async (_, __) => await LoadRecipes();
            _refreshTimer.Start();
            AppStyle.ApplyStyle(this);
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
            ratingLabel.Click += (s, e) => SelectRecipePanel(panel);
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
            bool isAdmin = false;
            try
            {
                var detailsForm = new RecipeDetailsForm(recipeId, userId, isAdmin);
                detailsForm.ShowDialog();
            }
            catch { }
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
            bool added;
            try
            {
                added = await DatabaseHelper.ExecuteNonQuery("add_recipe_to_user_storage", rpcParams);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (added)
            {
                MessageBox.Show("Рецепт добавлен в избранное.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Этот рецепт уже находится в вашем хранилище.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnOpenStorage_Click(object sender, EventArgs e) //открытие избранного
        {
            try
            {
                var storageForm = new UserStorageForm(userId);
                storageForm.ShowDialog();
            }
            catch {  }


        }
        private async void btnCreateRecipe_Click(object sender, EventArgs e) //создание рецепта
        {
            using var createRecipeForm = new CreateRecipeForm(userId);
            if (createRecipeForm.ShowDialog() == DialogResult.OK)
            {
                await LoadRecipes(); 
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }
        private async void txtSearch_TextChanged(object sender, EventArgs e) //поиск 
        {
            string keyword = txtSearch.Text.Trim();
            var rpcParams = new { p_key = keyword };
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
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
        }
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadRecipes();
        }
    }
}
