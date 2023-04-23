using rNascar23Multi.Models;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.ObjectModel;

namespace rNascar23Multi.ViewModels
{
    internal class VerticalGridViewModel : ObservableObject
    {
        ObservableCollection<GridViewModel> _models = new ObservableCollection<GridViewModel>();

        public ObservableCollection<GridViewModel> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        public VerticalGridViewModel()
        {
            LoadFromSource();
        }

        protected virtual void LoadFromSource()
        {
            Models.Add(new GridViewModel()
            {
                ViewType = GridViewTypes.Points
            });

            Models.Add(new GridViewModel()
            {
                ViewType = GridViewTypes.StagePoints
            });

            //Models.Add(new GridViewModel()
            //{
            //    ViewType = GridViewTypes.LapLeaders
            //});

            //Models.Add(new GridViewModel()
            //{
            //    ViewType = GridViewTypes.FastestLaps
            //});

            Models.Add(new GridViewModel()
            {
                ViewType = GridViewTypes.Movers
            });

            Models.Add(new GridViewModel()
            {
                ViewType = GridViewTypes.Fallers
            });
        }
    }
}
