using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class SubmitRecipeForm : Form
    {
        private int userId;
        public SubmitRecipeForm(int userId)
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            this.userId = userId;
            _ = LoadCategories();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private async Task LoadCategories() //загрузка категорий
        {
            DataTable categories = await DatabaseHelper.ExecuteQuery("get_categories");
            comboBoxCategory.DataSource = categories;
            comboBoxCategory.DisplayMember = "category_name";
            comboBoxCategory.ValueMember = "category_id";
        }
        private async void btnSubmitRecipe_Click_1(object sender, EventArgs e) //кнопка подтверждения
        {
            string recipeName = txtRecipeName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string ingredients = txtIngredients.Text.Trim();
            int categoryId = Convert.ToInt32(comboBoxCategory.SelectedValue);
            if (string.IsNullOrEmpty(recipeName) ||
                string.IsNullOrEmpty(description) ||
                string.IsNullOrEmpty(ingredients))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var rpcParams = new
            {
                p0 = userId,
                p1 = recipeName,
                p2 = description,
                p3 = ingredients,
                p4 = categoryId
            };
            bool success = await DatabaseHelper.ExecuteNonQuery("submit_user_recipe", rpcParams);
            if (success)
            {
                MessageBox.Show("Ваш рецепт отправлен на рассмотрение.", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось отправить рецепт. Попробуйте позже.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
