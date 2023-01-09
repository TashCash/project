using System.IO;
using System.Windows.Forms;

namespace FitnesClub.Forms
{
    public partial class Settings : Form
    {
        public Settings() => InitializeComponent();

        private void btnConnection_Click(object sender, System.EventArgs e)
        {
            if (tbServer.Text.Equals("") || tbUser.Text.Equals("") || tbPassword.Text.Equals("") || tbDBName.Text.Equals("") || tbPort.Text.Equals(""))
                MessageBox.Show("Вы не ввели все данные!", "Предупреждение!");
            else
            {
                try
                {
                    StreamWriter streamWriter = new StreamWriter(@"Data\\config.cfg");
                    streamWriter.WriteLine(string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};",
                        tbServer.Text, tbPort.Text, tbDBName.Text, tbUser.Text, tbPassword.Text));
                    streamWriter.Close();
                    MessageBox.Show("Данные были сохранены!", "Созданно!");
                    new Splashscreen().Show();
                    Close();
                }
                catch (System.Exception exc)
                {
                    MessageBox.Show(exc.Message, "Ошибка чтения конфиг файла");
                }
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            new Auth().Show();
            Close();
        }

        private void btnReturn_Click(object sender, System.EventArgs e)
        {
            new Main(true).Show();
            Close();
        }
    }
}