using rNascar23Multi.Settings.Models;

namespace rNascar23Multi.Settings.Ports
{
    // All the code in this file is included in all platforms.
    public interface ISettingsRepository
    {
        public SettingsModel GetSettings();
        public void SaveSettings(SettingsModel model);
    }
}