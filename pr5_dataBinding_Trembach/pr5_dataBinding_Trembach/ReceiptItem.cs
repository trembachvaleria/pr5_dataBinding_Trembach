using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pr5_dataBinding_Trembach
{
    public class ReceiptItem : INotifyPropertyChanged
    {
        private string name;
        private double price;
        private int count;

        public ReceiptItem() { }

        public ReceiptItem(string _name, double _price, int _count) {
            Name = _name;
            Price = _price;
            Count = _count;
        }
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ItemTotal));
            }
        }

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ItemTotal));
            }
        }

        public double ItemTotal
        {
            get { return price * count; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
