using FluentVideoPlayer.Models;
using FluentVideoPlayer.ViewModels;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentVideoPlayer.Views
{

    public sealed partial class LibraryPage : Page
    {
        public LibraryViewModel ViewModel { get; set; }

        public LibraryPage()
        {
            InitializeComponent();
            ViewModel = new LibraryViewModel();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            StorageFolder mainFolder;
            if (e.Parameter == null)
                mainFolder = KnownFolders.VideosLibrary;
            else
                mainFolder = e.Parameter as StorageFolder;

            await ViewModel.Fill(mainFolder);

        }

        private void AdaptiveView_ChoosingItemContainer(Windows.UI.Xaml.Controls.ListViewBase sender, ChoosingItemContainerEventArgs args)
        {
            if (args.ItemContainer == null)
            {
                GridViewItem container = (GridViewItem)args.ItemContainer ?? new GridViewItem();
                args.ItemContainer = container;
            }
            nativeadData = args.Item as NativeAdData;
            if (nativeadData is NativeAdData)
            {
                // I am trying to send the container to nativeaD OBJECT in nativeadData so I can register it for clicks.
                nativeadData.parent = args.ItemContainer;
                nativeadData.myNativeAdsManager?.RequestAd();
            }
        }
        private NativeAdData nativeadData;      
       
    }
}
