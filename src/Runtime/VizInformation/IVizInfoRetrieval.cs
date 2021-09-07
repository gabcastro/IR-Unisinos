using System.Collections.Generic;
using InformationRetrieval.Pipelining;
using InformationRetrieval.Runtime.RankManager;

namespace InformationRetrieval.Runtime.VizInformation
{
    internal interface IVizInfoRetrieval : IFilter
    {
        VizInfoRetrievalResponse Generator(VizInfoRetrievalRequest request);
    }

    class VizInfoRetrievalRequest
    {
        public string QueryString { get; set; }
        public List<RankRetrieval> ListRankRetrieval { get; set; }
    }

    class VizInfoRetrievalResponse
    {
        public bool IsOk { get; set; }
    }
}