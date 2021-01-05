using System.Collections.Generic;
using UnityEngine;
using Dominion.FunctionLibrary;

namespace Dominion.CabinetDrawing
{
    public class WB : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick * 2;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WG : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick * 2;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }

    }

    public class WO : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick * 2;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WOHC : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick * 2;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WF : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class WI : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }


        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfInterval = 1;
            shelfInterval = lineInfo.unit == LineCabinetInformation.Unit.inchi ? shelfInterval.InchiToMilli() : shelfInterval;

            float shelfWidth = spec.width - lineInfo.caseSideThick * 2;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -(lineInfo.caseBackThick - shelfInterval) * 0.5f);
                addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth - shelfInterval, shelfPos, Vector3.forward));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WDHD : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class WCD : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.diagonal;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - (lineInfo.caseSideThick + lineInfo.caseBackThick);
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, 0);
                addonMesh.Add(meshGen.DiagonalBoard(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward, cabinetType: cabinetType));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WCS : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.piecut;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - (lineInfo.caseSideThick + lineInfo.caseBackThick);
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, 0);
                addonMesh.Add(meshGen.PieBoard(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward, cabinetType: cabinetType));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WCB : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick * 2;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WE : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.roundEnd;
            roundness = 1;
            cabinetTopType = CabinetMeshGenerator.TopType.round;


            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            float shelfPosX = cabinetType == CabinetMeshGenerator.CabinetType.L ? lineInfo.caseSideThick * 0.5f : -lineInfo.caseSideThick * 0.5f;

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(shelfPosX, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.RoundBoard(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward, 1, cabinetType));
            }

            return meshGen.Combine(addonMesh.ToArray());
        }
    }
    public class WEA : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.triEnd;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            float shelfPosX = cabinetType == CabinetMeshGenerator.CabinetType.L ? lineInfo.caseSideThick * 0.5f : -lineInfo.caseSideThick * 0.5f;

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(shelfPosX, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.TriBoard(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward, cabinetType));
            }

            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WER : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.roundEnd;
            roundness = 10;
            cabinetTopType = CabinetMeshGenerator.TopType.round;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            float shelfPosX = cabinetType == CabinetMeshGenerator.CabinetType.L ? lineInfo.caseSideThick * 0.5f : -lineInfo.caseSideThick * 0.5f;

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(shelfPosX, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.RoundBoard(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward, roundness, cabinetType));
            }

            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WET : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.roundEnd;
            roundness = 10;
            cabinetTopType = CabinetMeshGenerator.TopType.rect;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            float shelfPosX = cabinetType == CabinetMeshGenerator.CabinetType.L ? lineInfo.caseSideThick * 0.5f : -lineInfo.caseSideThick * 0.5f;

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(shelfPosX, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.RoundBoard(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward, roundness, cabinetType));
            }

            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WEP : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.roundEnd;
            roundness = 2;
            cabinetTopType = CabinetMeshGenerator.TopType.diagonal;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfWidth = spec.width - lineInfo.caseSideThick;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            float shelfPosX = cabinetType == CabinetMeshGenerator.CabinetType.L ? lineInfo.caseSideThick * 0.5f : -lineInfo.caseSideThick * 0.5f;

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(shelfPosX, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                addonMesh.Add(meshGen.RoundBoard(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward, roundness, cabinetType));
            }

            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WR : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float shelfInterval = 3;
            shelfInterval = lineInfo.unit == LineCabinetInformation.Unit.inchi ? shelfInterval.InchiToMilli() : shelfInterval;

            float shelfWidth = spec.width - lineInfo.caseSideThick * 2;
            float shelfHeight = lineInfo.shelfThick;
            float shelfDepth = spec.depth - lineInfo.caseBackThick;

            float addonPosY = spec.height / (spec.shelfCnt + 1);

            List<Mesh> addonMesh = new List<Mesh>();

            for (int i = 0; i < spec.shelfCnt; i++)
            {
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -(lineInfo.caseBackThick - shelfInterval) * 0.5f);
                addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth - shelfInterval, shelfPos, Vector3.forward));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WMDO : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class WM : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class WW : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            List<Mesh> addonMesh = new List<Mesh>();
            if (spec.width < spec.height)
            {
                float shelfWidth = spec.width - lineInfo.caseSideThick * 2;
                float shelfHeight = lineInfo.shelfThick;
                float shelfDepth = spec.depth - lineInfo.caseBackThick;

                float addonPosY = spec.height / (spec.shelfCnt + 1);

                for (int i = 0; i < spec.shelfCnt; i++)
                {
                    Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -lineInfo.caseBackThick * 0.5f);
                    addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward));
                }
            }
            else
            {
                float shelfWidth = lineInfo.shelfThick;
                float shelfHeight = spec.height - lineInfo.caseSideThick * 2;
                float shelfDepth = spec.depth - lineInfo.caseBackThick;

                float addonPosX = spec.width / (spec.shelfCnt + 1);

                for (int i = 0; i < spec.shelfCnt; i++)
                {
                    Vector3 shelfPos = new Vector3(addonPosX * (i + 1) - spec.width * 0.5f, 0, -lineInfo.caseBackThick * 0.5f);
                    addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward));
                }
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }

    public class WWL : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }

        protected override Mesh GenerateCarcassAddon ()
        {
            float wineReckWidth = spec.width - lineInfo.caseSideThick;
            float wineReckHeight = spec.height - lineInfo.caseBottomThick;
            Vector3 wineReckOrigin = Vector3.back * ((spec.depth - lineInfo.caseSideThick - 0.0001f) * 0.5f);

            return meshGen.WineRack(wineReckWidth, wineReckHeight, lineInfo.caseBackThick, lineInfo.caseSideThick, wineReckOrigin, Vector3.forward);
        }
    }

    public class WWLDO : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }

        protected override GameObject CabinetAddon ()
        {

            return null;
        }
    }

    public class WL : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class WS : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class WT : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class WTD : WallCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.diagonal;

            return base.GenerateCabinet();
        }
    }
}