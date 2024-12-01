using System.Configuration;
using System.Globalization;
using System.Windows;

namespace Currency_Converter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string? appCulture = ConfigurationManager.AppSettings.Get("DefaultCulture");
            BindCultureInfo(appCulture);
        }

        private void BindCultureInfo(string? cultureName)
        {
            if (cultureName != null)
            {
                CultureInfo culture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }

}
