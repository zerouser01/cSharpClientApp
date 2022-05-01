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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace app4client3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /* Здесь создается экземпляр класса Clients для того чтобы во время получения списка тикетов
         * присвоить ему значение того клиента, тикеты которого необходимо отобразить в TicketsGrid
         */
        private Clients _instance4Newform;
        public MainWindow()
        {
            InitializeComponent();
            ClientsGrid.ItemsSource = OrganizationEntities.GetContext().Clients.ToList();
            //aga
        }


        private void ClientsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //не получилось брать тикеты по двойному клику, не знаю как представить объект сендер
            //Clients obj = (sender as DataGridCell).DataContext as Clients;

            //MessageBox.Show(ClientsGrid.SelectedIndex.ToString());
            //MessageBox.Show(obj.INN.ToString());
        }

        private void BtnEdit(object sender, RoutedEventArgs e)
        {
            /* Событие для кнопки "вызов окна редактирования данных о клиенте"
             */
            var obj = (sender as Button)?.DataContext as Clients;
            var window = new EditClient(obj);
            window.Show();
        }

        private List<Tickets> RefreshTickets(int clientID)
        {
            /* функция обновления списка тикетов. принимает в себя значение clientID
             * создает список типа Tickets, получает все тикеты из базы и проверяет каждый на принадлежность к clientID
             * возвращает список из подходящих тикетов. 
             */
            List<Tickets> arr = new List<Tickets>();
            foreach (Tickets t in OrganizationEntities.GetContext().Tickets.ToList())
            {
                if (t.ClientID == clientID)
                {
                    arr.Add(t);
                }
            }
            return arr;
        }
        
        private void BtnTickets(object sender, RoutedEventArgs e)
        {
            /* Действие при нажатии на кнопку =>
             * обрабатываем сендер как объект типа Client 
             * получаем список из подходящих тикетов
             * заполняем TicketsGrid полученным cписком
             */
            var obj = (sender as Button)?.DataContext as Clients;
            _instance4Newform = obj;
            if (obj == null) return;
            var arr = RefreshTickets(obj.ID);

            if (arr.Count > 0)
            {
                TicketsGrid.ItemsSource = arr;
                btnAddTicket.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("У выбранного вами клиента нет заявок");
                TicketsGrid.ItemsSource = null;
                btnAddTicket.IsEnabled = true;
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
            OrganizationEntities.GetContext().SaveChanges();
            ClientsGrid.ItemsSource = OrganizationEntities.GetContext().Clients.ToList();
        }



        private void btnAddTicket_Click(object sender, RoutedEventArgs e)
        {

            var a = new AddTicket(_instance4Newform);
            a.Show();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            /* Действие при нажатии на кнопку "сохранить изменения"
             */
            OrganizationEntities.GetContext().SaveChanges();
        }

        private void btnDelTicket_Click(object sender, RoutedEventArgs e)
        {
            /* Действие при нажатии на кнопку "DELETE" (Tickets) 
             * Обрабатываем сендер как объект класса Tickets, сохраняем значение СlientID
             * Вызываем контекст базы данных, обращаемся к классу Tickets и удаляем полученный из сендера экземпляр
             * Сохраняем изменения
             * С помощью функции refreshTickets передаем сохраненное значение ClientID, получаем новый список тикетов
             * загружаем список тикетов в TicketsGrid
             */
            var dataContext = (sender as Button)?.DataContext as Tickets;
            if (dataContext != null)
            {
                var id = dataContext.ClientID;
            }

            if (dataContext != null && dataContext.ClientID > 0)
            {
                OrganizationEntities.GetContext().Tickets.Remove(dataContext);
                OrganizationEntities.GetContext().SaveChanges();
                var arr = RefreshTickets(dataContext.ClientID);
                TicketsGrid.ItemsSource = arr;
            }
            else return;
        }

        private void Button_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            /*Событие для кнопки "добавить клиента"
             */
            var add = new AddClient();
            add.Show();
        }

        private void RefreshClient_Click(object sender, RoutedEventArgs e)
        {
            /*Событие для кнопки "обновить" (clientsGrid)*/
            ClientsGrid.ItemsSource = OrganizationEntities.GetContext().Clients.ToList();
        }


        private void delClient_Click(object sender, RoutedEventArgs e)
        {
            /* Событие для кнопки "удалить клиента"
             */
            if ((sender as Button)?.DataContext is Clients obj) OrganizationEntities.GetContext().Clients.Remove(obj);
            OrganizationEntities.GetContext().SaveChanges();
            ClientsGrid.ItemsSource = OrganizationEntities.GetContext().Clients.ToList();
        }

        private void btnSaveTicket_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnEditTicket_Click(object sender, RoutedEventArgs e)
        {
            /* Событие для кнопки "ЕDIT" - редактирование тикета, вызов соответствующего окна
             */
            if (!((sender as Button)?.DataContext is Tickets tickets)) return;
            var window = new EditTicket(_instance4Newform, tickets);
            window.Show();
        }
        

        private void ClientsGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if ((sender as DataGrid)?.CurrentCell.Item is Clients clients) labelAbout.Content = clients.About;
        }
    }
}
