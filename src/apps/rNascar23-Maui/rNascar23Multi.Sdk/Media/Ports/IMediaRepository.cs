using rNascar23Multi.Sdk.Common;
using rNascar23Multi.Sdk.Media.Models;

namespace rNascar23Multi.Sdk.Media.Ports
{
    public interface IMediaRepository
    {
        MediaImage GetCarNumberImage(SeriesTypes seriesId, int carNumber);
        Task<byte[]> GetCarNumberImageAsync(SeriesTypes seriesId, int carNumber, CancellationToken cancellationToken = default);
        Task<AudioConfiguration> GetAudioConfigurationAsync(SeriesTypes seriesId, CancellationToken cancellationToken = default);
        Task<VideoConfiguration> GetVideoConfigurationAsync(SeriesTypes seriesId, CancellationToken cancellationToken = default);
    }
}