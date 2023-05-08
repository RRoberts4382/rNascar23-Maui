using rNascar23.Sdk.Common;
using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.LapTimes.Ports;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal class LastBestLapsListBuilder : ListBuilder<DriverValueModel>, IListBuilder<DriverValueModel>
    {
        #region fields

        private ILapTimesRepository _lapTimeRepository;
        private ILapAveragesRepository _lapAveragesRepository;

        #endregion

        #region properties

        public int MaxRowCount { get; set; } = 10;
        public SpeedTimeType SpeedTimeType { get; set; } = SpeedTimeType.Seconds;
        public GridViewTypes GridViewType { get; set; } = GridViewTypes.Best5Laps;

        #endregion

        #region ctor

        public LastBestLapsListBuilder(ILapTimesRepository lapTimeRepository, ILapAveragesRepository lapAveragesRepository)
        {
            _lapTimeRepository = lapTimeRepository ?? throw new ArgumentNullException(nameof(lapTimeRepository));
            _lapAveragesRepository = lapAveragesRepository ?? throw new ArgumentNullException(nameof(lapTimeRepository));
        }

        #endregion

        #region public

        public override async Task UpdateDataAsync(ObservableCollection<DriverValueModel> models, int? seriesId, int? raceId)
        {
            if (!seriesId.HasValue)
                throw new ArgumentNullException(nameof(seriesId));

            if (!raceId.HasValue)
                throw new ArgumentNullException(nameof(raceId));

            IList<DriverValueModel> driverValues;

            var lapTimes = await _lapTimeRepository.GetLapTimeDataAsync((SeriesTypes)seriesId.Value, raceId.Value);

            if (lapTimes == null || lapTimes.Drivers?.Count == 0)
            {
                var lapAverages = await _lapAveragesRepository.GetLapAveragesAsync((SeriesTypes)seriesId.Value, raceId.Value);

                driverValues = BuildViewModelsByLapAverages(GridViewType, lapAverages.ToList());
            }
            else
            {
                driverValues = BuildNLapsData(GridViewType, lapTimes.Drivers);
            }

            ModelUpdater.UpdateModels(models, driverValues);
        }

        #endregion

        #region protected

        protected virtual IList<DriverValueModel> BuildNLapsData(GridViewTypes viewType, IList<DriverLaps> driverLaps)
        {
            switch (SpeedTimeType)
            {
                case SpeedTimeType.Seconds:
                    return BuildViewModelsByTimeByDriverLaps(viewType, driverLaps);
                case SpeedTimeType.MPH:
                    return BuildViewModelsBySpeedByDriverLaps(viewType, driverLaps);
                default:
                    return new List<DriverValueModel>();
            }
        }

        protected virtual IList<DriverValueModel> BuildViewModelsByTimeByDriverLaps(GridViewTypes viewType, IList<DriverLaps> data)
        {
            switch (viewType)
            {
                case GridViewTypes.Best5Laps:
                    return data.
                        OrderBy(d => d.Best5LapAverageTime().GetValueOrDefault(999)).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.Best5LapAverageTime().GetValueOrDefault(999), 3)
                        }).
                        ToList();

                case GridViewTypes.Best10Laps:
                    return data.
                        OrderBy(d => d.Best10LapAverageTime().GetValueOrDefault(999)).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.Best10LapAverageTime().GetValueOrDefault(999), 3)
                        }).
                        ToList();

                case GridViewTypes.Best15Laps:
                    return data.
                        OrderBy(d => d.Best15LapAverageTime().GetValueOrDefault(999)).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.Best15LapAverageTime().GetValueOrDefault(999), 3)
                        }).
                        ToList();

                case GridViewTypes.Last5Laps:
                    return data.
                        OrderBy(d => d.AverageTimeLast5Laps().GetValueOrDefault(999)).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.AverageTimeLast5Laps().GetValueOrDefault(999), 3)
                        }).
                        ToList();

                case GridViewTypes.Last10Laps:
                    return data.
                        OrderBy(d => d.AverageTimeLast10Laps().GetValueOrDefault(999)).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.AverageTimeLast10Laps().GetValueOrDefault(999), 3)
                        }).
                        ToList();

                case GridViewTypes.Last15Laps:
                    return data.
                        OrderBy(d => d.AverageTimeLast15Laps().GetValueOrDefault(999)).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.AverageTimeLast15Laps().GetValueOrDefault(999), 3)
                        }).
                        ToList();

                default:
                    return new List<DriverValueModel>();
            }
        }

        protected virtual IList<DriverValueModel> BuildViewModelsBySpeedByDriverLaps(GridViewTypes viewType, IList<DriverLaps> data)
        {
            switch (viewType)
            {
                case GridViewTypes.Best5Laps:
                    return data.
                        OrderByDescending(d => d.Best5LapAverageSpeed().GetValueOrDefault(-1)).
                        Where(d => d.Best5LapAverageSpeed().GetValueOrDefault(-1) != -1).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.Best5LapAverageSpeed().GetValueOrDefault(-1), 3)
                        }).
                        ToList();

                case GridViewTypes.Best10Laps:
                    return data.
                        OrderByDescending(d => d.Best10LapAverageSpeed().GetValueOrDefault(-1)).
                        Where(d => d.Best10LapAverageSpeed().GetValueOrDefault(-1) != -1).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.Best10LapAverageSpeed().GetValueOrDefault(-1), 3)
                        }).
                        ToList();

                case GridViewTypes.Best15Laps:
                    return data.
                        OrderByDescending(d => d.Best15LapAverageSpeed().GetValueOrDefault(-1)).
                        Where(d => d.Best15LapAverageSpeed().GetValueOrDefault(-1) != -1).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.Best15LapAverageSpeed().GetValueOrDefault(-1), 3)
                        }).
                        ToList();

                case GridViewTypes.Last5Laps:
                    return data.
                        OrderByDescending(d => d.AverageSpeedLast5Laps().GetValueOrDefault(-1)).
                        Where(d => d.AverageSpeedLast5Laps().GetValueOrDefault(-1) != -1).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.AverageSpeedLast5Laps().GetValueOrDefault(-1), 3)
                        }).
                        ToList();

                case GridViewTypes.Last10Laps:
                    return data.
                        OrderByDescending(d => d.AverageSpeedLast10Laps().GetValueOrDefault(-1)).
                        Where(d => d.AverageSpeedLast10Laps().GetValueOrDefault(-1) != -1).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.AverageSpeedLast10Laps().GetValueOrDefault(-1), 3)
                        }).
                        ToList();

                case GridViewTypes.Last15Laps:
                    return data.
                        OrderByDescending(d => d.AverageSpeedLast15Laps().GetValueOrDefault(-1)).
                        Where(d => d.AverageSpeedLast15Laps().GetValueOrDefault(-1) != -1).
                        Take(MaxRowCount).
                        Select((d, i) => new DriverValueModel()
                        {
                            Position = i,
                            Driver = d.FullName,
                            Value = (float)Math.Round(d.AverageSpeedLast15Laps().GetValueOrDefault(-1), 3)
                        }).
                        ToList();

                default:
                    return new List<DriverValueModel>();
            }
        }

        protected virtual IList<DriverValueModel> BuildViewModelsByLapAverages(GridViewTypes viewType, IList<LapAverages> data)
        {
            switch (viewType)
            {
                case GridViewTypes.Best5Laps:
                    var best5Laps = data.
                        Where(v => v.Con5LapRank != null).
                        OrderBy(v => v.Con5LapRank).
                        Take(MaxRowCount).
                        Select(d => new DriverValueModel()
                        {
                            Position = d.Con5LapRank.Value,
                            Driver = d.FullName,
                            Value = (float)d.Con5Lap
                        }).
                        ToList();

                    return best5Laps;

                case GridViewTypes.Best10Laps:
                    var best10Laps = data.
                        Where(v => v.Con10LapRank != null).
                        OrderBy(v => v.Con10LapRank).
                        Take(MaxRowCount).
                        Select(d => new DriverValueModel()
                        {
                            Position = d.Con10LapRank.Value,
                            Driver = d.FullName,
                            Value = (float)d.Con10Lap
                        }).
                        ToList();

                    return best10Laps;

                case GridViewTypes.Best15Laps:
                    var best15Laps = data.
                      Where(v => v.Con15LapRank != null).
                      OrderBy(v => v.Con15LapRank).
                      Take(MaxRowCount).
                      Select(d => new DriverValueModel()
                      {
                          Position = d.Con15LapRank.Value,
                          Driver = d.FullName,
                          Value = (float)d.Con15Lap
                      }).
                      ToList();

                    return best15Laps;

                case GridViewTypes.Last5Laps:
                    return new List<DriverValueModel>();

                case GridViewTypes.Last10Laps:
                    return new List<DriverValueModel>();

                case GridViewTypes.Last15Laps:
                    return new List<DriverValueModel>();

                default:
                    return new List<DriverValueModel>();
            }
        }

        #endregion
    }
}
