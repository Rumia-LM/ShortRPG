using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSender_m : MonoBehaviour
{
    private HttpClient_m httpClient;
    void Start()
    {
        httpClient=FindObjectOfType<HttpClient_m>();
        httpClient.SendJsonData("{\"player_name\":\"test005\",\"experience\":200, \"money\":200}");
    }
}