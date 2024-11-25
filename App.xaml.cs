using System.Configuration;
using System.Data;
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
        }
    }

}
