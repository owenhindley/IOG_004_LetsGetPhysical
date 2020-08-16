using UnityEngine;
using System;

public static class ArrayExtension {
    public static T GetRandom<T>(this System.Object[] array)
    {
        if(array == null || array.Length == 0)
        {
            Debug.LogWarning("Tried to get random element of an empty array");
            return default(T);
        } 
        return (T)array[UnityEngine.Random.Range(0,array.Length)];
    }

    //https://stackoverflow.com/questions/273313/randomize-a-listt
    public static T[] Shuffle<T>(this T[] array)
    {
        T[] result = null;
        array.CopyTo(result,0);
        int n = result.Length;  
        while (n > 1) {  
            n--;  
            int k = UnityEngine.Random.Range(0,n + 1);  
            T value = result[k];  
            result[k] = result[n];  
            result[n] = value;  
        }  
        return result;
    }    

    public static int IndexOf<T>(this System.Object[] array, T target)
    {
        return Array.IndexOf(array, target);
    }


    public static int GetNextIndex(int index, Type type, bool loop = true)
    {
        index++;
        if(type.IsEnum)
        {
            var enumValueCount = Enum.GetValues(type).Length;
            if(index >= enumValueCount)
            {
                if(loop)
                {
                    index = 0;
                }
                else
                {
                    index = enumValueCount - 1;
                }
            }
        }
        else
        {
            Debug.LogWarning("GetNextIndex was used with a type that's not an Enum. You should probably look into this");
        }

        return index;
    }

    public static int GetNextIndex(int index, UnityEngine.Object[] array, bool loop = true)
    {
        index++;
        if(index >= array.Length)
        {
            if(loop)
            {
                index = 0;
            }
            else
            {
                index = array.Length - 1;
            }
        }

        return index;
    }

    public static int GetNextIndex(int index, int max, bool loop = true)
    {
        index++;
        if(index >= max)
        {
            if(loop)
            {
                index = 0;
            }
            else
            {
                index = max -1;
            }
        }

        return index;
    }


    public static int GetPrevIndex(int index, int max, bool loop = true)
    {
        index--;
        if(index < 0)
        {
            if(loop)
            {
                index = max -1;
            }
            else
            {
                index = 0;
            }
        }

        return index;
    }


    public static int GetNextEnumValue(System.Object enumValue, bool loop = true)
    {
        var type = enumValue.GetType();
        var index = Convert.ToInt32(enumValue);        
        return GetNextIndex(index, type, loop);
    }  


}