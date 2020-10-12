﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewModeManager : MonoBehaviour
{
    public static ViewModeManager inst = null;
    public enum ViewModes { EyeLevel = 0, Iso = 1, Plane = 2, NumberOfTypes }
    public ViewModes currentViewMode = ViewModes.EyeLevel;
    [HideInInspector]
    public ViewModes lastViewMode = ViewModes.EyeLevel;
    public ViewModeBase[] viewModeObjs;

    public Vector2 planeMin = Vector2.zero;
    public Vector2 planeMax = Vector2.one;

    public Dictionary<ViewModes, ViewModeBase> viewModeObjDict = new Dictionary<ViewModes, ViewModeBase>();

    public Action<ViewModes> OnViewModeChanged;

    public GameObject moveBoundary;

    public bool isViewmodeChanging = false;

    private Vector2 currentMousePos = Vector2.zero;
    private Vector2 lastMousePos = Vector2.zero;

    private float doubleClickTimer = 0.3f;
    private float lastClickTime;
    private bool isMouseClicked = false;

    public Vector2 GetMousePosDelta { get { return lastMousePos - currentMousePos; } }

    public Action OnMouseDoubleClick;

    private void Awake ()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void Start ()
    {
        Init();
    }

    private void FixedUpdate ()
    {
        currentMousePos = Input.mousePosition;
    }

    private void Update ()
    {
        if (lastViewMode != currentViewMode)
        {
            ViewModeChange(currentViewMode);
        }

        GetMouseDoubleClick();
    }

    private void LateUpdate ()
    {
        lastMousePos = currentMousePos;
    }

    public void ViewModeChange (ViewModes nextMode)
    {
        currentViewMode = nextMode;
        if (OnViewModeChanged != null)
        {
            OnViewModeChanged.Invoke(currentViewMode);
        }

        //viewModeObjDict[lastViewMode].SetActive(false);
        //lastViewMode = currentViewMode;
    }

    private void Init ()
    {
        for (int i = 0; i < (int)ViewModes.NumberOfTypes; i++)
        {
            viewModeObjDict.Add((ViewModes)i, viewModeObjs[i]);
        }
        CreateMoveBoundary();
    }

    private void CreateMoveBoundary ()
    {
        moveBoundary = GameObject.CreatePrimitive(PrimitiveType.Plane);
        moveBoundary.transform.localScale = new Vector3(planeMax.x - planeMin.x, 1, planeMax.y - planeMin.y);
        moveBoundary.transform.position = new Vector3(planeMin.x + planeMax.x - planeMin.x, 0, planeMin.y + planeMax.y - planeMin.y);
        moveBoundary.transform.parent = transform;
    }

    public Vector3 GetKeyboardInputLocalAxis (Transform origin)
    {
        Vector3 inputAxis = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputAxis = origin.transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputAxis = -origin.transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputAxis = origin.transform.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputAxis = -origin.transform.right;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            inputAxis = origin.transform.up;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            inputAxis = -origin.transform.up;
        }
        return inputAxis;
    }

    private void GetMouseDoubleClick ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isMouseClicked)
            {

                OnMouseDoubleClick.Invoke();
                isMouseClicked = false;
            }
            else
            {
                isMouseClicked = true;
                lastClickTime = Time.time;
            }
        }
        if (Time.time - lastClickTime > doubleClickTimer && isMouseClicked)
        {
            isMouseClicked = false;
        }
    }



}
