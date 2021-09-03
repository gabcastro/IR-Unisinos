using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InformationRetrieval.Runtime.RankManager
{
    internal class Rank : IRank
    {
        private readonly ILogger<Rank> _logger;
        private readonly IConfigurationRoot _config;

        public Rank(IConfigurationRoot config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _logger = loggerFactory.CreateLogger<Rank>();
        }

        public dynamic Execute(dynamic inputData)
        {
            throw new System.NotImplementedException();
        }

        public RankResponse Generate(RankRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}