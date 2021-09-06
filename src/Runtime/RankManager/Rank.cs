using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InformationRetrieval.Runtime.RankManager
{
    internal class Rank : IRank
    {
        private readonly ILogger<Rank> _logger;
        private readonly IConfigurationRoot _config;

        public Rank(IConfigurationRoot config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _logger = loggerFactory.CreateLogger<Rank>();
        }

        public dynamic Execute(dynamic inputData)
        {
            return Generate(inputData);
        }

        public RankResponse Generate(RankRequest request)
        {
            var stopWordsDocuments = RemoveStopWords(request.OrganizedPdfsContent);
            Stemming(stopWordsDocuments);
            return new RankResponse {};
        }

        private List<List<string>> RemoveStopWords(List<List<string>> documents)
        {
            var stopWords = _config.GetSection("Stopwords").Get<string[]>();

            List<List<string>> auxListDocuments = new List<List<string>>();
            foreach (var ii in documents)
                auxListDocuments.Add(new List<string>(ii)); // necessary to not copy the reference from the main obj
            
            var auxRemoveTokens = new List<string>();
            int i = 0;

            foreach (var vectorDocuments in documents)
            {
                foreach (var token in vectorDocuments)
                {
                    if (stopWords.Any(t => t.Equals(token)))
                    {
                        auxListDocuments[i].RemoveAll(x => x.Equals(token));
                    }
                }
                i++;
            }

            return auxListDocuments;
        }

        private void Stemming(List<List<string>> documents)
        {

        }
    }
}