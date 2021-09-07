using System.Collections.Generic;
using InformationRetrieval.Runtime.RankManager;
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
            //request.ListRankRetrieval;
            SaveCSV(request.QueryString, request.ListRankRetrieval);
            return new CSVExportRespose
            {
                QueryString = request.QueryString,
                ListRankRetrieval = request.ListRankRetrieval
            };
        }

        private void SaveCSV(string queryString, List<RankRetrieval> listRankRetrieval)
        {
            
        }
    }
}