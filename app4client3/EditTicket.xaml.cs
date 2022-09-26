using System.Linq;
using System.Text;
using System.Windows;

namespace app4client3
{
    /// <summary>
    /// Логика взаимодействия для EditTicket.xaml
    /// </summary>
    public partial class EditTicket
    {
        Clients cInstance;
        Tickets tInstance;
        
        public EditTicket(Clients client, Tickets ticket)
        {
            InitializeComponent();
            cInstance = client;
            tInstance = ticket;
            labelClient.Content = client.Name + " \"" + client.FieldOfActivity + "\""; 
            comboStatus.ItemsSource = ApplicationContext.GetContext().Status.Select(c => c.CurrentStatus).ToArray();
            NaimenovanieTextBox.Text = tInstance.Naimenovanie;
            DeskriptTextBox.Text = tInstance.Deskription;
            
        }
        private string[] GetStatus()
        {
            var stat = ApplicationContext.GetContext().Status.Select(c => c.CurrentStatus).ToArray();
            return stat;
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(NaimenovanieTextBox.Text))
                sb.AppendLine(Constants.ENTER_NAIMENOVANIE);
            if (string.IsNullOrEmpty(DeskriptTextBox.Text))
                sb.AppendLine(Constants.ENTER_DESKRIPTION);
            if (comboStatus.SelectedItem == null)
                sb.AppendLine(Constants.ENTER_STATUS);

            if (sb.Length > 0)
            {//если пользователь неправильно ввел данные
                MessageBox.Show(sb.ToString());
                return;
            }

            var statusString = GetStatus();
            var selectedIndex = comboStatus.SelectedIndex;
            tInstance.Status = statusString[selectedIndex];

            tInstance.Naimenovanie = NaimenovanieTextBox.Text;
            tInstance.Deskription = DeskriptTextBox.Text;
            tInstance.DateOfTickets = System.DateTime.Now;
            ApplicationContext.GetContext().SaveChangesAsync();
            MessageBox.Show(Constants.SUCCESS + selectedIndex);
            Close();
        }
    }
}
