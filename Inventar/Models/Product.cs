using System.ComponentModel;

namespace Inventar.Models
{
    public class Product : INotifyPropertyChanged
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        private string? title = string.Empty;

        public string? Title
        {
            get => title;
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private string? description = string.Empty;

        public string? Description
        {
            get => description;
            set
            {
                description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        private int quantity;

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                NotifyPropertyChanged(nameof(Quantity));
            }
        }

        private decimal price;

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                NotifyPropertyChanged(nameof(Price));
            }
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string? arg)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(arg));    
        }

        #endregion 
    }
}
