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
        return Mathf.Asin(Mathf.Sin(t)) * (2.0f / Mathf.PI);
    }

    public static float Saw(float t) {
		float output = 0.0f;

		for (float n = 1.0f; n < 40.0; n++)
			output += (Mathf.Sin(n * t)) / n;

		return output * (2.0f / Mathf.PI);
    }
}
