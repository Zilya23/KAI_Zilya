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
using KAI_Zilya.DB;

namespace KAI_Zilya
{
    /// <summary>
    /// Логика взаимодействия для AddSubjectsPage.xaml
    /// </summary>
    public partial class AddSubjectsPage : Page
    {
        public AddSubjectsPage()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Title.Text.Trim().Length != 0 && tb_Description.Text.Trim().Length != 0)
            {
                Subjects subjects = new Subjects();
                subjects.Title = tb_Title.Text.Trim();
                subjects.Description = tb_Description.Text.Trim();
                bd_connection.connection.Subjects.Add(subjects);
                bd_connection.connection.SaveChanges();
                MessageBox.Show("Успешно!");
                NavigationService.Navigate(new SubjectsPage());
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SubjectsPage());
        }
    }
}
