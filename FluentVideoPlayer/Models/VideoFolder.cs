using FluentVideoPlayer.Helpers;
using Windows.Storage;
using Windows.UI.Xaml.Media;

namespace FluentVideoPlayer.Models
{
    /// <summary>
    /// Parent of video and folder objects
    /// </summary>
    public class VideoFolder : Observable
    {
        protected ImageSource _thumbnail;
        public ImageSource Thumbnail
        {
            get => _thumbnail;
            set => Set(ref _thumbnail, value, nameof(Thumbnail));
        }

        public string Title
        {
            get
            {
                if ((this as Video) != default(object))
                    return ((Video)this).MyVideoFile.DisplayName;

                return ((Folder)this).MyStorageFolder.DisplayName;
            }
        }    

        
    }
    /// <summary>
    /// the video object which will be used throughout the app to deal with video file
    /// </summary>
    public class Video : VideoFolder
    {
        public StorageFile MyVideoFile { get; set; }
        private string _duration;

        public string Duration
        {
            get => _duration;
            set => Set(ref _duration, value, nameof(Duration));
        }

        private bool _deleteButtonEnabled;

        public bool DeleteButtonEnabled
        {
            get => _deleteButtonEnabled;
            set => Set(ref _deleteButtonEnabled, value, nameof(DeleteButtonEnabled));
        }
        public string HistoryToken
        {
            get { return _historyToken; }
            set
            {
                _historyToken = value;
                if (!string.IsNullOrWhiteSpace(value))
                    DeleteButtonEnabled = true;
            }
        }

        
        private string _historyToken;

        public Video THIS => this;

    }
    /// <summary>
    /// the folder object which will be used as folder throught the app
    /// </summary>
    public class Folder : VideoFolder
    {
        public StorageFolder MyStorageFolder { get; set; }
        public Folder THIS => this;
    }

}
