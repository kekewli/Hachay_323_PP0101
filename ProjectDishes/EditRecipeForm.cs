using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class EditRecipeForm : Form
    {
        private int recipeId;
        private byte[] recipeImage = null;
        public EditRecipeForm(int recipeId)
        {
            InitializeComponent();
            this.recipeId = recipeId;
            AppStyle.ApplyStyle(this);
            LoadCategories();
            LoadRecipeDetails();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ConfigurePictureBox();
        }
        private void LoadCategories() //категории
        {
            var dt = DatabaseHelper.ExecuteQuery("get_categories").GetAwaiter().GetResult();
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "category_name";
            cmbCategory.ValueMember = "category_id";
        }
        private void LoadRecipeDetails() //детали рецепта
        {
            var rpcParams = new { p0 = recipeId };
            var recipeDetails = DatabaseHelper.ExecuteQuery("get_recipe_by_id", rpcParams) .GetAwaiter().GetResult();
            if (recipeDetails.Rows.Count > 0)
            {
                var row = recipeDetails.Rows[0];
                txtRecipeName.Text = row["recipe_name"].ToString();
                txtDescription.Text = row["description"].ToString(); 
                txtIngredients.Text = row["ingredients"].ToString(); 
                string categoryName = row["category_name"].ToString();
                cmbCategory.SelectedIndex = cmbCategory.FindStringExact(categoryName);
                if (row["image"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])row["image"];
                    using var ms = new MemoryStream(imageData);
                    pictureBoxRecipe.Image = Image.FromStream(ms);
                    pictureBoxRecipe.SizeMode = PictureBoxSizeMode.Zoom;
                    recipeImage = imageData;
                }
                else
                {
                    pictureBoxRecipe.Image = null;
                }
            }
        }
        private void ConfigurePictureBox()
        {
            pictureBoxRecipe.AllowDrop = true;
            pictureBoxRecipe.DragEnter += PictureBoxRecipe_DragEnter;
            pictureBoxRecipe.DragDrop += PictureBoxRecipe_DragDrop;
            pictureBoxRecipe.Click += PictureBoxRecipe_Click;
        }
        private void PictureBoxRecipe_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void PictureBoxRecipe_DragDrop(object sender, DragEventArgs e) //вставка картинки
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                try
                {
                    DisplayImage(files[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                }
            }
        }
        private void PictureBoxRecipe_Click(object sender, EventArgs e) //картинка
        {
            btnUploadImage_Click(sender, e);
        }
        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DisplayImage(openFileDialog.FileName);
            }
        }
        private void DisplayImage(string filePath) //вывод изображения
         {
            try
            {
                var img = Image.FromFile(filePath);
                pictureBoxRecipe.Image    = img;
                pictureBoxRecipe.SizeMode = PictureBoxSizeMode.Zoom;
                using var ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
                recipeImage = ms.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnSave_Click(object sender, EventArgs e) //сохранение
        {
            string recipeName = txtRecipeName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string ingredients = txtIngredients.Text.Trim();
            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            if (string.IsNullOrEmpty(recipeName) ||
                string.IsNullOrEmpty(description) ||
                string.IsNullOrEmpty(ingredients))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var rpcParams = new
            {
                p0 = recipeId,
                p1 = recipeName,
                p2 = description,
                p3 = ingredients,
                p4 = categoryId,
                p5 = recipeImage
            };
            bool ok = await DatabaseHelper.ExecuteNonQuery("update_recipe", rpcParams); // lower_snake_case
            if (ok)
            {
                MessageBox.Show("Рецепт успешно обновлён.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении рецепта.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUploadImage_Click_1(object sender, EventArgs e) //загрузка изображения
        {
            btnUploadImage_Click(sender, e);
        }
    }
}
