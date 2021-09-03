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
            string pdfsPath = @"D:\Downloads\coisas pessoais\unisinos\desenvolvimento de app para mineracao de texto em c#\Projeto Final - Dataset";

            if (!Directory.Exists(path: pdfsPath))
                throw new Exception("Invalid path, dir not found!");

            _logger.LogInformation("Type a query string");
            // string queryString = Console.ReadLine();
            string queryString = "tal AND txal";

            if (string.IsNullOrEmpty(queryString))
                throw new Exception("Query string empty");

            Generate(pdfsPath, queryString, CheckQueryString(queryString));
        }

        private void Generate(string pdfsPath, string queryString, Dictionary<string, int> keyWords)
        {
            _logger.LogInformation("Pipeline setup...");

            var p = new Pipeline<PdfParseRequest, PdfParseResponse>()
                .Add(new PdfParse(_config, _loggerFactory))
                .Add<PdfParseResponse, RankRequest>(Convert)
                .Add(new Rank(_config, _loggerFactory))
                .Add<RankResponse, CSVParseRequest>(Convert)
                .Add(new CSVParse(_config, _loggerFactory))
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
                
            };
        }

        private CSVParseRequest Convert(RankResponse input)
        {
            return new CSVParseRequest
            {

            };
        }
             
        #endregion

        #region Handling

        /// <summary>
        /// Check if query consist of an operation AND or spaces
        /// in case that there are two different operations, an exception is throwing     
        /// </summary>
        private Dictionary<string, int> CheckQueryString(string queryString)
        {
            var keyWords = new Dictionary<string, int>();

            if (Regex.IsMatch(queryString, @"\bAND\b") || queryString.Contains(" "))
                keyWords = SplitQuery(queryString);
            else 
                keyWords.Add(queryString, 0);
            
            return keyWords;
        }

        /// <summary>
        /// This method will return a tuple with:
        ///     a dictionary that represent each word to find 
        ///     an integer that represent what operation is
        /// </summary>
        private Dictionary<string, int> SplitQuery(string queryString)
        {
            var keyValues = new Dictionary<string, int>();

            if (Regex.IsMatch(queryString, @"\bAND\b"))
            {
                foreach (var i in queryString.Split("AND"))
                {
                    if (!i.Trim().Any(Char.IsUpper))
                        keyValues.Add(i.Trim(), 0);
                    else
                        throw new Exception("Strings must to be entire lower case");
                }
            }
            else
            {
                foreach(var i in queryString.Split(new char[0])) // whitespace
                {
                    if (!i.Trim().Any(Char.IsUpper))
                        keyValues.Add(i.Trim(), 0);
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