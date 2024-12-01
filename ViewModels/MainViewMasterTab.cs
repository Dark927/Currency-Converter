using Currency_Converter.Scripts;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Currency_Converter.ViewModels
{
    public class MainViewMasterTab : MainViewBase
    {
        private bool _isEditMode = false;
        private ObservableCollection<CurrencyMaster> _data;
        private ListCollectionView _listView;

        public ICommand DeleteCommand { get; private set; }

        public MainViewMasterTab(MainWindow window) : base(window)
        {
            DeleteCommand = new RelayCommand(DeleteItem);
        }

        public void SetEditMode()
        {
            _isEditMode = true;
        }

        public void SetPushMode()
        {
            _isEditMode = false;
        }

        public override void BindData(object data)
        {
            _data = (ObservableCollection<CurrencyMaster>)data;
            _listView = new ListCollectionView(_data);
            _listView.Filter = (item => ((CurrencyMaster)item).Amount > 0);

            Window.CurrencyDataGrid.ItemsSource = _listView;
        }

        public override void Clear(object? parameter = null)
        {
            Window.MasterAmountValue.Text = string.Empty;
            Window.MasterCurrencyName.Text = string.Empty;
        }

        public override void Confirm(object? parameter = null)
        {
            if (!IsInputCorrect())
            {
                return;
            }

            try
            {
                if (!_isEditMode)
                {
                    using (var context = new CurrencyMasterDataContext())
                    {
                        CurrencyMaster currencyToAdd = new CurrencyMaster();
                        FillInfoFromInput(currencyToAdd);

                        _data.Add(currencyToAdd);

                        context.CurrencyMasters.Add(currencyToAdd);
                        context.SaveChanges();
                    }
                }
                else
                {
                    int itemID = (int)parameter;
                    CurrencyMaster? targetItem = _data.FirstOrDefault(item => item.Id == itemID, null);

                    FillInfoFromInput(targetItem);
                }
            }
            catch (Exception ex)
            {
                InfoBoxes.ExceptionInfo(ex.Message);
            }
        }

        private void DeleteItem(object parameter)
        {
            int itemID = Convert.ToInt32(parameter);
            CurrencyMaster? targetItem = _data.FirstOrDefault(item => item.Id == itemID, null);

            if (targetItem != null)
            {
                _data.Remove(targetItem);

                using (var context = new CurrencyMasterDataContext())
                {
                    context.CurrencyMasters.Remove(targetItem);
                    context.SaveChanges();
                }
            }
        }

        private void FillInfoFromInput(CurrencyMaster itemToFill)
        {
            itemToFill.CurrencyName = Window.MasterCurrencyName.Text;
            itemToFill.Amount = Convert.ToDouble(Window.MasterAmountValue.Text);
        }

        private bool IsInputCorrect()
        {
            return (Window.MasterAmountValue.Text != string.Empty) && (Window.MasterCurrencyName.Text != string.Empty);
        }
    }
}
