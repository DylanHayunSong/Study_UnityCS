using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLevelBase : ViewModeBase
{
    private void Start ()
    {
        manager = ViewModeManager.inst;
        thisViewMode = ViewModeManager.ViewModes.EyeLevel;
        manager.OnViewModeChanged += ChangeViewMode;
    }
    protected override void ChangeViewMode (ViewModeManager.ViewModes nextMode)
    {
        base.ChangeViewMode(nextMode);
    }
    protected override void ResetTransform ()
    {

    }
}
