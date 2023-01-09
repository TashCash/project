using FitnesClub.Classes;
using FitnesClub.Forms;
using FitnesClub.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FitnesClub
{
    public partial class Main : Form
    {
        public bool isClose = true;
        public bool adminMode = false;

        // Конструктор
        public Main(bool adminMode)
        {
            this.adminMode = adminMode;
            InitializeComponent();
        }

        // Закрыть программу
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        // Открыть форму авторизации
        private void авторизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isClose = false;
            new Auth().Show();
            Close();
        }

        // Вызов справки
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e) => Help.ShowHelp(this, helpProvider.HelpNamespace);

        // Вызов окна о программе
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) => MessageBox.Show("Нужно создать бд SQL , " +
            "мы делаем на формах , связать ее с с#, над этой бд сделать операции добавления , " +
            "удаления , редактирования , фильтрации - это выборочно например по названию и т.д, поиск к каждой таблице," +
            "не менее 4 форм , база из 3 , 4 таблиц , сделать справку, о программе и обязательно чтоб выходился отчёт в ексель", "О программе");

        // Загрузка данных в грид (процедура)
        void LoadData()
        {
            BindingSource bindingSource = new BindingSource();
            object list = null;
            List<string> columns = null;

            if (adminMode == true)
            {
                switch (cbTables.SelectedIndex)
                {
                    case 0:
                        columns = ModelAttributes.GetFieldsName(Activator.CreateInstance(Type.GetType("FitnesClub.Models.admins")));
                        list = Connector.Get<admins>();
                        break;
                    case 1:
                        columns = ModelAttributes.GetFieldsName(Activator.CreateInstance(Type.GetType("FitnesClub.Models.clients")));
                        list = Connector.Get<clients>();
                        break;
                    case 2:
                        columns = ModelAttributes.GetFieldsName(Activator.CreateInstance(Type.GetType("FitnesClub.Models.services")));
                        list = Connector.Get<services>();
                        break;
                    case 3:
                        columns = ModelAttributes.GetFieldsName(Activator.CreateInstance(Type.GetType("FitnesClub.Models.employees")));
                        list = Connector.Get<employees>();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                columns = ModelAttributes.GetFieldsName(Activator.CreateInstance(Type.GetType("FitnesClub.Models.services")));
                list = Connector.Get<services>();
            }            

            if (list != null)
            {
                cbFilter.Items.Clear();
                bindingSource.DataSource = list;
                navigator.BindingSource = bindingSource;
                dgvMain.DataSource = bindingSource;

                for (int i = 0; i < columns.Count; i++)
                {
                    dgvMain.Columns[i].HeaderText = columns[i];
                }

                if (adminMode == false)
                {
                    dgvMain.Columns[0].Visible = false;

                    foreach (var item in columns)
                    {
                        if (item == "Id")
                            continue;
                        cbFilter.Items.Add(item);
                    }
                }
                // Admin mode true
                else
                {
                    foreach (var item in columns)
                    {
                        cbFilter.Items.Add(item);
                    }
                }

                cbFilter.SelectedIndex = 0;
            }
            else
                dgvMain.DataSource = null;
        }

        // Загрузка данных в грид
        private void btnLoad_Click(object sender, EventArgs e) => LoadData();

        // Открытие формы добавления
        private void btnAdd_Click(object sender, EventArgs e)
        {
            isClose = false;
            string table = "";
            switch (cbTables.SelectedIndex)
            {
                case 0:
                    table = "admins";
                    break;
                case 1:
                    table = "clients";
                    break;
                case 2:
                    table = "services";
                    break;
                case 3:
                    table = "employees";
                    break;
                default:
                    break;
            }

            new FormGenerator(this, "Добавить", table, null).Generate();
            Hide();
        }

        // Открытие формы для редактирования
        private void btnEdit_Click(object sender, EventArgs e)
        {
            isClose = false;
            if (dgvMain.RowCount <= 0)
                MessageBox.Show("Нечего изменять!", "Редактирование");

            string table = "";
            switch (cbTables.SelectedIndex)
            {
                case 0:
                    table = "admins";
                    break;
                case 1:
                    table = "clients";
                    break;
                case 2:
                    table = "services";
                    break;
                case 3:
                    table = "employees";
                    break;
                default:
                    break;
            }

            new FormGenerator(this, "Изменить", table, dgvMain[0, dgvMain.CurrentRow.Index].Value.ToString()).Generate();
            Hide();
        }

        // Удалить запись
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMain.RowCount <= 0)
                MessageBox.Show("Нечего удалять!", "Удаление");

            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                switch (cbTables.SelectedIndex)
                {
                    case 0:
                        Connector.Delete(Connector.Get<admins>().Where(a => a.Id == int.Parse(dgvMain[0, dgvMain.CurrentRow.Index].Value.ToString())).FirstOrDefault());
                        break;
                    case 1:
                        Connector.Delete(Connector.Get<clients>().Where(a => a.Id == int.Parse(dgvMain[0, dgvMain.CurrentRow.Index].Value.ToString())).FirstOrDefault());
                        break;
                    case 2:
                        Connector.Delete(Connector.Get<services>().Where(a => a.Id == int.Parse(dgvMain[0, dgvMain.CurrentRow.Index].Value.ToString())).FirstOrDefault());
                        break;
                    case 3:
                        Connector.Delete(Connector.Get<employees>().Where(a => a.Id == int.Parse(dgvMain[0, dgvMain.CurrentRow.Index].Value.ToString())).FirstOrDefault());
                        break;
                    default:
                        break;
                }

                LoadData();
            }
        }

        // Печать
        private void btnPrint_Click(object sender, EventArgs e) => new Exports().ExportToExcel(dgvMain);

        // Поиск данных
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dgvMain.RowCount <= 0)
                MessageBox.Show("Нечего искать!", "Поиск");

            int i, j = 0;

            for (i = 0; i < dgvMain.ColumnCount; i++)
            {
                for (j = 0; j < dgvMain.RowCount; j++)
                {
                    dgvMain[i, j].Style.BackColor = Color.White;
                    dgvMain[i, j].Style.ForeColor = Color.Black;
                }
            }
            for (i = 0; i < dgvMain.ColumnCount; i++)
            {
                for (j = 0; j < dgvMain.RowCount; j++)
                {
                    if ((dgvMain[i, j].FormattedValue.ToString().Contains(tbSearch.Text.Trim())))
                    {
                        dgvMain[i, j].Style.BackColor = Color.DarkGray;
                        dgvMain[i, j].Style.ForeColor = Color.White;
                    }
                }
            }
        }

        // Очистка поле ввода
        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            tbSearch.Clear();
            int i = 0, j = 0;

            for (i = 0; i <= dgvMain.ColumnCount - 1; i++)
            {
                for (j = 0; j <= dgvMain.RowCount - 1; j++)
                {
                    dgvMain[i, j].Style.BackColor = Color.White;
                    dgvMain[i, j].Style.ForeColor = Color.Black;
                }
            }
        }

        // Фильтрация данных
        private void btnFilter_Click(object sender, EventArgs e)
        {
            string table = "";
            string conditionValue = "";
            string filterValue = tbFilter.Text;

            if (string.IsNullOrEmpty(filterValue))
                MessageBox.Show("Нельзя фильтровать без значений. Введите значение и повторите попытку!", "Фильтрация");

            if (adminMode == true)
            {
                switch (cbTables.SelectedIndex)
                {
                    case 0:
                        table = "admins";
                        break;
                    case 1:
                        table = "clients";
                        break;
                    case 2:
                        table = "services";
                        break;
                    case 3:
                        table = "employees";
                        break;
                    default:
                        break;
                }
            }
            else
                table = "services";

            var model = Activator.CreateInstance(Type.GetType("FitnesClub.Models." + table));
            List<string> columns = ModelAttributes.GetFieldsName(model);
            var prop = model.GetType().GetProperties();

            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i] == cbFilter.SelectedItem.ToString())
                {
                    conditionValue = prop[i].Name;
                    break;
                }
            }

            string condition = string.Format("WHERE {0} LIKE '%{1}%'", conditionValue, filterValue);

            dgvMain.DataSource = Connector.Get(table, condition);
        }

        private void btnClerFilter_Click(object sender, EventArgs e) => LoadData();

        // Открытие формы настроек
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isClose = false;
            Settings settings = new Settings();
            settings.btnCancel.Enabled = false;
            settings.Show();
            Close();
        }

        // Загрузка формы
        private void Main_Load(object sender, EventArgs e)
        {
            //Настройка справочной системы
            helpProvider.HelpNamespace = @"Data\Help\Help.chm";
            helpProvider.SetHelpNavigator(this, HelpNavigator.Topic);
            helpProvider.SetShowHelp(this, true);

            if (adminMode != true)
            {
                настройкиToolStripMenuItem.Visible = false;
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;

                cbTables.Items.RemoveAt(0);
                cbTables.Items.RemoveAt(0);
                cbTables.Items.RemoveAt(1);
            }

            cbTables.SelectedIndex = 0;
            LoadData();
        }

        // Закрываем приложение
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isClose == true)
                Application.Exit();
        }
    }
}