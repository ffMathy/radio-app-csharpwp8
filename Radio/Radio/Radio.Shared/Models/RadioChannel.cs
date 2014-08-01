using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Radio.Helpers;

namespace Radio.Models
{

    public class RadioChannel : INotifyPropertyChanged
    {

        public enum ChannelCoverage
        {
            National,
            International,
            Local
        }

        public ProgramList ProgramList
        {
            get { return _programList; }
        }

        private readonly List<RadioWebFeed> _webRadioFeeds;
        public IEnumerable<RadioWebFeed> WebRadioFeeds
        {
            get { return _webRadioFeeds; }
        }

        private readonly List<RadioBeacon> _beacons;

        private RadioWebFeed _selectedWebRadioFeed;

        private readonly ProgramList _programList;
        private string _name;

        public IEnumerable<RadioBeacon> Beacons
        {
            get { return _beacons; }
        }

        public string LogoUri { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        public ChannelCoverage Coverage { get; set; }

        public RadioWebFeed SelectedWebRadioFeed
        {
            get { return _selectedWebRadioFeed; }
            set
            {
                if (Equals(value, _selectedWebRadioFeed)) return;

// ReSharper disable once CSharpWarnings::CS4014
                StorageHelper.StoreSetting("SelectedWebRadioFeed" + Name, value);

                _selectedWebRadioFeed = value;
                OnPropertyChanged();
            }
        }

        public RadioChannel() : this(null) {}

        public RadioChannel(ProgramList programList)
        {
            _beacons = new List<RadioBeacon>();
            _webRadioFeeds = new List<RadioWebFeed>();
            _programList = programList;
        }

        public RadioChannel(string name, Uri logoUri, IEnumerable<RadioWebFeed> webFeeds, ProgramList programs) : this(name, logoUri, webFeeds, null, programs) { }

        public RadioChannel(string name, Uri logoUri, IEnumerable<RadioBeacon> beacons, ProgramList programs) : this(name, logoUri, null, beacons, programs) { }

        public RadioChannel(string name, Uri logoUri, IEnumerable<RadioWebFeed> webFeeds) : this(name, logoUri, webFeeds, (ProgramList)null) { }

        public RadioChannel(string name, Uri logoUri, IEnumerable<RadioBeacon> beacons) : this(name, logoUri, beacons, null) { }

        public RadioChannel(string name, Uri logoUri, IEnumerable<RadioWebFeed> webFeeds,
            IEnumerable<RadioBeacon> beacons) : this(name, logoUri, webFeeds, beacons, null)
        {
        }

        public RadioChannel(string name, Uri logoUri, IEnumerable<RadioWebFeed> webFeeds, IEnumerable<RadioBeacon> beacons, ProgramList programs) : this(programs)
        {
            this.Name = name;
            this.LogoUri = logoUri + "";

            if (beacons != null)
            {
                foreach (var station in beacons)
                {
                    _beacons.Add(station);
                }
            }

            if (webFeeds != null)
            {
                foreach (var feed in webFeeds)
                {
                    if (_selectedWebRadioFeed == null)
                    {
                        _selectedWebRadioFeed = feed;
                    }
                    _webRadioFeeds.Add(feed);
                }

                LoadSelectedFeed();
            }
        }

        private async void LoadSelectedFeed()
        {
            var selectedFeed = await StorageHelper.GetSetting("SelectedWebRadioFeed" + Name, WebRadioFeeds.FirstOrDefault());
            if (selectedFeed != null)
            {
                SelectedWebRadioFeed = WebRadioFeeds.FirstOrDefault(f => f.AreaName == selectedFeed.AreaName);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is RadioChannel)
                {
                    var other = (RadioChannel) obj;
                    return other.Name == Name;
                }
                else
                {
                    var type = obj.GetType();
                    if (type.Name == "PanoramaItem")
                    {
                        var panoramaItem = (dynamic) obj;
                        return base.Equals((object) panoramaItem.Header);
                    }
                }
            }
            return base.Equals(obj);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
