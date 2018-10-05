using System;
using System.Threading.Tasks;
using FluentVideoPlayer.Services;
using FluentVideoPlayer.ViewModels;
using Microsoft.Toolkit.Uwp.Helpers;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Newtonsoft.Json.Linq;
using Windows.Media.Playback;
using Windows.Services.Store;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
namespace FluentVideoPlayer.Views
{
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame);
        }
       

        private void ShellPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ApplicationViewExtensions.SetExtendViewIntoTitleBar(this, true);
            TitleBarExtensions.SetButtonBackgroundColor(this, Colors.Transparent);
        }

   
    }
}
