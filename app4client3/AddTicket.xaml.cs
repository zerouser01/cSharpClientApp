using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace app4client3
{
    /// <summary>
    /// Логика взаимодействия для AddTicket.xaml
    /// </summary>
    public partial class AddTicket
    {
        /* Конструктор по умолчанию принимает объект класса Clients, для того чтобы мы знали
         * какому клиенту мы добавляем заявку
         */
        Clients instance;

        public AddTicket(Clients client)
        {
            InitializeComponent();
            instance = client;
            labelClient.Content = client.Name + " \"" + client.FieldOfActivity + "\"";
            comboStatus.ItemsSource = ApplicationContext.GetContext().Status.Select(c => c.CurrentStatus).ToArray();
        }
        

        private string[] GetStatus() =>
            ApplicationContext
                .GetContext()
                .Status
                .Select(c => c.CurrentStatus)
                .ToArray();
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /* Cобытие при нажатии на кнопку "добавить тикет"
             */
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(NaimenovanieTextBox.Text))
                sb.AppendLine(Constants.ENTER_NAIMENOVANIE);
            if (string.IsNullOrEmpty(DeskriptTextBox.Text))
                sb.AppendLine(Constants.ENTER_DESKRIPTION);
            if (comboStatus.SelectedItem == null)
                sb.AppendLine(Constants.ENTER_STATUS);

            if(sb.Length > 0)
            {//если пользователь неправильно ввел данные
                MessageBox.Show(sb.ToString());
                return;
            }
                    /* Механизм добавления статуса: получаем массив из всех валидных статусов
                     * получаем индекс выбранного в comboBox элемента
                     * присваиваем в newticket.Status тот элемент массива, который соответствует индексу комбобокса
                     */
                    var newticket = new Tickets();

                    var statusString = GetStatus();
                    var selectedIndex = comboStatus.SelectedIndex;
                    newticket.Status = statusString[selectedIndex];

                    newticket.Naimenovanie = NaimenovanieTextBox.Text;
                    newticket.Deskription = DeskriptTextBox.Text;
                    newticket.DateOfTickets = DateTime.Now;
                    newticket.ClientID = instance.ID;
                    instance.NumberOfTickets++; //увеличиваем количество заявок у клиента на 1
                    instance.DateLastTicket = DateTime.Now; //изменяем дату последнего тикета у клиента
                    ApplicationContext.GetContext().Tickets.Add(newticket);
                    ApplicationContext.GetContext().SaveChanges();
                    MessageBox.Show(Constants.SUCCESS_ADDTICKET);
                    Close();
        }
    }
}
