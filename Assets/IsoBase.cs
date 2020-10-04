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
        remainingRot.y += screenDelta.x * rotateSensitivity;
        remainingRot.x -= screenDelta.y * (rotateSensitivity/10);
        remainingRot.z = 0;
        float factor = GetDampenFactor(rotateDempening, Time.deltaTime);
        newRot = Vector3.Lerp(Vector3.zero, remainingRot, factor);
        float insRotX = cam.transform.position.y + newRot.x;

        if (insRotX < moveVerticalClamp.x || insRotX > moveVerticalClamp.y)
        {
            newRot.x = 0;
            remainingRot.x = 0;
        }
        cam.transform.position = cam.transform.position + new Vector3(0, newRot.x, 0);
        pivot.transform.eulerAngles = pivot.transform.eulerAngles + new Vector3(0, newRot.y, 0);
        remainingRot -= newRot;

    }
}
