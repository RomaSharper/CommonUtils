using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace org.krista.tools;

public static class StringUtils
{
    public const string Space = " ";

    public const string Empty = "";

    public const string LF = "\n";

    public const string CR = "\r";

    public const int IndexNotFound = -1;

    private const int PadLimit = 8192;

    public static bool IsEmpty(string? s)
    {
        return s == null || s.Length == 0;
    }

    public static bool IsNotEmpty(string? s)
    {
        return !IsEmpty(s);
    }

    public static bool IsAnyEmpty(params string?[]? ss)
    {
        if (ArrayUtils.IsEmpty(ss))
        {
            return true;
        }
        foreach (string? s in ss)
        {
            if (IsEmpty(s))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsNoneEmpty(params string?[]? ss)
    {
        return !IsAnyEmpty(ss);
    }

    public static bool IsBlank(string? s)
    {
        int strLen = s == null ? 0 : s.Length;
        if (s == null || strLen == 0)
        {
            return true;
        }
        for (int i = 0; i < strLen; i++)
        {
            if (!char.IsWhiteSpace(s[i]))
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsNotBlank(string? s)
    {
        return !IsBlank(s);
    }

    public static bool IsAnyBlank(params string?[]? ss)
    {
        if (ArrayUtils.IsEmpty(ss))
        {
            return true;
        }
        foreach (string? s in ss)
        {
            if (IsBlank(s))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsNoneBlank(params string?[]? ss)
    {
        return !IsAnyBlank(ss);
    }

    public static string? Trim(string? str)
    {
        return str == null ? null : str.Trim();
    }

    public static string? TrimToNull(string? str)
    {
        string? ts = Trim(str);
        return IsEmpty(ts) ? null : ts;
    }

    public static string TrimToEmpty(string? str)
    {
        return str == null ? Empty : str.Trim();
    }

    public static string? Truncate(string? str, int maxWidth)
    {
        return Truncate(str, 0, maxWidth);
    }

    public static string? Truncate(string? str, int offset, int maxWidth)
    {
        if (offset < 0)
        {
            throw new ArgumentException("offset cannot be negative");
        }
        if (maxWidth < 0)
        {
            throw new ArgumentException("maxWidth cannot be negative");
        }
        if (str == null)
        {
            return null;
        }
        if (offset > str.Length)
        {
            return Empty;
        }
        if (str.Length > maxWidth)
        {
            int ix = offset + maxWidth > str.Length ? str.Length : offset + maxWidth;
            return str.Substring(offset, ix);
        }
        return str.Substring(offset);
    }

    public static string? Strip(string? str)
    {
        return Strip(str, null);
    }

    public static string? StripToNull(string? str)
    {
        if (str == null)
        {
            return null;
        }
        str = Strip(str, null);
        return IsEmpty(str) ? null : str;
    }

    public static string? StripToEmpty(string? str)
    {
        return str == null ? Empty : Strip(str, null);
    }

    public static string? Strip(string? str, string? stripChars)
    {
        if (IsEmpty(str))
        {
            return str;
        }
        str = StripStart(str, stripChars);
        return StripEnd(str, stripChars);
    }

    public static string? StripStart(string? str, string? stripChars)
    {
        int strLen;
        if (str == null || (strLen = str.Length) == 0)
        {
            return str;
        }
        int start = 0;
        if (stripChars == null)
        {
            while (start != strLen && char.IsWhiteSpace(str[start]))
            {
                start++;
            }
        }
        else if (IsEmpty(stripChars))
        {
            return str;
        }
        else
        {
            while (start != strLen && stripChars.Contains(str[start]))
            {
                start++;
            }
        }
        return str.Substring(start);
    }

    public static string? StripEnd(string? str, string? stripChars)
    {
        int end;
        if (str == null || (end = str.Length) == 0)
        {
            return str;
        }

        if (stripChars == null)
        {
            while (end != 0 && char.IsWhiteSpace(str[end - 1]))
            {
                end--;
            }
        }
        else if (IsEmpty(stripChars))
        {
            return str;
        }
        else
        {
            while (end != 0 && stripChars.Contains(str[end - 1]))
            {
                end--;
            }
        }
        return str.Substring(0, end);
    }

    public static string?[]? StripAll(params string?[] strs)
    {
        return StripAll(strs, null);
    }

    public static string?[]? StripAll(string?[]? strs, string? stripChars)
    {
        int strsLen;
        if (strs == null || (strsLen = strs.Length) == 0)
        {
            return strs;
        }
        string?[] newArr = new string?[strsLen];
        for (int i = 0; i < strsLen; i++)
        {
            newArr[i] = Strip(strs[i], stripChars);
        }
        return newArr;
    }

    public static string? StripAccents(string? input)
    {
        if (input == null)
        {
            return null;
        }
        string normalizedString = input.Normalize(NormalizationForm.FormD);
        StringBuilder decomposed = new StringBuilder(normalizedString);
        ConvertRemainingAccentCharacters(decomposed);
        Regex pattern = new Regex(@"\p{InCombiningDiacriticalMarks}+");
        return pattern.Replace(decomposed.ToString(), Empty);
    }

    private static void ConvertRemainingAccentCharacters(StringBuilder decomposed)
    {
        for (int i = 0; i < decomposed.Length; i++)
        {
            if (decomposed[i] == '\u0141')
            {
                decomposed.Remove(i, 1);
                decomposed.Insert(i, 'L');
            }
            else if (decomposed[i] == '\u0142')
            {
                decomposed.Remove(i, 1);
                decomposed.Remove(i, 'l');
            }
        }
    }

    public static bool Equals(string? str1, string? str2)
    {
        if (str1 == null || str2 == null)
        {
            return false;
        }
        if (str1.Length != str2.Length)
        {
            return false;
        }
        return str1 == str2;
    }

    public static bool EqualsIgnoreCase(string? str1, string? str2)
    {
        if (str1 == null || str2 == null)
        {
            return str1 == str2;
        }
        else if (str1.Length != str2.Length)
        {
            return false;
        }
        else if (str1 == str2)
        {
            return true;
        }
        else if (str1.Equals(str2, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        return false;
    }

    public static int Compare(string? str1, string? str2)
    {
        return Compare(str1, str2, true);
    }

    public static int Compare(string? str1, string? str2, bool nullIsLess)
    {
        if (str1 == str2)
        {
            return 0;
        }
        if (str1 == null)
        {
            return nullIsLess ? -1 : 1;
        }
        if (str2 == null)
        {
            return nullIsLess ? 1 : -1;
        }
        return str1.CompareTo(str2);
    }

    public static int CompareIgnoreCase(string? str1, string? str2)
    {
        return CompareIgnoreCase(str1, str2, true);
    }

    public static int CompareIgnoreCase(string? str1, string? str2, bool nullIsLess)
    {
        if (str1 == str2)
        {
            return 0;
        }
        if (str1 == null)
        {
            return nullIsLess ? -1 : 1;
        }
        if (str2 == null)
        {
            return nullIsLess ? 1 : -1;
        }
        return str1.ToLower().CompareTo(str2.ToLower());
    }

    public static bool EqualsAny(string? str, params string?[]? searchStrings)
    {
        if (ArrayUtils.IsNotEmpty(searchStrings))
        {
            foreach (string? next in searchStrings)
            {
                if (Equals(str, next))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool EqualsAnyIgnoreCase(string? str, params string?[]? searchStrings)
    {
        if (ArrayUtils.IsNotEmpty(searchStrings))
        {
            foreach (string? next in searchStrings)
            {
                if (EqualsIgnoreCase(str, next))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static int IndexOf(string str, char searchChar)
    {
        if (IsEmpty(str))
        {
            return IndexNotFound;
        }
        return str.IndexOf(searchChar);
    }
}
