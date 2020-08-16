using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class RandomExtension
{
    public static IEnumerable<int> Range(int min, int max, int count)
    {
        var result = new List<int>();

        while (result.Distinct().Count() < count)
        {
            result.Add(Random.Range(min, max));
        }

        return result.Distinct();
    }

    public static bool boolean { get { return Random.value > 0.5f; } }

    public static Vector2 insideRect(Rect rect)
    {
        float x = Random.value * rect.width;
        float y = Random.value * rect.height;
        return new Vector2(x + rect.x, y + rect.y);
    }

    /// <summary>
    /// position inside the unit circle.
    /// </summary>
    /// <returns>a position inside the unit circle, outside of the deadzone.</returns>
    /// <param name="deadZone">Dead zone. Range [0, 1[ </param>
    public static Vector2 insideUnitCircleWithDeadzone(float deadZone = 0.0f)
    {
        deadZone = Mathf.Clamp(deadZone, 0, 0.9999999f);
        var point = Random.insideUnitCircle;

        while (point.magnitude < deadZone)
        {
            point = Random.insideUnitCircle;
        }
        return point;
    }

    public static float sign { get { return (Random.Range(0, 2) - 0.5f) * 2f; } }

    public static Color Color()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public static Color color { get { return Color(); } }

    //function for getting a random index for an array, with specified liklihoods for each index
    //use like:
    //string[] items = {"rock", "coin", "gem"};
    //float[] itemFrequency = {10f, 4f, 1f};
    //string randomItem = items[ GetWeightedRandomIndex(itemFrequency) ];
    public static int WeightedRandomIndex(float[] arrayWeights)
    {
        float tWeight = 0f;
        for (int i = 0; i < arrayWeights.Length; i++) tWeight += arrayWeights[i];
        float r = Random.Range(0f, tWeight);

        tWeight = 0f;
        for (int i = 0; i < arrayWeights.Length; i++)
        {
            if (r > tWeight && r <= tWeight + arrayWeights[i]) return i;
            tWeight += arrayWeights[i];
        }

        return arrayWeights.Length - 1;
    }

    public static T WeightedRandom<T>(IEnumerable<T> list, float[] weights)
    {
        if (list.Count() == 0)
        {
            return default(T);
        }

        if (list.Count() != weights.Length)
        {
            return list.First();
        }

        var index = WeightedRandomIndex(weights);

        if (index < list.Count())
        {
            return list.ElementAt(index);
        }

        return list.First();
    }
}


