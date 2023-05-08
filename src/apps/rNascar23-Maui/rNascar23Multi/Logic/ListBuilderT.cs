using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal abstract class ListBuilder<T>
    {
        public virtual Task UpdateDataAsync(ObservableCollection<T> models, int? seriesId = null, int? raceId = null)
        {
            throw new NotImplementedException();
        }
    }
}
