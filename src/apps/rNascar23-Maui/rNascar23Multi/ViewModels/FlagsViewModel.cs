using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using rNascar23.Sdk.Flags.Ports;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace rNascar23Multi.ViewModels
{
    public partial class FlagsViewModel : ObservableObject
    {
        ILogger<FlagsViewModel> _logger;
        private IFlagStateRepository _flagStateRepository;

        private ObservableCollection<FlagsModel> _models = new ObservableCollection<FlagsModel>();

        public ObservableCollection<FlagsModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private string _listHeader = "Flags";
        public string ListHeader
        {
            get => _listHeader;
            set => SetProperty(ref _listHeader, value);
        }

        public FlagsViewModel(
            ILogger<FlagsViewModel> logger,
            IFlagStateRepository flagStateRepository,
            UpdateNotificationHandler updateTimer)
        {
            _flagStateRepository = flagStateRepository ?? throw new ArgumentNullException(nameof(flagStateRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            updateTimer.UpdateTimerElapsed += UpdateTimer_UpdateTimerElapsed;
        }

        private async void UpdateTimer_UpdateTimerElapsed(object sender, UpdateNotificationEventArgs e)
        {
            try
            {
                _logger.LogInformation($"FlagsViewModel - UpdateTimer_UpdateTimerElapsed");

                if (e.SessionDetails != null)
                {
                    await LoadModelsAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FlagsViewModel:UpdateTimer_UpdateTimerElapsed");
            }
        }

        [RelayCommand]
        private async Task InitAsync()
        {
            try
            {
                await LoadModelsAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FlagsViewModel:InitAsync");
            }
        }

        private async Task LoadModelsAsync()
        {
            try
            {
                var flagStates = await _flagStateRepository.GetFlagStatesAsync();

                foreach (var flagState in flagStates)
                {
                    var existing = Models.FirstOrDefault(m => m.Lap == flagState.LapNumber && m.FlagState == (int)flagState.State);

                    if (existing != null)
                    {
                        existing.CautionFor = flagState.Comment;
                        existing.LuckyDog = flagState.Beneficiary;
                    }
                    else
                    {
                        Models.Add(new FlagsModel()
                        {
                            FlagState = (int)flagState.State,
                            Lap = flagState.LapNumber,
                            CautionFor = flagState.Comment,
                            LuckyDog = flagState.Beneficiary,
                            Timestamp = flagState.TimeOfDayOs
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FlagsViewModel:LoadModelsAsync");
            }
        }
    }
}