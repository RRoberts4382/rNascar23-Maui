using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Ports;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace rNascar23Multi.ViewModels
{
    internal class GridSetViewModel : ObservableObject, INotifyUpdateTarget, IDisposable
    {
        #region fields

        private ILogger<GridSetViewModel> _logger;
        private ISettingsRepository _settingsRepository;

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
                if (_viewType != value)
                {
                    _viewType = value;
                    LoadFromSource();
                }
            }
        }

        public GridOrientationType GridOrientation { get; set; }

        ObservableCollection<GridViewModel> _models = new ObservableCollection<GridViewModel>();
        public ObservableCollection<GridViewModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        #endregion

        #region ctor

        public GridSetViewModel(
            ILogger<GridSetViewModel> logger,
            ISettingsRepository settingsRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));
        }

        #endregion

        #region public

        public async Task UserSettingsUpdatedAsync()
        {
            try
            {
                LoadFromSource();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GridSetViewModel:UserSettingsUpdatedAsync");
            }
        }

        public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GridSetViewModel:UpdateTimer_UpdateTimerElapsed");
            }
        }

        #endregion

        #region protected

        protected virtual void LoadFromSource()
        {
            try
            {
                if (_settingsRepository == null)
                    return;

                var settings = _settingsRepository.GetSettings();

                IList<int> viewIdList = null;

                if (GridOrientation == GridOrientationType.Horizontal)
                {
                    viewIdList = ViewType == EventViewType.Race ? settings.RaceViewBottomGrids :
                        ViewType == EventViewType.Qualifying ? settings.QualifyingViewBottomGrids :
                            settings.PracticeViewBottomGrids;
                }
                else if (GridOrientation == GridOrientationType.Vertical)
                {
                    viewIdList = ViewType == EventViewType.Race ? settings.RaceViewRightGrids :
                        ViewType == EventViewType.Qualifying ? settings.QualifyingViewRightGrids :
                            settings.PracticeViewRightGrids;
                }

                IList<GridViewModel> gridViews = new List<GridViewModel>();

                foreach (int gridTypeId in viewIdList)
                {
                    var viewType = (GridViewTypes)gridTypeId;

                    gridViews.Add(new GridViewModel()
                    {
                        ViewType = viewType
                    });
                }

                //Models.Clear();

                //foreach (GridViewModel gridView in gridViews)
                //{
                //    Models.Add(gridView);
                //}

                Models = new ObservableCollection<GridViewModel>(gridViews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GridSetViewModel.LoadFromSource");
            }
        }

        #endregion

        #region IDisposable

        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _logger = null;
                _settingsRepository = null;
            }
            // free native resources if there are any.
        }

        #endregion
    }
}
