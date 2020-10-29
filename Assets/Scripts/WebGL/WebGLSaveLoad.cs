using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEditor;
using UnityEngine.Networking;
using System.Linq;

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

    private string domain = "http://gkdbs6862.dothome.co.kr";

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "InitGame";

        Load();
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
        datas.data[datas.data.Length - 1].num = dataInt;        
        StartCoroutine(SaveToWWW());
    }

    public void Load()
    {
        StartCoroutine(LoadFromWWW());
    }

    public IEnumerator SaveToWWW()
    {
        print("Hello");
        
        WWWForm form = new WWWForm();
        form.AddField("dataString", JsonConvert.SerializeObject(datas));

        UnityWebRequest www = UnityWebRequest.Post(domain + "/test.php", form);
        yield return www.SendWebRequest();

        yield return null;
    }

    public IEnumerator LoadFromWWW()
    {
        UnityWebRequest www = UnityWebRequest.Get(domain + "/JsonData.json");
        yield return www.SendWebRequest();
        yield return dataRaw = www.downloadHandler.text;
        datas = JsonConvert.DeserializeObject<GetJsonData>(dataRaw);

        if(datas.data.Length == 0)
        {
            jsonAsset = Resources.Load<TextAsset>("JsonData");
            datas = JsonConvert.DeserializeObject<GetJsonData>(jsonAsset.text);
        }

        dataInt = datas.data[datas.data.Length - 1].date == System.DateTime.Now.ToString("MM_dd") ?
            datas.data[datas.data.Length - 1].num : 0;
        dataList.Add(datas.data[0]);

        if (datas.data[datas.data.Length - 1].date != System.DateTime.Now.ToString("MM_dd"))
        {
            DataStruct newData = new DataStruct();
            newData.date = System.DateTime.Now.ToString("MM_dd");
            dataList.Add(newData);
            datas.data = dataList.ToArray();
        }

        foreach (DataStruct dataStruct in datas.data)
        {
            dataList.Add(dataStruct);
        }
        dataInt = datas.data[datas.data.Length-1].num;

        yield return null;
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
