using System.Collections.Generic;
using UnityEngine;
using Dominion.FunctionLibrary;

namespace Dominion.CabinetDrawing
{
    public class BB : BaseCabinetFactory
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
    public sealed class BO : BaseCabinetFactory
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
    public class BI : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }
    public class BD : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }
    public class BDAD : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }
    public class BDFD : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }
    public class BN : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BHW : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BHWTM : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BHWS : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BS : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BSF : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BP : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BCK : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.piecut;

            return base.GenerateCabinet();
        }
    }

    public class BCP : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.piecut;

            return base.GenerateCabinet();
        }
    }

    public class BCD : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.diagonal;

            return base.GenerateCabinet();
        }
    }

    public class BCB : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BCM : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BE : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.squareEnd;

            return base.GenerateCabinet();
        }
    }

    public class BET : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.roundEnd;
            roundness = 1;

            return base.GenerateCabinet();
        }
    }

    public class BEA : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.triEnd;

            return base.GenerateCabinet();
        }
    }

    public class BER : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.softRoundEnd;
            roundness = 10;

            return base.GenerateCabinet();
        }
    }

    public class BEP : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.softRoundEnd;
            roundness = 1;

            return base.GenerateCabinet();
        }
    }

    public class BM : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BMF : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BV : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
            carcassType = CarcassType.cube;

            return base.GenerateCabinet();
        }
    }

    public class BW : BaseCabinetFactory
    {
        public override GameObject GenerateCabinet ()
        {
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
                Vector3 shelfPos = new Vector3(0, addonPosY * (i + 1) - spec.height * 0.5f, -(lineInfo.caseBackThick) * 0.5f);
                addonMesh.Add(meshGen.Panel(shelfWidth, shelfHeight, shelfDepth, shelfPos, Vector3.forward));
            }
            return meshGen.Combine(addonMesh.ToArray());
        }
    }
}