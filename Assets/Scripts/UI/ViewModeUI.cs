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
        
    }

    public void ChangeViweMode()
    {
        manager.ViewModeChange((ViewModeManager.ViewModes)viewModeDropDown.value);
    }
}
