using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace zadanie14._3
{
    public partial class MainPage : ContentPage
    {
        public class BmiCalculatorViewModel : INotifyPropertyChanged
        {
            private double height = 1.75;
            private double weight = 70;
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
                    }
                }
            }
            public double Weight
            {
                get => weight;
                set
                {
                    if((weight != value) && (weight >0))
                    {
                        weight = value;
                        OnPropertyChanged();
                    }
                }
            }
            public double Bmi
            {
                get => bmi;
                set
                {
                    if (bmi != value)
                    {
                        Bmi = Weight / (Height * Height);
                    }
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
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
        }
    }
}
