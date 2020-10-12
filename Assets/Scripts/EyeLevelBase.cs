using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLevelBase : ViewModeBase
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
        remainingPos += manager.GetKeyboardInputLocalAxis(pivot.transform) * moveSpeed;
        if (Input.GetMouseButton(1))
        {
            remainingPos.x += manager.GetMousePosDelta.normalized.x * moveSpeed * 2;
            remainingPos.z += manager.GetMousePosDelta.normalized.y * moveSpeed * 2;
        }
        float factor = GetDampenFactor(moveDempening, Time.deltaTime);
        newPos = Vector3.Lerp(Vector3.zero, remainingPos, factor);
        pivot.transform.position = pivot.transform.position + new Vector3(newPos.x, 0, newPos.z);
        float insPosY = cam.transform.position.y + remainingPos.y;
        if (insPosY > moveVerticalClamp.y || insPosY < moveVerticalClamp.x) 
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
        if (Input.GetMouseButton(0))
        {
            screenDelta = manager.GetMousePosDelta;
        }
        else
        {
            screenDelta = Vector2.zero;
        }
        remainingRot.y += screenDelta.x * rotateSensitivity;
        remainingRot.x -= screenDelta.y * rotateSensitivity;
        remainingRot.z = 0;
        float factor = GetDampenFactor(rotateDempening, Time.deltaTime);
        newRot = Vector3.Lerp(Vector3.zero, remainingRot, factor);
        float insRotX = cam.transform.eulerAngles.x + remainingRot.x;

        if (insRotX > rotateVerticalClamp.x && insRotX < rotateVerticalClamp.y)
        {
            newRot.x = 0;
            remainingRot.x = 0;
        }
        cam.transform.eulerAngles = cam.transform.eulerAngles + new Vector3(newRot.x, 0, 0);
        pivot.transform.eulerAngles = pivot.transform.eulerAngles + new Vector3(0, newRot.y, 0);
        remainingRot -= newRot;
    }

    protected override void Teleport ()
    {
        base.Teleport();
    }
}
