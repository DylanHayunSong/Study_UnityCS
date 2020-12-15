using UnityEngine;

public class TallCabinetFactory : CabinetFactory
{
    public TallCabinetFactory cabinet (CabinetType type)
    {
        switch (type)
        {
            case CabinetType.TBDO:
                return null;
            default:
                return null;
        }
    }
    public override GameObject GenerateCabinet ()
    {
        GenerateOuter();
        return null;
    }

    protected override GameObject GenerateCarcass (CarcassType type)
    {
        return null;
    }

    protected override GameObject GenerateOuter ()
    {
        return null;
    }

    protected override GameObject GenerateInner ()
    {
        return null;
    }

    public enum CabinetType
    {
        TBDO,
        TBDOS,
        TBDOR,
        TPDO,
        TODO,
        TODD
    }
}

