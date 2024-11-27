using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Currency_Converter.Scripts
{
    public class TextValidator
    {
        public static bool IsCurrencyValueCorrect(string text)
        {
            Regex rg = new Regex("^(([1-9][0-9]*)|(0?))(,[0-9]*)?$");
            return rg.IsMatch(text);
        }
    }
}
