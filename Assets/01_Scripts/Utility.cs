using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class Utility
{
    public static T Choose<T>(params T[] input)
    {
        return input[Random.Range(0, input.Length)];
    }

    public static T GetRandomItem<T>(this T[] input)
    {
        return input[Random.Range(0, input.Length)];
    }

    public static T GetRandomItem<T>(this List<T> input)
    {
        return input[Random.Range(0, input.Count)];
    }

    public static List<T> Shuffle<T>(this List<T> input)
    {
        for (int i = 0; i < input.Count; i++)
        {
            int rnd = Random.Range(0, input.Count);
            var val = input[rnd];
            input[rnd] = input[i];
            input[i] = val;
        }
        return input;
    }

    public static T GetLast<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }

    public static T GetLast<T>(this T[] list)
    {
        return list[list.Length - 1];
    }

    public static int Sign(float input)
    {
        if (input == 0) { return 0; }
        if (input < 0) { return -1; }
        return 1;
    }

    public static float Remap(float input, float oldMin, float oldMax, float newMin, float newMax)
    {
        return (input - oldMin) / (oldMax - oldMin)  * (newMax - newMin) + newMin;
    }

    public static Vector3 WithY(this Vector3 vec, float val)
    {
        vec.y = val;
        return vec;
    }

    public static T GetClosestObjectInRange<T>(Vector3 position, List<T> input, float range) where T : MonoBehaviour
    {
        T closest = default(T);
        float closestDistance = float.MaxValue;
        foreach(var item in input)
        {
            float sqr = Vector3.SqrMagnitude(item.transform.position - position);
            if (sqr <= range * range)
            {
                if(sqr < closestDistance)
                {
                    closest = item;
                    closestDistance = sqr;
                }
            }
        }
        return closest;
    }
}
