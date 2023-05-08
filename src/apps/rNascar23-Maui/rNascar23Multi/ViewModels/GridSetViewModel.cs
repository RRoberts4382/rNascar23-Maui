using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Models;
using rNascar23Multi.Settings.Ports;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace rNascar23Multi.ViewModels
{
    internal class GridSetViewModel : ObservableObject, INotifySettingsChanged, IDisposable
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

        public void UserSettingsUpdated(SettingsModel settings)
        {
            try
            {
                LoadFromSource(settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GridSetViewModel:UserSettingsUpdatedAsync");
            }
        }

        #endregion

        #region protected

        protected virtual void LoadFromSource()
        {
            if (_settingsRepository == null)
                return;

            var settings = _settingsRepository.GetSettings();

            LoadFromSource(settings);
        }

        protected virtual void LoadFromSource(SettingsModel settings)
        {
            try
            {
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

                IList<int> indexesToRemove = new List<int>();
                for (int i = 0; i < Models.Count; i++)
                {
                    GridViewModel existing = Models[i];

                    if (!gridViews.Any(g => g.ViewType == existing.ViewType))
                    {
                        indexesToRemove.Add(i);
                    }
                }

                for (int i = indexesToRemove.Count - 1; i >= 0; i--)
                {
                    Models.RemoveAt(indexesToRemove[i]);
                }

                foreach (GridViewModel gridView in gridViews)
                {
                    if (!Models.Any(g => g.ViewType == gridView.ViewType))
                    {
                        Models.Add(gridView);
                    }
                }
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

            _disposed = true;
        }

        #endregion
    }
}
