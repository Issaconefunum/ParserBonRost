using ApiTestParser.Services.ParserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ApiTestParser.ConfigurationManager;
using ApiTestParser.Models.Card;
using AngleSharp;
using Microsoft.VisualBasic;
using ApiTestParser.Models;
namespace ApiTestParser.Controllers
{
    [Route("/api/products/")]
    public class ApiController : Controller
    {
        private readonly IParserWorker _parserWorker;
        //;
        public ApiController(IParserWorker parserWorker)
        {
            _parserWorker = parserWorker;
        }
        void StopFunction(object state)
        {
            throw new InvalidDataException("Waiting limit exceeded");
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchProducts([FromBody] string phrase)
        {
            Timer timer = new Timer(StopFunction, null, Configurations.ParserSettings.WaitTimeout*100, Timeout.Infinite);//Создает таймер, который через определенное время закончит выполнение метода, если тот не успеет отработать
            var variants = new object[2];
            var phraseList = new Dictionary<string, string>() { { "phrase", phrase } };
    
            List<BonrostCard> content;
            try { content = await _parserWorker.GetProductsParse(); }
            catch { throw new InvalidDataException("Data not found"); }

            variants[0] = phraseList;
            variants[1] = content.Where(i => i.Name.Contains(phrase ?? string.Empty));

            return Ok(variants);
        }

        [HttpPost("details")]
        public async Task<IActionResult> DetailsProduct([FromBody] string phrase)
        {
            Timer timer = new Timer(StopFunction, null, Configurations.ParserSettings.WaitTimeout * 100, Timeout.Infinite);//Создает таймер, который через определенное время закончит выполнение метода, если тот не успеет отработать

            BonrostCard content;
            try { content = await _parserWorker.GetProductsParse(phrase); }
            catch { throw new InvalidDataException("Data not found"); }

            return Ok(content);
        }
    }
}
