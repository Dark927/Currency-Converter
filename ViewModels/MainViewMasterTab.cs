using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Currency_Converter.ViewModels
{
    public class MainViewMasterTab : MainViewBase
    {
        public MainViewMasterTab(MainWindow window) : base(window)
        {
        }

        public override void BindData(object data)
        {
            IEnumerable<CurrencyMaster> dataToBind = (IEnumerable<CurrencyMaster>)data;
            Window.CurrencyDataGrid.ItemsSource = dataToBind.Where(item => item.Amount > 0);
        }

        public override void Clear(object? parameter = null)
        {
            Window.MasterAmountValue.Text = string.Empty;
            Window.MasterCurrencyName.Text = string.Empty;
        }

        public override void Confirm(object? parameter = null)
        {
            MessageBox.Show("Hello ;)");
        }
    }
}
