using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dominion.FunctionLibrary;
using Dominion.CabinetDrawing;

public class CabinetGenerator : MonoBehaviour
{    
    private CabinetFactory factory;

    public LineCabinetInformation lineInfo;
    public CabinetSpec cabinetSpec;

    GameObject cabinetObj;

    public Vector3 position;
    public Vector3 rotation;

    private void Start ()
    {
        cabinetObj = GenerateCabinet();
    }

    private void Update ()
    {
        Destroy(cabinetObj);
        cabinetObj = GenerateCabinet();
        //cabinetObj.hideFlags = HideFlags.HideInHierarchy;
    }

    public GameObject GenerateCabinet()
    {
        GameObject cabinet;

        LineCabinetInformation newLineInfo = lineInfo;
        CabinetSpec newSpec = cabinetSpec;

        if(lineInfo.unit == LineCabinetInformation.Unit.inchi)
        {
            newLineInfo.faceStileWidth = lineInfo.faceStileWidth.InchiToMilli();
            newLineInfo.faceRailWidth = lineInfo.faceRailWidth.InchiToMilli();
            newLineInfo.faceOverhang = lineInfo.faceOverhang.InchiToMilli();

            newLineInfo.gapWallTopBottom = lineInfo.gapWallTopBottom.InchiToMilli();
            newLineInfo.gapBaseTopBottom = lineInfo.gapBaseTopBottom.InchiToMilli();
            newLineInfo.gapCountertop = lineInfo.gapCountertop.InchiToMilli();
            newLineInfo.gapSide = lineInfo.gapSide.InchiToMilli();
            newLineInfo.gapBetween = lineInfo.gapBetween.InchiToMilli();

            newLineInfo.toekickHeight = lineInfo.toekickHeight.InchiToMilli();
            newLineInfo.toekickOffset = lineInfo.toekickOffset.InchiToMilli();

            newLineInfo.shelfThick = lineInfo.shelfThick.InchiToMilli();

            newLineInfo.caseBackThick = lineInfo.caseBackThick.InchiToMilli();
            newLineInfo.caseSideThick = lineInfo.caseSideThick.InchiToMilli();
            newLineInfo.caseBottomThick = lineInfo.caseBottomThick.InchiToMilli();

            newLineInfo.drawerSideThick = lineInfo.drawerSideThick.InchiToMilli();
            newLineInfo.drawerBottomThick = lineInfo.drawerBottomThick.InchiToMilli();
            newLineInfo.topDrawerHeight = lineInfo.topDrawerHeight.InchiToMilli();

            newLineInfo.seperationWidth = lineInfo.seperationWidth.InchiToMilli();

            newLineInfo.doorThick = lineInfo.doorThick.InchiToMilli();

            newSpec.width = cabinetSpec.width.InchiToMilli();
            newSpec.height = cabinetSpec.height.InchiToMilli();
            newSpec.depth = cabinetSpec.depth.InchiToMilli();
        }

        if (Enum.IsDefined(typeof(WallCabinetFactory.CabinetType), newSpec.drawingData))
        {
            WallCabinetFactory.CabinetType cabinetType;
            Enum.TryParse(newSpec.drawingData, out cabinetType);
            factory = new WallCabinetFactory().cabinet(cabinetType);
            factory.lineInfo = newLineInfo;
            factory.spec = newSpec;

            cabinet = factory.GenerateCabinet();
        }
        else if (Enum.IsDefined(typeof(BaseCabinetFactory.CabinetType), newSpec.drawingData))
        {
            BaseCabinetFactory.CabinetType cabinetType;
            Enum.TryParse(newSpec.drawingData, out cabinetType);
            factory = new BaseCabinetFactory().cabinet(cabinetType);
            factory.lineInfo = newLineInfo;
            factory.spec = newSpec;

            cabinet = factory.GenerateCabinet();
        }
        else if (Enum.IsDefined(typeof(TallCabinetFactory.CabinetType), newSpec.drawingData))
        {
            TallCabinetFactory.CabinetType cabinetType;
            Enum.TryParse(newSpec.drawingData, out cabinetType);
            factory = new TallCabinetFactory().cabinet(cabinetType);
            factory.lineInfo = newLineInfo;
            factory.spec = newSpec;

            cabinet = factory.GenerateCabinet();
        }
        else
        {
            cabinet = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cabinet.name = string.Format("NotExistCode({0})", newSpec.drawingData);
            MeshRenderer meshR = cabinet.GetComponent<MeshRenderer>();
            meshR.material.color = Color.black;
            Vector3 nullScale = new Vector3(newSpec.width, newSpec.height, newSpec.depth);

            cabinet.transform.localScale = nullScale;
        }

        return cabinet;
    }
}
