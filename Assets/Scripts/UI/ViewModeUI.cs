using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewModeUI : MonoBehaviour
{
    ViewModeManager manager;

    public Dropdown viewModeDropDown;

    // Start is called before the first frame update
    void Start()
    {
        manager = ViewModeManager.inst;
    }

    // Update is called once per frame
    void Update()
    {
        if((ViewModeManager.ViewModes)viewModeDropDown.value != manager.currentViewMode)
        {
            viewModeDropDown.value = (int)manager.currentViewMode;
        }
    }

    //
    //summary
    //  ChangeViewMode
    public void ChangeViewMode()
    {
        manager.ViewModeChange((ViewModeManager.ViewModes)viewModeDropDown.value);
    }
}
