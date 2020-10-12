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
        cam.transform.localPosition = Vector3.up * 10f + cam.transform.forward * -manager.planeMax.y*10;
        base.Initialize();
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
        cam.transform.position = cam.transform.position + new Vector3(0, newPos.y, 0);

        remainingPos -= newPos;
    }
    protected override void Rotate ()
    {
        Vector2 screenDelta;
        if(Input.GetMouseButton(0))
        {
            screenDelta = manager.GetMousePosDelta;
        }else
        {
            screenDelta  = Vector2.zero;
        }
        remainingRot.x += screenDelta.x * rotateSensitivity;
        remainingRot.y -= screenDelta.y * (rotateSensitivity/5);
        remainingRot.z = 0;
        float factor = GetDampenFactor(rotateDempening, Time.deltaTime);
        newRot = Vector3.Lerp(Vector3.zero, remainingRot, factor);
        float insRotX = cam.transform.position.y - newRot.y;

        if (insRotX < moveVerticalClamp.x || insRotX > moveVerticalClamp.y)
        {
            newRot.y = 0;
            remainingRot.y = 0;
        }
        cam.transform.position = cam.transform.position - new Vector3(0, newRot.y, 0);
        pivot.transform.eulerAngles = pivot.transform.eulerAngles + new Vector3(0, newRot.x, 0);
        remainingRot -= newRot;

    }

    protected override void Teleport ()
    {
        base.Teleport();
    }
}
