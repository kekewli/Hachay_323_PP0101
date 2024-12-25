using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            LoadCategories();
            AppStyle.ApplyStyle(this);
            pictureBoxRecipe.AllowDrop = true;
            pictureBoxRecipe.DragEnter += PictureBoxRecipe_DragEnter;
            pictureBoxRecipe.DragDrop += PictureBoxRecipe_DragDrop;
        }
        private void LoadCategories() //категории
        {
            DataTable categories = DatabaseHelper.ExecuteQuery("GetCategories");
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
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
                string imagePath = files[0];
                try
                {
                    DisplayImage(imagePath);  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                }
            }
        }
        private void btnUploadImage_Click(object sender, EventArgs e) //загрузка изоб.
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DisplayImage(openFileDialog.FileName);
            }
        }
        private void DisplayImage(string filePath)
        {
            try
            {
                Image image = Image.FromFile(filePath);
                pictureBoxRecipe.Image = image;
                pictureBoxRecipe.SizeMode = PictureBoxSizeMode.Zoom;  
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    recipeImage = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e) //подтверждение
        {
            string recipeName = txtRecipeName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string ingredients = txtIngredients.Text.Trim();
            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);

            if (string.IsNullOrEmpty(recipeName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(ingredients))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (recipeImage == null)
            {
                DialogResult result = MessageBox.Show(
                    "Вы не добавили изображение. Загрузить сейчас?",
                    "Изображение отсутствует",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                        Title = "Выберите изображение"
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            DisplayImage(openFileDialog.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы отменили выбор изображения. Рецепт не будет сохранен без изображения.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Рецепт не может быть сохранён без изображения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Готовим параметры для сохранения
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserID", userId),
        new SqlParameter("@RecipeName", recipeName),
        new SqlParameter("@Description", description),
        new SqlParameter("@Ingredients", ingredients),
        new SqlParameter("@CategoryID", categoryId),
        new SqlParameter("@Image", recipeImage ?? (object)DBNull.Value)
            };

            // Сохраняем данные в базу
            if (DatabaseHelper.ExecuteNonQuery("SubmitUserRecipe", parameters))
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

        }
        private void btnUploadImage_Click_1(object sender, EventArgs e) //открытие проводника
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
    }
}
