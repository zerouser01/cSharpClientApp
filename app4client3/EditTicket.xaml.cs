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
    /// Логика взаимодействия для EditTicket.xaml
    /// </summary>
    public partial class EditTicket : Window
    {
        Clients cInstance;
        Tickets tInstance;
        public EditTicket(Clients client, Tickets ticket)
        {
            InitializeComponent();
            cInstance = client;
            tInstance = ticket;
            labelClient.Content = client.Name + " \"" + client.FieldOfActivity + "\""; 
            comboStatus.ItemsSource = OrganizationEntities.GetContext().Status.Select(c => c.CurrentStatus).ToArray();
            NaimenovanieTextBox.Text = tInstance.Naimenovanie;
            DeskriptTextBox.Text = tInstance.Deskription;
        }
        private string[] getStatus()
        {
            string[] stat = OrganizationEntities.GetContext().Status.Select(c => c.CurrentStatus).ToArray();
            return stat;
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(NaimenovanieTextBox.Text))
                sb.AppendLine("Введите наименование работ");
            if (string.IsNullOrEmpty(DeskriptTextBox.Text))
                sb.AppendLine("Введите описание работ");
            if (comboStatus.SelectedItem == null)
                sb.AppendLine("Выберите статус заявки");

            if (sb.Length > 0)
            {//если пользователь неправильно ввел данные
                MessageBox.Show(sb.ToString());
                return;
            }

            string[] statusString = getStatus();
            int i = comboStatus.SelectedIndex;
            tInstance.Status = statusString[i];

            tInstance.Naimenovanie = NaimenovanieTextBox.Text;
            tInstance.Deskription = DeskriptTextBox.Text;
            tInstance.DateOfTickets = System.DateTime.Now;
            int k = OrganizationEntities.GetContext().SaveChanges();
            if (k > 0)
            {
                MessageBox.Show("Данные успешно обновлены! " + i);
                Close();
            }

        }
    }
}
