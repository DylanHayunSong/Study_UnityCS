using UnityEngine;
using System.Collections.Generic;

namespace Dominion.CabinetDrawing
{
    public class BaseCabinetFactory : CabinetFactory
    {
        public BaseCabinetFactory cabinet (CabinetType type)
        {
            switch (type)
            {
                case CabinetType.BB:
                    return new BB();
                case CabinetType.BO:
                    return new BO();
                case CabinetType.BI:
                    return new BI();
                case CabinetType.BD:
                    return new BD();
                case CabinetType.BDAD:
                    return new BDAD();
                case CabinetType.BDFD:
                    return new BDFD();
                case CabinetType.BN:
                    return new BN();
                case CabinetType.BHW:
                    return new BHW();
                case CabinetType.BHWTM:
                    return new BHWTM();
                case CabinetType.BHWS:
                    return new BHWS();
                case CabinetType.BS:
                    return new BS();
                case CabinetType.BSF:
                    return new BSF();
                case CabinetType.BP:
                    return new BP();
                case CabinetType.BCK:
                    return new BCK();
                case CabinetType.BCP:
                    return new BCP();
                case CabinetType.BCD:
                    return new BCD();
                case CabinetType.BCB:
                    return new BCB();
                case CabinetType.BCM:
                    return new BCM();
                case CabinetType.BE:
                    return new BE();
                case CabinetType.BET:
                    return new BET();
                case CabinetType.BEA:
                    return new BEA();
                case CabinetType.BER:
                    return new BER();
                case CabinetType.BEP:
                    return new BEP();
                case CabinetType.BM:
                    return new BM();
                case CabinetType.BMF:
                    return new BMF();
                case CabinetType.BV:
                    return new BV();
                case CabinetType.BW:
                    return new BW();
                default:
                    return null;
            }
        }
        public override GameObject GenerateCabinet ()
        {
            GameObject cabinetObj = new GameObject(spec.drawingData);
            CabinetAddon();
            GenerateCarcass(carcassType).transform.parent = cabinetObj.transform;
            return cabinetObj;
        }

        protected override GameObject GenerateCarcass (CarcassType type)
        {
            GameObject carcass = new GameObject("carcass");
            MeshRenderer meshR = carcass.AddComponent<MeshRenderer>();
            MeshFilter meshF = carcass.AddComponent<MeshFilter>();
            meshR.material = new Material(Shader.Find("Standard"));

            float bottomThick = lineInfo.caseBottomThick;
            float sideThick = lineInfo.caseSideThick;
            float backThcik = lineInfo.caseBackThick;
            float doorThick = lineInfo.doorThick;
            float toekickHeight = lineInfo.toekickHeight;
            float toekickOffset = lineInfo.toekickOffset;

            float width = spec.width;
            float height = spec.height - toekickHeight;
            float depth = spec.depth;

            doorThick = Mathf.Clamp(doorThick, 0, depth);

            Vector3 origin = Vector3.up * toekickHeight * 0.5f;
            Mesh carcassMesh;

            switch (type)
            {
                case CarcassType.cube:
                    carcassMesh = meshGen.Cube(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, false);
                    break;
                case CarcassType.diagonal:
                    carcassMesh = meshGen.Diagonal(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, isTopBlocked: false, cabinetType: cabinetType);
                    break;
                case CarcassType.diagonalEnd:
                    carcassMesh = meshGen.DiagonalEnd(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, true, cabinetType);
                    break;
                case CarcassType.piecut:
                    carcassMesh = meshGen.Piecut(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, isTopBlocked: false, cabinetType: cabinetType);
                    break;
                case CarcassType.roundEnd:
                    carcassMesh = meshGen.RoundEnd(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, roundness, true, cabinetTopType, cabinetType);
                    break;
                case CarcassType.softRoundEnd:
                    carcassMesh = meshGen.SoftRoundEnd(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, roundness, true, cabinetType);
                    break;
                case CarcassType.squareEnd:
                    carcassMesh = meshGen.SquareEnd(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, true, cabinetType);
                    break;
                case CarcassType.triEnd:
                    carcassMesh = meshGen.TriEnd(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, true, cabinetType);
                    break;
                default:
                    carcassMesh = new Mesh();
                    break;
            }

            Mesh carcassAddon = GenerateCarcassAddon();
            if (carcassAddon != null)
            {
                Vector3[] newAddonVert = carcassAddon.vertices;
                Matrix4x4 addonTrans = Matrix4x4.TRS(origin, Quaternion.identity, Vector3.one);
                for (int i = 0; i < newAddonVert.Length; i++)
                {
                    newAddonVert[i] = addonTrans.MultiplyPoint(carcassAddon.vertices[i]);
                }
                carcassAddon.vertices = newAddonVert;

                Mesh[] carcassWithAddons =
                {
                carcassMesh,
                carcassAddon
            };

                meshF.mesh = meshGen.Combine(carcassWithAddons);
            }
            else
            {
                meshF.mesh = carcassMesh;
            }


            return carcass;
        }

        protected override GameObject CabinetAddon ()
        {
            return null;
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            return null;
        }

        public enum CabinetType
        {
            BO,
            BB,
            BI,
            BD,
            BN,
            BDAD,
            BDFD,
            BHW,
            BHWTM,
            BHWS,
            BS,
            BSF,
            BP,
            BCK,
            BCP,
            BCD,
            BCB,
            BCM,
            BE,
            BET,
            BEA,
            BER,
            BEP,
            BM,
            BMF,
            BV,
            BW
        }
    }



}