using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal interface IListBuilder<T>
    {
        Task UpdateDataAsync(ObservableCollection<T> models, int? seriesId = null, int? raceId = null);
    }
}
