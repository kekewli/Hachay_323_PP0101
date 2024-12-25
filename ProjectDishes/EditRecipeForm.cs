using System;
using System.Data;
using System.Data.SqlClient;
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
            DataTable categories = DatabaseHelper.ExecuteQuery("GetCategories");
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
        }
        private void LoadRecipeDetails() //детали
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RecipeID", recipeId)
            };

            DataTable recipeDetails = DatabaseHelper.ExecuteQuery("GetRecipeById", parameters);

            if (recipeDetails.Rows.Count > 0)
            {
                DataRow row = recipeDetails.Rows[0];
                txtRecipeName.Text = row["RecipeName"].ToString();
                txtDescription.Text = row["Description"].ToString();
                txtIngredients.Text = row["Ingredients"].ToString();
                string categoryName = recipeDetails.Rows[0]["CategoryName"].ToString();
                cmbCategory.SelectedIndex = cmbCategory.FindStringExact(categoryName);
                if (row["Image"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])row["Image"];
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBoxRecipe.Image = Image.FromStream(ms);
                        pictureBoxRecipe.SizeMode = PictureBoxSizeMode.Zoom;
                        recipeImage = imageData; 
                    }
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
        private void btnSave_Click(object sender, EventArgs e) //сохранение
        {
            string recipeName = txtRecipeName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string ingredients = txtIngredients.Text.Trim();
            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);

            if (string.IsNullOrEmpty(recipeName) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(ingredients))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RecipeID", recipeId),
                new SqlParameter("@RecipeName", recipeName),
                new SqlParameter("@Description", description),
                new SqlParameter("@Ingredients", ingredients),
                new SqlParameter("@CategoryID", categoryId),
                new SqlParameter("@Image", recipeImage ?? (object)DBNull.Value) 
            };

            if (DatabaseHelper.ExecuteNonQuery("UpdateRecipe", parameters))
            {
                MessageBox.Show("Рецепт успешно обновлен.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении рецепта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUploadImage_Click_1(object sender, EventArgs e) //загрузка изображения
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
