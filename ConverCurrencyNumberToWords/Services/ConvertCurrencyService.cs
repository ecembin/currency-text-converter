using ConverCurrencyNumberToWords.ServiceContracts;
using System.Globalization;
using System.Net;


namespace ConverCurrencyNumberToWords.Services
{
    public class ConvertCurrencyService : IConvertCurrencyService
    {
        static string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        static string[] teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        static string[] tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };



        public string ConvertToWords(string amount)
        {
            string result = "";
            try {
                if (string.IsNullOrWhiteSpace(amount))
                    throw new Exception("Amount cannot be empty");

                string[] splitAmount = amount.Replace(" ", "").Split(',');
                int dollars = int.Parse(splitAmount[0]);
                int cents = splitAmount.Length > 1 ? int.Parse(splitAmount[1]) : 0;


                if (splitAmount.Length == 0)
                    return "Invalid input";

                if (splitAmount.Length > 2)
                    return "Invalid input. Only one comma or dot allowed.";

                if (!int.TryParse(splitAmount[0], NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out dollars))
                    return "Invalid input";

                if (splitAmount.Length == 2)
                {
                    if (splitAmount[1].Length == 1)
                        splitAmount[1] += "0";
                    if (!int.TryParse(splitAmount[1], out cents))
                        return "Invalid input";
                }

                if (dollars == 0 && cents == 0)
                    result = "zero dollars";

                if (dollars > 0)
                    result += ConvertNumber(dollars) + " dollars";

                if (cents > 0)
                {
                    if (dollars > 0)
                        result += " and";

                    result += " " + ConvertNumber(cents) + " cents";
                }
            }

            catch(Exception ex) {

                throw new Exception($"An error occurred during the conversion process.Error: {ex.Message}");
            }
            return  result;
        }

        private string ConvertNumber(int number)
        {
            if (number < 10)
                return ones[number];

            if (number < 20)
                return teens[number - 10];

            if (number < 100)
                return tens[number / 10] + (number % 10 != 0 ? "-" + ones[number % 10] : "");

            if (number < 1000)
                return ones[number / 100] + " hundred" + (number % 100 != 0 ? " " + ConvertNumber(number % 100) : "");

            if (number < 1000000)
                return ConvertNumber(number / 1000) + " thousand" + (number % 1000 != 0 ? " " + ConvertNumber(number % 1000) : "");

            if (number < 1000000000)
                return ConvertNumber(number / 1000000) + " million" + (number % 1000000 != 0 ? " " + ConvertNumber(number % 1000000) : "");

            return "";
        }
    }
}
