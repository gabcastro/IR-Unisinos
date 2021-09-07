using Microsoft.Extensions.Logging;

namespace InformationRetrieval.Runtime.VizInformation
{
    internal class VizInfoRetrieval : IVizInfoRetrieval
    {
        private readonly ILogger<VizInfoRetrieval> _logger;

        public VizInfoRetrieval(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<VizInfoRetrieval>();
        }
        
        public dynamic Execute(dynamic inputData)
        {
            throw new System.NotImplementedException();
        }

        public VizInfoRetrievalResponse Generator(VizInfoRetrievalRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}