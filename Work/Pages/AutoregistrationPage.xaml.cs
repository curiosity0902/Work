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
using Work.DB;

namespace Work.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutoregistrationPage.xaml
    /// </summary>
    public partial class AutoregistrationPage : Page
    {
        public static List<Worker> workers { get; set; }
        public AutoregistrationPage()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text.Trim();
            string password = PasswordTB.Password.Trim();

            workers = new List<Worker>(DBConnection.workerEntities.Worker.ToList());
            Worker currentUser = workers.FirstOrDefault(i => i.Login == login && i.Password == password);

            if (currentUser != null)
                NavigationService.Navigate(new MainMenuPage());
            else
                MessageBox.Show(" Введите данные корректно!");

        }
    }
}
