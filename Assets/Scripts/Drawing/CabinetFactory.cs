using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CabinetFactory
{
    public LineCabinetInformation lineInfo;
    public CabinetSpec spec;
    public abstract GameObject GenerateCabinet ();


    protected float doorThick;
    protected CarcassType carcassType;
    protected MeshGenerator meshGen = new MeshGenerator();
    protected abstract GameObject GenerateCarcass (CarcassType type);
    protected abstract GameObject GenerateOuter ();
    protected abstract GameObject GenerateInner ();
    protected enum CarcassType
    {
        cube,
        diagonal,
        piecut,
        roundEnd,
        diagonalEnd,
        softRoundEnd,
        triEnd,
        squareEnd
    }
    protected enum OuterType
    {
        door,
        drawer,
        wineReck,
        hutch
    }
    protected enum InnerType
    {
        shelf, 
        intervalShelf, 
        drawer
    }
}

[System.Serializable]
public struct LineCabinetInformation
{
    public enum CabinetFaceType { frameless, framed }
    public enum CabinetOverlayType { full, half }
    public enum CabinetToekickType { sidePanel }

    public float faceStileWidth;
    public float faceRailWidth;
    public float faceOverhang;

    public float gapWallTopBottom;
    public float gapBaseTopBottom;
    public float gapCountertop;
    public float gapSide;
    public float gapBetween;

    public float toekickHeight;
    public float toekickOffset;

    public float shelfThick;

    public float caseBackThick;
    public float caseSideThick;
    public float caseBottomThick;

    public float drawerSideThick;
    public float drawerBottomThick;
    public float topDrawerHeight;

    public float seperationWidth;

    public CabinetFaceType faceType;
    public CabinetOverlayType overlayType;
    public CabinetToekickType toekickType;
}
[System.Serializable]
public struct CabinetSpec
{
    public enum DoorHinge { L, R, LR, T, N }

    public string code;
    public float width;
    public float height;
    public float depth;
    public int drawerCnt;
    public int shelfCnt;
    public DoorHinge hinge;
}