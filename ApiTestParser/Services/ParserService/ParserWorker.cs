using AngleSharp.Html.Parser;
using ApiTestParser.ConfigurationManager;
using AngleSharp;
using AngleSharp.Html.Dom;
//using Serilog;
using ApiTestParser.Models.Card;
//using Elastic.CommonSchema;
using AngleSharp.Browser;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using ApiTestParser.Models;
namespace ApiTestParser.Services.ParserService
{
    class ParserWorker : IParserWorker
    {
        IParser parser;

        public ParserWorker(IParser parser)
        {
            this.parser = parser;

        }
        public async Task<List<BonrostCard>> GetProductsParse()
        {

            var list = new List<BonrostCard>();
            var source = await HtmlLoader.GetSourceByPageId(Configurations.ParserSettings.Url);
            if (string.IsNullOrEmpty(source)) throw new InvalidOperationException("Data source not found");
            var domParser = new HtmlParser();

            var document = await domParser.ParseDocumentAsync(source);

            var result = parser.ParseProducts(document);
            list.AddRange(result);
            return list;

        }
        public async Task<BonrostCard> GetProductsParse(string urlProduct)
        {

            var source = await HtmlLoader.GetSourceByPageId(Configurations.ParserSettings.Url, urlProduct);
            if (string.IsNullOrEmpty(source)) throw new InvalidOperationException("Data source not found");
            var domParser = new HtmlParser();

            var document = await domParser.ParseDocumentAsync(source);
            var result = parser.ParseProductDetails(document);
            return result;
        }
    }
}
