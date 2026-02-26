using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace QuanLyBanHang.Data
{
    public static class StringExtensions
    {
        public static string GenerateSlug(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            string normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            string noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC);

            string slug = Regex.Replace(noDiacritics, @"[^a-zA-Z0-9]+", "-");
            slug = slug.Trim('-').ToLower();

            return slug;
        }
    }
}