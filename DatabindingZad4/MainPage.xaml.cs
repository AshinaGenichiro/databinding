using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DatabindingZad4 { 
     public class ShoppingItem : INotifyPropertyChanged

{

    private string _name;

    private double _price;

    private int _quantity;

    private double _totalPrice;

    public string Name
    {
        get { return _name; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _name = value;
            else
                throw new Exception("Nazwa nie moze byc pusta");
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

    public double TotalPrice
    {
        get
        {
            return Price * Quantity;
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
    public class ShoppingListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ShoppingItem> Items;
        private double _totalSum;
        public double TotalSum
        {
            get
            {
                return Items.Sum(item => item.TotalPrice);
            }
        }
        public void AddItem(string name, double price, int quantity)
        {
            Items.Add(new ShoppingItem(name, price, quantity));
        }
        public void RemoveItem(ShoppingItem item)
        {
            Items.Remove(item);
        }
        public void CollectionView()
        {
            foreach (ShoppingItem item in Items)
            {
                Console.WriteLine(item.Name, item.Price, item.Quantity, item.TotalPrice);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public partial class MainPage : ContentPage

    {

    

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ShoppingItem("item",2,2);

        }
    }
}

