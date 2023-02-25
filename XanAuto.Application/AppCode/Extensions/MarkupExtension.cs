using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XanAuto.Application.AppCode.Extensions
{
    public static partial class Extension
    {
        static public string ToPlainText(this string text)
        {
            text = Regex.Replace(text, "<[^>]*>", "");
            return text;
        }

        static public string ToSlug(this string context)
        {
            if (string.IsNullOrWhiteSpace(context))
                return null;


            context = context.Replace("Ü", "u").Replace("ü", "u")
                .Replace("İ", "i").Replace("I", "i").Replace("ı", "i")
                .Replace("Ş", "s").Replace("ş", "s")
                .Replace("Ö", "o").Replace("ö", "o")
                .Replace("Ç", "c").Replace("ç", "c")
                .Replace("Ğ", "g").Replace("ğ", "g")
                .Replace("Ə", "e").Replace("ə", "e")
                .Replace(" ", "-").Replace("?", "").Replace("/", "")
                .Replace("\\", "").Replace(".", "").Replace("'", "").Replace("#", "").Replace("%", "")
                .Replace("&", "").Replace("*", "").Replace("!", "").Replace("@", "").Replace("+", "")
                .ToLower().Trim();

            context = Regex.Replace(context, @"\&+", "and");
            context = Regex.Replace(context, @"[^a-z0-9]+", "-");
            context = Regex.Replace(context, @"-+", "-");
            context = context.Trim('-');

            return context;
        }

        static public string ToJsonArray(this int[] array)
        {
            if (array == null || array.Length == 0)
            {
                return "[]";
            }
            return $"[{string.Join(",", array)}]";
        }
    }
}
