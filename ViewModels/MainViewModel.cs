using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Currency_Converter.ViewModels;

namespace Currency_Converter;

public class MainViewModel : INotifyPropertyChanged
{
    public enum TabType
    {
        Converter,
        Master,
    }

    #region Fields

    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly MainWindow _mainWindow;

    private Dictionary<TabType, MainViewBase> _mainViewTabsDict;

    #endregion


    #region Properties

    public MainWindow Window { get => _mainWindow; }
    public ICommand? ClearMainTabCommand { get; private set; }



    #endregion


    #region Methods

    public MainViewModel()
    {
        _mainWindow = (MainWindow)Application.Current.MainWindow;

        _mainViewTabsDict = new Dictionary<TabType, MainViewBase>
        {
            [TabType.Converter] = new MainViewConverterTab(Window),
        };

        InitCommands();
    }

    private void InitCommands()
    {
        ClearMainTabCommand = new RelayCommand(_mainViewTabsDict[TabType.Converter].Clear);
    }



    #endregion

    //private void BindCurrency()
    //{
    //    DataTable dtCurrency = new DataTable();
    //    dtCurrency.Columns.Add("Text");
    //    dtCurrency.Columns.Add("Value");

    //    dtCurrency.Rows.Add("--SELECT--", 0);
    //    dtCurrency.Rows.Add("INR", 1);
    //    dtCurrency.Rows.Add("USD", 75);
    //    dtCurrency.Rows.Add("EUR", 85);
    //    dtCurrency.Rows.Add("SAR", 20);
    //    dtCurrency.Rows.Add("POUND", 5);
    //    dtCurrency.Rows.Add("DEM", 43);

    //    BindComboBox(_sourceCurrencyCmb, dtCurrency);
    //    BindComboBox(_targetCurrencyCmb, dtCurrency);
    //}

    //private void BindComboBox(ComboBox comboBox, DataTable dataTable)
    //{
    //    comboBox.ItemsSource = dataTable.DefaultView;
    //    comboBox.DisplayMemberPath = "Text";
    //    comboBox.SelectedValuePath = "Value";
    //    comboBox.SelectedIndex = 0;
    //}




    //private void Convert_Click(object sender, RoutedEventArgs e)
    //{
    //    if (IsAllOptionsCorrect())
    //    {
    //        decimal currencyInput = Convert.ToDecimal(_currencyAmountInput.Text);
    //        decimal currencyFromValue = Convert.ToDecimal(_sourceCurrencyCmb.SelectedValue.ToString());
    //        decimal convertedToValue = Convert.ToDecimal(_targetCurrencyCmb.SelectedValue.ToString());
    //        string convertationResult = ConvertCurrency(currencyInput, currencyFromValue, convertedToValue).ToString("N3");

    //        string? fromCurrencyName = GetDataTextFromCmb(_sourceCurrencyCmb);
    //        string? toCurrencyName = GetDataTextFromCmb(_targetCurrencyCmb);

    //        string resultLabel = $"{currencyInput.ToString().Trim('0')} {fromCurrencyName} = {convertationResult} {toCurrencyName}";
    //        UpdateResultLabel(resultLabel, _defaultResultBrush);
    //    }
    //}

    //private string? GetDataTextFromCmb(ComboBox cmb)
    //{
    //    DataRowView rowView = (DataRowView)cmb.SelectedItem;
    //    return rowView["Text"].ToString();
    //}

    //private decimal ConvertCurrency(decimal currencyAmount, decimal sourceCurrency, decimal targetCurrency)
    //{
    //    return (currencyAmount * sourceCurrency) / targetCurrency;
    //}

    //private void btnSave_Click(object sender, RoutedEventArgs e)
    //{

    //}

    //private void btnCancel_Click(object sender, RoutedEventArgs e)
    //{

    //}

    //private bool IsAllOptionsCorrect()
    //{
    //    bool isCorrect = true;
    //    string messageBoxTitle = "Info";

    //    if ((_currencyAmountInput.Text == null) || (_currencyAmountInput.Text.Trim() == string.Empty))
    //    {
    //        DisplayMessageBox("Please Enter Currency", messageBoxTitle);
    //        _currencyAmountInput.Focus();
    //        isCorrect = false;
    //    }
    //    else if ((_sourceCurrencyCmb.SelectedValue == null) || (_sourceCurrencyCmb.SelectedIndex == 0))
    //    {
    //        DisplayMessageBox("Please Enter Currency FROM", messageBoxTitle);
    //        _sourceCurrencyCmb.Focus();
    //        isCorrect = false;
    //    }
    //    else if ((_targetCurrencyCmb.SelectedValue == null) || (_targetCurrencyCmb.SelectedIndex == 0))
    //    {
    //        DisplayMessageBox("Please Enter Currency TO", messageBoxTitle);
    //        _targetCurrencyCmb.Focus();
    //        isCorrect = false;
    //    }

    //    return isCorrect;
    //}

    //private void DisplayMessageBox(string message, string title)
    //{
    //    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
    //}



    //private void ValidateCurrencyAmountInput(object sender, TextCompositionEventArgs e)
    //{
    //    Regex rg = new Regex("^(([1-9][0-9]*)|(0?))(,[0-9]*)?$");
    //    e.Handled = !rg.IsMatch(_currencyAmountInput.Text + e.Text);
    //}

    //private void dgvCurrency_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    //{

    //}


    //public void OnPropertyChanged(string propertyName)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}
}
