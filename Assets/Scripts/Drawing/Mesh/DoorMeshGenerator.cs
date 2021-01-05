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
    public Mesh SquareEdge(float width, float height, float stile, float rail, float thick)
    {
        return SquareEdge(width, height, stile, rail, thick, Vector3.zero, Vector3.forward);
    }
    public Mesh SquareEdge(float width, float height, float stile, float rail, float thick, Vector3 origin, Vector3 dir)
    {
        if(dir == Vector3.zero)
        {
            dir = Vector3.forward;
        } 
        List<Mesh> meshes = new List<Mesh>();
        Matrix4x4 trans;
        Vector2 uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        // smallist interval value
        float sInterval = (stile + rail + thick) * 0.03f;

        Vector3[] fVert =
        {
            // 0
            new Vector3(-(width * 0.5f), -(height * 0.5f), -(thick * 0.5f - sInterval * 2)),
            new Vector3(-(width * 0.5f), (height * 0.5f), -(thick * 0.5f - sInterval * 2)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), (height * 0.5f), -(thick * 0.5f - sInterval * 2)),
            new Vector3((width * 0.5f - (stile - sInterval * 2)), (height * 0.5f), -(thick * 0.5f - sInterval * 2)),
            new Vector3((width * 0.5f), (height * 0.5f), -(thick * 0.5f - sInterval * 2)),
            new Vector3((width * 0.5f), -(height * 0.5f), -(thick * 0.5f - sInterval * 2)),
            new Vector3((width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f), -(thick * 0.5f - sInterval * 2)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f), -(thick * 0.5f - sInterval * 2)),

            // 8
            new Vector3(-(width * 0.5f - sInterval), -(height * 0.5f - sInterval), -(thick * 0.5f - sInterval * 0.2f)),
            new Vector3(-(width * 0.5f - sInterval), (height * 0.5f - sInterval), -(thick * 0.5f - sInterval * 0.2f)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), (height * 0.5f - sInterval), -(thick * 0.5f - sInterval * 0.2f)),
            new Vector3((width * 0.5f - (stile - sInterval * 2)), (height * 0.5f - sInterval), -(thick * 0.5f - sInterval * 0.2f)),
            new Vector3((width * 0.5f - sInterval), (height * 0.5f - sInterval), -(thick * 0.5f - sInterval)),
            new Vector3((width * 0.5f - sInterval), -(height * 0.5f - sInterval), -(thick * 0.5f - sInterval)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f - sInterval), -(thick * 0.5f - sInterval * 0.2f)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f - sInterval), -(thick * 0.5f - sInterval * 0.2f)),

            // 16
            new Vector3(-(width * 0.5f - sInterval), -(height * 0.5f - sInterval), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - sInterval), (height * 0.5f - sInterval), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), (height * 0.5f - sInterval), -(thick * 0.5f)),
            new Vector3((width * 0.5f - (stile - sInterval * 2)), (height * 0.5f - sInterval), -(thick * 0.5f)),
            new Vector3((width * 0.5f - sInterval), (height * 0.5f - sInterval), -(thick * 0.5f)),
            new Vector3((width * 0.5f - sInterval), -(height * 0.5f - sInterval), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f - sInterval), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f - sInterval), -(thick * 0.5f)),

            // 24
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f - (rail - sInterval * 2)), -(thick * 0.5f)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), (height * 0.5f - (rail - sInterval * 2)), -(thick * 0.5f)),
            new Vector3((width * 0.5f - (stile - sInterval * 2)), (height * 0.5f - (rail - sInterval * 2)), -(thick * 0.5f)),
            new Vector3((width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f - (rail - sInterval * 2)), -(thick * 0.5f)),

            // 28
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f - (rail - sInterval * 2)), -(thick * 0.5f - sInterval * 0.3f)),
            new Vector3(-(width * 0.5f - (stile - sInterval * 2)), (height * 0.5f - (rail - sInterval * 2)), -(thick * 0.5f - sInterval * 0.3f)),
            new Vector3((width * 0.5f - (stile - sInterval * 2)), (height * 0.5f - (rail - sInterval * 2)), -(thick * 0.5f - sInterval * 0.3f)),
            new Vector3((width * 0.5f - (stile - sInterval * 2)), -(height * 0.5f - (rail - sInterval * 2)), -(thick * 0.5f - sInterval * 0.3f)),

            // 32
            new Vector3(-(width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - sInterval* 2)),
            new Vector3(-(width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - sInterval* 2)),
            new Vector3((width * 0.5f - stile), (height * 0.5f - rail), -(thick * 0.5f - sInterval* 2)),
            new Vector3((width * 0.5f - stile), -(height * 0.5f - rail), -(thick * 0.5f - sInterval* 2)),
        };

        #region Side
        Vector3[] LVert =
        {
            fVert[0], fVert[1], fVert[9], fVert[8],
            fVert[8], fVert[9], fVert[17],fVert[16],
            fVert[9], fVert[1], fVert[2], fVert[10],
            fVert[17], fVert[9], fVert[10], fVert[18],
            fVert[16], fVert[17], fVert[18], fVert[23],
            fVert[0], fVert[8], fVert[15], fVert[7],
            fVert[8], fVert[16], fVert[23], fVert[15],
            fVert[24], fVert[25], fVert[29], fVert[28],
            fVert[28], fVert[29], fVert[33], fVert[32],
            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f), new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f), fVert[1], fVert[0], 
            fVert[1], new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f), new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f), fVert[2],
            fVert[32], fVert[33], new Vector3(fVert[33].x, fVert[33].y, thick * 0.5f), new Vector3(fVert[32].x, fVert[32].y, thick * 0.5f),
            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f), fVert[0], fVert[7], new Vector3(fVert[7].x, fVert[7].y, thick * 0.5f),
            
            new Vector3(fVert[32].x, fVert[32].y, thick * 0.5f),
            new Vector3(fVert[33].x, fVert[33].y, thick * 0.5f),
            new Vector3(fVert[29].x, fVert[29].y, thick * 0.5f),
            new Vector3(fVert[28].x, fVert[28].y, thick * 0.5f),

            new Vector3(fVert[7].x, fVert[7].y, thick * 0.5f),
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            new Vector3(fVert[1].x, fVert[1].y, thick * 0.5f),
            new Vector3(fVert[0].x, fVert[0].y, thick * 0.5f),
        };
        Vector2[] LUV = new Vector2[LVert.Length];
        for(int i = 0; i < LUV.Length; i++)
        {
            LUV[i] = LVert[i];
            LUV[i] += uvRandom;
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

        trans = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1, 1, 1));
        Vector3[] RVert = new Vector3[LVert.Length];
        Vector2[] RUV = new Vector2[LUV.Length];
        int[] RTri;
        List<int> newTri = new List<int>();
        for(int i = 0; i < RVert.Length; i++)
        {
            RVert[i] = trans.MultiplyPoint(LVert[i]);
        } 
        foreach(int i in LTri)
        {
            newTri.Add(i);
        }
        newTri.Reverse();
        RTri = newTri.ToArray();

        meshes.Add(GenerateMesh(RVert, LUV, RTri));
        #endregion

        #region TB
        Vector3[] TVert =
        {
            fVert[10], fVert[2], fVert[3], fVert[11],
            fVert[18], fVert[10], fVert[11], fVert[19],
            fVert[25], fVert[18], fVert[19], fVert[26],
            fVert[29], fVert[25], fVert[26], fVert[30],
            fVert[33], fVert[29], fVert[30], fVert[34],
            new Vector3(fVert[33].x, fVert[33].y, thick * 0.5f), fVert[33], fVert[34], new Vector3(fVert[34].x, fVert[34].y, thick * 0.5f),
            fVert[2], new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f), new Vector3(fVert[3].x, fVert[3].y, thick * 0.5f), fVert[3],

            new Vector3(fVert[26].x, fVert[26].y, thick * 0.5f),
            new Vector3(fVert[3].x, fVert[3].y, thick * 0.5f),
            new Vector3(fVert[2].x, fVert[2].y, thick * 0.5f),
            new Vector3(fVert[25].x, fVert[25].y, thick * 0.5f),

            new Vector3(fVert[34].x, fVert[34].y, thick * 0.5f),
            new Vector3(fVert[26].x, fVert[26].y, thick * 0.5f),
            new Vector3(fVert[25].x, fVert[25].y, thick * 0.5f),
            new Vector3(fVert[33].x, fVert[33].y, thick * 0.5f),
            
        };

        uvRandom = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        trans = Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(Vector3.forward, Vector3.right), Vector3.one);
        Vector2[] TUV = new Vector2[TVert.Length];
        for(int i = 0; i < TUV.Length; i++)
        {
            TUV[i] = trans.MultiplyPoint(TVert[i]);
            TUV[i] += uvRandom;
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

        trans = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, -1, 1));
        Vector3[] BVert = new Vector3[TVert.Length];
        Vector2[] BUV = new Vector2[TUV.Length];
        int[] BTri;
        newTri = new List<int>();
        for (int i = 0; i < BVert.Length; i++)
        {
            BVert[i] = trans.MultiplyPoint(TVert[i]);
        }
        foreach (int i in TTri)
        {
            newTri.Add(i);
        }
        newTri.Reverse();
        BTri = newTri.ToArray();

        meshes.Add(GenerateMesh(BVert, TUV, BTri));

        #endregion

        trans = Matrix4x4.TRS(origin, Quaternion.LookRotation(dir), Vector3.one);
        Mesh newMesh = Combine(meshes.ToArray());
        Vector3[] newVert = new Vector3[newMesh.vertices.Length];
        for(int i = 0; i < newVert.Length; i++)
        {
            newVert[i] = trans.MultiplyPoint(newMesh.vertices[i]);
        }
        newMesh.vertices = newVert;

        return newMesh;
    }
    #endregion

    #region CenterPanel
    public Mesh RaisedPanel(float width, float height, float stile, float rail, float thick)
    {
        return RaisedPanel(width, height, stile, rail, thick, Vector3.zero, Vector3.forward);
    }

    public Mesh RaisedPanel (float width, float height, float stile, float rail, float thick, Vector3 origin, Vector3 dir)
    {

        Vector3[] fVert =
        {

        };

        return null;
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