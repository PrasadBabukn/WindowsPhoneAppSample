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
using System.ComponentModel;
using System.Collections.ObjectModel;
using WindowsPhoneAppSample.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsPhoneAppSample.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
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

        private const string youtubeSearchUrl = 
            "https://gdata.youtube.com/feeds/api/videos?v=2&alt=jsonc&author=MobileTechReview&max-results=50";

        private ObservableCollection<YoutubeItem> videos;
        public ObservableCollection<YoutubeItem> Videos
        {
            get { return videos; }
            set
            {
                videos = value;
                RaisePropertyChanged("Videos");
            }
        }

        private Visibility loadingVisibility = Visibility.Visible;
        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set
            {
                loadingVisibility = value;
                RaisePropertyChanged("LoadingVisibility");
            }
        }

        public MainPageViewModel()
        {
            MakeWebRequestCall(youtubeSearchUrl);
        }

        private void MakeWebRequestCall(string uri)
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (s, e)=>
            {
                if (e.Error == null)
                {
                    var stuff = JObject.Parse(e.Result);
                    var youtubeDataStuff = JObject.Parse(stuff["data"].ToString());
                    Videos = JsonConvert.DeserializeObject<ObservableCollection<YoutubeItem>>
                        (youtubeDataStuff["items"].ToString());
                }
                else
                {
                    MessageBox.Show(e.Error.Message, "Error in Network", MessageBoxButton.OK);
                }

                LoadingVisibility = Visibility.Collapsed;
            };
            client.DownloadStringAsync(new Uri(uri, UriKind.RelativeOrAbsolute));
        }


    }
}
