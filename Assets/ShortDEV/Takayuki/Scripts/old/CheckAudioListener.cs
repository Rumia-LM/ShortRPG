using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAudioListener : MonoBehaviour
{
    void Start()
    {
        AudioListener listener = FindObjectOfType<AudioListener>();
        if (listener == null)
        {
            Debug.LogError("No AudioListener found in the scene. Please add one to the Main Camera.");
        }
        else
        {
            Debug.Log("AudioListener is present.");
        }
    }
}
