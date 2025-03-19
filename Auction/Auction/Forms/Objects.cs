using Auction.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Auction.Forms
{
    public partial class Objects : Form
    {
        private int _itemCount = 0;
        public Objects()
        {
            InitializeComponent();

            LoadAndInitData();

            var CategoryType = Program.context.TypeObjects.OrderBy(p => p.TypeName).ToList();
            CategoryType.Insert(0, new Models.TypeObject
            {
                TypeName = "Все типы"
            }
            );

            comboBoxTypes.DataSource = CategoryType;
            comboBoxTypes.DisplayMember = "TypeName";
            comboBoxTypes.ValueMember = "TypeId";

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// Загрузка данных о товаре в таблицу
        /// </summary>
        private void LoadAndInitData()
        {
            var currentGoods = Program.context.ObjectSells.Join(Program.context.TypeObjects, p => p.TypeId, t => t.TypeId,
                (p, t) => new { p.ObjectId, p.ObjectName, p.ReleaseYear, p.ObjectOwner, p.DateOfAdmission, p.EstimatedCost, t.TypeName, p.TypeId }).ToList();

            dgvObjects.DataSource = currentGoods;
            dgvObjects.Columns[7].Visible = false;

            dgvObjects.Columns[0].HeaderText = "Артикул предмета";
            dgvObjects.Columns[1].HeaderText = "Название";
            dgvObjects.Columns[2].HeaderText = "Год создания";
            dgvObjects.Columns[3].HeaderText = "Владелец";
            dgvObjects.Columns[4].HeaderText = "Дата приема";
            dgvObjects.Columns[5].HeaderText = "Оценочная стоимость";
            dgvObjects.Columns[6].HeaderText = "Тип";

            _itemCount = dgvObjects.Rows.Count;

            labelCountObject.Text = $" Результат запроса: {currentGoods.Count} записей из {_itemCount}";
        }

        /// <summary>
        /// Метод для фильтрации и сортировки данных
        /// </summary>
        public void UpdateData(double? minCost = null, double? maxCost = null)
        {
            var currentGoods = Program.context.ObjectSells.Join(Program.context.TypeObjects, p => p.TypeId, t => t.TypeId,
               (p, t) => new { p.ObjectId, p.ObjectName, p.ReleaseYear, p.ObjectOwner, p.DateOfAdmission, p.EstimatedCost, t.TypeName, p.TypeId }).ToList();

            if (comboBoxTypes.SelectedIndex > 0)
                currentGoods = currentGoods.Where(y => y.TypeId == (comboBoxTypes.SelectedItem as TypeObject).TypeId).ToList();

            currentGoods = currentGoods.Where(p => p.ObjectName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            if (minCost != null && maxCost != null)
            {
                if (minCost <= maxCost)
                {
                    currentGoods = currentGoods
                        .Where(p => p.EstimatedCost >= minCost && p.EstimatedCost <= maxCost)
                        .ToList();
                }
                else
                {
                    MessageBox.Show("Минимальная цена не может быть больше максимальной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (comboBoxSort.SelectedIndex >= 0)
            {
                if (comboBoxSort.SelectedIndex == 0)
                    currentGoods = currentGoods.OrderBy(p => p.ReleaseYear).ToList();
                if (comboBoxSort.SelectedIndex == 1)
                    currentGoods = currentGoods.OrderByDescending(p => p.ReleaseYear).ToList();
            }
            dgvObjects.DataSource = currentGoods;

            labelCountObject.Text = $" Результат запроса: {currentGoods.Count} записей из {_itemCount}";
        }

        private void comboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltr_Click(object sender, EventArgs e)
        {
            double minCost = (double)numericMinCost.Value;
            double maxCost = (double)numericMaxCost.Value;

            UpdateData(minCost, maxCost); // Передаём значения в функцию
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            UpdateData();
            numericMaxCost.Value = 0;
            numericMinCost.Value = 0;
        }
    }
}
