using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Currency_Converter.ViewModels
{
    public class MainViewConverterTab : MainViewBase
    {
        public MainViewConverterTab(MainWindow window) : base(window)
        {
        }

        public override void Clear(object? parameter = null)
        {
            Window.ResetResultLabel();

            Window.CurrencyAmountInput.Clear();
            Window.SourceCurrencyCmb.SelectedIndex = 0;
            Window.TargetCurrencyCmb.SelectedIndex = 0;
        }
    }
}
