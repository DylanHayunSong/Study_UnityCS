using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEditor;

public class WebGLSaveLoad : MonoBehaviour
{
    public Text textUI = null;
    public string dataString = "";
    public int dataInt = 0;

    TextAsset jsonAsset;
    string dataRaw;
    [SerializeField]
    public GetJsonData datas = new GetJsonData();
    public List<DataStruct> dataList = new List<DataStruct>();

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "InitGame";

        Load();

        foreach(DataStruct dataStruct in datas.data)
        {
            dataList.Add(dataStruct);
        }

        if(datas.data[datas.data.Length - 1].date != System.DateTime.Now.ToString("MM_dd"))
        {
            DataStruct newData = new DataStruct();
            newData.date = System.DateTime.Now.ToString("MM_dd");
            dataList.Add(newData);
            datas.data = dataList.ToArray();
        }

        print(JsonConvert.SerializeObject(datas));
        print(datas.data.Length - 1);
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

        dataString = string.Format("Currnet Integer = {0}", dataInt);

        textUI.text = dataString;        
    }

    public void Save()
    {
        PlayerPrefs.SetInt("SavedInt", dataInt);
        datas.data[datas.data.Length - 1].num = dataInt;
        File.WriteAllText(Application.persistentDataPath + ("/JsonData.json"), JsonConvert.SerializeObject(datas));
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + ("/JsonData.json")) == false)
        {
            return;
        }

        jsonAsset = Resources.Load<TextAsset>("JsonData");
        dataRaw = File.ReadAllText(Application.persistentDataPath + ("/JsonData.json"));
        datas = JsonConvert.DeserializeObject<GetJsonData>(dataRaw);
        dataInt = datas.data[datas.data.Length - 1].date == System.DateTime.Now.ToString("MM_dd") ?
            datas.data[datas.data.Length - 1].num : 0;
    }
}

[Serializable]
public class GetJsonData 
{
    public DataStruct[] data;
}

[Serializable]
public struct DataStruct
{
    public string date;
    public int num;
}
