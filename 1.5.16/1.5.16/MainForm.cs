
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WarehouseManager
{
    public partial class MainForm : Form
    {
        private List<Product> products = new List<Product>();

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // DataGridView Setup
            dgvProducts.Columns.Add("Name", "Наименование");
            dgvProducts.Columns.Add("Category", "Категория");
            dgvProducts.Columns.Add("Quantity", "Количество");
            dgvProducts.Columns.Add("Unit", "Ед. изм.");
            dgvProducts.Columns.Add("Price", "Цена за ед.");

            cmbCategory.Items.AddRange(new[] { "Электроника", "Продукты", "Одежда", "Прочее" });
            cmbUnit.Items.AddRange(new[] { "шт.", "кг", "л" });
            cmbFilterCategory.Items.Add("Все категории");
            cmbFilterCategory.Items.AddRange(cmbCategory.Items.Cast<object>().ToArray());
            cmbFilterCategory.SelectedIndex = 0;
        }

        private void RefreshGrid()
        {
            dgvProducts.Rows.Clear();
            var filtered = products;

            if (cmbFilterCategory.SelectedItem.ToString() != "Все категории")
            {
                filtered = filtered.Where(p => p.Category == cmbFilterCategory.SelectedItem.ToString()).ToList();
            }

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                filtered = filtered.Where(p => p.Name.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            foreach (var p in filtered)
            {
                dgvProducts.Rows.Add(p.Name, p.Category, p.Quantity, p.Unit, p.Price);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || cmbCategory.SelectedItem == null ||
                !int.TryParse(txtQuantity.Text, out int quantity) ||
                cmbUnit.SelectedItem == null ||
                !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Заполните все поля корректно.");
                return;
            }

            var product = new Product
            {
                Name = txtName.Text,
                Category = cmbCategory.SelectedItem.ToString(),
                Quantity = quantity,
                Unit = cmbUnit.SelectedItem.ToString(),
                Price = price
            };

            products.Add(product);
            RefreshGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                var name = dgvProducts.SelectedRows[0].Cells[0].Value.ToString();
                products.RemoveAll(p => p.Name == name);
                RefreshGrid();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) return;

            var selectedRow = dgvProducts.SelectedRows[0];
            var name = selectedRow.Cells[0].Value.ToString();
            var product = products.FirstOrDefault(p => p.Name == name);
            if (product == null) return;

            if (!int.TryParse(txtQuantity.Text, out int quantity) || !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Неверные значения количества или цены.");
                return;
            }

            product.Name = txtName.Text;
            product.Category = cmbCategory.SelectedItem.ToString();
            product.Quantity = quantity;
            product.Unit = cmbUnit.SelectedItem.ToString();
            product.Price = price;

            RefreshGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
    }
}
