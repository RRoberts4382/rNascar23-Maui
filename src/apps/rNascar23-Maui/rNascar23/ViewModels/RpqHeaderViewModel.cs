using rNascar23.Models;
using Microsoft.VisualStudio.PlatformUI;

namespace rNascar23.ViewModels
{
    public class RpqHeaderViewModel : ObservableObject
    {
        private RpqHeaderModel _model;

        public RpqHeaderModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public RpqHeaderViewModel()
        {
            LoadFromSource();
        }

        private void LoadFromSource()
        {
            _model = new RpqHeaderModel()
            {
                FlagState = 2,
                EventName = "Jiffy Lube Service 499 Presented by SpongeBob - Cup Series - North Wilkesboro (50/50/100)",
                RaceLapInfo = "Race: Lap 175 of 200",
                StageLapInfo = "Stage 3: Lap 75 of 100"
            };
        }
    }
}