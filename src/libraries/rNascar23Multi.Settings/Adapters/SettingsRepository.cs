using Newtonsoft.Json;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.Settings.Ports;

namespace rNascar23Multi.Settings.Adapters
{
    internal class SettingsRepository : ISettingsRepository
    {
        #region consts

        private const string DefaultRootDirectory = "rNascar23\\";
        private const string DefaultDataDirectory = "Data\\";
        private const string DefaultLogDirectory = "Logs\\";
        private const string UserSettingsFileName = "UserSettings.json";

        #endregion

        public SettingsModel GetSettings()
        {
            var rootDirectory = GetDefaultRootDirectory();

            var userSettingsFilePath = Path.Combine(rootDirectory, UserSettingsFileName);

            if (!File.Exists(userSettingsFilePath))
                return GetDefaultUSerSettings();
            else
            {
                var json = File.ReadAllText(userSettingsFilePath);

                if (String.IsNullOrEmpty(json))
                    return GetDefaultUSerSettings();
                else
                {
                    var userSettings = JsonConvert.DeserializeObject<SettingsModel>(json);

                    EnsureDirectoryExists(userSettings.DataDirectory);
                    EnsureDirectoryExists(userSettings.LogDirectory);

                    if (userSettings == null)
                        return GetDefaultUSerSettings();
                    else
                        return userSettings;
                }
            }
        }

        public void SaveSettings(SettingsModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            EnsureDirectoryExists(model.DataDirectory);
            EnsureDirectoryExists(model.LogDirectory);

            var json = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);

            var rootDirectory = GetDefaultRootDirectory();

            var userSettingsFilePath = Path.Combine(rootDirectory, UserSettingsFileName);

            File.WriteAllText(userSettingsFilePath, json);
        }

        #region private

        private static string GetDefaultRootDirectory()
        {
            var myDataDirectory = $"{FileSystem.Current.AppDataDirectory}\\";

            var defaultRootDirectoryPath = Path.Combine(myDataDirectory, DefaultRootDirectory);

            if (!Directory.Exists(defaultRootDirectoryPath))
            {
                Directory.CreateDirectory(defaultRootDirectoryPath);
            }

            Console.WriteLine($"defaultRootDirectoryPath={defaultRootDirectoryPath}");

            return defaultRootDirectoryPath;
        }

        private void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private SettingsModel GetDefaultUSerSettings()
        {
            var rootDirectory = GetDefaultRootDirectory();

            var userSettings = new SettingsModel()
            {
                LogDirectory = Path.Combine(rootDirectory, DefaultLogDirectory),
                DataDirectory = Path.Combine(rootDirectory, DefaultDataDirectory),
            };

            EnsureDirectoryExists(userSettings.DataDirectory);
            EnsureDirectoryExists(userSettings.LogDirectory);

            userSettings.RaceViewBottomGrids = new List<int>()
            {
                12,
                7,
                8,
                9,
                4,
                5,
                6
            };
            userSettings.RaceViewRightGrids = new List<int>()
            {
                1,
                10,
                11,
                2
            };
            userSettings.QualifyingViewBottomGrids = new List<int>()
            {
                12,
                1
            };
            userSettings.QualifyingViewRightGrids = new List<int>();
            userSettings.PracticeViewBottomGrids = new List<int>()
            {
                12,
                7,
                8,
                9,
                4,
                5,
                6
            };
            userSettings.PracticeViewRightGrids = new List<int>()
            {
                1
            };

            SaveSettings(userSettings);

            return userSettings;
        }

        #endregion
    }
}
