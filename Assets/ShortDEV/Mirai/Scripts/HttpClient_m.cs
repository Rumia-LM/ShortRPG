using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class HttpClient_m : MonoBehaviour
{
    const string serverUrl = "http://192.168.2.120:8080/Pages/JsonServlet";

    public void SendJsonData(string jsonData)
    {
        StartCoroutine(PostRequest(jsonData));
    }

    private IEnumerator PostRequest(string jsonData)
    {
        using (UnityWebRequest request = new UnityWebRequest(serverUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("JSON送信成功: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("エラー、JSONが送れませんでした: " + request.error);
            }
        }
    }
}
