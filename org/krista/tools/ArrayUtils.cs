namespace org.krista.tools;

public static class ArrayUtils
{
    public static readonly string[] EmptyStringArray = new string[0];
    
    public static bool IsEmpty(string?[] array)
    {
        return array == null || array.Length == 0;
    }

    public static bool IsEmpty(char[] array)
    {
        return array == null || array.Length == 0;
    }

    public static bool IsNotEmpty(string?[] array)
    {
        return !IsEmpty(array);
    }

    public static bool IsNotEmpty(char[] array)
    {
        return !IsEmpty(array);
    }
}