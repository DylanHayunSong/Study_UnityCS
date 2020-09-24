using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModeBase : MonoBehaviour
{
    protected ViewModeManager.ViewModes thisViewMode;
    protected ViewModeManager manager = ViewModeManager.inst;

    public Camera cam;
    public Transform pivot;
    

    protected virtual void ChangeViewMode (ViewModeManager.ViewModes nextMode)
    {
        manager.viewModeObjDict[nextMode].SetActive(true);
    }

    protected virtual void ResetTransform()
    {

    }
}
