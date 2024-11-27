using Currency_Converter.Scripts;
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

namespace Currency_Converter;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Fields

    private readonly SolidColorBrush _resultDefaultColor = new SolidColorBrush(Colors.Black);
    private string? _defaultResultValue;
    private readonly Brush _defaultResultBrush;
    private MainViewModel _mainViewModel;

    #endregion


    #region Properties 

    // Global

    public TabControl Tabs { get => _tabControl; }

    // Converter Tab

    public TextBox CurrencyAmountInput { get => _currencyAmountInput; }
    public ComboBox SourceCurrencyCmb { get => _sourceCurrencyCmb; }
    public ComboBox TargetCurrencyCmb { get => _targetCurrencyCmb; }


    // Master Tab

    public TextBox MasterAmountValue { get => _masterAmountValue; }
    public TextBox MasterCurrencyName { get => _masterCurrencyName; }
    public DataGrid CurrencyDataGrid { get => _currencyDataGrid; }


    #endregion


    public MainWindow()
    {
        InitializeComponent();
        _defaultResultValue = _resultLbl.Content.ToString();
        _defaultResultBrush = _resultLbl.Foreground;
        _currencyAmountInput.Focus();

        _mainViewModel = new MainViewModel();
        DataContext = _mainViewModel;
    }

    public void ConverterResetResult()
    {
        UpdateResultLabel(_defaultResultValue, _defaultResultBrush);
    }

    public async void CenterWindow(int delayInMillisecs = 0)
    {
        await Task.Run(async () =>
        {
            await Task.Delay(delayInMillisecs);
        });
        Left = (SystemParameters.PrimaryScreenWidth - ActualWidth) / 2;
        Top = (SystemParameters.PrimaryScreenHeight - ActualHeight) / 2;
    }

    public void UpdateResultLabel(string? value, Brush? brush = null)
    {
        if (brush != null)
        {
            _resultLbl.Foreground = brush;
        }
        else
        {
            _resultLbl.Foreground = _resultDefaultColor;
        }

        _resultLbl.Content = value;
    }

    private void ValidateCurrencyAmountInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !TextValidator.IsCurrencyValueCorrect(CurrencyAmountInput.Text + e.Text);
    }

    private void dgvCurrency_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {

    }
}