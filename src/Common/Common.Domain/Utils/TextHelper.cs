using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Domain.Utils;

public static class TextHelper
{
    public static bool IsText(this string value)
    {
        var isNumber = Regex.IsMatch(value, @"^\d+$");
        return !isNumber;
    }

    public static string SetUnReadableEmail(this string email)
    {
        email = email.Split('@')[0];
        var emailLength = email.Length;
        email = "..." + email.Substring(0, emailLength - 1);

        return email;
    }

    public static string ToSlug(this string url)
    {
        //return url.Trim().ToLower()
        //    .Replace("$", "")
        //    .Replace("+", "")
        //    .Replace("%", "")
        //    .Replace("?", "")
        //    .Replace("^", "")
        //    .Replace("*", "")
        //    .Replace("@", "")
        //    .Replace("!", "")
        //    .Replace("#", "")
        //    .Replace("&", "")
        //    .Replace("~", "")
        //    .Replace("(", "")
        //    .Replace("=", "")
        //    .Replace(")", "")
        //    .Replace("/", "")
        //    .Replace(@"\", "")
        //    .Replace(".", "")
        //    .Replace(" ", "-");
        var s = url.RemoveDiacritics().ToLower();
        s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s-]",
            ""); // remove invalid characters
        s = Regex.Replace(s, @"\s+", " ").Trim(); // single space
        s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim(); // cut and trim
        s = Regex.Replace(s, @"\s", "-"); // insert hyphens        
        s = Regex.Replace(s, @"‌", "-"); // half space
        return s.ToLower();
    }

    public static string RemoveDiacritics(this string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        var normalizedString = text.Normalize(NormalizationForm.FormKC);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static bool IsUniCode(this string value)
    {
        return value.Any(c => c > 255);
    }

    public static string Subscribe(this string text, int length)
    {
        if (text.Length > length)
        {
            return text.Substring(0, length - 3) + "...";
        }

        return text;
    }

    public static string GenerateCode(int length)
    {
        var random = new Random();
        var code = "";
        for (int i = 0; i < length; i++)
        {
            code += random.Next(0, 9).ToString();
        }

        return code;
    }

    public static string ConvertHtmlToText(this string text)
    {
        return Regex.Replace(text, "<.*?>", " ")
            .Replace("&zwnj;", " ")
            .Replace(";&zwnj", " ")
            .Replace("&nbsp;", " ");
    }
}