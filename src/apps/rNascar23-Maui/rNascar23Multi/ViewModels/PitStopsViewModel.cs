using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Flags.Models;
using rNascar23.Sdk.Flags.Ports;
using rNascar23.Sdk.PitStops.Models;
using rNascar23.Sdk.PitStops.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23Multi.Settings.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace rNascar23Multi.ViewModels
{
    public partial class PitStopsViewModel : ObservableObject, INotifyUpdateTarget, INotifySettingsChanged, IDisposable
    {
        #region consts

        private const string AllDriversPitStopsHeader = "Pit Stops";
        private const string DriverPitStopsHeader = "Driver Pit Stops";
        private const string DriverPitStopsStatisticsHeader = "Driver Pit Stop Statistics";

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

        private string _allDriverPitStopsHeader = $"All {AllDriversPitStopsHeader}";
        public string AllDriverPitStopsHeader
        {
            get => _allDriverPitStopsHeader;
            set => SetProperty(ref _allDriverPitStopsHeader, value);
        }

        private string _driverPitStopsName = DriverPitStopsHeader;
        public string DriverPitStopsName
        {
            get => _driverPitStopsName;
            set => SetProperty(ref _driverPitStopsName, value);
        }

        private string _driverPitStopStatisticsName = DriverPitStopsStatisticsHeader;
        public string DriverPitStopStatisticsName
        {
            get => _driverPitStopStatisticsName;
            set => SetProperty(ref _driverPitStopStatisticsName, value);
        }

        private DriverPitStopStatisticsModel _driverStatistics = new DriverPitStopStatisticsModel();
        public DriverPitStopStatisticsModel DriverStatistics
        {
            get => _driverStatistics;
            set => SetProperty(ref _driverStatistics, value);
        }

        private ObservableCollection<DriverValueModel> _totalGainLoss = new ObservableCollection<DriverValueModel>();
        public ObservableCollection<DriverValueModel> TotalGainLoss
        {
            get => _totalGainLoss;
            set => SetProperty(ref _totalGainLoss, value);
        }

        private ObservableCollection<DriverValueModel> _averagePitTime = new ObservableCollection<DriverValueModel>();
        public ObservableCollection<DriverValueModel> AveragePitTime
        {
            get => _averagePitTime;
            set => SetProperty(ref _averagePitTime, value);
        }

        private ObservableCollection<DriverValueModel> _averageInOutTime = new ObservableCollection<DriverValueModel>();
        public ObservableCollection<DriverValueModel> AverageInOutTime
        {
            get => _averageInOutTime;
            set => SetProperty(ref _averageInOutTime, value);
        }

        private ObservableCollection<DriverValueModel> _averageTotalTime = new ObservableCollection<DriverValueModel>();
        public ObservableCollection<DriverValueModel> AverageTotalTime
        {
            get => _averageTotalTime;
            set => SetProperty(ref _averageTotalTime, value);
        }

        private ObservableCollection<DriverValueModel> _greenPitTime = new ObservableCollection<DriverValueModel>();
        public ObservableCollection<DriverValueModel> GreenPitTime
        {
            get => _greenPitTime;
            set => SetProperty(ref _greenPitTime, value);
        }

        private ObservableCollection<DriverValueModel> _greenInOutTime = new ObservableCollection<DriverValueModel>();
        public ObservableCollection<DriverValueModel> GreenInOutTime
        {
            get => _greenInOutTime;
            set => SetProperty(ref _greenInOutTime, value);
        }

        private ObservableCollection<DriverValueModel> _greenTotalTime = new ObservableCollection<DriverValueModel>();
        public ObservableCollection<DriverValueModel> GreenTotalTime
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

        #endregion

        #region public

        public void UserSettingsUpdated(SettingsModel settings)
        {
            try
            {
                ReloadAllDriversPitStops();

                DisplayPitStopAverages(_averages);
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
                        // new race event
                        SeriesId = (SeriesTypes)e.SessionDetails.SeriesId;
                        RaceId = e.SessionDetails.RaceId;

                        await UpdateRangeSelectionListsAsync();

                        if (SelectedCaution != null)
                            await FilterByCautionAsync();
                        else
                            await AllPitStopsAsync();

                        await UpdateAveragesAsync();
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
                if (selectedDriver == null)
                    ClearDriverPitStops();
                else
                    await LoadDriverPitStopsAsync(SeriesId, RaceId, selectedDriver.CarNumber, selectedDriver.Driver);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateSelectedDriverAsync");
            }
        }

        public async Task FilterByLapsAsync()
        {
            try
            {
                SelectedCaution = null;
                SelectedDriver = null;

                var start = StartLap ?? 1;
                var end = EndLap ?? EndLaps.Max();

                AllDriverPitStopsHeader = $"{AllDriversPitStopsHeader} - Laps {start} to {end}";

                await LoadAllDriversPitStopsAsync(SeriesId, RaceId, StartLap, EndLap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateByLapsAsync");
            }
        }

        public async Task FilterByCautionAsync()
        {
            try
            {
                if (SelectedCaution != null)
                {
                    StartLap = null;
                    EndLap = null;
                    SelectedDriver = null;

                    AllDriverPitStopsHeader = $"{AllDriversPitStopsHeader} - Caution {SelectedCaution.Title}";

                    await LoadAllDriversPitStopsAsync(SeriesId, RaceId, SelectedCaution.StartLap, SelectedCaution.EndLap);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateByCautionAsync");
            }
        }

        public async Task FilterByDriverAsync()
        {
            try
            {
                if (SelectedDriver != null)
                {
                    StartLap = null;
                    EndLap = null;
                    SelectedCaution = null;

                    AllDriverPitStopsHeader = $"{AllDriversPitStopsHeader} - {SelectedDriver.Name}";

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

                AllDriverPitStopsHeader = $"All {AllDriversPitStopsHeader}";

                await LoadAllDriversPitStopsAsync(SeriesId, RaceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:AllPitStopsAsync");
            }
        }

        #endregion

        #region private

        private void ReloadAllDriversPitStops()
        {
            var existing = AllDriversPitStops.ToList();

            AllDriversPitStops.Clear();

            Selected = null;

            UpdateModels(AllDriversPitStops, existing);
        }

        private async Task LoadAllDriversPitStopsAsync(
            SeriesTypes seriesId,
            int raceId,
            int? startLap = null,
            int? endLap = null,
            string carNumber = null)
        {
            try
            {
                var driverPitStops = await _pitStopsRepository.GetPitStopsInRangeAsync(
                    seriesId,
                    raceId,
                    startLap,
                    endLap,
                    carNumber);

                var allDriversPitStopModels = driverPitStops.OrderBy(p => p.LapCount).
                      Select((p, i) => new PitStopsAllDriversModel()
                      {
                          Index = i,
                          CarNumber = p.VehicleNumber,
                          Driver = p.DriverName,
                          Lap = p.LapCount,
                          Flag = p.PitInFlagStatus,
                          Tires = p.PitStopType,
                          TotalTime = p.TotalDuration,
                          PitTime = p.PitStopDuration,
                          PositionIn = p.PitInRank,
                          PositionOut = p.PitOutRank,
                          PositionDelta = p.PositionsGainedLost
                      }).ToList();

                PitStopsViewModel.UpdateModels(AllDriversPitStops, allDriversPitStopModels);

                if (AllDriversPitStops.Count > 0)
                {
                    var selectedDriver = AllDriversPitStops[0];

                    selectedDriver.IsSelected = true;

                    Selected = selectedDriver;
                }
                else
                    ClearDriverPitStops();
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

                if (!flagStates.Any())
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
                CautionsList.Add(item);

            if (previouslySelectedCaution != null)
                SelectedCaution = cautionModels.FirstOrDefault(c => c.CautionNumber == previouslySelectedCaution.CautionNumber);
            else
                SelectedCaution = cautionModels.FirstOrDefault();
        }

        private async Task UpdateAveragesAsync()
        {
            try
            {
                var pitStops = await _pitStopsRepository.GetPitStopsAsync(SeriesId, RaceId);

                _averages = PitStopsViewModel.GetPitStopAverages(pitStops);

                DisplayPitStopAverages(_averages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:UpdateAveragesAsync");
            }
        }

        private void DisplayPitStopAverages(IList<PitStopAverages> averages)
        {
            LoadTotalGainLoss(averages);
            LoadAveragePitTime(averages);
            LoadAverageInOutTime(averages);
            LoadAverageTotalTime(averages);
            LoadGreenPitTime(averages);
            LoadGreenInOutTime(averages);
            LoadGreenTotalTime(averages);
        }

        private void ClearDriverPitStops()
        {
            DriverPitStopsName = DriverPitStopsHeader;

            DriverPitStopStatisticsName = DriverPitStopsStatisticsHeader;

            DriverPitStops.Clear();

            DriverStatistics = new DriverPitStopStatisticsModel();
        }

        private async Task LoadDriverPitStopsAsync(SeriesTypes seriesId, int raceId, string carNumber, string driverName)
        {
            try
            {
                var driverPitStops = await _pitStopsRepository.GetPitStopsInRangeAsync(seriesId, raceId, null, null, carNumber);

                var driverPitStopModels = driverPitStops.OrderBy(p => p.LapCount).
                       Select((p, i) => new PitStopsAllDriversModel()
                       {
                           Index = i,
                           Lap = p.LapCount,
                           Tires = p.PitStopType,
                           TotalTime = p.TotalDuration,
                           PitTime = p.PitStopDuration,
                           Flag = p.PitInFlagStatus,
                           PositionIn = p.PitInRank,
                           PositionOut = p.PitOutRank,
                           PositionDelta = p.PositionsGainedLost
                       }).ToList();

                UpdateDriverPitStopsModels(driverPitStopModels);

                if (DriverStatistics == null || !driverPitStops.Any())
                    DriverStatistics = new DriverPitStopStatisticsModel();

                if (driverPitStops.Any())
                {
                    DriverStatistics.NumberOfStops = driverPitStops.Count();
                    DriverStatistics.AverageGainLoss = driverPitStops.Average(p => p.PositionsGainedLost);
                    DriverStatistics.AveragePitTime = driverPitStops.Average(p => p.PitStopDuration);
                    DriverStatistics.AverageTotalTime = driverPitStops.Average(p => p.TotalDuration);
                    DriverStatistics.AverageInOutTime = driverPitStops.Average(p => (p.InTravelDuration + p.OutTravelDuration));

                    var carAverages = _averages?.FirstOrDefault(a => a.CarNumber == carNumber);

                    if (carAverages != null)
                    {
                        DriverStatistics.AverageGainLossRank = _averages.OrderByDescending(a => a.TotalGainLoss).ToList().IndexOf(carAverages) + 1;
                        DriverStatistics.AveragePitTimeRank = _averages.OrderBy(a => a.AveragePitTime).ToList().IndexOf(carAverages) + 1;
                        DriverStatistics.AverageTotalTimeRank = _averages.OrderBy(a => a.AverageTotalTime).ToList().IndexOf(carAverages) + 1;
                        DriverStatistics.AverageInOutTimeRank = _averages.OrderBy(a => a.AverageInOutTime).ToList().IndexOf(carAverages) + 1;
                    }
                    else
                    {
                        DriverStatistics.AverageGainLossRank = null;
                        DriverStatistics.AveragePitTimeRank = null;
                        DriverStatistics.AverageTotalTimeRank = null;
                        DriverStatistics.AverageInOutTimeRank = null;
                    }
                }

                DriverPitStopsName = $"{DriverPitStopsHeader} - [{carNumber}] {driverName}";

                DriverPitStopStatisticsName = $"{DriverPitStopsStatisticsHeader} -  [{carNumber}] {driverName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PitStopsViewModel:LoadDriverPitStopsAsync");
            }
        }

        private void LoadTotalGainLoss(IList<PitStopAverages> averages)
        {
            TotalGainLoss.Clear();

            IList<DriverValueModel> pitStopStats = new List<DriverValueModel>();

            int i = 1;
            foreach (var item in averages.OrderByDescending(a => a.TotalGainLoss))
            {
                pitStopStats.Add(new DriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.TotalGainLoss
                });

                i++;
            }

            TotalGainLoss = new ObservableCollection<DriverValueModel>(pitStopStats.ToList());
        }

        private void LoadAveragePitTime(IList<PitStopAverages> averages)
        {
            AveragePitTime.Clear();

            IList<DriverValueModel> pitStopStats = new List<DriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AveragePitTime != 0).OrderBy(a => a.AveragePitTime))
            {
                pitStopStats.Add(new DriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AveragePitTime
                });

                i++;
            }

            AveragePitTime = new ObservableCollection<DriverValueModel>(pitStopStats.ToList());
        }

        private void LoadAverageInOutTime(IList<PitStopAverages> averages)
        {
            AverageInOutTime.Clear();

            IList<DriverValueModel> pitStopStats = new List<DriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageInOutTime != 0).OrderBy(a => a.AverageInOutTime))
            {
                pitStopStats.Add(new DriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageInOutTime
                });

                i++;
            }

            AverageInOutTime = new ObservableCollection<DriverValueModel>(pitStopStats.ToList());
        }

        private void LoadAverageTotalTime(IList<PitStopAverages> averages)
        {
            AverageTotalTime.Clear();

            IList<DriverValueModel> pitStopStats = new List<DriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageTotalTime != 0).OrderBy(a => a.AverageTotalTime))
            {
                pitStopStats.Add(new DriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageTotalTime
                });

                i++;
            }

            AverageTotalTime = new ObservableCollection<DriverValueModel>(pitStopStats.ToList());
        }

        private void LoadGreenPitTime(IList<PitStopAverages> averages)
        {
            GreenPitTime.Clear();

            IList<DriverValueModel> pitStopStats = new List<DriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageGreenPitTime != 0).OrderBy(a => a.AverageGreenPitTime))
            {
                pitStopStats.Add(new DriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageGreenPitTime
                });

                i++;
            }

            GreenPitTime = new ObservableCollection<DriverValueModel>(pitStopStats.ToList());
        }

        private void LoadGreenInOutTime(IList<PitStopAverages> averages)
        {
            GreenInOutTime.Clear();

            IList<DriverValueModel> pitStopStats = new List<DriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageGreenInOutTime != 0).OrderBy(a => a.AverageGreenInOutTime))
            {
                pitStopStats.Add(new DriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageGreenInOutTime
                });

                i++;
            }

            GreenInOutTime = new ObservableCollection<DriverValueModel>(pitStopStats.ToList());
        }

        private void LoadGreenTotalTime(IList<PitStopAverages> averages)
        {
            GreenTotalTime.Clear();

            IList<DriverValueModel> pitStopStats = new List<DriverValueModel>();

            int i = 1;
            foreach (var item in averages.Where(a => a.AverageGreenTotalTime != 0).OrderBy(a => a.AverageGreenTotalTime))
            {
                pitStopStats.Add(new DriverValueModel()
                {
                    Position = i,
                    Driver = item.Driver,
                    Value = item.AverageGreenTotalTime
                });

                i++;
            }

            GreenTotalTime = new ObservableCollection<DriverValueModel>(pitStopStats.ToList());
        }

        private static IList<PitStopAverages> GetPitStopAverages(IEnumerable<PitStop> pitStops)
        {
            var averages = new List<PitStopAverages>();

            foreach (var driverPitStops in pitStops.GroupBy(p => p.VehicleNumber))
            {
                if (driverPitStops != null && driverPitStops.Any())
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

                    if (greenFlagStops != null && greenFlagStops.Any())
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

        private void UpdateDriverPitStopsModels(IList<PitStopsAllDriversModel> allDriversPitStops)
        {
            if (DriverPitStops.Count > allDriversPitStops.Count)
            {
                for (int i = DriverPitStops.Count - 1; i > allDriversPitStops.Count - 1; i--)
                {
                    DriverPitStops.RemoveAt(i);
                }
            }

            for (int i = 0; i < allDriversPitStops.Count; i++)
            {
                if (DriverPitStops.Count <= i)
                {
                    DriverPitStops.Add(allDriversPitStops[i]);
                }
                else
                {
                    DriverPitStops[i] = allDriversPitStops[i];
                }
            }
        }

        private static void UpdateModels<T>(ObservableCollection<T> models, IList<T> values)
        {
            if (models.Count > values.Count)
            {
                for (int i = models.Count - 1; i > values.Count - 1; i--)
                {
                    models.RemoveAt(i);
                }
            }

            for (int i = 0; i < values.Count; i++)
            {
                if (models.Count <= i)
                {
                    models.Add(values[i]);
                }
                else
                {
                    models[i] = values[i];
                }
            }
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

            _disposed = true;
        }

        #endregion
    }
}
