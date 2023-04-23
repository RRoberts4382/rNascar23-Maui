using rNascar23Multi.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;
using rNascar23Multi.Sdk.Flags.Ports;

namespace rNascar23Multi.ViewModels
{
    public class FlagsViewModel : ObservableObject
    {
        private readonly Task initTask;

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

        public FlagsViewModel() // public FlagsViewModel(IFlagStateRepository flagStateRepository)
        {
            //_flagStateRepository = flagStateRepository ?? throw new ArgumentNullException(nameof(flagStateRepository));

            this.initTask = LoadModelsAsync();
        }

        private async Task LoadModelsAsync()
        {
            try
            {
                _flagStateRepository = App.serviceProvider.GetService<IFlagStateRepository>();

                var flagStates = await _flagStateRepository.GetFlagStatesAsync();

                foreach (var flagState in flagStates)
                {
                    Models.Add(new FlagsModel()
                    {
                        FlagState = (int)flagState.State,
                        Lap = flagState.LapNumber,
                        CautionFor = flagState.Comment,
                        Timestamp = flagState.TimeOfDayOs
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}