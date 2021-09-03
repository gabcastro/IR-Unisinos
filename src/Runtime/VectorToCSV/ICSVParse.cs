using InformationRetrieval.Pipelining;

namespace InformationRetrieval.Runtime.VectorToCSV
{
    internal interface ICSVParse : IFilter
    {
        CSVParseRespose Generate(CSVParseRequest request);
    }

    class CSVParseRequest
    {

    }

    class CSVParseRespose
    {

    }
}