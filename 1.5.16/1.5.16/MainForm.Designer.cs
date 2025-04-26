namespace WarehouseManager
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.ComboBox cmbFilterCategory;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dgvProducts = new DataGridView();
            txtName = new TextBox();
            txtQuantity = new TextBox();
            txtPrice = new TextBox();
            txtSearch = new TextBox();
            cmbCategory = new ComboBox();
            cmbUnit = new ComboBox();
            cmbFilterCategory = new ComboBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.BackgroundColor = SystemColors.GradientActiveCaption;
            dgvProducts.Location = new Point(12, 12);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(960, 300);
            dgvProducts.TabIndex = 0;
            // 
            // txtName
            // 
            txtName.Location = new Point(12, 330);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Наименование";
            txtName.Size = new Size(150, 23);
            txtName.TabIndex = 1;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(330, 330);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.PlaceholderText = "Кол-во";
            txtQuantity.Size = new Size(100, 23);
            txtQuantity.TabIndex = 3;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(550, 330);
            txtPrice.Name = "txtPrice";
            txtPrice.PlaceholderText = "Цена";
            txtPrice.Size = new Size(100, 23);
            txtPrice.TabIndex = 5;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Location = new Point(12, 370);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Поиск...";
            txtSearch.Size = new Size(300, 23);
            txtSearch.TabIndex = 9;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Location = new Point(170, 330);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(150, 23);
            cmbCategory.TabIndex = 2;
            // 
            // cmbUnit
            // 
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.Location = new Point(440, 330);
            cmbUnit.Name = "cmbUnit";
            cmbUnit.Size = new Size(100, 23);
            cmbUnit.TabIndex = 4;
            // 
            // cmbFilterCategory
            // 
            cmbFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterCategory.Location = new Point(430, 370);
            cmbFilterCategory.Name = "cmbFilterCategory";
            cmbFilterCategory.Size = new Size(200, 23);
            cmbFilterCategory.TabIndex = 11;
            cmbFilterCategory.SelectedIndexChanged += cmbFilterCategory_SelectedIndexChanged;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = SystemColors.ButtonHighlight;
            btnAdd.Location = new Point(670, 330);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 23);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = SystemColors.ButtonHighlight;
            btnEdit.Location = new Point(780, 330);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 23);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = SystemColors.ButtonHighlight;
            btnDelete.Location = new Point(890, 330);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = SystemColors.ButtonHighlight;
            btnSearch.Location = new Point(324, 370);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 23);
            btnSearch.TabIndex = 10;
            btnSearch.Text = "Поиск";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // MainForm
            // 
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1000, 400);
            Controls.Add(dgvProducts);
            Controls.Add(txtName);
            Controls.Add(cmbCategory);
            Controls.Add(txtQuantity);
            Controls.Add(cmbUnit);
            Controls.Add(txtPrice);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(cmbFilterCategory);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Система управления складом";
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
