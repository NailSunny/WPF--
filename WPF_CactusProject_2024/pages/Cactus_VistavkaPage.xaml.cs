using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using WPF_CactusProject_2024.pages;

namespace WPF_CactusProject_2024.pages
{
    /// <summary>
    /// Логика взаимодействия для CactusPage.xaml
    /// </summary>
    public partial class Cactus_VistavkaPage : Page
    {
        private readonly Cactus_ProjectEntities1 _dbContext;

        public Cactus_VistavkaPage()
        {

            InitializeComponent();
            LvCactus.ItemsSource = ConnectionClass.db.Cactus.ToList();
            Refresh();
            LvVistavka.ItemsSource = ConnectionClass.db.Vistavka.ToList();
            LvVistavka.ItemsSource = ConnectionClass.db.Cactus_Vistavka.ToList();

            Refresh1();

        }


        public void Refresh()
        {
            LvCactus.ItemsSource = null;
            LvCactus.ItemsSource = ConnectionClass.db.Cactus.ToList();
        }

        public void Refresh1()
        {
            //LvVistavka.ItemsSource = null;
            //LvVistavka.ItemsSource = ConnectionClass.db.Vistavka.ToList();
            //LvVistavka.ItemsSource = ConnectionClass.db.Cactus_Vistavka.ToList();
            try
            {
                
                {
                    // Загружаем данные из базы данных с учетом связанных данных
                    var vistavkaList = ConnectionClass.db.Cactus_Vistavka
                        .Include(cv => cv.Cactus)
                        .Include(cv => cv.Vistavka)
                        .Include(cv => cv.Cactus.Vid)
                        .ToList();

                    // Привязываем данные к ListView
                    LvVistavka.ItemsSource = vistavkaList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnEdit1_Click(object sender, RoutedEventArgs e)
        {
            var a = (sender as Button).DataContext as DB.Cactus;
            Edit_CactusWindow edit_CactusWindow = new Edit_CactusWindow(a);
            edit_CactusWindow.Show();
        }

        private void BtnDelete1_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                var d = (sender as Button).DataContext as DB.Cactus;
                if (d != null)
                {
                    ConnectionClass.db.Cactus.Remove(d);
                    ConnectionClass.db.SaveChanges();
                    Refresh();
                    MessageBox.Show($"{d.Name_cactus} удален", "Удален", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //var a = (sender as Button).DataContext as DB.Vistavka;
            //var b = (sender as Button).DataContext as DB.Cactus_Vistavka;
            //Edit_VistavkaWindow edit_VistavkaWindow = new Edit_VistavkaWindow(a, b);
            //edit_VistavkaWindow.Show();
            // Получаем выбранный элемент для редактирования
            var selectedCactusVistavka = (Cactus_Vistavka)LvVistavka.SelectedItem;

            if (selectedCactusVistavka == null)
            {
                MessageBox.Show("Выберите элемент для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Открываем окно редактирования
            var editWindow = new Edit_VistavkaWindow(selectedCactusVistavka);

            // Ожидаем результат сохранения
            if (editWindow.ShowDialog() == true)
            {
                Refresh1(); // Обновляем список после успешного сохранения
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            //{
            //    return;
            //}
            //else
            //{
            //    //    var d = (sender as Button).DataContext as DB.Vistavka;
            //    //    var cv = (sender as Button).DataContext as DB.Cactus_Vistavka;

            //    //    if (d != null && cv != null)
            //    //    {
            //    //        ConnectionClass.db.Vistavka.Remove(d);
            //    //        ConnectionClass.db.Cactus_Vistavka.Remove(cv);
            //    //        ConnectionClass.db.SaveChanges();
            //    //        Refresh1();
            //    //        MessageBox.Show($"Выставки больше не существует.", "Удален", MessageBoxButton.OK, MessageBoxImage.Information);
            //    //    }
            //    var a = (sender as Button).DataContext as DB.Vistavka;
            //    var b = (sender as Button).DataContext as DB.Cactus_Vistavka;

            //    if (a != null)
            //        {
            //            ConnectionClass.db.Vistavka.Remove(a);
            //        }
            //    else if (b != null)
            //    {
            //        ConnectionClass.db.Cactus_Vistavka.Remove(b);
            //    }
            //    ConnectionClass.db.SaveChanges();
            //        Refresh1();
            //        MessageBox.Show($"Выставки больше не существует.", "Удален", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            // Получаем выбранную запись из ListView
            var selectedCactusVistavka = (Cactus_Vistavka)LvVistavka.SelectedItem;

            if (selectedCactusVistavka == null)
            {
                MessageBox.Show("Выберите запись для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Подтверждение удаления
            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                
                {
                    // Находим выбранную запись по Id
                    var vistavka = ConnectionClass.db.Vistavka.Find(selectedCactusVistavka.Id_vistavka);
                    var cactusVistavka = ConnectionClass.db.Cactus_Vistavka.Find(selectedCactusVistavka.Id_CV);

                    // Удаляем запись и связанные данные
                    if (vistavka != null)
                    {
                        ConnectionClass.db.Vistavka.Remove(vistavka); // Каскадное удаление связано с таблицей Cactus_Vistavka
                    }

                    if (cactusVistavka != null)
                    {
                        ConnectionClass.db.Cactus_Vistavka.Remove(cactusVistavka);
                    }

                    // Сохраняем изменения
                    ConnectionClass.db.SaveChanges();

                    // Обновляем список
                    Refresh1();

                    MessageBox.Show("Запись успешно удалена.", "Удалено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_VisvavkaPage());
        }

        private void BtnAdd1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Add_CactusPage());
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            Refresh1();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCactusToVistavkaPage());
        }
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LvCactus.ItemsSource = ConnectionClass.db.Cactus.Where(z => z.Name_cactus.ToLower().Contains(TxtSearch.Text)).ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LvCactus.ItemsSource = ConnectionClass.db.Cactus.OrderBy(z => z.Name_cactus).ToList();
        }
    }
}
