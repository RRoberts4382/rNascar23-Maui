using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.LoopData.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Resources.Styles;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.Settings.Ports;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly ILogger<SettingsViewModel> _logger;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IDriverInfoRepository _driverInfoRepository;
        private readonly UpdateNotificationHandler _updateHandler;

        private SettingsModel _model;
        public SettingsModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        ObservableCollection<GridSelectionModel> _raceRightGrids = new ObservableCollection<GridSelectionModel>();
        public ObservableCollection<GridSelectionModel> RaceRightGrids
        {
            get => _raceRightGrids;
            set => SetProperty(ref _raceRightGrids, value);
        }

        ObservableCollection<GridSelectionModel> _raceBottomGrids = new ObservableCollection<GridSelectionModel>();
        public ObservableCollection<GridSelectionModel> RaceBottomGrids
        {
            get => _raceBottomGrids;
            set => SetProperty(ref _raceBottomGrids, value);
        }

        ObservableCollection<GridSelectionModel> _qualifyingRightGrids = new ObservableCollection<GridSelectionModel>();
        public ObservableCollection<GridSelectionModel> QualifyingRightGrids
        {
            get => _qualifyingRightGrids;
            set => SetProperty(ref _qualifyingRightGrids, value);
        }

        ObservableCollection<GridSelectionModel> _qualifyingBottomGrids = new ObservableCollection<GridSelectionModel>();
        public ObservableCollection<GridSelectionModel> QualifyingBottomGrids
        {
            get => _qualifyingBottomGrids;
            set => SetProperty(ref _qualifyingBottomGrids, value);
        }

        ObservableCollection<GridSelectionModel> _practiceRightGrids = new ObservableCollection<GridSelectionModel>();
        public ObservableCollection<GridSelectionModel> PracticeRightGrids
        {
            get => _practiceRightGrids;
            set => SetProperty(ref _practiceRightGrids, value);
        }

        ObservableCollection<GridSelectionModel> _practicingBottomGrids = new ObservableCollection<GridSelectionModel>();
        public ObservableCollection<GridSelectionModel> PracticeBottomGrids
        {
            get => _practicingBottomGrids;
            set => SetProperty(ref _practicingBottomGrids, value);
        }

        ObservableCollection<FavoriteDriverSelectionModel> _favoriteDrivers = new ObservableCollection<FavoriteDriverSelectionModel>();
        public ObservableCollection<FavoriteDriverSelectionModel> FavoriteDrivers
        {
            get => _favoriteDrivers;
            set => SetProperty(ref _favoriteDrivers, value);
        }

        public SettingsViewModel()
        {
            _logger = App.serviceProvider.GetService<ILogger<SettingsViewModel>>();
            _settingsRepository = App.serviceProvider.GetService<ISettingsRepository>();
            _updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

            LoadModelAsync();
        }

        public SettingsViewModel(
            ILogger<SettingsViewModel> logger,
            ISettingsRepository settingsRepository,
            IDriverInfoRepository driverInfoRepository,
            UpdateNotificationHandler updateHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
            _driverInfoRepository = driverInfoRepository ?? throw new ArgumentNullException(nameof(driverInfoRepository));
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));

            Shell.Current.Navigating += Current_Navigating;
        }

        [RelayCommand]
        private async Task InitAsync()
        {
            try
            {
                await LoadModelAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SettingsViewModel:InitAsync");
            }
        }

        private void Current_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            if (e.Current.Location.ToString() == "//Settings")
            {
                SaveModel();
            }
        }

        public void SaveModel()
        {
            Model.RaceViewRightGrids = RaceRightGrids.Where(g => g.Selected).Select(g => (int)g.GridViewType).ToList();
            Model.RaceViewBottomGrids = RaceBottomGrids.Where(g => g.Selected).Select(g => (int)g.GridViewType).ToList();

            Model.QualifyingViewRightGrids = QualifyingRightGrids.Where(g => g.Selected).Select(g => (int)g.GridViewType).ToList();
            Model.QualifyingViewBottomGrids = QualifyingBottomGrids.Where(g => g.Selected).Select(g => (int)g.GridViewType).ToList();

            Model.PracticeViewRightGrids = PracticeRightGrids.Where(g => g.Selected).Select(g => (int)g.GridViewType).ToList();
            Model.PracticeViewBottomGrids = PracticeBottomGrids.Where(g => g.Selected).Select(g => (int)g.GridViewType).ToList();

            Model.FavoriteDrivers = FavoriteDrivers.Where(d => d.Selected).Select(d => d.DriverName).ToList();

            _settingsRepository.SaveSettings(Model);

            _updateHandler.UpdateUserSettings(Model);

            UpdateTheme();
        }

        public void UpdateTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                if (Model.UseDarkTheme)
                {
                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    mergedDictionaries.Clear();
                    mergedDictionaries.Add(new LightTheme());
                }
            }
        }

        private async Task LoadModelAsync()
        {
            try
            {
                Model = _settingsRepository.GetSettings();

                Model.PropertyChanged += Model_PropertyChanged;

                var raceBottomGrids = LoadGridSelectionModels();
                var raceRightGrids = LoadGridSelectionModels();
                var qualifyingBottomGrids = LoadGridSelectionModels();
                var qualifyingRightGrids = LoadGridSelectionModels();
                var practiceBottomGrids = LoadGridSelectionModels();
                var practiceRightGrids = LoadGridSelectionModels();

                RaceBottomGrids = new ObservableCollection<GridSelectionModel>(raceBottomGrids);
                foreach (int selectedGridId in Model.RaceViewBottomGrids)
                {
                    var grid = RaceBottomGrids.FirstOrDefault(g => (int)g.GridViewType == selectedGridId);

                    if (grid != null)
                        grid.Selected = true;
                }

                RaceRightGrids = new ObservableCollection<GridSelectionModel>(raceRightGrids);
                foreach (int selectedGridId in Model.RaceViewRightGrids)
                {
                    var grid = RaceRightGrids.FirstOrDefault(g => (int)g.GridViewType == selectedGridId);

                    if (grid != null)
                        grid.Selected = true;
                }

                QualifyingBottomGrids = new ObservableCollection<GridSelectionModel>(qualifyingBottomGrids);
                foreach (int selectedGridId in Model.QualifyingViewBottomGrids)
                {
                    var grid = QualifyingBottomGrids.FirstOrDefault(g => (int)g.GridViewType == selectedGridId);

                    if (grid != null)
                        grid.Selected = true;
                }

                QualifyingRightGrids = new ObservableCollection<GridSelectionModel>(qualifyingRightGrids);
                foreach (int selectedGridId in Model.QualifyingViewRightGrids)
                {
                    var grid = QualifyingRightGrids.FirstOrDefault(g => (int)g.GridViewType == selectedGridId);

                    if (grid != null)
                        grid.Selected = true;
                }

                PracticeBottomGrids = new ObservableCollection<GridSelectionModel>(practiceBottomGrids);
                foreach (int selectedGridId in Model.PracticeViewBottomGrids)
                {
                    var grid = PracticeBottomGrids.FirstOrDefault(g => (int)g.GridViewType == selectedGridId);

                    if (grid != null)
                        grid.Selected = true;
                }

                PracticeRightGrids = new ObservableCollection<GridSelectionModel>(practiceRightGrids);
                foreach (int selectedGridId in Model.PracticeViewRightGrids)
                {
                    var grid = PracticeRightGrids.FirstOrDefault(g => (int)g.GridViewType == selectedGridId);

                    if (grid != null)
                        grid.Selected = true;
                }

                var driverModels = await LoadFavoriteDriverModelsAsync();
                FavoriteDrivers = new ObservableCollection<FavoriteDriverSelectionModel>(driverModels);
                foreach (string driver in Model.FavoriteDrivers)
                {
                    var grid = FavoriteDrivers.FirstOrDefault(d => d.DriverName == driver);

                    if (grid != null)
                        grid.Selected = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SettingsViewModel:LoadModel");
            }
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.NotifyPropertyChanged("Model");
        }

        private List<GridSelectionModel> LoadGridSelectionModels()
        {
            try
            {
                var models = new List<GridSelectionModel>();

                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.FastestLaps,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.LapLeaders,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Movers,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Fallers,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.KeyMoments,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Flags,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Points,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.StagePoints,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Last5Laps,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Last10Laps,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Last15Laps,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Best5Laps,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Best10Laps,
                });
                models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Best15Laps,
                });

                return models;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GridSelectionViewModel:LoadModels");
            }

            return new List<GridSelectionModel>();
        }

        private async Task<List<FavoriteDriverSelectionModel>> LoadFavoriteDriverModelsAsync()
        {
            try
            {
                var drivers = await _driverInfoRepository.GetDriversAsync();

                return drivers.Select(d => new FavoriteDriverSelectionModel() { DriverName = d.Name }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GridSelectionViewModel:LoadFavoriteDriverModels");
            }

            return new List<FavoriteDriverSelectionModel>();
        }
    }
}
