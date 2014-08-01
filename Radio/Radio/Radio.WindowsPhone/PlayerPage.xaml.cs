using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Radio.Models;
using Radio.ViewModels;

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

        public PlayerPage()
        {
            InitializeComponent();

            _fadeBackgroundImageOutAnimation = (Storyboard)Resources["FadeBackgroundImageOut"];
            _fadeBackgroundImageInAnimation = (Storyboard)Resources["FadeBackgroundImageIn"];

            var applicationBar = new CommandBar();
            //applicationBar.PrimaryCommands.Add(_sleepTimerBarButton = new AppBarButton() {Icon = new IconElement() {}new Uri("/Assets/Bar/appbar.clock.png", UriKind.Relative))}
            //{
            //    Text = "sleep timer"
            //});

            //applicationBar.PrimaryCommands.Add(_pinBarButton = new AppBarButton(new Uri("/Assets/Bar/appbar.pin.png", UriKind.Relative))
            //{
            //    Text = "fastgør"
            //});

            //_sleepTimerBarButton.Click += SleepTimerBarButtonOnClick;
            //_pinBarButton.Click += PinBarButtonOnClick;

            BottomAppBar = applicationBar;

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

        //private async void SetSelectedItem()
        //{
        //    var currentPlayingChannel = PlayerViewModel.Instance.CurrentlyPlayingRadio ?? PlayerViewModel.Instance.Playlist.First();

        //    if (!NavigationContext.QueryString.ContainsKey("radio"))
        //    {
        //        NavigationContext.QueryString.Add("radio", string.Empty);
        //    }
        //    NavigationContext.QueryString["radio"] = currentPlayingChannel.Name;

        //    if (!Equals(_currentPanoramaChannel, currentPlayingChannel))
        //    {
        //        _currentPanoramaChannel = currentPlayingChannel;

        //        _fadeBackgroundImageInAnimation.Pause();
        //        _fadeBackgroundImageOutAnimation.Begin();

        //        await Task.Delay((int)_fadeBackgroundImageOutAnimation.Duration.TimeSpan.TotalMilliseconds);
        //        BackgroundImage.Source = new BitmapImage(new Uri(_currentPanoramaChannel.LogoUri, UriKind.Relative));

        //        _fadeBackgroundImageOutAnimation.Pause();
        //        _fadeBackgroundImageInAnimation.Begin();

        //        await Task.Delay((int)_fadeBackgroundImageInAnimation.Duration.TimeSpan.TotalMilliseconds);

        //    }
        //}

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

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{

        //    DataContext = PlayerViewModel.Instance;
        //    var parameters = NavigationContext.QueryString;
        //    if (parameters.ContainsKey("radio"))
        //    {
        //        var radioName = parameters["radio"];

        //        var radio = RadioListViewModel.Instance.AllChannels.FirstOrDefault(r => r.Name == radioName);
        //        if (radio != null)
        //        {
        //            PlayerViewModel.Instance.CurrentlyPlayingRadio = radio;
        //            Panorama.DefaultItem = radio;
        //            SetSelectedItem();
        //        }
        //    }

        //    InstallSleepTimerIfNeeded();
        //}

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

        //private async void Panorama_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var newChannel = (RadioChannel)null;

        //    var item = Panorama.SelectedItem;
        //    if (item is RadioChannel)
        //    {
        //        newChannel = (RadioChannel)item;
        //    }
        //    else if (item is PanoramaItem && (item as PanoramaItem).Header is RadioChannel)
        //    {
        //        newChannel = (item as PanoramaItem).Header as RadioChannel;
        //    }

        //    if (newChannel != null)
        //    {

        //        PlayerViewModel.Instance.CurrentlyPlayingRadio = newChannel;
        //        SetSelectedItem();

        //    }
        //}
    }
}