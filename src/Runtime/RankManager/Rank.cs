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
            // var vectorSpaceStemming = Stemming(stopWordsDocuments); // TODO: create a logic to this func and replace stopWordsDocuments to vectorSpaceStemming
            var similarity = Similarity(stopWordsDocuments, request.QueryString);
            var organizedElements = GroupBy(request.PackFilesPath, stopWordsDocuments, similarity);
            
            return new RankResponse {
                QueryString = string.Join(" AND ", request.QueryString),
                ListRankRetrieval = organizedElements
            };
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

        private List<List<string>> Stemming(List<List<string>> documents)
        {
            return new List<List<string>>();
        }

        /// <summary>
        /// Responsability
        ///     calculate the similarity between the search string and each of the documents
        ///     each document contains an unique element from a word, so for each word of a query string, it will be compare to know if is present 
        ///     if present, counts a new increment
        /// </summary>
        private List<int> Similarity(List<List<string>> documents, List<string> queryString)
        {
            int wordPresent = 0;
            var listSimilarity = new List<int>();

            foreach(var vectorDocuments in documents)
            {
                foreach(var strQ in queryString)
                {
                    if (vectorDocuments.Any(x => x.Equals(strQ)))
                        wordPresent++;
                }
                listSimilarity.Add(wordPresent);
                wordPresent = 0;
            }

            return listSimilarity;
        }

        /// <summary>
        /// Responsability
        ///     organize all information into an object called RankRetrieval and make a rank by similarity
        /// </summary>
        private List<RankRetrieval> GroupBy(List<string> paths, List<List<string>> documents, List<int> similarity)
        {
            int auxIndex = 0, clist = 1;
            var rankRetrieval = new List<RankRetrieval>();
            var auxRankRetrieval = new List<RankRetrieval>();

            foreach (var vectorDocuments in documents)
            {
                string docName = paths[auxIndex].Split(@"\")[^1];

                rankRetrieval.Add(new RankRetrieval{
                    DocName = docName,
                    Tokens = vectorDocuments,
                    Similarity = similarity[auxIndex],
                    Ranking = 0
                });

                auxIndex++;
            }

            foreach(var r in rankRetrieval.OrderByDescending(x => x.Similarity).ToList())
                auxRankRetrieval.Add(new RankRetrieval{
                    DocName = r.DocName,
                    Tokens = r.Tokens,
                    Similarity = r.Similarity,
                    Ranking = clist++
                });
            
            return auxRankRetrieval;
        }
    }
}