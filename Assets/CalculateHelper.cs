using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateHelper
{
    public static float GetDampenFactor (float dampening, float deltaTime)
    {
        if (dampening < 0.0f)
            return 1.0f;

        if (Application.isPlaying == false)
            return 1.0f;

        return 1.0f - Mathf.Exp(-dampening * deltaTime);
    }
    public static Vector3 GetBezierPoint (float t, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p1;
        p += 2 * u * t * p2;
        p += tt * p3;
        return p;
    }
    public static Vector3 GetCenterOfPoints (List<Vector3> points, float height)
    {

        float totalX = 0;
        float totalZ = 0;
        float centerX;
        float centerZ;

        for (int j = 0; j < points.Count; j++)
        {
            totalX += points[j].x;
            totalZ += points[j].z;
        }
        centerX = totalX / points.Count;
        centerZ = totalZ / points.Count;

        Vector3 center = new Vector3(centerX, height / 2, centerZ);

        return center;
    }
    public static bool IsContain (Vector2 a, Vector2 b, Vector2 c, Vector2 p)
    {
        var c1 = (b.x - a.x) * (p.y - b.y) - (b.y - a.y) * (p.x - b.x);
        var c2 = (c.x - b.x) * (p.y - c.y) - (c.y - b.y) * (p.x - c.x);
        var c3 = (a.x - c.x) * (p.y - a.y) - (a.y - c.y) * (p.x - a.x);

        return c1 > 0f && c2 > 0f && c3 > 0f || c1 < 0f && c2 < 0f && c3 < 0f;
    }
}
