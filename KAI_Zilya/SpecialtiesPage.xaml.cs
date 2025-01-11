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
    /// Логика взаимодействия для SpecialtiesPage.xaml
    /// </summary>
    public partial class SpecialtiesPage : Page
    {
        public List<Specialties> Specialties { get; set; }
        public int numberRecord { get; set; }
        public SpecialtiesPage()
        {
            InitializeComponent();
            Specialties = bd_connection.connection.Specialties.ToList();
            numberRecord = 1;
            if (Specialties.Count == 0)
            {
                tb_count.Text = "Нет записей в базе данных";
                tb_Title.Text = "Нет записей в базе данных";
                tb_Description.Text = "Нет записей в базе данных";
                btn_delete.Visibility = Visibility.Hidden;
                btn_edit.Visibility = Visibility.Hidden;
            }
            else
            {
                tb_count.Text = numberRecord+ " из " + Specialties.Count;
                tb_Title.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Description;
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            numberRecord++;
            if(numberRecord > Specialties.Count)
            {
                numberRecord = 1;
                tb_count.Text = numberRecord + " из " + Specialties.Count;
                tb_Title.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Description;
            }
            else
            {
                tb_count.Text = numberRecord + " из " + Specialties.Count;
                tb_Title.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Description;
            }
        }

        private void btn_previous_Click(object sender, RoutedEventArgs e)
        {
            numberRecord--;
            if (numberRecord == 0)
            {
                numberRecord = Specialties.Count;
                tb_count.Text = numberRecord + " из " + Specialties.Count;
                tb_Title.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Description;
            }
            else
            {
                tb_count.Text = numberRecord + " из " + Specialties.Count;
                tb_Title.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault().Description;
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSpecialitesPage());
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите безвозвратно удалить запись и все связанные с ней данные?", "Удаление", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Specialties specialties = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault();
                bd_connection.connection.Specialties.Remove(specialties);
                bd_connection.connection.SaveChanges();
            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Сохрнаить изменения?", "Редактировать", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                if (tb_Title.Text.Trim().Length != 0 && tb_Description.Text.Trim().Length != 0)
                {
                    Specialties specialties = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault();
                    specialties.Title = tb_Title.Text.Trim();
                    specialties.Description = tb_Description.Text.Trim();
                    bd_connection.connection.SaveChanges();
                    MessageBox.Show("Успешно!");
                    NavigationService.Navigate(new SpecialtiesPage());
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
        }
    }
}
