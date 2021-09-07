using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
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

            SaveCSV(request.QueryString, request.ListRankRetrieval);

            return new CSVExportRespose
            {
                QueryString = request.QueryString,
                ListRankRetrieval = request.ListRankRetrieval
            };
        }

        private void SaveCSV(string queryString, List<RankRetrieval> listRankRetrieval)
        {
            using (var writer = new StreamWriter("result.csv", false, Encoding.UTF8))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    foreach (var list in listRankRetrieval)
                    {
                        var row = string.Concat(list.DocName + ", ", string.Join(", ", list.Tokens));
                        csv.WriteComment(row);
                        csv.NextRecord();
                    }
                }
            }
        }
    }
}