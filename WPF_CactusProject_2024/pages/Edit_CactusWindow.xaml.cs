using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
using WPF_CactusProject_2024.DB;

namespace WPF_CactusProject_2024.pages
{
    /// <summary>
    /// Логика взаимодействия для Edit_CactusWindow.xaml
    /// </summary>
    public partial class Edit_CactusWindow : Window
    {
        DB.Cactus cactus;
        public Edit_CactusWindow(DB.Cactus _cactus)
        {

            InitializeComponent();
            cactus = _cactus;
            this.DataContext = cactus;
            CmbxVid.ItemsSource = ConnectionClass.db.Vid.ToList();
        }

        private void TxtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text[0]))
            {
                e.Handled = true;
            }
        }

        private void CmbxVid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtProishogdenie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TxtVozrast_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextInteger(e.Text);
        }

        private void TxtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextDecimal(e.Text, ((TextBox)sender).Text);
        }
        // Проверка на целое число (разрешает только цифры)
        private bool IsTextInteger(string text)
        {
            return int.TryParse(text, out _);
        }

        // Проверка на дробное или целое число
        private bool IsTextDecimal(string text, string currentText)
        {
            // Проверяем, что введенный символ является числом или десятичной точкой, но только одна точка
            return decimal.TryParse(currentText + text, out _);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var a = ConnectionClass.db.Cactus.Where(z => z.Id_cactus == cactus.Id_cactus).FirstOrDefault();
                if (string.IsNullOrEmpty(a.Name_cactus) || string.IsNullOrEmpty(a.Proishogdenie) || string.IsNullOrEmpty(a.Instruction) || a.Vozrast == null || a.Price == null || CmbxVid.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                a.Name_cactus = TxtName.Text;
                a.Proishogdenie = TxtProishogdenie.Text;
                a.Vozrast = Convert.ToInt32(TxtVozrast.Text);
                a.Price = Convert.ToInt32(TxtPrice.Text);
                a.Id_vid = ((DB.Vid)CmbxVid.SelectedItem).Id_vid;
                a.Instruction = TxtInstruction.Text;

                ConnectionClass.db.SaveChanges();
                MessageBox.Show("Изменения сохранены", "Изменения данных", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
