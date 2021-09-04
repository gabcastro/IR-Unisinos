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
        public List<string> KeyWords { get; set; }
    }

    class PdfParseResponse
    {
        public List<string> PackFilesPath { get; set; }
        public List<string> QueryString { get; set; }
        public List<List<string>> OrganizedPdfsContent { get; set; }
    }

}