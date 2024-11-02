using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Xml.Linq;
using WPF_CactusProject_2024.DB;

namespace WPF_CactusProject_2024.pages
{
    /// <summary>
    /// Логика взаимодействия для Add_VisvavkaPage.xaml
    /// </summary>
    public partial class Add_VisvavkaPage : Page
    {
        private Vistavka v;
        private Cactus_Vistavka cv;
        public Add_VisvavkaPage()
        {
            InitializeComponent();
            v = new Vistavka();
            cv = new Cactus_Vistavka();
            CmbxCactus.ItemsSource = ConnectionClass.db.Cactus.ToList();
        }


        private void TxtMesto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TxtNagrada_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TxtCactus_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            DateTime enteredDate;
            bool isDateValid = DateTime.TryParseExact(
                Date.Text,
                "dd.MM.yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out enteredDate
            );

            if (!isDateValid || enteredDate > DateTime.Today)
            {
                MessageBox.Show("Введите корректную дату (формат: дд.мм.гггг), которая не больше текущей даты.", "Ошибка даты", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Прекращаем выполнение метода сохранения
            }
            try
            {
                if (string.IsNullOrEmpty(TxtMesto.Text) || Date.SelectedDate == null || CmbxCactus.SelectedItem == null ||
                        string.IsNullOrEmpty(TxtNagrada.Text) || string.IsNullOrEmpty(TxtComment.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    //v.Mesto_provedeniya = TxtMesto.Text;
                    //cv.Date = Convert.ToDateTime(Date.Text);
                    //cv.Nagrada = TxtNagrada.Text;

                    //cv.Id_cactus = ((Cactus)CmbxCactus.SelectedItem).Id_cactus;
                    //v.Comment = TxtComment.Text;
                    //ConnectionClass.db.Vistavka.Add(v);
                    //ConnectionClass.db.Cactus_Vistavka.Add(cv);
                    //ConnectionClass.db.SaveChanges();
                    //MessageBox.Show($"Выставка добавлена", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Information);
                    //NavigationService.Navigate(new Cactus_VistavkaPage());
                    Vistavka vistavka = new Vistavka
                    {
                        Mesto_provedeniya = TxtMesto.Text,
                        Comment = TxtComment.Text,
                        Date = Convert.ToDateTime(Date.Text)
                    };

                Cactus_Vistavka cactusVistavka = new Cactus_Vistavka
                {
                    Id_vistavka = vistavka.Id_vistavka,
                    Id_cactus = (CmbxCactus.SelectedItem as Cactus).Id_cactus, 
                    Nagrada = TxtNagrada.Text
                };

                ConnectionClass.db.Vistavka.Add(vistavka);
                ConnectionClass.db.Cactus_Vistavka.Add(cactusVistavka);
                ConnectionClass.db.SaveChanges();
                MessageBox.Show($"Выставка добавлена", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new Cactus_VistavkaPage());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
