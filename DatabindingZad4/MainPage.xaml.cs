using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace DatabindingZad4
{
    public class ShoppingItem : INotifyPropertyChanged
    {
        private string _name;
        private double _price;
        private int _quantity;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Nazwa nie moze byc pusta");
                _name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public ShoppingItem(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public double TotalPrice => Price * Quantity;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ShoppingListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; }

        public double TotalSum => ShoppingItems.Sum(item => item.TotalPrice);

        public ShoppingListViewModel()
        {
            ShoppingItems = new ObservableCollection<ShoppingItem>();
            ShoppingItems.CollectionChanged += (s, e) =>
            {
                if (e.OldItems != null)
                {
                    foreach (ShoppingItem item in e.OldItems)
                        item.PropertyChanged -= OnItemPropertyChanged;
                }
                if (e.NewItems != null)
                {
                    foreach (ShoppingItem item in e.NewItems)
                        item.PropertyChanged += OnItemPropertyChanged;
                }
                OnPropertyChanged(nameof(TotalSum));
            };
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ShoppingItem.TotalPrice))
            {
                OnPropertyChanged(nameof(TotalSum));
            }
        }

        public void AddItem(string name, double price, int quantity)
        {
            var newItem = new ShoppingItem(name, price, quantity);
            ShoppingItems.Add(newItem);
        }

        public void RemoveItem(ShoppingItem item)
        {
            ShoppingItems.Remove(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class MainPage : ContentPage
    {
        private readonly ShoppingListViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new ShoppingListViewModel();
            BindingContext = _viewModel;
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            if (!double.TryParse(PriceEntry.Text, out double price))
            {
                DisplayAlert("Błąd", "Nieprawidłowa cena.", "OK");
                return;
            }
            int quantity = (int)QuantityStepper.Value;

            _viewModel.AddItem(name, price, quantity);
            name = "";
            price = 0;
            quantity = 1;
        }

        private void OnRemoveClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is ShoppingItem item)
            {
                _viewModel.RemoveItem(item);
            }
        }
    }
}