using FitnesClub.Classes;
using FitnesClub.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FitnesClub.Forms
{
    public partial class Auth : Form
    {
        public Auth() => InitializeComponent();

        // Авторизация
        private void btnAuth_Click(object sender, EventArgs e)
        {
            bool adminMode = false;
            string login = tbLogin.Text;
            string password = tbPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                MessageBox.Show("Все поля обязательны для заполнения!","Внимание!");
            else
            {
                if (Connector.Get<clients>().Where(u => u.Login.Equals(login) && u.Password.Equals(password)).FirstOrDefault() != null)
                    adminMode = false;
                else if (Connector.Get<admins>().Where(u => u.Login.Equals(login) && u.Password.Equals(password)).FirstOrDefault() != null)
                    adminMode = true;
                else
                {
                    MessageBox.Show("Не верный логин или пароль!", "Внимание!");
                    return;
                }

                new Main(adminMode).Show();
                Close();
            }
        }

        // Закрыть приложение
        private void btnClose_Click(object sender, EventArgs e) => Application.Exit();
    }
}