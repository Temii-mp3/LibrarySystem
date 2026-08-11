using System;

public static class UtilityClass<T>
{
    public static void dump(List<T>? arr)
    {
        foreach (T value in arr)
        {
            Console.Write(value.ToString() + " ");
        }
    }
}
