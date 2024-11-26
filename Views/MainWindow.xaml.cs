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


    //SolidColorBrush _resetColor = new SolidColorBrush(Colors.Black);
    private string? _defaultResultValue;
    private readonly Brush _defaultResultBrush;

    #endregion


    #region Properties 

    public TextBox CurrencyAmountInput { get => _currencyAmountInput; }
    public ComboBox SourceCurrencyCmb { get => _sourceCurrencyCmb; }
    public ComboBox TargetCurrencyCmb { get => _targetCurrencyCmb; }

    #endregion


    public MainWindow()
    {
        InitializeComponent();
        _defaultResultValue = _resultLbl.Content.ToString();
        _defaultResultBrush = _resultLbl.Foreground;
        _currencyAmountInput.Focus();
    }

    public void ResetResultLabel()
    {
        UpdateResultLabel(_defaultResultValue, _defaultResultBrush);
    }

    public void UpdateResultLabel(string? value, Brush brush)
    {
        _resultLbl.Foreground = brush;
        _resultLbl.Content = value;
    }

    private void ValidateCurrencyAmountInput(object sender, TextCompositionEventArgs e)
    {

    }

    private void dgvCurrency_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {

    }
}