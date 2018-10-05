using FluentVideoPlayer.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentVideoPlayer.DataTemplateSelectors
{
    public class VideoFolderDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VideoTemplate { get; set; }
        public DataTemplate FolderTemplate { get; set; }
        public DataTemplate NativeAdTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            switch (item)
            {
                case Video _:
                    return VideoTemplate;
                case Folder _:
                    return FolderTemplate;
                case NativeAdData _:
                    return NativeAdTemplate;
            }

            return base.SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) => SelectTemplateCore(item);
        

    }
}
