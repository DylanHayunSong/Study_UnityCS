﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModeBase : MonoBehaviour
{
    protected ViewModeManager.ViewModes thisViewMode;
    protected ViewModeManager manager = ViewModeManager.inst;

    public Camera cam;
    public Transform pivot;

    [Header("Move")]
    public float moveSpeed = 0.1f;
    public float moveDempening = 10f;
    public Vector2 moveVerticalClamp = new Vector2(0, 1);

    [Header("Rotate")]
    public float rotateSensitivity = 0.1f;
    public float rotateDempening = 10f;
    public Vector2 rotateVerticalClamp = new Vector2(50, 310);

    [Header("Zoom")]
    public float zoomSpeed = 0.1f;
    public float zoomDempening = 10f;
    public Vector2 zoomClamp = new Vector2(1, 10);

    protected Vector3 remainingPos;
    protected Vector3 pivotOriginalPos;
    protected Quaternion pivotOriginalRot;

    protected Vector3 remainingRot;
    protected Vector3 camOriginalPos;
    protected Quaternion camOriginalRot;

    protected bool isCamLookPivot;

    protected Vector3 newPos;
    protected Vector3 newRot;


    private void Start ()
    {
        manager = ViewModeManager.inst;        
        manager.OnViewModeChanged += ChangeViewMode;
        Initialize();
    }

    private void FixedUpdate ()
    {
        print(Input.mouseScrollDelta);
        if (isCamLookPivot)
            cam.transform.LookAt(pivot);
        if (!manager.isViewmodeChanging)
        {
            Move();
            Rotate();
            Zoom();
        }
    }

    protected virtual void Initialize ()
    {
        pivotOriginalPos = pivot.transform.position;
        pivotOriginalRot = pivot.transform.rotation;

        camOriginalPos = cam.transform.position;
        camOriginalRot = cam.transform.rotation;
    }

    protected virtual void ChangeViewMode (ViewModeManager.ViewModes nextMode)
    {
        
        if (manager.isViewmodeChanging == false)
        {
            StartCoroutine(ChangeViewModeRoutine(nextMode));
        }
        manager.isViewmodeChanging = true;
    }

    protected IEnumerator ChangeViewModeRoutine(ViewModeManager.ViewModes nextMode)
    {
        manager.viewModeObjDict[nextMode].gameObject.SetActive(true);
        manager.viewModeObjDict[nextMode].cam.gameObject.SetActive(false);

        Vector3 camPos = cam.transform.position;
        Quaternion camRot = cam.transform.rotation;
        for(float i = 0; i < 1f; i += 0.01f)
        {
            CamEasing(i, camPos, camRot);
            yield return null;
        }
        manager.OnViewModeChanged = null;
        ResetTransform();
        manager.viewModeObjDict[manager.lastViewMode].gameObject.SetActive(false);
        manager.lastViewMode = manager.currentViewMode;
        manager.viewModeObjDict[nextMode].cam.gameObject.SetActive(true);
        manager.OnViewModeChanged += manager.viewModeObjDict[manager.currentViewMode].ChangeViewMode;
        manager.isViewmodeChanging = false;

        yield return null;
    }

    protected void CamEasing(float i, Vector3 camPos, Quaternion camRot)
    {
        if(camRot.x < 0)
        {
            camRot.x = -camRot.x;
        }
        cam.transform.position = Vector3.Lerp(camPos, manager.viewModeObjDict[manager.currentViewMode].cam.transform.position, i);
        cam.transform.rotation = Quaternion.Lerp(camRot, manager.viewModeObjDict[manager.currentViewMode].cam.transform.rotation, i);
    }

    protected virtual void Move ()
    {
        
    }

    protected virtual void Rotate ()
    {

    }

    protected virtual void Zoom()
    {

    }

    protected virtual void ResetTransform ()
    {
        pivot.transform.position = pivotOriginalPos;
        pivot.transform.rotation = pivotOriginalRot;
        cam.transform.position = camOriginalPos;
        cam.transform.rotation = camOriginalRot;
    }

    protected float GetDampenFactor (float dampening, float deltaTime)
    {
        if (dampening < 0.0f)
            return 1.0f;

        if (Application.isPlaying == false)
            return 1.0f;

        return 1.0f - Mathf.Exp(-dampening * deltaTime);
    }
}
