using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace app4client3
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        Clients instance;
        public EditClient(Clients client)
        {
            InitializeComponent();
            instance = client;
            NameTextBox.Text = client.Name;
            InnTextBox.Text = client.INN;
            FieldofActivityTextBox.Text = client.FieldOfActivity;
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
                var custom = OrganizationEntities.GetContext().Clients.Where(c => c.ID == instance.ID).FirstOrDefault();
                custom.Name = NameTextBox.Text;
                custom.FieldOfActivity = FieldofActivityTextBox.Text;
                custom.INN = InnTextBox.Text;
                int i = OrganizationEntities.GetContext().SaveChanges();
                if(i > 0)
                {
                    MessageBox.Show("Данные успешно обновлены! " + i);
                    Close();
                }
                
            }
        }
    }
}
