using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dominion.FunctionLibrary;

public class MeshGenerateTest : MonoBehaviour
{
    MeshFilter filter;
    MeshGenerator gen = new MeshGenerator();

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

    [Range(1, 100)]
    public int roundness;
    public bool isTopBlocked;

    public Vector3[] points;

    public MeshGenerator.TopType topType;
    public MeshGenerator.CabinetType cabinetType;


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
                filter.mesh = gen.Panel(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction);
                break;
            case GeneratorFunction.cube:
                filter.mesh = gen.Cube(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, isTopBlocked);
                break;
            case GeneratorFunction.diagonalBoard:
                filter.mesh = gen.DiagonalBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, cCutDepth, cabinetType);
                break;
            case GeneratorFunction.diagonal:
                filter.mesh = gen.Diagonal(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, cCutDepth, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.pieBoard:
                filter.mesh = gen.PieBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, cCutDepth, cabinetType);
                break;
            case GeneratorFunction.piecut:
                filter.mesh = gen.Piecut(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, cCutDepth, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.roundBoard:
                filter.mesh = gen.RoundBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, roundness, cabinetType);
                break;
            case GeneratorFunction.roundEnd:
                filter.mesh = gen.RoundEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, roundness, isTopBlocked, topType, cabinetType);
                break;
            case GeneratorFunction.diagonalEnd:
                filter.mesh = gen.DiagonalEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.softRoundBoard:
                filter.mesh = gen.SoftRoundBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, roundness, cabinetType);
                break;
            case GeneratorFunction.softRoundEnd:
                filter.mesh = gen.SoftRoundEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, roundness, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.triBoard:
                filter.mesh = gen.TriBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, cabinetType);
                break;
            case GeneratorFunction.triEnd:
                filter.mesh = gen.TriEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, isTopBlocked, cabinetType);
                break;
            case GeneratorFunction.squareBoard:
                filter.mesh = gen.SquareBoard(cWhd.x, cWhd.y, cWhd.z, cOrigin, direction, cabinetType);
                break;
            case GeneratorFunction.squareEnd:
                filter.mesh = gen.SquareEnd(cWhd.x, cWhd.y, cWhd.z, cBottomThick, cSideThick, cBackThick, cOrigin, direction, isTopBlocked, cabinetType);
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
