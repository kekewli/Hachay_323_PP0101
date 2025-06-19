using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
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
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Shown += async (s, e) =>
            {
                await LoadCategories();
                await LoadRecipeDetails(); 
                ConfigurePictureBox();
            };
            AppStyle.ApplyStyle(this);
        }
        private async Task LoadCategories() //категории
        {
            var dt = await DatabaseHelper.ExecuteQuery("get_categories");
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "category_name";
            cmbCategory.ValueMember = "category_id";
        }
        private async Task LoadRecipeDetails() //детали рецепта
        {
            var rpcParams = new { p_recipe_id = recipeId };
            var dt = await DatabaseHelper.ExecuteQuery("get_recipe_by_id", rpcParams);
            if (dt.Rows.Count == 0) return;
            var row = dt.Rows[0];
            txtRecipeName.Text = row["recipe_name"].ToString();
            txtDescription.Text = row["description"].ToString();
            txtIngredients.Text = row["ingredients"].ToString();
            cmbCategory.SelectedValue = row["category_id"];
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
                p_id = recipeId,
                p_name = recipeName,
                p_desc = description,
                p_ingr = ingredients,
                p_cat = categoryId,
                p_image = recipeImage
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
