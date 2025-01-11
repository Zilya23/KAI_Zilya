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
    /// Логика взаимодействия для StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        public List<Students> StudentsList { get; set; }
        public List<Specialties> SpecialtiesList { get; set; }
        public int numberRecord { get; set; }
        public Students studentsSelect { get; set; }
        public StudentsPage()
        {
            InitializeComponent();
            StudentsList = bd_connection.connection.Students.ToList();
            SpecialtiesList = bd_connection.connection.Specialties.ToList();
            cb_Specialitets.ItemsSource = SpecialtiesList;
            cb_Specialitets.DisplayMemberPath = "Title";
            numberRecord = 1;
            DataContext = this;

            if (StudentsList.Count == 0)
            {
                Students studentsNULL = new Students();
                FullStudent(studentsNULL);
                btn_delete.Visibility = Visibility.Hidden;
                btn_edit.Visibility = Visibility.Hidden;
            }
            else
            {
                tb_count.Text = numberRecord + " из " + StudentsList.Count;
                studentsSelect = bd_connection.connection.Students.Where(x => x.ID_Student == numberRecord).FirstOrDefault();
                FullStudent(studentsSelect);
            }
        }

        public void FullStudent(Students students)
        {
            tb_FIO.Text = students.FIO;
            tb_Gender.Text = students.Gender;
            dp_Birth.SelectedDate = students.Date_Of_Birth;
            tb_Parents.Text = students.Parents;
            tb_Address.Text = students.Address;
            tb_Phone.Text = students.Phone;
            tb_Pasport.Text = students.Pasport_num;
            tb_CreditCard.Text = students.Credit_Card_Number.ToString();
            dp_Receipt.SelectedDate = students.Date_Of_Receipt;
            tb_Group.Text = students.Group;
            tb_Course.Text = students.Course.ToString();
            cb_Specialitets.SelectedItem = students.Specialties;
            cb_FullTime.IsChecked = students.Full_Time_Education;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            numberRecord++;
            if (numberRecord > StudentsList.Count)
            {
                numberRecord = 1;
                tb_count.Text = numberRecord + " из " + StudentsList.Count;
                studentsSelect = bd_connection.connection.Students.Where(x => x.ID_Student == numberRecord).FirstOrDefault();
                FullStudent(studentsSelect);
            }
            else
            {
                tb_count.Text = numberRecord + " из " + StudentsList.Count;
                studentsSelect = bd_connection.connection.Students.Where(x => x.ID_Student == numberRecord).FirstOrDefault();
                FullStudent(studentsSelect);
            }
        }

        private void btn_previous_Click(object sender, RoutedEventArgs e)
        {
            numberRecord--;
            if (numberRecord == 0)
            {
                numberRecord = StudentsList.Count;
                tb_count.Text = numberRecord + " из " + StudentsList.Count;
                studentsSelect = bd_connection.connection.Students.Where(x => x.ID_Student == numberRecord).FirstOrDefault();
                FullStudent(studentsSelect);
            }
            else
            {
                tb_count.Text = numberRecord + " из " + StudentsList.Count;
                studentsSelect = bd_connection.connection.Students.Where(x => x.ID_Student == numberRecord).FirstOrDefault();
                FullStudent(studentsSelect);
            }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStudentPage());
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите безвозвратно удалить запись и все связанные с ней данные?", "Удаление", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                Students students = bd_connection.connection.Students.Where(x => x.ID_Student == numberRecord).FirstOrDefault();
                bd_connection.connection.Students.Remove(students);
                bd_connection.connection.SaveChanges();
            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Сохрнаить изменения?", "Редактировать", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    studentsSelect.FIO = tb_FIO.Text.Trim();
                    studentsSelect.Gender = tb_Gender.Text.Trim();
                    studentsSelect.Address = tb_Address.Text.Trim();
                    studentsSelect.Parents = tb_Parents.Text.Trim();
                    studentsSelect.Phone = tb_Phone.Text.Trim();
                    studentsSelect.Pasport_num = tb_Pasport.Text.Trim();
                    studentsSelect.Credit_Card_Number = Convert.ToInt32(tb_CreditCard.Text.Trim());
                    studentsSelect.Group = tb_Group.Text.Trim();
                    studentsSelect.Course = Convert.ToInt32(tb_Course.Text.Trim());
                    studentsSelect.ID_Specialties = (cb_Specialitets.SelectedItem as Specialties).ID_Specialties;
                    studentsSelect.Full_Time_Education = cb_FullTime.IsChecked;
                    bd_connection.connection.SaveChanges();
                    MessageBox.Show("Успешно изменено");
                    NavigationService.Navigate(new StudentsPage());
                }
                catch
                {
                    MessageBox.Show("Заполните все обязательные поля");
                }

                //if (tb_Title.Text.Trim().Length != 0 && tb_Description.Text.Trim().Length != 0)
                //{
                //    Specialties specialties = bd_connection.connection.Specialties.Where(x => x.ID_Specialties == numberRecord).FirstOrDefault();
                //    specialties.Title = tb_Title.Text.Trim();
                //    specialties.Description = tb_Description.Text.Trim();
                //    bd_connection.connection.SaveChanges();
                //    MessageBox.Show("Успешно!");
                //    NavigationService.Navigate(new SpecialtiesPage());
                //}
                //else
                //{
                //    MessageBox.Show("Заполните все поля!");
                //}
            }
        }
    }
}
