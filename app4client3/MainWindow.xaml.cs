using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace app4client3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /* Здесь создается экземпляр класса Clients для того чтобы во время получения списка тикетов
         * присвоить ему значение того клиента, тикеты которого необходимо отобразить в TicketsGrid
         */
        private Clients _instance4NewForm;
        public MainWindow()
        {
            InitializeComponent();
            ClientsGrid.ItemsSource = ApplicationContext.GetContext().Clients.ToList();
        }


        private void BtnEdit(object sender, RoutedEventArgs e)
        {
            /* Событие для кнопки "вызов окна редактирования данных о клиенте"
             */
            var obj = (sender as Button)?.DataContext as Clients;
            var window = new EditClient(obj);
            window.Show();
        }
        
        /* функция обновления списка тикетов. принимает в себя значение clientID
         * создает список типа Tickets, получает все тикеты из базы и проверяет каждый на принадлежность к clientID
         * возвращает список из подходящих тикетов. 
         */
        private List<Tickets> RefreshTickets(int clientId) => 
            ApplicationContext
                .GetContext()
                .Tickets
                .Where(item => item.ClientID == clientId)
                .ToList();
            
        
        
        private void BtnTickets(object sender, RoutedEventArgs e)
        {
            /* Действие при нажатии на кнопку =>
             * обрабатываем сендер как объект типа Client 
             * получаем список из подходящих тикетов
             * заполняем TicketsGrid полученным cписком
             */
            var obj = (sender as Button)?.DataContext as Clients;
            _instance4NewForm = obj;
            if (obj == null) return;
            var arr = RefreshTickets(obj.ID);

            if (arr.Count > 0)
            {
                TicketsGrid.ItemsSource = arr;
                BtnAddTicket.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("У выбранного вами клиента нет заявок");
                TicketsGrid.ItemsSource = null;
                BtnAddTicket.IsEnabled = true;
            }

            if (obj.NumberOfTickets == arr.Count) return;
            var message = new StringBuilder();
            message.AppendLine("Число заявок клиента в таблице с информацией о клиенте не совпадает с реальным количеством заявок!");
            message.AppendLine("Количество заявок в таблице клиента: " + obj.NumberOfTickets);
            message.AppendLine("Количество фактических заявок удалось найти: " + arr.Count);
            message.AppendLine("Обратитесь к разработчику, либо к системному администратору");
            message.AppendLine("Количество заявок будет изменено на фактическое");

            MessageBox.Show(message.ToString());
            obj.NumberOfTickets = arr.Count;
            ApplicationContext.GetContext().SaveChanges();
            ClientsGrid.ItemsSource = ApplicationContext.GetContext().Clients.ToList();
        }



        private void BtnAddTicket_Click(object sender, RoutedEventArgs e)
        {

            var a = new AddTicket(_instance4NewForm);
            a.Show();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            /* Действие при нажатии на кнопку "сохранить изменения"
             */
            ApplicationContext.GetContext().SaveChanges();
        }

        private void BtnDelTicket_Click(object sender, RoutedEventArgs e)
        {
            /* Действие при нажатии на кнопку "DELETE" (Tickets) 
             * Обрабатываем сендер как объект класса Tickets, сохраняем значение СlientID
             * Вызываем контекст базы данных, обращаемся к классу Tickets и удаляем полученный из сендера экземпляр
             * Сохраняем изменения
             * С помощью функции refreshTickets передаем сохраненное значение ClientID, получаем новый список тикетов
             * загружаем список тикетов в TicketsGrid
             */

            if (!((sender as Button)?.DataContext is Tickets dataContext) || dataContext.ClientID <= 0) 
                return;
            
            ApplicationContext.GetContext().Tickets.Remove(dataContext);
            ApplicationContext.GetContext().SaveChanges();
            TicketsGrid.ItemsSource = RefreshTickets(dataContext.ClientID);
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            /*Событие для кнопки "добавить клиента"
             */
            var add = new AddClient();
            add.Show();
        }

        private void RefreshClient_Click(object sender, RoutedEventArgs e)
        {
            /*Событие для кнопки "обновить" (clientsGrid)*/
            ClientsGrid.ItemsSource = ApplicationContext.GetContext().Clients.ToList();
        }


        private void DelClient_Click(object sender, RoutedEventArgs e)
        {
            /* Событие для кнопки "удалить клиента"
             */
            if ((sender as Button)?.DataContext is Clients obj) ApplicationContext.GetContext().Clients.Remove(obj);
            ApplicationContext.GetContext().SaveChanges();
            ClientsGrid.ItemsSource = ApplicationContext.GetContext().Clients.ToList();
        }

        private void BtnEditTicket_Click(object sender, RoutedEventArgs e)
        {
            /* Событие для кнопки "ЕDIT" - редактирование тикета, вызов соответствующего окна
             */
            if (!((sender as Button)?.DataContext is Tickets tickets)) return;
            var window = new EditTicket(_instance4NewForm, tickets);
            window.Show();
        }
        

        private void ClientsGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if ((sender as DataGrid)?.CurrentCell.Item is Clients clients) LabelAbout.Content = clients.About;
        }
    }
}
