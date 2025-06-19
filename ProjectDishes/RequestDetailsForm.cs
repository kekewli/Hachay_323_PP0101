using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
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
            _=LoadRequestDetails();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private async Task LoadRequestDetails() //загрузка заппросов
        {
            var rpcParams = new { p_req_id = requestId };
            var dt = await DatabaseHelper.ExecuteQuery("get_request_details", rpcParams);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Информация о запросе не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            var row = dt.Rows[0];

            lblRecipeName.Text = $"Название: {row["recipe_name"].ToString()}";
            lblRecipeName.AutoSize = true;
            lblRecipeName.Size = new Size(320, lblRecipeName.PreferredHeight);

            lblCategory.Text = $"Категория: {row["category_name"]}";
            lblCategory.AutoSize = false;
            lblCategory.MaximumSize = new Size(320, 0);
            lblCategory.Size = new Size(320, lblCategory.PreferredHeight);

            txtDescription.Text = row["description"].ToString();
            txtDescription.Height = 110;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            ConfigureReadOnlyTextBox(txtDescription);

            txtIngredients.Text = row["ingredients"].ToString();
            txtIngredients.Height = 100;
            txtIngredients.ScrollBars = ScrollBars.Vertical;
            ConfigureReadOnlyTextBox(txtIngredients);

            if (row["image"] != DBNull.Value)
            {
                byte[] imageData = (byte[])row["image"];
                using (var ms = new MemoryStream(imageData))
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
