using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ViewModeManager : MonoBehaviour
{
    public static ViewModeManager inst = null;
    public enum ViewModes { EyeLevel = 0, Iso = 1, Plane = 2, NumberOfTypes }
    public ViewModes currentViewMode = ViewModes.EyeLevel;
    private ViewModes lastViewMode = ViewModes.EyeLevel;
    public GameObject[] viewModeObjs;

    public Vector2 planeMin = Vector2.zero;
    public Vector2 planeMax = Vector2.one;

    public Dictionary<ViewModes, GameObject> viewModeObjDict = new Dictionary<ViewModes, GameObject>();

    public Action<ViewModes> OnViewModeChanged;

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

    private void Update ()
    {
        if (lastViewMode != currentViewMode)
        {
            ViewModeChange(currentViewMode);
        }
    }

    public void ViewModeChange (ViewModes nextMode)
    {
        currentViewMode = nextMode;
        if (OnViewModeChanged != null)
        {
            OnViewModeChanged.Invoke(currentViewMode);
        }

        viewModeObjDict[lastViewMode].SetActive(false);
        lastViewMode = currentViewMode;
    }

    private void Init ()
    {
        for (int i = 0; i < (int)ViewModes.NumberOfTypes; i++)
        {
            viewModeObjDict.Add((ViewModes)i, viewModeObjs[i]);
            viewModeObjs[i].SetActive(false);
        }
        viewModeObjDict[currentViewMode].SetActive(true);
        CreateMoveBoundary();
    }

    private void CreateMoveBoundary ()
    {
        GameObject boundary = GameObject.CreatePrimitive(PrimitiveType.Plane);
        boundary.transform.localScale = new Vector3(planeMax.x - planeMin.x, 1, planeMax.y - planeMin.y);
        boundary.transform.position = new Vector3(planeMin.x + planeMax.x - planeMin.x, 0, planeMin.y + planeMax.y - planeMin.y);
        boundary.transform.parent = transform;
    }
}
