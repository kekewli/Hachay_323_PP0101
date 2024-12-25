using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace ProjectDishes
{
    class AppStyle
    {
        private static Color PrimaryColor = Color.FromArgb(46, 139, 87);
        private static Color SecondaryColor = Color.FromArgb(220, 220, 220);
        private static Color BackgroundColor = Color.FromArgb(245, 245, 245);
        private static Font PrimaryFont = new Font("Arial", 12, FontStyle.Regular);
        private static Font HeaderFont = new Font("Arial", 14, FontStyle.Bold);
        public static void ApplyStyle(Form form) //стиль формы
        {
            form.BackColor = BackgroundColor;
            form.Font = PrimaryFont;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
            foreach (Control control in form.Controls)
            {
                ApplyControlStyle(control);
            }
            ApplyCenterScreen(form);
        }
        public static void ApplyCenterScreen(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
        }
        private static void ApplyControlStyle(Control control) //шрифт
        {
            if (control is Button button)
            {
                button.BackColor = PrimaryColor;
                button.ForeColor = Color.White;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = PrimaryFont;
            }
            else if (control is Label label)
            {
                label.Font = HeaderFont;
                label.ForeColor = PrimaryColor;
            }
            else if (control is DataGridView dataGridView)
            {
                ApplyDataGridViewStyle(dataGridView);
            }
            else if (control is TextBox textBox)
            {
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = PrimaryFont;
                textBox.BackColor = Color.White;
                textBox.ForeColor = Color.Black;
            }
            else if (control.HasChildren)
            {
                foreach (Control child in control.Controls)
                {
                    ApplyControlStyle(child);
                }
            }
        }
        public static void ApplyDataGridViewStyle(DataGridView dataGridView) //датагридвью
        {
            dataGridView.BackgroundColor = SecondaryColor;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridView.RowHeadersVisible = false;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = HeaderFont;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.DefaultCellStyle.Font = PrimaryFont;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridView.CellToolTipTextNeeded += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var cellValue = dataGridView[e.ColumnIndex, e.RowIndex].Value;
                    if (cellValue != null)
                    {
                        e.ToolTipText = cellValue.ToString();
                    }
                }
            };
        }
    }
}
