using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Firma.Models.Validators
{
    public class StringValidator : Validator
    {
        public static string CheckIsStartsWithUpper(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            try
            {
                if (!Char.IsUpper(value, 0))
                {
                    return "Wartość musi się rozpoczynać wielką literą";
                }
            }
            catch (Exception) { }
            return null;
        }
        public static string CheckIsAllUpper(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            if (value.ToUpper() != value)
                return "Wartość nie może zawierać małych liter";
            return null;
        }
        public static string CheckIsEmpty(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return null;
        }
        public static string CheckIsEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            if (Regex.IsMatch(value, pattern))
                return null;
            return "Nieprawidłowy adres e-mail";
        }
        public static string CheckIsNumeric(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            const string pattern = @"^\d+$";

            if (Regex.IsMatch(value, pattern))
                return null;
            return "Nieprawidłowy numer telefonu";
        }
    }
}
