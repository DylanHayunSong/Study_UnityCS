using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBase : ViewModeBase
{
    protected override void Initialize ()
    {
        isCamLookPivot = true;
        pivot.transform.position = manager.moveBoundary.transform.position;
        cam.transform.position = manager.moveBoundary.transform.position + Vector3.up * 50;
        base.Initialize();
    }
    protected override void ChangeViewMode (ViewModeManager.ViewModes nextMode)
    {
        base.ChangeViewMode(nextMode);
    }
    protected override void Move ()
    {
        base.Move();
    }
    protected override void Rotate ()
    {
        base.Rotate();
    }
}
