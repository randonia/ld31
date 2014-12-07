using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MathUtils
{
    public static Vector3 HalfwayPointBetween(Vector3 from, Vector3 to)
    {
        Vector3 direction = to - from;
        return from + direction * 0.5f;
    }

    public static float Map(float value, float startFrom, float endFrom, float startTo, float endTo)
    {
        return ((value - startFrom) / (endFrom - startFrom)) * (endTo - startTo) + startTo;
    }
}
