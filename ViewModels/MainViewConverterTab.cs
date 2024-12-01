using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Currency_Converter.ViewModels
{
    public class MainViewConverterTab : MainViewBase
    {
        private CurrencyConverter _converter;
        private TextBox _inputValueTb;
        private ComboBox _selectedSourceCurrencyCmb;
        private ComboBox _selectedTargetCurrencyCmb;

        private decimal _lastResult;

        public MainViewConverterTab(MainWindow window) : base(window)
        {
            _converter = new CurrencyConverter();
            _inputValueTb = window.CurrencyAmountInput;
            _selectedSourceCurrencyCmb = window.SourceCurrencyCmb;
            _selectedTargetCurrencyCmb = window.TargetCurrencyCmb;
            _lastResult = 0;
        }

        public override void BindData(object data)
        {
            BindComboBox(_selectedSourceCurrencyCmb, data);
            BindComboBox(_selectedTargetCurrencyCmb, data);
        }

        public override void Clear(object? parameter = null)
        {
            Window.ConverterResetResult();

            _inputValueTb.Clear();
            _selectedSourceCurrencyCmb.SelectedIndex = 0;
            _selectedTargetCurrencyCmb.SelectedIndex = 0;
        }

        public override void Confirm(object? parameter = null)
        {
            if (IsInputCorrect())
            {
                ConvertCurrency();
            }
            else
            {
                DisplayIncorrectInput();
            }
        }

        private void ConvertCurrency()
        {
            decimal amount = Convert.ToDecimal(_inputValueTb.Text);
            decimal fromCurrencyRate = Convert.ToDecimal(_selectedSourceCurrencyCmb.SelectedValue);
            decimal toCurrencyRate = Convert.ToDecimal(_selectedTargetCurrencyCmb.SelectedValue);

            decimal result = _converter.Convert(amount, fromCurrencyRate, toCurrencyRate);

            string? fromCurrencyName = GetDataTextFromCmb(_selectedSourceCurrencyCmb);
            string? toCurrencyName = GetDataTextFromCmb(_selectedTargetCurrencyCmb);

            string resultLabel = _converter.GenerateResultLabel(fromCurrencyName, toCurrencyName, 3);

            Window.UpdateResultLabel(resultLabel);
        }

        private string? GetDataTextFromCmb(ComboBox cmb)
        {
            CurrencyMaster master = (CurrencyMaster)cmb.SelectedItem;
            return master.CurrencyName;
        }

        private bool IsInputCorrect()
        {
            bool isCorrect = IsInputCorrect(_inputValueTb)
                && IsCurrencySelectedCorrectly(_selectedSourceCurrencyCmb)
                && IsCurrencySelectedCorrectly(_selectedTargetCurrencyCmb);

            return isCorrect;
        }

        private void DisplayIncorrectInput()
        {
            string messageBoxInfo = string.Empty;

            if (!IsInputCorrect(_inputValueTb))
            {
                messageBoxInfo = "Please Enter Currency Amount";
                _inputValueTb.Focus();
            }
            else if (!IsCurrencySelectedCorrectly(_selectedSourceCurrencyCmb))
            {
                messageBoxInfo = "Please Enter Currency FROM";
                _selectedSourceCurrencyCmb.Focus();
            }
            else if (!IsCurrencySelectedCorrectly(_selectedTargetCurrencyCmb))
            {
                messageBoxInfo = "Please Enter Currency TO";
                _selectedTargetCurrencyCmb.Focus();
            }
            MessageBox.Show(messageBoxInfo, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsCurrencySelectedCorrectly(ComboBox comboBox) => !((comboBox.SelectedValue == null) || (comboBox.SelectedIndex == 0));
        private bool IsInputCorrect(TextBox textBox) => !((textBox.Text == null) || (textBox.Text.Trim() == string.Empty));

        private void BindComboBox(ComboBox comboBox, object data)
        {
            ObservableCollection<CurrencyMaster> dataList = (ObservableCollection<CurrencyMaster>)data;
            comboBox.ItemsSource = dataList;
            comboBox.DisplayMemberPath = nameof(CurrencyMaster.CurrencyName);
            comboBox.SelectedValuePath = nameof(CurrencyMaster.Amount);
            comboBox.SelectedIndex = 0;
        }
    }
}
