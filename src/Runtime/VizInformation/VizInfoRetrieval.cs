using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace InformationRetrieval.Runtime.VizInformation
{
    internal class VizInfoRetrieval : IVizInfoRetrieval
    {
        private readonly ILogger<VizInfoRetrieval> _logger;

        public VizInfoRetrieval(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<VizInfoRetrieval>();
        }
        
        public dynamic Execute(dynamic inputData)
        {
            return Generator(inputData);
        }

        public VizInfoRetrievalResponse Generator(VizInfoRetrievalRequest request)
        {
            StringBuilder strBuffer = new StringBuilder();

            strBuffer.AppendLine("╔═══════════════════════╦═══════════════════════╦═══════════════╦══┈┈┈┈┈┈┈┈┈┈⇝");
            strBuffer.AppendLine("║ Doc.\t\t\t║\tSimilarity\t║\tRank\t║ QueryString");
            strBuffer.AppendLine("╠═══════════════════════╬═══════════════════════╬═══════════════╬══┈┈┈┈┈┈┈┈┈┈⇝");
            
            foreach (var i in request.ListRankRetrieval)
                strBuffer.AppendLine("║ " + i.DocName.Substring(0, i.DocName.Length < 20 ? i.DocName.Length : 20) + "...\t" + "║\t\t" + i.Similarity + "\t║\t" + i.Ranking + "\t║ " + request.QueryString);

            strBuffer.AppendLine("╚═══════════════════════╩═══════════════════════╩═══════════════╩══┈┈┈┈┈┈┈┈┈┈⇝");
            
            Console.WriteLine(strBuffer.ToString());

            return new VizInfoRetrievalResponse { IsOk = true };
        }
    }
}