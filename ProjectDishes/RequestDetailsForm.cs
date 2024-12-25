using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ProjectDishes
{
    public partial class RequestDetailsForm : Form
    {
        private int requestId;
        public RequestDetailsForm(int requestId)
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            this.requestId = requestId;
            LoadRequestDetails();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void LoadRequestDetails() //загрузка заппросов
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestID", requestId)
            };

            DataTable recipeDetails = DatabaseHelper.ExecuteQuery("GetRequestDetails", parameters);

            if (recipeDetails.Rows.Count == 0)
            {
                MessageBox.Show("Информация о рецепте не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            DataRow row = recipeDetails.Rows[0];

            lblRecipeName.Text = row["RecipeName"].ToString();
            lblRecipeName.AutoSize = true;
            lblRecipeName.Size = new Size(320, lblRecipeName.PreferredHeight);

            lblCategory.Text = $"Категория: {row["CategoryName"]}";
            lblCategory.AutoSize = false;
            lblCategory.MaximumSize = new Size(320, 0);
            lblCategory.Size = new Size(320, lblCategory.PreferredHeight);

            txtDescription.Text = row["Description"].ToString();
            txtDescription.Height = 110;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            ConfigureReadOnlyTextBox(txtDescription);

            txtIngredients.Text = row["Ingredients"].ToString();
            txtIngredients.Height = 100;
            txtIngredients.ScrollBars = ScrollBars.Vertical;
            ConfigureReadOnlyTextBox(txtIngredients);

            if (row["Image"] != DBNull.Value)
            {
                byte[] imageData = (byte[])row["Image"];
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pictureBoxRequest.Image = Image.FromStream(ms);
                    pictureBoxRequest.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else
            {
                pictureBoxRequest.Image = null;
            }
        }
        private void ConfigureReadOnlyTextBox(TextBox textBox)
        {
            textBox.ReadOnly = true;
            textBox.TabStop = false;
            textBox.Multiline = true;
        }

        private void RequestDetailsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
