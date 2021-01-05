using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dominion.FunctionLibrary;

public class MeshGenerator
{
    #region Basic form

    // Bottom plane Rule : Always Left Back point is first
    // 1←←←4
    // ↓OOO↑
    // ↓OOO↑
    // 2→→→3
    //
    public Mesh Panel (float width, float height, float depth)
    {
        return Panel(width, height, depth, Vector3.zero, Vector3.forward);
    }
    public Mesh Panel (float width, float height, float depth, Vector3 origin, Vector3 dir)
    {
        Vector3[] bottomVert =
        {
            new Vector3(-width, -height, depth) * 0.5f,
            new Vector3(-width, -height, -depth) * 0.5f,
            new Vector3(width, -height, -depth) * 0.5f,
            new Vector3(width, -height, depth) * 0.5f
        };

        Vector2[] bottomUV = new Vector2[bottomVert.Length];

        int[] bottomTri =
        {
            0, 1, 2,
            0, 2, 3
        };

        for (int i = 0; i < bottomVert.Length; i++)
        {
            bottomUV[i] = new Vector2(bottomVert[i].x, bottomVert[i].z);
        }

        return BoardFromBottomPlane(bottomVert, bottomUV, bottomTri, height, origin, dir);
    }
    public Mesh Panel (Vector3 LB, Vector3 RB, Vector3 RT, Vector3 LT, float thick)
    {
        Vector3[] vert =
        {
            // Bottom
            new Vector3(LB.x, LB.y, LB.z + thick),
            new Vector3(LB.x, LB.y, LB.z),
            new Vector3(RB.x, RB.y, RB.z),
            new Vector3(RB.x, RB.y, RB.z + thick),
            
            // LR
            new Vector3(LB.x, LB.y, LB.z + thick),
            new Vector3(LT.x, LT.y, LT.z + thick),
            new Vector3(LT.x, LT.y, LT.z),
            new Vector3(LB.x, LB.y, LB.z),

            new Vector3(RB.x, RB.y, RB.z),
            new Vector3(RT.x, RT.y, RT.z),
            new Vector3(RT.x, RT.y, RT.z + thick),
            new Vector3(RB.x, RB.y, RB.z + thick),

            // BF
            new Vector3(LT.x, LT.y, LT.z + thick),
            new Vector3(LB.x, LB.y, LB.z + thick),
            new Vector3(RB.x, RB.y, RB.z + thick),
            new Vector3(RT.x, RT.y, RT.z + thick),

            new Vector3(LB.x, LB.y, LB.z),
            new Vector3(LT.x, LT.y, LT.z),
            new Vector3(RT.x, RT.y, RT.z),
            new Vector3(RB.x, RB.y, RB.z),

            // Top
            new Vector3(LT.x, LT.y, LT.z),
            new Vector3(LT.x, LT.y, LT.z + thick),
            new Vector3(RT.x, RT.y, RT.z + thick),
            new Vector3(RT.x, RT.y, RT.z),

        };

        Vector2[] uv =
        {
            new Vector2(LB.x, LB.z + thick),
            new Vector2(LB.x, LB.z),
            new Vector2(RB.x, RB.z),
            new Vector2(RB.x, RB.z + thick),

            new Vector2(LB.y, LB.z + thick),
            new Vector2(LT.y, LT.z + thick),
            new Vector2(LT.y, LT.z),
            new Vector2(LB.y, LB.z),

            new Vector2(RB.y, RB.z),
            new Vector2(RT.y, RT.z),
            new Vector2(RT.y, RT.z + thick),
            new Vector2(RB.y, RB.z + thick),

            new Vector2(LT.x, LT.y),
            new Vector2(LB.x, LB.y),
            new Vector2(RB.x, RB.y),
            new Vector2(RT.x, RT.y),

            new Vector2(LB.x, LB.y),
            new Vector2(LT.x, LT.y),
            new Vector2(RT.x, RT.y),
            new Vector2(RB.x, RB.y),

            new Vector2(LT.x, LT.z),
            new Vector2(LT.x, LT.z + thick),
            new Vector2(RT.x, RT.z + thick),
            new Vector2(RT.x, RT.z)
        };

        int[] tri =
        {
            0, 1, 2,
            0, 2, 3,

            4, 5, 6,
            4, 6, 7,

            8, 9, 10,
            8, 10, 11,

            12, 13, 14,
            12, 14, 15,

            16, 17, 18,
            16, 18, 19,

            20, 21, 22,
            20, 22, 23,
        };

        return GenerateMesh(vert, uv, tri);
    }


    /// <summary>
    /// 9-Slice door mesh.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="depth"></param>
    /// <param name="sliceCoord"> x : stile, y : rail</param>
    /// <param name="uvCoord"> x : stilePixel / widthPixel <br/> y : railPixel / heightPixel</param>
    /// <param name="origin"></param>
    /// <param name="dir"></param>
    /// <returns></returns>
    public Mesh SlicedDoorNormal (float width, float height, float depth, Vector2 sliceCoord, Vector2 uvCoord, Vector3 origin, Vector3 dir)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;

        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        float stile = sliceCoord.x;
        float rail = sliceCoord.y;

        Vector3[] verts =
        {
            // Front
            new Vector3(-width * 0.5f, -height * 0.5f, -depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, -depth * 0.5f),

            new Vector3(-width * 0.5f + stile, -height * 0.5f + rail, -depth * 0.5f),
            new Vector3(-width * 0.5f + stile, height * 0.5f - rail, -depth * 0.5f),
            new Vector3(width * 0.5f - stile, height * 0.5f - rail, -depth * 0.5f),
            new Vector3(width * 0.5f - stile, -height * 0.5f + rail, -depth * 0.5f),

            new Vector3(-width * 0.5f, -height * 0.5f + rail, -depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f - rail, -depth * 0.5f),

            new Vector3(-width * 0.5f + stile, height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f - stile, height * 0.5f, -depth * 0.5f),

            new Vector3(width * 0.5f, height * 0.5f - rail, -depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f + rail, -depth * 0.5f),

            new Vector3(width * 0.5f - stile, -height * 0.5f, -depth * 0.5f),
            new Vector3(-width * 0.5f + stile, -height * 0.5f, -depth * 0.5f),

            // Sides (L , R)
            new Vector3(-width * 0.5f, -height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(-width * 0.5f, -height * 0.5f, -depth * 0.5f),

            new Vector3(width * 0.5f, -height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, depth * 0.5f),

            // Edges (B, T)
            new Vector3(-width * 0.5f, -height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, -height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, depth * 0.5f),

            new Vector3(-width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, -depth * 0.5f),

            // Back
            new Vector3(width * 0.5f, -height * 0.5f, depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, -height * 0.5f, depth * 0.5f),
        };

        Vector2[] uv2 =
        {
            // Front
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),

            new Vector2(uvCoord.x, uvCoord.y),
            new Vector2(uvCoord.x, 1 - uvCoord.y),
            new Vector2(1 - uvCoord.x, 1 - uvCoord.y),
            new Vector2(1 - uvCoord.x, uvCoord.y),

            new Vector2(0, uvCoord.y),
            new Vector2(0, 1 - uvCoord.y),

            new Vector2(uvCoord.x, 1),
            new Vector2(1 - uvCoord.x, 1),

            new Vector2(1, 1 - uvCoord.y),
            new Vector2(1, uvCoord.y),

            new Vector2(1 - uvCoord.x, 0),
            new Vector2(uvCoord.x, 0),

            // Sides (L , R)
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),

            // Edges (B, T)
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),

            // Back
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0f),
        };

        Vector2[] uvs =
        {
            // Front
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),

            new Vector2(uvCoord.x, uvCoord.y),
            new Vector2(uvCoord.x, 1 - uvCoord.y),
            new Vector2(1 - uvCoord.x, 1 - uvCoord.y),
            new Vector2(1 - uvCoord.x, uvCoord.y),

            new Vector2(0, uvCoord.y),
            new Vector2(0, 1 - uvCoord.y),

            new Vector2(uvCoord.x, 1),
            new Vector2(1 - uvCoord.x, 1),

            new Vector2(1, 1 - uvCoord.y),
            new Vector2(1, uvCoord.y),

            new Vector2(1 - uvCoord.x, 0),
            new Vector2(uvCoord.x, 0),

            // Sides (L , R)
            new Vector2(depth, 0),
            new Vector2(depth, height),
            new Vector2(0, height),
            new Vector2(0, 0),

            new Vector2(0, 0),
            new Vector2(0, height),
            new Vector2(depth, height),
            new Vector2(depth, 0),

            // Edges (B, T)
            new Vector2(0, depth),
            new Vector2(0, 0),
            new Vector2(width, 0),
            new Vector2(width, depth),

            new Vector2(0, 0),
            new Vector2(0, depth),
            new Vector2(width, depth),
            new Vector2(width, 0),

            // Back
            new Vector2(width, 0),
            new Vector2(width, height),
            new Vector2(0, height),
            new Vector2(0, 0)
        };

        int[] tris =
        {
            // Front
            0, 8, 15,
            8, 4, 15,
            8, 9, 4,
            9, 5, 4,
            9, 1, 5,
            1, 10, 5,

            10, 11, 5,
            11, 6, 5,
            11, 2, 6,
            2, 12, 6,

            12, 13, 6,
            13, 7, 6,

            3, 14, 13,
            14, 7, 13,
            14, 15, 7,
            15, 4, 7,

            4, 5, 7,
            5, 6, 7,

            // Sides (L , R)
            16, 17, 18,
            16, 18, 19,

            20, 21, 22,
            20, 22, 23,

            // Edges (B, T)
            24, 25, 26,
            24, 26, 27,

            28, 29, 30,
            28, 30, 31,

            // Back
            32, 33, 34,
            32, 34, 35
        };

        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = trans.inverse.MultiplyVector(verts[i]);
        }

        return GenerateMesh(verts, uvs, uv2, tris);
    }
    public Mesh SlicedDoorAlpha (float width, float height, float depth, Vector2 sliceCoord, Vector2 uvCoord, Vector3 origin, Vector3 dir)
    {
        dir = dir == Vector3.zero ? Vector3.forward : dir;

        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir, Vector3.up), Vector3.one);

        float stile = sliceCoord.x;
        float rail = sliceCoord.y;

        Vector3[] verts =
        {
            // Front
            new Vector3(-width * 0.5f, -height * 0.5f, -depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, -depth * 0.5f),

            new Vector3(-width * 0.5f + stile, -height * 0.5f + rail, -depth * 0.5f),
            new Vector3(-width * 0.5f + stile, height * 0.5f - rail, -depth * 0.5f),
            new Vector3(width * 0.5f - stile, height * 0.5f - rail, -depth * 0.5f),
            new Vector3(width * 0.5f - stile, -height * 0.5f + rail, -depth * 0.5f),

            new Vector3(-width * 0.5f, -height * 0.5f + rail, -depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f - rail, -depth * 0.5f),

            new Vector3(-width * 0.5f + stile, height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f - stile, height * 0.5f, -depth * 0.5f),

            new Vector3(width * 0.5f, height * 0.5f - rail, -depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f + rail, -depth * 0.5f),

            new Vector3(width * 0.5f - stile, -height * 0.5f, -depth * 0.5f),
            new Vector3(-width * 0.5f + stile, -height * 0.5f, -depth * 0.5f),

            // Sides (L , R)
            new Vector3(-width * 0.5f, -height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(-width * 0.5f, -height * 0.5f, -depth * 0.5f),

            new Vector3(width * 0.5f, -height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, depth * 0.5f),

            // Edges (B, T)
            new Vector3(-width * 0.5f, -height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, -height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, -depth * 0.5f),
            new Vector3(width * 0.5f, -height * 0.5f, depth * 0.5f),

            new Vector3(-width * 0.5f, height * 0.5f, -depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, -depth * 0.5f),

            // Back
            new Vector3(width * 0.5f, -height * 0.5f, depth * 0.5f),
            new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, height * 0.5f, depth * 0.5f),
            new Vector3(-width * 0.5f, -height * 0.5f, depth * 0.5f),
        };

        Vector2[] uvs =
        {
            // Front
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),

            new Vector2(uvCoord.x, uvCoord.y),
            new Vector2(uvCoord.x, 1 - uvCoord.y),
            new Vector2(1 - uvCoord.x, 1 - uvCoord.y),
            new Vector2(1 - uvCoord.x, uvCoord.y),

            new Vector2(0, uvCoord.y),
            new Vector2(0, 1 - uvCoord.y),

            new Vector2(uvCoord.x, 1),
            new Vector2(1 - uvCoord.x, 1),

            new Vector2(1, 1 - uvCoord.y),
            new Vector2(1, uvCoord.y),

            new Vector2(1 - uvCoord.x, 0),
            new Vector2(uvCoord.x, 0),

            // Sides (L , R)
            new Vector2(depth, 0),
            new Vector2(depth, height),
            new Vector2(0, height),
            new Vector2(0, 0),

            new Vector2(0, 0),
            new Vector2(0, height),
            new Vector2(depth, height),
            new Vector2(depth, 0),

            // Edges (B, T)
            new Vector2(0, depth),
            new Vector2(0, 0),
            new Vector2(width, 0),
            new Vector2(width, depth),

            new Vector2(0, 0),
            new Vector2(0, depth),
            new Vector2(width, depth),
            new Vector2(width, 0),

            // Back
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0),
            new Vector2(0, 0)
        };

        int[] tris =
        {
            // Front
            0, 8, 15,
            8, 4, 15,
            8, 9, 4,
            9, 5, 4,
            9, 1, 5,
            1, 10, 5,

            10, 11, 5,
            11, 6, 5,
            11, 2, 6,
            2, 12, 6,

            12, 13, 6,
            13, 7, 6,

            3, 14, 13,
            14, 7, 13,
            14, 15, 7,
            15, 4, 7,

            4, 5, 7,
            5, 6, 7,

            // Sides (L , R)
            16, 17, 18,
            16, 18, 19,

            20, 21, 22,
            20, 22, 23,

            // Edges (B, T)
            24, 25, 26,
            24, 26, 27,

            28, 29, 30,
            28, 30, 31,

            // Back
            32, 33, 34,
            32, 34, 35
        };

        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] = trans.inverse.MultiplyVector(verts[i]);
        }

        return GenerateMesh(verts, uvs, tris);
    }

    #endregion

    #region Else
    /// Countertop Rule
    /// (1)←←←←←←←←←(10)(4)
    /// ↓ooo(8)→→(9)(5)ooo↑
    /// ↓ooo(7)←←←←←(6)ooo↑
    /// (2)→→→→→→→→→→→→→(3)
    public Mesh CounterTop (List<Vector3> points, float height)
    {
        Vector3 origin = points[0];
        Vector3 forward = (points[points.Count - 1] - points[0]).normalized;

        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(forward, Vector3.up), Vector3.one);

        Vector2[] points2D = new Vector2[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            points2D[i] = new Vector2(points[i].x, points[i].z);
        }

        Triangulator tr = new Triangulator(points2D);
        int[] tri = tr.Triangulate();
        Vector3[] vert = new Vector3[points.Count];
        for (int i = 0; i < points.Count; ++i)
        {
            vert[i] = trans.inverse.MultiplyPoint(points[i]);
        }

        return BoardFromBottomPlane(vert.ToArray(), points2D, tri.Reverse().ToArray(), height, Vector3.zero, Vector3.forward);
    }
    public Mesh CounterTop (List<Vector3> points, Vector3 sinkOrigin, Vector3 sinkForward, Vector2 sinkSize, float height)
    {
        Vector3 origin = points[0];
        Vector3 forward = (points[points.Count - 1] - points[0]).normalized;

        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(forward, Vector3.up), Vector3.one);

        Vector3 sinkRight = Vector3.Cross(Vector3.up, sinkForward);
        Vector3[] sinkPoints =
        {
            sinkOrigin - (sinkRight * sinkSize.x/2) - (sinkForward * sinkSize.y/2),
            sinkOrigin - (sinkRight * sinkSize.x/2) + (sinkForward * sinkSize.y/2),
            sinkOrigin + (sinkRight * sinkSize.x/2) + (sinkForward * sinkSize.y/2),
            sinkOrigin + (sinkRight * sinkSize.x/2) - (sinkForward * sinkSize.y/2),
        };
        List<Vector3> vert = new List<Vector3>();
        for (int i = 0; i < sinkPoints.Length; i++)
        {
            sinkPoints[i].y = points[0].y;
        }
        Dictionary<Vector2, float> dists = new Dictionary<Vector2, float>();

        for (int i = 0; i < points.Count; i++)
        {
            for (int j = 0; j < sinkPoints.Length; j++)
            {
                dists.Add(new Vector2Int(i, j), Vector3.Distance(points[i], sinkPoints[j]));
            }
        }
        int closestPointIndex = 0;
        int sinkClosestPoint = 0;
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = 0; j < sinkPoints.Length; j++)
            {
                if (Mathf.Min(dists.Values.ToArray()) == dists[new Vector2Int(i, j)])
                {
                    closestPointIndex = i;
                    sinkClosestPoint = j;
                }
            }
        }
        for (int i = 0; i < points.Count; i++)// Vector3 point in points)
        {
            if (closestPointIndex + i < points.Count)
            {
                vert.Add(points[closestPointIndex + i]);
            }
            else
            {
                vert.Add(points[closestPointIndex + i - points.Count]);
            }
        }

        Vector2[] _vert2D = new Vector2[vert.Count];

        for (int i = 0; i < vert.Count; i++)
        {
            _vert2D[i] = new Vector2(vert[i].x, vert[i].z);
        }

        Triangulator _tr = new Triangulator(_vert2D);
        int[] _tri = _tr.Triangulate();

        List<bool> contains = new List<bool>();
        for (int i = 0; i < sinkPoints.Length; i++)
        {
            List<bool> pointContain = new List<bool>();
            for (int j = 0; j + 2 < _tri.Length; j += 3)
            {
                pointContain.Add(CalculateHelper.IsContain(new Vector2(points[_tri[j]].x, points[_tri[j]].z),
                    new Vector2(points[_tri[j + 1]].x, points[_tri[j + 1]].z),
                    new Vector2(points[_tri[j + 2]].x, points[_tri[j + 2]].z),
                    new Vector2(sinkPoints[i].x, sinkPoints[i].z)));
            }
            if (pointContain.Contains(true))
            {
                contains.Add(true);
            }
            else
            {
                contains.Add(false);
            }
        }

        if (!contains.Contains(false))
        {
            Vector3 prePoint = closestPointIndex == 0 ? points[points.Count - 1] : points[closestPointIndex - 1];
            Vector3 nextPoint = closestPointIndex == points.Count + 1 ? points[0] : points[closestPointIndex + 1];
            int nextDir = (nextPoint.x - points[closestPointIndex].x > 0) || (nextPoint.z - points[closestPointIndex].z > 0) ? -1 : 1;
            int sinkisLeft = sinkPoints[sinkClosestPoint].x < points[closestPointIndex].x ? -1 : 1;
            vert.Add(points[closestPointIndex] + (prePoint - points[closestPointIndex]).normalized * 0.00001f * nextDir * sinkisLeft);

            for (int i = 0; i < sinkPoints.Length; i++)
            {
                //vert.Add(sinkPoints[i]);
                if (sinkClosestPoint + i < sinkPoints.Length)
                {
                    vert.Add(sinkPoints[sinkClosestPoint + i]);
                }
                else
                {
                    vert.Add(sinkPoints[sinkClosestPoint + i - sinkPoints.Length]);
                }
            }
            vert.Add(sinkPoints[sinkClosestPoint] - (prePoint - points[closestPointIndex]).normalized * 0.00001f * nextDir * sinkisLeft);
        }

        Vector2[] vert2D = new Vector2[vert.Count];

        for (int i = 0; i < vert.Count; i++)
        {
            vert2D[i] = new Vector2(vert[i].x, vert[i].z);
        }

        Triangulator tr = new Triangulator(vert2D);
        int[] tri = tr.Triangulate();

        for (int i = 0; i < vert.Count; ++i)
        {
            vert[i] = trans.inverse.MultiplyPoint(vert[i]);
        }

        return BoardFromBottomPlane(vert.ToArray(), vert2D, tri.Reverse().ToArray(), height, Vector3.zero, Vector3.forward);
    }
    #endregion 

    #region Helper
    protected Mesh BoardFromPoints (Vector3[] points, float thick, Vector3 origin, Vector3 dir, bool isReverseLR = false, bool uvRandom = false)
    {
        Vector2[] uv = new Vector2[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            uv[i] = new Vector2(points[i].x, points[i].z);
        }

        Triangulator triangulator = new Triangulator(uv);
        int[] tri = triangulator.Triangulate();

        return BoardFromBottomPlane(points, uv, tri.Reverse().ToArray(), thick, origin, dir, isReverseLR, uvRandom);
    }
    protected Mesh BoardFromBottomPlane (Vector3[] bottomVert, Vector2[] bottomUV, int[] bottomTri, float thick, Vector3 origin, Vector3 dir, bool isReverseLR = false, bool uvRandom = false)
    {
        Vector3 transScale = isReverseLR ? new Vector3(-1, 1, 1) : Vector3.one;
        dir = dir == Vector3.zero ? Vector3.forward : dir;
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir), transScale);

        Vector2 uvFirstPoint = uvRandom == true ? new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)) : Vector2.zero;

        Vector3[] topVert = new Vector3[bottomVert.Length];
        for (int i = 0; i < topVert.Length; i++)
        {
            topVert[i] = bottomVert[i] + Vector3.up * thick;
        }

        int n = 0;
        Vector3[] sideVert = new Vector3[bottomVert.Length * 4];
        for (int i = 0; i + 4 < sideVert.Length; i += 4)
        {
            sideVert[i] = bottomVert[n];
            sideVert[i + 1] = bottomVert[n + 1];
            sideVert[i + 2] = topVert[n + 1];
            sideVert[i + 3] = topVert[n];
            n += 1;
        }
        sideVert[sideVert.Length - 4] = bottomVert[n];
        sideVert[sideVert.Length - 3] = bottomVert[0];
        sideVert[sideVert.Length - 2] = topVert[0];
        sideVert[sideVert.Length - 1] = topVert[n];

        Vector2[] sideUv = new Vector2[sideVert.Length];

        sideUv[0] = uvFirstPoint;
        sideUv[1] = uvFirstPoint + new Vector2(Vector3.Distance(sideVert[0], sideVert[1]), 0);
        sideUv[2] = sideUv[1] + Vector2.up * thick;
        sideUv[3] = sideUv[0] + Vector2.up * thick;

        for (int i = 4; i + 4 <= sideUv.Length; i += 4)
        {
            sideUv[i] = sideUv[i - 3];
            sideUv[i + 1] = new Vector2(sideUv[i].x + Vector3.Distance(sideVert[i], sideVert[i + 1]), 0);
            sideUv[i + 2] = sideUv[i + 1] + Vector2.up * thick;
            sideUv[i + 3] = sideUv[i] + Vector2.up * thick;
        }

        int[] sideTri = new int[sideVert.Length / 2 * 3];
        n = 0;
        for (int i = 0; i + 6 <= sideTri.Length; i += 6)
        {
            sideTri[i] = n;
            sideTri[i + 1] = n + 1;
            sideTri[i + 2] = n + 2;
            sideTri[i + 3] = n;
            sideTri[i + 1 + 3] = n + 1 + 1;
            sideTri[i + 2 + 3] = n + 2 + 1;
            n += 4;
        }

        if (isReverseLR)
        {
            bottomTri = bottomTri.Reverse().ToArray();
        }

        Mesh[] meshes =
        {
            GenerateMesh(bottomVert, bottomUV, bottomTri),
            GenerateMesh(sideVert, sideUv, isReverseLR == true? sideTri: sideTri.Reverse().ToArray()),
            GenerateMesh(topVert, bottomUV, bottomTri.Reverse().ToArray())
        };

        Mesh newMesh = Combine(meshes);
        Vector3[] newVec = new Vector3[newMesh.vertexCount];
        for (int i = 0; i < newMesh.vertexCount; i++)
        {
            newVec[i] = trans.MultiplyPoint(newMesh.vertices[i]);
        }
        newMesh.vertices = newVec;
        newMesh.RecalculateBounds();
        newMesh.RecalculateNormals();
        newMesh.RecalculateTangents();

        return newMesh;
    }
    protected Mesh GenerateMesh (Vector3[] vert, Vector2[] uv, int[] tri)
    {
        Mesh mesh = new Mesh
        {
            vertices = vert,
            uv = uv,
            triangles = tri
        };

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }
    protected Mesh GenerateMesh (Vector3[] vert, Vector2[] uv, Vector2[] uv2, int[] tri)
    {
        Mesh mesh = new Mesh
        {
            vertices = vert,
            uv = uv,
            uv2 = uv2,
            triangles = tri
        };

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }
    public Mesh Combine (Mesh[] meshes)
    {
        Mesh mesh = new Mesh();

        CombineInstance[] combine = new CombineInstance[meshes.Length];

        for (int i = 0; i < meshes.Length; i++)
        {
            combine[i].mesh = meshes[i];
            combine[i].transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        }

        mesh.CombineMeshes(combine);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        return mesh;
    }
    public Mesh Combine (GameObject[] objs)
    {
        Mesh mesh = new Mesh();

        CombineInstance[] combine = new CombineInstance[objs.Length];

        for (int i = 0; i < objs.Length; i++)
        {
            combine[i].mesh = objs[i].GetComponent<MeshFilter>().mesh;
            combine[i].transform = objs[i].transform.localToWorldMatrix;
        }

        mesh.CombineMeshes(combine);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        foreach (GameObject obj in objs)
        {
            Object.Destroy(obj);
        }

        return mesh;
    }
    public GameObject Combine (GameObject rootObj)
    {
        Vector3 basePosition = rootObj.transform.position;
        Quaternion baseRotation = rootObj.transform.rotation;
        rootObj.transform.position = Vector3.zero;
        rootObj.transform.rotation = Quaternion.identity;

        ArrayList materials = new ArrayList();
        ArrayList combineInstanceArrays = new ArrayList();
        MeshFilter[] meshFilters = rootObj.gameObject.GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

            if (!meshRenderer ||
                !meshFilter.sharedMesh ||
                meshRenderer.sharedMaterials.Length != meshFilter.sharedMesh.subMeshCount)
            {
                continue;
            }

            for (int s = 0; s < meshFilter.sharedMesh.subMeshCount; s++)
            {
                int materialArrayIndex = -1;
                for (int i = 0; i < materials.Count; i++)
                {
                    if (((Material)materials[i]).name == meshRenderer.sharedMaterials[s].name)
                    {
                        materialArrayIndex = i;
                    }
                }
                if (materialArrayIndex == -1)
                {
                    materials.Add(meshRenderer.sharedMaterials[s]);
                    materialArrayIndex = materials.Count - 1;
                }
                combineInstanceArrays.Add(new ArrayList());

                CombineInstance combineInstance = new CombineInstance();
                combineInstance.transform = meshRenderer.transform.localToWorldMatrix;
                combineInstance.subMeshIndex = s;
                combineInstance.mesh = meshFilter.sharedMesh;
                (combineInstanceArrays[materialArrayIndex] as ArrayList).Add(combineInstance);
            }
        }

        // Get / Create mesh filter & renderer
        MeshFilter meshFilterCombine = rootObj.gameObject.GetComponent<MeshFilter>();
        if (meshFilterCombine == null)
        {
            meshFilterCombine = rootObj.gameObject.AddComponent<MeshFilter>();
        }
        MeshRenderer meshRendererCombine = rootObj.gameObject.GetComponent<MeshRenderer>();
        if (meshRendererCombine == null)
        {
            meshRendererCombine = rootObj.gameObject.AddComponent<MeshRenderer>();
        }

        // Combine by material index into per-material meshes
        // also, Create CombineInstance array for next step
        Mesh[] meshes = new Mesh[materials.Count];
        CombineInstance[] combineInstances = new CombineInstance[materials.Count];

        for (int m = 0; m < materials.Count; m++)
        {
            CombineInstance[] combineInstanceArray = (combineInstanceArrays[m] as ArrayList).ToArray(typeof(CombineInstance)) as CombineInstance[];
            meshes[m] = new Mesh();
            meshes[m].CombineMeshes(combineInstanceArray, true, true);

            combineInstances[m] = new CombineInstance();
            combineInstances[m].mesh = meshes[m];
            combineInstances[m].subMeshIndex = 0;
        }

        // Combine into one
        meshFilterCombine.sharedMesh = new Mesh();
        meshFilterCombine.sharedMesh.CombineMeshes(combineInstances, false, false);

        // Destroy other meshes
        foreach (Mesh oldMesh in meshes)
        {
            oldMesh.Clear();
            Object.DestroyImmediate(oldMesh);
        }

        // Assign materials
        Material[] materialsArray = materials.ToArray(typeof(Material)) as Material[];
        meshRendererCombine.materials = materialsArray;

        foreach (MeshFilter meshFilter in meshFilters)
        {
            Object.DestroyImmediate(meshFilter.gameObject);
        }

        rootObj.transform.position = basePosition;
        rootObj.transform.rotation = baseRotation;

        return rootObj;
    }
    #endregion


}

