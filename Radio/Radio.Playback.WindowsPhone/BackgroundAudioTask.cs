using System;
using Windows.ApplicationModel.Background;
using Windows.Media;
using Windows.Media.Playback;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage;
using Radio.Helpers;
using Radio.Models;

namespace Radio.Playback.WindowsPhone
{
    public sealed class BackgroundAudioTask : IBackgroundTask
    {

        private SystemMediaTransportControls _systemMediaTransportControl;
        private BackgroundTaskDeferral _deferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {

            BackgroundAudioTaskHelper.Run(taskInstance, out _systemMediaTransportControl, out _deferral);

            BackgroundMediaPlayer.MessageReceivedFromForeground += MessageReceivedFromForeground;
            BackgroundMediaPlayer.Current.CurrentStateChanged += BackgroundMediaPlayerCurrentStateChanged;

            //associate a cancellation and completed handlers with the background task.
            taskInstance.Canceled += OnCanceled;
            taskInstance.Task.Completed += TaskCompleted;
        }

        private void MessageReceivedFromForeground(object sender, MediaPlayerDataReceivedEventArgs e)
        {
            var valueSet = e.Data;
            foreach (string key in valueSet.Keys)
            {
                var data = valueSet[key] + "";
                switch (key)
                {
                    case "Play":
                        var channel = SerializationHelper.Deserialize<RadioChannel>(data);
                        Play(channel);
                        break;
                }
            }
        }

        private void TaskCompleted(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            BackgroundMediaPlayer.Shutdown();
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            BackgroundMediaPlayer.Shutdown();
        }

        private async void Play(RadioChannel channel)
        {
            var mediaPlayer = BackgroundMediaPlayer.Current;
            mediaPlayer.AutoPlay = true;

            var uri = new Uri(channel.SelectedWebRadioFeed.HighQualityStreamUri);
            mediaPlayer.SetUriSource(uri);

            //update the universal volume control.
            _systemMediaTransportControl.ButtonPressed += MediaTransportControlButtonPressed;
            _systemMediaTransportControl.DisplayUpdater.Type = MediaPlaybackType.Music;

            var artist = string.Empty;

            var programList = channel.ProgramList;
            if (programList != null)
            {
                var currentProgram = channel.ProgramList.CurrentProgram;
                if (currentProgram != null)
                {
                    artist = currentProgram.Title;
                }
            }
            
            _systemMediaTransportControl.DisplayUpdater.MusicProperties.Title = channel.Name;
            _systemMediaTransportControl.DisplayUpdater.MusicProperties.Artist = artist;

            _systemMediaTransportControl.DisplayUpdater.Update();

            mediaPlayer.Play();
        }

        private void BackgroundMediaPlayerCurrentStateChanged(MediaPlayer sender, object args)
        {
            if (sender.CurrentState == MediaPlayerState.Playing)
            {
                _systemMediaTransportControl.PlaybackStatus = MediaPlaybackStatus.Playing;
            }
            else if (sender.CurrentState == MediaPlayerState.Stopped)
            {
                _systemMediaTransportControl.PlaybackStatus = MediaPlaybackStatus.Stopped;
            }
        }

        private void MediaTransportControlButtonPressed(SystemMediaTransportControls sender,
            SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    BackgroundMediaPlayer.Current.Play();
                    break;
                case SystemMediaTransportControlsButton.Stop:
                    BackgroundMediaPlayer.Current.Pause();
                    break;
            }
        }

    }
}
