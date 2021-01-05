using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dominion.FunctionLibrary;

public class CabinetMeshGenerateTest : MonoBehaviour
{
    MeshFilter filter;
    CabinetMeshGenerator cabinetMeshGen = new CabinetMeshGenerator();

    public enum GeneratorFunction
    {
        panel,
        cube,
        diagonalBoard,
        diagonal,
        pieBoard,
        piecut,
        roundBoard,
        roundEnd,
        diagonalEnd,
        softRoundBoard,
        softRoundEnd,
        triBoard,
        triEnd,
        squareBoard,
        squareEnd,
        sliceDoorNormal,
        sliceDoorAlbedo,
        wineRack
    }
    public enum VisualizeUnit { Inchi, Meter }

    public VisualizeUnit unit = VisualizeUnit.Inchi;
    public GeneratorFunction func = GeneratorFunction.panel;

    public Vector3 whd;
    public Vector3 origin;
    public Vector3 direction;

    public float thick;
    public float cutDepth;
    public float sideThick;
    public float backThick;
    public float bottomThick;

    private Vector3 cWhd { get { return unit == VisualizeUnit.Inchi ? whd.InchiToMilli() : whd; } }
    private Vector3 cOrigin { get { return unit == VisualizeUnit.Inchi ? origin.InchiToMilli() : origin; } }
    private float cThick { get { return unit == VisualizeUnit.Inchi ? thick.InchiToMilli() : thick; } }
    private float cCutDepth { get { return unit == VisualizeUnit.Inchi ? cutDepth.InchiToMilli() : cutDepth; } }

    private float cSideThick { get { return unit == VisualizeUnit.Inchi ? sideThick.InchiToMilli() : sideThick; } }
    private float cBackThick { get { return unit == VisualizeUnit.Inchi ? backThick.InchiToMilli() : backThick; } }
    private float cBottomThick { get { return unit == VisualizeUnit.Inchi ? bottomThick.InchiToMilli() : bottomThick; } }
    private Vector2 cSliceCoord { get { return unit == VisualizeUnit.Inchi ? sliceCoord.InchiToMilli() : sliceCoord; } }
    private Vector2 cSliceUVCoord { get { return unit == VisualizeUnit.Inchi ? sliceUVCoord : sliceUVCoord; } }

    [Range(1, 100)]
    public int roundness;
    public bool isTopBlocked;

    public Vector3[] points;

    public CabinetMeshGenerator.TopType topType;
    public CabinetMeshGenerator.CabinetType cabinetType;

    public Vector2 sliceCoord;
    public Vector2 sliceUVCoord;

    // Start is called before the first frame update
    void Start ()
    {
        filter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update ()
    {

            MeshGenerate();
            RecalculateCollider();
        
    }

    void MeshGenerate()
    {
        switch (func)
        {
            case GeneratorFunction.panel:
                filter.mesh = cabinetMeshGen.Panel(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction);
                break;
            case GeneratorFunction.cube:
                filter.mesh = cabinetMeshGen.Cube(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, isTopBlocked);
                break;
            case GeneratorFunction.diagonalBoard:
                filter.mesh = cabinetMeshGen.DiagonalBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, cCutDepth, cabinetType);
                break;
            case GeneratorFunction.diagonal:
                filter.mesh = cabinetMeshGen.Diagonal(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, cCutDepth, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.pieBoard:
                filter.mesh = cabinetMeshGen.PieBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, cCutDepth, cabinetType);
                break;
            case GeneratorFunction.piecut:
                filter.mesh = cabinetMeshGen.Piecut(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, cCutDepth, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.roundBoard:
                filter.mesh = cabinetMeshGen.RoundBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, roundness, cabinetType);
                break;
            case GeneratorFunction.roundEnd:
                filter.mesh = cabinetMeshGen.RoundEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, roundness, isTopBlocked, topType, cabinetType);
                break;
            case GeneratorFunction.diagonalEnd:
                filter.mesh = cabinetMeshGen.DiagonalEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.softRoundBoard:
                filter.mesh = cabinetMeshGen.SoftRoundBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, roundness, cabinetType);
                break;
            case GeneratorFunction.softRoundEnd:
                filter.mesh = cabinetMeshGen.SoftRoundEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, roundness, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.triBoard:
                filter.mesh = cabinetMeshGen.TriBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, cabinetType);
                break;
            case GeneratorFunction.triEnd:
                filter.mesh = cabinetMeshGen.TriEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.squareBoard:
                filter.mesh = cabinetMeshGen.SquareBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, cabinetType);
                break;
            case GeneratorFunction.squareEnd:
                filter.mesh = cabinetMeshGen.SquareEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.sliceDoorNormal:
                filter.mesh = cabinetMeshGen.SlicedDoorNormal(cWhd.x, cWhd.y, cWhd.z, cSliceCoord, cSliceUVCoord, origin, direction);
                break;
            case GeneratorFunction.sliceDoorAlbedo:
                filter.mesh = cabinetMeshGen.SlicedDoorAlpha(cWhd.x, cWhd.y, cWhd.z, cSliceCoord, cSliceUVCoord, origin, direction);
                break;
            case GeneratorFunction.wineRack:
                filter.mesh = cabinetMeshGen.WineRack(cWhd.x - cSideThick, cWhd.y - cBottomThick, cBackThick, cSideThick, origin - Vector3.forward * ((cWhd.z - cSideThick - 0.0001f) * 0.5f), direction);
                break;
        }
    }

    void RecalculateCollider()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        MeshRenderer meshR = GetComponent<MeshRenderer>();

        collider.size = meshR.bounds.size;
    }
}
