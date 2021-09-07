using System.Collections.Generic;
using InformationRetrieval.Pipelining;

namespace InformationRetrieval.Runtime.RankManager
{
    internal interface IRank : IFilter
    {
        RankResponse Generate(RankRequest request);
    }

    class RankRequest
    {
        public List<string> PackFilesPath { get; set; }
        public List<string> QueryString { get; set; }
        public List<List<string>> OrganizedPdfsContent { get; set; }
    }

    class RankResponse
    {
        public string QueryString { get; set; }
        public List<RankRetrieval> ListRankRetrieval { get; set; }
    }

    class RankRetrieval
    {
        public string DocName { get; set; }
        public List<string> Tokens { get; set; }
        public int Similarity { get; set; }
        public int Ranking { get; set; }
    }
}