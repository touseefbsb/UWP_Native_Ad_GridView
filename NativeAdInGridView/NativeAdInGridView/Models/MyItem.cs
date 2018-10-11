using CommonHelpers.Common;

namespace NativeAdInGridView.Models
{
    public class MyItem : BindableBase
    {
        private string _itemTitle;
        private bool _isAd;
        private string _adAppId;
        private string _adUnitId;

        public string ItemTitle
        {
            get => _itemTitle;
            set => SetProperty(ref _itemTitle, value);
        }

        public bool IsAd
        {
            get => _isAd;
            set => SetProperty(ref _isAd, value);
        }

        public string AdAppId
        {
            get => _adAppId;
            set => SetProperty(ref _adAppId, value);
        }

        public string AdUnitId
        {
            get => _adUnitId;
            set => SetProperty(ref _adUnitId, value);
        }
    }
}
