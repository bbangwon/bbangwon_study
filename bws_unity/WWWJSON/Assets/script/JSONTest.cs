using UnityEngine;
using System.Collections;
using LitJson;

public class JSONTest : MonoBehaviour {

    string url = "http://www.bbangwon.com/json/Test.json";    

	// Use this for initialization
	void Start () {
        WWWHelper helper = WWWHelper.Instance;
        helper.OnHttpRequest += OnHttpRequest;
        helper.get(100, url);	
	}

    void OnHttpRequest(int id, WWW www)
    {
        if(www.error != null)
        {
            Debug.Log("[ERROR] " + www.error);
        }
        else
        {
            Debug.Log(www.text);
        }

        JsonData data = JsonMapper.ToObject(www.text);

        int n = data["item"].Count;
        Debug.Log(data["item"][0]["name"].ToString());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
