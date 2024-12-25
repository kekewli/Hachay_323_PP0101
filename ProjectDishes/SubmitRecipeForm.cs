using System;
using System.Data;
using System.Data.SqlClient;
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
            LoadCategories();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AppStyle.ApplyStyle(this);
        }
        private void LoadCategories() //загрузка категорий
        {
            DataTable categories = DatabaseHelper.ExecuteQuery("GetCategories");
            comboBoxCategory.DataSource = categories;
            comboBoxCategory.DisplayMember = "CategoryName";
            comboBoxCategory.ValueMember = "CategoryID";
        }
        private void btnSubmitRecipe_Click_1(object sender, EventArgs e) //подтверждение
        {
            string recipeName = txtRecipeName.Text;
            string description = txtDescription.Text;
            string ingredients = txtIngredients.Text;
            int categoryId = (int)comboBoxCategory.SelectedValue;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RecipeName", recipeName),
                new SqlParameter("@Description", description),
                new SqlParameter("@Ingredients", ingredients),
                new SqlParameter("@CategoryID", categoryId),
                new SqlParameter("@UserID", userId)
            };
            if (DatabaseHelper.ExecuteNonQuery("SubmitRecipeRequest", parameters))
            {
                MessageBox.Show("Ваш рецепт отправлен на рассмотрение.");
                this.Close();
            }
        }
    }
}
