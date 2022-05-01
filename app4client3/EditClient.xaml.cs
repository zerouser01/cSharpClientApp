using System.Linq;
using System.Text;
using System.Windows;


namespace app4client3
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient
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
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text))
                sb.AppendLine(Constants.ENTER_NAIMENOVANIE);
            if (string.IsNullOrEmpty(FieldofActivityTextBox.Text))
                sb.AppendLine(Constants.ENTER_DESKRIPTION);
            if (string.IsNullOrEmpty(InnTextBox.Text))
                sb.AppendLine(Constants.ENTER_INN);

            if (sb.Length > 0)
            {//если пользователь неправильно ввел данные
                MessageBox.Show(sb.ToString());
                return;
            }

            var currentClient = ApplicationContext.GetContext().Clients.FirstOrDefault(c => c.ID == instance.ID);
            if (currentClient != null)
            {
                currentClient.Name = NameTextBox.Text;
                currentClient.FieldOfActivity = FieldofActivityTextBox.Text;
                currentClient.INN = InnTextBox.Text;
            }
            ApplicationContext.GetContext().SaveChanges();
            MessageBox.Show(Constants.SUCCESS);
            Close();
        }
    }
}
