using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeViewBase : ViewModeBase
{
    protected override void Initialize ()
    {
        isCamLookPivot = false;
        pivot.transform.position = manager.moveBoundary.transform.position + Vector3.up * 2;
        remainingPos = Vector3.zero;
        base.Initialize();
    }
    protected override void Move ()
    {
        base.Move();
    }
    protected override void Rotate ()
    {
        base.Rotate();
    }
    protected override void Teleport ()
    {
        base.Teleport();
    }
}
