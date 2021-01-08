using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMeshGenerateTest : MonoBehaviour
{
    MeshFilter filter;
    DoorMeshGenerator gen = new DoorMeshGenerator();

    public Vector3 whd;
    public Vector2 stileRail;

    public Vector3 origin;
    public Vector3 direction;

    private void Start ()
    {
        filter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Mesh> meshes = new List<Mesh>()
        {
            //gen.SquareEdge(whd.x, whd.y, stileRail.x, stileRail.y, whd.z, origin, direction),
            //gen.ShakerEdge(whd.x, whd.y, stileRail.x, stileRail.y, whd.z, origin, direction),
            //gen.RaisedPanel(whd.x, whd.y, stileRail.x, stileRail.y, whd.z, origin, direction),
            //gen.FlatPanel(whd.x, whd.y, stileRail.x, stileRail.y, whd.z, origin, direction),
            //gen.FlatCombined(whd.x, whd.y, whd.z, origin, direction),
            gen.FlatGlassCombined(whd.x, whd.y, whd.z, origin, direction),
        };
        filter.mesh = gen.Combine(meshes.ToArray());
        //GetComponent<MeshRenderer>().materials = new Material[filter.mesh.subMeshCount];
    }
}
