using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetGenerator
{
    MeshGenerator meshGen = new MeshGenerator();
    
    public void WBDO()
    {

    }

}

public class LineCabinetInformation
{
    enum CabinetFaceType { frameless, framed}
    enum CabinetOverlayType { full, half}
    enum CabinetToekickType { sidePanel}

    float faceStileWidth;
    float faceRailWidth;
    float faceOverhang;
    float gapWallTopBottom;
    float gapBaseTopBottom;
    float gapCountertop;
    float gapSide;
    float gapBetewwn;
    float toekickHeight;
    float toekickOffset;
    float shelfThick;
    float caseBackThick;
    float caseSideThick;
    float caseBottomThick;
    float drawerSideThick;
    float drawerBottomThick;
    float topDrawerHeight;
    float seperationWidth;
}


