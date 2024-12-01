using System.Windows;

namespace Currency_Converter.Scripts
{
    static public class InfoBoxes
    {
        public static void ExceptionInfo(string message)
        {
            MessageBox.Show(message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
