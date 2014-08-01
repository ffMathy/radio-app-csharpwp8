using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Radio.Models;

namespace Radio.ViewModels
{
    class RadioListViewModel
    {
        private readonly ObservableCollection<RadioChannel> _latestChannels;

        public IEnumerable<RadioChannel> LatestChannels
        {
            get { return _latestChannels; }
        }

        public IEnumerable<RadioChannel> NationalChannels { get; private set; }
        public IEnumerable<RadioChannel> LocalChannels { get; private set; }
        public IEnumerable<RadioChannel> InternationalChannels { get; private set; }

        private static RadioListViewModel _instance;
        public static RadioListViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RadioListViewModel();
                }
                return _instance;
            }
        }

        private RadioListViewModel()
        {
            var factory = RadioChannelFactory.GetNationalChannelFactory(AppResources.FactoryName);
            var channels = factory.CreateRadioChannels().ToArray();

            _latestChannels = Debugger.IsAttached ? new ObservableCollection<RadioChannel>(channels.Take(10)) : StorageHelper.GetSetting("LatestChannels", new ObservableCollection<RadioChannel>());
            foreach (var channel in _latestChannels.ToArray())
            {
                if (!channels.Contains(channel))
                {
                    _latestChannels.Remove(channel);
                }
            }

            StorageHelper.StoreSetting("LatestChannels", _latestChannels);

            ChannelTappedCommand = new DelegateWaitCommand(async delegate(object o)
            {
                var channel = (RadioChannel)o;
                if (_latestChannels.Contains(channel))
                {
                    _latestChannels.Remove(channel);
                }

                _latestChannels.Insert(0, channel);

                if (_latestChannels.Count > 10)
                {
                    _latestChannels.RemoveAt(_latestChannels.Count - 1);
                }

                StorageHelper.StoreSetting("LatestChannels", _latestChannels);

                Messenger.Default.Send(new GoToPageMessage("PlayerPage"));
            });

            NationalChannels = channels.Where(c => c.Coverage == RadioChannel.ChannelCoverage.National);
            LocalChannels = channels.Where(c => c.Coverage == RadioChannel.ChannelCoverage.Local);
            InternationalChannels = channels.Where(c => c.Coverage == RadioChannel.ChannelCoverage.International);
        }

        public DelegateWaitCommand ChannelTappedCommand { get; private set; }
    }
}
