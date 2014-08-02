using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Networking.Connectivity;
using Radio.Helpers;
using Radio.Models;

namespace Radio.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {

        private static PlayerViewModel _instance;

        private RadioChannel _currentlyPlayingRadio;
        private ConnectionProfile _currentNetworkAvailability;

        public delegate void OnPlayRequestedEventHandler(RadioChannel channel);

        public event OnPlayRequestedEventHandler OnPlayRequested;

        public IList<object> PanoramaPlaylist { get; private set; }

        public IList<RadioChannel> Playlist
        {
            get { return PanoramaPlaylist.OfType<RadioChannel>().ToList(); }
        }

        //public DateTime? SleepTimer
        //{
        //    get { return StorageHelper.GetSetting<DateTime?>("SleepTimer"); }
        //    set { StorageHelper.StoreSetting("SleepTimer", value); }
        //}

        public static PlayerViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerViewModel();
                }
                else
                {
                    _instance.RefreshPlaylist();
                }
                return _instance;
            }
        }

        public RadioChannel CurrentlyPlayingRadio
        {
            get { return _currentlyPlayingRadio; }
            set
            {
                if (Equals(value, _currentlyPlayingRadio)) return;

                if (_currentlyPlayingRadio != null)
                {
                    _currentlyPlayingRadio.PropertyChanged -= value_PropertyChanged;
                }
                _currentlyPlayingRadio = value;
                if (value != null)
                {
                    value.PropertyChanged += value_PropertyChanged;
                }

                PlayRadio(value);
            }
        }

        void value_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedWebRadioFeed")
            {
                PlayRadio(CurrentlyPlayingRadio);
            }
        }

        private ConnectionProfile GetFastestConnection()
        {
            return NetworkInformation.GetInternetConnectionProfile();
        }

        private PlayerViewModel()
        {
            _currentNetworkAvailability = GetFastestConnection();
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;

            RefreshPlaylist();

            //var player = BackgroundAudioPlayer.Instance;
            //if (player != null && player.Track != null && player.Track.Source != null)
            //{
            //    RefreshCurrentTrack();
            //}
            //BackgroundAudioPlayer.Instance.PlayStateChanged += (sender, args) => RefreshCurrentTrack();
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            OnConnectionChanged();
        }

        public void OnConnectionChanged(bool forceFailConnection = false)
        {
            var newStatus = GetFastestConnection();
            if (newStatus != _currentNetworkAvailability || forceFailConnection)
            {
                _currentNetworkAvailability = newStatus;

                PlayRadio(CurrentlyPlayingRadio);
            }
        }

        private void RefreshPlaylist()
        {
            var viewModel = RadioListViewModel.Instance;

            var latestChannels = viewModel.LatestChannels.ToList();
            var allChannels = viewModel.AllChannels.ToList();

            PanoramaPlaylist = allChannels.OrderBy(channel => latestChannels.IndexOf(channel) == -1 ? int.MaxValue : latestChannels.IndexOf(channel)).Cast<object>().ToList();
        }

        //private void RefreshCurrentTrack()
        //{
        //    var player = BackgroundAudioPlayer.Instance;
        //    try
        //    {
        //        if (player.Track != null)
        //        {
        //            CurrentlyPlayingRadio =
        //                Playlist.First(
        //                    s =>
        //                        s.WebRadioFeeds.Any(
        //                            w =>
        //                                w.HighQualityStreamUri.Replace(":80", "") == player.Track.Source + "" ||
        //                                w.LowQualityStreamUri.Replace(":80", "") == player.Track.Source + ""));
        //        }
        //    }
        //    catch
        //    {
        //        if (Debugger.IsAttached)
        //        {
        //            throw;
        //        }
        //    }
        //}

        private void PlayRadio(RadioChannel radioChannel)
        {
            _currentlyPlayingRadio = radioChannel;
            if (OnPlayRequested != null)
            {
                OnPlayRequested(radioChannel);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
