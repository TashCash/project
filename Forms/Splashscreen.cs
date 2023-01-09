using FitnesClub.Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace FitnesClub.Forms
{
    public partial class Splashscreen : Form
    {
        public Splashscreen() => InitializeComponent();

        private void timerMain_Tick(object sender, EventArgs e)
        {
            //Если существует файл настроек подключения
            if (File.Exists(@"Data\config.cfg"))
            {
                //Создаем объект для чтения из файла читаем данные о подключении в переменную
                StreamReader streamReader = new StreamReader(@"Data\config.cfg");
                string ConnectionString = streamReader.ReadLine();
                streamReader.Close();

                //Передаем строку подключения на ранение в экземпляр класса
                Connector.NpgsqlConnectionstring = ConnectionString;                

                if (Connector.SQLQuery("SELECT version();") == true)
                {
                    //Остановка таймера
                    timerMain.Stop();
                    //Загрузка формы авторизации
                    new Auth().Show();
                    //Скрытие этой формы
                    Hide();
                }
                else
                {
                    labelConn.Text = "Ошибка...";
                    //Остановка таймера
                    timerMain.Stop();
                    //Предложение об ручной настройке соединения
                    if (MessageBox.Show("Не удалось подключиться к базе данных, \nНастроить подключение?", "Ошибка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //Если нажали да, то создаем экземпляр класса формы настроек
                        Settings settings = new Settings();
                        settings.btnReturn.Enabled = false;
                        settings.btnCancel.Enabled = true;
                        settings.Show();
                        //Скрываем эту форму
                        Hide();
                    }
                    else
                        Application.Exit();
                }
            }
            else
            {
                //Создаем объект для создания файла со строкой подключения
                new StreamWriter(@"Data\config.cfg").Close();
            }
        }

        private void Splashscreen_Load(object sender, EventArgs e)
        {
            labelConn.Text = "Загружаем";
            timerMain.Interval = 1000;
            timerMain.Start();
        }
    }
}