using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace ProjectDishes
{
    public partial class UserRequestsForm : Form
    {
        public UserRequestsForm()
        {
            InitializeComponent();
            AppStyle.ApplyStyle(this);
            LoadUserRequests();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void LoadUserRequests() //загрузка запросов 
        {
            DataTable userRequests = DatabaseHelper.ExecuteQuery("GetUserRecipeRequests");
            flowLayoutPanelRequests.Controls.Clear();
            foreach (DataRow row in userRequests.Rows)
            {
                Panel requestPanel = CreateRequestPanel(row);
                flowLayoutPanelRequests.Controls.Add(requestPanel);
            }
        }
        private Panel CreateRequestPanel(DataRow row) //создание панелей
        {
            Panel panel = new Panel
            {
                Size = new Size(300, 100),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = row["RequestID"]
            };
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            if (row["Image"] != DBNull.Value)
            {
                byte[] imageData = (byte[])row["Image"];
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pictureBox.Image = Image.FromStream(ms);
                }
            }
            Label nameLabel = new Label
            {
                Text = row["RecipeName"].ToString(),
                Location = new Point(100, 10),
                Size = new Size(200, 50),
                Font = new Font("Arial", 10, FontStyle.Regular),
                AutoEllipsis = true
            };
            Button viewButton = new Button
            {
                Text = "Подробнее",
                Location = new Point(100, 60),
                Size = new Size(100, 30)
            };
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(viewButton);
            panel.Click += (sender, e) => SelectRecipePanel(panel);
            pictureBox.Click += (sender, e) => SelectRecipePanel(panel);
            nameLabel.Click += (sender, e) => SelectRecipePanel(panel);
            viewButton.Click += (sender, e) => OpenRecipeDetails((int)panel.Tag);

            return panel;
        }
        private void SelectRecipePanel(Panel panel) //выбор панелей
        {
            foreach (Control control in flowLayoutPanelRequests.Controls)
            {
                if (control is Panel otherPanel)
                {
                    otherPanel.BackColor = SystemColors.Control;
                }
            }
            panel.BackColor = Color.LightBlue; 
        }
        private void OpenRecipeDetails(int requestId) //открытие деталей
        {
            RequestDetailsForm detailsForm = new RequestDetailsForm(requestId);
            detailsForm.ShowDialog();
        }
        private DataTable GetRequestDetails(int requestId) //метод открытия деталей
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestID", requestId)
            };
            return DatabaseHelper.ExecuteQuery("GetRequestDetailsById", parameters);
        }
        private void btnApproveRecipe_Click(object sender, EventArgs e) //подтверждение
        {
            Panel selectedPanel = flowLayoutPanelRequests.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => p.BackColor == Color.LightBlue);
            if (selectedPanel == null)
            {
                MessageBox.Show("Выберите запрос для утверждения.");
                return;
            }
            int requestId = (int)selectedPanel.Tag;
            DataTable requestData = GetRequestDetails(requestId);
            DataRow row = requestData.Rows[0];
            string recipeName = row["RecipeName"].ToString();
            string description = row["Description"].ToString();
            string ingredients = row["Ingredients"].ToString();
            int categoryId = Convert.ToInt32(row["CategoryID"]);
            byte[] imageData = null;
            if (row["Image"] != DBNull.Value)
            {
                imageData = (byte[])row["Image"]; 
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RecipeName", recipeName),
                new SqlParameter("@Description", description),
                new SqlParameter("@Ingredients", ingredients),
                new SqlParameter("@CategoryID", categoryId),
                new SqlParameter("@Image", imageData ?? (object)DBNull.Value)
            };
            if (DatabaseHelper.ExecuteNonQuery("AddRecipe", parameters))
            {
                SqlParameter[] deleteParameters = new SqlParameter[]
                {
                    new SqlParameter("@RequestID", requestId)
                };
                DatabaseHelper.ExecuteNonQuery("DeleteUserRequest", deleteParameters);
                MessageBox.Show("Запрос утвержден.");
                LoadUserRequests(); 
            }
        }
        private void btnDeleteRequest_Click(object sender, EventArgs e) //уаление
        {
            Panel selectedPanel = flowLayoutPanelRequests.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => p.BackColor == Color.LightBlue);

            if (selectedPanel == null)
            {
                MessageBox.Show("Выберите запрос для удаления.");
                return;
            }

            int requestId = (int)selectedPanel.Tag;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestID", requestId)
            };

            if (DatabaseHelper.ExecuteNonQuery("DeleteUserRequest2", parameters))
            {
                MessageBox.Show("Запрос удален.");
                LoadUserRequests();
            }
            else
            {
                MessageBox.Show("Не удалось удалить запрос.");
            }
        }
    }
}
