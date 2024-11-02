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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_CactusProject_2024.DB;

namespace WPF_CactusProject_2024.pages
{
    /// <summary>
    /// Логика взаимодействия для AddCactusToVistavkaPage.xaml
    /// </summary>
    public partial class AddCactusToVistavkaPage : Page
    {
        private Vistavka _selectedVistavka;

        public AddCactusToVistavkaPage()
        {
            InitializeComponent();
            LoadVistavkas();  // Загружаем список выставок
            LoadCactuses();   // Загружаем список кактусов
        }

        // Метод загрузки списка выставок
        private void LoadVistavkas()
        {
            
            {
                CmbxVistavka.ItemsSource = ConnectionClass.db.Vistavka.ToList();
            }
        }

        // Метод загрузки списка кактусов
        private void LoadCactuses()
        {
           
            {
                LvCactuses.ItemsSource = ConnectionClass.db.Cactus.ToList();
            }
        }

        // Выбор выставки из ComboBox
        private void CmbxVistavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedVistavka = (Vistavka)CmbxVistavka.SelectedItem;
        }

        // Кнопка для добавления выбранных кактусов в выставку
        private void BtnAddCactuses_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedVistavka == null)
            {
                MessageBox.Show("Выберите выставку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedCactuses = LvCactuses.SelectedItems.Cast<Cactus>().ToList();

            if (selectedCactuses.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы один кактус.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            {
                var vistavka = ConnectionClass.db.Vistavka.Find(_selectedVistavka.Id_vistavka);

                if (vistavka != null)
                {
                    foreach (var cactus in selectedCactuses)
                    {
                        // Создаем новую связь кактуса с выставкой
                        bool isAlreadyAdded = ConnectionClass.db.Cactus_Vistavka
                            .Any(cv => cv.Id_cactus == cactus.Id_cactus && cv.Id_vistavka == vistavka.Id_vistavka);

                        if (isAlreadyAdded)
                        {
                            MessageBox.Show($"Кактус '{cactus.Name_cactus}' уже добавлен на эту выставку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            continue;
                        }

                        // Создаем новую связь кактуса с выставкой
                        var cactusVistavka = new Cactus_Vistavka
                        {
                            Id_cactus = cactus.Id_cactus,
                            Id_vistavka = vistavka.Id_vistavka,
                            Nagrada = "Без награды" // Можно изменить или настроить для выбора награды
                        };

                        ConnectionClass.db.Cactus_Vistavka.Add(cactusVistavka);
                    }

                    ConnectionClass.db.SaveChanges();
                    MessageBox.Show("Кактусы успешно добавлены в выставку!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            // Обновление списка кактусов, чтобы отразить изменения (например, убрать добавленные)
            LoadCactuses();
        }

        // Возврат на предыдущую страницу
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
