using System.Collections.Generic;
using System.Linq;
using Radio.Models;

namespace Radio.ViewModels
{
    class PlayerViewModel
    {
        public IEnumerable<RadioChannel> Playlist { get; private set; }

        private static PlayerViewModel _instance;
        public static PlayerViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerViewModel();
                }
                return _instance;
            }
        }

        private PlayerViewModel()
        {
            Playlist = RadioListViewModel.Instance.LatestChannels;
        }

        public void PlayRadio(RadioChannel radioChannel)
        {
            var feeds = radioChannel.WebRadioFeeds;
            var feed = feeds.First();

            //BackgroundAudioPlayer.Instance.Track = new AudioTrack(new Uri(feed.HighQualityStreamUri), radioChannel.Name, null, null, null);
            //BackgroundAudioPlayer.Instance.Play();
        }
    }
}
