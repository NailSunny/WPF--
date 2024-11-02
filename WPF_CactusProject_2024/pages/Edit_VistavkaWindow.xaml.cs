using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using WPF_CactusProject_2024.DB;

namespace WPF_CactusProject_2024.pages
{
    /// <summary>
    /// Логика взаимодействия для Edit_VistavkaWindow.xaml
    /// </summary>
    public partial class Edit_VistavkaWindow : Window
    {
        //DB.Vistavka vistavka;
        //DB.Cactus_Vistavka cv;
        //public Edit_VistavkaWindow(DB.Vistavka _vistavka, DB.Cactus_Vistavka _cv)
        //{
        //    InitializeComponent();
        //    this.DataContext = vistavka;
        //    vistavka = _vistavka;
        //    this.DataContext = cv;
        //    cv = _cv;

        //    CmbxCactus.ItemsSource = ConnectionClass.db.Cactus.ToList();
        //}

        //private void BtnSave_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        var a = ConnectionClass.db.Vistavka.Where(z => z.Id_vistavka == vistavka.Id_vistavka).FirstOrDefault();
        //        var b = ConnectionClass.db.Cactus_Vistavka.Where(x => x.Id_CV == cv.Id_CV).FirstOrDefault();
        //        if (string.IsNullOrEmpty(a.Mesto_provedeniya) || string.IsNullOrEmpty(a.Comment) || string.IsNullOrEmpty(b.Nagrada) || a.Date == null || CmbxCactus.SelectedItem == null)
        //        {
        //            MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return;
        //        }

        //        a.Mesto_provedeniya = TxtMesto.Text;
        //        b.Nagrada = TxtNagrada.Text;
        //        a.Date = Convert.ToDateTime(Date.Text); ;

        //        b.Id_cactus = ((DB.Cactus_Vistavka)CmbxCactus.SelectedItem).Id_cactus;
        //        a.Comment = TxtComment.Text;

        //        ConnectionClass.db.SaveChanges();
        //        MessageBox.Show("Изменения сохранены", "Изменения данных", MessageBoxButton.OK, MessageBoxImage.Information);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
        private Cactus_Vistavka _cactusVistavka; // Экземпляр редактируемого объекта

        public Edit_VistavkaWindow(Cactus_Vistavka cactusVistavka)
        {
            InitializeComponent();
            _cactusVistavka = cactusVistavka;
            DataContext = _cactusVistavka; // Устанавливаем контекст данных для привязки

            // Загружаем данные для ComboBox с кактусами
            LoadCactuses();
        }

        private void LoadCactuses()
        {
           
            {
                CmbxCactus.ItemsSource = ConnectionClass.db.Cactus.ToList(); // Загрузка кактусов в ComboBox
            }
        }

        // Сохранение изменений
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            //{
            //    var vistavka = ConnectionClass.db.Vistavka.Find(_cactusVistavka.Id_vistavka);

            //    if (vistavka != null)
            //    {
            //        vistavka.Mesto_provedeniya = TxtMesto.Text;
            //        vistavka.Date = DatePicker.SelectedDate ?? vistavka.Date; // Проверка на null
            //        vistavka.Comment = TxtComment.Text;

            //        _cactusVistavka.Nagrada = TxtNagrada.Text;
            //        _cactusVistavka.Cactus = (Cactus)CmbxCactus.SelectedItem;

            //        ConnectionClass.db.SaveChanges(); // Сохранение изменений в базе данных
            //        MessageBox.Show("Изменения сохранены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //}

            //DialogResult = true; // Закрываем окно с подтверждением
            DateTime enteredDate;
            bool isDateValid = DateTime.TryParseExact(
                DatePicker.Text,
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
            var vistavka = ConnectionClass.db.Vistavka.Find(_cactusVistavka.Id_vistavka);

            if (vistavka != null)
            {
                vistavka.Mesto_provedeniya = TxtMesto.Text;
                vistavka.Date = DatePicker.SelectedDate ?? vistavka.Date; // Проверка на null
                vistavka.Comment = TxtComment.Text;

                // Обновляем награду
                _cactusVistavka.Nagrada = TxtNagrada.Text;

                // Получаем выбранный кактус из ComboBox
                var selectedCactus = (Cactus)CmbxCactus.SelectedItem;

                // Проверка, существует ли уже связь между выбранным кактусом и текущей выставкой
                bool isCactusAlreadyInExhibition = ConnectionClass.db.Cactus_Vistavka
                    .Any(cv => cv.Id_cactus == selectedCactus.Id_cactus && cv.Id_vistavka == vistavka.Id_vistavka && cv.Id_CV != _cactusVistavka.Id_CV);

                if (isCactusAlreadyInExhibition)
                {
                    MessageBox.Show("Этот кактус уже участвует в этой выставке.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Прерываем сохранение изменений
                }

                // Устанавливаем выбранный кактус для связи
                _cactusVistavka.Cactus = selectedCactus;

                // Сохраняем изменения в базе данных
                ConnectionClass.db.SaveChanges();
                MessageBox.Show("Изменения сохранены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            DialogResult = true; // Закрываем окно с подтверждением
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void TxtNagrada_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TxtMesto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void CmbxCactus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
