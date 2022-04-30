using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace app4client3
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    
    
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text))
                sb.AppendLine("Введите имя клиента");
            if (string.IsNullOrEmpty(FieldofActivityTextBox.Text))
                sb.AppendLine("Введите Поле деятельности");
            if (string.IsNullOrEmpty(InnTextBox.Text))
                sb.AppendLine("Введите ИНН");

            if (sb.Length > 0)
            {//если пользователь неправильно ввел данные
                MessageBox.Show(sb.ToString());
                return;
            }
            else
            {
                Clients _client = new Clients();
                _client.Name = NameTextBox.Text;
                _client.FieldOfActivity = FieldofActivityTextBox.Text;
                _client.INN = InnTextBox.Text;
                _client.DateLastTicket = DateTime.Now;
                OrganizationEntities.GetContext().Clients.Add(_client);
                if(OrganizationEntities.GetContext().SaveChanges() == 1)
                {
                    MessageBox.Show("Клиент успешно добавлен!");
                    
                    Close();
                }

            }

        }
    }
}
