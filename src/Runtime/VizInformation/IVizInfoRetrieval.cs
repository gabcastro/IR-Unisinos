using System.Collections.Generic;
using InformationRetrieval.Pipelining;

namespace InformationRetrieval.Runtime.VizInformation
{
    internal interface IVizInfoRetrieval : IFilter
    {
        VizInfoRetrievalResponse Generator(VizInfoRetrievalRequest request);
    }

    class VizInfoRetrievalRequest
    {
        public string QueryString { get; set; }
        public List<string> PackFilePath { get; set; }
        public List<int> Similarity { get; set; }
    }

    class VizInfoRetrievalResponse
    {
        public bool IsOk { get; set; }
    }
}