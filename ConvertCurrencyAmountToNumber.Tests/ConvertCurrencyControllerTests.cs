

using ConverCurrencyNumberToWords.ServiceContracts;



namespace ConvertCurrencyAmountToNumber.Tests
{
    public class ConvertCurrencyIntegrationTests : 
    {
        private readonly IConvertCurrencyService _convertCurrencyService;

        public ConvertCurrencyIntegrationTests(IConvertCurrencyService convertCurrencyService)
        { 
             _convertCurrencyService = convertCurrencyService;

        }

        [Theory]
        [InlineData("25,1", "twenty-five dollars and ten cents")]
        [InlineData("0,01", "zero dollars and one cent")]
        [InlineData("45 100", "forty-five thousand one hundred dollars")]
        [InlineData("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public void ConvertCurrency_ReturnsCorrectWords(string amount, string expectedWords)
        {
            // Act
            var response = _convertCurrencyService.ConvertToWords(amount);

            // Assert
            Assert.Equal(expectedWords, response);
        }
    }
}