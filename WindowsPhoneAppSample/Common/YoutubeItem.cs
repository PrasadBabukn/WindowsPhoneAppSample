using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace WindowsPhoneAppSample.Common
{
    [DataContract, KnownType(typeof(Thumbnail))]
    public class YoutubeItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public Thumbnail thumbnail { get; set; }

        #region Image for VideoData
        private ImageSource videoImage = null;
        public ImageSource VideoImage
        {
            get
            {
                if (videoImage == null && thumbnail != null &&
                    thumbnail.sqDefault != null)
                {
                    videoImage = new
                        BitmapImage(new Uri(thumbnail.sqDefault));
                }
                return this.videoImage;
            }
            set { videoImage = value; }
        }
        #endregion

        private string url;
        public string Url
        {
            get
            {
                if (!string.IsNullOrEmpty(id))
                {
                    url = string.Format("http://www.youtube.com/watch?v={0}", id);
                }
                return url;
            }
            set { url = value; }
        }
    }

    [DataContract]
    public class Thumbnail
    {
        [DataMember]
        public string sqDefault { get; set; }
        [DataMember]
        public string hqDefault { get; set; }
    }
}
