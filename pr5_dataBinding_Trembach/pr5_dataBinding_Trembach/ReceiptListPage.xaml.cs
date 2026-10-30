using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace pr5_dataBinding_Trembach
{
    /// <summary>
    /// Логика взаимодействия для ReceiptListPage.xaml
    /// </summary>
    public partial class ReceiptListPage : Page
    {
        public ObservableCollection<Receipt> List { get; set; }
        public Receipt Selected { get; set; }

        public ReceiptListPage()
        {
            InitializeComponent();
            List = JsonSave.Load();
            DataContext = this;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReceiptFormPage(List));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Selected = lvList.SelectedItem as Receipt;
            if (Selected == null)
            {
                MessageBox.Show("Выберите чек");
                return;
            }

            if (MessageBox.Show("Удалить чек?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                List.Remove(Selected);
                JsonSave.Save(List);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Selected = lvList.SelectedItem as Receipt;
            if (Selected == null)
            {
                MessageBox.Show("Выберите чек");
                return;
            }
            NavigationService.Navigate(new ReceiptFormPage(List, Selected));
        }
    }
}
