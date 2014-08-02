using Windows.ApplicationModel.Background;
using Windows.Media;

namespace Radio.Playback
{
    public static class BackgroundAudioTaskHelper
    {

        private static SystemMediaTransportControls _systemMediaTransportControl;
        private static BackgroundTaskDeferral _deferral;

        public static void Run(IBackgroundTaskInstance taskInstance, out SystemMediaTransportControls systemMediaTransportControl, out BackgroundTaskDeferral deferral)
        {
            _systemMediaTransportControl = SystemMediaTransportControls.GetForCurrentView();
            _systemMediaTransportControl.IsEnabled = true;
            _systemMediaTransportControl.IsPlayEnabled = true;
            _systemMediaTransportControl.IsStopEnabled = true;
            _systemMediaTransportControl.IsNextEnabled = true;
            _systemMediaTransportControl.IsPreviousEnabled = true;

            //associate a cancellation and completed handlers with the background task.
            taskInstance.Canceled += OnCanceled;
            taskInstance.Task.Completed += TaskCompleted;

            _deferral = taskInstance.GetDeferral();

            systemMediaTransportControl = _systemMediaTransportControl;
            deferral = _deferral;
        }

        private static void TaskCompleted(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            _deferral.Complete();
        }

        private static void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _deferral.Complete();
        }


    }
}
