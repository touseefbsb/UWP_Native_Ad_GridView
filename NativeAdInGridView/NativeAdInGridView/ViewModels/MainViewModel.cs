using System.Collections.ObjectModel;
using NativeAdInGridView.Models;

namespace NativeAdInGridView.ViewModels
{
    public class MainViewModel 
    {
        public MainViewModel()
        {
            // TODO USe your app's ad info. Also note that you cant reuse the same AdUnitID in the same view
            MyItems = new ObservableCollection<MyItem>
            {
                new MyItem {ItemTitle = "I'm not an ad."},
                new MyItem {ItemTitle = "Ad1", IsAd = true, AdUnitId = "1100033196", AdAppId = "9nblggh6850j"},
                new MyItem {ItemTitle = "I'm not an ad, too!"},
                new MyItem {ItemTitle = "Ad2", IsAd = true, AdUnitId = "1100033197", AdAppId = "9nblggh6850j"},
            };
        }

        public ObservableCollection<MyItem> MyItems { get; set; }
    }
}
