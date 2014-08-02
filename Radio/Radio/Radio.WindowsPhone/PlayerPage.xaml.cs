using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Radio.Controls;
using Radio.Helpers;
using Radio.Models;
using Radio.ViewModels;
using Windows.UI.ViewManagement;

namespace Radio
{
    public partial class PlayerPage : Page
    {

        private readonly Storyboard _fadeBackgroundImageInAnimation;
        private readonly Storyboard _fadeBackgroundImageOutAnimation;

        private Task _sleepTimerTask;

        private RadioChannel _currentPanoramaChannel;

        private readonly AppBarButton _sleepTimerBarButton;
        private readonly AppBarButton _musicQualityBarButton;
        private readonly AppBarButton _pinBarButton;
        private readonly AppBarButton _fmRadioBarButton;

        private readonly MediaPlayer _player;

        public PlayerPage()
        {
            _player = BackgroundMediaPlayer.Current;

            var statusBar = StatusBar.GetForCurrentView();
            statusBar.HideAsync();

            InitializeComponent();

            _fadeBackgroundImageOutAnimation = (Storyboard)Resources["FadeBackgroundImageOut"];
            _fadeBackgroundImageInAnimation = (Storyboard)Resources["FadeBackgroundImageIn"];

            //var applicationBar = new CommandBar();
            ////applicationBar.PrimaryCommands.Add(_sleepTimerBarButton = new AppBarButton() {Icon = new IconElement() {}new Uri("/Assets/Bar/appbar.clock.png", UriKind.Relative))}
            ////{
            ////    Text = "sleep timer"
            ////});

            ////applicationBar.PrimaryCommands.Add(_pinBarButton = new AppBarButton(new Uri("/Assets/Bar/appbar.pin.png", UriKind.Relative))
            ////{
            ////    Text = "fastgør"
            ////});

            ////_sleepTimerBarButton.Click += SleepTimerBarButtonOnClick;
            ////_pinBarButton.Click += PinBarButtonOnClick;

            //BottomAppBar = applicationBar;

            if (Debugger.IsAttached)
            {

                //applicationBar.Buttons.Add(_musicQualityBarButton = new ApplicationBarIconButton(new Uri("/Assets/Bar/appbar.connection.quality.veryhigh.png", UriKind.Relative))
                //{
                //    Text = "lydkvalitet"
                //});

                //applicationBar.Buttons.Add(_fmRadioBarButton = new ApplicationBarIconButton(new Uri("/Assets/Bar/appbar.tower.png", UriKind.Relative))
                //{
                //    Text = "FM radio"
                //});
            }

        }

        private async void SetSelectedItem()
        {
            var currentPlayingChannel = PlayerViewModel.Instance.CurrentlyPlayingRadio ?? PlayerViewModel.Instance.Playlist.First();

            if (!Equals(_currentPanoramaChannel, currentPlayingChannel))
            {
                _currentPanoramaChannel = currentPlayingChannel;

                _fadeBackgroundImageInAnimation.Pause();
                _fadeBackgroundImageOutAnimation.Begin();

                await Task.Delay((int)_fadeBackgroundImageOutAnimation.Duration.TimeSpan.TotalMilliseconds);
                BackgroundImage.Source = new BitmapImage(new Uri(_currentPanoramaChannel.LogoUri));

                _fadeBackgroundImageOutAnimation.Pause();
                _fadeBackgroundImageInAnimation.Begin();

                await Task.Delay((int)_fadeBackgroundImageInAnimation.Duration.TimeSpan.TotalMilliseconds);

            }
        }

        //private void PinBarButtonOnClick(object sender, EventArgs eventArgs)
        //{
        //    var currentRadio = PlayerViewModel.Instance.CurrentlyPlayingRadio;
        //    var navigationUri = "/Views/PlayerPage.xaml?radio=" + currentRadio.Name;

        //    var existingTiles = ShellTile.ActiveTiles.Where(t => t.NavigationUri + "" == navigationUri);
        //    foreach (var tile in existingTiles)
        //    {
        //        tile.Delete();
        //    }

        //    try
        //    {
        //        var newTile = new FlipTileData();
        //        newTile.Title = currentRadio.Name;
        //        newTile.BackgroundImage =
        //            newTile.WideBackgroundImage =
        //                newTile.SmallBackgroundImage = new Uri(currentRadio.LogoUri, UriKind.Relative);
        //        ShellTile.Create(new Uri(navigationUri, UriKind.Relative), newTile, true);
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        //tiles can only be created when in the foreground.
        //    }
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            var viewModel = PlayerViewModel.Instance;
            DataContext = viewModel;

            viewModel.OnPlayRequested += viewModel_OnPlayRequested;

            var radioName = (string)e.Parameter;
            if (!string.IsNullOrEmpty(radioName))
            {

                var radio = RadioListViewModel.Instance.AllChannels.FirstOrDefault(r => r.Name == radioName);
                if (radio != null)
                {
                    PlayerViewModel.Instance.CurrentlyPlayingRadio = radio;
                    SetSelectedItem();
                }
            }

            //InstallSleepTimerIfNeeded();
        }

        void viewModel_OnPlayRequested(RadioChannel channel)
        {
            var message = new ValueSet
                    {
                        {
                            "Play",
                            SerializationHelper.Serialize(channel)
                        }
                    };
            BackgroundMediaPlayer.SendMessageToBackground(message);

        }

        //private void InstallSleepTimerIfNeeded()
        //{
        //    if (_sleepTimerTask == null || _sleepTimerTask.Status != TaskStatus.Running)
        //    {
        //        _sleepTimerTask = RunSleepTimerCheck();
        //    }
        //}

        //void SleepTimerBarButtonOnClick(object sender, EventArgs e)
        //{

        //    Panorama.DefaultItem = _currentPanoramaChannel;

        //    InstallSleepTimerIfNeeded();

        //    var content = new SleepTimerMessageBoxContent(PlayerViewModel.Instance.SleepTimer);
        //    var box = new CustomMessageBox();

        //    box.IsFullScreen = false;
        //    box.Content = content;

        //    box.Show();
        //}

        //private async Task RunSleepTimerCheck()
        //{
        //    while (true)
        //    {
        //        if (!Debugger.IsAttached)
        //        {
        //            await Task.Delay(60000);
        //        }
        //        else
        //        {
        //            await Task.Delay(10000);
        //        }

        //        var sleepTimer = PlayerViewModel.Instance.SleepTimer;
        //        if (sleepTimer != null && DateTime.Now > sleepTimer)
        //        {
        //            PlayerViewModel.Instance.SleepTimer = null;
        //            BackgroundAudioPlayer.Instance.Stop();

        //            Application.Current.Terminate();

        //            return;
        //        }
        //    }
        //}

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    Panorama.DefaultItem = _currentPanoramaChannel;
        //    base.OnNavigatingFrom(e);
        //}

        private void Hub_OnSectionsInViewChanged(object sender, SectionsInViewChangedEventArgs e)
        {
            var hub = (ItemsHub)sender;
            var sections = hub.SectionsInView;

            if (sections.Any())
            {
                var item = sections.First().DataContext;

                var channel = item as RadioChannel;
                if (channel != null)
                {

                    PlayerViewModel.Instance.CurrentlyPlayingRadio = channel;
                    SetSelectedItem();

                }

            }
        }
    }
}