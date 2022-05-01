using System;
using System.Text;
using System.Windows;

namespace app4client3
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    
    
    public partial class AddClient
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text))
                sb.AppendLine(Constants.ENTER_CLIENT_NAME);
            if (string.IsNullOrEmpty(FieldofActivityTextBox.Text))
                sb.AppendLine(Constants.ENTER_FIELDOFACTIVITY);
            if (string.IsNullOrEmpty(InnTextBox.Text))
                sb.AppendLine(Constants.ENTER_INN);

            if (sb.Length > 0)
            {//если пользователь неправильно ввел данные
                MessageBox.Show(sb.ToString());
            }
            else
            {
                var client = new Clients
                {
                    Name = NameTextBox.Text,
                    FieldOfActivity = FieldofActivityTextBox.Text,
                    INN = InnTextBox.Text,
                    DateLastTicket = DateTime.Now
                };
                ApplicationContext.GetContext().Clients.Add(client);
                ApplicationContext.GetContext().SaveChanges();
                MessageBox.Show(Constants.ADD_CLIENT);
                Close();
            }

        }
    }
}
