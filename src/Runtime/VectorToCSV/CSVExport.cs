using Microsoft.Extensions.Logging;

namespace InformationRetrieval.Runtime.VectorToCSV
{
    internal class CSVExport : ICSVExport
    {
        private readonly ILogger<CSVExport> _logger;
        
        public CSVExport(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CSVExport>();
        }
        
        public dynamic Execute(dynamic inputData)
        {
            return Generate(inputData);
        }

        public CSVExportRespose Generate(CSVExportRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}