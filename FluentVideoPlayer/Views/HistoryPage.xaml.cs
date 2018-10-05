using FluentVideoPlayer.ViewModels;
using Windows.UI.Xaml.Controls;

namespace FluentVideoPlayer.Views
{
    public sealed partial class HistoryPage : Page
    {
        public HistoryViewModel ViewModel { get; } = new HistoryViewModel();

        public HistoryPage()
        {
            InitializeComponent();
        }

       
    }
}
