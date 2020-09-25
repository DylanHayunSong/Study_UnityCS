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
        remainingPos += manager.GetKeyboardInputLocalAxis(pivot.transform) * moveSpeed;
        if (Input.GetMouseButton(1))
        {
            remainingPos += pivot.transform.forward * manager.GetMousePosDelta.normalized.y * moveSpeed;
            remainingPos.z -= manager.GetMousePosDelta.normalized.y * moveSpeed;
        }
        float factor = GetDampenFactor(moveDempening, Time.deltaTime);
        newPos = Vector3.Lerp(Vector3.zero, remainingPos, factor);
        pivot.transform.position = pivot.transform.position + new Vector3(newPos.x, 0, newPos.z);
        float insPosX = cam.transform.position.y + remainingPos.y;
        if (insPosX < moveVerticalClamp.x || insPosX > moveVerticalClamp.y)
        {
            newPos.y = 0;
            remainingPos.y = 0;
        }
        print(insPosX);
        cam.transform.position = cam.transform.position + new Vector3(0, newPos.y, 0);

        remainingPos -= newPos;
    }
    protected override void Rotate ()
    {
        base.Rotate();
    }
}
