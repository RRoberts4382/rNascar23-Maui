using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.PlatformUI;
using rNascar23.Sdk.Flags.Ports;
using rNascar23Multi.Logic;
using rNascar23Multi.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace rNascar23Multi.ViewModels
{
    public partial class FlagsViewModel : ObservableObject, INotifyUpdateTarget, IDisposable
    {
        #region fields

        private ILogger<FlagsViewModel> _logger;
        private IFlagStateRepository _flagStateRepository;

        #endregion

        #region properties

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

        #endregion

        #region ctor

        public FlagsViewModel(
            ILogger<FlagsViewModel> logger,
            IFlagStateRepository flagStateRepository)
        {
            _flagStateRepository = flagStateRepository ?? throw new ArgumentNullException(nameof(flagStateRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region public

        public async Task UserSettingsUpdatedAsync()
        {

        }

        public async Task UpdateTimerElapsedAsync(UpdateNotificationEventArgs e)
        {
            try
            {
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

        #endregion

        #region private

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

                if (Models.Count > flagStates.Count())
                {
                    Models.Clear();
                }

                foreach (var flagState in flagStates)
                {
                    var existing = Models.FirstOrDefault(m => m.Timestamp == flagState.TimeOfDayOs);

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

        #endregion

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
                _flagStateRepository = null;
            }
            // free native resources if there are any.
        }

        ~FlagsViewModel()
        {
            Debug.WriteLine("********************************* FlagsViewModel Disposed");
        }
    }
}