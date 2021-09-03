using InformationRetrieval.Pipelining;

namespace InformationRetrieval.Runtime.RankManager
{
    internal interface IRank : IFilter
    {
        RankResponse Generate(RankRequest request);
    }

    class RankRequest
    {

    }

    class RankResponse
    {
        
    }
}