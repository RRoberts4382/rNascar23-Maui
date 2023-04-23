using rNascar23Multi.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    internal class HorizontalGridViewModel : ObservableObject
    {
        ObservableCollection<GridViewModel> _models = new ObservableCollection<GridViewModel>();

        public ObservableCollection<GridViewModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        public HorizontalGridViewModel()
        {
            LoadFromSource();
        }

        protected virtual void LoadFromSource()
        {
            Models.Add(new GridViewModel()
            {
                ViewType = GridViewTypes.Flags
            });

            Models.Add(new GridViewModel()
            {
                ViewType = GridViewTypes.FastestLaps
            });

            Models.Add(new GridViewModel()
            {
                ViewType = GridViewTypes.LapLeaders
            });

            //Models.Add(new GridViewModel()
            //{
            //    ViewType = GridViewTypes.Movers
            //});

            //Models.Add(new GridViewModel()
            //{
            //    ViewType = GridViewTypes.Fallers
            //});

            //Models.Add(new GridViewModel()
            //{
            //    ViewType = GridViewTypes.Best5Laps
            //});

            //Models.Add(new GridViewModel()
            //{
            //    ViewType = GridViewTypes.Last5Laps
            //});

            //Models.Add(new GridViewModel()
            //{
            //    ViewType = GridViewTypes.Last15Laps
            //});

            Models.Add(new GridViewModel()
            {
                ViewType = GridViewTypes.KeyMoments
            });
        }
    }
}
