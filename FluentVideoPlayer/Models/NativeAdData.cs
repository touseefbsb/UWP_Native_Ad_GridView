using FluentVideoPlayer.Helpers;
using Microsoft.Advertising.WinRT.UI;
using System;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using static FluentVideoPlayer.Helpers.Constants;
namespace FluentVideoPlayer.Models
{
    /// <summary>
    /// Wrapper class I use to bind to gridview and show NativeAdContent and make the gridviewItem Clickable as well
    /// </summary>
    public class NativeAdData : VideoFolder
    {
        public NativeAdsManagerV2 myNativeAdsManager;
        private NativeAdV2 nativeAd;
        public SelectorItem parent;
        public NativeAdData()
        {
            myNativeAdsManager = new NativeAdsManagerV2(myAppId, myAdUnitId);
            //this might cause memory leak because the event is not being unSubscribed.
            //Though not sure how to work around this.
            myNativeAdsManager.AdReady += (s, e) =>
            {
                nativeAd = e.NativeAd;

                // Show the ad icon.
                if (nativeAd.AdIcon != null)
                {
                    AdIcon = nativeAd.AdIcon.Source;
                }
                //Show the ad title.
                Title = nativeAd.Title;

                // Show the ad description.
                //if (!string.IsNullOrEmpty(nativeAd.Description))
                //{
                //    Description = nativeAd.Description;
                //}

                // Display the first main image for the ad. Note that the service
                // might provide multiple main images. 
                if (nativeAd.MainImages.Count > 0)
                {
                    NativeImage mainImage = nativeAd.MainImages[0];
                    BitmapImage bitmapImage = new BitmapImage
                    {
                        UriSource = new Uri(mainImage.Url)
                    };
                    MainImageUrl = bitmapImage;
                }

                // Add the call to action string to the button.
                //if (!string.IsNullOrEmpty(nativeAd.CallToActionText))
                //{
                //    CallToAction = nativeAd.CallToActionText;
                //}
                // Show the ad sponsored by value.
                //if (!string.IsNullOrEmpty(nativeAd.SponsoredBy))
                //{
                //    SponsoredBy = nativeAd.SponsoredBy;
                //}
                // Show the icon image for the ad.
                //if (nativeAd.IconImage != null)
                //{
                //    BitmapImage bitmapImage = new BitmapImage
                //    {
                //        UriSource = new Uri(nativeAd.IconImage.Url)
                //    };
                //}
                nativeAd?.RegisterAdContainer(parent);
            };
            myNativeAdsManager.ErrorOccurred += (s, e) => Title = "ErrorAds".GetLocalized();
        }

        private ImageSource _adIcon;
        public ImageSource AdIcon { get => _adIcon; set => Set(ref _adIcon, value, nameof(AdIcon)); }

        private string _title;
        public new string Title { get => _title; set => Set(ref _title, value, nameof(Title)); }

        private string _description;
        public string Description { get => _description; set => Set(ref _description, value, nameof(Description)); }

        private ImageSource _mainImageUrl;
        public ImageSource MainImageUrl { get => _mainImageUrl; set => Set(ref _mainImageUrl, value, nameof(MainImageUrl)); }

        private string _callToAction;
        public string CallToAction { get => _callToAction; set => Set(ref _callToAction, value, nameof(CallToAction)); }

        private string _sponsoredBy;
        public string SponsoredBy { get => _sponsoredBy; set => Set(ref _sponsoredBy, value, nameof(SponsoredBy)); }

        private bool _isRegistered;
        public bool IsRegistered { get => _isRegistered; set => Set(ref _isRegistered, value, nameof(IsRegistered)); }

    }
}
