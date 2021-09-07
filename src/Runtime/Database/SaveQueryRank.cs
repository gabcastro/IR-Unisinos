using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InformationRetrieval.Runtime.Database
{
    internal class SaveQueryRank : ISaveQueryRank
    {
        private readonly ILogger<SaveQueryRank> _logger;
        private readonly IConfigurationRoot _config;
        private readonly DatabaseUtils _databaseUtils;

        public SaveQueryRank(IConfigurationRoot config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _logger = loggerFactory.CreateLogger<SaveQueryRank>();
            // _databaseUtils = new DatabaseUtils(_config["DatabasePath"], _config["DatabaseName"]);
        }

        public dynamic Execute(dynamic inputData)
        {
            return Generate(inputData);
        }

        public SaveQueryRankResponse Generate(SaveQueryRankRequest request)
        {
            return new SaveQueryRankResponse {};
        } 
    }
}