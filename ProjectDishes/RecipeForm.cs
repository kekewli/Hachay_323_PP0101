using System;
using System.Data;
using Npgsql;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Supabase.Storage;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class RecipeForm : Form
    {
        private int recipeId = -1;
        private byte[] recipeImage = null;
        public RecipeForm()
        {
            InitializeComponent();
            _ = LoadCategories();
            AppStyle.ApplyStyle(this);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ConfigurePictureBox();
        }
        public RecipeForm(int recipeId)
        {
            this.recipeId = recipeId;
            _ = LoadCategories();
        }
        private async Task LoadCategories() //загрузка категорий
        {
            DataTable categories = await DatabaseHelper.ExecuteQuery("get_categories");
            comboBoxCategory.DataSource = categories;
            comboBoxCategory.DisplayMember = "category_name"; 
            comboBoxCategory.ValueMember = "category_id";
        }

        private void ConfigurePictureBox() //картинка
        {
            pictureBoxRecipe.AllowDrop = true;
            pictureBoxRecipe.DragEnter += PictureBoxRecipe_DragEnter;
            pictureBoxRecipe.DragDrop += PictureBoxRecipe_DragDrop;
            pictureBoxRecipe.Click += pictureBoxRecipe_Click;
        }
        private void PictureBoxRecipe_DragEnter(object sender, DragEventArgs e) //драгентер картинки
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void PictureBoxRecipe_DragDrop(object sender, DragEventArgs e) //драгндроп картинки
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
        private void btnUploadImage_Click(object sender, EventArgs e) //загрузка изоб.
        {
            PickImageFromDialog();
        }
        private void PickImageFromDialog() 
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Выберите изображение"
            };
            if (dlg.ShowDialog() == DialogResult.OK) DisplayImage(dlg.FileName);
        }
        private void DisplayImage(string filePath) //отображение изоб.
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
        private async void btnSave_Click_1(object sender, EventArgs e) //сохранение рецепта
        {
            string name = txtRecipeName.Text.Trim();
            string desc = txtDescription.Text.Trim();
            string ingr = txtIngredients.Text.Trim();
            int catId = Convert.ToInt32(comboBoxCategory.SelectedValue);
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(ingr))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (recipeImage == null)
            {
                var res = MessageBox.Show(
                    "Вы не добавили изображение. Загрузить сейчас?", "Изображение отсутствует", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    if (!TryLoadImageFromDialog())
                    {
                        MessageBox.Show("Изображение не было добавлено — отмена сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Рецепт не может быть сохранён без изображения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            var rpcParams = new
            {
                p_name = name,
                p_desc = desc,
                p_ingr = ingr,
                p_cat_id = catId,
                p_image = recipeImage
            };
            bool ok;
            if (recipeId == -1)
            {
                ok = await DatabaseHelper.ExecuteNonQuery("add_recipe", rpcParams);
                if (ok) MessageBox.Show("Рецепт успешно добавлен.");
            }
            else
            {
                var updateParams = new
                {
                    p_id = recipeId,
                    p_name = name,
                    p_desc = desc,
                    p_ingr = ingr,
                    p_cat = catId,
                    p_image = recipeImage
                };
                ok = await DatabaseHelper.ExecuteNonQuery("update_recipe", updateParams);
                if (ok) MessageBox.Show("Рецепт успешно обновлён.");
            }
            if (ok)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private bool TryLoadImageFromDialog() // попытка загрузки изображения через диалог
        {
            using var openFileDialog = new OpenFileDialog {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title  = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DisplayImage(openFileDialog.FileName);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }
        private void pictureBoxRecipe_Click(object sender, EventArgs e)
        {
        }

    }
}
