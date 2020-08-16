using System.Collections.Generic;
using System.Linq;

 public class SerializableDictionaryHelper
 {
    public static Dictionary<TKey, TValue> PostDeserialize<TKey, TValue>(List<TKey> keys, List<TValue> values)
    {
        var result = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            result[keys[i]] = values[i];
        }
        return result;        
    }

    public static void PreSerialize<TKey, TValue>(Dictionary<TKey, TValue> dictionary, out List<TKey> keys, out List<TValue> values)
    {
        keys = dictionary.Keys.ToList();
        values = dictionary.Values.ToList();
    }

 }