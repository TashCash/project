using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FitnesClub.Classes
{
    public class FormGenerator
    {
        static int lblSize = 0;
        static Form _form;
        static string _table;
        static string _switchTable;
        static string _action;

        static Panel panel;
        static TextBox tbBuffer = new TextBox();

        static List<string> values = new List<string>();
        static string _idEdit;

        static object model = null;
        static List<string> ruColumns = null;
        static PropertyInfo[] prop = null;

        public FormGenerator(Form form, string action, string table, string idEdit)
        {
            _form = form;
            _table = table;
            _switchTable = table;
            _action = action;
            _idEdit = idEdit;

            // Генерация объёкта модели
            model = Activator.CreateInstance(Type.GetType("FitnesClub.Models." + _table));
            // Получение русских названий полей
            ruColumns = ModelAttributes.GetFieldsName(model);
            // Получние получение свойств модели
            prop = model.GetType().GetProperties();
        }

        public static void Form_FormClosing(object sender, FormClosingEventArgs e) => new Main(true).Show();

        static string tbName = "";
        public static void ButtonOk_Click(object sender, EventArgs e)
        {
            foreach (Control itemPanel in panel.Controls)
            {
                if (itemPanel.GetType().Equals(typeof(TextBox)))
                {
                    tbBuffer = (TextBox)itemPanel;

                    string Name = tbBuffer.Name;

                    if (tbName == Name)
                    {
                        continue;
                    }

                    tbName = tbBuffer.Name.ToString();

                    if (string.IsNullOrEmpty(tbBuffer.Text))
                    {
                        MessageBox.Show("Не все поля заполненны!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbBuffer.Focus();
                        return;
                    }
                }
            }

            string query = "";

            if (_action.Equals("Добавить"))
            {
                query += "INSERT INTO " + _table + " (";

                for (int i = 0; i < prop.Length; i++)
                {
                    query += prop[i].Name + ",";
                }

                query = query.Substring(0, query.LastIndexOf(","));
                query += ") VALUES ('";

                foreach (Control itemPanel in panel.Controls)
                {
                    if (itemPanel.GetType().Equals(typeof(TextBox)))
                    {
                        tbBuffer = (TextBox)itemPanel;
                        query += tbBuffer.Text + "','";
                    }
                }

                query = query.Substring(0, query.LastIndexOf("',"));
                query += "');";

                if (Connector.SQLQuery(query) == false)
                    MessageBox.Show("Не смог добавить новую запись. Ошибка введёных данных. " +
                        "Проверьте корректность данных и повторите попытку", "Ошибка");
                else
                    MessageBox.Show("Результат был внесен в базу, можете закрыть форму!", "Добавление");
            }
            // Изменить
            else
            {
                tbName = "";

                values.Clear();

                query += "UPDATE " + _table + " SET";

                foreach (Control itemPanel in panel.Controls)
                {
                    if (itemPanel.GetType().Equals(typeof(TextBox)))
                    {
                        tbBuffer = (TextBox)itemPanel;

                        string Name = tbBuffer.Name;

                        if (tbName == Name)
                        {
                            continue;
                        }

                        tbName = tbBuffer.Name.ToString();

                        values.Add(tbBuffer.Text);
                    }
                }

                for (int i = 0; i < prop.Length; i++)
                {
                    if (i == 0)
                    {
                        query += " " + prop[i].Name + " = '" + _idEdit + "',";
                        continue;
                    }

                    query += " " + prop[i].Name + " = '" + values[i - 1] + "',";
                }

                query = query.Substring(0, query.LastIndexOf("',"));
                query += "' WHERE Id = " + _idEdit;
                
                if (Connector.SQLQuery(query) == false)
                    MessageBox.Show("Не смог изменить текущую запись. Ошибка введёных данных. " +
                        "Проверьте корректность данных и повторите попытку", "Ошибка");
                else
                    MessageBox.Show("Результат был внесен в базу, можете закрыть форму!", "Добавление");
            }
        }

        public Label GenerateLbl(string Text, string Name, int lblY)
        {
            Label label = new Label
            {
                Location = new Point(6, lblY),
                Name = "lbl" + Name,
                Anchor = ((AnchorStyles.Top | AnchorStyles.Left)),
                Size = new Size(346, 22),
                Text = Text,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Margin = new Padding(10)
            };

            lblSize = label.Size.Height;
            return label;
        }

        public TextBox GenerateTb(string Text, string Name, int tbY)
        {
            return new TextBox
            {
                Location = new Point(9, tbY),
                Name = "tb" + Name,
                Anchor = ((AnchorStyles.Top | AnchorStyles.Left)),
                Size = new Size(346, 20),
                Text = Text,
                Margin = new Padding(10)
            };
        }

        public void Generate()
        {
            Form form = new Form
            {
                Width = 430,
                Height = 400,
                Font = _form.Font,
                Icon = _form.Icon,
                BackColor = _form.BackColor,
                ForeColor = _form.ForeColor,
                FormBorderStyle = _form.FormBorderStyle,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                Text = _action
            };

            form.FormClosing += Form_FormClosing;

            panel = new Panel
            {
                Name = "panelMain",
                Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(12, 12),
                Padding = new Padding(10),
                Margin = new Padding(20)
            };

            int tbY = 0;
            int lblY = 0;

            if (_action.Equals("Добавить"))
            {
                // Генерация label и textBox полей
                for (int i = 0; i < ruColumns.Count; i++)
                {
                    if (tbY == 0 && lblY == 0)
                    {
                        lblY += 7;
                        tbY += 23;
                    }
                    else
                    {
                        lblY += 47;
                    }

                    try
                    {
                        panel.Controls.Add(GenerateLbl(ruColumns[i], prop[i].Name, lblY));
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                    tbY = lblY + lblSize + 10;

                    try
                    {
                        panel.Controls.Add(GenerateTb("", prop[i].Name, tbY));
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                    lblY += 10;
                }

                panel.AutoScrollMinSize = new Size(0, (55 * ruColumns.Count));
            }
            // Изменить
            else
            {
                model = Connector.Get(_table, "WHERE Id = " + _idEdit).FirstOrDefault();

                for (int i = 1; i < ruColumns.Count; i++)
                {
                    if (tbY == 0 && lblY == 0)
                    {
                        lblY += 7;
                        tbY += 23;
                    }
                    else
                    {
                        lblY += 47;
                    }

                    panel.Controls.Add(GenerateLbl(ruColumns[i], prop[i].Name, lblY));

                    tbY = lblY + lblSize + 10;

                    panel.Controls.Add(GenerateTb(prop[i].GetValue(model, null).ToString(), prop[i].Name, tbY));

                    lblY += 10;
                }

                panel.AutoScrollMinSize = new Size(0, (55 * ruColumns.Count));
            }

            panel.Size = new Size(390, 308);

            Button bOk = new Button
            {
                Name = "bOk",
                Text = "Применить",
                Width = 390,
                Height = 40,
                Anchor = (AnchorStyles.Bottom | AnchorStyles.Left),
                Location = new Point(12, 320),
                ForeColor = Color.White,
                Margin = new Padding(20)
            };

            bOk.Click += ButtonOk_Click;

            form.Controls.Add(panel);
            form.Controls.Add(bOk);
            form.Show();
        }
    }
}
