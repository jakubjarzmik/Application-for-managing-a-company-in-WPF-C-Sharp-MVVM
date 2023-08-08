using System;

namespace Firma.Models.Validators
{
    public class BusinessValidator : Validator
    {
        public static string CheckIsSet(int value)
        {
            if (value < 1)
                return "Obiekt nie może być pusty";
            return null;
        }
        public static string CheckBetween0and100(decimal? value)
        {
            if (value != null)
                if (value < 0 || value > 100)
                    return "Wartość powinna być z przedzialu 0-100";
            return null;
        }
        public static string CheckIsNotLessThanZero(decimal? value)
        {
            if (value != null)
                if (value < 0)
                    return "Wartość nie może być mniejsza od 0";
            return null;
        }
        public static string CheckDateIsNotEarlier(DateTime prev, DateTime? later)
        {
            if (prev != null && later != null)
                if (prev > later)
                    return "Data nie może być wcześniej niż poprzednia";
            return null;
        }
        public static string CheckIsNotNull(Object value)
        {
            if (value == null)
                return "Wartość nie może być pusta";
            return null;
        }
        public static string CheckIsNip(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            if (value.Length != 10)
                return "Numer NIP powinien składać się z 10 cyfr.";

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (value[i] - '0') * (i + 1);

            int checkSum = sum % 11;
            if (checkSum == 10)
                checkSum = 0;

            if (value[9] - '0' != checkSum)
                return "Numer NIP jest nieprawidłowy.";

            return null;
        }
        public static string CheckIsRegon(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            int length = value.Length;
            if (length != 9 && length != 14)
                return "Numer REGON powinien składać się z 9 lub 14 cyfr.";

            int[] weights = { 8, 9, 2, 3, 4, 5, 6, 7 };
            int sum = 0;
            for (int i = 0; i < length - 1; i++)
                sum += (value[i] - '0') * weights[i % 8];

            int checkSum = sum % 11;
            if (checkSum == 10)
                checkSum = 0;

            if (value[length - 1] - '0' != checkSum)
                return "Numer REGON jest nieprawidłowy.";

            return null;
        }
        public static string CheckIsPesel(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            int length = value.Length;
            if (length != 11)
                return "Numer PESEL powinien składać się z 11 cyfr.";

            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;
            for (int i = 0; i < length - 1; i++)
                sum += (value[i] - '0') * weights[i];

            int checkSum = 10 - (sum % 10);
            if (checkSum == 10)
                checkSum = 0;

            if (value[length - 1] - '0' != checkSum)
                return "Numer PESEL jest nieprawidłowy.";

            return null;
        }
        public static string CheckIsUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            if (Uri.TryCreate(value, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                return null;
            return "Wprowadzony adres URL jest niepoprawny.";
        }
    }
}
