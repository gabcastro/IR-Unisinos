using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using System.Linq;

namespace InformationRetrieval.Runtime.PdfManager
{
    internal class PdfParse : IPdfParse
    {
        private readonly ILogger<PdfParse> _logger;
        private readonly IConfigurationRoot _config;
        
        public PdfParse(IConfigurationRoot config, ILoggerFactory loggerFactory)
        {
            _config = config;
            _logger = loggerFactory.CreateLogger<PdfParse>();
        }

        public dynamic Execute(dynamic inputData)
        {
            return Generate(inputData);
        }

        public PdfParseResponse Generate(PdfParseRequest request)
        {
            try
            {
                var packFiles = new List<string>();
                var organizedPdfContent = new List<List<string>>();
                var lisQueryString = CleanQueryString(request.KeyWords);

                foreach (string file in Directory.GetFiles(request.PdfPath, "*.pdf"))
                    packFiles.Add(file);

                // TODO: uncomment this foreach to read all documents
                // foreach(var i in packFiles)
                //     organizedPdfContent.Add(CleanContent(i));
                
                organizedPdfContent.Add(CleanContent(packFiles[0]));
                
                return new PdfParseResponse {
                    PackFilesPath = packFiles,
                    QueryString = lisQueryString,
                    OrganizedPdfsContent = organizedPdfContent
                };   
            }
            catch (Exception e)
            {
                _logger.LogError("Error message: {0}", e.Message);
                throw;
            }
        }

        /// <summary>
        /// Responsability:
        ///     Read pdf file
        ///     Remove accents from all words
        ///     Return a list with entire phrase 
        /// </summary>
        private List<string> CleanContent(string pathPdf)
        {
            var tempList = new List<string>();

            var pdfDocument = new PdfDocument(new PdfReader(filename: pathPdf));
            var strategy = new LocationTextExtractionStrategy();
            
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
            {
                var page = pdfDocument.GetPage(i);
                string text = PdfTextExtractor.GetTextFromPage(page, strategy);
                string[] lines = text.Split('\n');
                
                foreach(string line in lines)
                {
                    if (CheckContentLine(line))
                    {
                        string lowerCaseTextString = RemoveDiacritics(line.ToLower());
                        tempList.AddRange (
                            from l in lowerCaseTextString.Split(new char[] {' ', '(', ')', ',', '.'}) 
                            where l.Length > 0
                            select l.Trim()
                        );
                    }
                }
            }

            return tempList.Distinct().ToList();
        }

        /// <summary>
        /// Remove accents from a string
        /// <summary>
        private string RemoveDiacritics(string text) 
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Check if a line contains some of the phrases that appear at header and footer
        /// TODO: find a way more flexible
        /// </summary>
        private bool CheckContentLine(string line)
        {
            return line switch
            {
                string a when a.Contains("Desenvolvimento de Aplicações para Mineração de Texto") => false,
                string b when b.Contains("em C#") => false,
                string c when c.Contains("Márcio Garcia Martins e Luciano Ignaczak") => false,
                string d when d.Contains("Fonte: https://") => false,
                string e when e.Contains(".ghtml") => false,
                string f when f.Contains((char)32) && f.Length == 1 => false, // new line
                _ => true,
            };
        }

        private List<string> CleanQueryString(List<string> lQueryString)
        {
            var tempList = new List<string>();

            foreach(var i in lQueryString)
                tempList.Add(RemoveDiacritics(i));

            return tempList;
        }
    }
}