using AngleSharp.Html.Dom;
using ApiTestParser.Models.Card;

namespace ApiTestParser.Services.ParserService
{
    internal interface IParser
    {
        List<BonrostCard> ParseProducts(IHtmlDocument document);
        BonrostCard ParseProductDetails(IHtmlDocument document);
    }
}
