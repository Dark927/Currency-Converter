namespace Currency_Converter
{
    public class CurrencyConverter
    {
        private decimal _lastAmount;
        private decimal _lastResult;

        public CurrencyConverter()
        {
        }

        public decimal Convert(decimal amount, decimal sourceCurrencyRate, decimal targetCurrencyRate)
        {
            _lastAmount = amount;
            _lastResult = (amount * targetCurrencyRate) / sourceCurrencyRate;

            return _lastResult;
        }

        public string GenerateResultLabel(string? fromCurrencyName, string? toCurrencyName, int resultPrecision)
        {
            string resultFormat = "N" + resultPrecision.ToString();
            return $"{_lastAmount} {fromCurrencyName} = {_lastResult.ToString(resultFormat)} {toCurrencyName}";
        }
    }
}
