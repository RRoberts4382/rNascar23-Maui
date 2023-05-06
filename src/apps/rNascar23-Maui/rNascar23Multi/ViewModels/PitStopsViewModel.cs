using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Flags.Models;
using rNascar23.Sdk.Flags.Ports;
using rNascar23.Sdk.PitStops.Models;
using rNascar23.Sdk.PitStops.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public partial class PitStopsViewModel : ObservableObject, INotifyUpdateTarget, IDisposable
    {
        #region consts

        private const string DefaultAllDriversHeader = "Pit Stops";

        #endregion

        #region fields

        private ILogger<PitStopsViewModel> _logger;
        private IPitStopsRepository _pitStopsRepository;
        private IFlagStateRepository _flagStateRepository;
        IList<PitStopAverages> _averages = new List<PitStopAverages>();

        #endregion

        #region properties

        public SeriesTypes SeriesId { get; set; }

        public int RaceId { get; set; }

        private PitStopsAllDriversModel _selected;
        public PitStopsAllDriversModel Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private ObservableCollection<PitStopsAllDriversModel> _allDriversPitStops = new ObservableCollection<PitStopsAllDriversModel>();
        public ObservableCollection<PitStopsAllDriversModel> AllDriversPitStops
        {
            get => _allDriversPitStops;
            set => SetProperty(ref _allDriversPitStops, value);
        }

        private ObservableCollection<PitStopsAllDriversModel> _driverPitStops = new ObservableCollection<PitStopsAllDriversModel>();
        public ObservableCollection<PitStopsAllDriversModel> DriverPitStops
        {
            get => _driverPitStops;
            set => SetProperty(ref _driverPitStops, value);
        }

        private string _allDriverPitStopsHeader = $"All {DefaultAllDriversHeader}";
        public string AllDriverPitStopsHeader
        {
            get => _allDriverPitStopsHeader;
            set => SetProperty(ref _allDriverPitStopsHeader, value);
        }

        private string _driverPitStopsName = "Driver Pit Stops";
        public string DriverPitStopsName
        {
            get => _driverPitStopsName;
            set => SetProperty(ref _driverPitStopsName, value);
        }

        private DriverPitStopStatisticsModel _driverStatistics = new DriverPitStopStatisticsModel();
        public DriverPitStopStatisticsModel DriverStatistics
        {
            get => _driverStatistics;
            set => SetProperty(ref _driverStatistics, value);
        }

        private ObservableCollection<PositionDriverValueModel> _totalGainLoss = new ObservableCollection<PositionDriverValueModel>();
        public ObservableCollection<PositionDriverValueModel> TotalGainLoss
        {
            get => _totalGainLoss;
            set => SetProperty(ref _totalGainLoss, value);
        }

        private ObservableCollection<PositionDriverValueModel> _averagePitTime = new ObservableCollection<PositionDriverValueModel>();
        public ObservableCollection<PositionDriverValueModel> AveragePitTime
        {
            get => _averagePitTime;
            set => SetProperty(ref _averagePitTime, value);
        }

        private ObservableCollection<PositionDriverValueModel> _averageInOutTime = new ObservableCollection<PositionDriverValueModel>();
        public ObservableCollection<PositionDriverValueModel> AverageInOutTime
        {
            get => _averageInOutTime;
            set => SetProperty(ref _averageInOutTime, value);
        }

        private ObservableCollection<PositionDriverValueModel> _averageTotalTime = new ObservableCollection<PositionDriverValueModel>();
        public ObservableCollection<PositionDriverValueModel> AverageTotalTime
        {
            get => _averageTotalTime;
            set => SetProperty(ref _averageTotalTime, value);
        }

        private ObservableCollection<PositionDriverValueModel> _greenPitTime = new ObservableCollection<PositionDriverValueModel>();
        public ObservableCollection<PositionDriverValueModel> GreenPitTime
        {
            get => _greenPitTime;
            set => SetProperty(ref _greenPitTime, value);
        }

        private ObservableCollection<PositionDriverValueModel> _greenInOutTime = new ObservableCollection<PositionDriverValueModel>();
        public ObservableCollection<PositionDriverValueModel> GreenInOutTime
        {
            get => _greenInOutTime;
            set => SetProperty(ref _greenInOutTime, value);
        }

        private ObservableCollection<PositionDriverValueModel> _greenTotalTime = new ObservableCollection<PositionDriverValueModel>();
        public ObservableCollection<PositionDriverValueModel> GreenTotalTime
        {
            get => _greenTotalTime;
            set => SetProperty(ref _greenTotalTime, value);
        }

        // range selection lists
        // filter by laps
        private ObservableCollection<int> _startLaps = new ObservableCollection<int>();
        public ObservableCollection<int> StartLaps
        {
            get => _startLaps;
            set => SetProperty(ref _startLaps, value);
        }

        private int? _startLap;
        public int? StartLap
        {
            get => _startLap;
            set => SetProperty(ref _startLap, value);
        }

        private ObservableCollection<int> _endLaps = new ObservableCollection<int>();
        public ObservableCollection<int> EndLaps
        {
            get => _endLaps;
            set => SetProperty(ref _endLaps, value);
        }

        private int? _endLap;
        public int? EndLap
        {
            get => _endLap;
            set => SetProperty(ref _endLap, value);
        }

        // filter by driver
        private ObservableCollection<DriverRangeModel> _driverList = new ObservableCollection<DriverRangeModel>();
        public ObservableCollection<DriverRangeModel> DriverList
        {
            get => _driverList;
            set => SetProperty(ref _driverList, value);
        }

        private DriverRangeModel _selectedDriver;
        public DriverRangeModel SelectedDriver
        {
            get => _selectedDriver;
            set => SetProperty(ref _selectedDriver, value);
        }

        // filter by caution
        private ObservableCollection<CautionRangeModel> _cautionsList = new ObservableCollection<CautionRangeModel>();
        public ObservableCollection<CautionRangeModel> CautionsList
        {
            get => _cautionsList;
            set => SetProperty(ref _cautionsList, value);
        }

        private CautionRangeModel _selectedCaution;
        public CautionRangeModel SelectedCaution
        {
            get => _selectedCaution;
            set => SetProperty(ref _selectedCaution, value);
        }

        #endregion

        #region ctor

        public PitStopsViewModel(
            ILogger<PitStopsViewModel> logger,
            IPitStopsRepository pitStopsRepository,
            IFlagStateRepository flagStateRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _pitStopsRepository = pitStopsRepository ?? throw new ArgumentNullException(nameof(pitStopsRepository));

            _flagStateRepository = flagStateRepository ?? throw new ArgumentNullException(nameof(flagStateRepository));
        }

        public PitStopsViewModel()
        {
            _logger = App.serviceProvider.GetRequiredService<ILogger<PitStopsViewModel>>();

            _pitStopsRepository = App.serviceProvider.GetRequiredService<IPitStopsRepository>();

            _flagStateRepository = App.serviceProvider.GetRequiredService<IFlagStateRepository>();
        }

        #endregion

        #region public

        public async Task UserSettingsUpdatedAsync()
        {
            try
            {
                await LoadAllDriversPitStopsAsync(SeriesId, RaceId);

                await UpdateAveragesAsync();

                if (Selected != null)
                {
                    await UpdateSelectedDriverAsync(Selected);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UserSettingsUpdatedAsync");
            }
        }

        public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
        {
            try
            {
                if (e != null && e.SessionDetails != null)
                {
                    if (SeriesId != (SeriesTypes)e.SessionDetails.SeriesId || RaceId != e.SessionDetails.RaceId)
                    {
                        SeriesId = (SeriesTypes)e.SessionDetails.SeriesId;
                        RaceId = e.SessionDetails.RaceId;

                        await LoadAllDriversPitStopsAsync(SeriesId, RaceId);

                        await UpdateAveragesAsync();

                        if (Selected != null)
                        {
                            await UpdateSelectedDriverAsync(Selected);
                        }

                        await UpdateRangeSelectionListsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateTimerElapsedAsync");
            }
        }

        public async Task UpdateSelectedDriverAsync(PitStopsAllDriversModel selectedDriver)
        {
            try
            {
                await LoadDriverPitStopsAsync(SeriesId, RaceId, selectedDriver?.CarNumber, selectedDriver?.Driver);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateSelectedDriverAsync");
            }
        }

        public async Task UpdateByLapsAsync()
        {
            try
            {
                SelectedCaution = null;
                SelectedDriver = null;

                var start = StartLap.HasValue ? StartLap.Value : 1;
                var end = EndLap.HasValue ? EndLap.Value : EndLaps.Max();

                AllDriverPitStopsHeader = $"{DefaultAllDriversHeader} - Laps {start} to {end}";

                await LoadAllDriversPitStopsAsync(SeriesId, RaceId, StartLap, EndLap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateByLapsAsync");
            }
        }

        public async Task UpdateByCautionAsync()
        {
            try
            {
                if (SelectedCaution != null)
                {
                    StartLap = null;
                    EndLap = null;
                    SelectedDriver = null;

                    AllDriverPitStopsHeader = $"{DefaultAllDriversHeader} - Caution {SelectedCaution.Title}";

                    await LoadAllDriversPitStopsAsync(SeriesId, RaceId, SelectedCaution.StartLap, SelectedCaution.EndLap);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateByCautionAsync");
            }
        }

        public async Task UpdateByDriverAsync()
        {
            try
            {
                if (SelectedDriver != null)
                {
                    StartLap = null;
                    EndLap = null;
                    SelectedCaution = null;

                    AllDriverPitStopsHeader = $"{DefaultAllDriversHeader} - {SelectedDriver.Name}";

                    await LoadAllDriversPitStopsAsync(SeriesId, RaceId, null, null, SelectedDriver.CarNumber);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateByDriverAsync");
            }
        }

        public async Task AllPitStopsAsync()
        {
            try
            {
                StartLap = null;
                EndLap = null;
                SelectedCaution = null;
                SelectedDriver = null;

                AllDriverPitStopsHeader = $"All {DefaultAllDriversHeader}";

                await LoadAllDriversPitStopsAsync(SeriesId, RaceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:AllPitStopsAsync");
            }
        }

        #endregion

        #region private

        private async Task LoadAllDriversPitStopsAsync(
            SeriesTypes seriesId,
            int raceId,
            int? startLap = null,
            int? endLap = null,
            string carNumber = null)
        {
            try
            {
                AllDriversPitStops.Clear();

                IList<PitStopsAllDriversModel> allDriversPitStops = new List<PitStopsAllDriversModel>();

                var driverPitStops = await _pitStopsRepository.GetPitStopsInRangeAsync(
                    seriesId,
                    raceId,
                    startLap,
                    endLap,
                    carNumber);

                foreach (var pitStop in driverPitStops)
                {
                    var allDriversPitStop = new PitStopsAllDriversModel()
                    {
                        Position = pitStop.PitOutRank,
                        CarNumber = pitStop.VehicleNumber,
                        Driver = pitStop.DriverName,
                        Lap = pitStop.LapCount,
                        Flag = pitStop.PitInFlagStatus,
                        Tires = pitStop.PitStopType,
                        TotalTime = pitStop.TotalDuration,
                        PitTime = pitStop.PitStopDuration,
                        PositionIn = pitStop.PitInRank,
                        PositionOut = pitStop.PitOutRank,
                        PositionDelta = pitStop.PositionsGainedLost
                    };

                    allDriversPitStops.Add(allDriversPitStop);
                }

                AllDriversPitStops = new ObservableCollection<PitStopsAllDriversModel>(allDriversPitStops.OrderBy(p => p.Position).ToList());

                if (AllDriversPitStops.Count > 0)
                {
                    var selectedDriver = AllDriversPitStops[0];

                    selectedDriver.IsSelected = true;

                    Selected = selectedDriver;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:LoadAllDriversPitStopsAsync");
            }
        }

        private async Task UpdateRangeSelectionListsAsync()
        {
            try
            {
                await LoadCautionRangesAsync();

                var pitStops = await _pitStopsRepository.GetPitStopsAsync(SeriesId, RaceId);

                var maxLap = pitStops.Max(p => p.LeaderLap);

                StartLaps.Clear();
                EndLaps.Clear();

                for (int i = 1; i <= maxLap; i++)
                {
                    StartLaps.Add(i);
                    EndLaps.Add(i);
                }

                DriverList.Clear();

                var driverSelectionList = pitStops.
                    GroupBy(d => new { d.DriverName, d.VehicleNumber }).
                    Select(g => new DriverRangeModel()
                    {
                        Name = g.Key.DriverName,
                        CarNumber = g.Key.VehicleNumber
                    }).
                    OrderBy(d => d.Name).
                    ToList();

                foreach (var item in driverSelectionList)
                {
                    DriverList.Add(item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateRangeSelectionLists");
            }
        }

        private async Task LoadCautionRangesAsync()
        {
            try
            {
                var flagStates = await _flagStateRepository.GetFlagStatesAsync();

                if (flagStates.Count() == 0)
                    return;

                var orderedFlagStates = flagStates.
                    Where(f => f.State == FlagColors.Green || f.State == FlagColors.Yellow).
                    OrderByDescending(f => f.LapNumber);

                var currentFlagState = orderedFlagStates.FirstOrDefault();

                if (currentFlagState != null)
                {
                    if (currentFlagState.State == FlagColors.Green)
                    {
                        var endOfLastCaution = currentFlagState;

                        var beginningOfLastCaution = orderedFlagStates.
                            Where(f => f.LapNumber < endOfLastCaution.LapNumber).
                            FirstOrDefault(f => f.State == FlagColors.Yellow);

                        _startLap = beginningOfLastCaution == null ? 0 : beginningOfLastCaution.LapNumber;

                        _endLap = endOfLastCaution.LapNumber;
                    }
                    else if (currentFlagState.State == FlagColors.Yellow)
                    {
                        var beginningOfLastCaution = orderedFlagStates.FirstOrDefault(f => f.State == FlagColors.Green);

                        _startLap = beginningOfLastCaution.LapNumber + 1;

                        _endLap = currentFlagState.LapNumber;
                    }
                }

                PopulateCautionList(flagStates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:LoadCautionRangesAsync");
            }
        }

        private void PopulateCautionList(IEnumerable<FlagState> flagStates)
        {
            var previouslySelectedCaution = SelectedCaution;

            IList<CautionRangeModel> cautionModels = new List<CautionRangeModel>();
            CautionRangeModel cautionViewModel = null;

            foreach (FlagState flagState in flagStates)
            {
                if (flagState.State == FlagColors.Green && cautionViewModel != null)
                {
                    // end of caution
                    cautionViewModel.EndLap = flagState.LapNumber;
                    cautionViewModel.CautionNumber = cautionModels.Count + 1;

                    cautionModels.Add(cautionViewModel);

                    cautionViewModel = null;
                }
                else if (flagState.State == FlagColors.Yellow)
                {
                    // start of caution
                    cautionViewModel = new CautionRangeModel()
                    {
                        StartLap = flagState.LapNumber,
                        Comment = flagState.Comment
                    };
                }
            }

            CautionsList.Clear();

            foreach (var item in cautionModels.OrderBy(c => c.CautionNumber))
            {
                CautionsList.Add(item);
            }

            if (previouslySelectedCaution != null)
            {
                SelectedCaution = cautionModels.FirstOrDefault(c => c.CautionNumber == previouslySelectedCaution.CautionNumber);
            }
        }

        private async Task UpdateAveragesAsync()
        {
            try
            {
                var pitStops = await _pitStopsRepository.GetPitStopsAsync(SeriesId, RaceId);

                _averages = GetPitStopAverages(pitStops);

                LoadTotalGainLoss(_averages);
                LoadAveragePitTime(_averages);
                LoadAverageInOutTime(_averages);
                LoadAverageTotalTime(_averages);
                LoadGreenPitTime(_averages);
                LoadGreenInOutTime(_averages);
                LoadGreenTotalTime(_averages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateAveragesAsync");
            }
        }

        private async Task LoadDriverPitStopsAsync(SeriesTypes seriesId, int raceId, string carNumber, string driverName)
        {
            try
            {
                DriverPitStops.Clear();

                IList<PitStopsAllDriversModel> allDriversPitStops = new List<PitStopsAllDriversModel>();

                var driverPitStops = await _pitStopsRepository.GetPitStopsInRangeAsync(seriesId, raceId, null, null, carNumber);

                foreach (var pitStop in driverPitStops)
                {
                    var allDriversPitStop = new PitStopsAllDriversModel()
                    {
                        Lap = pitStop.LapCount,
                        Tires = pitStop.PitStopType,
                        TotalTime = pitStop.TotalDuration,
                        PitTime = pitStop.PitStopDuration,
                        Flag = pitStop.PitInFlagStatus,
                        PositionIn = pitStop.PitInRank,
                        PositionOut = pitStop.PitOutRank,
                        PositionDelta = pitStop.PositionsGainedLost
                    };

                    allDriversPitStops.Add(allDriversPitStop);
                }

                DriverPitStops = new ObservableCollection<PitStopsAllDriversModel>(allDriversPitStops.ToList());

                DriverPitStopsName = $"Driver Pit Stops - {carNumber} {driverName}";

                DriverStatistics = new DriverPitStopStatisticsModel();

                if (driverPitStops.Count() > 0)
                {
                    DriverStatistics.NumberOfStops = driverPitStops.Count();
                    DriverStatistics.AverageGainLoss = driverPitStops.Average(p => p.PositionsGainedLost);
                    DriverStatistics.AveragePitTime = driverPitStops.Average(p => p.PitStopDuration);
                    DriverStatistics.AverageTotalTime = driverPitStops.Average(p => p.TotalDuration);
                    DriverStatistics.AverageInOutTime = driverPitStops.Average(p => (p.InTravelDuration + p.OutTravelDuration));

                    if (_averages != null && _averages.Count > 0)
                    {
                        var carAverages = _averages.FirstOrDefault(a => a.CarNumber == carNumber);

                        DriverStatistics.AverageGainLossRank = _averages.OrderByDescending(a => a.TotalGainLoss).ToList().IndexOf(carAverages) + 1;
                        DriverStatistics.AveragePitTimeRank = _averages.OrderBy(a => a.AveragePitTime).ToList().IndexOf(carAverages) + 1;
                        DriverStatistics.AverageTotalTimeRank = _averages.OrderBy(a => a.AverageTotalTime).ToList().IndexOf(carAverages) + 1;
                        DriverStatistics.AverageInOutTimeRank = _averages.OrderBy(a => a.AverageInOutTime).ToList().IndexOf(carAverages) + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:LoadDriverPitStopsAsync");
            }
        }

        private void LoadTotalGainLoss(IList<PitStopAverages> averages)
        {
            TotalGainLoss.Clear();

            IList<PositionDriverValueModel> pitStopStats = new List<PositionDriverValueModel>();

            int i = 1;
            foreach (var item in averages.OrderByDescending(a => a.TotalGainLoss))
            {
                pitStopStats.Add(new PositionDriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.TotalGainLoss
                });

                i++;
            }

            TotalGainLoss = new ObservableCollection<PositionDriverValueModel>(pitStopStats.ToList());
        }

        private void LoadAveragePitTime(IList<PitStopAverages> averages)
        {
            AveragePitTime.Clear();

            IList<PositionDriverValueModel> pitStopStats = new List<PositionDriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AveragePitTime != 0).OrderBy(a => a.AveragePitTime))
            {
                pitStopStats.Add(new PositionDriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AveragePitTime
                });

                i++;
            }

            AveragePitTime = new ObservableCollection<PositionDriverValueModel>(pitStopStats.ToList());
        }

        private void LoadAverageInOutTime(IList<PitStopAverages> averages)
        {
            AverageInOutTime.Clear();

            IList<PositionDriverValueModel> pitStopStats = new List<PositionDriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageInOutTime != 0).OrderBy(a => a.AverageInOutTime))
            {
                pitStopStats.Add(new PositionDriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageInOutTime
                });

                i++;
            }

            AverageInOutTime = new ObservableCollection<PositionDriverValueModel>(pitStopStats.ToList());
        }

        private void LoadAverageTotalTime(IList<PitStopAverages> averages)
        {
            AverageTotalTime.Clear();

            IList<PositionDriverValueModel> pitStopStats = new List<PositionDriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageTotalTime != 0).OrderBy(a => a.AverageTotalTime))
            {
                pitStopStats.Add(new PositionDriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageTotalTime
                });

                i++;
            }

            AverageTotalTime = new ObservableCollection<PositionDriverValueModel>(pitStopStats.ToList());
        }

        private void LoadGreenPitTime(IList<PitStopAverages> averages)
        {
            GreenPitTime.Clear();

            IList<PositionDriverValueModel> pitStopStats = new List<PositionDriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageGreenPitTime != 0).OrderBy(a => a.AverageGreenPitTime))
            {
                pitStopStats.Add(new PositionDriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageGreenPitTime
                });

                i++;
            }

            GreenPitTime = new ObservableCollection<PositionDriverValueModel>(pitStopStats.ToList());
        }

        private void LoadGreenInOutTime(IList<PitStopAverages> averages)
        {
            GreenInOutTime.Clear();

            IList<PositionDriverValueModel> pitStopStats = new List<PositionDriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageGreenInOutTime != 0).OrderBy(a => a.AverageGreenInOutTime))
            {
                pitStopStats.Add(new PositionDriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageGreenInOutTime
                });

                i++;
            }

            GreenInOutTime = new ObservableCollection<PositionDriverValueModel>(pitStopStats.ToList());
        }

        private void LoadGreenTotalTime(IList<PitStopAverages> averages)
        {
            GreenTotalTime.Clear();

            IList<PositionDriverValueModel> pitStopStats = new List<PositionDriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageGreenTotalTime != 0).OrderBy(a => a.AverageGreenTotalTime))
            {
                pitStopStats.Add(new PositionDriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageGreenTotalTime
                });

                i++;
            }

            GreenTotalTime = new ObservableCollection<PositionDriverValueModel>(pitStopStats.ToList());
        }

        private IList<PitStopAverages> GetPitStopAverages(IEnumerable<PitStop> pitStops)
        {
            var averages = new List<PitStopAverages>();

            foreach (var driverPitStops in pitStops.GroupBy(p => p.VehicleNumber))
            {
                if (driverPitStops != null && driverPitStops.Count() > 0)
                {
                    var driverPitStopSet = new PitStopAverages
                    {
                        CarNumber = driverPitStops.Key,
                        Driver = driverPitStops.First().DriverName,
                        TotalGainLoss = driverPitStops.Sum(p => p.PositionsGainedLost),
                        AveragePitTime = driverPitStops.Average(p => p.PitStopDuration),
                        AverageInOutTime = driverPitStops.Average(p => (p.InTravelDuration + p.OutTravelDuration)),
                        AverageTotalTime = driverPitStops.Average(p => p.TotalDuration)
                    };

                    var greenFlagStops = driverPitStops.Where(p => p.PitInFlagStatus == 1);

                    if (greenFlagStops != null && greenFlagStops.Count() > 0)
                    {
                        driverPitStopSet.AverageGreenPitTime = greenFlagStops.Average(p => p.PitStopDuration);
                        driverPitStopSet.AverageGreenTotalTime = greenFlagStops.Average(p => p.TotalDuration);
                        driverPitStopSet.AverageGreenInOutTime = greenFlagStops.Average(p => p.InTravelDuration + p.OutTravelDuration);
                    }

                    averages.Add(driverPitStopSet);
                }
            }

            return averages;
        }

        #endregion

        #region classes

        //private class CautionViewModel
        //{
        //    public int CautionNumber { get; set; }
        //    public int StartLap { get; set; }
        //    public int EndLap { get; set; }
        //    public string Comment { get; set; }

        //    public override string ToString()
        //    {
        //        return $"[{CautionNumber}] {StartLap}-{EndLap}  {Comment}";
        //    }
        //}

        public class CautionRangeModel
        {
            public int CautionNumber { get; set; }
            public int StartLap { get; set; }
            public int EndLap { get; set; }
            public string Comment { get; set; }
            public string Title
            {
                get
                {
                    return $"[{CautionNumber}] {StartLap}-{EndLap}  {Comment}";
                }
            }
        }

        public class DriverRangeModel
        {
            public string Name { get; set; }
            public string CarNumber { get; set; }
        }

        private class PitStopAverages
        {
            public string CarNumber { get; set; }
            public string Driver { get; set; }
            public int TotalGainLoss { get; set; }
            public float AveragePitTime { get; set; }
            public float AverageGreenPitTime { get; set; }
            public float AverageTotalTime { get; set; }
            public float AverageGreenTotalTime { get; set; }
            public float AverageInOutTime { get; set; }
            public float AverageGreenInOutTime { get; set; }
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
                _pitStopsRepository = null;
                _flagStateRepository = null;
            }
            // free native resources if there are any.
        }

        #endregion
    }
}
