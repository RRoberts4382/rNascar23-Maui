using rNascar23Multi.Settings.Models;

namespace rNascar23Multi.ViewModels
{
    public interface INotifySettingsChanged
    {
        void UserSettingsUpdated(SettingsModel settings);
    }
}
