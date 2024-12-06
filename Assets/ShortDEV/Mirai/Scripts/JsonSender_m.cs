using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JsonSender_m : MonoBehaviour
{
    public string player_name;
    public int experience;
    public int money;
    private HttpClient_m httpClient;
    void Start()
    {
        httpClient=FindObjectOfType<HttpClient_m>();
        Debug.Log(SaveToString());
        httpClient.SendJsonData(SaveToString());
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}