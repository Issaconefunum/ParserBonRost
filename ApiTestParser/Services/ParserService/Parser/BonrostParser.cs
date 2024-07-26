using AngleSharp.Html.Dom;
using ApiTestParser.ConfigurationManager;
using ApiTestParser.Models.Card;
using System.Net.Sockets;
using System.Text.RegularExpressions;
namespace ApiTestParser.Services.ParserService.Parser

{
    public class BonrostParser : IParser
    {
        public List<BonrostCard> ParseRoducts(IHtmlDocument document)
        {
            var ticket = document.QuerySelectorAll("tbody");
            if (ticket == null) throw new InvalidDataException("Data not found");

            var BonrostCardList = new List<BonrostCard>();
            foreach (var card in ticket)
            {
                var name = card.QuerySelectorAll("td.product-header.pr_otstup");
                var producer = card.QuerySelectorAll("td.proizvoditel");
                var priceKg = card.QuerySelectorAll("td.product-price").Where(m => m.GetAttribute("aria-label") == "Цена руб/кг");
                var priceMeter = card.QuerySelectorAll("td.product-price").Where(m => m.GetAttribute("aria-label") == "Цена руб/м");
                var link = card.QuerySelectorAll("a").Where(m => m.GetAttribute("class") != "collapse_a");

                for (int i = 0; i < (name.Length < Configurations.ParserSettings.MaxProductCount ? name.Length : Configurations.ParserSettings.MaxProductCount); i++)
                {
                    var bonrostCard = new BonrostCard();
                    bonrostCard.Name = DeleateExtraCharacters(name.ElementAt(i)?.TextContent ?? "Not Found");
                    bonrostCard.Producer = DeleateExtraCharacters(producer.ElementAt(i)?.TextContent ?? "Not Found");
                    bonrostCard.PriceKg = DeleateExtraCharacters(priceKg.ElementAt(i)?.TextContent ?? "Not Found");
                    bonrostCard.PriceMeter = DeleateExtraCharacters(priceMeter.ElementAt(i)?.TextContent ?? "Not Found");
                    bonrostCard.Link = DeleateExtraCharacters(link.ElementAt(i)?.GetAttribute("href") ?? "Not Found");
                    BonrostCardList.Add(bonrostCard);
                }
            }
            return BonrostCardList;
        }

        public BonrostCard ParseProductDetails(IHtmlDocument document)
        {
            if (document == null) throw new InvalidDataException("Data not found");

            var bonrostCard = new BonrostCard();
            var price = document.QuerySelectorAll("td.product-price");
            bonrostCard.Name = DeleateExtraCharacters(document.QuerySelector("td.product-header").TextContent ?? "Not Found");
            bonrostCard.Producer = DeleateExtraCharacters(document.QuerySelector("td.proizvoditel").TextContent ?? "Not Found");
            bonrostCard.PriceKg = DeleateExtraCharacters(price[0].TextContent ?? "Not Found");
            bonrostCard.PriceMeter = DeleateExtraCharacters(price[1].TextContent ?? "Not Found");
            var link = document.QuerySelector("div.list_catalog_button");
            bonrostCard.Link = DeleateExtraCharacters(link.QuerySelector("a").GetAttribute("href") ?? "Not Found");

            return bonrostCard;
        }

        public string DeleateExtraCharacters(string? request)
        {
            if (string.IsNullOrEmpty(request)) return "Data not found"; // Решил сделать так, чтобы данные все равно писались

            string pattern = @"\s+";
            string replacement = " ";

            return Regex.Replace(request, pattern, replacement).Trim();
        }
    }
}
