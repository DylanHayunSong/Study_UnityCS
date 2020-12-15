using UnityEngine;

public class WBDO : WallCabinetFactory
{
    public override GameObject GenerateCabinet ()
    {
        carcassType = CarcassType.cube;

        return base.GenerateCabinet();
    }
}



