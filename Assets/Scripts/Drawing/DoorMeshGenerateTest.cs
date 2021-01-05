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
        filter.mesh = gen.SquareEdge(whd.x, whd.y, stileRail.x, stileRail.y, whd.z, origin, direction);
    }
}
