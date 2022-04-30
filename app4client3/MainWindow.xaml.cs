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
        Clients instance4newform;
        public MainWindow()
        {
            InitializeComponent();
            ClientsGrid.ItemsSource = OrganizationEntities.GetContext().Clients.ToList();

        }


        private void ClientsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //не получилось брать тикеты по двойному клику, не знаю как представить объект сендер
            //Clients obj = (sender as DataGridCell).DataContext as Clients;

            //MessageBox.Show(ClientsGrid.SelectedIndex.ToString());
            //MessageBox.Show(obj.INN.ToString());
        }

        private void btnEdit(object sender, RoutedEventArgs e)
        {
            /* Событие для кнопки "вызов окна редактирования данных о клиенте"
             */
            Clients obj = (sender as Button).DataContext as Clients;
            EditClient window = new EditClient(obj);
            window.Show();
        }

        private List<Tickets> refreshTickets(int clientID)
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

        private void btnTickets(object sender, RoutedEventArgs e)
        {
            /* Действие при нажатии на кнопку =>
             * обрабатываем сендер как объект типа Client 
             * получаем список из подходящих тикетов
             * заполняем TicketsGrid полученным cписком
             */
            Clients obj = (sender as Button).DataContext as Clients;
            instance4newform = obj;
            List<Tickets> arr = refreshTickets(obj.ID);

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
            if(obj.NumberOfTickets != arr.Count)
            {
                
                StringBuilder message = new StringBuilder();
                message.AppendLine("Число заявок клиента в таблице с информацией о клиенте не совпадает с реальным количеством заявок!");
                message.AppendLine("Количество заявок в таблице клиента: " + obj.NumberOfTickets);
                message.AppendLine("Количество фактических заявок удалось найти: " + arr.Count);
                message.AppendLine("Обратитесь к разработчику, либо к системному администратору");
                message.AppendLine("Количество заявок в таблице клиента будет изменено на фактическое");

                MessageBox.Show(message.ToString());
                obj.NumberOfTickets = arr.Count;
                OrganizationEntities.GetContext().SaveChanges();
                ClientsGrid.ItemsSource = OrganizationEntities.GetContext().Clients.ToList();
                

            }
        }



        private void btnAddTicket_Click(object sender, RoutedEventArgs e)
        {

            AddTicket a = new AddTicket(instance4newform);
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
            Tickets obj = (sender as Button).DataContext as Tickets;
            var id = obj.ClientID;
            if (obj.ClientID > 0)
            {
                OrganizationEntities.GetContext().Tickets.Remove(obj);
                OrganizationEntities.GetContext().SaveChanges();
                List<Tickets> arr = refreshTickets(obj.ClientID);
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
            AddClient add = new AddClient();
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
            Clients obj = (sender as Button).DataContext as Clients;
            OrganizationEntities.GetContext().Clients.Remove(obj);
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
            Tickets obj = (sender as Button).DataContext as Tickets;
            EditTicket window = new EditTicket(instance4newform, obj);
            window.Show();
        }
    }
}
