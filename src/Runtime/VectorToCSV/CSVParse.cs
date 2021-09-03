using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InformationRetrieval.Runtime.VectorToCSV
{
    internal class CSVParse : ICSVParse
    {
        private readonly ILogger<CSVParse> _logger;
        private readonly IConfigurationRoot _config;
        
        public CSVParse(IConfigurationRoot config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _logger = loggerFactory.CreateLogger<CSVParse>();
        }
        
        public dynamic Execute(dynamic inputData)
        {
            throw new System.NotImplementedException();
        }

        public CSVParseRespose Generate(CSVParseRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}