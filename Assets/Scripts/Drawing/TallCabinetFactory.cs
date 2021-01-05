using UnityEngine;

namespace Dominion.CabinetDrawing
{
    public class TallCabinetFactory : CabinetFactory
    {
        public TallCabinetFactory cabinet (CabinetType type)
        {
            switch (type)
            {
                case CabinetType.TB:
                    return new TB();
                case CabinetType.TBTW:
                    return new TBTW();
                case CabinetType.TBR:
                    return new TBR();
                case CabinetType.TP:
                    return new TP();
                case CabinetType.TO:
                    return new TO();
                case CabinetType.TOD:
                    return new TOD();
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

            float width = spec.width;
            float height = spec.height;
            float depth = spec.depth;
            float bottomThick = lineInfo.caseBottomThick;
            float sideThick = lineInfo.caseSideThick;
            float backThcik = lineInfo.caseBackThick;
            float doorThick = lineInfo.doorThick;
            float toekickHeight = lineInfo.toekickHeight;
            float toekickOffset = lineInfo.toekickOffset;

            doorThick = Mathf.Clamp(doorThick, 0, depth);

            Vector3 origin = Vector3.zero;
            Mesh carcassMesh;

            switch (type)
            {
                case CarcassType.cube:
                    carcassMesh = meshGen.Cube(width, height, depth, bottomThick, sideThick, backThcik, origin, Vector3.forward, false);
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
            TB,
            TBTW,
            TBR,
            TP,
            TO,
            TOD
        }
    }

}