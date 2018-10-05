using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentVideoPlayer.Helpers;
using FluentVideoPlayer.Models;
using Microsoft.Toolkit.Uwp.UI;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentVideoPlayer.ViewModels
{
    public class LibraryViewModel : Observable
    {
        private const uint _thumbnailReqestedSize = 190;
        private const uint _stepSize = 3;
        private ObservableCollection<VideoFolder> SourcePrivate { get; }
        public AdvancedCollectionView Source { get; set; }
        public StorageFolder MainFolder { get; set; }

        public LibraryViewModel()
        {
            SourcePrivate = new ObservableCollection<VideoFolder>();
            Source = new AdvancedCollectionView(SourcePrivate, true);
            // advanced collection view is just used to sorting and filtering in my app
        }

        public async Task Fill(StorageFolder mainfolder)
        {
            if (SourcePrivate.Count > 0)
                SourcePrivate.Clear();

            MainFolder = mainfolder;
            IndexedState folderIndexedState = await MainFolder.GetIndexedStateAsync();
            if (folderIndexedState == IndexedState.NotIndexed || folderIndexedState == IndexedState.Unknown)
            {
                //Only possible in indexed directories.                
            }
            else
            {
                //this method only works for indexed stuff ( known folders only )
                //For More Details visit https://blogs.msdn.microsoft.com/adamdwilson/2017/12/20/fast-file-enumeration-with-partially-initialized-storagefiles/

                await FillUpFolders();
                FillAds();
                await FillUpFiles();
            }
        }

        private void FillAds()
        {
            var ad = new NativeAdData();
            SourcePrivate.Add(ad);
        }

        private async Task FillUpFiles()
        {
            uint index = 0;
            var VideoQuery = FileHelper.GetVideoFilesQuery(MainFolder, _thumbnailReqestedSize);
            IReadOnlyList<StorageFile> files = await VideoQuery.GetFilesAsync(index, _stepSize);
            index += _stepSize;
            while (files.Count != 0)
            {
                var fileTask = VideoQuery.GetFilesAsync(index, _stepSize).AsTask();
                foreach (StorageFile file in files)
                {
                    var vv = new Video
                    {
                        MyVideoFile = file,
                        Thumbnail = new BitmapImage(new Uri("ms-appx:///Assets/SplashScreen.scale-200.png"))
                    };
                    SourcePrivate.Add(vv);
                    //set duration
                    IDictionary<string, object> size = await file.Properties.RetrievePropertiesAsync(
                                                       FileHelper.RequiredVideoProperties);
                    var ticks = Convert.ToInt64(size["System.Media.Duration"]);
                    vv.Duration = TimeSpan.FromTicks(ticks).ToString(@"hh\:mm\:ss");

                    //set asyncronosly thumbnail
                    BitmapImage bitm = null;
                    using (var imgSource = await vv.MyVideoFile?.GetScaledImageAsThumbnailAsync(ThumbnailMode.VideosView, _thumbnailReqestedSize, ThumbnailOptions.UseCurrentScale))
                    {
                        if (imgSource != default(object))//bsically null checking in a better way
                        {
                            bitm = new BitmapImage();
                            await bitm.SetSourceAsync(imgSource);
                        }
                    }
                    if (!(bitm is default(BitmapImage)))
                        vv.Thumbnail = bitm;

                }
                files = await fileTask;
                index += _stepSize;
            }
        }

        private async Task FillUpFolders()
        {
            uint index = 0;
            var VideoQuery = FileHelper.GetVideoFoldersQuery(MainFolder);
            IReadOnlyList<StorageFolder> folders = await VideoQuery.GetFoldersAsync(index, _stepSize);
            index += _stepSize;
            while (folders.Count != 0)
            {
                var folderTask = VideoQuery.GetFoldersAsync(index, _stepSize).AsTask();
                foreach (StorageFolder folder in folders)
                {
                    var vv = new Folder
                    {
                        MyStorageFolder = folder
                    };
                    SourcePrivate.Add(vv);
                }
                folders = await folderTask;
                index += _stepSize;
            }
        }
    }
}
