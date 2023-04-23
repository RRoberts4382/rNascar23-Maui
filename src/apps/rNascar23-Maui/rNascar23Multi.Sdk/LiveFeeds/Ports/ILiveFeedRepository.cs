using rNascar23Multi.Sdk.LiveFeeds.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23Multi.Sdk.Data.LiveFeeds.Ports
{
    public interface ILiveFeedRepository
    {
        Task<LiveFeed> GetLiveFeedAsync(CancellationToken cancellationToken = default);
    }
}
