using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace Currency_Converter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string? _connection;
        public string? Connection { get => _connection; private set => _connection = value; }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

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
