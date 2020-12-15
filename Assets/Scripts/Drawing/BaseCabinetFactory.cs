using UnityEngine;

public class BaseCabinetFactory : CabinetFactory
{
    public BaseCabinetFactory cabinet (CabinetType type)
    {
        switch (type)
        {
            case CabinetType.BBDO:
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
        BBDO,
        BBDD,
        BBDDI,
        BBDR,
        BBDRB,
        BBDRA,
        BBDRF,
        BBDRW,
        BBDRT,
        BBDRO,
        BSDO,
        BSDOF,
        BPDD,
        BCDOK,
        BCDOP,
        BCDOD,
        BCDDB,
        BCDDM,
        BEDO,
        BEDOT,
        BEDOA,
        BENOR,
        BENOP,
        BMDR,
        BMDRF,
        BONO,
        BWNO
    }
}



