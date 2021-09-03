using System.Collections.Generic;
using InformationRetrieval.Pipelining;

namespace InformationRetrieval.Runtime.PdfManager
{
    internal interface IPdfParse : IFilter
    {
        PdfParseResponse Generate(PdfParseRequest request);
    }

    class PdfParseRequest
    {
        public string PdfPath { get; set; }
        public string QueryString { get; set; }
        public Dictionary<string, int> KeyWords { get; set; }
    }

    class PdfParseResponse
    {
        public string PdfName { get; set; }
        public string QueryString { get; set; }
        public string WordsCount { get; set; }
    }

}