using System;

public static class UtilityClass<T>
{
    public static void dump(List<T>? arr)
    {
        if(arr is not null)
        {
            foreach (T value in arr)
            {
                if (value == null)
                {
                    continue;
                }
                Console.Write(value.ToString() + " ");
            }
        }

    }
}
