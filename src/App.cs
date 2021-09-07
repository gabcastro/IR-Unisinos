using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using InformationRetrieval.Pipelining;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InformationRetrieval.Runtime.PdfManager;
using InformationRetrieval.Runtime.RankManager;
using InformationRetrieval.Runtime.VectorToCSV;
using InformationRetrieval.Runtime.Database;
using InformationRetrieval.Runtime.VizInformation;

namespace InformationRetrieval
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfigurationRoot _config;

        public App(IConfigurationRoot config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<App>();
        }

        public async Task Execute()
        {
            _logger.LogInformation("Type dataset path (pdf files)");
            // string pdfsPath = Console.ReadLine();
            string pdfsPath = @"E:\vitor_desktop\iCybersec\C#\Projeto Final - Dataset";

            if (!Directory.Exists(path: pdfsPath))
                throw new Exception("Invalid path, dir not found!");

            _logger.LogInformation("Type a query string");
            // string queryString = Console.ReadLine();
            string queryString = "manhã AND brasil AND natação AND australiano AND inferiores AND ultrapassar";

            if (string.IsNullOrEmpty(queryString))
                throw new Exception("Query string empty");

            Generate(pdfsPath, queryString, CheckQueryString(queryString));
        }

        private void Generate(string pdfsPath, string queryString, List<string> keyWords)
        {
            _logger.LogInformation("Pipeline setup...");

            var p = new Pipeline<PdfParseRequest, VizInfoRetrievalResponse>()
                .Add(new PdfParse(_loggerFactory))
                .Add<PdfParseResponse, RankRequest>(Convert)
                .Add(new Rank(_config, _loggerFactory))
                .Add<RankResponse, CSVExportRequest>(Convert)
                .Add(new CSVExport(_loggerFactory))
                .Add<CSVExportRespose, SaveQueryRankRequest>(Convert)
                .Add(new SaveQueryRank(_config, _loggerFactory))
                .Add<SaveQueryRankResponse, VizInfoRetrievalRequest>(Convert)
                .Add(new VizInfoRetrieval(_loggerFactory))
            ;

            p.PipelineExecuting += PipelineExecutingLog;

            using (p)
            {
                // first request
                var request = new PdfParseRequest
                {
                    PdfPath = pdfsPath,
                    QueryString = queryString,
                    KeyWords = keyWords
                };

                p.Execute(request);
            }
        }

        #region Convert executions

        private RankRequest Convert(PdfParseResponse input)
        {
            return new RankRequest
            {
                PackFilesPath = input.PackFilesPath,
                QueryString = input.QueryString,
                OrganizedPdfsContent = input.OrganizedPdfsContent
            };
        }

        private CSVExportRequest Convert(RankResponse input)
        {
            return new CSVExportRequest
            {
                QueryString = input.QueryString,
                ListRankRetrieval = input.ListRankRetrieval
            };
        }

        private SaveQueryRankRequest Convert(CSVExportRespose input)
        {
            return new SaveQueryRankRequest
            {
                QueryString = input.QueryString,
                ListRankRetrieval = input.ListRankRetrieval
            };
        }

        private VizInfoRetrievalRequest Convert(SaveQueryRankResponse input)
        {
            return new VizInfoRetrievalRequest
            {
                QueryString = input.QueryString,
                ListRankRetrieval = input.ListRankRetrieval
            };
        }

        #endregion

        #region Handling

        /// <summary>
        /// Check if query consist of an operation AND or spaces
        /// in case that there are two different operations, an exception is throwing     
        /// </summary>
        private List<string> CheckQueryString(string queryString)
        {
            var keyWords = new List<string>();

            if (Regex.IsMatch(queryString, @"\bAND\b") || queryString.Contains(" "))
                keyWords = SplitQuery(queryString);
            else
                keyWords.Add(queryString);

            return keyWords;
        }

        /// <summary>
        /// This method will return a list with each word of query string
        /// </summary>
        private List<string> SplitQuery(string queryString)
        {
            var keyValues = new List<string>();

            if (Regex.IsMatch(queryString, @"\bAND\b"))
            {
                foreach (var i in queryString.Split("AND"))
                {
                    if (!i.Trim().Any(Char.IsUpper))
                        keyValues.Add(i.Trim());
                    else
                        throw new Exception("Strings must to be entire lower case");
                }
            }
            else
            {
                foreach (var i in queryString.Split(new char[0])) // whitespace
                {
                    if (!i.Trim().Any(Char.IsUpper))
                        keyValues.Add(i.Trim());
                    else
                        throw new Exception("Strings must to be entire lower case");
                }
            }

            return keyValues;
        }

        #endregion

        protected void PipelineExecutingLog(IFilter filter)
        {
            var msg = $"Step: {filter.GetType().Name}";

            _logger.LogInformation(msg);
        }
    }
}