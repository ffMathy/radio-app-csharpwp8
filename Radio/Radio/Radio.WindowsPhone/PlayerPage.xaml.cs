using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using RadioV2.Extensions;
using RadioV2.Helpers;
using RadioV2.MessageBoxes;
using RadioV2.Models;
using RadioV2.ViewModels;
using RadioV2.ViewModels.Helpers;

namespace RadioV2.Views
{
    public partial class PlayerPage : PhoneApplicationPage
    {

        private readonly Storyboard _fadeBackgroundImageInAnimation;
        private readonly Storyboard _fadeBackgroundImageOutAnimation;

        private Task _sleepTimerTask;

        private RadioChannel _currentPanoramaChannel;

        private readonly ApplicationBarIconButton _sleepTimerBarButton;
        private readonly ApplicationBarIconButton _musicQualityBarButton;
        private readonly ApplicationBarIconButton _pinBarButton;
        private readonly ApplicationBarIconButton _fmRadioBarButton;

        public PlayerPage()
        {
            InitializeComponent();

            _fadeBackgroundImageOutAnimation = (Storyboard)Resources["FadeBackgroundImageOut"];
            _fadeBackgroundImageInAnimation = (Storyboard)Resources["FadeBackgroundImageIn"];

            var applicationBar = new ApplicationBar();
            applicationBar.Buttons.Add(_sleepTimerBarButton = new ApplicationBarIconButton(new Uri("/Assets/Bar/appbar.clock.png", UriKind.Relative))
            {
                Text = "sleep timer"
            });

            applicationBar.Buttons.Add(_pinBarButton = new ApplicationBarIconButton(new Uri("/Assets/Bar/appbar.pin.png", UriKind.Relative))
            {
                Text = "fastgør"
            });

            _sleepTimerBarButton.Click += SleepTimerBarButtonOnClick;
            _pinBarButton.Click += PinBarButtonOnClick;

            ApplicationBar = applicationBar;

            if (Debugger.IsAttached)
            {

                applicationBar.Buttons.Add(_musicQualityBarButton = new ApplicationBarIconButton(new Uri("/Assets/Bar/appbar.connection.quality.veryhigh.png", UriKind.Relative))
                {
                    Text = "lydkvalitet"
                });

                applicationBar.Buttons.Add(_fmRadioBarButton = new ApplicationBarIconButton(new Uri("/Assets/Bar/appbar.tower.png", UriKind.Relative))
                {
                    Text = "FM radio"
                });
            }

        }

        private async void SetSelectedItem()
        {
            var currentPlayingChannel = PlayerViewModel.Instance.CurrentlyPlayingRadio ?? PlayerViewModel.Instance.Playlist.First();

            if (!NavigationContext.QueryString.ContainsKey("radio"))
            {
                NavigationContext.QueryString.Add("radio", string.Empty);
            }
            NavigationContext.QueryString["radio"] = currentPlayingChannel.Name;

            if (!Equals(_currentPanoramaChannel, currentPlayingChannel))
            {
                _currentPanoramaChannel = currentPlayingChannel;

                _fadeBackgroundImageInAnimation.Pause();
                _fadeBackgroundImageOutAnimation.Begin();

                await Task.Delay((int)_fadeBackgroundImageOutAnimation.Duration.TimeSpan.TotalMilliseconds);
                BackgroundImage.Source = new BitmapImage(new Uri(_currentPanoramaChannel.LogoUri, UriKind.Relative));

                _fadeBackgroundImageOutAnimation.Pause();
                _fadeBackgroundImageInAnimation.Begin();

                await Task.Delay((int)_fadeBackgroundImageInAnimation.Duration.TimeSpan.TotalMilliseconds);

            }
        }

        private void PinBarButtonOnClick(object sender, EventArgs eventArgs)
        {
            var currentRadio = PlayerViewModel.Instance.CurrentlyPlayingRadio;
            var navigationUri = "/Views/PlayerPage.xaml?radio=" + currentRadio.Name;

            var existingTiles = ShellTile.ActiveTiles.Where(t => t.NavigationUri + "" == navigationUri);
            foreach (var tile in existingTiles)
            {
                tile.Delete();
            }

            try
            {
                var newTile = new FlipTileData();
                newTile.Title = currentRadio.Name;
                newTile.BackgroundImage =
                    newTile.WideBackgroundImage =
                        newTile.SmallBackgroundImage = new Uri(currentRadio.LogoUri, UriKind.Relative);
                ShellTile.Create(new Uri(navigationUri, UriKind.Relative), newTile, true);
            }
            catch (InvalidOperationException)
            {
                //tiles can only be created when in the foreground.
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            DataContext = PlayerViewModel.Instance;
            var parameters = NavigationContext.QueryString;
            if (parameters.ContainsKey("radio"))
            {
                var radioName = parameters["radio"];

                var radio = RadioListViewModel.Instance.AllChannels.FirstOrDefault(r => r.Name == radioName);
                if (radio != null)
                {
                    PlayerViewModel.Instance.CurrentlyPlayingRadio = radio;
                    Panorama.DefaultItem = radio;
                    SetSelectedItem();
                }
            }

            InstallSleepTimerIfNeeded();
        }

        private void InstallSleepTimerIfNeeded()
        {
            if (_sleepTimerTask == null || _sleepTimerTask.Status != TaskStatus.Running)
            {
                _sleepTimerTask = RunSleepTimerCheck();
            }
        }

        void SleepTimerBarButtonOnClick(object sender, EventArgs e)
        {

            Panorama.DefaultItem = _currentPanoramaChannel;

            InstallSleepTimerIfNeeded();

            var content = new SleepTimerMessageBoxContent(PlayerViewModel.Instance.SleepTimer);
            var box = new CustomMessageBox();

            box.IsFullScreen = false;
            box.Content = content;

            box.Show();
        }

        private async Task RunSleepTimerCheck()
        {
            while (true)
            {
                if (!Debugger.IsAttached)
                {
                    await Task.Delay(60000);
                }
                else
                {
                    await Task.Delay(10000);
                }

                var sleepTimer = PlayerViewModel.Instance.SleepTimer;
                if (sleepTimer != null && DateTime.Now > sleepTimer)
                {
                    PlayerViewModel.Instance.SleepTimer = null;
                    BackgroundAudioPlayer.Instance.Stop();

                    Application.Current.Terminate();

                    return;
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Panorama.DefaultItem = _currentPanoramaChannel;
            base.OnNavigatingFrom(e);
        }

        private async void Panorama_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newChannel = (RadioChannel)null;

            var item = Panorama.SelectedItem;
            if (item is RadioChannel)
            {
                newChannel = (RadioChannel)item;
            }
            else if (item is PanoramaItem && (item as PanoramaItem).Header is RadioChannel)
            {
                newChannel = (item as PanoramaItem).Header as RadioChannel;
            }

            if (newChannel != null)
            {

                PlayerViewModel.Instance.CurrentlyPlayingRadio = newChannel;
                SetSelectedItem();

            }
        }

        private async void BuyFull_OnClick(object sender, RoutedEventArgs e)
        {
            await AdHelper.Instance.RemoveAds();
        }
    }
}