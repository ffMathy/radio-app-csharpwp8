using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using GalaSoft.MvvmLight.Messaging;
using Radio.Commands;
using Radio.Factories;
using Radio.Helpers;
using Radio.Messages;
using Radio.Models;

namespace Radio.ViewModels
{
    class RadioListViewModel
    {
        private ObservableCollection<RadioChannel> _latestChannels;

        public IEnumerable<RadioChannel> LatestChannels
        {
            get { return _latestChannels; }
        }

        public IEnumerable<RadioChannel> AllChannels
        {
            get { return NationalChannels.Union(LocalChannels).Union(InternationalChannels); }
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
            var resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");

            var factoryName = resourceMap.GetValue("FactoryName").ValueAsString;
            var factory = RadioChannelFactory.GetNationalChannelFactory(factoryName);
            var channels = factory.CreateRadioChannels().ToArray();

            LoadLatestChannels(channels);

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

                await StorageHelper.StoreSetting("LatestChannels", _latestChannels);

                NavigationHelper.NavigationService.Navigate(typeof(PlayerPage));
            });

            NationalChannels = channels.Where(c => c.Coverage == RadioChannel.ChannelCoverage.National);
            LocalChannels = channels.Where(c => c.Coverage == RadioChannel.ChannelCoverage.Local);
            InternationalChannels = channels.Where(c => c.Coverage == RadioChannel.ChannelCoverage.International);
        }

        private async void LoadLatestChannels(IEnumerable<RadioChannel> channels)
        {
            _latestChannels = Debugger.IsAttached
                ? new ObservableCollection<RadioChannel>(channels.Take(10))
                : await StorageHelper.GetSetting("LatestChannels", new ObservableCollection<RadioChannel>());
            foreach (var channel in _latestChannels.ToArray())
            {
                if (!channels.Contains(channel))
                {
                    _latestChannels.Remove(channel);
                }
            }

            await StorageHelper.StoreSetting("LatestChannels", _latestChannels);
        }

        public DelegateWaitCommand ChannelTappedCommand { get; private set; }
    }
}
