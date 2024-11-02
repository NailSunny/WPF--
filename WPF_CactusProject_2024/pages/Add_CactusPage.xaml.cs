using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WPF_CactusProject_2024.pages
{
    /// <summary>
    /// Логика взаимодействия для Add_CactusPage.xaml
    /// </summary>
    public partial class Add_CactusPage : Page
    {
        private Cactus c;
        public Add_CactusPage()
        {
            
            InitializeComponent();
            c = new Cactus();
            CmbxVid.ItemsSource = ConnectionClass.db.Vid.ToList();
        }

        private void TxtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text[0]))
            {
                e.Handled = true;
            }
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

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtName.Text) || string.IsNullOrEmpty(TxtProishogdenie.Text) || CmbxVid.SelectedItem == null || 
                        Convert.ToInt32(TxtPrice.Text) == null || Convert.ToInt32(TxtVozrast.Text) == null || string.IsNullOrEmpty(TxtInstruction.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    c.Name_cactus = TxtName.Text;
                    c.Proishogdenie = TxtProishogdenie.Text;
                    c.Vozrast = Convert.ToInt32(TxtVozrast.Text);
                    c.Price = Convert.ToInt32(TxtPrice.Text);
                    c.Vid = ((Vid)CmbxVid.SelectedItem);
                    c.Instruction = TxtInstruction.Text;  
                    ConnectionClass.db.Cactus.Add(c);
                    ConnectionClass.db.SaveChanges();
                    MessageBox.Show($"{TxtName.Text} добавлен", "Добавление записи", MessageBoxButton.OK, MessageBoxImage.Information);
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
