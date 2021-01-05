using System.Collections.Generic;
using UnityEngine;

namespace Dominion.CabinetDrawing
{
    public class WallCabinetFactory : CabinetFactory
    {
        public WallCabinetFactory cabinet (CabinetType type)
        {
            switch (type)
            {
                case CabinetType.WB:
                    return new WB();
                case CabinetType.WO:
                    return new WO();
                case CabinetType.WG:
                    return new WG();
                case CabinetType.WOHC:
                    return new WOHC();
                case CabinetType.WF:
                    return new WF();
                case CabinetType.WI:
                    return new WI();
                case CabinetType.WDHD:
                    return new WDHD();
                case CabinetType.WCD:
                    return new WCD();
                case CabinetType.WCS:
                    return new WCS();
                case CabinetType.WCB:
                    return new WCB();
                case CabinetType.WE:
                    return new WE();
                case CabinetType.WEA:
                    return new WEA();
                case CabinetType.WER:
                    return new WER();
                case CabinetType.WET:
                    return new WET();
                case CabinetType.WEP:
                    return new WEP();
                case CabinetType.WR:
                    return new WR();
                case CabinetType.WMDO:
                    return new WMDO();
                case CabinetType.WM:
                    return new WM();
                case CabinetType.WW:
                    return new WW();
                case CabinetType.WWL:
                    return new WWL();
                case CabinetType.WWLDO:
                    return new WWLDO();
                case CabinetType.WL:
                    return new WL();
                case CabinetType.WS:
                    return new WS();
                case CabinetType.WT:
                    return new WT();
                case CabinetType.WTD:
                    return new WTD();
                default:
                    return null;
            }
        }
        public override GameObject GenerateCabinet ()
        {
            GameObject cabinetObj = new GameObject(spec.drawingData);
            GenerateCarcass(carcassType).transform.parent = cabinetObj.transform;
            if (CabinetAddon() != null)
                CabinetAddon().transform.parent = cabinetObj.transform;
            return cabinetObj;
        }

        protected override GameObject GenerateCarcass (CarcassType type)
        {
            GameObject carcass = new GameObject("carcass");
            MeshRenderer meshR = carcass.AddComponent<MeshRenderer>();
            MeshFilter meshF = carcass.AddComponent<MeshFilter>();
            meshR.material = new Material(Shader.Find("Standard"));

            float width = spec.width;
            float height = spec.height;
            float depth = spec.depth;
            float bottomThick = lineInfo.caseBottomThick;
            float sideThick = lineInfo.caseSideThick;
            float backThcik = lineInfo.caseBackThick;
            float doorThick = lineInfo.doorThick;

            Vector3 origin = Vector3.zero;
            Mesh carcassMesh;

            switch (type)
            {
                case CarcassType.cube:
                    carcassMesh = meshGen.Cube(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, true);
                    break;
                case CarcassType.diagonal:
                    carcassMesh = meshGen.Diagonal(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, isTopBlocked: true, cabinetType: cabinetType);
                    break;
                case CarcassType.diagonalEnd:
                    carcassMesh = meshGen.DiagonalEnd(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, true, cabinetType);
                    break;
                case CarcassType.piecut:
                    carcassMesh = meshGen.Piecut(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, isTopBlocked: true, cabinetType: cabinetType);
                    break;
                case CarcassType.roundEnd:
                    carcassMesh = meshGen.RoundEnd(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, roundness, true, cabinetTopType, cabinetType);
                    break;
                case CarcassType.triEnd:
                    carcassMesh = meshGen.TriEnd(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, true, cabinetType);
                    break;
                default:
                    carcassMesh = null;
                    break;
            }

            Mesh[] meshes =
            {
            carcassMesh,
            GenerateCarcassAddon()
        };

            meshF.mesh = meshGen.Combine(meshes);

            return carcass;
        }

        //Addon
        protected override GameObject CabinetAddon ()
        {
            return null;
        }

        //WillCombineWithCarcass
        protected override Mesh GenerateCarcassAddon ()
        {
            return new Mesh();
        }

        public enum CabinetType
        {
            WB,
            WG,
            WO,
            WOHC,
            WF,
            WI,
            WDHD,
            WCD,
            WCS,
            WCB,
            WE,
            WEA,
            WER,
            WET,
            WEP,
            WR,
            WMDO,
            WM,
            WW,
            WWL,
            WWLDO,
            WL,
            WS,
            WT,
            WTD
        }
    }
}