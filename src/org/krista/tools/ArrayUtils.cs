using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace org.krista.tools;

public static class ArrayUtils
{
    public static bool IsEmpty(string?[]? array)
    {
        return array == null || array.Length == 0;
    }
}