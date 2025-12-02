using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DatabindingZad3
{
    public partial class MainPage : ContentPage
    {
        public class BmiCalculatorViewModel : INotifyPropertyChanged
        {
            private double height;
            private double weight;
            private double bmi { get; }
            private string category { get; }

            public double Height
            {
                get => height;
                set
                {
                    if (height != value)
                    {

                        height = value;
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(Bmi));

                        OnPropertyChanged(nameof(Category));

                    }
                }
            }
            public double Weight
            {
                get => weight;
                set
                {
                    if (weight != value)
                    {
                        weight = value;
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(Bmi));
                        OnPropertyChanged(nameof(Category));

                    }
                }
            }
            public double Bmi
            {
                get
                {
                    return (weight / (height * height));
                }

            }
            public string Category
            {
                get
                {
                    if (Bmi < 18.5)
                        return "Niedowaga";
                    else if (Bmi <= 24.9)
                        return "Norma";
                    else if (Bmi <= 29.9)
                        return "Nadwaga";
                    else
                        return "Otyłość";
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new BmiCalculatorViewModel();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
        }
    }
}