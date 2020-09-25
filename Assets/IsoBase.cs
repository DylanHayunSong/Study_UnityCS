using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoBase : ViewModeBase
{
    protected override void Initialize ()
    {
        isCamLookPivot = true;
        pivot.transform.position = manager.moveBoundary.transform.position;
        cam.transform.LookAt(pivot);
        cam.transform.localPosition = Vector3.up * 5f + cam.transform.forward * -10f;
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
