using rNascar23Multi.Models;
using rNascar23Multi.ViewModels;
using rNascar23Multi.Views;

namespace rNascar23Multi.Logic
{
    class ViewModelFactory
    {
        public IViewTypeViewModel GetViewTypeViewModel<T>(T view, EventViewType viewType) where T : IView
        {
            if (view is HorizontalGridView)
            {
                var viewModel = App.serviceProvider.GetRequiredService<HorizontalGridViewModel>();

                viewModel.ViewType = viewType;

                return viewModel;
            }
            else if (view is VerticalGridView)
            {
                var viewModel = App.serviceProvider.GetRequiredService<VerticalGridViewModel>();

                viewModel.ViewType = viewType;

                return viewModel;
            }
            else
                throw new ArgumentException($"Invalid view type for ViewModelFactory:{typeof(T)}", nameof(view));
        }
    }
}
