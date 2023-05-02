using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Ports;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    internal class VerticalGridViewModel : ObservableObject
    {
        private readonly ILogger<VerticalGridViewModel> _logger;
        private readonly ISettingsRepository _settingsRepository;
        private readonly UpdateNotificationHandler _updateHandler;

        private EventViewType _viewType;
        public EventViewType ViewType
        {
            get
            {
                return _viewType;
            }
            set
            {
                _viewType = value;
                LoadFromSource();
            }
        }

        ObservableCollection<GridViewModel> _models = new ObservableCollection<GridViewModel>();
        public ObservableCollection<GridViewModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        public Guid id;

        public VerticalGridViewModel()
        {
            id = Guid.NewGuid();

            _logger = App.serviceProvider.GetRequiredService<ILogger<VerticalGridViewModel>>();

            _logger.LogInformation($"id:{id}; VerticalGridViewModel - ctor");

            _settingsRepository = App.serviceProvider.GetService<ISettingsRepository>();

            _updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

            _updateHandler.UserSettingsUpdated += _updateHandler_UserSettingsUpdated;

            LoadFromSource();
        }

        public VerticalGridViewModel(EventViewType viewType)
        {
            id = Guid.NewGuid();

            _logger = App.serviceProvider.GetRequiredService<ILogger<VerticalGridViewModel>>();

            _logger.LogInformation($"id:{id}; VerticalGridViewModel - ctor(EventViewType viewType)");

            ViewType = viewType;

            _settingsRepository = App.serviceProvider.GetService<ISettingsRepository>();

            _updateHandler = App.serviceProvider.GetService<UpdateNotificationHandler>();

            _updateHandler.UserSettingsUpdated += _updateHandler_UserSettingsUpdated;

            LoadFromSource();
        }

        private void _updateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
        {
            LoadFromSource();
        }

        protected virtual void LoadFromSource()
        {
            if (_settingsRepository == null)
                return;

            Models.Clear();

            var settings = _settingsRepository.GetSettings();

            var viewIdList = ViewType == EventViewType.Race ? settings.RaceViewRightGrids :
                ViewType == EventViewType.Qualifying ? settings.QualifyingViewRightGrids :
                    settings.PracticeViewRightGrids;

            foreach (int gridTypeId in viewIdList)
            {
                Models.Add(new GridViewModel()
                {
                    ViewType = (GridViewTypes)gridTypeId
                });
            }
        }
    }
}
