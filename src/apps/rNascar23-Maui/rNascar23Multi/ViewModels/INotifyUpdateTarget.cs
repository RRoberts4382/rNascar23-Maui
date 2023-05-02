using rNascar23Multi.Logic;

namespace rNascar23Multi.ViewModels
{
    public interface INotifyUpdateTarget
    {
        Task UserSettingsUpdatedAsync();
        Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e);
    }
}
