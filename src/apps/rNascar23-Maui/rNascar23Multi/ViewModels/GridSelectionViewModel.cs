using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    public class GridSelectionViewModel : ObservableObject
    {
        ILogger<GridSelectionViewModel> _logger;

        private ObservableCollection<GridSelectionModel> _models = new ObservableCollection<GridSelectionModel>();

        public ObservableCollection<GridSelectionModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        public GridSelectionViewModel(
            ILogger<GridSelectionViewModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            LoadModels();
        }

        private void LoadModels()
        {
            try
            {
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.FastestLaps,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.LapLeaders,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Movers,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Fallers,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.KeyMoments,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Flags,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Points,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.StagePoints,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Last5Laps,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Last10Laps,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Last15Laps,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Best5Laps,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Best10Laps,
                });
                Models.Add(new GridSelectionModel()
                {
                    GridViewType = GridViewTypes.Best15Laps,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GridSelectionViewModel:LoadModels");
            }
        }
    }
}