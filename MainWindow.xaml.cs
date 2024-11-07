using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Currency_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string Culture = "uk-UA";
        private string? _defaultResultValue;
        private readonly Brush _defaultResultBrush;

        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
            BindCultureInfo(Culture);
            _defaultResultValue = _resultLbl.Content.ToString();
            _defaultResultBrush = _resultLbl.Foreground;
            _currencyAmountInput.Focus();
        }

        private void BindCultureInfo(string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("SAR", 20);
            dtCurrency.Rows.Add("POUND", 5);
            dtCurrency.Rows.Add("DEM", 43);

            BindComboBox(_sourceCurrencyCmb, dtCurrency);
            BindComboBox(_targetCurrencyCmb, dtCurrency);
        }

        private void BindComboBox(ComboBox comboBox, DataTable dataTable)
        {
            comboBox.ItemsSource = dataTable.DefaultView;
            comboBox.DisplayMemberPath = "Text";
            comboBox.SelectedValuePath = "Value";
            comboBox.SelectedIndex = 0;
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush resetColor = new SolidColorBrush(Colors.Black);
            UpdateResultLabel(_defaultResultValue, resetColor);
            _currencyAmountInput.Clear();
            _sourceCurrencyCmb.SelectedIndex = 0;
            _targetCurrencyCmb.SelectedIndex = 0;

        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (IsAllOptionsCorrect())
            {
                decimal currencyInput = Convert.ToDecimal(_currencyAmountInput.Text);
                decimal currencyFromValue = Convert.ToDecimal(_sourceCurrencyCmb.SelectedValue.ToString());
                decimal convertedToValue = Convert.ToDecimal(_targetCurrencyCmb.SelectedValue.ToString());
                string convertationResult = ConvertCurrency(currencyInput, currencyFromValue, convertedToValue).ToString("N3");

                string? fromCurrencyName = GetDataTextFromCmb(_sourceCurrencyCmb);
                string? toCurrencyName = GetDataTextFromCmb(_targetCurrencyCmb);

                string resultLabel = $"{_currencyAmountInput.Text} {fromCurrencyName} = {convertationResult} {toCurrencyName}";
                UpdateResultLabel(resultLabel, _defaultResultBrush);
            }
        }

        private string? GetDataTextFromCmb(ComboBox cmb)
        {
            DataRowView rowView = (DataRowView)cmb.SelectedItem;
            return rowView["Text"].ToString();
        }

        private decimal ConvertCurrency(decimal currencyAmount, decimal sourceCurrency, decimal targetCurrency)
        {
            return (currencyAmount * sourceCurrency) / targetCurrency;
        }

        private bool IsAllOptionsCorrect()
        {
            bool isCorrect = true;
            string messageBoxTitle = "Info";

            if ((_currencyAmountInput.Text == null) || (_currencyAmountInput.Text.Trim() == string.Empty))
            {
                DisplayMessageBox("Please Enter Currency", messageBoxTitle);
                _currencyAmountInput.Focus();
                isCorrect = false;
            }
            else if ((_sourceCurrencyCmb.SelectedValue == null) || (_sourceCurrencyCmb.SelectedIndex == 0))
            {
                DisplayMessageBox("Please Enter Currency FROM", messageBoxTitle);
                _sourceCurrencyCmb.Focus();
                isCorrect = false;
            }
            else if ((_targetCurrencyCmb.SelectedValue == null) || (_targetCurrencyCmb.SelectedIndex == 0))
            {
                DisplayMessageBox("Please Enter Currency TO", messageBoxTitle);
                _targetCurrencyCmb.Focus();
                isCorrect = false;
            }

            return isCorrect;
        }

        private void DisplayMessageBox(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateResultLabel(string? value, Brush brush)
        {
            _resultLbl.Foreground = brush;
            _resultLbl.Content = value;
        }

        private void ValidateCurrencyAmountInput(object sender, TextCompositionEventArgs e)
        {
            Regex rg = new Regex("(^[0-9]+)(,?([0-9]+)?)$");
            e.Handled = !rg.IsMatch(_currencyAmountInput.Text + e.Text);
        }
    }
}