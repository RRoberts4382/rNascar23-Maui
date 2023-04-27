using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Ports;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    internal class HorizontalGridViewModel : ObservableObject, IViewTypeViewModel
    {
        #region fields

        private readonly ILogger<HorizontalGridViewModel> _logger;
        private readonly ISettingsRepository _settingsRepository;
        private readonly UpdateNotificationHandler _updateHandler;

        #endregion

        #region properties

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

        #endregion

        #region ctor

        public HorizontalGridViewModel(
            ILogger<HorizontalGridViewModel> logger,
            ISettingsRepository settingsRepository,
            UpdateNotificationHandler updateHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));

            _updateHandler.UserSettingsUpdated += _updateHandler_UserSettingsUpdated;
        }

        #endregion

        #region protected

        protected virtual void _updateHandler_UserSettingsUpdated(object sender, Settings.Models.SettingsModel e)
        {
            try
            {
                LoadFromSource();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HorizontalGridViewModel.UserSettingsUpdated");
            }
        }

        protected virtual void LoadFromSource()
        {
            try
            {
                if (_settingsRepository == null)
                    return;

                var settings = _settingsRepository.GetSettings();

                var viewIdList = ViewType == EventViewType.Race ? settings.RaceViewBottomGrids :
                    ViewType == EventViewType.Qualifying ? settings.QualifyingViewBottomGrids :
                        settings.PracticeViewBottomGrids;

                IList<GridViewModel> gridViews = new List<GridViewModel>();

                foreach (int gridTypeId in viewIdList)
                {
                    var viewType = (GridViewTypes)gridTypeId;

                    gridViews.Add(new GridViewModel()
                    {
                        ViewType = viewType
                    });
                }

                Models = new ObservableCollection<GridViewModel>(gridViews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HorizontalGridViewModel.LoadFromSource");
            }
        }

        #endregion
    }
}
