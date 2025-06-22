using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Supabase;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class RecipeDetailsForm : Form
    {
        private int recipeId;
        private readonly int _userId;
        private readonly bool _isAdmin;
        private readonly Timer _refreshTimer;
        public RecipeDetailsForm(int recipeId, int userId, bool isAdmin)
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            this.recipeId = recipeId;
            _userId = userId;
            _isAdmin = isAdmin;
            if (_isAdmin == true && userId == -1)
            {
                btnRateRecipe.Enabled = false;
            }
            _ = LoadRecipeDetails();
            _refreshTimer = new Timer { Interval = 10000 };
            _refreshTimer.Tick += async (_, __) => await LoadRecipeDetails();
            _refreshTimer.Start();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private async Task LoadRecipeDetails() //загрузка деталей
        {
            var rpcParams = new { p_id = recipeId };
            DataTable dt = await DatabaseHelper.ExecuteQuery("get_recipe_details", rpcParams);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Информация о рецепте не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            var row = dt.Rows[0];

            lblRecipeName.Text = $"Название: {row["recipe_name"].ToString()}";
            lblRecipeName.AutoSize = true;
            lblRecipeName.Size = new Size(320, lblRecipeName.PreferredHeight);

            lblCategory.Text = $"Категория: {row["category_name"]}";
            lblCategory.AutoSize = false;
            lblCategory.MaximumSize = new Size(320, 0);
            lblCategory.Size = new Size(320, lblCategory.PreferredHeight);

            txtDescription.Text = row["description"].ToString();
            txtDescription.Height = 110;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            ConfigureReadOnlyTextBox(txtDescription);

            txtIngredients.Text = row["ingredients"].ToString();
            txtIngredients.Height = 100;
            txtIngredients.ScrollBars = ScrollBars.Vertical;
            ConfigureReadOnlyTextBox(txtIngredients);

            decimal avg = 0;
            if (row.Table.Columns.Contains("average_rating") && row["average_rating"] != DBNull.Value)
                avg = Convert.ToDecimal(row["average_rating"]);
            btnRateRecipe.Text = $"★ {avg:F1}";

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
                        pictureBoxRecipe.Image = Image.FromStream(ms);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                pictureBoxRecipe.Image = null;
            }
        }
        private void ConfigureReadOnlyTextBox(TextBox textBox)
        {
            textBox.ReadOnly = true;
            textBox.TabStop = false;
            textBox.Multiline = true;
        }
        private void pictureBoxRecipe_Click(object sender, EventArgs e)
        {
        }
        private async void btnRateRecipe_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Поставьте оценку рецепту (1–5):","Оценить рецепт","5");
            if (!int.TryParse(input, out int rating) || rating < 1 || rating > 5)
            {
                MessageBox.Show("Пожалуйста, введите число от 1 до 5.", "Неверный формат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var rpcParams = new
            {
                p_user = _userId,
                p_recipe = recipeId,
                p_rating = rating
            };
            try
            {
                var dt = await DatabaseHelper.ExecuteRpcScalarAsync("rate_recipe", rpcParams);
                if (dt.HasValue)
                {
                    MessageBox.Show("Спасибо за вашу оценку!", "Оценка принята", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не удалось получить обновлённый рейтинг.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке рейтинга: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
