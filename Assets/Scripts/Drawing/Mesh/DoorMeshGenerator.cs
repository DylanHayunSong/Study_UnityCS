using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMeshGenerator : MeshGenerator
{
    // Front Vertex Rule
    // 
    // 1→→→→→2
    // ↑O5→6O↓
    // ↑O↑O↓O↓
    // ↑O4←7O↓
    // 0←←←←←3
    //

    #region Edge
    public Mesh SquareEdge (float width, float height, float stile, float rail, float thick)
    {
        return SquareEdge(width, height, stile, rail, thick, Vector3.zero, Vector3.forward);
    }
    public Mesh SquareEdge (float width, float height, float stile, float rail, float thick, Vector3 origin, Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            direction = Vector3.forward;
        }


        List<Mesh> meshes = new List<Mesh>();

        Vector2 uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        float interval = (stile + rail + thick) * 0.03f;
        float centerInt = interval * 1.5f;
        float frameD = interval * 2f;

        Vector3[] fVert =
        {
            // 0
            new Vector3(-(width * 0.5f), -(height * 0.5f), -(thick * 0.5f - frameD)),
            new Vector3(-(width * 0.5f), (height * 0.5f), -(thick * 0.5f - frameD)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), (height * 0.5f), -(thick * 0.5f - frameD)),
            new Vector3((width * 0.5f - (stile - centerInt)), (height * 0.5f), -(thick * 0.5f - frameD)),
            new Vector3((width * 0.5f), (height * 0.5f), -(thick * 0.5f - frameD)),
            new Vector3((width * 0.5f), -(height * 0.5f), -(thick * 0.5f - frameD)),
            new Vector3((width * 0.5f - (stile - centerInt)), -(height * 0.5f), -(thick * 0.5f - frameD)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), -(height * 0.5f), -(thick * 0.5f - frameD)),

            // 8
            new Vector3(-(width * 0.5f - interval), -(height * 0.5f - interval), -(thick * 0.5f - interval * 0.2f)),
            new Vector3(-(width * 0.5f - interval), (height * 0.5f - interval), -(thick * 0.5f - interval * 0.2f)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), (height * 0.5f - interval), -(thick * 0.5f - interval * 0.2f)),
            new Vector3((width * 0.5f - (stile - centerInt)), (height * 0.5f - interval), -(thick * 0.5f - interval * 0.2f)),
            new Vector3((width * 0.5f - interval), (height * 0.5f - interval), -(thick * 0.5f - interval)),
            new Vector3((width * 0.5f - interval), -(height * 0.5f - interval), -(thick * 0.5f - interval)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), -(height * 0.5f - interval), -(thick * 0.5f - interval * 0.2f)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), -(height * 0.5f - interval), -(thick * 0.5f - interval * 0.2f)),

            // 16
            new Vector3(-(width * 0.5f - interval), -(height * 0.5f - interval), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - interval), (height * 0.5f - interval), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), (height * 0.5f - interval), -(thick * 0.5f)),
            new Vector3((width * 0.5f - (stile - centerInt)), (height * 0.5f - interval), -(thick * 0.5f)),
            new Vector3((width * 0.5f - interval), (height * 0.5f - interval), -(thick * 0.5f)),
            new Vector3((width * 0.5f - interval), -(height * 0.5f - interval), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), -(height * 0.5f - interval), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), -(height * 0.5f - interval), -(thick * 0.5f)),

            // 24
            new Vector3(-(width * 0.5f - (stile - centerInt)), -(height * 0.5f - (rail - centerInt)), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), (height * 0.5f - (rail - centerInt)), -(thick * 0.5f)),
            new Vector3((width * 0.5f - (stile - centerInt)), (height * 0.5f - (rail - centerInt)), -(thick * 0.5f)),
            new Vector3((width * 0.5f - (stile - centerInt)), -(height * 0.5f - (rail - centerInt)), -(thick * 0.5f)),

            // 28
            new Vector3(-(width * 0.5f - (stile - centerInt)), -(height * 0.5f - (rail - centerInt)), -(thick * 0.5f - interval * 0.3f)),
            new Vector3(-(width * 0.5f - (stile - centerInt)), (height * 0.5f - (rail - centerInt)), -(thick * 0.5f - interval * 0.3f)),
            new Vector3((width * 0.5f - (stile - centerInt)), (height * 0.5f - (rail - centerInt)), -(thick * 0.5f - interval * 0.3f)),
            new Vector3((width * 0.5f - (stile - centerInt)), -(height * 0.5f - (rail - centerInt)), -(thick * 0.5f - interval * 0.3f)),

            // 32
            new Vector3(-(width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - frameD)),
            new Vector3(-(width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - frameD)),
            new Vector3((width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - frameD)),
            new Vector3((width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - frameD)),
        };

        #region Side
        Vector3[] LVert =
        {
            // Front 0
            fVert[0], fVert[1], fVert[9], fVert[8],
            fVert[8], fVert[9], fVert[17],fVert[16],
            fVert[9], fVert[1], fVert[2], fVert[10],
            fVert[17], fVert[9], fVert[10], fVert[18],
            fVert[16], fVert[17], fVert[18], fVert[23],
            fVert[0], fVert[8], fVert[15], fVert[7],
            fVert[8], fVert[16], fVert[23], fVert[15],
            fVert[24], fVert[25], fVert[29], fVert[28],
            fVert[28], fVert[29], fVert[33], fVert[32],

            // Back 36
            new Vector3(fVert[32].x, fVert[32].y, thick * 0.5f),
            new Vector3(fVert[33].x, fVert[33].y, thick * 0.5f),
            new Vector3(fVert[29].x, fVert[29].y, thick * 0.5f),
            new Vector3(fVert[28].x, fVert[28].y, thick * 0.5f),

            new Vector3(fVert[7].x, fVert[7].y, thick * 0.5f),
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f),
            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f),

            // Side 44 LR
            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f),
            new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f),
            fVert[1],
            fVert[0],

            fVert[32],
            fVert[33],
            new Vector3(fVert[33].x, fVert[33].y, thick * 0.5f),
            new Vector3(fVert[32].x, fVert[32].y, thick * 0.5f),

            // Side 52 TB

            fVert[1],
            new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f),
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            fVert[2],

            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f),
            fVert[0],
            fVert[7],
            new Vector3(fVert[7].x, fVert[7].y, thick * 0.5f),
        };

        Vector2[] LUV = new Vector2[LVert.Length];
        for (int i = 0; i < 44; i++)
        {
            LUV[i] = new Vector2(LVert[i].x, LVert[i].y) + uvRandom;
        }
        for (int i = 44; i < 52; i++)
        {
            LUV[i] = new Vector2(LVert[i].z, LVert[i].y) + uvRandom;
        }
        for (int i = 52; i < LVert.Length; i++)
        {
            LUV[i] = new Vector2(LVert[i].x, LVert[i].z) + uvRandom;
        }

        int[] LTri =
        {
            0, 1, 3, 1, 2, 3,
            4, 5, 7, 5, 6, 7,
            8, 9, 11, 9, 10, 11,
            12, 13, 15, 13, 14, 15,
            16, 17, 19, 17, 18, 19,
            20, 21, 23, 21, 22, 23,
            24, 25, 27, 25, 26, 27,
            28, 29, 31, 29, 30, 31,
            32, 33, 35, 33, 34, 35,
            36, 37, 39, 37, 38, 39,
            40, 41, 43, 41, 42, 43,
            44, 45, 47, 45, 46, 47,
            48, 49, 51, 49, 50, 51,
            52, 53, 55, 53, 54, 55,
            56, 57, 59, 57, 58, 59
        };

        meshes.Add(GenerateMesh(LVert, LUV, LTri));

        meshes.Add(DecalcoEdge(LVert, LUV, LTri, new Vector3(-1, 1, 1)));
        #endregion

        #region TB
        Vector3[] TVert =
        {
            // Front 0
            fVert[10], fVert[2], fVert[3], fVert[11],
            fVert[18], fVert[10], fVert[11], fVert[19],
            fVert[25], fVert[18], fVert[19], fVert[26],
            fVert[29], fVert[25], fVert[26], fVert[30],
            fVert[33], fVert[29], fVert[30], fVert[34],

            // Back 20
            new Vector3(fVert[26].x, fVert[26].y, thick * 0.5f),
            new Vector3(fVert[3].x, fVert[3].y, thick * 0.5f),
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            new Vector3(fVert[25].x, fVert[25].y, thick * 0.5f),

            new Vector3(fVert[34].x, fVert[34].y, thick * 0.5f),
            new Vector3(fVert[26].x, fVert[26].y, thick * 0.5f),
            new Vector3(fVert[25].x, fVert[25].y, thick * 0.5f),
            new Vector3(fVert[33].x, fVert[33].y, thick * 0.5f),

            // Side TB 28
            new Vector3(fVert[33].x, fVert[33].y, thick * 0.5f), fVert[33], fVert[34], new Vector3(fVert[34].x, fVert[34].y, thick * 0.5f),
            fVert[2], new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f), new Vector3(fVert[3].x, fVert[3].y, thick * 0.5f), fVert[3],
        };

        uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        Matrix4x4 trans;
        trans = Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(Vector3.forward, Vector3.right), Vector3.one);
        Vector2[] TUV = new Vector2[TVert.Length];
        Vector3 rotatedVert;
        for (int i = 0; i < 28; i++)
        {
            rotatedVert = trans.MultiplyPoint(TVert[i]);
            TUV[i] = new Vector2(rotatedVert.x, rotatedVert.y) + uvRandom;
        }
        for (int i = 28; i < TVert.Length; i++)
        {
            rotatedVert = trans.MultiplyPoint(TVert[i]);
            TUV[i] = new Vector2(rotatedVert.z, rotatedVert.y) + uvRandom;
        }


        int[] TTri =
        {
            0, 1, 3, 1, 2, 3,
            4, 5, 7, 5, 6, 7,
            8, 9, 11, 9, 10, 11,
            12, 13, 15, 13, 14, 15,
            16, 17, 19, 17, 18, 19,
            20, 21, 23, 21, 22, 23,
            24, 25, 27, 25, 26, 27,
            28, 29, 31, 29, 30, 31,
            32, 33, 35, 33, 34, 35
        };

        meshes.Add(GenerateMesh(TVert, TUV, TTri));

        meshes.Add(DecalcoEdge(TVert, TUV, TTri, new Vector3(1, -1, 1)));

        #endregion

        Mesh newMesh = Combine(meshes.ToArray());

        return ChangeMeshTransform(newMesh, origin, direction);
    }

    public Mesh ShakerEdge (float width, float height, float stile, float rail, float thick)
    {
        return ShakerEdge(width, height, stile, rail, thick, Vector3.zero, Vector3.forward);
    }
    public Mesh ShakerEdge (float width, float height, float stile, float rail, float thick, Vector3 origin, Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            direction = Vector3.forward;
        }

        List<Mesh> meshes = new List<Mesh>();

        Vector2 uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        float interval = (stile + rail + thick) * 0.03f;
        float edgeD = interval;

        Vector3[] fVert =
        {
            // 0
            new Vector3(-(width * 0.5f), -(height * 0.5f), -(thick * 0.5f - edgeD)),
            new Vector3(-(width * 0.5f), (height * 0.5f), -(thick * 0.5f - edgeD)),
            new Vector3(-(width * 0.5f - stile), (height * 0.5f), -(thick * 0.5f - edgeD)),
            new Vector3((width * 0.5f - stile), (height * 0.5f), -(thick * 0.5f - edgeD)),
            new Vector3((width * 0.5f), (height * 0.5f), -(thick * 0.5f - edgeD)),
            new Vector3((width * 0.5f), -(height * 0.5f), -(thick * 0.5f - edgeD)),
            new Vector3((width * 0.5f - stile), -(height * 0.5f), -(thick * 0.5f - edgeD)),
            new Vector3(-(width * 0.5f- stile), -(height * 0.5f), -(thick * 0.5f - edgeD)),

            // 8
            new Vector3(-(width * 0.5f - edgeD), -(height * 0.5f - edgeD), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - edgeD), (height * 0.5f - edgeD), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - stile), (height * 0.5f - edgeD), -(thick * 0.5f)),
            new Vector3((width * 0.5f - stile), (height * 0.5f - edgeD), -(thick * 0.5f)),
            new Vector3((width * 0.5f - edgeD), (height * 0.5f - edgeD), -(thick * 0.5f)),
            new Vector3((width * 0.5f - edgeD), -(height * 0.5f - edgeD), -(thick * 0.5f)),
            new Vector3((width * 0.5f - stile), -(height * 0.5f - edgeD), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - stile), -(height * 0.5f - edgeD), -(thick * 0.5f)),

            // 16
            new Vector3(-(width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f)),
            new Vector3((width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f)),
            new Vector3((width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f))
        };

        #region Side

        Vector3[] LVert =
        {
            // Front
            fVert[0], fVert[1], fVert[9], fVert[8],
            fVert[8], fVert[9], fVert[10], fVert[15],
            fVert[9], fVert[1], fVert[2], fVert[10],
            fVert[0], fVert[8], fVert[15], fVert[7],

            // Back 16
            new Vector3(fVert[7].x, fVert[7].y, thick * 0.5f),
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f),
            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f),

            // Side 20 LR
            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f),
            new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f),
            fVert[1],
            fVert[0],

            fVert[16],
            fVert[17],
            new Vector3(fVert[17].x, fVert[17].y, thick * 0.5f),
            new Vector3(fVert[16].x, fVert[16].y, thick * 0.5f),

            // Side 28 TB
            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f),
            fVert[0],
            fVert[7],
            new Vector3(fVert[7].x, fVert[7].y, thick * 0.5f),

            fVert[1],
            new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f),
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            fVert[2],
        };

        Vector2[] LUV = new Vector2[LVert.Length];

        for (int i = 0; i < 20; i++)
        {
            LUV[i] = new Vector2(LVert[i].x, LVert[i].y) + uvRandom;
        }
        for (int i = 20; i < 28; i++)
        {
            LUV[i] = new Vector2(LVert[i].z, LVert[i].y) + uvRandom;
        }
        for (int i = 28; i < LVert.Length; i++)
        {
            LUV[i] = new Vector2(LVert[i].x, LVert[i].z) + uvRandom;
        }

        int[] LTri =
        {
            0, 1, 3, 1, 2, 3,
            4, 5, 7, 5, 6, 7,
            8, 9, 11, 9, 10, 11,
            12, 13, 15, 13, 14, 15,
            16, 17, 19, 17, 18, 19,
            20, 21, 23, 21, 22, 23,
            24, 25, 27, 25, 26, 27,
            28, 29, 31, 29, 30, 31,
            32, 33, 35, 33, 34, 35
        };

        meshes.Add(GenerateMesh(LVert, LUV, LTri));

        meshes.Add(DecalcoEdge(LVert, LUV, LTri, new Vector3(-1, 1, 1)));

        #endregion

        #region TB

        Vector3[] TVert =
        {
            // Front 0
            fVert[17], fVert[10], fVert[11], fVert[18],
            fVert[10], fVert[2], fVert[3], fVert[11],

            // Back 8
            new Vector3(fVert[18].x, fVert[18].y, thick * 0.5f),
            new Vector3(fVert[3].x, fVert[3].y, thick * 0.5f),
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            new Vector3(fVert[17].x, fVert[17].y, thick * 0.5f),

            // Side TB 12
            fVert[2],
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            new Vector3(fVert[3].x, fVert[3].y, thick * 0.5f),
            fVert[3],

            new Vector3(fVert[17].x, fVert[17].y, thick * 0.5f),
            fVert[17],
            fVert[18],
            new Vector3(fVert[18].x, fVert[18].y, thick * 0.5f),
        };

        Vector2[] TUV = new Vector2[TVert.Length];

        Matrix4x4 trans;
        trans = Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(Vector3.forward, Vector3.right), Vector3.one);
        Vector3 rotatedVert;
        for (int i = 0; i < 12; i++)
        {
            rotatedVert = trans.MultiplyPoint(TVert[i]);
            TUV[i] = new Vector2(rotatedVert.x, rotatedVert.y) + uvRandom;
        }
        for (int i = 12; i < TVert.Length; i++)
        {
            rotatedVert = trans.MultiplyPoint(TVert[i]);
            TUV[i] = new Vector2(rotatedVert.z, rotatedVert.y) + uvRandom;
        }

        int[] TTri =
        {
            0, 1, 3, 1, 2, 3,
            4, 5, 7, 5, 6, 7,
            8, 9, 11, 9, 10, 11,
            12, 13, 15, 13, 14, 15,
            16, 17, 19, 17, 18, 19
        };

        meshes.Add(GenerateMesh(TVert, TUV, TTri));

        meshes.Add(DecalcoEdge(TVert, TUV, TTri, new Vector3(1, -1, 1)));

        #endregion

        Mesh newMesh = Combine(meshes.ToArray());

        return ChangeMeshTransform(newMesh, origin, direction);
    }
    #endregion

    #region CenterPanel
    public Mesh RaisedPanel (float width, float height, float stile, float rail, float thick)
    {
        return RaisedPanel(width, height, stile, rail, thick, Vector3.zero, Vector3.forward);
    }
    public Mesh RaisedPanel (float width, float height, float stile, float rail, float thick, Vector3 origin, Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            direction = Vector3.forward;
        }

        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(direction), Vector3.one);

        float interval = (stile + rail + thick) * 0.03f;
        float raiseVal = interval * 4f;
        float raiseDepth = interval * 2f;
        float backDepth = interval * 0.2f;
        Vector2 uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        Vector3[] fVert =
        {
            // Front
            new Vector3(-(width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - raiseDepth)),
            new Vector3(-(width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - raiseDepth)),
            new Vector3((width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - raiseDepth)),
            new Vector3((width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - raiseDepth)),

            new Vector3(-(width * 0.5f - raiseVal - stile), -(height * 0.5f - raiseVal - rail), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - raiseVal - stile), (height * 0.5f - raiseVal - rail), -(thick * 0.5f)),
            new Vector3((width * 0.5f - raiseVal - stile), (height * 0.5f - raiseVal - rail), -(thick * 0.5f)),
            new Vector3((width * 0.5f - raiseVal - stile), -(height * 0.5f - raiseVal - rail), -(thick * 0.5f)),
        };

        Vector3[] vert =
        {
            // Front
            fVert[0], fVert[1], fVert[5], fVert[4],
            fVert[5], fVert[1], fVert[2], fVert[6],
            fVert[7], fVert[6], fVert[2], fVert[3],
            fVert[0], fVert[4], fVert[7], fVert[3],
            fVert[4], fVert[5], fVert[6], fVert[7],
            new Vector3(fVert[3].x, fVert[3].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[2].x, fVert[2].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[1].x, fVert[1].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[0].x, fVert[0].y, (thick * 0.5f - backDepth)),
        };

        for (int i = 0; i < vert.Length; i++)
        {
            vert[i] = trans.MultiplyPoint(vert[i]);
        }

        Vector2[] uv = new Vector2[vert.Length];
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = vert[i];
            uv[i] += uvRandom;
        }

        int[] tri =
        {
            0, 1, 3, 1, 2, 3,
            4, 5, 7, 5, 6, 7,
            8, 9, 11, 9, 10, 11,
            12, 13, 15, 13, 14, 15,
            16, 17, 19, 17, 18, 19,
            20, 21, 23, 21, 22, 23
        };


        return GenerateMesh(vert, uv, tri);
    }

    public Mesh FlatPanel (float width, float height, float stile, float rail, float thick)
    {
        return FlatPanel(width, height, stile, rail, thick, Vector3.zero, Vector3.forward);
    }
    public Mesh FlatPanel (float width, float height, float stile, float rail, float thick, Vector3 origin, Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            direction = Vector3.forward;
        }
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(direction), Vector3.one);

        float interval = (stile + rail + thick) * 0.03f;
        float frontDepth = interval * 2f;
        float backDepth = interval * 0.2f;
        Vector2 uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        Vector3[] fVert =
        {
            new Vector3(-(width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - frontDepth)),
            new Vector3(-(width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - frontDepth)),
            new Vector3((width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - frontDepth)),
            new Vector3((width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - frontDepth)),
        };

        Vector3[] vert =
        {
            fVert[0], fVert[1], fVert[2], fVert[3],
            new Vector3(fVert[3].x, fVert[3].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[2].x, fVert[2].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[1].x, fVert[1].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[0].x, fVert[0].y, (thick * 0.5f - backDepth)),
        };

        for (int i = 0; i < vert.Length; i++)
        {
            vert[i] = trans.MultiplyPoint(vert[i]);
        }

        Vector2[] uv = new Vector2[vert.Length];
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = vert[i];
            uv[i] += uvRandom;
        }

        int[] tri =
        {
            0, 1, 3, 1, 2, 3,
            4, 5, 7, 5, 6, 7
        };

        return GenerateMesh(vert, uv, tri);
    }

    public Mesh FlatGlassPanel(float width, float height, float stile, float rail, float thick)
    {
        return FlatGlassPanel(width, height, stile, rail, thick, Vector3.zero, Vector3.forward);
    }
    public Mesh FlatGlassPanel (float width, float height, float stile, float rail, float thick, Vector3 origin, Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            direction = Vector3.forward;
        }
        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(direction), Vector3.one);

        float interval = (stile + rail + thick) * 0.03f;
        float frontDepth = interval * 2f;
        float backDepth = interval * 0.2f;
        Vector2 uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        Vector3[] fVert =
        {
            new Vector3(-(width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - frontDepth)),
            new Vector3(-(width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - frontDepth)),
            new Vector3((width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - frontDepth)),
            new Vector3((width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - frontDepth)),
        };

        Vector3[] vert =
        {
            fVert[0], fVert[1], fVert[2], fVert[3],
            new Vector3(fVert[3].x, fVert[3].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[2].x, fVert[2].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[1].x, fVert[1].y, (thick * 0.5f - backDepth)),
            new Vector3(fVert[0].x, fVert[0].y, (thick * 0.5f - backDepth)),
        };

        for (int i = 0; i < vert.Length; i++)
        {
            vert[i] = trans.MultiplyPoint(vert[i]);
        }

        Vector2[] uv = new Vector2[vert.Length];
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = vert[i];
            uv[i] += uvRandom;
        }

        int[] tri =
        {
            0, 1, 3, 1, 2, 3,
            4, 5, 7, 5, 6, 7
        };

        return GenerateMesh(vert, null, uv, tri);
    }

    #endregion

    #region Combined
    public Mesh FlatCombined(float width, float height, float thick, Vector3 origin, Vector3 direction)
    {
        Mesh newMesh = Panel(width, height, thick, origin, direction);
        Vector2 uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        Vector2[] newUv = new Vector2[newMesh.uv.Length];
        for(int i = 0; i < newMesh.uv.Length; i++)
        {
            newUv[i] = newMesh.uv[i] + uvRandom;
        }
        newMesh.uv = newUv;

        return newMesh;
    }
    public Mesh FlatGlassCombined (float width, float height, float thick, Vector3 origin, Vector3 direction)
    {
        Mesh newMesh = Panel(width, height, thick, origin, direction);
        Vector2 uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        Vector2[] newUv = new Vector2[newMesh.uv.Length];
        for (int i = 0; i < newMesh.uv.Length; i++)
        {
            newUv[i] = newMesh.uv[i] + uvRandom;
        }
        newMesh.uv = newUv;

        newMesh.subMeshCount = 2;
        int[] newTri = {0, 0, 0};
        
        newMesh.SetTriangles(newMesh.triangles, 1);
        newMesh.SetTriangles(newTri, 0);

        return newMesh;
    }
    #endregion

    #region Helper

    private Mesh DecalcoEdge (Vector3[] oldVert, Vector2[] oldUV, int[] oldTri, Vector3 decalVec)
    {
        Matrix4x4 trans = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, decalVec);

        Vector3[] newVert = new Vector3[oldVert.Length];
        int[] newTri;
        List<int> triList = new List<int>();
        for (int i = 0; i < newVert.Length; i++)
        {
            newVert[i] = trans.MultiplyPoint(oldVert[i]);
        }
        foreach (int i in oldTri)
        {
            triList.Add(i);
        }
        triList.Reverse();
        newTri = triList.ToArray();

        return GenerateMesh(newVert, oldUV, newTri);
    }

    private Mesh ChangeMeshTransform (Mesh mesh, Vector3 origin, Vector3 direction)
    {
        Mesh newMesh = mesh;

        Matrix4x4 trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(direction), Vector3.one);

        Vector3[] newVert = new Vector3[mesh.vertexCount];
        for (int i = 0; i < newVert.Length; i++)
        {
            newVert[i] = trans.MultiplyPoint(mesh.vertices[i]);
        }

        newMesh.vertices = newVert;

        return newMesh;
    }

    #endregion

    #region Enums
    public enum DoorType
    {
        Door,
        SmallDrawer,
        LargeDrawer,
        GlassDoor,
        GlassSmallDrawer,
        GlassLargeDrawer
    }
    #endregion
}