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
                decomposed.Insert(i, 'l');
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
        return string.Compare(str1, str2, StringComparison.OrdinalIgnoreCase);
    }

    public static bool EqualsAny(string? str, params string?[] searchStrs)
    {
        if (ArrayUtils.IsNotEmpty(searchStrs))
        {
            foreach (string? next in searchStrs)
            {
                if (Equals(str, next))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool EqualsAnyIgnoreCase(string? str, params string?[] searchStrs)
    {
        if (ArrayUtils.IsNotEmpty(searchStrs))
        {
            foreach (string? next in searchStrs)
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

    public static int IndexOf(string? str, string? searchStr)
    {
        if (str == null || searchStr == null)
        {
            return IndexNotFound;
        }
        return str.IndexOf(searchStr);
    }

    public static int IndexOf(string? str, string? searchStr, int startPos)
    {
        if (str == null || searchStr == null)
        {
            return IndexNotFound;
        }
        return str.IndexOf(searchStr, startPos);
    }

    public static int OrdinalIndexOf(string? str, string? searchStr, int ordinal)
    {
        return OrdinalIndexOf(str, searchStr, ordinal, false);
    }

    public static int OrdinalIndexOf(string? str, string? searchStr, int ordinal, bool lastIndex)
    {
        if (str == null || searchStr == null || ordinal <= 0)
        {
            return IndexNotFound;
        }
        if (searchStr.Length == 0)
        {
            return lastIndex ? str.Length : 0;
        }
        int found = 0;
        int index = lastIndex ? str.Length : IndexNotFound;
        do
        {
            if (lastIndex)
            {
                index = str.LastIndexOf(searchStr, index - 1);
            }
            else
            {
                index = str.IndexOf(searchStr, index + 1);
            }
            if (index < 0)
            {
                return index;
            }
            found++;
        } while (found < ordinal);
        return index;
    }

    public static int IndexOfIgnoreCase(string? str, string? searchStr)
    {
        return IndexOfIgnoreCase(str, searchStr, 0);
    }

    public static int IndexOfIgnoreCase(string? str, string? searchStr, int startPos)
    {
        if (str == null || searchStr == null)
        {
            return IndexNotFound;
        }
        if (startPos < 0)
        {
            startPos = 0;
        }
        int endLimit = str.Length - searchStr.Length + 1;
        if (startPos > endLimit)
        {
            return IndexNotFound;
        }
        if (searchStr.Length == 0)
        {
            return startPos;
        }
        for (int i = startPos; i < endLimit; i++)
        {
            if (str[i].ToString().Equals(searchStr[i].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }
        return IndexNotFound;
    }

    public static int LastIndexOf(string? str, char searchChar)
    {
        if (IsEmpty(str))
        {
            return IndexNotFound;
        }
        return str.LastIndexOf(searchChar);
    }

    public static int LastIndexOf(string? str, char searchChar, int startPos)
    {
        if (IsEmpty(str))
        {
            return IndexNotFound;
        }
        return str.LastIndexOf(str, searchChar, startPos);
    }

    public static int LastIndexOf(string? str, string? searchStr)
    {
        if (str == null || searchStr == null)
        {
            return IndexNotFound;
        }
        return str.LastIndexOf(searchStr, str.Length);
    }

    public static int LastOrdinalIndexOf(string? str, string? searchStr, int ordinal)
    {
        return OrdinalIndexOf(str, searchStr, ordinal, true);
    }

    public static int LastIndexOf(string? str, string? searchStr, int startPos)
    {
        if (str == null || searchStr == null)
        {
            return IndexNotFound;
        }
        return str.LastIndexOf(searchStr, str.Length, startPos);
    }

    public static int LastIndexOfIgnoreCase(string? str, string? searchStr)
    {
        if (str == null || searchStr == null)
        {
            return IndexNotFound;
        }
        return LastIndexOfIgnoreCase(str, searchStr, str.Length);
    }

    public static int LastIndexOfIgnoreCase(string? str, string? searchStr, int startPos)
    {
        if (str == null || searchStr == null)
        {
            return IndexNotFound;
        }
        if (startPos > str.Length - searchStr.Length)
        {
            startPos = str.Length - searchStr.Length;
        }
        if (startPos < 0)
        {
            return IndexNotFound;
        }
        if (searchStr.Length == 0)
        {
            return startPos;
        }

        for (int i = startPos; i >= 0; i--)
        {
            if (str[i].ToString().Equals(searchStr[i].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }
        return IndexNotFound;
    }

    public static bool Contains(string? str, char searchChar)
    {
        if (IsEmpty(str))
        {
            return false;
        }
        return str.Contains(searchChar);
    }

    public static bool Contains(string? str, string? searchStr)
    {
        if (str == null || searchStr == null)
        {
            return false;
        }
        return str.Contains(searchStr);
    }

    public static bool ContainsIgnoreCase(string? str, string? searchStr)
    {
        if (str == null || searchStr == null)
        {
            return false;
        }
        int len = searchStr.Length;
        int max = str.Length - len;
        for (int i = 0; i <= max; i++)
        {
            if (str[i].ToString().Equals(searchStr[i].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    public static bool ContainsWhitespace(string? str)
    {
        if (IsEmpty(str))
        {
            return false;
        }
        int strLen = str.Length;
        for (int i = 0; i <= strLen; i++)
        {
            if (char.IsWhiteSpace(str[i]))
            {
                return true;
            }
        }
        return false;
    }

    public static int IndexOfAny(string? str, char[] searchChars)
    {
        if (IsEmpty(str) || ArrayUtils.IsEmpty(searchChars))
        {
            return IndexNotFound;
        }
        int strLen = str.Length;
        int strLast = strLen - 1;
        int searchLen = searchChars.Length;
        int searchLast = searchLen - 1;
        for (int i = 0; i < strLen; i++)
        {
            char ch = str[i];
            for (int j = 0; j < searchLen; j++)
            {
                if (searchChars[j] == ch)
                {
                    if (i < strLast && j < searchLast && char.IsHighSurrogate(ch))
                    {
                        if (searchChars[j + 1] == str[i + 1])
                        {
                            return i;
                        }
                    }
                    else
                    {
                        return i;
                    }
                }
            }
        }
        return IndexNotFound;
    }

    public static int IndexOfAny(string? str, string? searchChars)
    {
        if (IsEmpty(str) || IsEmpty(searchChars))
        {
            return IndexNotFound;
        }
        return IndexOfAny(
            str,
            searchChars.ToCharArray()
        );
    }

    public static bool ContainsAny(string? str, params char[] searchChars)
    {
        if (IsEmpty(str) || ArrayUtils.IsEmpty(searchChars))
        {
            return false;
        }
        int strLen = str.Length;
        int searchLen = searchChars.Length;
        int strLast = strLen - 1;
        int searchLast = searchLen - 1;
        for (int i = 0; i < strLen; i++)
        {
            char ch = str[i];
            for (int j = 0; j < searchLen; i++)
            {
                if (searchChars[j] == ch)
                {
                    if (char.IsHighSurrogate(ch))
                    {
                        if (j == searchLast)
                        {
                            return true;
                        }
                        if (i < strLast && searchChars[j + 1] == str[i + 1])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool ContainsAny(string? str, string? searchChars)
    {
        if (searchChars == null)
        {
            return false;
        }
        return ContainsAny(str, searchChars.ToCharArray());
    }

    public static bool ContainsAny(string? str, params string?[] searchStrs)
    {
        if (IsEmpty(str) || ArrayUtils.IsEmpty(searchStrs))
        {
            return false;
        }
        foreach (string? searchStr in searchStrs)
        {
            if (Contains(str, searchStr))
            {
                return true;
            }
        }
        return false;
    }

    public static int IndexOfAnyBut(string? str, params char[] searchChars)
    {
        if (IsEmpty(str) || ArrayUtils.IsEmpty(searchChars))
        {
            return IndexNotFound;
        }
        int strLen = str.Length;
        int strLast = strLen - 1;
        int searchLen = searchChars.Length;
        int searchLast = searchLen - 1;
        outer:
        for (int i = 0; i < strLen; i++)
        {
            char ch = str[i];
            for (int j = 0; j < searchLen; j++)
            {
                if (searchChars[j] == ch)
                {
                    if (i < strLast && j < searchLast && char.IsHighSurrogate(ch))
                    {
                        if (searchChars[j + 1] == str[i + 1])
                        {
                            goto outer;
                        }
                    }
                    else
                    {
                        goto outer;
                    }
                }
            }
            return i;
        }
        return IndexNotFound;
    }

    public static int IndexOfAnyBut(string? str, string? searchChars)
    {
        if (IsEmpty(str) || IsEmpty(searchChars))
        {
            return IndexNotFound;
        }
        int strLen = str.Length;
        for (int i = 0; i < strLen; i++)
        {
            char ch = str[i];
            bool chFound = searchChars.Contains(ch);
            if (i + 1 < strLen && char.IsHighSurrogate(ch))
            {
                char ch2 = str[i + 1];
                if (chFound && !searchChars.Contains(ch2))
                {
                    return i;
                }
            }
            else
            {
                if (!chFound)
                {
                    return i;
                }
            }
        }
        return IndexNotFound;
    }

    public static bool ContainsOnly(string? str, params char[] valid)
    {
        if (str == null || valid == null)
        {
            return false;
        }
        if (str.Length == 0)
        {
            return true;
        }
        if (valid.Length == 0)
        {
            return false;
        }
        return IndexOfAnyBut(str, valid) == IndexNotFound;
    }

    public static bool ContainsOnly(string? str, string? validChars)
    {
        if (str == null || validChars == null)
        {
            return false;
        }
        return ContainsOnly(str, validChars.ToCharArray());
    }

    public static bool ContainsNone(string? str, params char[] searchChars)
    {
        if (str == null || searchChars == null)
        {
            return true;
        }
        int strLen = str.Length;
        int strLast = strLen - 1;
        int searchLen = searchChars.Length;
        int searchLast = searchLen - 1;
        for (int i = 0; i < strLen; i++)
        {
            char ch = str[i];
            for (int j = 0; j < searchLen; j++)
            {
                if (searchChars[j] == ch)
                {
                    if (char.IsHighSurrogate(ch))
                    {
                        if (j == searchLast)
                        {
                            return false;
                        }
                        if (i < strLast && searchChars[j + 1] == str[i + 1])
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public static bool ContainsNone(string? str, string? invalidChars)
    {
        if (str == null || invalidChars == null)
        {
            return true;
        }
        return ContainsNone(str, invalidChars.ToCharArray());
    }

    public static int IndexOfAny(string? str, params string?[] searchStrs)
    {
        if (str == null || searchStrs == null)
        {
            return IndexNotFound;
        }
        int sz = searchStrs.Length;

        int ret = int.MaxValue;
        int tmp = 0;
        for (int i = 0; i < sz; i++)
        {
            string? search = searchStrs[i];
            if (search == null)
            {
                continue;
            }
            tmp = str.IndexOf(search, 0);
            if (tmp == IndexNotFound)
            {
                continue;
            }

            if (tmp < ret)
            {
                ret = tmp;
            }
        }

        return ret == int.MaxValue ? IndexNotFound : ret;
    }

    public static int LastIndexOfAny(string? str, params string?[] searchStrs)
    {
        if (str == null || searchStrs == null)
        {
            return IndexNotFound;
        }
        int sz = searchStrs.Length;
        int ret = IndexNotFound;
        int tmp = 0;
        for (int i = 0; i < sz; i++)
        {
            string? search = searchStrs[i];
            if (search == null)
            {
                continue;
            }
            tmp = str.IndexOf(search, str.Length);
            if (tmp > ret)
            {
                ret = tmp;
            }
        }
        return ret;
    }

    public static string? Substring(string? str, int start)
    {
        if (str == null)
        {
            return null;
        }

        if (start < 0)
        {
            start = str.Length + start;
        }

        if (start < 0)
        {
            start = 0;
        }
        if (start > str.Length)
        {
            return Empty;
        }

        return str.Substring(start);
    }

    public static string? Substring(string? str, int start, int end)
    {
        if (str == null)
        {
            return null;
        }

        if (end < 0)
        {
            end = str.Length + end;
        }
        if (start < 0)
        {
            start = str.Length + start;
        }

        if (end > str.Length)
        {
            end = str.Length;
        }

        if (start > end)
        {
            return Empty;
        }

        if (start < 0)
        {
            start = 0;
        }

        if (end < 0)
        {
            end = 0;
        }

        return str.Substring(start, end);
    }

    public static string? Left(string? str, int len)
    {
        if (str == null)
        {
            return null;
        }
        if (len <= 0)
        {
            return Empty;
        }
        if (str.Length <= len)
        {
            return str;
        }
        return str.Substring(0, len);
    }

    public static string? Right(string? str, int len)
    {
        if (str == null)
        {
            return null;
        }
        if (len <= 0)
        {
            return Empty;
        }
        if (str.Length <= len)
        {
            return str;
        }
        return str.Substring(str.Length - len);
    }

    public static string? Mid(string? str, int pos, int len)
    {
        if (str == null)
        {
            return null;
        }
        if (len <= 0 || pos > str.Length)
        {
            return Empty;
        }
        if (pos < 0)
        {
            pos = 0;
        }
        if (str.Length <= pos + len)
        {
            return str.Substring(pos);
        }
        return str.Substring(pos, pos + len);
    }

    public static string? SubstringBefore(string? str, string? separator)
    {
        if (IsEmpty(str) || separator == null)
        {
            return str;
        }
        if (IsEmpty(separator))
        {
            return Empty;
        }
        int pos = IndexOf(str, separator);
        if (pos == IndexNotFound)
        {
            return str;
        }
        return Substring(str, 0, pos);
    }

    public static string? SubstringAfter(string? str, string? separator)
    {
        if (IsEmpty(str))
        {
            return str;
        }
        if (separator == null)
        {
            return Empty;
        }
        int pos = IndexOf(str, separator);
        if (pos == IndexNotFound)
        {
            return Empty;
        }
        return Substring(str, pos + separator.Length);
    }

    public static string? SubstringBeforeLast(string? str, string? separator)
    {
        if (IsEmpty(str) || IsEmpty(separator))
        {
            return str;
        }
        int pos = LastIndexOf(str, separator);
        if (pos == IndexNotFound)
        {
            return str;
        }
        return Substring(str, 0, pos);
    }

    public static string? SubstringAfterLast(string? str, string? separator)
    {
        if (IsEmpty(str))
        {
            return str;
        }
        if (IsEmpty(separator))
        {
            return Empty;
        }
        int pos = LastIndexOf(str, separator);
        if (pos == IndexNotFound || pos == str.Length - separator.Length)
        {
            return Empty;
        }
        return Substring(str, pos + separator.Length);
    }

    public static string? SubstringBetween(string? str, string? tag)
    {
        return SubstringBetween(str, tag, tag);
    }

    public static string? SubstringBetween(string? str, string? open, string? close)
    {
        if (str == null || open == null || close == null)
        {
            return null;
        }
        int start = IndexOf(str, open);
        if (start != IndexNotFound)
        {
            int end = IndexOf(str, close, start + open.Length);
            if (end != IndexNotFound)
            {
                return Substring(str, start + open.Length, end);
            }
        }
        return null;
    }

    public static string?[]? SubstringsBetween(string? str, string? open, string? close)
    {
        if (str == null || IsEmpty(open) || IsEmpty(close))
        {
            return null;
        }
        int strLen = str.Length;
        if (strLen == 0)
        {
            return ArrayUtils.EmptyStringArray;
        }
        int closeLen = close.Length;
        int openLen = open.Length;
        List<string> list = [];
        int pos = 0;
        while (pos < strLen - closeLen)
        {
            int start = IndexOf(str, open, pos);
            if (start < 0)
            {
                break;
            }
            start += openLen;
            int end = IndexOf(str, close, start);
            if (end < 0)
            {
                break;
            }
            list.Add(Substring(str, start, end));
            pos = end + closeLen;
        }
        if (!list.Any())
        {
            return null;
        }
        return list.ToArray();
    }
}
