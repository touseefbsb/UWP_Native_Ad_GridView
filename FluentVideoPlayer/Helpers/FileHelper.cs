using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentVideoPlayer.Helpers
{
    internal static class FileHelper
    {
        #region StaticFields
        static readonly string videoFilter = "System.Kind:=System.Kind#Video";
        internal readonly static List<string> VideoFileTypes = new List<string> { ".webm", ".flv", ".mkv", ".vob", ".ogv", ".ogg",
                                                         ".drc", ".gif", ".gifv", ".mng", ".avi",".mov",
                                                         ".qt",".wmv",".yuv",".rm",".rmvb",".asf",".amv",
                                                         ".mp4",".m4p",".m4v",".mpg",".mp2",".mpeg",".mpe",
                                                         ".mpv",".m2v",".svi",".3gp",".3g2",".mxf",
                                                         ".roq",".nsv",".f4v",".f4p",".f4a",".f4b",".divx"};



        internal readonly static string[] RequiredVideoProperties = new String[] { "System.Media.Duration" };
        private static QueryOptions videoFileOptions;
        private static QueryOptions videoFolderOptions;
        #endregion

        #region StaticMethods       

        internal static StorageFileQueryResult GetVideoFilesQuery(StorageFolder Folder, uint thumbnailRequestedSize)
        {
            if (videoFileOptions is default(QueryOptions))
            {
                videoFileOptions = new QueryOptions()
                {
                    IndexerOption = IndexerOption.UseIndexerWhenAvailable
                };
                foreach (var filter in VideoFileTypes)
                {
                    videoFileOptions.FileTypeFilter.Add(filter);
                }
                videoFileOptions.ApplicationSearchFilter += videoFilter;
                videoFileOptions.SetPropertyPrefetch(PropertyPrefetchOptions.VideoProperties, RequiredVideoProperties);
                videoFileOptions.SetThumbnailPrefetch(ThumbnailMode.VideosView, thumbnailRequestedSize, ThumbnailOptions.UseCurrentScale);
            }
            return Folder.CreateFileQueryWithOptions(videoFileOptions);
        }

        internal static StorageFolderQueryResult GetVideoFoldersQuery(StorageFolder Folder)
        {
            if (videoFolderOptions is default(QueryOptions))
            {
                videoFolderOptions = new QueryOptions(CommonFolderQuery.DefaultQuery)
                {
                    IndexerOption = IndexerOption.UseIndexerWhenAvailable
                };
            }
            return Folder.CreateFolderQueryWithOptions(videoFolderOptions);
        }
        #endregion

    }
}
