using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Currency_Converter.ViewModels;
using System.Windows.Controls;
using Currency_Converter.Scripts;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;

namespace Currency_Converter;

public class MainViewModel : INotifyPropertyChanged
{
    // Tab Control depends on values of TabType
    public enum TabType
    {
        Converter = 1,
        Master,
    }


    #region Fields


    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly MainWindow _mainWindow;

    private Dictionary<TabType, MainViewBase> _mainViewTabsDict;

    private TabType _currentTabType;

    private ObservableCollection<CurrencyMaster>? _currencyMasterList;


    #endregion


    #region Properties

    public MainWindow Window { get => _mainWindow; }
    public ICommand? ClearCommand { get; private set; }
    public ICommand? ConfirmCommand { get; private set; }

    private TabItem _selectedTab;

    public TabItem SelectedTab
    {
        get => _selectedTab;
        set
        {
            if (_selectedTab != value)
            {
                _selectedTab = value;
                OnCurrentTabChanged(value.TabIndex);
            }
        }
    }


    #endregion


    #region Methods


    // Pragma used to dissable NonNullableField in constructor warning
#pragma warning disable CS8618

    public MainViewModel()
    {
        try
        {
            _mainWindow = (MainWindow)Application.Current.MainWindow;

            _mainViewTabsDict = new Dictionary<TabType, MainViewBase>
            {
                [TabType.Converter] = new MainViewConverterTab(Window),
                [TabType.Master] = new MainViewMasterTab(Window),
            };

            _currentTabType = TabType.Converter;
            InitCommands();


            // Currency Binding 

            SelectedTab = Window.Tabs.Items.OfType<TabItem>().FirstOrDefault(new TabItem());
            _currencyMasterList = null;
            Window.Loaded += BindCurrency;
        }
        catch (Exception ex)
        {
            InfoBoxes.ExceptionInfo(ex.Message);
        }
    }

#pragma warning restore CS8618


    private void InitCommands()
    {
        ClearCommand = new RelayCommand(Clear);
        ConfirmCommand = new RelayCommand(Confirm);
    }

    public void Clear(object parameter)
    {
        TryExecuteCommand(_mainViewTabsDict[_currentTabType].Clear);
    }

    public void Confirm(object parameter)
    {
        TryExecuteCommand(_mainViewTabsDict[_currentTabType].Confirm);
    }

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void OnCurrentTabChanged(int selectedTabIndex)
    {
        try
        {
            _currentTabType = (TabType)selectedTabIndex;
        }
        catch (Exception ex)
        {
            InfoBoxes.ExceptionInfo(ex.Message);
        }
    }

    private void TryExecuteCommand(Action<object> command, object parameter = null)
    {
        try
        {
            command(parameter);
        }
        catch (Exception ex)
        {
            InfoBoxes.ExceptionInfo(ex.Message);
        }
    }

    private void BindCurrency(object? sender = null, RoutedEventArgs? args = null)
    {
        using (CurrencyMasterDataContext dataContext = new CurrencyMasterDataContext())
        {
            _currencyMasterList = new ObservableCollection<CurrencyMaster>(dataContext.CurrencyMasters);
            CurrencyMaster firstMaster = new CurrencyMaster() { Amount = 0, CurrencyName = "--SELECT--" };
            _currencyMasterList.Insert(0, firstMaster);
        }

        //dtCurrency.Rows.Add("SAR", 20);
        //dtCurrency.Rows.Add("POUND", 5);
        //dtCurrency.Rows.Add("DEM", 43);

        _mainViewTabsDict[TabType.Converter].BindData(_currencyMasterList);
        _mainViewTabsDict[TabType.Master].BindData(_currencyMasterList);
    }


    #endregion






    //private void Convert_Click(object sender, RoutedEventArgs e)
    //{
    //    if (IsAllOptionsCorrect())
    //    {
    //        decimal currencyInput = Convert.ToDecimal(_currencyAmountInput.Text);
    //        decimal currencyFromValue = Convert.ToDecimal(_sourceCurrencyCmb.SelectedValue.ToString());
    //        decimal convertedToValue = Convert.ToDecimal(_targetCurrencyCmb.SelectedValue.ToString());
    //        string convertationResult = ConvertCurrency(currencyInput, currencyFromValue, convertedToValue).ToString("N3");


    //        UpdateResultLabel(resultLabel, _defaultResultBrush);
    //    }
    //}

    //private string? GetDataTextFromCmb(ComboBox cmb)
    //{
    //    DataRowView rowView = (DataRowView)cmb.SelectedItem;
    //    return rowView["Text"].ToString();
    //}


    //private void btnSave_Click(object sender, RoutedEventArgs e)
    //{

    //}

    //private void btnCancel_Click(object sender, RoutedEventArgs e)
    //{

    //}

    //private void dgvCurrency_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    //{

    //}



}
