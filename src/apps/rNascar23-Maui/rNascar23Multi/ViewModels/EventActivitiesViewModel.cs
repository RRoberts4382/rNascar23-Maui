using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Schedules.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public class EventActivitiesViewModel : ObservableObject, IDisposable
    {
        #region fields

        private ILogger<EventActivitiesViewModel> _logger;
        private ISchedulesRepository _schedulesRepository;

        #endregion

        #region properties

        private ObservableCollection<ActivityGroup> _models = new ObservableCollection<ActivityGroup>();
        public ObservableCollection<ActivityGroup> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        #endregion

        #region ctor

        public EventActivitiesViewModel(
            ILogger<EventActivitiesViewModel> logger,
            ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository ?? throw new ArgumentNullException(nameof(schedulesRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region public

        public async Task LoadModelsAsync(int raceId)
        {
            try
            {
                var seriesEvent = await _schedulesRepository.GetEventAsync(raceId);

                Models.Clear();

                foreach (var eventActivityGroup in seriesEvent.EventActivities.GroupBy(a => a.StartTimeLocal.ToString("dddd, MMMM d yyyy")))
                {
                    var group = new ActivityGroup(eventActivityGroup.Key);

                    foreach (var eventActivity in eventActivityGroup)
                    {
                        group.Add(new EventActivityModel()
                        {
                            SeriesId = seriesEvent.SeriesId,
                            Timestamp = eventActivity.StartTimeLocal,
                            ActivityName = eventActivity.EventName,
                            Notes = eventActivity.Notes,
                            OnTrack = eventActivity.Description
                        });
                    }

                    Models.Add(group);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EventActivitiesViewModel:LoadModelsAsync");
            }
        }

        #endregion

        #region classes

        public class ActivityGroup : List<EventActivityModel>
        {
            public string GroupName { get; set; }

            public ActivityGroup(string groupName)
                : this(groupName, new List<EventActivityModel>())
            {
            }
            public ActivityGroup(string groupName, List<EventActivityModel> activities)
                : base(activities)
            {
                GroupName = groupName;
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
                _schedulesRepository = null;
            }

            _disposed = true;
        }

        #endregion
    }
}
