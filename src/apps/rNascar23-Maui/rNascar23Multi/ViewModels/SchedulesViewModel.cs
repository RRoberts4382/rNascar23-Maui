using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LiveFeeds.Models;
using rNascar23.Sdk.LiveFeeds.Ports;
using rNascar23.Sdk.Schedules.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    partial class SchedulesViewModel : ObservableObject, IDisposable
    {
        #region fields

        private ILogger<SchedulesViewModel> _logger;
        private ISchedulesRepository _schedulesRepository;

        #endregion

        #region properties

        private SeriesEventModel _selected;
        public SeriesEventModel Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private ObservableCollection<SeriesEventModel> _models = new ObservableCollection<SeriesEventModel>();
        public ObservableCollection<SeriesEventModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _scheduleTitle = "Schedule";
        public string ScheduleTitle
        {
            get => _scheduleTitle;
            set => SetProperty(ref _scheduleTitle, value);
        }

        private ScheduleType _scheduleType = ScheduleType.ThisWeek;
        public ScheduleType ScheduleType
        {
            get => _scheduleType;
            set => SetProperty(ref _scheduleType, value);
        }

        private ActivitiesResultsTypes _activitiesResultsType = ActivitiesResultsTypes.Activities;
        public ActivitiesResultsTypes ActivitiesResultsType
        {
            get => _activitiesResultsType;
            set => SetProperty(ref _activitiesResultsType, value);
        }

        #endregion

        #region ctor

        public SchedulesViewModel(
            ILogger<SchedulesViewModel> logger,
            ISchedulesRepository schedulesRepository)
        {
            _schedulesRepository = schedulesRepository ?? throw new ArgumentNullException(nameof(schedulesRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.PropertyChanged += SchedulesViewModel_PropertyChanged;
        }

        public SchedulesViewModel()
        {
            _logger = App.serviceProvider.GetRequiredService<ILogger<SchedulesViewModel>>();
            _schedulesRepository = App.serviceProvider.GetRequiredService<ISchedulesRepository>();
        }

        #endregion

        #region public

        public async Task LoadSeriesEventModelsAsync()
        {
            try
            {
                var scheduledEvents = await _schedulesRepository.GetSchedulesAsync(ScheduleType);

                scheduledEvents = scheduledEvents.OrderBy(e => e.RaceDate).ToList();

                if (Models.Count > scheduledEvents.Count())
                {
                    for (int i = Models.Count - 1; i > scheduledEvents.Count - 1; i--)
                    {
                        Models.RemoveAt(i);
                    }
                }

                for (int i = 0; i < scheduledEvents.Count; i++)
                {
                    var seriesEvent = scheduledEvents[i];

                    if (Models.Count <= i)
                    {
                        Models.Add(new SeriesEventModel()
                        {
                            RaceId = seriesEvent.RaceId,
                            EventName = seriesEvent.RaceName,
                            SeriesId = seriesEvent.SeriesId,
                            Tv = seriesEvent.TelevisionBroadcaster == "FOX" ? TvTypes.Fox :
                                seriesEvent.TelevisionBroadcaster == "FS1" ? TvTypes.FS1 :
                                seriesEvent.TelevisionBroadcaster == "FS2" ? TvTypes.FS2 :
                                seriesEvent.TelevisionBroadcaster == "NBC" ? TvTypes.NBC :
                                seriesEvent.TelevisionBroadcaster == "NBCSN" ? TvTypes.NBCSN :
                                seriesEvent.TelevisionBroadcaster == "CNBC" ? TvTypes.CNBC :
                                seriesEvent.TelevisionBroadcaster == "USA" ? TvTypes.USA :
                                TvTypes.Unknown,
                            Radio = seriesEvent.RadioBroadcaster == "MRN" ? RadioTypes.MRN :
                                seriesEvent.RadioBroadcaster == "PRN" ? RadioTypes.PRN :
                                seriesEvent.RadioBroadcaster == "IMS" ? RadioTypes.IMS :
                                RadioTypes.Unknown,
                            SatelliteRadio = seriesEvent.SatelliteRadioBroadcaster == "SIRIUSXM" ? SatelliteTypes.Sirius :
                                SatelliteTypes.Unknown,
                            Track = scheduledEvents[i].TrackName,
                            Date = scheduledEvents[i].RaceDate.ToString("dddd, MMM d yyyy"),
                            Time = scheduledEvents[i].RaceDate.ToString("h:mm tt"),
                            Timestamp = scheduledEvents[i].RaceDate,
                            Distance = $"{scheduledEvents[i].ScheduledLaps} Laps/{scheduledEvents[i].ScheduledDistance} Miles"
                        });
                    }
                    else
                    {
                        Models[i] = new SeriesEventModel()
                        {
                            RaceId = seriesEvent.RaceId,
                            EventName = seriesEvent.RaceName,
                            SeriesId = seriesEvent.SeriesId,
                            Tv = seriesEvent.TelevisionBroadcaster == "FOX" ? TvTypes.Fox :
                                seriesEvent.TelevisionBroadcaster == "FS1" ? TvTypes.FS1 :
                                seriesEvent.TelevisionBroadcaster == "FS2" ? TvTypes.FS2 :
                                seriesEvent.TelevisionBroadcaster == "NBC" ? TvTypes.NBC :
                                seriesEvent.TelevisionBroadcaster == "NBCSN" ? TvTypes.NBCSN :
                                seriesEvent.TelevisionBroadcaster == "CNBC" ? TvTypes.CNBC :
                                seriesEvent.TelevisionBroadcaster == "USA" ? TvTypes.USA :
                                TvTypes.Unknown,
                            Radio = seriesEvent.RadioBroadcaster == "MRN" ? RadioTypes.MRN :
                                seriesEvent.RadioBroadcaster == "PRN" ? RadioTypes.PRN :
                                seriesEvent.RadioBroadcaster == "IMS" ? RadioTypes.IMS :
                                RadioTypes.Unknown,
                            SatelliteRadio = seriesEvent.SatelliteRadioBroadcaster == "SIRIUSXM" ? SatelliteTypes.Sirius :
                                SatelliteTypes.Unknown,
                            Track = scheduledEvents[i].TrackName,
                            Date = scheduledEvents[i].RaceDate.ToString("dddd, MMM d yyyy"),
                            Time = scheduledEvents[i].RaceDate.ToString("h:mm tt"),
                            Timestamp = scheduledEvents[i].RaceDate,
                            Distance = $"{scheduledEvents[i].ScheduledLaps} Laps/{scheduledEvents[i].ScheduledDistance} Miles"
                        };
                    }
                }

                if (Models.Count > 0)
                {
                    Models[0].IsSelected = true;
                    Selected = Models[0];
                }

                SetTitle(ScheduleType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SeriesEventViewModel:LoadModelsAsync");
            }
        }

        #endregion

        #region private

        private async void SchedulesViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ScheduleType))
            {
                await LoadSeriesEventModelsAsync();
            }
        }

        [RelayCommand]
        private async Task InitAsync()
        {
            try
            {
                await LoadSeriesEventModelsAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SeriesEventViewModel:InitAsync");
            }
        }

        private void SetTitle(ScheduleType scheduleType)
        {
            switch (scheduleType)
            {
                case ScheduleType.Cup:
                    ScheduleTitle = "NASCAR Cup Series Schedule";
                    break;
                case ScheduleType.Xfinity:
                    ScheduleTitle = "NASCAR Xfinity Series Schedule";
                    break;
                case ScheduleType.Trucks:
                    ScheduleTitle = "NASCAR Craftsman Truck Series Schedule";
                    break;
                case ScheduleType.All:
                    ScheduleTitle = "Schedule (All Series)";
                    break;
                case ScheduleType.ThisWeek:
                    ScheduleTitle = "This Week's Schedule";
                    break;
                case ScheduleType.NextWeek:
                    ScheduleTitle = "Next Week's Schedule";
                    break;
                case ScheduleType.Today:
                    ScheduleTitle = "Today's Schedule";
                    break;
                default:
                    ScheduleTitle = "Schedule";
                    break;
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
            // free native resources if there are any.
        }

        #endregion
    }
}
