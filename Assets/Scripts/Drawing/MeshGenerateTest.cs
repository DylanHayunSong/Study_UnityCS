﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        softRoundBoard,
        softRoundEnd,
        triBoard,
        triEnd,
        squareBoard,
        squareEnd,
    }

    public GeneratorFunction func = GeneratorFunction.panel;

    public Vector3 whd;
    public Vector3 origin;
    public Vector3 direction;
    public float thick;
    public float cutDepth;

    public float sideThick;
    public float backThick;
    public float bottomThick;

    [Range(1, 100)]
    public int roundness;
    public bool isTopBlocked;

    public Vector3[] points;

    public MeshGenerator.TopType topType;
    public MeshGenerator.EndType endType;
    public MeshGenerator.CornerType cornerType;

    // Start is called before the first frame update
    void Start ()
    {
        filter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update ()
    {
        switch (func)
        {
            case GeneratorFunction.panel:
                filter.mesh = gen.Panel(whd.x, whd.y, whd.z, origin, direction);
                break;
            case GeneratorFunction.cube:
                filter.mesh = gen.Cube(whd.x, whd.y, whd.z, thick, true);
                break;
            case GeneratorFunction.diagonalBoard:
                filter.mesh = gen.DiagonalBoard(whd.x, whd.y, whd.z, origin, direction, cutDepth, cornerType);
                break;
            case GeneratorFunction.diagonal:
                filter.mesh = gen.Diagonal(whd.x, whd.y, whd.z, bottomThick, sideThick, backThick, cutDepth, isTopBlocked, cornerType);
                break;
            case GeneratorFunction.pieBoard:
                filter.mesh = gen.PieBoard(whd.x, whd.y, whd.z, origin, direction, cutDepth, cornerType);
                break;
            case GeneratorFunction.piecut:
                filter.mesh = gen.Piecut(whd.x, whd.y, whd.z, bottomThick, sideThick, backThick, cutDepth, isTopBlocked, cornerType);
                break;
            case GeneratorFunction.roundBoard:
                filter.mesh = gen.RoundBoard(whd.x, whd.y, whd.z, origin, direction, roundness, endType);
                break;
            case GeneratorFunction.roundEnd:
                filter.mesh = gen.RoundEnd(whd.x, whd.y, whd.z, bottomThick, sideThick, backThick, roundness, isTopBlocked, topType, endType);
                break;
            case GeneratorFunction.softRoundBoard:
                filter.mesh = gen.SoftRoundBoard(whd.x, whd.y, whd.z, origin, direction, roundness, endType);
                break;
            case GeneratorFunction.softRoundEnd:
                filter.mesh = gen.SoftRoundEnd(whd.x, whd.y, whd.z, bottomThick, sideThick, backThick, roundness, isTopBlocked, endType);
                break;
            case GeneratorFunction.triBoard:
                filter.mesh = gen.TriBoard(whd.x, whd.y, whd.z, origin, direction, endType);
                break;
            case GeneratorFunction.triEnd:
                filter.mesh = gen.TriEnd(whd.x, whd.y, whd.z, bottomThick, sideThick, backThick, isTopBlocked, endType);
                break;
            case GeneratorFunction.squareBoard:
                filter.mesh = gen.SquareBoard(whd.x, whd.y, whd.z, origin, direction, endType);
                break;
            case GeneratorFunction.squareEnd:
                filter.mesh = gen.SquareEnd(whd.x, whd.y, whd.z, bottomThick, sideThick, backThick, isTopBlocked, endType);
                break;

        }
    }
}