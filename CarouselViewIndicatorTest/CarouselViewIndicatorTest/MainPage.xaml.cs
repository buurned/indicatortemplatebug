using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CarouselViewIndicatorTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


    }
    public class Item
    {
        public string Text { get; set; }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private static readonly Random _random = new();
        private bool _showCarouselIndicatorView = false;
        private bool _hasSelectedFiles = false;
        private ObservableCollection<Item> items;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Item> Items
        {
            get => items;
            set
            {
                if (items != value)
                {
                    items = value;
                    _hasSelectedFiles = items != null && items.Count > 0;
                    _showCarouselIndicatorView = items != null && items.Count > 1;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        public MainViewModel()
        {
            Items = new ObservableCollection<Item>
            {
                new Item { Text = "Item 1" },
                new Item { Text = "Item 2" },
                new Item { Text = "Item 3" }
            };
        }

      
        //Add new Item
        public ICommand AddRandomItemCommand => new Command(() =>
        {
            Items.Add(new Item { Text = "NewRandItem " + _random.Next(1, 100) });
        });

        //Remove current item
        public ICommand RemoveCurrentItemCommand => new Command<int>(index =>
        {
            if (index >= 0 && index < Items.Count)
            {
                Items.RemoveAt(index);
            }
        });
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Optional
        // This stuff is optional...
        public bool ShowCarouselIndicatorView
        {
            get => _showCarouselIndicatorView;
            set
            {
                _showCarouselIndicatorView = value;
                OnPropertyChanged(nameof(ShowCarouselIndicatorView));
            }
        }
        public bool HasSelectedFilesShowCarousel
        {
            get => _hasSelectedFiles;
            set
            {
                _hasSelectedFiles = value;
                OnPropertyChanged(nameof(HasSelectedFilesShowCarousel));
            }
        }
        #endregion
    }
}
