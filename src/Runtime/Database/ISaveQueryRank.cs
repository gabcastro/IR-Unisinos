using System.Collections.Generic;
using InformationRetrieval.Pipelining;

namespace InformationRetrieval.Runtime.Database
{
    internal interface ISaveQueryRank : IFilter
    {
        SaveQueryRankResponse Generate(SaveQueryRankRequest request);
    }

    class SaveQueryRankRequest
    {
        public string QueryString { get; set; }
        public List<string> PackFilePath { get; set; }
        public List<List<string>> VectorSpace { get; set; }
        public List<int> Similarity { get; set; }
        public List<int> Rank { get; set; }
    }

    class SaveQueryRankResponse
    {
        public string QueryString { get; set; }
        public List<string> PackFilePath { get; set; }
        public List<int> Similarity { get; set; }
        public List<int> Rank { get; set;}
    }
}