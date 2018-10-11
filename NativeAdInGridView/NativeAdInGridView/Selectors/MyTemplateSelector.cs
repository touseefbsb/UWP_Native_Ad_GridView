using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NativeAdInGridView.Models;

namespace NativeAdInGridView.Selectors
{
    public class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate AdTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is MyItem myItem)
            {
                return myItem.IsAd ? AdTemplate : NormalTemplate;
            }

            return base.SelectTemplateCore(item);
        }
    }
}
