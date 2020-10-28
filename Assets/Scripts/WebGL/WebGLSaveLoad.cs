using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebGLSaveLoad : MonoBehaviour
{
    public Text textUI = null;
    public string data = "";
    public int dataInt = 0;


    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "InitGame";
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKey(KeyCode.RightArrow))
        {
            dataInt += 1;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            dataInt -= 1;
        }

        data = string.Format("Currnet Integer = {0}", dataInt);

        textUI.text = data;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("SavedInt", dataInt);
    }

    public void Load()
    {
        dataInt = PlayerPrefs.GetInt("SavedInt");
    }
}
