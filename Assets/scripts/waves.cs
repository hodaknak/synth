using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves
{
    public static float Sin(float t) {
        return Mathf.Sin(t);
    }

    public static float Square(float t) {
        return Mathf.Sign(Mathf.Sin(t));
    }

    public static float Triangle(float t) {
        return Mathf.PingPong(t, 2) - 1;
    }
}
