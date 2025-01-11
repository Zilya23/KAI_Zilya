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
    /// Логика взаимодействия для SubjectsPage.xaml
    /// </summary>
    public partial class SubjectsPage : Page
    {
        public List<Subjects> SubjectsList { get; set; }
        public int numberRecord { get; set; }
        public SubjectsPage()
        {
            InitializeComponent();
            SubjectsList = bd_connection.connection.Subjects.ToList();
            numberRecord = 1;
            if (SubjectsList.Count == 0)
            {
                tb_count.Text = "Нет записей в базе данных";
                tb_Title.Text = "Нет записей в базе данных";
                tb_Description.Text = "Нет записей в базе данных";
                btn_delete.Visibility = Visibility.Hidden;
                btn_edit.Visibility = Visibility.Hidden;
            }
            else
            {
                tb_count.Text = numberRecord + " из " + SubjectsList.Count;
                tb_Title.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Description;
            }
        }


        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }
        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            numberRecord++;
            if (numberRecord > SubjectsList.Count)
            {
                numberRecord = 1;
                tb_count.Text = numberRecord + " из " + SubjectsList.Count;
                tb_Title.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Description;
            }
            else
            {
                tb_count.Text = numberRecord + " из " + SubjectsList.Count;
                tb_Title.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Description;
            }
        }

        private void btn_previous_Click(object sender, RoutedEventArgs e)
        {
            numberRecord--;
            if (numberRecord == 0)
            {
                numberRecord = SubjectsList.Count;
                tb_count.Text = numberRecord + " из " + SubjectsList.Count;
                tb_Title.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Description;
            }
            else
            {
                tb_count.Text = numberRecord + " из " + SubjectsList.Count;
                tb_Title.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Title;
                tb_Description.Text = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault().Description;
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSubjectsPage());
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите безвозвратно удалить запись и все связанные с ней данные?", "Удаление", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {

            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Сохрнаить изменения?", "Редактировать", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                if (tb_Title.Text.Trim().Length != 0 && tb_Description.Text.Trim().Length != 0)
                {
                    Subjects subjects = bd_connection.connection.Subjects.Where(x => x.ID_Subjects == numberRecord).FirstOrDefault();
                    subjects.Title = tb_Title.Text.Trim();
                    subjects.Description = tb_Description.Text.Trim();
                    bd_connection.connection.SaveChanges();
                    MessageBox.Show("Успешно!");
                    NavigationService.Navigate(new SubjectsPage());
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
        }
    }
}
