using rNascar23Multi.Sdk.Flags.Models;

namespace rNascar23Multi.Sdk.Flags.Ports
{
    public interface IFlagStateRepository
    {
        Task<IList<FlagState>> GetFlagStatesAsync();
    }
}
