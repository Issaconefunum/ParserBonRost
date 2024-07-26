using ApiTestParser.Models;
using ApiTestParser.Models.Card;

namespace ApiTestParser.Services.ParserService
{
    public interface IParserWorker
    {
        public Task<List<BonrostCard>> GetProductsParse();
        public Task<BonrostCard> GetProductsParse(string Url);
    }
}
