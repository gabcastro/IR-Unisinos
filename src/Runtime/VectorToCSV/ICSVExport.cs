using System.Collections.Generic;
using InformationRetrieval.Pipelining;
using InformationRetrieval.Runtime.RankManager;

namespace InformationRetrieval.Runtime.VectorToCSV
{
    internal interface ICSVExport : IFilter
    {
        CSVExportRespose Generate(CSVExportRequest request);
    }

    class CSVExportRequest
    {
        public string QueryString { get; set; }
        public List<RankRetrieval> ListRankRetrieval { get; set; }
    }

    class CSVExportRespose
    {
        public string QueryString { get; set; }
        public List<string> PackFilePath { get; set; }
        public List<int> Similarity { get; set; }
    }
}