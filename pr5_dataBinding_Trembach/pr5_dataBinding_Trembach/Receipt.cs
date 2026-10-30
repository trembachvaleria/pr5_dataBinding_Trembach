using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pr5_dataBinding_Trembach
{
    public class Receipt : INotifyPropertyChanged
    {
        private int id;
        private DateTime date;
        private int cashNumber;
        private string cashier;
        private ObservableCollection<ReceiptItem> items;

        public Receipt()
        {
            Date = DateTime.Now;
            Items = new ObservableCollection<ReceiptItem>();
            Items.CollectionChanged += ItemsChanged;
        }

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }

        public int CashNumber
        {
            get { return cashNumber; }
            set { cashNumber = value; OnPropertyChanged(); }
        }

        public string Cashier
        {
            get { return cashier; }
            set { cashier = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ReceiptItem> Items
        {
            get { return items; }
            set
            {
                if (items != null)
                {
                    items.CollectionChanged -= ItemsChanged;
                    foreach (var item in items)
                    {
                        item.PropertyChanged -= ItemChanged;
                    }
                }
                items = value;
                if (items != null)
                {
                    OnPropertyChanged();
                    items.CollectionChanged += ItemsChanged;
                    foreach (var item in items)
                    {
                        item.PropertyChanged += ItemChanged;
                    }
                }
                OnPropertyChanged(nameof(Total));
            }
        }

        private void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    if (item != null)
                    {
                        item.PropertyChanged += ItemChanged;
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    if (item != null)
                        item.PropertyChanged -= ItemChanged;

                }
            }

            OnPropertyChanged(nameof(Total));
        }

        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ReceiptItem.ItemTotal))
            {
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Total
        {
            get { return items == null ? 0 : items.Sum(i => i.ItemTotal); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
