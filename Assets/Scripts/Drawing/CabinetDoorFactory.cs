using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoorFactory
{
    public GameObject GenerateDoor(float width, float height, float depth, DoorStyle style, DoorMeshGenerator.DoorType type)
    {
        GameObject door = new GameObject("Door");
        DoorMeshGenerator doorMeshGenerator = null;

        switch (style)
        {
            case DoorStyle.Flat:
                break;
            case DoorStyle.Shaker:
                break;
            case DoorStyle.Square:
                break;
            case DoorStyle.MiterRaised:
                break;
            default:
                break;
        }

        //doorMeshGenerator.width = width;
        //doorMeshGenerator.height = height;
        //doorMeshGenerator.depth = depth;
        //doorMeshGenerator.doorType = type;

        GameObject frame = new GameObject("Frame");
        //frame.AddComponent<MeshFilter>().mesh = doorMeshGenerator.GenerateFrameMesh();

        GameObject centerPanel = new GameObject("CenterPanel");
        //centerPanel.AddComponent<MeshFilter>().mesh = doorMeshGenerator.GenerateCenterMesh();

        frame.transform.parent = door.transform;
        centerPanel.transform.parent = door.transform;

        return door;
    }


    public enum DoorStyle
    {
        Flat,
        Shaker,
        Square,
        MadisonMiter,
        MiterRaised,
        Kent,
        Square_970
    }
}
