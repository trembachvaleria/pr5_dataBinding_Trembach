using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для ReceiptFormPage.xaml
    /// </summary>
    public partial class ReceiptFormPage : Page
    {
        private ObservableCollection<Receipt> list;
        private Receipt current;
        private Receipt original;

        public ReceiptFormPage(ObservableCollection<Receipt> _list, Receipt receipt = null)
        {
            InitializeComponent();
            list = _list;

            if (receipt != null)
            {
                original = receipt;
                current = new Receipt
                {
                    Id = receipt.Id,
                    Date = receipt.Date,
                    CashNumber = receipt.CashNumber,
                    Cashier = receipt.Cashier,
                    Items = new ObservableCollection<ReceiptItem>()
                };
                this.Title = "Редактирование";

                foreach (var item in receipt.Items)
                {
                    current.Items.Add(item);
                }
            }
            else
            {
                current = new Receipt();
                current.Id = list.Count + 1;
                this.Title = "Новый чек";
            }

            DataContext = current;
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            current.Items.Add(new ReceiptItem("Новый товар", 100, 1));
        }

        private void btnDelItem_Click(object sender, RoutedEventArgs e)
        {
            if (lvItems.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар");
                return;
            }

            if (MessageBox.Show("Удалить товар?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                current.Items.Remove((ReceiptItem)lvItems.SelectedItem);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(current.Cashier))
            {
                MessageBox.Show("Введите кассира");
                return;
            }

            if (original != null)
            {
                original.Date = current.Date;
                original.CashNumber = current.CashNumber;
                original.Cashier = current.Cashier;
                original.Items.Clear();
                foreach (var item in current.Items)
                {
                    original.Items.Add(item);
                }
            }
            else
            {
                list.Add(current);
            }

            JsonSave.Save(list);
            NavigationService.GoBack();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
