using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Advertising.WinRT.UI;
using NativeAdInGridView.Models;

namespace NativeAdInGridView.UserControls
{
    public sealed partial class MyAdContainer : UserControl
    {
        private NativeAdsManagerV2 _myNativeAdsManager;

        public MyAdContainer()
        {
            InitializeComponent();

            Loaded += MyAdContainer_Loaded;
            Unloaded += MyAdContainer_Unloaded;
        }

        private void MyAdContainer_Unloaded(object sender, RoutedEventArgs e)
        {
            _myNativeAdsManager.AdReady -= MyNativeAd_AdReady;
            _myNativeAdsManager.ErrorOccurred -= MyNativeAdsManager_ErrorOccurred;
        }

        private void MyAdContainer_Loaded(object sender, RoutedEventArgs e)
        {
            // test AdID
            //string myAppId = "d25517cb-12d4-4699-8bdc-52040c712cab";
            //string myAdUnitId = "test";

            // ad info passed with DataContext
            var myAppId = (DataContext as MyItem)?.AdUnitId;
            var myAdUnitId = (DataContext as MyItem)?.AdUnitId;

            if(string.IsNullOrEmpty(myAppId) || string.IsNullOrEmpty(myAdUnitId))
                return;

            _myNativeAdsManager = new NativeAdsManagerV2(myAppId, myAdUnitId);
            _myNativeAdsManager.AdReady += MyNativeAd_AdReady;
            _myNativeAdsManager.ErrorOccurred += MyNativeAdsManager_ErrorOccurred;
            
            _myNativeAdsManager.RequestAd();

            TitleTextBlock.Text = "Ad requested, waiting...";
        }

        void MyNativeAd_AdReady(object sender, NativeAdReadyEventArgs e)
        {
            NativeAdV2 nativeAd = e.NativeAd;

            // Show the ad icon.
            if (nativeAd.AdIcon != null)
            {
                AdIconImage.Source = nativeAd.AdIcon.Source;

                // Adjust the Image control to the height and width of the 
                // provided ad icon.
                AdIconImage.Height = nativeAd.AdIcon.Height;
                AdIconImage.Width = nativeAd.AdIcon.Width;
            }

            // Show the ad title.
            TitleTextBlock.Text = nativeAd.Title;

            // Show the ad description.
            if (!string.IsNullOrEmpty(nativeAd.Description))
            {
                DescriptionTextBlock.Text = nativeAd.Description;
                DescriptionTextBlock.Visibility = Visibility.Visible;
            }

            // Display the first main image for the ad. Note that the service
            // might provide multiple main images. 
            if (nativeAd.MainImages.Count > 0)
            {
                NativeImage mainImage = nativeAd.MainImages[0];
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri(mainImage.Url);
                MainImageImage.Source = bitmapImage;

                // Adjust the Image control to the height and width of the 
                // main image.
                MainImageImage.Height = mainImage.Height;
                MainImageImage.Width = mainImage.Width;
                MainImageImage.Visibility = Visibility.Visible;
            }

            // Add the call to action string to the button.
            if (!string.IsNullOrEmpty(nativeAd.CallToActionText))
            {
                CallToActionButton.Content = nativeAd.CallToActionText;
                CallToActionButton.Visibility = Visibility.Visible;
            }

            // Show the ad sponsored by value.
            if (!string.IsNullOrEmpty(nativeAd.SponsoredBy))
            {
                SponsoredByTextBlock.Text = nativeAd.SponsoredBy;
                SponsoredByTextBlock.Visibility = Visibility.Visible;
            }

            // Show the icon image for the ad.
            if (nativeAd.IconImage != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri(nativeAd.IconImage.Url);
                IconImageImage.Source = bitmapImage;

                // Adjust the Image control to the height and width of the 
                // icon image.
                IconImageImage.Height = nativeAd.IconImage.Height;
                IconImageImage.Width = nativeAd.IconImage.Width;
                IconImageImage.Visibility = Visibility.Visible;
            }

            // Register the container of the controls that display
            // the native ad elements for clicks/impressions.
            nativeAd.RegisterAdContainer(NativeAdContainer);
        }

        private void MyNativeAdsManager_ErrorOccurred(object sender, NativeAdErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"NativeAd error\n\t{e.ErrorMessage}\n\tErrorCode: {e.ErrorCode}");

            TitleTextBlock.Text = e.ErrorMessage;
        }
    }
}
