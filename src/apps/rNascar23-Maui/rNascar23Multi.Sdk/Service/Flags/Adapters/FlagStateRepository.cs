using AutoMapper;
using Microsoft.Extensions.Logging;
using rNascar23Multi.Sdk.Data;
using rNascar23Multi.Sdk.Flags.Models;
using rNascar23Multi.Sdk.Flags.Ports;
using rNascar23Multi.Sdk.Service.Flags.Data.Models;

namespace rNascar23Multi.Sdk.Service.Flags.Adapters
{
    internal class FlagStateRepository : JsonDataRepository, IFlagStateRepository
    {
        private readonly IMapper _mapper;

        public FlagStateRepository(IMapper mapper, ILogger<FlagStateRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public string Url { get => @"https://cf.nascar.com/live/feeds/live-flag-data.json"; }

        public async Task<IList<FlagState>> GetFlagStatesAsync()
        {
            try
            {
                var json = await GetAsync(Url).ConfigureAwait(false);

                if (string.IsNullOrEmpty(json))
                    return new List<FlagState>();

                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<FlagStateModel[]>(json);

                var flagStates = _mapper.Map<List<FlagState>>(model);

                return flagStates;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
