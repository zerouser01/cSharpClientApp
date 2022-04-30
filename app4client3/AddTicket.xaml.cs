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
    /// Логика взаимодействия для AddTicket.xaml
    /// </summary>
    public partial class AddTicket : Window
    {
        /* Конструктор по умолчанию принимает объект класса Clients, для того чтобы мы знали
         * какому клиенту мы добавляем заявку
         */
        Clients instance;

        public AddTicket(Clients client)
        {
            InitializeComponent();
            instance = client;
            string clientstring = client.Name + " \"" + client.FieldOfActivity + "\"";
            labelClient.Content = clientstring;
            comboStatus.ItemsSource = OrganizationEntities.GetContext().Status.Select(c => c.CurrentStatus).ToArray();
        }
        

        private string[] getStatus()
        {
            string[] stat = OrganizationEntities.GetContext().Status.Select(c => c.CurrentStatus).ToArray();
            return stat;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /* Cобытие при нажатии на кнопку "добавить тикет"
             */
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(NaimenovanieTextBox.Text))
                sb.AppendLine("Введите наименование работ");
            if (string.IsNullOrEmpty(DeskriptTextBox.Text))
                sb.AppendLine("Введите описание работ");
            if (comboStatus.SelectedItem == null)
                sb.AppendLine("Выберите статус заявки");

            if(sb.Length > 0)
            {//если пользователь неправильно ввел данные
                MessageBox.Show(sb.ToString());
                return;
            }
            
                    /* Механизм добавления статуса: получаем массив из всех валидных статусов
                     * получаем индекс выбранного в comboBox элемента
                     * присваиваем в newticket.Status тот элемент массива, который соответствует индексу комбобокса
                     */
                    Tickets newticket = new Tickets();

                    string[] statusString = getStatus();
                    int i = comboStatus.SelectedIndex;
                    newticket.Status = statusString[i];

                    newticket.Naimenovanie = NaimenovanieTextBox.Text;
                    newticket.Deskription = DeskriptTextBox.Text;
                    newticket.DateOfTickets = System.DateTime.Now;
                    newticket.ClientID = instance.ID;
                    instance.NumberOfTickets += 1; //увеличиваем количество заявок у клиента на 1
                    instance.DateLastTicket = System.DateTime.Now; //изменяем дату последнего тикета у клиента
                    OrganizationEntities.GetContext().Tickets.Add(newticket);
                    int status = OrganizationEntities.GetContext().SaveChanges();
                    if (status > 0)
                    {
                        MessageBox.Show($"Запись тикета в базу успешна! {status}");
                        Close();
                    }
                    
                
                
                
            
            
            
        }
    }
}
