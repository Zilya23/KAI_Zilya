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

namespace KAI_Zilya
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void btn_Specialties_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SpecialtiesPage());
        }

        private void btn_Subjects_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SubjectsPage());
        }

        private void btn_Students_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentsPage());
        }
    }
}
