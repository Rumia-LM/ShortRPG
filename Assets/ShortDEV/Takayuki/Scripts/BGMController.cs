using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMController : MonoBehaviour
{
    public AudioClip awakeClip; // 起動時に再生するBGM
    public AudioClip loopClip;  // ループ再生するBGM

    private AudioSource awakeAudioSource; // Awake用AudioSource
    private AudioSource loopAudioSource;  // Loop用AudioSource

    [Header("Delay Settings")]
    public float awakeDelay = 1f; // Awake BGMの再生遅延時間（秒）
    public float loopDelay = 2f;  // Loop BGMの再生遅延時間（秒）

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float awakeVolume = 1f; // Awake BGMの初期ボリューム
    [Range(0f, 1f)]
    public float loopVolume = 1f;  // Loop BGMの初期ボリューム

    void Start()
    {
        // Awake用AudioSourceの設定
        awakeAudioSource = gameObject.AddComponent<AudioSource>();
        awakeAudioSource.clip = awakeClip;
        awakeAudioSource.loop = false; // ループしない
        awakeAudioSource.playOnAwake = false; // 手動で再生
        awakeAudioSource.volume = awakeVolume; // 初期ボリュームを設定

        // Loop用AudioSourceの設定
        loopAudioSource = gameObject.AddComponent<AudioSource>();
        loopAudioSource.clip = loopClip;
        loopAudioSource.loop = true; // ループ再生
        loopAudioSource.playOnAwake = false; // 手動で再生
        loopAudioSource.volume = loopVolume; // 初期ボリュームを設定

        // 遅延付きでBGMを再生
        StartCoroutine(PlayAwakeBGMWithDelay());
        StartCoroutine(PlayLoopBGMWithDelay());
    }

    IEnumerator PlayAwakeBGMWithDelay()
    {
        Debug.Log($"Waiting for {awakeDelay} seconds before playing Awake BGM...");
        yield return new WaitForSeconds(awakeDelay); // Awakeの遅延時間を待つ
        awakeAudioSource.Play();
        Debug.Log("Awake BGM started playing.");
    }

    IEnumerator PlayLoopBGMWithDelay()
    {
        Debug.Log($"Waiting for {loopDelay} seconds before playing Loop BGM...");
        yield return new WaitForSeconds(loopDelay); // Loopの遅延時間を待つ
        loopAudioSource.Play();
        Debug.Log("Loop BGM started playing.");
    }

    public void SetAwakeVolume(float volume)
    {
        awakeVolume = Mathf.Clamp(volume, 0f, 1f); // ボリュームを0〜1の範囲に制限
        awakeAudioSource.volume = awakeVolume;
        Debug.Log("Awake BGM volume set to: " + awakeVolume);
    }

    public void SetLoopVolume(float volume)
    {
        loopVolume = Mathf.Clamp(volume, 0f, 1f); // ボリュームを0〜1の範囲に制限
        loopAudioSource.volume = loopVolume;
        Debug.Log("Loop BGM volume set to: " + loopVolume);
    }

    public void StopAwakeBGM()
    {
        if (awakeAudioSource.isPlaying)
        {
            awakeAudioSource.Stop();
            Debug.Log("Awake BGM stopped.");
        }
    }

    public void StopLoopBGM()
    {
        if (loopAudioSource.isPlaying)
        {
            loopAudioSource.Stop();
            Debug.Log("Loop BGM stopped.");
        }
    }
}
