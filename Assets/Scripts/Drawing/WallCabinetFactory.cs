using UnityEngine;

public class WallCabinetFactory : CabinetFactory
{
    public WallCabinetFactory cabinet (CabinetType type)
    {
        switch (type)
        {
            case CabinetType.WBDO:
                return new WBDO();
            default:
                return null;
        }
    }
    public override GameObject GenerateCabinet ()
    {
        GenerateOuter();
        return null;
    }

    protected override GameObject GenerateCarcass(CarcassType type)
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
        WBDO,
        WBNO,
        WBDOF,
        WBDOB,
        WBDR,
        WCDOD,
        WCDOS,
        WCDOB,
        WEDO,
        WEDOA,
        WENOR,
        WENOT,
        WENOP,
        WRDO,
        WMDO,
        WMNO,
        WWNO,
        WWNOL,
        WWCOL,
        WLNO,
        WSDD,
        WTDO,
        WTDOD
    }
}