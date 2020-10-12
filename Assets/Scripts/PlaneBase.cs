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
    protected override void Move ()
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
        remainingPos.x += screenDelta.x * moveSpeed;
        remainingPos.z += screenDelta.y * moveSpeed;
        float factor = GetDampenFactor(moveDempening, Time.deltaTime);
        newPos.x = Mathf.Lerp(0, remainingPos.x, factor);
        newPos.z = Mathf.Lerp(0, remainingPos.z, factor);

        pivot.transform.position += new Vector3(remainingPos.x, 0, remainingPos.z);

        remainingPos.x -= newPos.x;
        remainingPos.z -= newPos.z;
    }
    protected override void Rotate ()
    {
        base.Rotate();
    }

    protected override void Zoom ()
    {
        float scroolDelta = Input.mouseScrollDelta.y;
        if(scroolDelta != 0)
        {
            remainingPos.y = 0;
        } 

        remainingPos.y += scroolDelta * zoomSpeed;

        float factor = GetDampenFactor(zoomDempening, Time.deltaTime);

        newPos.y = Mathf.Lerp(0, remainingPos.y, factor);

        cam.transform.position -= Vector3.up * remainingPos.y;

        remainingPos.y -= newPos.y;
        
    }

    protected override void Teleport ()
    {
        base.Teleport();
    }

}
