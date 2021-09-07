using System.Collections.Generic;
using InformationRetrieval.Pipelining;
using InformationRetrieval.Runtime.RankManager;

namespace InformationRetrieval.Runtime.Database
{
    internal interface ISaveQueryRank : IFilter
    {
        SaveQueryRankResponse Generate(SaveQueryRankRequest request);
    }

    class SaveQueryRankRequest
    {
        public string QueryString { get; set; }
        public List<RankRetrieval> ListRankRetrieval { get; set; }
    }

    class SaveQueryRankResponse
    {
        public string QueryString { get; set; }
        public List<RankRetrieval> ListRankRetrieval { get; set; }
    }
}