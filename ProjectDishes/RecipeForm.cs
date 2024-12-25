using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            LoadCategories();
            AppStyle.ApplyStyle(this);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ConfigurePictureBox();
        }
        public RecipeForm(int recipeId)
        {
            InitializeComponent();
            this.recipeId = recipeId;
            LoadCategories();
            LoadRecipeDetails();
            AppStyle.ApplyStyle(this);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ConfigurePictureBox();
        }
        private void LoadCategories()
        {
            DataTable categories = DatabaseHelper.ExecuteQuery("GetCategories");
            comboBoxCategory.DataSource = categories;
            comboBoxCategory.DisplayMember = "CategoryName";
            comboBoxCategory.ValueMember = "CategoryID";
        }
        private void LoadRecipeDetails() //загрузка деталей
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RecipeID", recipeId)
            };
            DataTable recipeDetails = DatabaseHelper.ExecuteQuery("GetRecipeById", parameters);
            if (recipeDetails.Rows.Count > 0)
            {
                txtRecipeName.Text = recipeDetails.Rows[0]["RecipeName"].ToString();
                txtDescription.Text = recipeDetails.Rows[0]["Description"].ToString();
                txtIngredients.Text = recipeDetails.Rows[0]["Ingredients"].ToString();
                comboBoxCategory.SelectedValue = recipeDetails.Rows[0]["CategoryID"];
                if (recipeDetails.Rows[0]["Image"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])recipeDetails.Rows[0]["Image"];
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBoxRecipe.Image = Image.FromStream(ms);
                        pictureBoxRecipe.SizeMode = PictureBoxSizeMode.Zoom;
                        recipeImage = imageData;
                    }
                }
            }
        }
        private void ConfigurePictureBox() //картинка
        {
            pictureBoxRecipe.AllowDrop = true;
            pictureBoxRecipe.DragEnter += PictureBoxRecipe_DragEnter;
            pictureBoxRecipe.DragDrop += PictureBoxRecipe_DragDrop;
            pictureBoxRecipe.Click += pictureBoxRecipe_Click;
        }
        private void PictureBoxRecipe_DragEnter(object sender, DragEventArgs e) //драгндроп картинки
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
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Выберите изображение"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DisplayImage(openFileDialog.FileName);
            }
        }
        private void DisplayImage(string filePath) //отображение изоб.
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
        private void btnSave_Click_1(object sender, EventArgs e) //сохранение
        {
            string recipeName = txtRecipeName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string ingredients = txtIngredients.Text.Trim();
            int categoryId = Convert.ToInt32(comboBoxCategory.SelectedValue);

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
                    if (!TryLoadImageFromDialog())
                    {
                        MessageBox.Show("Изображение не было добавлено. Рецепт не будет сохранён.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Рецепт не может быть сохранён без изображения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@RecipeName", recipeName),
        new SqlParameter("@Description", description),
        new SqlParameter("@Ingredients", ingredients),
        new SqlParameter("@CategoryID", categoryId),
        new SqlParameter("@Image", recipeImage ?? (object)DBNull.Value)
            };

            if (recipeId == -1)
            {
                if (DatabaseHelper.ExecuteNonQuery("AddRecipe", parameters))
                {
                    MessageBox.Show("Рецепт успешно добавлен.");
                    this.Close();
                }
            }
            else
            {
                SqlParameter[] updateParameters = new SqlParameter[]
                {
            new SqlParameter("@RecipeID", recipeId),
            new SqlParameter("@RecipeName", recipeName),
            new SqlParameter("@Description", description),
            new SqlParameter("@Ingredients", ingredients),
            new SqlParameter("@CategoryID", categoryId),
            new SqlParameter("@Image", recipeImage ?? (object)DBNull.Value)
                };

                if (DatabaseHelper.ExecuteNonQuery("UpdateRecipe", updateParameters))
                {
                    MessageBox.Show("Рецепт успешно обновлён.");
                    this.Close();
                }
            }
        }
        private bool TryLoadImageFromDialog() // попытка загрузки изображения через диалог
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
            btnUploadImage_Click(sender, e);
        }

    }
}
