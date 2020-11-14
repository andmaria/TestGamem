using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index : MonoBehaviour
{
    public static void Shuffle<T>(T[] ts)
    {
        var length = ts.Length;
        var last = length - 1;

        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, length);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
  
}
