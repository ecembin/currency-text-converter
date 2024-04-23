using ConverCurrencyNumberToWords.ServiceContracts;
using ConvertCurrencyAmountToWords.Response;
using Microsoft.AspNetCore.Mvc;


namespace ConverCurrencyNumberToWords.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertCurrencyController : ControllerBase
    {
        private readonly ILogger<ConvertCurrencyController> _logger;
        private readonly IConvertCurrencyService _convertCurrencyService;

        public ConvertCurrencyController(ILogger<ConvertCurrencyController> logger, IConvertCurrencyService convertCurrencyService)
        {
            _logger = logger;
            _convertCurrencyService = convertCurrencyService;
        }

        [HttpGet("ConvertToAmount")]
        public IActionResult ConvertToWords(string amount)
        {
            var result =   _convertCurrencyService.ConvertToWords(amount);

            var response = new ConvertToWordsResponse()
            {
                converterResult = result
            };
            return Ok(response);
        }


    }
}
