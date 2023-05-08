using System.Collections.ObjectModel;

namespace rNascar23Multi.Logic
{
    internal static class ModelUpdater
    {
        internal static void ReloadModels<T>(ObservableCollection<T> models)
        {
            var existing = models.ToList();

            models.Clear();

            UpdateModels(models, existing);
        }

        internal static void UpdateModels<T>(ObservableCollection<T> models, IList<T> driverValues)
        {
            if (models.Count > driverValues.Count)
            {
                for (int i = models.Count - 1; i > driverValues.Count - 1; i--)
                {
                    models.RemoveAt(i);
                }
            }

            for (int i = 0; i < driverValues.Count; i++)
            {
                if (models.Count <= i)
                {
                    models.Add(driverValues[i]);
                }
                else
                {
                    if (!models[i].Equals(driverValues[i]))
                        models[i] = driverValues[i];
                }
            }
        }
    }
}
