using System;
using System.Data;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace ProjectDishes
{
    public partial class UserRequestsForm : Form
    {
        private int? _selectedRequestId;
        private readonly Timer _refreshTimer;
        public UserRequestsForm()
        {
            InitializeComponent();
            _ = LoadUserRequests();
            _refreshTimer = new Timer { Interval = 10000 };
            _refreshTimer.Tick += async (_, __) => await LoadUserRequests();
            _refreshTimer.Start();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AppStyle.ApplyStyle(this);
        }
        private async Task LoadUserRequests() //загрузка запросов 
        {
            DataTable userRequests = await DatabaseHelper.ExecuteQuery("get_user_recipe_requests");
            if (userRequests == null || userRequests.Columns.Count == 0)
            {
                return;
            }
            flowLayoutPanelRequests.Controls.Clear();
            foreach (DataRow row in userRequests.Rows)
            {
                Panel panel = CreateRequestPanel(row);
                flowLayoutPanelRequests.Controls.Add(panel);
            }
        }
        private Panel CreateRequestPanel(DataRow row) //создание панелей
        {
            var panel = new Panel
            {
                Size = new Size(300, 120),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = row["request_id"]
            };
            var pictureBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };
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
                        pictureBox.Image = Image.FromStream(ms);
                    }
                    catch
                    {
                    }
                }
            }
            var nameLabel = new Label
            {
                Text = row["recipe_name"].ToString(),
                Location = new Point(100, 10),
                Size = new Size(200, 50),
                Font = new Font("Arial", 10),
                AutoEllipsis = true
            };
            var viewButton = new Button
            {
                Text = "Подробнее",
                Location = new Point(100, 60),
                Size = new Size(100, 30)
            };
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(viewButton);
            panel.Click += (s, e) => SelectRequestPanel(panel);
            pictureBox.Click += (s, e) => SelectRequestPanel(panel);
            nameLabel.Click += (s, e) => SelectRequestPanel(panel);
            viewButton.Click += (s, e) => OpenRequestDetails(Convert.ToInt32(panel.Tag));
            return panel;
        }

        private void SelectRequestPanel(Panel panel) //выбор панелей
        {
            foreach (Control control in flowLayoutPanelRequests.Controls)
            {
                if (control is Panel otherPanel)
                {
                    otherPanel.BackColor = SystemColors.Control;
                }
            }
            panel.BackColor = Color.LightBlue;
            _selectedRequestId = Convert.ToInt32(panel.Tag);
        }
        private void OpenRequestDetails(int requestId) //открытие деталей
        {
            try
            {
                var detailsForm = new RequestDetailsForm(requestId);
                detailsForm.ShowDialog();
            }
            catch { }
        }
        private async void btnApproveRecipe_Click(object sender, EventArgs e) //подтверждение
        {
            if (_selectedRequestId == null)
            {
                MessageBox.Show("Выберите запрос для утверждения.", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var rpcParams = new { p0 = _selectedRequestId.Value }; //получение деталя самого запроса
            DataTable dt = await DatabaseHelper.ExecuteQuery("get_request_details_by_id", rpcParams);
            if (dt.Rows.Count == 0) return;
            DataRow row = dt.Rows[0];
            var imgValue = row["image"];
            byte[] imgBytes = null;
            if (imgValue != DBNull.Value)
            {
                if (imgValue is byte[] bytes)
                {
                    imgBytes = bytes;
                }
                else if (imgValue is string hexString)
                {
                    if (hexString.StartsWith("\\x") || hexString.StartsWith("0x"))
                        hexString = hexString.Substring(2);
                    int len = hexString.Length;
                    imgBytes = new byte[len / 2];
                    for (int i = 0; i < len; i += 2)
                        imgBytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
                }
            }
            var rpcAdd = new
            {
                p_name = row["recipe_name"].ToString(),
                p_desc = row["description"].ToString(),
                p_ingr = row["ingredients"].ToString(),
                p_cat_id = Convert.ToInt32(row["category_id"]),
                p_image = imgBytes
            };
            if (await DatabaseHelper.ExecuteNonQuery("add_recipe", rpcAdd)) //добавление рецепта в основную таблицу
            {
                var rpcDel = new { p_request = _selectedRequestId.Value };
                await DatabaseHelper.ExecuteNonQuery("delete_user_request", rpcDel);

                MessageBox.Show("Запрос утверждён.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadUserRequests();
            }
            else
            {
                MessageBox.Show("Не удалось добавить рецепт.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnDeleteRequest_Click(object sender, EventArgs e) //удаление рецепта
        {
            if (_selectedRequestId == null)
            {
                MessageBox.Show("Выберите запрос для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var rpcParams = new { p_request = _selectedRequestId.Value };
            if (await DatabaseHelper.ExecuteNonQuery("delete_user_request", rpcParams))
            {
                MessageBox.Show("Запрос удалён.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadUserRequests();
            }
            else
            {
                MessageBox.Show("Не удалось удалить запрос.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UserRequestsForm_FormClosed(object sender, FormClosedEventArgs e) //выход
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
        }
    }
}
