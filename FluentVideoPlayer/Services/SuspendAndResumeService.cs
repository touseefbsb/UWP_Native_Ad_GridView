using System;
using System.Threading.Tasks;

using FluentVideoPlayer.Activation;

using Windows.ApplicationModel.Activation;

namespace FluentVideoPlayer.Services
{
    // More details regarding the application lifecycle and how to handle suspend and resume at https://docs.microsoft.com/windows/uwp/launch-resume/app-lifecycle
    internal class SuspendAndResumeService : ActivationHandler<LaunchActivatedEventArgs>
    {
        private const string StateFilename = "SuspendAndResumeState";

        // TODO WTS: Subscribe to this event if you want to save the current state. It is fired just before the app enters the background.
        public event EventHandler<OnBackgroundEnteringEventArgs> OnBackgroundEntering;

        public void SaveStateAsync()
        {
            var suspensionState = new SuspensionState()
            {
                SuspensionDate = DateTime.Now
            };

            var target = OnBackgroundEntering?.Target.GetType();
            var onBackgroundEnteringArgs = new OnBackgroundEnteringEventArgs(suspensionState, target);

            OnBackgroundEntering?.Invoke(this, onBackgroundEnteringArgs);
            
        }

        protected override async Task HandleInternalAsync(LaunchActivatedEventArgs args)
        {
            RestoreStateAsync();
        }

        protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
        {
            return args.PreviousExecutionState == ApplicationExecutionState.Terminated;
        }

        private void RestoreStateAsync()
        {
            //var saveState = await ApplicationData.Current.LocalFolder.ReadAsync<OnBackgroundEnteringEventArgs>(StateFilename);
            //if (saveState?.Target != null && typeof(Page).IsAssignableFrom(saveState.Target))
            //{
            //    NavigationService.Navigate(saveState.Target, saveState.SuspensionState);
            //}
        }
    }
}
