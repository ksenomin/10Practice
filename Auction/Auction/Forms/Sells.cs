using System;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Auction.Forms
{
    public partial class Sells : Form
    {
        private int _itemCount = 0;
        public Sells()
        {
            InitializeComponent();
            LoadAndInitData();
        }

        /// <summary>
        /// Загрузка данных о товаре в таблицу
        /// </summary>
        private void LoadAndInitData()
        {
            var currentGoods = Program.context.AuctionSales.Join(Program.context.ObjectSells, p => p.ObjectId, t => t.ObjectId,
                (p, t) => new { p.SaleId, p.DateSale, p.StartCost, p.EndCost, p.SignSale, p.FamBuyer, t.ObjectName, p.ObjectId }).ToList();

            dgvObjects.DataSource = currentGoods;
            dgvObjects.Columns[7].Visible = false;

            dgvObjects.Columns[0].HeaderText = "Артикул предмета";
            dgvObjects.Columns[1].HeaderText = "Дата покупки";
            dgvObjects.Columns[2].HeaderText = "Начальная стоимость";
            dgvObjects.Columns[3].HeaderText = "Итоговая стоимость";
            dgvObjects.Columns[4].HeaderText = "Статус продажи";
            dgvObjects.Columns[5].HeaderText = "Фамилия покупателя";
            dgvObjects.Columns[6].HeaderText = "Предмет";

            _itemCount = dgvObjects.Rows.Count;

            labelCount.Text = $" Результат: {currentGoods.Count} записей из {_itemCount}";
        }

        private void dgvObjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Проверяем, что индекс строки корректный
            {
                // Получаем ObjectId из DataGridView (из столбца с названием "ObjectId")
                int objectId = Convert.ToInt32(dgvObjects.Rows[e.RowIndex].Cells["ObjectId"].Value);

                // Находим связанный объект из ObjectSells по ObjectId
                var product = Program.context.ObjectSells
                                .FirstOrDefault(p => p.ObjectId == objectId);

                if (product != null)
                {
                    // Отображаем данные в элементах формы
                    labelName.Text = product.ObjectName;
                    txtDescription.Text = product.Discription;
                }
                else
                {
                    // Если объект не найден — очищаем поля
                    labelName.Text = "Не найдено";
                    txtDescription.Clear();
                }
            }
        }
        
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvObjects.Rows.Count > 0)
            {
                // Создаём приложение Excel
                Excel.Application excelApp = new Excel.Application
                {
                    SheetsInNewWorkbook = 1
                };

                // Создаём рабочую книгу
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);

                // Создаём рабочий лист
                Excel.Worksheet workSheet = (Excel.Worksheet)workbook.Sheets[1];
                workSheet.Name = "Продажи";


                for (int i = 0; i < dgvObjects.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = dgvObjects.Columns[i].HeaderText;
                }

                for (int i = 0; i < dgvObjects.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvObjects.Columns.Count; j++)
                    {
                        if (dgvObjects.Rows[i].Cells[j].Value != null)
                        {
                            workSheet.Cells[i + 2, j + 1] = dgvObjects.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                workSheet.Columns.AutoFit(); // Подгоняем ширину столбцов

                excelApp.Visible = true;

                MessageBox.Show("Данные успешно экспортированы в Excel!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Нет данных для экспорта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
