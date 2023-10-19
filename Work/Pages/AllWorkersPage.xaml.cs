using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    /// Логика взаимодействия для AllWorkersPage.xaml
    /// </summary>
    public partial class AllWorkersPage : Page
    {
        public static List<Worker> workers { get; set; }
        public static List<Gender> genders { get; set; }

        public AllWorkersPage()
        {
            InitializeComponent();
            workers = new List<Worker> (DBConnection.workerEntities.Worker.ToList());
            genders = new List<Gender> (DBConnection.workerEntities.Gender.ToList());
            this.DataContext = this;
            WorkersLv.ItemsSource = new List<Worker>(DBConnection.workerEntities.Worker.ToList());

        }

        private void ExitBT_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenuPage());
        }

        private void GenderFilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            var filtred = DBConnection.workerEntities.Worker.ToList();

            var gender = GenderFilterCB.SelectedItem as Gender;
            var surchText = SearchTB.Text.ToLower();

            //if (GenderFilterCB.SelectedIndex != 0 && gender != null)
                filtred = filtred.Where(x => x.IDGender == gender.ID).ToList();
      

            if (!string.IsNullOrWhiteSpace(surchText))
                filtred = filtred.Where(x => x.Surname.ToLower().Contains(surchText)).ToList();
            WorkersLv.ItemsSource = filtred.ToList();
        }

        private void SearchTB_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
