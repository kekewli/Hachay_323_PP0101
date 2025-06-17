using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Newtonsoft.Json;

namespace ProjectDishes
{
    public partial class CreateRecipeForm : Form
    {
        private int userId;
        private byte[] recipeImage = null;
        public CreateRecipeForm(int userId)
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            this.userId = userId;
            _ = LoadCategories();
            pictureBoxRecipe.AllowDrop = true;
            pictureBoxRecipe.DragEnter += PictureBoxRecipe_DragEnter;
            pictureBoxRecipe.DragDrop += PictureBoxRecipe_DragDrop;
        }
        private async Task LoadCategories() //категории
        {
            DataTable categories = await DatabaseHelper.ExecuteQuery("get_categories");
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "category_name";
            cmbCategory.ValueMember = "category_id";
        }
        private void PictureBoxRecipe_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void PictureBoxRecipe_DragDrop(object sender, DragEventArgs e)
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
        private void PickImageFromDialog()
        {
            using var open = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Выберите изображение"
            };
            if (open.ShowDialog() == DialogResult.OK)
                DisplayImage(open.FileName);
        }
        private void DisplayImage(string filePath)
        {
            try
            {
                var img = Image.FromFile(filePath);
                pictureBoxRecipe.Image = img;
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
        private async void btnSubmit_Click(object sender, EventArgs e) //подтверждение
        {
            string recipeName = txtRecipeName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string ingredients = txtIngredients.Text.Trim();
            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            if (string.IsNullOrEmpty(recipeName) ||
                string.IsNullOrEmpty(description) ||
                string.IsNullOrEmpty(ingredients))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (recipeImage == null)
            {
                var res = MessageBox.Show("Вы не добавили изображение! Загрузить сейчас?","Изображение отсутствует", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    PickImageFromDialog();
                    if (recipeImage == null) return;
                }
                else
                {
                    MessageBox.Show(
                        "Рецепт не может быть сохранён без изображения.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            var rpcParams = new
            {
                p_user = userId,
                p_name = recipeName,
                p_desc = description,
                p_ingr = ingredients,
                p_cat_id = categoryId,
                p_image = recipeImage
            };
            bool success = await DatabaseHelper.ExecuteNonQuery("submit_user_recipe", rpcParams);
            if (success)
            {
                MessageBox.Show("Рецепт отправлен на рассмотрение.");
                this.Close();
            }
        }
        private void CreateRecipeForm_Load(object sender, EventArgs e)
        {
            pictureBoxRecipe.Image = null;
            pictureBoxRecipe.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void pictureBoxRecipe_Click(object sender, EventArgs e)
        {
            PickImageFromDialog();
        }
        private void btnUploadImage_Click_1(object sender, EventArgs e) //открытие проводника
        {
            PickImageFromDialog();
        }
    }
}
