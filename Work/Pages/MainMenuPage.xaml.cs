using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public static List<Worker> workers { get; set; }
        public static List<Gender> genders { get; set; }
        public static Worker worker = new Worker();
        public MainMenuPage()
        {
            InitializeComponent();
            genders = new List<Gender>
                (DBConnection.workerEntities.Gender.ToList());
            workers = new List<Worker>
             (DBConnection.workerEntities.Worker.ToList());
            this.DataContext = this;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var dcvd = SurnameTb.Text + " " + NameTb.Text + " " + PatronymicTb.Text + " " + DateDp.SelectedDate + " " + PassportTb.Text + " " + PhoneTb.Text;

            if (MessageBox.Show(dcvd, "Проверьте корректность введенных данных", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
               

                var t = GenderCB.SelectedItem as Gender;
                worker.Surname = SurnameTb.Text.Trim();
                worker.Name = NameTb.Text.Trim();
                worker.Patronymic = PatronymicTb.Text.Trim();
                worker.DateOfBirth = DateDp.SelectedDate;
                worker.Passport = PassportTb.Text.Trim();
                worker.Phone = PhoneTb.Text.Trim();
                worker.IDGender = t.ID;
                

                DBConnection.workerEntities.Worker.Add(worker);
                DBConnection.workerEntities.SaveChanges();

                NavigationService.Navigate(new AllWorkersPage());
            }

        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                worker.Photo = File.ReadAllBytes(openFileDialog.FileName);
                TestImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }

        }
    }
}
